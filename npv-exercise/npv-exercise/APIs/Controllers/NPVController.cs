using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using npv_exercise.Models;
using Npv_Exercise.Core.Models.NpvVariables;
using Npv_Exercise.Service.Services.NPV;
using Npv_Exercise.Service.Services.NpvVariables;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace npv_exercise.APIs.Controllers
{
    [AllowAnonymous]
    [Route("api/npv")] // not the best way to name it, but i don't know what to properly call it :(
    [ApiController]
    public class NPVController : ControllerBase
    {
        private readonly INpvService _npvService;
        private readonly INpvVariableService _npvVariableService;

        public NPVController(
            INpvService npvService,
            INpvVariableService npvVariableService)
        {
            _npvService = npvService;
            _npvVariableService = npvVariableService;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> CalculateAsync(int id)
        {
            var fetchResult = await _npvVariableService.GetNpvVariableById(id);

            if(fetchResult.HasErrors)
            {
                return NotFound(fetchResult);
            }

            var computeResult = _npvService.ComputeNPVs(
                                    fetchResult.Data.InitialValue,
                                    fetchResult.Data.LowerBoundRate,
                                    fetchResult.Data.UpperBoundRate,
                                    fetchResult.Data.Increment,
                                    fetchResult.Data.Cashflows.Select(x => x.Cashflow).ToList()
                                );

            if (computeResult.HasErrors)
            {
                return BadRequest(computeResult);
            }

            return Ok(computeResult);
        }

        [HttpGet("")]
        public async Task<IActionResult> CalculateAsync([FromQuery]NpvCalculateModel npvCalculateModel)
        {
            var computeResult = _npvService.ComputeNPVs(
                                    npvCalculateModel.InitialValue,
                                    npvCalculateModel.LowerBoundRate,
                                    npvCalculateModel.UpperBoundRate,
                                    npvCalculateModel.Increment,
                                    npvCalculateModel.Cashflows
                                );

            if (!computeResult.HasErrors)
            {
                var npvVariable = new NpvVariable
                {
                    InitialValue = npvCalculateModel.InitialValue,
                    LowerBoundRate = npvCalculateModel.LowerBoundRate,
                    UpperBoundRate = npvCalculateModel.UpperBoundRate,
                    Increment = npvCalculateModel.Increment,
                    Cashflows = new List<NpvVariableCashflow>()
                };

                for (int ctr = 0; ctr < npvCalculateModel.Cashflows.Count; ctr++) 
                {
                    npvVariable.Cashflows.Add(new NpvVariableCashflow
                    {
                        Cashflow = npvCalculateModel.Cashflows[ctr],
                        Order = ctr
                    });
                } 

                var addResult = await _npvVariableService.AddNpvVariable(npvVariable);
                if (addResult.HasErrors)
                {
                    computeResult.Errors.AddRange(addResult.Errors);
                }
            }

            if (computeResult.HasErrors)
            {
                return BadRequest(computeResult);
            }

            return Ok(computeResult);
        }
    }
}