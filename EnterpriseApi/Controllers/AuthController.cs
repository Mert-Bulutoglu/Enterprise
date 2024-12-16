using EnterpriseBusinessLayer.Abstract;
using EnterpriseEntityLayer.DTOs;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto request)
        {
            var token = _authService.Login(request.Username, request.Password);
            return Ok(token);
        }

        [HttpDelete("logout/{userId}/{token}")]
        public IActionResult Logout(string token, int userId)
        {
            var result = _authService.Logout(token, userId);
            if (result)
            {
                return Ok(new { message = "User logged out successfully." });
            }
            return Unauthorized("Invalid token or user not logged in.");
        }



    }
}
