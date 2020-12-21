using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PavlovWebApi.Models;

namespace PavlovWebApi.Storage
{
    public class StorageService
    {
        private readonly IStorage<CustomerData> _storage;

        public StorageService(IStorage<CustomerData> storage)
        {
            _storage = storage;
        }

        public string GetStorageType()
        {
            return _storage.StorageType;
        }

        public int GetNumberOfItems()
        {
            return _storage.All.Count;
        }
    }
}
