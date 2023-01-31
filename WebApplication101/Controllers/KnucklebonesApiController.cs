using Microsoft.AspNetCore.Mvc;
using WebApplication101.EfCore;
using WebApplication101.Migrations;
using WebApplication101.Model;

namespace WebApplication101.Controllers
{

    [ApiController]
    public class KnucklebonesApiController : ControllerBase
    {
        private readonly DbHelper _db;
        public KnucklebonesApiController(EF_DataContext eF_DataContext)
        {
            _db = new DbHelper(eF_DataContext);
        }

        [HttpGet]
        [Route("api/[controller]/GetRatingByChatId/{chatId}:{userName}")]
        public IActionResult GetRatingByChatId(long chatId, string userName)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                RatingModel data = new();
                if (_db.CheckIdUserRating(chatId))
                {
                    data = _db.GetRatingByChatId(chatId);
                }
                else
                {
                    data = new() 
                    { 
                    IdUser = chatId,
                    UserName = userName,
                    Win = 0,
                    Lose = 0,
                    Draw = 0,
                    Points = 0
                    };
                    _db.SaveRating(data);
                }
                int position = _db.GetPositionByChatId(chatId);
                var response = new { data, position };
                if (data == null)
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetAppResponse(type, response));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpGet]
        [Route("api/[controller]/GetTop10Rating")]
        public IActionResult GetTop10Rating()
        {
            ResponseType type = ResponseType.Success;
            try
            {
                List<EfCore.Rating> data = _db.GetTop10Rating();

                if (data == null)
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetAppResponse(type, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
        [HttpGet]
        [Route("api/[controller]/GetFreeLobbies")]
        public IActionResult GetFreeLobbies()
        {
            ResponseType type = ResponseType.Success;
            try
            {
                string data = _db.GetFreeLobbies();

                if (!data.Any())
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetAppResponse(type, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpGet]
        [Route("api/[controller]/GetGameByChatId/{chatId}")]
        public IActionResult GetGameByChatId(long chatId)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                GameModel data = _db.GetGameByChatId(chatId);

                if (data == null)
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetAppResponse(type, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpGet]
        [Route("api/[controller]/GetLobbyByChatId/{chatId}")]
        public IActionResult GetLobbyByChatId(long chatId)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                LobbyModel data = _db.GetLobbyByChatId(chatId);

                if (data == null)
                {
                    data.IdGame = 0;
                }
                return Ok(ResponseHandler.GetAppResponse(type, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpGet]
        [Route("api/[controller]/GetRandomLobby/{size}")]
        public IActionResult GetRandomLobby(int size)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                LobbyModel data = _db.GetRandomLobby(size);
                if (data == null)
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetAppResponse(type, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpGet]
        [Route("api/[controller]/GetLobbyByName/{name}")]
        public IActionResult GetLobbyByName(string name)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                LobbyModel data = _db.GetLobbyByName(name);
                if (data == null)
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetAppResponse(type, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPost]
        [Route("api/[controller]/SaveLobby")]
        public IActionResult PostLobby([FromBody] LobbyModel model)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.SaveLobby(model);
                return Ok(ResponseHandler.GetAppResponse(type, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPost]
        [Route("api/[controller]/SaveGame")]
        public IActionResult PostGame([FromBody] GameModel model)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.SaveGame(model);
                return Ok(ResponseHandler.GetAppResponse(type, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPost]
        [Route("api/[controller]/SaveLog")]
        public IActionResult PostLog([FromBody] LogModel model)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.SaveLog(model);
                return Ok(ResponseHandler.GetAppResponse(type, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPut]
        [Route("api/[controller]/UpdateLobby")]
        public IActionResult PutLobby([FromBody] LobbyModel model)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                if (model.Type == "public" && model.Size != 0 || model.Type == "private" && model.Password != "" && model.Size != 0)
                {
                    model.Status = "Waiting for another player";
                }
                if (model.IdUser2 != 0 && model.Type == "public" && model.Size != 0 || model.IdUser2 != 0 && model.Type == "private" && model.Size != 0 && model.Password == "")
                {
                    model.Status = "In-game";
                    GameModel game = new()
                    {
                        IdUser1 = model.IdUser1,
                        IdUser2 = (long)model.IdUser2,
                        Field1 = new string('0', (model.Size * model.Size)),
                        Field2 = new string('0', (model.Size * model.Size)),
                        Dice = 0,
                        Size = model.Size,
                        Move = 1
                    };
                    _db.SaveGame(game);
                }
                _db.PutLobby(model);
                return Ok(ResponseHandler.GetAppResponse(type, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPut]
        [Route("api/[controller]/UpdateRating/{chatId}:{number}:{userName}")]
        public IActionResult PutRating(long chatId, int number, string userName)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                if (_db.CheckIdUserRating(chatId))
                {
                    switch (number)
                    {
                        case 1:
                            _db.WinPoint(chatId);
                            break;
                        case 2: 
                            _db.LosePoint(chatId);
                            break;
                        case 3:
                            _db.DrawPoint(chatId);
                            break;
                    }
                }
                else
                {
                    RatingModel rating = new() { IdUser = chatId, UserName = userName };
                    switch (number)
                    {
                        case 1:
                            rating.Win = 1;
                            rating.Lose = 0;
                            rating.Draw = 0;
                            rating.Points = 1;
                            break;
                        case 2:
                            rating.Win = 0;
                            rating.Lose = 1;
                            rating.Draw = 0;
                            rating.Points = -1;
                            break;
                        case 3:
                            rating.Win = 0;
                            rating.Lose = 0;
                            rating.Draw = 1;
                            rating.Points = 0;
                            break;
                    }
                    _db.SaveRating(rating);

                }
                return Ok(ResponseHandler.GetAppResponse(type, chatId));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPut]
        [Route("api/[controller]/UpdateGame")]
        public IActionResult PutGame([FromBody] GameModel model)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.PutGame(model);
                return Ok(ResponseHandler.GetAppResponse(type, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpDelete]
        [Route("api/[controller]/DeleteLobby/{id}")]
        public IActionResult DeleteLobby(int id)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.DeleteLobby(id);
                return Ok(ResponseHandler.GetAppResponse(type, "Delete Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpDelete]
        [Route("api/[controller]/DeleteGame/{id}")]
        public IActionResult DeleteGame(int id)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.DeleteGame(id);
                return Ok(ResponseHandler.GetAppResponse(type, "Delete Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }


        [HttpGet]
        [Route("api/[controller]/CheckName/{name}")]
        public bool CheckName(string name)
        {
            try
            {
                bool data = _db.CheckNameLobby(name);
                return data;
            }
            catch
            {
                return true;
            }
        }

        [HttpGet]
        [Route("api/[controller]/CheckUserId/{chatId}")]
        public bool CheckName(long chatId)
        {
            try
            {
                bool data = _db.CheckUserIdLobby(chatId);
                return data;
            }
            catch
            {
                return true;
            }
        }
    }
}
