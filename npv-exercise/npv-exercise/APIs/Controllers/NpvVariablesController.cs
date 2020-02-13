using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Npv_Exercise.Service.Services.NpvVariables;
using System.Threading.Tasks;

namespace npv_exercise.APIs.Controllers
{
    [AllowAnonymous]
    [Route("api/npvVariables")]
    [ApiController]
    public class NpvVariablesController : ControllerBase
    {
        private readonly INpvVariableService _npvVariableService;

        public NpvVariablesController(INpvVariableService npvVariableService)
        {            
            _npvVariableService = npvVariableService;
        }


        [HttpGet("")]
        public async Task<IActionResult> GetNpvVariables()
        {
            var fetchResult = await _npvVariableService.GetNpvVariablesList();
            if (fetchResult.HasErrors)
            {
                return BadRequest(fetchResult);
            }

            return Ok(fetchResult);
        }
    }
}