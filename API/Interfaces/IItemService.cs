using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;

namespace API.Interfaces
{
    public interface IItemService
    {
        Item AddItem(Item item);
        Item UpdateItem(Item item);
        Item DeleteItem(string id);
        List<Item> FindItem(Item item);
        Item FindOneItem(string id);
        List<Item> getAllItems();
    }
}
