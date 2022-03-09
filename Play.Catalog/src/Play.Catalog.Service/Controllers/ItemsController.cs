using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Play.Catalog.Service.Dtos;
using System;
using System.Linq;

namespace Play.Catalog.Service.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private static readonly List<ItemDto> items = new()
        {
            new ItemDto(Guid.NewGuid(), "Potion", "Restores a small amount of HP", 5, System.DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Antidote", "Cures poison", 7, System.DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(), "Bronze Sword", "Deals a small amount of damage", 20, System.DateTimeOffset.UtcNow)

        };

        [HttpGet]
        public IEnumerable<ItemDto> Get()
        {
            return items;
        }
        [HttpGet("{id}")]
        public ItemDto GetById(Guid id)
        {
            var item = items.Where(item => item.Id == id).SingleOrDefault();
            return item;
        }

    }



}