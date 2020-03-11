using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using InventoryManagementSystem.Application.Interfaces;
using InventoryManagementSystem.Infrastructure.Configurations;

namespace InventoryManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IOptions<KeysConfig> _keyConfiguration;
        private readonly IAuthenticationApp _authenticationApp;

        public TokenController(IOptions<KeysConfig> keyConfiguration, IAuthenticationApp authenticationApp)
        {
            _keyConfiguration = keyConfiguration;
            _authenticationApp = authenticationApp;
        }

        [HttpPost]
        public IActionResult Create(string email, string password)
        {
            if (ValidationUserPassword(email, password))
                return new ObjectResult(GenerateToken(email));

            return BadRequest();
        }

        private bool ValidationUserPassword(string email, string password) 
            => !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password) && _authenticationApp.Authenticate(email, password);

        private string GenerateToken(string email)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, email),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
            };

            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_keyConfiguration.Value.SecretKey)),
                                             SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}