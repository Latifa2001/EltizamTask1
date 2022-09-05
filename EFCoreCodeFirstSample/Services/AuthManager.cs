using EFCoreCodeFirstSample.Data;
using EFCoreCodeFirstSample.Models;
using EFCoreCodeFirstSample.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Services.DelegatedAuthorization;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreCodeFirstSample.Services
{
    public class AuthManager : IAuthManager
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly IConfiguration _configuration;
        private ApiUser _user;
        public AuthManager(UserManager<ApiUser> userManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        //Done
        public async Task<string> CreateToken()
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var token = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //Done
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var jwtSettingsIsuu = jwtSettings.GetSection("Issuer").Value;
            var jwtSettingsAud = jwtSettings.GetSection("Audience").Value;
            var expiration = DateTime.Now.AddMinutes(Convert.ToDouble(
                jwtSettings.GetSection("lifetime").Value));

            var token = new JwtSecurityToken(
                issuer: jwtSettingsIsuu,
                audience: jwtSettingsAud,
                claims: claims,
                expires: expiration,
                signingCredentials: signingCredentials
                );

            return token;
        }

        //Done
        private async Task<List<Claim>> GetClaims()
        {

            var claims = new List<Claim>
             {
                 new Claim(ClaimTypes.Name, _user.UserName)
             };

            var roles = await _userManager.GetRolesAsync(_user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        //Done
        private SigningCredentials GetSigningCredentials()
        {
            //key
            var key = "hh33hhh-eee-3333-333";
            //encode
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            //send the secret with the algo name
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        //Done
        public async Task<bool> ValidateUser(LoginUserDTO userDTO)
        {
            _user = await _userManager.FindByNameAsync(userDTO.Email);
            var validPassword = await _userManager.CheckPasswordAsync(_user, userDTO.Password);
            return (_user != null && validPassword);
        }


        //public async Task<string> CreateRefreshToken()
        //{
        //    await _userManager.RemoveAuthenticationTokenAsync(_user, "HotelListingApi", "RefreshToken");
        //    var newRefreshToken = await _userManager.GenerateUserTokenAsync(_user, "HotelListingApi", "RefreshToken");
        //    var result = await _userManager.SetAuthenticationTokenAsync(_user, "HotelListingApi", "RefreshToken", newRefreshToken);
        //    return newRefreshToken;
        //}

        //public async Task<TokenRequest> VerifyRefreshToken(TokenRequest request)
        //{
        //    var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        //    var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(request.Token);
        //    var username = tokenContent.Claims.ToList().FirstOrDefault(q => q.Type == ClaimTypes.Name)?.Value;
        //    _user = await _userManager.FindByNameAsync(username);
        //    try
        //    {
        //        var isValid = await _userManager.VerifyUserTokenAsync(_user, "HotelListingApi", "RefreshToken", request.RefreshToken);
        //        if (isValid)
        //        {
        //            return new TokenRequest { Token = await CreateToken(), RefreshToken = await CreateRefreshToken() };
        //        }
        //        await _userManager.UpdateSecurityStampAsync(_user);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return null;
        //}

        //public ClaimsIdentity GetClaimsIdentity(string username, string password)
        //{
        //    throw new NotImplementedException();
        //}
    }
}