using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yakse.Core;

namespace Yakse.Infrastructure
{
    public class InMemoryRepository : IRepository
    {
        private readonly ConcurrentDictionary<Type, ConcurrentDictionary<string, BaseEntity>> _items =
            new ConcurrentDictionary<Type, ConcurrentDictionary<string, BaseEntity>>();

        public Task<IEnumerable<T>> All<T>() where T : BaseEntity
        {
            if (_items.ContainsKey(typeof(T)))
            {
                var items = _items[typeof(T)].Select(x => (T)x.Value);

                return Task.FromResult(items);
            }

            return Task.FromResult(Enumerable.Empty<T>());
        }

        public Task<T?> GetById<T>(string id) where T : BaseEntity
        {
            if (_items.ContainsKey(typeof(T)))
            {
                var item = _items[typeof(T)]
                    .Select(x => (T)x.Value)
                    .SingleOrDefault(x => x.Id == id);

                return Task.FromResult(item);
            }

            return Task.FromResult<T?>(null);
        }

        public Task<IEnumerable<T>> Find<T>(Expression<Func<T, bool>> expression) where T : BaseEntity
        {
            if (_items.ContainsKey((typeof(T))))
            {
                var items = _items[typeof(T)]
                    .Select(x => (T) x.Value)
                    .Where(expression.Compile());

                return Task.FromResult(items);
            }

            return Task.FromResult(Enumerable.Empty<T>());
        }

        public Task Insert<T>(T obj) where T : BaseEntity
        {
            if (!_items.ContainsKey(typeof(T)))
            {
                _items.TryAdd(typeof(T), new ConcurrentDictionary<string, BaseEntity>());
            }

            _items[typeof(T)].TryAdd(obj.Id, obj);

            return Task.CompletedTask;
        }

        public Task Update<T>(T obj, string id) where T : BaseEntity
        {
            if (!_items.ContainsKey(typeof(T)))
            {
                _items.TryAdd(typeof(T), new ConcurrentDictionary<string, BaseEntity>());
            }

            _items[typeof(T)][id] = obj;

            return Task.CompletedTask;
        }
    }
}