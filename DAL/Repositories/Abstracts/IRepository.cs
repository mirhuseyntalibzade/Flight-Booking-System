using CORE.Models.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Abstracts
{
    public interface IRepository<T> where T : BaseAuditableEntity, new()
    {
        DbSet<T> Table { get; }
        Task<ICollection<T>> GetAllAsync(params string[] includes);
        Task<T> GetByConditionAsync(Expression<Func<T, bool>> expression, params string[] includes);
        Task<T> GetByIdAsync(int Id, params string[] includes);
        Task AddAsync(T entity);
        Task AddRangeAsync(List<T> entities);
        void Remove(T entity);
        void Update(T entity);
        void SoftDelete(T entity);
        void RevertSoftDelete(T entity);
        Task<int> SaveChangesAsync();
    }
}
