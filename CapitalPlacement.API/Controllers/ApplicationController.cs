using CapitalPlacement.Application.Interfaces.Application_Layer;
using CapitalPlacement.Domain;
using CapitalPlacement.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using Mapster;
using System.Reflection.Metadata.Ecma335;

namespace CapitalPlacement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationBankService _appService;

        public ApplicationController(IApplicationBankService userServ)
        {
            _appService = userServ;
        }

        [HttpGet("AllApplication")]
        public async Task<IActionResult> AllApplication()
        {
            var result = await _appService.GetAllApplicationQuestion();
            return Ok(result);
        }

        [HttpGet("GetApplicationById")]
        public async Task<IActionResult> DefaultApplication(string applicationId)
        {
            var result = await _appService.GetApplicationByIdAsync(applicationId);
            return Ok(result);
        }


        [HttpPost("AddNewApplication")]
        public async Task<IActionResult> AddNewApplication(ApplicationBankDto applicationBank)
        {
            var result = await _appService.AddNewApplication(applicationBank.Adapt<ApplicationBank>());
            return Ok(result);
        }

        [HttpPost("AddNewApplicationQuestions")]
        public async Task<IActionResult> AddNewApplicationQuestions(ApplicationQuestionsDto applicationQuestions, string ApplicationBankId)
        {
            var result = await _appService.AddNewApplicationQuestions(applicationQuestions.Adapt<ApplicationQuestions>(), ApplicationBankId);
            return Ok(result);
        }

        [HttpPut("UpdateApplicationBank")]
        public async Task<IActionResult> UpdateApplicationBankAsync(ApplicationBankDto applicationQuestions, string ApplicationBankId)
        {
            var result = await _appService.UpdateApplicationBankAsync(applicationQuestions.Adapt<ApplicationBank>(), ApplicationBankId);
            return Ok(result);
        }

    }
}
