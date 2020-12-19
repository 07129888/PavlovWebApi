using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PavlovWebApi.Models;

namespace PavlovWebApi.Storage
{
    public class MemCache : IStorage<CustomerData>
    {
        private object _sync = new object();
        private List<CustomerData> _memCache = new List<CustomerData>();

        public CustomerData this[Guid id]
        {
            get
            {
                lock (_sync)
                {
                    if (!Has(id)) throw new IncorrectCustomerDataException($"No CustomerData with id {id}");
                    return _memCache.Single(x => x.Id == id);
                }
            }
            set
            {
                if (id == Guid.Empty) throw new IncorrectCustomerDataException("Cannot request CustomerData with an empty id");
                lock (_sync)
                {
                    if (Has(id))
                    {
                        RemoveAt(id);
                    }
                    value.Id = id;
                    _memCache.Add(value);
                }
            }
        }

        public System.Collections.Generic.List<CustomerData> All => _memCache.Select(x => x).ToList();

        public void Add(CustomerData value)
        {
            if (value.Id != Guid.Empty) throw new IncorrectCustomerDataException($"Cannot add value with predefined id {value.Id}");
            value.Id = Guid.NewGuid();
            this[value.Id] = value;
        }

        public bool Has(Guid id)
        {
            return _memCache.Any(x => x.Id == id);
        }


        public void RemoveAt(Guid id)
        {
            lock (_sync)
            {
                _memCache.RemoveAll(x => x.Id == id);
            }
        }
    }
}
