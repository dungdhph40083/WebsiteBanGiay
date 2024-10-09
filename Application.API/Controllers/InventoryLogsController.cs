using Application.Data.DTOs;
using Application.Data.Repositories;
using Application.Data.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryLogsController : ControllerBase
    {
        private readonly IInventoryLogRepository _inventoryLogRepository;

        public InventoryLogsController(IInventoryLogRepository inventoryLogRepository)
        {
            _inventoryLogRepository = inventoryLogRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryLogDto>>> GetInventoryLogs()
        {
            var logs = await _inventoryLogRepository.GetAllInventoryLogsAsync();
            return Ok(logs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryLogDto>> GetInventoryLog(Guid id)
        {
            var log = await _inventoryLogRepository.GetInventoryLogByIdAsync(id);
            if (log == null) return NotFound();
            return Ok(log);
        }

        [HttpPost]
        public async Task<ActionResult<InventoryLogDto>> CreateInventoryLog([FromBody] InventoryLogDto inventoryLogDto)
        {
            var createdLog = await _inventoryLogRepository.CreateInventoryLogAsync(inventoryLogDto);
            return CreatedAtAction(nameof(GetInventoryLog), new { id = createdLog.LogID }, createdLog);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInventoryLog(Guid id, [FromBody] InventoryLogDto inventoryLogDto)
        {
            var result = await _inventoryLogRepository.UpdateInventoryLogAsync(id, inventoryLogDto);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventoryLog(Guid id)
        {
            var result = await _inventoryLogRepository.DeleteInventoryLogAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
