using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Net.Http.Json;
using System.Text;
using Telegram.Bot.Types;
using WebApplication101.Model;

namespace WebApplication101
{
    internal class KnuckleBones
    {
        HttpClient client = new HttpClient();
        public int Randomizer()
        {
            Random random = new Random();
            int number = random.Next(1, 7);
            return number;

        }
        public async Task<object[]> PlayerStep(int column, int number, GameModel game, int player)
        {
            bool isFullCol = false;
            Console.WriteLine($"Player {player} choosen column:{column + 1}");
            int[,] field1;
            int[,] field2;
            List<int> DownDigits = new List<int>();
            if (player == 2)
            {
                field1 = StringtoArr(game.Field1, game.Size);
                field2 = StringtoArr(game.Field2, game.Size);
            }
            else
            {
                field1 = StringtoArr(game.Field2, game.Size);
                field2 = StringtoArr(game.Field1, game.Size);
            }
            int counter = 0;
            if (field2[game.Size-1, column] == 0)
            {
                counter = 0;
                while (field2[counter, column] != 0)
                {
                    counter++;
                }
                field2[counter, column] = number;
                
            }
            else
            {
                isFullCol = true;
            }
            if (!isFullCol)
            {
                for (int i = 0; i < game.Size; i++)
                {
                    if (field1[i, column] == number)
                    {
                        field1[i, column] = 0;
                    }
                    else
                    {
                        DownDigits.Add(field1[i, column]);
                    }
                }
                counter = 0;
                foreach (var dig in DownDigits)
                {
                    field1[counter++, column] = dig;
                }
                for (int i = counter; i < game.Size; i++)
                {
                    field1[i, column] = 0;
                }
                if (player == 2) game.Move = 1;
                else game.Move = 2;
            }
            string strField1 = "";
            string strField2 = "";
            foreach (var k in field1) strField1 += String.Join("", k);
            foreach (var k in field2) strField2 += String.Join("", k);
            if(player == 2)
            {
                game.Field1 = string.Join("", strField1);
                game.Field2 = string.Join("", strField2);
                
            }
            else
            {
                game.Field1 = string.Join("", strField2);
                game.Field2 = string.Join("", strField1);
            }

            await PutGame(game);
            await PostLog(new LogModel
            {
                Id = 1,
                DateTime = DateTime.Now.ToString("G"),
                IdGame = game.IdGame,
                IdUser1 = game.IdUser1,
                IdUser2 = game.IdUser2,
                Field1 = game.Field1,
                Field2 = game.Field2,
                Dice = game.Dice,
                Move = player==1 ? 1 : 2
            });
            object[] ret = { isFullCol, game };
            return ret;
        }

        public bool isField1Full(int[,] field1, int size)
        {
            bool full = false;
            int count = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (field1[i, j] != 0) count++;
                }
            }
            if (count == size*size) full = true;
            return full;
        }

        public bool isField2Full(int[,] field2, int size)
        {
            bool full = false;
            int count = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (field2[i, j] != 0) count++;
                }
            }
            if (count == size*size) full = true;
            return full;
        }

        public async Task<string> Ending(GameModel game, string userName)
        {
            string end = "\n";
            int sum1 = 0;
            int sum2 = 0;
            int[,] field1 = StringtoArr(game.Field1, game.Size);
            int[,] field2 = StringtoArr(game.Field2, game.Size);

            for (int i = 0; i < game.Size; i++)
            {
                HashSet<int> digitsFirst = new HashSet<int>();
                HashSet<int> digitsSecond = new HashSet<int>();
                for (int j = 0; j < game.Size; j++)
                {
                    digitsFirst.Add(field1[j, i]);
                    digitsSecond.Add(field2[j, i]);
                }
                Console.WriteLine("New");
                foreach (var dig in digitsFirst)
                {
                    int count = 0;
                    for (int j = 0; j < game.Size; j++)
                    {
                        if (dig == field1[j, i]) count++;
                    }
                    sum1 += (count * dig)*count;
                    Console.WriteLine($"Set: {dig}; count: {count}; sum: {sum1}.");
                }
                foreach (var dig in digitsSecond)
                {
                    int count = 0;
                    for (int j = 0; j < game.Size; j++)
                    {
                        if (dig == field2[j, i]) count++;
                    }
                    sum2 += (count * dig) * count;
                    Console.WriteLine($"Set: {dig}; count: {count}; sum: {sum2}.");
                }
            }
            

            Console.WriteLine($"Score of player 1 is {sum1}, score of player 2 is {sum2}.");
            end += $"Score of player 1 is {sum1}, score of player 2 is {sum2}.\n";
            if (sum1 > sum2)
            {
                Console.WriteLine("Winner is player 1.");
                end += "Winner is player 1.";
                await UpdateRating(game.IdUser1, 1, userName);
                await UpdateRating(game.IdUser2, 2, userName);
            }
            else if (sum1 < sum2)
            {
                Console.WriteLine("Winner is player 2");
                end += "Winner is player 2.";
                await UpdateRating(game.IdUser1, 2, userName);
                await UpdateRating(game.IdUser2, 1, userName);
            }
            else
            {
                Console.WriteLine("Draw.");
                end += "Draw.";
                await UpdateRating(game.IdUser1, 3, userName);
                await UpdateRating(game.IdUser2, 3, userName);
            }
            return end;
        }

        public string Print(int[,] field1, int[,] field2, int size)
        {
            string get = "";
            for (int i = size-1; i >= 0; i--)
            {
                for (int j = 0; j < size; j++)
                {
                    get += field1[i, j]+"\t";
                }
                get += "\n";
            }
            get += "-----------------\n";
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    get += field2[i, j]+"\t";
                }
                get += "\n";
            }
            return get;
        }

        public int[,] StringtoArr(string str, int size)
        {
            int moveStr = 0;
            int[,] field = new int[size, size];
            for (int i = 0; i < size; i++)
            {

                for (int j = 0; j < size; j++)
                {
                    field[i, j] = int.Parse(str[moveStr].ToString());
                    moveStr++;
                }
            }
            return field;
        }

        public async Task PutGame(GameModel game)
        {
            try
            {
                using StringContent jsonContent = new(
                System.Text.Json.JsonSerializer.Serialize(new
                {
                    idGame = game.IdGame,
                    idUser1 = game.IdUser1,
                    idUser2 = game.IdUser2,
                    field1 = game.Field1,
                    field2 = game.Field2,
                    dice = game.Dice,
                    size = game.Size,
                    move = game.Move
                }),
                Encoding.UTF8,
                "application/json");
                HttpResponseMessage response = await client.PutAsync("https://localhost:7275/api/knucklebonesApi/UpdateGame", jsonContent);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine($"ex.Message: {ex.Message}");
            }
        }

        public async Task UpdateRating(long chatId, int number, string userName)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsync($"https://localhost:7275/api/knucklebonesApi/UpdateRating/{chatId}:{number}:{userName}",null);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine($"ex.Message: {ex.Message}");
            }
        }

        public async Task PostLog(LogModel log)
        {
            try
            {
                using StringContent jsonContent = new(
                System.Text.Json.JsonSerializer.Serialize(new
                {
                    id = log.Id,
                    dateTime = log.DateTime,
                    idGame = log.IdGame,
                    idUser1 = log.IdUser1,
                    idUser2 = log.IdUser2,
                    field1 = log.Field1,
                    field2 = log.Field2,
                    dice = log.Dice,
                    move = log.Move
                }),
                Encoding.UTF8,
                "application/json");
                HttpResponseMessage response = await client.PostAsync("https://localhost:7275/api/knucklebonesApi/SaveLog", jsonContent);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine($"ex.Message: {ex.Message}");
            }
        }

        public async Task AddLobby(long chatId)
        {
            try
            {
                using StringContent jsonContent = new(
                System.Text.Json.JsonSerializer.Serialize(new
                {
                    idgame = 0,
                    idUser1 = 0,
                    idUser2 = chatId,
                    status = "",
                    lobbyName = "",
                    type = "",
                    password = "",
                    size = 0
                }),
                Encoding.UTF8,
                "application/json");
                HttpResponseMessage response = await client.PutAsync("https://localhost:7275/api/KnucklebonesApi/AddUser", jsonContent);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine($"ex.Message: {ex.Message}");
            }
        }

        public async Task CreateLobby(long chatId, string name)
        {
            try
            {
                using StringContent jsonContent = new(
                System.Text.Json.JsonSerializer.Serialize(new
                {
                    idgame = 0,
                    idUser1 = chatId,
                    idUser2 = 0,
                    status = "Creating a lobby",
                    lobbyName = name,
                    type = "",
                    password = "",
                    size = 0
                }),
                Encoding.UTF8,
                "application/json");
                HttpResponseMessage response = await client.PostAsync("https://localhost:7275/api/KnucklebonesApi/SaveLobby", jsonContent);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine($"ex.Message: {ex.Message}");
            }
        }
  
        public async Task<LobbyModel> GetLobby(long chatId)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"https://localhost:7275/api/knucklebonesApi/GetLobbyByChatId/{chatId}");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var resp1 = await response.Content.ReadAsStreamAsync();
                LobbyModel resp2;
                using (var sr = new StreamReader(resp1))
                {
                    var reader = new JsonTextReader(sr);
                    var jObject = JObject.Load(reader);

                    resp2 = new LobbyModel()
                    {
                        IdGame = jObject["responseData"]["idGame"].Value<int>(),
                        IdUser1 = jObject["responseData"]["idUser1"].Value<long>(),
                        IdUser2 = jObject["responseData"]["idUser2"].Value<long>(),
                        Status = jObject["responseData"]["status"].Value<string>(),
                        LobbyName = jObject["responseData"]["lobbyName"].Value<string>(),
                        Type = jObject["responseData"]["type"].Value<string>(),
                        Password = jObject["responseData"]["password"].Value<string>(),
                        Size = jObject["responseData"]["size"].Value<int>()
                    };
                }
                
               Console.WriteLine(responseBody);
               return resp2;
                
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine($"ex.Message: {ex.Message}");
               return new LobbyModel
               {
                   IdGame = 999,
                   IdUser1 = 0,
                   IdUser2 = 0,
                   Status = "",
                   LobbyName = "",
                   Type = "",
                   Password = "",
                   Size = 0
               };
            }
        }

        public async Task<string> GetTop()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"https://localhost:7275/api/knucklebonesApi/GetTop10Rating");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var stream = await response.Content.ReadAsStreamAsync();
                string resp = "";

                Console.WriteLine(responseBody);
                using (var sr = new StreamReader(stream))
                {
                    var reader = new JsonTextReader(sr);
                    var jObject = JObject.Load(reader)["responseData"];
                    int count = 1;
                    foreach (var j in jObject)
                    {
                        resp += count++ + ": ";
                        resp += "Username: " + j["userName"].Value<String>() + ";\n";
                        resp += "Wins: " + j["win"].Value<String>() + "; ";
                        resp += "Loses: " + j["lose"].Value<String>() + "; ";
                        resp += "Draws: " + j["draw"].Value<String>() + "; ";
                        resp += "Points: " + j["points"].Value<String>() + "; ";
                        resp += "\n--------------------------\n";
                    }
                }
                return resp;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine($"ex.Message: {ex.Message}");
                return ex.Message;
            }
        }

        public async Task<string> GetUserStats(long chatId, string userName)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"https://localhost:7275/api/knucklebonesApi/GetRatingByChatId/{chatId}:{userName}");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var stream = await response.Content.ReadAsStreamAsync();
                string resp = "";
                
                Console.WriteLine(responseBody);
                using (var sr = new StreamReader(stream))
                {
                    var reader = new JsonTextReader(sr);
                    var jObject = JObject.Load(reader);

                    resp += "Position: " + jObject["responseData"]["position"].Value<int>() + "; ";
                    resp += "Wins: " + jObject["responseData"]["data"]["win"].Value<String>()+"; ";
                    resp += "Loses: " + jObject["responseData"]["data"]["lose"].Value<String>() + "; ";
                    resp += "Draws: " + jObject["responseData"]["data"]["draw"].Value<String>() + "; ";
                    resp += "Points: " + jObject["responseData"]["data"]["points"].Value<String>() + "; ";
                }
                return resp;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine($"ex.Message: {ex.Message}");
                return ex.Message;
            }
        }

        public async Task<string> GetFreeLobbies()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:7275/api/knucklebonesApi/GetFreeLobbies");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var stream = await response.Content.ReadAsStreamAsync();
                string resp = "";
                Console.WriteLine(responseBody);
                using (var sr = new StreamReader(stream))
                {
                    var reader = new JsonTextReader(sr);
                    var jObject = JObject.Load(reader);

                    resp = jObject["responseData"].ToString();
                }
                return resp;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine($"ex.Message: {ex.Message}");
                return ex.Message;
            }
        }
        public async Task<LobbyModel> GetRandomLobby(int size)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:7275/api/knucklebonesApi/GetRandomLobby/"+size);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);
                var resp1 = await response.Content.ReadAsStreamAsync();
                LobbyModel resp2;
                using (var sr = new StreamReader(resp1))
                {
                    var reader = new JsonTextReader(sr);
                    var jObject = JObject.Load(reader);

                    resp2 = new LobbyModel()
                    {
                        IdGame = jObject["responseData"]["idGame"].Value<int>(),
                        IdUser1 = jObject["responseData"]["idUser1"].Value<long>(),
                        IdUser2 = jObject["responseData"]["idUser2"].Value<long>(),
                        Status = jObject["responseData"]["status"].Value<string>(),
                        LobbyName = jObject["responseData"]["lobbyName"].Value<string>(),
                        Type = jObject["responseData"]["type"].Value<string>(),
                        Password = jObject["responseData"]["password"].Value<string>(),
                        Size = jObject["responseData"]["size"].Value<int>()
                    };
                }
                return resp2;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine($"ex.Message: {ex.Message}");

                return new LobbyModel()
                {
                    IdGame = 999,
                    IdUser1 = 0,
                    IdUser2 = 0,
                    Status = "",
                    LobbyName = "",
                    Type = "",
                    Password = "",
                    Size = 0
                };
            }
        }

        public async Task<LobbyModel> GetLobbyByName(string name)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:7275/api/knucklebonesApi/GetLobbyByName/"+name);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);
                var resp1 = await response.Content.ReadAsStreamAsync();
                LobbyModel resp2;
                using (var sr = new StreamReader(resp1))
                {
                    var reader = new JsonTextReader(sr);
                    var jObject = JObject.Load(reader);

                    resp2 = new LobbyModel()
                    {
                        IdGame = jObject["responseData"]["idGame"].Value<int>(),
                        IdUser1 = jObject["responseData"]["idUser1"].Value<long>(),
                        IdUser2 = jObject["responseData"]["idUser2"].Value<long>(),
                        Status = jObject["responseData"]["status"].Value<string>(),
                        LobbyName = jObject["responseData"]["lobbyName"].Value<string>(),
                        Type = jObject["responseData"]["type"].Value<string>(),
                        Password = jObject["responseData"]["password"].Value<string>(),
                        Size = jObject["responseData"]["size"].Value<int>()
                    };
                }
                return resp2;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine($"ex.Message: {ex.Message}");

                return new LobbyModel()
                {
                    IdGame = 0,
                    IdUser1 = 0,
                    IdUser2 = 0,
                    Status = "",
                    LobbyName = "",
                    Type = "",
                    Password = "",
                    Size = 0
                };
            }
        }
        public async Task<GameModel> GetGame(long chatId)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"https://localhost:7275/api/knucklebonesApi/GetGameByChatId/{chatId}");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var resp1 = await response.Content.ReadAsStreamAsync();
                GameModel resp2;
                using (var sr = new StreamReader(resp1))
                {
                    var reader = new JsonTextReader(sr);
                    var jObject = JObject.Load(reader);

                    resp2 = new GameModel()
                    {
                        IdGame = jObject["responseData"]["idGame"].Value<int>(),
                        IdUser1 = jObject["responseData"]["idUser1"].Value<long>(),
                        IdUser2 = jObject["responseData"]["idUser2"].Value<long>(),
                        Field1 = jObject["responseData"]["field1"].Value<string>(),
                        Field2 = jObject["responseData"]["field2"].Value<string>(),
                        Dice = jObject["responseData"]["dice"].Value<int>(),
                        Size = jObject["responseData"]["size"].Value<int>(),
                        Move = jObject["responseData"]["move"].Value<int>()
                    };
                }

                Console.WriteLine(responseBody);
                return resp2;

            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine($"ex.Message: {ex.Message}");
                return new GameModel
                {
                    IdGame = 999,
                    IdUser1 = 0,
                    IdUser2 = 0,
                    Field1 = "",
                    Field2 = "",
                    Dice = 0,
                    Size = 0,
                    Move = 0
                };
            }
        }
        public async Task PutLobby(LobbyModel lobby)
        {
            try
            {
                using StringContent jsonContent = new(
                System.Text.Json.JsonSerializer.Serialize(new
                {
                    idGame = lobby.IdGame,
                    idUser1 = lobby.IdUser1,
                    idUser2 = lobby.IdUser2,
                    status = lobby.Status,
                    lobbyName = lobby.LobbyName,
                    type = lobby.Type,
                    password = lobby.Password,
                    size = lobby.Size

                }),
                Encoding.UTF8,
                "application/json");
                HttpResponseMessage response = await client.PutAsync("https://localhost:7275/api/knucklebonesApi/UpdateLobby", jsonContent);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine($"ex.Message: {ex.Message}");
            }
        }

        public async Task DeleteLobby(int id)
        {
            try
            {
                HttpResponseMessage responseLobby = await client.DeleteAsync("https://localhost:7275/api/knucklebonesApi/DeleteLobby/" + id);
                responseLobby.EnsureSuccessStatusCode();
                string responseBodyLobby = await responseLobby.Content.ReadAsStringAsync();

                Console.WriteLine(responseBodyLobby);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine($"ex.Message: {ex.Message}");
            }
        }
        public async Task DeleteGame(int id)
        {
            try
            {
                HttpResponseMessage responseGame = await client.DeleteAsync("https://localhost:7275/api/knucklebonesApi/DeleteGame/" + id);
                responseGame.EnsureSuccessStatusCode();
                string responseBodyGame = await responseGame.Content.ReadAsStringAsync();

                Console.WriteLine(responseBodyGame);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine($"ex.Message: {ex.Message}");
            }
        }

        public async Task<bool> CheckUserId(long id)
        {
            try
            {
                HttpResponseMessage responseLobby = await client.GetAsync("https://localhost:7275/api/knucklebonesApi/CheckUserId/" + id);
                responseLobby.EnsureSuccessStatusCode();
                string responseBodyLobby = await responseLobby.Content.ReadAsStringAsync();

                Console.WriteLine(responseBodyLobby);

                return bool.Parse(responseBodyLobby);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine($"ex.Message: {ex.Message}");
                return true;
            }
        }

        public async Task<bool> CheckLobbyName(string name)
        {
            try
            {
                HttpResponseMessage responseLobby = await client.GetAsync("https://localhost:7275/api/knucklebonesApi/CheckName/" + name);
                responseLobby.EnsureSuccessStatusCode();
                string responseBodyLobby = await responseLobby.Content.ReadAsStringAsync();

                Console.WriteLine(responseBodyLobby);

                return bool.Parse(responseBodyLobby);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine($"ex.Message: {ex.Message}");
                return true;
            }
        }
    }
}
