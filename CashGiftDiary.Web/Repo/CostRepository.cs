using Entity;
using System.Collections.Generic;
using System.Linq;

namespace CashGiftDiary.Web.Repo
{
    public class CostRepository : BaseRepo<Cost, string>, ICostRepository
    {
        private readonly DiaryDbContext _dbContext;
        public CostRepository(DiaryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public double CalculateTotal(string activityId)
        {
            var costs = GetAll(activityId);
            double result = 0;
            foreach(var cost in costs)
            {
                result += cost.Money;
            }
            return result;
        }

        public IEnumerable<Cost> GetAll(string activityId)
        {
            return _dbContext.Costs.Where(c => c.ActivityId == activityId);
        }
    }
}
