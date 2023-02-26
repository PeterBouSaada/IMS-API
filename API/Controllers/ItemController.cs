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
    [Route("[controller]s")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private ICache<Item> _cacheService;
        public ItemController(IItemService itemService, ICache<Item> cache)
        {
            _itemService = itemService;
            _cacheService = cache;
        }

        [HttpGet]
        public IActionResult getAll()
        {
            List<Item> items = _itemService.getAllItems();
            return items != null ? new ObjectResult(items) { StatusCode = StatusCodes.Status200OK } : BadRequest();
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            
            Item foundItem = _cacheService.GetOrSet("item_" + id, 60 * 4, () => _itemService.FindOneItem(id)); ;
            return foundItem != null ? new ObjectResult(foundItem) { StatusCode = StatusCodes.Status200OK } : BadRequest();
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody] Item item)
        {
            Item addedItem = _itemService.AddItem(item);
            return addedItem != null ? new ObjectResult(addedItem) { StatusCode = StatusCodes.Status201Created } : BadRequest();
        }

        [HttpPost("search")]
        public IActionResult Search(Item item)
        {
            List<Item> items = _itemService.FindItem(item);
            return items != null ? new ObjectResult(items) { StatusCode = StatusCodes.Status200OK } : BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] Item item)
        {
            Item updatedItem = _itemService.UpdateItem(id, item);
            return updatedItem != null ? new ObjectResult(updatedItem) { StatusCode = StatusCodes.Status200OK } : BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            Item deletedItem = _itemService.DeleteItem(id);
            return deletedItem != null ? Ok() : BadRequest();
        }

    }
}
