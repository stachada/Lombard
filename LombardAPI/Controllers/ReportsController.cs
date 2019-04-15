using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lombard.BL.Services;
using LombardAPI.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LombardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IMapper _mapper;

        public ReportsController(IReportService reportService, IMapper mapper)
        {
            _reportService = reportService;
            _mapper = mapper;
        }

        [HttpGet("profit")]
        public async Task<ActionResult> Get([FromQuery]PeriodQuery query)
        {
            decimal profit;
            try
            {
                profit = await _reportService.GetProfit(query.StartDate, query.EndDate);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }

            return Ok(profit);
        }

        [HttpGet("allitems")]
        public async Task<ActionResult> Get()
        {
            var items = await _reportService.GetAllAsync();

            return Ok(items);
        }

        [HttpGet("itemstoreorder")]
        public async Task<ActionResult>Get([FromQuery]float minQuantity)
        {
            var list = await _reportService.GetItemsWithQuantityLowerThanAsync(minQuantity);

            return Ok(list);
        }
    }
}