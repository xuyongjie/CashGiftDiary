using Entity;
using Entity.ResultModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CashGiftDiary.Web.Repo
{
    public class ActivityRepository : BaseRepo<Activity, string>, IActivityRepository
    {
        private readonly DiaryDbContext _dbContext;
        public ActivityRepository(DiaryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public BaseResultModel<IEnumerable<Activity>> GetActivities(string phone, bool withDetail)
        {
            var result = new BaseResultModel<IEnumerable<Activity>>();
            var user = _dbContext.Users.Where(u => u.Phone == phone).FirstOrDefault();
            if (user != null)
            {
                IEnumerable<Activity> activities = null;
                if (withDetail)
                {
                    activities = _dbContext.Activities.Where(a => a.UserId == user.Id).Include(a => a.Costs).Include(a => a.CashGiftIns).OrderByDescending(a => a.CreateTime);
                }
                else
                {
                    activities = _dbContext.Activities.Where(a => a.UserId == user.Id).OrderByDescending(a => a.CreateTime);
                }
                result.StatusCode = Constant.STATUS_CODE_OK;
                result.Desc = "获取成功";
                result.ResponseData = activities;
            }
            else
            {
                result.StatusCode = Constant.STATUS_CODE_ERROR;
                result.Desc = "获取失败，用户不存在";
            }
            return result;
        }
    }
}
