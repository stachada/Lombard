using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lombard.BL.Models;
using Lombard.BL.Services;
using LombardAPI.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LombardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionsService _transactionsService;

        public TransactionsController(ITransactionsService transactionsService)
        {
            _transactionsService = transactionsService;
        }

        [HttpPost("buy")]
        public async Task<ActionResult> Buy([FromBody]TransactionDto transactionDto)
        {
            Transaction transaction = null;
            try
            {
                transaction = await _transactionsService.BuyAsync(
                    transactionDto.ItemId,
                    transactionDto.CustomerId,
                    transactionDto.Quantity,
                    transactionDto.Price);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction("Get", new { Id = transaction.TransactionId }, transaction);
        }

        [HttpPost("sell")]
        public async Task<ActionResult> Sell([FromBody]TransactionDto transactionDto)
        {
            Transaction transaction = null;
            try
            {
                transaction = await _transactionsService.SellAsync(
                    transactionDto.ItemId,
                    transactionDto.CustomerId,
                    transactionDto.Quantity,
                    transactionDto.Price);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

            return CreatedAtAction("Get", new { Id = transaction.TransactionId }, transaction);
        }
    }
}