using Entity;
using System.Collections.Generic;
using System.Linq;

namespace CashGiftDiary.Web.Repo
{
    public class CashGiftInRepository : BaseRepo<CashGiftIn, string>, ICashGiftInRepository
    {
        private readonly DiaryDbContext _dbContext;
        public CashGiftInRepository(DiaryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<CashGiftIn> GetAll(string activityId)
        {
            return GetBy(from c in _dbContext.CashGiftIns where c.ActivityId == activityId select c);
        }
    }
}
