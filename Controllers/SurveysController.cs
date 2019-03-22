using System;
using System.Collections.Generic;
using SurveyApi.Models;
using SurveyApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SurveyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveysController : Controller
    {
        private readonly SurveyService _surveyService;

        public SurveysController(SurveyService surveyService)
        {
            _surveyService = surveyService;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<List<Survey>> Get()
        {
            Console.WriteLine("Get 요청을 받았습니다. Get 요청을 받았습니다. Get 요청을 받았습니다.");
            return _surveyService.Get();
        }

        //not authorize
        [HttpGet("{id}")]
        public ActionResult<Survey> Get(string id)
        {
            var survey = _surveyService.Get(id);

            if (survey == null)
            {
                return NotFound();
            }

            Console.WriteLine("Get 요청을 받았습니다. Get 요청을 받았습니다. Get 요청을 받았습니다.");
            return survey;
        }
        
        [Authorize]
        [HttpPost]
        public ActionResult<Survey> Create(Survey survey)
        {
            _surveyService.Create(survey);

            Console.WriteLine("Post 요청을 받았습니다. Post 요청을 받았습니다. Post 요청을 받았습니다.");
            return NoContent();
        }

        [Authorize]
        [HttpPost("{id}")]
        public ActionResult<Survey> Deployed(string id)
        {
            Console.WriteLine("설문지가 DEPOLY 되었습니다. 설문지가 DEPOLY 되었습니다.");
            _surveyService.Deployed(id);

            return NoContent();
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(string id, Survey newSurvey)
        {

            Console.WriteLine("Put 요청을 받았습니다 id = {0}. Put 요청을 받았습니다 id = {0}. Put 요청을 받았습니다 id = {0}.", id);
            var survey = _surveyService.Get(id);

            if (survey == null)
            {
                return NotFound();
            }

            _surveyService.Update(id, newSurvey);

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var survey = _surveyService.Get(id);
            
            if (survey == null)
            {
                return NotFound();
            }

            _surveyService.Remove(survey._id);

            Console.WriteLine("Delete 요청을 받았습니다. Delete 요청을 받았습니다. Delete 요청을 받았습니다.");
            return NoContent();
        }
    }
}
