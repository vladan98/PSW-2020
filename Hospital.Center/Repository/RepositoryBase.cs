using Hospital.Domain;
using Hospital.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Hospital.Center.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hospital.Center.Repository
{
    public class RepositoryBase<T, ID> : IRepositoryBase<T, ID>
        where T : class, IIdentifiable<ID>
        where ID : IComparable
    {
        protected DbContext repositoryContext { get; set; }
        internal DbSet<T> databaseSet;
        public RepositoryBase(DbContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
            this.databaseSet = repositoryContext.Set<T>();
        }

        public List<T> FindAll()
        {
            return this.databaseSet.ToList();
        }

        public List<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.databaseSet.Where(expression).AsNoTracking().ToList<T>();
        }
        public T Create(T entity)
        {
            if (!ExistsBy(entity.GetId()))
            {
                databaseSet.Add(entity);
                repositoryContext.SaveChanges();
                return entity;
            }
            return null;
        }
        public bool ExistsBy(ID id)
        {
            if (databaseSet.Find(id) == null)
                return false;

            return true;
        }

        public T GetById(ID id)
        {
            return databaseSet.Find(id);
        }
        public T Update(T entity)
        {
            if (ExistsBy(entity.GetId()))
            {
                var entry = GetById(entity.GetId());
                repositoryContext.Entry(entry).CurrentValues.SetValues(entity);
                repositoryContext.SaveChanges();
                return entity;
            }
            return null;
        }
        public void Delete(T entity)
        {
            this.databaseSet.Remove(entity);
            repositoryContext.SaveChanges();
        }

    }
}
