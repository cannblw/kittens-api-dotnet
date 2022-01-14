using System.Threading.Tasks;
using KittensApi.Dto.Actions;
using KittensApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace KittensApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        // TODO: Don't return object
        public async Task<ActionResult<object>> RegisterUser([FromBody] RegisterUserAction action)
        {
            var (user, token) = await _authService.RegisterUser(action.Email, action.UserName, action.Password);

            // TODO: Convert to DTO
            return new
            {
                User = user,
                Token = token
            };
        }
    }
}
