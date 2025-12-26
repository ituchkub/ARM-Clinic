using ARM_Clinic_API.Model;
using ARM_Clinic_API.Repo;
using ARM_Clinic_API_Helper;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace ARM_Clinic_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        GenToken genToken = new GenToken();
        MasterRepo masterRepo = new MasterRepo();
        AuthenRepo authenRepo = new AuthenRepo();

        [HttpPost]
        [Route("Authen/UserLogin")]
        public IActionResult onPost_UserGetLogin([FromBody] UserAuthen request)
        {
            if (request != null)
            {
                return Ok(authenRepo.onPost_GetUserLogin(request));
            }
            else
            {
                return BadRequest(new ResponseMessage<int>() { Status = false, ErrorMessage = "Access denied" });
            }
        }

        [Authorize]
        [HttpPost]
        [Route("Authen/UserManagement")]
        public IActionResult onPost_UserGetUserData([FromBody] RequestMessage<UserUserManagement> request)
        {
            if (request != null)
            {
                return Ok(authenRepo.onPost_GetUser_Insert_Update(request));
            }
            else
            {
                return BadRequest(new ResponseMessage<int>() { Status = false, ErrorMessage = "Access denied" });
            }
        }

    }
}
