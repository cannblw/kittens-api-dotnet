using System.Threading.Tasks;
using AutoMapper;
using KittensApi.Dto.Actions;
using KittensApi.Dto.Details;
using KittensApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace KittensApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(
            IAuthService authService,
            IMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        [ProducesResponseType(400)]
        public async Task<ActionResult<RegistrationDetails>> RegisterUser([FromBody] RegisterUserAction action)
        {
            var (user, token) = await _authService.RegisterUser(action.Email, action.UserName, action.Password);

            var userDetails = _mapper.Map<UserDetails>(user);

            return new RegistrationDetails(userDetails, token);
        }
    }
}
