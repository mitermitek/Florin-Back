using AutoMapper;
using Florin_Back.Models.DTOs.Transaction;
using Florin_Back.Models.Entities;
using Florin_Back.Models.Utilities;
using Florin_Back.Models.Utilities.Filters;
using Florin_Back.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Florin_Back.Controllers.Users
{
    [Route("api/User/[controller]")]
    [Tags("User/Transactions")]
    [ApiController]
    [Authorize]
    public class TransactionsController(IMapper mapper, ITransactionService transactionService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetTransactions([FromQuery] PaginationFilters pagination, TransactionFilters filters)
        {
            var transactions = await transactionService.GetUserTransactionsAsync(pagination, filters);
            var transactionsDTO = mapper.Map<Pagination<TransactionDTO>>(transactions);

            return Ok(transactionsDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionDTO transactionDto)
        {
            var transaction = mapper.Map<Transaction>(transactionDto);
            var createdTransaction = await transactionService.CreateUserTransactionAsync(transaction);
            var createdTransactionDTO = mapper.Map<TransactionDTO>(createdTransaction);

            return CreatedAtAction(nameof(GetTransaction), new { id = createdTransactionDTO.Id }, createdTransactionDTO);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetTransaction([FromRoute] long id)
        {
            var transaction = await transactionService.GetUserTransactionAsync(id);
            var transactionDTO = mapper.Map<TransactionDTO>(transaction);

            return Ok(transactionDTO);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateTransaction([FromRoute] long id, [FromBody] UpdateTransactionDTO transactionDto)
        {
            var transaction = mapper.Map<Transaction>(transactionDto);
            var updatedTransaction = await transactionService.UpdateUserTransactionAsync(id, transaction);
            var updatedTransactionDTO = mapper.Map<TransactionDTO>(updatedTransaction);

            return Ok(updatedTransactionDTO);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteTransaction([FromRoute] long id)
        {
            await transactionService.DeleteUserTransactionAsync(id);

            return NoContent();
        }
    }
}
