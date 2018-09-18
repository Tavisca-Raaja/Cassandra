using Microsoft.AspNetCore.Mvc;
using TicTacToe.GameAttributes;
using TicTacToe.Exceptions;
using TicTacToe.Authorize;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TicTacToe.Controllers
{
    [Route("api/[controller]")]
    [Logging]
    [TokenCheck]
    public class GameController : Controller
    {
        [Route("matchstatus")]
        [HttpGet]
        public ActionResult GetMatchStatus()
        {
            GameProccess status = new GameProccess();
            return Ok(status.checkGameStatus());
        }

        [Route("startgame")]
        [HttpPost]
        [Authorize]
        public ActionResult StartGame([FromHeader]string player1AccessKey,[FromHeader]string player2AccessKey)
        {
            GameProccess start = new GameProccess();
            if (start.StartPlay(player1AccessKey, player2AccessKey))
                return Ok("Game started");

              return BadRequest("Match Is going on between other two users");
        }

        [Route("makemove")]
        [HttpPut]

        public ActionResult MakeMove([FromHeader]string accessKey,[FromHeader]string position)
        {
            string currentGameStatus = "";
           GameProccess update = new GameProccess();
            if (update.checkGameStartedOrNot())
            {
                if (update.validateAccesskey(accessKey))
                {
                    if (update.checkInProccessOrNot())
                    {
                        currentGameStatus = update.updateMoves(accessKey, position);
                        if (currentGameStatus.Equals("Invalid Move, already occupied") || currentGameStatus.Equals("Invalid move,it is not your turn"))
                            return BadRequest(currentGameStatus);
                    }
                    else
                        return BadRequest("Game already Ends");
                }
                else
                    return BadRequest("New player not allowed to make move");
            }
            else
                return BadRequest("Game Not started");
             return Ok(currentGameStatus);
        }

      
    }
}
