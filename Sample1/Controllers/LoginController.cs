using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample1.Models;

namespace Sample1.Controllers {
    [Route("api/auth")]
    [ApiController]
    [Authorize]
    
    public class LoginController : ControllerBase {
        private AuthService auth;

        public LoginController(AuthService service) {
            auth = service;
        }

        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [HttpPost]
        public IActionResult authenticate([FromBody]User userparam) {
            if (auth.Authenticate(userparam.username, userparam.password))
                return Ok(auth.GenerateToken(userparam.username));
            else
                return BadRequest(new { message = "username or password is incorrect" });
        }
        [HttpGet("value")]
        public IActionResult GetValue() {
            return Ok("value");
        }
    }
}