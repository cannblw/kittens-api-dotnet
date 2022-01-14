using System.Threading.Tasks;
using AutoMapper;
using KittensApi.Dto.Actions;
using KittensApi.Dto.Details;
using KittensApi.Exceptions;
using KittensApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KittensApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AuthController(
            ILogger<AuthController> logger,
            IAuthService authService,
            IMapper mapper)
        {
            _logger = logger;
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        [ProducesResponseType(400)]
        public async Task<ActionResult<RegistrationDetails>> RegisterUser([FromBody] RegisterUserAction action)
        {
            try
            {
                _logger.LogInformation("Creating user {UserName}", action.UserName);

                var (user, token) = await _authService.RegisterUser(action.Email, action.UserName, action.Password);
                
                var userDetails = _mapper.Map<UserDetails>(user);
                return new RegistrationDetails(userDetails, token);
            }
            catch (UserAlreadyExistsException ex)
            {
                _logger.LogError(ex, "Error creating user {UserName}", action.UserName);
                throw new KnownErrorException(ex.Message);
            }
        }
    }
}
