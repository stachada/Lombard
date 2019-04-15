﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lombard.BL.Services;
using LombardAPI.Dtos;
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
            var profit = await _reportService.GetProfit(query.StartDate, query.EndDate);

            return Ok(profit);
        }

        [HttpGet("turnover")]
        public async Task<ActionResult> GetTotalTurnver([FromQuery] PeriodQuery query)
        {
            try
            {
                var turnover = await _reportService.GetTurnover(query.StartDate, query.EndDate);
                return Ok(turnover);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("categories")]
        public async Task<ActionResult> GetItemsGroupedByCategories()
        {
            try
            {
                var categories = await _reportService.GetQuantityInCategoriesAsync();
                return Ok(categories);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}