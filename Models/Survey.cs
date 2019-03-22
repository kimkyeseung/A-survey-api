using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SurveyApi.Models
{
    public class Survey
    {
        //[BsonRepresentation(BsonType.ObjectId)]
        //public string _id { get; set; }

        public string _id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("questions")]
        public List<Question> Questions { get; set; }
        

        [BsonElement("is_deployed")]
        public bool Is_deployed { get; set; }
    }

    public class Question
    {
        [BsonElement("type")]
        public string Type { get; set; }

        [BsonElement("ask")]
        public string Ask { get; set; }

        [BsonElement("selections")]
        public List<Selection> Selections { get; set; }

    }

    public class Selection
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("value")]
        public int Value { get; set; }
    }
}
