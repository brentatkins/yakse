using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Yakse.Core
{
    public interface IRepository
    {
        Task<IEnumerable<T>> All<T>() where T : BaseEntity;

        Task<IEnumerable<T>> Find<T>(Expression<Func<T, bool>> expression) where T : BaseEntity;

        Task<T?> GetById<T>(string id) where T : BaseEntity;
        
        Task Insert<T>(T obj) where T : BaseEntity;

        Task Update<T>(T obj, string id) where T : BaseEntity;
    }
}