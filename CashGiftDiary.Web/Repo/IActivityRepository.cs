using CashGiftDiary.Web.Models.ResultModel;
using Entity;
using System.Collections.Generic;

namespace CashGiftDiary.Web.Repo
{
    public interface IActivityRepository:IBaseRepo<Activity,string>
    {
        BaseResultModel<IEnumerable<Activity>> GetActivities(string phone,bool withDetail);
    }
}
