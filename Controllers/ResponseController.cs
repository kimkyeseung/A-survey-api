using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SurveyApi.Models;
using SurveyApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Microsoft.AspNetCore.Authorization;

namespace SurveyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseController : Controller
    {
        private readonly ResponseService _responseService;

        public ResponseController(ResponseService responseService)
        {
            _responseService = responseService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet("{id}")] // fix this
        public IActionResult Get(string id)
        {
            var result = _responseService.Get(id); //return json

            if (result == null)
            {
                return NotFound();
            }
            var length = result.Count;
            var data = new { length }; // hold
            return Json(result);
        }

        
        [HttpGet("{id}/{participant}")]
        public IActionResult GetResponseDetail(string id, string participant)
        {
            var response = _responseService.GetResponseDetail(participant, id);

            var data = new { response };
            return Json(data);
        }

        [Authorize]
        [HttpPost("{id}")]
        // public ActionResult<Response> Create(Response response)
        public IActionResult Create(Response response)
        {
            _responseService.Create(response);
            return NoContent();
        }

    }
}