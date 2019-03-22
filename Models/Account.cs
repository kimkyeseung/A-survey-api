using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SurveyApi.Models
{
    public class Authenticate
    {
        [BsonElement("login_id")]
        public string Login_id { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }
    }
    public class Account : Authenticate
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("surveys")]
        public List<ObjectId> Surveys { get; set; }
    }
}
