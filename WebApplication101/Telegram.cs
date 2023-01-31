using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using WebApplication101;
using WebApplication101.Model;




TelegramBotClient client = new TelegramBotClient("5748587283:AAH8XVYcTeprlfNDiGixtsGyj9J0ZlM6D-U");

using var cts = new CancellationTokenSource();
var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = Array.Empty<UpdateType>()
};


int number = 0;
KnuckleBones knuckles = new KnuckleBones();

ReplyKeyboardMarkup startButton = new(new[]
{
    new KeyboardButton[] { "Start" },
})
{
    ResizeKeyboard = true
};

ReplyKeyboardMarkup chooseThree = new(new[]
{
    new KeyboardButton[] { "1", "2", "3" },
})
{
    ResizeKeyboard = true,
};

ReplyKeyboardMarkup chooseFour = new(new[]
{
    new KeyboardButton[] { "1", "2", "3", "4" },
})
{
    ResizeKeyboard = true,
};

ReplyKeyboardMarkup chooseFive = new(new[]
{
    new KeyboardButton[] { "1", "2", "3", "4", "5" },
})
{
    ResizeKeyboard = true,
};

ReplyKeyboardMarkup chooseStart = new(new[]
{
    new KeyboardButton[] { "Find by name", "Create lobby", "Random lobby" },
    new KeyboardButton[] { "List of lobbies" },
    new KeyboardButton[] { "My stats", "Top-10" }
})
{
    ResizeKeyboard = true,
};

ReplyKeyboardMarkup types = new(new[]
{
    new KeyboardButton[] { "Public", "Private"},
    new KeyboardButton[] { "Cancel" }
})
{
    ResizeKeyboard = true,
};

ReplyKeyboardMarkup sizesToCreate = new(new[]
{
    new KeyboardButton[] { "3x3", "4x4", "5x5"},
    new KeyboardButton[] { "Random" },
    new KeyboardButton[] { "Cancel" }
})
{
    ResizeKeyboard = true,
};

ReplyKeyboardMarkup sizesToRandom = new(new[]
{
    new KeyboardButton[] { "3х3", "4х4", "5х5"},
    new KeyboardButton[] { "Rаndom" },
    new KeyboardButton[] { "Cancel" }
})
{
    ResizeKeyboard = true,
};

ReplyKeyboardRemove remove = new ReplyKeyboardRemove();

ForceReplyMarkup force = new ForceReplyMarkup() { Selective = true };



client.StartReceiving(
    updateHandler: HandleUpdateAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token);


Program.Main();

cts.Cancel();

async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
{
    if (update.Message is not { } message)
        return;
    if (message.Text is not { } messageText)
        return;

    var chatId = message.Chat.Id;
    var nameChat = message.Chat.FirstName + " " + message.Chat.LastName;
    
    
    
    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");
    switch (update.Type)
    {
        case UpdateType.Message:
                if (messageText == "1")
                {
                    GameModel game = await knuckles.GetGame(chatId);
                    if (game.Size != 0)
                    {
                        await client.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Choosen column: 1",
                                replyMarkup: remove,
                                cancellationToken: cancellationToken);
                        int column = 1;

                        int chooser = 1;
                        if (chatId == game.IdUser2) chooser = 2;
                        if (chooser == game.Move) await ColumnChecker(game, chooser, cancellationToken, column - 1, nameChat);
                        else await client.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Not your turn.",
                            replyMarkup: remove,
                            cancellationToken: cancellationToken);
                    }
                }
                else if (messageText == "2")
                {
                    GameModel game = await knuckles.GetGame(chatId);
                    if (game.Size != 0)
                    {
                        Message first = await client.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Choosen column: 2",
                                replyMarkup: remove,
                                cancellationToken: cancellationToken);

                        int column = 2;

                        int chooser = 1;
                        if (chatId == game.IdUser2) chooser = 2;
                        if (chooser == game.Move) await ColumnChecker(game, chooser, cancellationToken, column - 1, nameChat);
                        else await client.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Not your turn.",
                            replyMarkup: remove,
                            cancellationToken: cancellationToken);
                    }
                }
                else if (messageText == "3")
                {
                    GameModel game = await knuckles.GetGame(chatId);
                    if (game.Size != 0)
                    {
                        await client.SendTextMessageAsync(
                                chatId: chatId,
                                text: "Choosen column: 3",
                                replyMarkup: remove,
                                cancellationToken: cancellationToken);

                        int column = 3;

                        int chooser = 1;
                        if (chatId == game.IdUser2) chooser = 2;
                        if (chooser == game.Move) await ColumnChecker(game, chooser, cancellationToken, column - 1, nameChat);
                        else await client.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Not your turn.",
                            replyMarkup: remove,
                            cancellationToken: cancellationToken);
                    }
                }
            else if (messageText == "4")
            {
                GameModel game = await knuckles.GetGame(chatId);
                if (game.Size == 4 || game.Size == 5)
                {
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Choosen column: 4",
                        replyMarkup: remove,
                        cancellationToken: cancellationToken);
                    int column = 4;
                    int chooser = 1;
                    if (chatId == game.IdUser2) chooser = 2;
                    if (chooser == game.Move) await ColumnChecker(game, chooser, cancellationToken, column - 1, nameChat);
                    else await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Not your turn.",
                        replyMarkup: remove,
                        cancellationToken: cancellationToken);
                }
                    
            }
            else if (messageText == "5")
            {
                GameModel game = await knuckles.GetGame(chatId);
                if (game.Size == 5)
                {
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Choosen column: 5",
                        replyMarkup: remove,
                        cancellationToken: cancellationToken);

                    int column = 5;
                    int chooser = 1;
                    if (chatId == game.IdUser2) chooser = 2;
                    if (chooser == game.Move) await ColumnChecker(game, chooser, cancellationToken, column - 1, nameChat);
                    else await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Not your turn.",
                        replyMarkup: remove,
                        cancellationToken: cancellationToken);
                }
            }

            else if (messageText == "Start")
            {
                await StartCheck(chatId, cancellationToken);
            }
            else if (messageText == "Create lobby" )
            {
                bool userIdCheck = await knuckles.CheckUserId(chatId);
                if (!userIdCheck)
                {
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Enter name for lobby or \"0\" to cancel:",
                        replyMarkup: force);
                }
                else
                {
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "You are already in session.",
                        replyMarkup: force);
                }

            }
            else if (messageText == "Random lobby")
            {
                bool userIdCheck = await knuckles.CheckUserId(chatId);
                if (!userIdCheck)
                {
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Choose size of fields: ",
                        replyMarkup: sizesToRandom,
                        cancellationToken: cancellationToken);
                }
                else
                {
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "You are already in session.",
                        replyMarkup: remove,
                        cancellationToken: cancellationToken);
                }
                
            }
            else if (messageText == "Find by name") {
                bool userIdCheck = await knuckles.CheckUserId(chatId);
                if (!userIdCheck)
                {
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Enter the name of the lobby or \"0\" to cancel: ",
                        replyMarkup: force);
                }
                else
                {
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "You are already in session.",
                        replyMarkup: remove);
                }

            }
            else if (messageText == "List of lobbies")
            {
                string list = "List of free lobbies:\n";
                list += await knuckles.GetFreeLobbies();

                await client.SendTextMessageAsync(
                    chatId: chatId,
                    text: list,
                    replyMarkup: chooseStart,
                    cancellationToken: cancellationToken);
            }
            else if (messageText == "Public")
            {
                LobbyModel lobby = await knuckles.GetLobby(chatId);
                lobby.Type = "public";
                await knuckles.PutLobby(lobby);


                await client.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Choose size of fields: ",
                    replyMarkup: sizesToCreate,
                    cancellationToken: cancellationToken);
            }
            else if (messageText == "Private")
            {
                LobbyModel lobby = await knuckles.GetLobby(chatId);
                lobby.Type = "private";
                await knuckles.PutLobby(lobby);

                await client.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Enter a password for lobby or \"0\" to cancel:",
                    replyMarkup: force,
                    cancellationToken: cancellationToken);
            }
            else if (messageText == "My stats")
            {
                string stats = await knuckles.GetUserStats(chatId, nameChat);

                await client.SendTextMessageAsync(
                    chatId: chatId,
                    text: stats,
                    replyMarkup: chooseStart,
                    cancellationToken: cancellationToken);
            }
            else if (messageText == "Top-10")
            {
                string top = await knuckles.GetTop();

                await client.SendTextMessageAsync(
                    chatId: chatId,
                    text: top,
                    replyMarkup: chooseStart,
                    cancellationToken: cancellationToken);
            }
            else if (messageText == "3x3")
            {
                LobbyModel lobby = await knuckles.GetLobby(chatId);
                lobby.Size = 3;
                await knuckles.PutLobby(lobby);

                await client.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Lobby created! Wait for another player.",
                    replyMarkup: remove,
                    cancellationToken: cancellationToken);
            }
            else if (messageText == "4x4")
            {
                LobbyModel lobby = await knuckles.GetLobby(chatId);
                lobby.Size = 4;
                await knuckles.PutLobby(lobby);

                await client.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Lobby created! Wait for another player.",
                    replyMarkup: remove,
                    cancellationToken: cancellationToken);
            }
            else if (messageText == "5x5")
            {
                LobbyModel lobby = await knuckles.GetLobby(chatId);
                lobby.Size = 5;
                await knuckles.PutLobby(lobby);

                await client.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Lobby created! Wait for another player.",
                    replyMarkup: remove,
                    cancellationToken: cancellationToken);
            }
            else if (messageText == "Random")
            {
                LobbyModel lobby = await knuckles.GetLobby(chatId);
                Random rand = new Random();
                lobby.Size = rand.Next(3, 6);
                await knuckles.PutLobby(lobby);

                await client.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Lobby created! Wait for another player.",
                    replyMarkup: remove,
                    cancellationToken: cancellationToken);
            }
            else if (messageText == "3х3")
            {
                await RandomLobby(chatId, cancellationToken, 3);
            }
            else if (messageText == "4х4")
            {

                await RandomLobby(chatId, cancellationToken, 4);

            }
            else if (messageText == "5х5")
            {
                await RandomLobby(chatId, cancellationToken, 5);

            }
            else if (messageText == "Rаndom")
            {
                await RandomLobby(chatId, cancellationToken, 0);
            }
            else if (messageText == "/start" || messageText == "Cancel" || messageText == "0")
            {
                bool checkUserId = await knuckles.CheckUserId(chatId);
                if (checkUserId)
                {
                    LobbyModel lobby = await knuckles.GetLobby(chatId);
                    
                    if (lobby.IdUser2 != 0 && lobby.Status == "In-game")
                    {
                        GameModel game = await knuckles.GetGame(chatId);
                        await knuckles.DeleteGame(game.IdGame);
                        await client.SendTextMessageAsync(
                            chatId: game.IdUser1,
                            text: "Stopped game.",
                            replyMarkup: chooseStart,
                            cancellationToken: cancellationToken);

                        await client.SendTextMessageAsync(
                            chatId: game.IdUser2,
                            text: "Stopped game.",
                            replyMarkup: chooseStart,
                            cancellationToken: cancellationToken);
                    }
                    if (chatId == lobby.IdUser1) await knuckles.DeleteLobby(lobby.IdGame);
                }
                string stats = await knuckles.GetUserStats(chatId, nameChat);
                Message start = await client.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Welcome to Knucklebones!",
                    replyMarkup: chooseStart,
                    cancellationToken: cancellationToken);
            }
            else if (messageText == "/stopgame")
            {
                LobbyModel lobby = await knuckles.GetLobby(chatId);
                if (lobby.IdGame != 0) await knuckles.DeleteLobby(lobby.IdGame);
                if (lobby.IdUser2 != 0 && lobby.Size != 0)
                {
                    GameModel game = await knuckles.GetGame(chatId);
                    await knuckles.DeleteGame(game.IdGame);
                    await knuckles.UpdateRating(chatId, 2, nameChat);
                    if (chatId == game.IdUser1)
                    {
                        await knuckles.UpdateRating(game.IdUser2, 1, nameChat);
                    }
                    else
                    { 
                        await knuckles.UpdateRating(game.IdUser1, 1, nameChat);
                    }

                }

                await client.SendTextMessageAsync(
                    chatId: lobby.IdUser1,
                    text: "Stopped game.",
                    replyMarkup: chooseStart,
                    cancellationToken: cancellationToken);
                if (lobby.IdUser2 != 0)
                {
                    await client.SendTextMessageAsync(
                    chatId: lobby.IdUser2,
                    text: "Stopped game.",
                    replyMarkup: chooseStart,
                    cancellationToken: cancellationToken);
                }
            }
            else if (messageText == "/rules")
            {
                await client.SendTextMessageAsync(
                    chatId: chatId,
                    text: "У игроков есть поля 3х3, которые нужно заполнить числами от 1 до 6, которые выпадают случайно. Игроки могут выбрать столбец, в который нужно вставить число. Если у игрока есть два или три числа в одном столбце, их значения складываются и умножаются на количество совпадений. Если игрок помещает число, совпадающее с одним или несколькими числами в соответствующем столбце оппонента, все совпадающие кубики его оппонента удаляются. Игра заканчивается, когда поле одного из игроков полностью заполнено.",
                    replyMarkup: chooseStart,
                    cancellationToken: cancellationToken);
            }
            if (message.ReplyToMessage != null && message.ReplyToMessage.Text.Contains("name of the lobby") && messageText != "0")
            {
                var lobby = await knuckles.GetLobbyByName(messageText);
                if (lobby.IdGame != 0)
                {
                    if (lobby.Type == "public")
                    {
                        await FindByName(chatId, cancellationToken, lobby);
                    }
                    else if (lobby.Type == "private")
                    {
                        await client.SendTextMessageAsync(
                            chatId: chatId,
                            text: "Enter lobby's password or \"0\" to cancel:",
                            replyMarkup: force);

                        lobby.IdUser2 = chatId;
                        await knuckles.PutLobby(lobby);
                    }
                }
                else
                {
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Lobby not found.",
                        replyMarkup: chooseStart);
                }
                
                
            }
            if (message.ReplyToMessage != null && message.ReplyToMessage.Text.Contains("name for lobby") && messageText != "0")
            {
                bool checkLobbyName = await knuckles.CheckLobbyName(messageText);
                if (!checkLobbyName)
                {
                    await knuckles.CreateLobby(chatId, messageText);

                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Choose a type of lobby:",
                        replyMarkup: types);
                }
                else
                {
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Name is busy. Enter the name of the lobby: ",
                        replyMarkup: force);
                }
;
            }
            if (message.ReplyToMessage != null && message.ReplyToMessage.Text.Contains("lobby's password") && messageText != "0")
            {
                var lobby = await knuckles.GetLobby(chatId);
                if (lobby.Password == messageText)
                {
                    await FindByName(chatId, cancellationToken, lobby);
                }
                else
                {
                    await client.SendTextMessageAsync(
                        chatId: chatId,
                        text: "Wrong password. Enter lobby's password:",
                        replyMarkup: force);
                }


            }
            if (message.ReplyToMessage != null && message.ReplyToMessage.Text.Contains("password for lobby") && messageText != "0")
            {
                LobbyModel lobby = await knuckles.GetLobby(chatId);
                lobby.Password = messageText;
                await knuckles.PutLobby(lobby);

                await client.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Choose size of fields: ",
                    replyMarkup: sizesToCreate,
                    cancellationToken: cancellationToken);
            }
            break;
        default:
            break;
    }

}

Task HandlePollingErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken cancellationToken)
{
    var ErrorMessage = exception switch
    {
        ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
        _ => exception.ToString()
    };

    Console.WriteLine(ErrorMessage);
    return Task.CompletedTask;
}

async Task StartCheck(long chatId, CancellationToken cancellationToken)
{
    await client.SendTextMessageAsync(
    chatId: chatId,
    text: "Waiting for another player...",
    replyMarkup: remove);

    await knuckles.AddLobby(chatId);
    LobbyModel lobby = await knuckles.GetLobby(chatId);
    if (lobby.IdUser2 != 0)
    {
        GameModel game = await knuckles.GetGame(chatId);
        Message startMessage = await client.SendTextMessageAsync(
            chatId: chatId,
            text: "Starting game...",
            replyMarkup: remove);
        await ToPlayers(game, 1, cancellationToken);
    }
        }

async Task ToPlayers(GameModel game, int chooser, CancellationToken cancellationToken)
{
    long chatIdFirst = game.IdUser1;
    long chatIdSecond = game.IdUser2;
    Message toFirst = await client.SendTextMessageAsync(
        chatId: chatIdFirst,
        text: knuckles.Print(knuckles.StringtoArr(game.Field2, game.Size), knuckles.StringtoArr(game.Field1, game.Size), game.Size),
        cancellationToken: cancellationToken);

    Message toSecond = await client.SendTextMessageAsync(
        chatId: chatIdSecond,
        text: knuckles.Print(knuckles.StringtoArr(game.Field1, game.Size), knuckles.StringtoArr(game.Field2, game.Size), game.Size),
        cancellationToken: cancellationToken);


    number = knuckles.Randomizer();
    game.Dice = number;
    await knuckles.PutGame(game);

    Message randomNumberOne = await client.SendTextMessageAsync(
    chatId: chatIdFirst,
    text: "Random number: " + number,
    cancellationToken: cancellationToken);

    Message randomNumberTwo = await client.SendTextMessageAsync(
    chatId: chatIdSecond,
    text: "Random number: " + number,
    cancellationToken: cancellationToken);
    ReplyKeyboardMarkup cols;
    if (game.Size == 3) cols = chooseThree;
    else if (game.Size == 4) cols = chooseFour;
    else cols = chooseFive;

    if (chooser == 1)
    {
        
        Message choice = await client.SendTextMessageAsync(
            chatId: chatIdFirst,
            text: "Choose a column: ",
            replyMarkup: cols,
            cancellationToken: cancellationToken);

        Message second = await client.SendTextMessageAsync(
            chatId: chatIdSecond,
            text: "Waiting for an enemy's turn...",
            cancellationToken: cancellationToken);
    } else
    {
        Message choice = await client.SendTextMessageAsync(
            chatId: chatIdSecond,
            text: "Choose a column: ",
            replyMarkup: cols,
            cancellationToken: cancellationToken);

        Message second = await client.SendTextMessageAsync(
            chatId: chatIdFirst,
            text: "Waiting for an enemy's turn...",
            cancellationToken: cancellationToken);
    }


}

async Task ColumnChecker(GameModel game, int chooser, CancellationToken cancellationToken, int column, string userName)
{
    int[,] field1 = knuckles.StringtoArr(game.Field1, game.Size);
    int[,] field2 = knuckles.StringtoArr(game.Field2, game.Size);
    int number = game.Dice;
    long second = game.IdUser2;
    long first = game.IdUser1;
    ReplyKeyboardMarkup cols;
    if (game.Size == 3) cols = chooseThree;
    else if (game.Size == 4) cols = chooseFour;
    else cols = chooseFive;
    if (chooser == 1)
    {
        object[] ret = await knuckles.PlayerStep(column, number, game,1);
        bool isFullCol = (bool)ret[0];
        game = (GameModel)ret[1];
        if (isFullCol)
        {
            Message choice = await client.SendTextMessageAsync(
                chatId: first,
                text: "Choosen column is full.\nChoose a column: ",
                replyMarkup: cols,
                cancellationToken: cancellationToken);
        }
        else
        {
            field1 = knuckles.StringtoArr(game.Field1, game.Size);
            field2 = knuckles.StringtoArr(game.Field2, game.Size);
            if (!knuckles.isField1Full(field1, game.Size))
            {
                await ToPlayers(game, 2, cancellationToken);
            }
            else
            {
                string end = knuckles.Ending(game, userName).Result;
                Message endGame1 = await client.SendTextMessageAsync(
                    chatId: first,
                    text: knuckles.Print(field2, field1, game.Size) + end,
                    replyMarkup: chooseStart,
                    cancellationToken: cancellationToken);


                Message endGame2 = await client.SendTextMessageAsync(
                    chatId: second,
                    text: knuckles.Print(field1, field2, game.Size) + end,
                    replyMarkup: chooseStart,
                    cancellationToken: cancellationToken);

                await knuckles.DeleteLobby(game.IdGame);
                await knuckles.DeleteGame(game.IdGame);
            }

        }

    } else
    {
        object[] ret = await knuckles.PlayerStep(column, number, game,2);
        bool isFullCol = (bool) ret[0];
        game = (GameModel)ret[1];
        if (isFullCol)
        {
            Message choice = await client.SendTextMessageAsync(
                chatId: second,
                text: "Choosen column is full.\nChoose a column: ",
                replyMarkup: cols,
                cancellationToken: cancellationToken);
        }
        else
        {
            field1 = knuckles.StringtoArr(game.Field1, game.Size);
            field2 = knuckles.StringtoArr(game.Field2, game.Size);
            if (!knuckles.isField2Full(field2, game.Size))
            {
                await ToPlayers(game, 1, cancellationToken);
            }
            else
            {
                string end = knuckles.Ending(game, userName).Result;
                Message endGame1 = await client.SendTextMessageAsync(
                    chatId: first,
                    text: knuckles.Print(field2, field1, game.Size) + end,
                    replyMarkup: chooseStart,
                    cancellationToken: cancellationToken);


                Message endGame2 = await client.SendTextMessageAsync(
                    chatId: second,
                    text: knuckles.Print(field1, field2, game.Size) + end,
                    replyMarkup: chooseStart,
                    cancellationToken: cancellationToken);

                await knuckles.DeleteLobby(game.IdGame);
                await knuckles.DeleteGame(game.IdGame);
            }

        }
    }
    
    
}

async Task RandomLobby(long chatId, CancellationToken cancellationToken, int size)
{
    await client.SendTextMessageAsync(
    chatId: chatId,
    text: "Looking for lobby...",
    replyMarkup: remove);

    LobbyModel randomLobby = await knuckles.GetRandomLobby(size);

    if (randomLobby.IdGame != 0)
    {
        randomLobby.IdUser2 = chatId;
        await knuckles.PutLobby(randomLobby);

        await client.SendTextMessageAsync(
           chatId: chatId,
           text: "Lobby found!");

        GameModel game = await knuckles.GetGame(chatId);

        await client.SendTextMessageAsync(
            chatId: chatId,
            text: "You are player 2. Starting game...",
            replyMarkup: remove);

        await client.SendTextMessageAsync(
            chatId: game.IdUser1,
            text: "You are player 1. Starting game...",
            replyMarkup: remove);

        await ToPlayers(game, 1, cancellationToken);
    }
    else
    {
        await client.SendTextMessageAsync(
            chatId: chatId,
            text: "Lobby not found.",
            replyMarkup: chooseStart);
    }
}

async Task FindByName(long chatId, CancellationToken cancellationToken, LobbyModel lobby)
{
        lobby.Password = "";
        lobby.IdUser2 = chatId;
        await knuckles.PutLobby(lobby);

        await client.SendTextMessageAsync(
           chatId: chatId,
           text: "Lobby found!",
           cancellationToken: cancellationToken);

        GameModel game = await knuckles.GetGame(chatId);
        await client.SendTextMessageAsync(
            chatId: chatId,
            text: "You are player 2. Starting game...",
            replyMarkup: remove,
           cancellationToken: cancellationToken);

        await client.SendTextMessageAsync(
            chatId: game.IdUser1,
            text: "You are player 1. Starting game...",
            replyMarkup: remove,
           cancellationToken: cancellationToken);

        
        await ToPlayers(game, 1, cancellationToken);
}