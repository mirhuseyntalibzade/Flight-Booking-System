using CORE.Models.Base;
using DAL.Contexts;
using DAL.Repositories.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;

namespace DAL.Repositories.Concretes
{
    public class Repository<T> : IRepository<T> where T : BaseAuditableEntity, new()
    {
        readonly AppDbContext _context;
        readonly IHttpContextAccessor _httpContext;

        public Repository(AppDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task AddAsync(T entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.CreatedBy = _httpContext.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? "Default";
            await Table.AddAsync(entity);
        }

        public async Task<ICollection<T>> GetAllAsync(params string[] includes)
        {
            IQueryable<T> query = Table;

            if (includes.Count() > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetByConditionAsync(Expression<Func<T, bool>> expression, params string[] includes)
        {
            IQueryable<T> query = Table;

            if (includes.Count() > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.SingleOrDefaultAsync(expression);
        }

        public async Task<T> GetByIdAsync(int Id, params string[] includes)
        {
            IQueryable<T> query = Table;

            if (includes.Count() > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.SingleOrDefaultAsync(e=>e.Id == Id);
        }
        public void Update(T entity)
        {
            entity.UpdatedDate = DateTime.Now;
            entity.UpdatedBy = _httpContext.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
            Table.Update(entity);
        }

        public void Remove(T entity)
        {
            Table.Remove(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void SoftDelete(T entity)
        {
            entity.DeletedDate = DateTime.Now;
            entity.isDeleted = true;
            entity.DeletedBy = _httpContext.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
            Table.Update(entity);
        }

        public void RevertSoftDelete(T entity)
        {
            entity.DeletedDate = null;
            entity.isDeleted = false;
            entity.DeletedBy = null;
            Table.Update(entity);
        }

        public async Task AddRangeAsync(List<T> entities)
        {
            await Table.AddRangeAsync(entities);
        }
    }
}
