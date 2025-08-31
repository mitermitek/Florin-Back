using AutoMapper;
using Florin_Back.DTOs.UserTransaction;
using Florin_Back.Models;
using Florin_Back.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Florin_Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserTransactionsController(IUserContextService userContextService, IMapper mapper, ITransactionService transactionService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetUserTransactions()
        {
            var userId = userContextService.GetUserId();
            var userTransactions = await transactionService.GetUserTransactionsAsync(userId);
            var userTransactionsDTO = mapper.Map<IEnumerable<UserTransactionDTO>>(userTransactions);

            return Ok(userTransactionsDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserTransaction([FromBody] CreateUserTransactionDTO transactionDto)
        {
            var userId = userContextService.GetUserId();
            var mappedTransaction = mapper.Map<Transaction>(transactionDto);
            var createdTransaction = await transactionService.CreateUserTransactionAsync(userId, mappedTransaction);
            var createdTransactionDto = mapper.Map<UserTransactionDTO>(createdTransaction);

            return CreatedAtAction(nameof(GetUserTransaction), new { id = createdTransactionDto.Id }, createdTransactionDto);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetUserTransaction([FromRoute] long id)
        {
            var userId = userContextService.GetUserId();
            var transaction = await transactionService.GetUserTransactionByIdAsync(userId, id);
            var transactionDto = mapper.Map<UserTransactionDTO>(transaction);

            return Ok(transactionDto);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateUserTransaction([FromRoute] long id, [FromBody] UpdateUserTransactionDTO transactionDto)
        {
            var userId = userContextService.GetUserId();
            var mappedTransaction = mapper.Map<Transaction>(transactionDto);
            var updatedTransaction = await transactionService.UpdateUserTransactionAsync(userId, id, mappedTransaction);
            var updatedTransactionDto = mapper.Map<UserTransactionDTO>(updatedTransaction);

            return Ok(updatedTransactionDto);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteUserTransaction([FromRoute] long id)
        {
            var userId = userContextService.GetUserId();
            await transactionService.DeleteUserTransactionAsync(userId, id);

            return NoContent();
        }
    }
}
