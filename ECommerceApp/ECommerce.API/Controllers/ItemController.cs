using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{
    private readonly IItemService _itemService;

    public ItemController(IItemService itemService)
    {
        _itemService = itemService;
    }

    // GET: api/item
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _itemService.GetAllAsync();
        return Ok(items);
    }

    // GET: api/item/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _itemService.GetByIdAsync(id);
        if (item is null) return NotFound();
        return Ok(item);
    }

    // POST: api/item
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateItemDto dto)
    {
        var created = await _itemService.CreateAsync(dto);
        return Ok(created);
    }

    // PUT: api/item/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateItemDto dto)
    {
        if (id != dto.Id) return BadRequest("Id mismatch.");

        var success = await _itemService.UpdateAsync(dto);
        if (!success) return NotFound();
        return NoContent();
    }

    // DELETE: api/item/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _itemService.DeleteAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }

    // PUT: api/item/add-quantity
    [HttpPut("add-quantity")]
    public async Task<IActionResult> AddQuantity([FromBody] AddQuantityDto dto)
    {
        var success = await _itemService.AddQuantityAsync(dto);
        if (!success) return NotFound();
        return NoContent();
    }
}
