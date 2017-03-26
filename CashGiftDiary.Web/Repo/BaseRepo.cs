using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashGiftDiary.Web.Repo
{
    public class BaseRepo<T, TKey> : IBaseRepo<T, TKey> where T : class
    {
        private readonly DiaryDbContext _dbContext;
        public BaseRepo(DiaryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public T Add(T item)
        {
            _dbContext.Add(item);
            _dbContext.SaveChanges();
            return item;
        }

        public bool Delete(TKey key)
        {
            T deleted = GetByKey(key);
            if (deleted != null)
            {
                _dbContext.Remove(deleted);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<T> GetBy(IQueryable<T> query)
        {
            return query.ToList();
        }

        public T GetByKey(TKey key)
        {
            return _dbContext.Find<T>(new object[] { key});
        }

        public void Modify(T newItem)
        {
            _dbContext.Update(newItem);
            _dbContext.SaveChanges();
        }
    }
}
