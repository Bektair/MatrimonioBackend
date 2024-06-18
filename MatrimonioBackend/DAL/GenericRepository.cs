using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MatrimonioBackend.DAL
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        internal WeddingContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(WeddingContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null, //We send in a lambda expression based on entity ex. student => student.LastName == "Smith"
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, //IQueryable labda with  versjon of order ex. q => q.OrderBy(s => s.LastName)
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity? GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual TEntity? GetByIDComposite(object[] ids)
        {
            return dbSet.Find(ids);
        }

        public virtual void Insert(TEntity entity)
        {
           context.Add(entity);
        }

        public virtual bool Delete(object id)
        {
            TEntity? entityToDelete = dbSet.Find(id);

            if(entityToDelete != null) { 
                Delete(entityToDelete);
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}