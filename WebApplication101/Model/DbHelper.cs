using Newtonsoft.Json.Linq;
using Telegram.Bot.Types;
using WebApplication101.EfCore;
using Game = WebApplication101.EfCore.Game;

namespace WebApplication101.Model
{
    public class DbHelper
    {
        private readonly EF_DataContext _context;
        public DbHelper(EF_DataContext context)
        {
            _context = context;
        }

        public string GetFreeLobbies()
        {
            var data = _context.Lobbies.Where(l => l.Type == "public" && l.IdUser2 == 0).Select(l => l.LobbyName).ToList();
            string str = "";
            foreach (var d in data)
            {
                str += d + "\n";
            }
            return str;
        }


        public RatingModel GetRatingByChatId(long chatId)
        {
            var row = _context.Ratings.Where(d => d.IdUser.Equals(chatId)).FirstOrDefault();
            return new RatingModel()
            {
                IdUser = row.IdUser,
                UserName = row.UserName,
                Win = row.Win,
                Lose =row.Lose,
                Draw =row.Draw,
                Points=row.Points
            };
        }
        public int GetPositionByChatId(long chatId)
        {
            List<Rating> response = new();
            int count=0;
            var row = _context.Ratings.Where(d => d.IdUser.Equals(chatId)).FirstOrDefault();
            var ratingList = _context.Ratings.OrderByDescending(a => a.Points).ToList();
            foreach (var item in ratingList)
            {
                if (item.IdUser == row.IdUser)
                {
                    break;
                }
                count++;
            }
            return count+1;
        }

        public List<Rating> GetTop10Rating()
        {
            List<Rating> response = new();
            int count = 0;
            var ratingList = _context.Ratings.OrderByDescending(a => a.Points).ToList();
            foreach (var item in ratingList)
            {
                if (count == 10)
                {
                    break;
                }
                count++;
                response.Add(item);
            }
            return response;
        }
        public GameModel GetGameByChatId(long chatId)
        {
            GameModel response = new();
            var row = _context.Games.Where(d => d.IdUser1.Equals(chatId) || d.IdUser2.Equals(chatId)).FirstOrDefault();
            return new GameModel()
            {
                IdGame = row.IdGame,
                IdUser1 = row.IdUser1,
                IdUser2 = row.IdUser2,
                Field1 = row.Field1,
                Field2 = row.Field2,
                Dice = row.Dice,
                Size = row.Size,
                Move = row.Move
            };
        }

        public LobbyModel GetLobbyByChatId(long chatId)
        {
            var row = _context.Lobbies.Where(d => d.IdUser1.Equals(chatId) || d.IdUser2.Equals(chatId)).FirstOrDefault();
            return new LobbyModel
            {
                IdGame = row.IdGame,
                IdUser1 = row.IdUser1,
                IdUser2 = row.IdUser2,
                Status = row.Status,
                LobbyName = row.LobbyName,
                Type = row.Type,
                Password = row.Password,
                Size = row.Size
            };
        }

        public int GetNextLobbyId()
        {
            try
            {
                var dataList = _context.Lobbies.ToList();
                return dataList.Last().IdGame + 1;
            }
            catch
            {
                return 1;
            }
        }

        public bool CheckIdUser2()
        {
            try
            {
                var dataList = _context.Lobbies.ToList();
                dataList.Reverse();
                return dataList.Last().IdUser2 == 0;
            }
            catch
            {
                return false;
            }
        }

        public bool CheckNameLobby(string name)
        {
            try
            {
                var dataList = _context.Lobbies.ToList();
                foreach (var item in dataList)
                {
                    if (name == item.LobbyName)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool CheckIdUserRating(long id)
        {
            try
            {
                var dataList = _context.Ratings.ToList();
                foreach (var item in dataList)
                {
                    if (id == item.IdUser)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool CheckUserIdLobby(long chatId)
        {
            try
            {
                var dataList = _context.Lobbies.ToList();
                foreach (var item in dataList)
                {
                    if (chatId == item.IdUser1 || chatId == item.IdUser2)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public int GetNextGameId()
        {
            try
            {
                var dataList = _context.Games.ToList();
                return dataList.Last().IdGame + 1;
            }
            catch
            {
                return 1;
            }
        }

        public int GetNextLogId()
        {
            try
            {
                var dataList = _context.Logs.ToList();
                return dataList.Last().Id + 1;
            }
            catch
            {
                return 1;
            }
        }

        public LobbyModel GetRandomLobby(int size)
        {
            List<Lobby> dataList;
            if (size == 0) dataList = _context.Lobbies.Where(l => l.Type == "public" && l.IdUser2 == 0).ToList();
            else dataList = _context.Lobbies.Where(l => l.Type == "public" && l.IdUser2 == 0 && l.Size == size).ToList();
            if (dataList.Count != 0)
            {
                Lobby lastLobbyData = dataList.First();

                return new LobbyModel()
                {
                    IdGame = lastLobbyData.IdGame,
                    IdUser1 = lastLobbyData.IdUser1,
                    IdUser2 = lastLobbyData.IdUser2,
                    Status = lastLobbyData.Status,
                    LobbyName = lastLobbyData.LobbyName,
                    Type = lastLobbyData.Type,
                    Password = lastLobbyData.Password,
                    Size = lastLobbyData.Size
                };
            }
            else
            {
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

        public LobbyModel GetLobbyByName(string name)
        {
            var lobby = _context.Lobbies.First(l => l.LobbyName == name && l.IdUser2 == 0);
            if (lobby != null)
            {
                return new LobbyModel()
                {
                    IdGame = lobby.IdGame,
                    IdUser1 = lobby.IdUser1,
                    IdUser2 = lobby.IdUser2,
                    Status = lobby.Status,
                    LobbyName = lobby.LobbyName,
                    Type = lobby.Type,
                    Password = lobby.Password,
                    Size = lobby.Size
                };
            }
            else
            {
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
        public void SaveLobby(LobbyModel lobbyModel)
        {
            Lobby dbTable = new()
            {
                IdGame = GetNextLobbyId(),
                IdUser1 = lobbyModel.IdUser1,
                IdUser2 = lobbyModel.IdUser2,
                Status = lobbyModel.Status,
                LobbyName = lobbyModel.LobbyName,
                Type = lobbyModel.Type,
                Password = lobbyModel.Password,
                Size = lobbyModel.Size
            };
            _context.Lobbies.Add(dbTable);
            _context.SaveChanges();
        }

        public void SaveRating(RatingModel ratingModel)
        {
            Rating dbTable = new()
            {
                IdUser = ratingModel.IdUser,
                UserName = ratingModel.UserName,
                Win = ratingModel.Win,
                Lose =  ratingModel.Lose,
                Draw = ratingModel.Draw,
                Points = ratingModel.Points
            };
            _context.Ratings.Add(dbTable);
            _context.SaveChanges();
        }

        public void SaveLobbyWithOneUser(LobbyModel lobbyModel)
        {
            Lobby dbTable = new()
            {
                IdGame = GetNextLobbyId(),
                IdUser1 = (long)lobbyModel.IdUser2,
                IdUser2 = 0,
                Status = "Waiting for another player",
                LobbyName = lobbyModel.LobbyName,
                Type = lobbyModel.Type,
                Password = lobbyModel.Password,
                Size = lobbyModel.Size
            };
            _context.Lobbies.Add(dbTable);
            _context.SaveChanges();
        }
        public void SaveGame(GameModel gameModel)
        {
            Game dbTable = new()
            {
                IdGame = GetNextGameId(),
                IdUser1 = gameModel.IdUser1,
                IdUser2 = gameModel.IdUser2,
                Field1 = gameModel.Field1,
                Field2 = gameModel.Field2,
                Dice = gameModel.Dice,
                Size = gameModel.Size,
                Move = gameModel.Move
            };
            _context.Games.Add(dbTable);
            _context.SaveChanges();
        }

        public void SaveLog(LogModel logModel)
        {
            Log dbTable = new()
            {
                Id = GetNextLogId(),
                DateTime = logModel.DateTime,
                IdGame = logModel.IdGame,
                IdUser1 = logModel.IdUser1,
                IdUser2 = logModel.IdUser2,
                Field1 = logModel.Field1,
                Field2 = logModel.Field2,
                Dice = logModel.Dice,
                Move = logModel.Move
            };
            _context.Logs.Add(dbTable);
            _context.SaveChanges();
        }

        public LobbyModel PutIdUser2(LobbyModel lobbyModel)
        {
            var order = _context.Lobbies.Where(d => d.IdGame.Equals(lobbyModel.IdGame)).FirstOrDefault();
            if (order != null)
            {
                order.IdUser2 = lobbyModel.IdUser2;
            }
            _context.SaveChanges();
            return new LobbyModel()
            {
                IdGame = order.IdGame,
                IdUser1 = order.IdUser1,
                IdUser2 = order.IdUser2,
            };
        }


        public void PutLobby(LobbyModel lobbyModel)
        {
            var order = _context.Lobbies.Where(d => d.IdGame.Equals(lobbyModel.IdGame)).FirstOrDefault();
            order.IdGame = lobbyModel.IdGame;
            order.IdUser1 = lobbyModel.IdUser1;
            order.IdUser2 = lobbyModel.IdUser2;
            order.LobbyName = lobbyModel.LobbyName;
            order.Status = lobbyModel.Status;
            order.Type = lobbyModel.Type;
            order.Password = lobbyModel.Password;
            order.Size = lobbyModel.Size;
            _context.SaveChanges();
        }

        public void PutGame(GameModel gameModel)
        {
            var order = _context.Games.Where(d => d.IdGame.Equals(gameModel.IdGame)).FirstOrDefault();
            if (order != null)
            {
                order.Field1 = gameModel.Field1;
                order.Field2 = gameModel.Field2;
                order.Dice = gameModel.Dice;
                order.Move = gameModel.Move;
            }
            _context.SaveChanges();
        }

        public void WinPoint(long id)
        {
            var order = _context.Ratings.Where(d => d.IdUser.Equals(id)).FirstOrDefault();
            if (order != null)
            {
                order.Win++;
                order.Points++;
            }
            _context.SaveChanges();
        }

        public void LosePoint(long id)
        {
            var order = _context.Ratings.Where(d => d.IdUser.Equals(id)).FirstOrDefault();
            if (order != null)
            {
                order.Lose++;
                order.Points--;
            }
            _context.SaveChanges();
        }

        public void DrawPoint(long id)
        {
            var order = _context.Ratings.Where(d => d.IdUser.Equals(id)).FirstOrDefault();
            if (order != null)
            {
                order.Draw++;
            }
            _context.SaveChanges();
        }

        public void DeleteGame(int id)
        {
            var game = _context.Games.Where(d => d.IdGame.Equals(id)).FirstOrDefault();
            if (game != null)
            {
                _context.Games.Remove(game);
                _context.SaveChanges();
            }
        }

        public void DeleteLobby(int id)
        {
            var lobby = _context.Lobbies.Where(d => d.IdGame.Equals(id)).FirstOrDefault();
            if (lobby != null)
            {
                _context.Lobbies.Remove(lobby);
                _context.SaveChanges();
            }
        }
    }
}
