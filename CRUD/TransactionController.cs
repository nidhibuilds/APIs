using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUD
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _service;

        public TransactionController(ITransactionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetAll()
        {
            var transactions = await _service.GetAllTransactionsAsync();
            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetById(int id)
        {
            var transaction = await _service.GetTransactionByIdAsync(id);
            if (transaction == null) return NotFound();
            return Ok(transaction);
        }

        [HttpPost]
        public async Task<ActionResult<Transaction>> Create(Transaction transaction)
        {
            var created = await _service.CreateTransactionAsync(transaction);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Transaction>> Update(int id, Transaction transaction)
        {
            if (id != transaction.Id) return BadRequest();
            var updated = await _service.UpdateTransactionAsync(transaction);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteTransactionAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
} 