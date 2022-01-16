using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
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

        public async Task<(User user, string token)> RegisterUser(string email, string userName, string password)
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
                
                throw new KnownErrorException(errorMessage, HttpStatusCode.InternalServerError);
            }
            
            var jwtToken = GenerateJwtToken( newUser);

            return (newUser, jwtToken);
        }

        public async Task<(User user, string token)> Login(string userName, string password)
        {
            var existingUser = await _userManager.FindByNameAsync(userName);

            if (existingUser == null)
            {
                throw new UnauthorizedException();
            }

            var hasValidCredentials = await _userManager.CheckPasswordAsync(existingUser, password);
            if (!hasValidCredentials)
            {
                throw new UnauthorizedException();
            }

            var jwtToken = GenerateJwtToken(existingUser);

            return (existingUser, jwtToken);
        }

        public void RefreshJwtToken(string refreshToken)
        {
            /*
             * I did not implement this on purpose. I would do it if I had the time, but I will explain what this would do.
             *
             * This endpoint would receive the refresh token, which is similar to the normal JWT token, but with a longer
             * expiration date. The server would validate that this refresh token is valid and would send the user a new
             * JWT token. The new JWT token will be used to make requests instead of the old, expired token.
             */

            throw new KnownErrorException("This is not implemented. Please, go to AuthService.cs and read the comment", HttpStatusCode.NotImplemented);
        }

        public async Task<User> GetUserByClaimsPrincipal(ClaimsPrincipal claimsPrincipal)
        {
            var userName = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "UserName")?.Value;

            if (userName == null)
            {
                throw new UnauthorizedException();
            }
            
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                throw new UnauthorizedException();
            }

            return user;
        }

        private string GenerateJwtToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_settings.Authentication.JwtSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new []
                {
                    new Claim("UserID", user.Id),
                    new Claim("UserName", user.UserName),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(_settings.Authentication.JwtHoursUntilExpiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}
