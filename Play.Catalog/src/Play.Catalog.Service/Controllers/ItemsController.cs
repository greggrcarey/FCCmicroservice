using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Play.Catalog.Service.Dtos;
using Play.Catalog.Service.Repositories;

using System;
using System.Linq;
using System.Threading.Tasks;
using Play.Catalog.Service.Entities;

namespace Play.Catalog.Service.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {

        private readonly ItemsRepository _itemsRepository = new();


        [HttpGet]
        public async Task<IEnumerable<ItemDto>> Get()
        {
            var items = (await _itemsRepository.GetAllAsync())
                .Select(item => item.AsDto());
            return items;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetByIdAsync(Guid id)
        {
            var item = await _itemsRepository.GetAsync(id);


            if (item is null)
            {
                return NotFound();
            }

            return item.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> Post(CreateItemDto createItemDto)
        {
            var (name, description, price) = createItemDto;

            var item = new Item()
            {
                Name = name,
                Description = description,
                Price = price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await _itemsRepository.CreateAsync(item);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id }, item);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, UpdateItemDto updateItemDto)
        {
            var (name, description, price) = updateItemDto;
            var existingItem = await _itemsRepository.GetAsync(id);

            if (existingItem is null)
            {
                return NotFound();
            }

            existingItem.Name = name;
            existingItem.Description = description;
            existingItem.Price = price;

            await _itemsRepository.UpdateAsync(existingItem);

            return NoContent();

        }
        //DELETE /items/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var item = await _itemsRepository.GetAsync(id);

            if (item is null)
            {
                return NotFound();
            }

            await _itemsRepository.RemoveAsync(item.Id);
            return NoContent();

        }

    }



}