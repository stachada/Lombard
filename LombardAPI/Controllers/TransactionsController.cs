using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lombard.BL.Models;
using Lombard.BL.RepositoriesInterfaces;
using Lombard.BL.Services;
using LombardAPI.Dtos;
using LombardAPI.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LombardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionsService _transactionsService;
        private readonly ITransactionsRepository _transactionsRepository;
        private readonly IMapper _mapper;

        public TransactionsController(
            ITransactionsService transactionsService,
            ITransactionsRepository transactionsRepository,
            IMapper mapper)
        {
            _transactionsService = transactionsService;
            _transactionsRepository = transactionsRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetTransactions([FromQuery]TransactionsQuery query)
        {
            var transactions = await _transactionsRepository.GetTransactions(query.PageNumber, query.PageSize);

            var transactionsToReturn = _mapper.Map<IEnumerable<TransactionDto>>(transactions);

            return Ok(transactionsToReturn);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var transaction = await _transactionsRepository.GetByIdAsync(id);

            var transactionToReturn = _mapper.Map<TransactionDto>(transaction);

            return Ok(transactionToReturn);
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

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody]TransactionDto transactionDto)
        {
            var transaction = _mapper.Map<Transaction>(transactionDto);

            try
            {
                await _transactionsService.UpdateTransactionAsync(transaction);
            }
            catch(InvalidOperationException ex)
            {
                BadRequest(ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _transactionsService.DeleteTransactionAsync(id);
            }
            catch(InvalidOperationException ex)
            {
                BadRequest(ex.Message);
            }

            return NoContent();
        }
    }
}