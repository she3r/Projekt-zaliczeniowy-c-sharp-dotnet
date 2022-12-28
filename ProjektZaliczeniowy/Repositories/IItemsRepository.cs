﻿using System;
using System.Collections.Generic;
using ProjektZaliczeniowy.Entities;

namespace ProjektZaliczeniowy.Repositories
{
    public interface IItemsRepository
    {
        IEnumerable<Item> GetItems();
        Item GetItem(Guid id);
        void CreateItem(Item item);
        void UpdateItem(Item item);
        void DeleteItem(Guid id);
    }
}