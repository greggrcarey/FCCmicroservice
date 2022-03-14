using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Play.Catalog.Service.Dtos;
using Play.Common;

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

        private readonly IRepository<Item> _itemsRepository;
        private static int requestCoutner = 0;

        public ItemsController(IRepository<Item> itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetAsync()
        {
            requestCoutner++;
            Console.WriteLine($"Request {requestCoutner}: Starting...");

            if (requestCoutner <= 2)
            {
                Console.WriteLine($"Request {requestCoutner}: Delaying...");
                await Task.Delay(TimeSpan.FromSeconds(10));
            }

            if (requestCoutner <= 4)
            {
                Console.WriteLine($"Request {requestCoutner}: 500 (Internal Server Error)");
                return StatusCode(500);
            }

            var items = (await _itemsRepository.GetAllAsync())
                .Select(item => item.AsDto());
            Console.WriteLine($"Request {requestCoutner}: 200(Ok)");
            return Ok(items);
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