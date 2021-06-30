using Hospital.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hospital.Center.Interfaces
{
    public interface IRepositoryBase<T, ID>
        where T : IIdentifiable<ID>
        where ID : IComparable
    {
        List<T> FindAll();
        List<T> FindByCondition(Expression<Func<T, bool>> expression);
        T Create(T entity);
        T Update(T entity);
        void Delete(T entity);
        T GetById(ID id);
        bool ExistsBy(ID id);

    }
}
