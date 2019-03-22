using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SurveyApi.Models;
using MongoDB.Driver;
using System.Diagnostics;
using Microsoft.Extensions.Options;

namespace SurveyApi.Services
{
    public class AccountService
    {
        private readonly IMongoCollection<Account> _accounts;
        public readonly Helper _helper;

        public AccountService(IConfiguration config)
        {
            MongoHelper.ConnectToMongoService();
            var database = MongoHelper.Database;
            _accounts = database.GetCollection<Account>("accounts");
        }

        public Account Get(string id)
        {
            {
                var result = _accounts.Find(account => account.Login_id == id).FirstOrDefault();

                return result;
            }
        }
    }
}
