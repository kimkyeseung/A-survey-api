using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using SurveyApi.Models;

namespace SurveyApi.Services
{
    public class MongoHelper
    {
        public static IMongoClient Client { get; set; }
        public static IMongoDatabase Database { get; set; }
        public static string MongoConnection = "mongodb+srv://admin:1234@cluster0-po23p.mongodb.net/test?retryWrites=true";
        public static string SurveyDatabase = "SurveyDb";

        public static IMongoCollection<Survey> Survey_collection { get; set; }
        public static IMongoCollection<Account> Account_collection { get; set; }
        public static IMongoCollection<Response> Response_collection { get; set; }

        internal static void ConnectToMongoService()
        {
            try
            {
                Client = new MongoClient(MongoConnection);
                Database = Client.GetDatabase(SurveyDatabase);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
