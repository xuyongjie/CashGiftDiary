using Entity;
using System.Collections.Generic;

namespace CashGiftDiary.Web.Repo
{
    public interface ICashGiftInRepository:IBaseRepo<CashGiftIn,string>
    {
        IEnumerable<CashGiftIn> GetAll(string activityId);
    }
}
