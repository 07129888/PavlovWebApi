using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PavlovWebApi.Models;
using Serilog;

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
                    if (!Has(id))
                    {
                        Log.Warning($"Попытка доступа к несуществующему ID {id}");
                        throw new IncorrectCustomerDataException($"No CustomerData with id {id}");
                    }
                    Log.Information($"Get-запрос к ID {id}");
                    return _memCache.Single(x => x.Id == id);
                }
            }
            set
            {
                if (id == Guid.Empty)
                {
                    Log.Warning("Попытка изменения несуществующего ID");
                    throw new IncorrectCustomerDataException("Cannot request CustomerData with an empty id");
                }
                lock (_sync)
                {
                    Log.Information($"Изменение ID {id}");
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
            if (value.Id != Guid.Empty)
            {
                Log.Warning($"Попытка записи {value.ToString()} по существующему id {value.Id}");
                throw new IncorrectCustomerDataException($"Cannot add value with predefined id {value.Id}");
            }
            Log.Information($"Добавление новой записи {value.ToString()} с id {value.Id}");
            value.Id = Guid.NewGuid();
            this[value.Id] = value;
        }

        public bool Has(Guid id)
        {
            Log.Information($"Проверка наличия id {id}");
            return _memCache.Any(x => x.Id == id);
        }


        public void RemoveAt(Guid id)
        {
            lock (_sync)
            {
                Log.Information($"Удаление данных об id {id}");
                _memCache.RemoveAll(x => x.Id == id);
            }
        }
    }
}
