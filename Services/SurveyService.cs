using System;
using System.Collections.Generic;
using System.Linq;
using SurveyApi.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace SurveyApi.Services
{
    public class SurveyService
    {
        private readonly IMongoCollection<Survey> _surveys;

        public SurveyService(IConfiguration config)
        {
            MongoHelper.ConnectToMongoService();
            var database = MongoHelper.Database;
            _surveys = database.GetCollection<Survey>("surveys");

        }

        public List<Survey> Get()
        {
            Console.WriteLine("check");
            return _surveys.Find(survey => true).ToList();
        }

        public Survey Get(string id)
        {
            Console.WriteLine("check id");
            Console.WriteLine(id);
            var filter = Builders<Survey>.Filter.Eq("_id", id);
            return _surveys.Find(filter).FirstOrDefault();
        }

        public Survey Create(Survey survey)
        {
            _surveys.InsertOne(survey);
            return survey;
        }

        public void Update(string id, Survey newSurvey)
        {
            _surveys.ReplaceOne(survey => survey._id == id, newSurvey);
        }

        public void Remove(Survey oldSurvey)
        {
            _surveys.DeleteOne(survey => survey._id == oldSurvey._id);
        }

        public void Remove(string id)
        {
            _surveys.DeleteOne(survey => survey._id == id);
        }

        public void Deployed(string id)
        {
            Console.WriteLine(id);
            var filter = Builders<Survey>.Filter.Eq("_id", id);
            var update = Builders<Survey>.Update.Set(survey => survey.Is_deployed, true);
            _surveys.UpdateOne(filter, update);
        }
    }
}
