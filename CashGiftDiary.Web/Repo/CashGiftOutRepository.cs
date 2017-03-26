using Entity;
using System.Collections.Generic;
using System.Linq;

namespace CashGiftDiary.Web.Repo
{
    public class CashGiftOutRepository : BaseRepo<CashGiftOut, string>, ICashGiftOutRepository
    {
        private readonly DiaryDbContext _dbContext;
        public CashGiftOutRepository(DiaryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<CashGiftOut> GetAll(string userId)
        {
            return _dbContext.CashGiftOuts.Where(c => c.UserId == userId).ToList();
        }
    }
}
