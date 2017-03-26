using Entity;
using System.Collections.Generic;

namespace CashGiftDiary.Web.Repo
{
    public interface ICashGiftOutRepository:IBaseRepo<CashGiftOut,string>
    {
        IEnumerable<CashGiftOut> GetAll(string userId);
    }
}
