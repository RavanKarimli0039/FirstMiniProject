using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        void Create(T data);
        void Delete(T data);
        T Get(Predicate<T> filter); 
        List<T> GetAll(Predicate<T> filter = null); 
    }
}
