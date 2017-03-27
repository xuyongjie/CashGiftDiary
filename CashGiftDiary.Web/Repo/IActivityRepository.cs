using Entity;
using Entity.ResultModel;
using System.Collections.Generic;

namespace CashGiftDiary.Web.Repo
{
    public interface IActivityRepository:IBaseRepo<Activity,string>
    {
        BaseResultModel<IEnumerable<Activity>> GetActivities(string phone,bool withDetail);
    }
}
