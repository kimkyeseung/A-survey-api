using System;
using System.Collections.Generic;
using SurveyApi.Models;
using SurveyApi.Services;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace SurveyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : Controller
    {
        private readonly AccountService _accountService;

        private readonly IConfiguration Configuration;

        public string temp;

        public AccountsController(AccountService accountService, IConfiguration config)
        {
            _accountService = accountService;
            Configuration = config;
        }
        
        
        [HttpPost]
        public JsonResult Get(Authenticate accountRequest)
        {
            _accountService.Get(accountRequest.Login_id);
            var account = _accountService.Get(accountRequest.Login_id);

            if (account == null)
            {
                return Json(new { status = "fail", message = "noAccount" });
            }
            else if (account.Password != accountRequest.Password)
            {
                return Json(new { status = "fail", message = "incorrectPasswrd" });
            }

            Console.WriteLine(Configuration["JWT:issuer"]);
            Console.WriteLine(Configuration["JWT:audience"]);
            Console.WriteLine(Configuration["JWT:key"]);
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:key"]));
            var signIngCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                issuer: Configuration["JWT:issuer"],
                audience: Configuration["JWT:audience"],
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signIngCred
                );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Json(new { status = "success", message = "success", name = account.Name, token = tokenString });

        }

        // PUT: api/Accounts/5
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] string value)
        {
            return "put";
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
