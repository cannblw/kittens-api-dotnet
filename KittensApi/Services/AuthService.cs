using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using KittensApi.Config;
using KittensApi.Domain;
using KittensApi.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace KittensApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly AppSettings _settings;
        
        public AuthService(
            UserManager<User> userManager,
            AppSettings settings)
        {
            _userManager = userManager;
            _settings = settings;
        }

        public async Task<(User, string)> RegisterUser(string email, string userName, string password)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);

            if (existingUser != null)
            {
                throw new UserAlreadyExistsException();
            }

            var newUser = new User { Email = email, UserName = userName };
            var isCreated = await _userManager.CreateAsync(newUser, password);
            
            if (!isCreated.Succeeded)
            {
                var errorMessage = string.Join(" ", isCreated.Errors.Select(x => x.Description).ToList());
                
                throw new KnownErrorException(errorMessage);
            }
            
            var jwtToken = GenerateJwtToken( newUser);

            return (newUser, jwtToken);
        }
        
        private string GenerateJwtToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_settings.Authentication.JwtSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new []
                {
                    new Claim("Id", user.Id), 
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}
