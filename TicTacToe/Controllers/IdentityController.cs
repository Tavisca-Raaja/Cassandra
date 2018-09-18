using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.DataAccess;
using TicTacToe.Authorize;
using TicTacToe.Exceptions;
using TicTacToe.GameAttributes;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TicTacToe.Controllers
{
    [Route("api/[controller]")]
    [TokenCheck]
    [Logging]
    public class IdentityController : Controller
    {
        // POST api/identity
        [HttpPost]
        public ActionResult RegisterUser([FromBody]User newUser)
        {
            UserRegistration newGamer = new UserRegistration();
            if (newGamer.UpdateNewUser(newUser))
            {
                return Ok("User Added Successfully");
            }

              return BadRequest("UserName already exist,Enter new UserName");
        }

        [Route("{userName}/AccessToken")]
        [HttpPut]
        
        public ActionResult GenerateAccessToken(string UserName)
        {
            UserRegistration updateGamer = new UserRegistration();
            if (!updateGamer.ValidateUserName(UserName))
            {
                if (updateGamer.GenerateToken(UserName))
                    return Ok("AccessToken Generated Successfully");
                else
                    return BadRequest("AccessToken Already Generated");
            }
            return BadRequest("No user With User name:"+UserName);
        }


    }
}
