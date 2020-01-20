using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.IO;

namespace JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase    
    {
        [HttpPost("token")]
       public ActionResult GetToken()
        {
            IdentityModelEventSource.ShowPII = true;
            //Security Key
            string securityKey = "This_is_the_security_key_please_replace_this_with_your_security_key";

            //Symmetric Security Key
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            //Signing Credentials
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            //Claims
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            //Create Token
            var token = new JwtSecurityToken(issuer: "niksaj", audience: "readers", expires: DateTime.Now.AddHours(1),signingCredentials:signingCredentials,claims:claims);
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
