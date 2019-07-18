using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample1.Interfaces;
using Sample1.Models;

namespace Sample1.Controllers {
    [Route("api/auth")]
    [ApiController]
    [Authorize]
    
    public class LoginController : ControllerBase {
        private IJwtService jwt;
        private IAuthService auth;

        public LoginController(IJwtService jwtService,IAuthService authService) {
            jwt = jwtService;
            auth = authService;
        }

        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate([FromBody]User userparam) {
            if (auth.Authenticate(userparam.username, userparam.password))
                return Ok(jwt.GenerateToken(userparam.username));
            else
                return BadRequest(new { message = "username or password is incorrect" });
        }
        [HttpGet("value")]
        public IActionResult GetValue() {
            return Ok("value");
        }
    }
}