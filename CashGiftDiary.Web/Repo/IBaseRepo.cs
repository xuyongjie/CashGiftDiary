using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashGiftDiary.Web.Repo
{
    public interface IBaseRepo<T,TKey>  where T:class
    {                    
        T Add(T item);
        bool Delete(TKey key);
        void Modify(T newItem);
        T GetByKey(TKey key);
        IEnumerable<T> GetBy(IQueryable<T> query);
    }
}
