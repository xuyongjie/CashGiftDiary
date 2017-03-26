using Entity;
using System.Collections.Generic;

namespace CashGiftDiary.Web.Repo
{
    public interface ICostRepository:IBaseRepo<Cost,string>
    {
        double CalculateTotal(string activityId);
        IEnumerable<Cost> GetAll(string activityId);
    }
}
