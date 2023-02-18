using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("API/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private ICache<Item> _cacheService;
        public ItemController(IItemService itemService, ICache<Item> cache)
        {
            _itemService = itemService;
            _cacheService = cache;
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            
            Item foundItem = _cacheService.GetOrSet("item_" + id, 60 * 4, () => _itemService.FindOneItem(id)); ;
            return foundItem != null ? new ObjectResult(foundItem) { StatusCode = StatusCodes.Status200OK } : new ObjectResult(foundItem) { StatusCode = StatusCodes.Status400BadRequest };
        }

        [HttpPost("search")]
        public IActionResult Search(Item item)
        {
            List<Item> items = _itemService.FindItem(item);
            return items != null ? new ObjectResult(items) { StatusCode = StatusCodes.Status200OK } : new ObjectResult(items) { StatusCode = StatusCodes.Status400BadRequest };
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] Item item)
        {
            Item addedItem = _itemService.AddItem(item);
            return addedItem != null ? new ObjectResult(addedItem) { StatusCode = StatusCodes.Status201Created } : new ObjectResult(addedItem) { StatusCode = StatusCodes.Status400BadRequest };
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            Item deletedItem = _itemService.DeleteItem(id);
            return deletedItem != null ? new ObjectResult(deletedItem) { StatusCode = StatusCodes.Status200OK} : new ObjectResult(deletedItem) { StatusCode = StatusCodes.Status400BadRequest };
        }

        [HttpPut]
        public IActionResult Update([FromBody] Item item)
        {
            Item updatedItem = _itemService.UpdateItem(item);
            return updatedItem != null ? new ObjectResult(updatedItem) { StatusCode = StatusCodes.Status200OK } : new ObjectResult(updatedItem) { StatusCode = StatusCodes.Status400BadRequest };
        }

        [HttpGet]
        public IActionResult getAll()
        {
            List<Item> items = _itemService.getAllItems();
            return items != null ? new ObjectResult(items) { StatusCode = StatusCodes.Status200OK } : new ObjectResult(items) { StatusCode = StatusCodes.Status400BadRequest };
        }
    }
}
