using System.Collections.Generic;

namespace MVCSchool.UnitOfWork.Repositories
{
    public interface IRepository <TEntity> where TEntity:class
    {
        IEnumerable<TEntity> Get();
        TEntity FindById(int? id);

        void Remove(TEntity entity);
        void Add(TEntity entity);
        void Edit(TEntity entity);

    }
}