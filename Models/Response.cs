using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SurveyApi.Models
{
    public class Response
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        [BsonElement("creation_date")]
        public DateTime Creation_date { get; set; }

        [BsonElement("survey_id")]
        public string Survey_id { get; set; }

        [BsonElement("participant")]
        public string Participant { get; set; }

        [BsonElement("answers")]
        public List<List<string>> Answers { get; set; }
    }
}
