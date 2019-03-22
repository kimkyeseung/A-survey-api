using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SurveyApi.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace SurveyApi.Services
{
    public class ResponseService
    {
        private readonly IMongoCollection<Survey> _surveys;

        private readonly IMongoCollection<Response> _results;

        public ResponseService(IConfiguration config)
        {
            MongoHelper.ConnectToMongoService();
            var database = MongoHelper.Database;
            _surveys = database.GetCollection<Survey>("surveys");
            _results = database.GetCollection<Response>("results");
        }

        public List<Response> Get(string id) //
        {
            return _results.Find(response => response.Survey_id == id).ToList(); //
        }

        public Response GetResponseDetail(string participant, string id)
        {
            var filterParticapant = Builders<Response>.Filter.Eq("participant", participant);
            var filterSurvey = Builders<Response>.Filter.Eq("Survey_id", id);

            return _results.Find(filterParticapant & filterSurvey).FirstOrDefault();
        }

        public void Record(Response response)
        {
            string id = response.Survey_id;
            string participant = response.Participant;
            UpdateDefinition<Survey> update = Builders<Survey>.Update.Push("Results", participant);
            Console.WriteLine("RECODING");
            Console.WriteLine(id);
            Console.WriteLine(participant);
            
            _surveys.UpdateOne(survey => survey._id == id, update);
            
        }

        public void Create(Response response)
        {
            _results.InsertOne(response);
        }
    }
}
