using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models.Common;

namespace BookStore.Data.Common
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly IDbSet<T> DbSet;
        protected BookStoreDbContext Context;

        protected Repository(BookStoreDbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public virtual void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            DbSet.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> @where)
        {
            var objects = DbSet.Where(where).AsEnumerable();
            foreach (var obj in objects)
                DbSet.Remove(obj);
        }

        public virtual T GetById(int id)
        {
            return DbSet.Find(id);
        }

        public virtual T Get(Expression<Func<T, bool>> @where)
        {
            return DbSet.Where(where).FirstOrDefault();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return DbSet.AsNoTracking().ToList();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> @where)
        {
            return DbSet.Where(where).ToList();
        }


        public virtual IEnumerable<T> AllInclude(params Expression<Func<T, object>>[] includeProperties)
        {
            return GetAllIncluding(includeProperties).ToList();
        }

        public virtual IEnumerable<T> FindByInclude(Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties)
        {
            var query = GetAllIncluding(includeProperties);
            IEnumerable<T> results = query.Where(predicate).ToList();
            return results;
        }

        private IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = DbSet.AsNoTracking();
            return includeProperties.Aggregate(queryable, (current, property) => current.Include(property));
        }


        public IQueryable<T> GetQueryable()
        {
            return this.DbSet.AsQueryable<T>();
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }


        public IQueryable<T> GetQueryableData(out int totalCount, Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null,
            int? skip = null,
            int? take = null)

        {
            includeProperties = includeProperties ?? string.Empty;
            IQueryable<T> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            totalCount = query.Count();

            foreach (var includeProperty in includeProperties.Split
                (new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }
    }
}