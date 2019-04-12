using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task Buy([FromBody]TransactionDto transactionDto)
        {
            await _transactionsService.BuyAsync(
                transactionDto.ItemId,
                transactionDto.CustomerId,
                transactionDto.Quantity,
                transactionDto.Price);
        }

        [HttpPost("sell")]
        public async Task Sell([FromBody]TransactionDto transactionDto)
        {
            await _transactionsService.SellAsync(
                transactionDto.ItemId,
                transactionDto.CustomerId,
                transactionDto.Quantity,
                transactionDto.Price);
        }
    }
}