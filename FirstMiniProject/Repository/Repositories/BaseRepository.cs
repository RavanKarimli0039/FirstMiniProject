using Domain.Common;
using Repository.Data;
using Repository.Repositories.Interfaces;


namespace Repository.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        public void Create(T data)
        {
            AppDbContext<T>.datas.Add(data);
        }

        public void Delete(T data)
        {
            AppDbContext<T>.datas.Remove(data);
        }

        public T Get(Predicate<T> filter)
        {
            return AppDbContext<T>.datas.Find(filter);
        }

        public List<T> GetAll(Predicate<T> filter = null)
        {
            if (filter == null)
            {
                return AppDbContext<T>.datas;
            }
            else
            {
                return AppDbContext<T>.datas.FindAll(filter);
            }
        }

        public void Update(T data)
        {
           
        }
    }
}
