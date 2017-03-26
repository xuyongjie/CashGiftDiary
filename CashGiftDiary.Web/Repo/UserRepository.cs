using System.Linq;
using CashGiftDiary.Web.Models.ResultModel;
using CashGiftDiary.Web.Services;
using Entity;

namespace CashGiftDiary.Web.Repo
{
    public class UserRepository : BaseRepo<User, string>, IUserRepository
    {
        private readonly DiaryDbContext _dbContext;
        public UserRepository(DiaryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public BaseResultModel<User> CheckUser(string phone, string password)
        {
            var user = GetUserByPhone(phone);
            var result = new BaseResultModel<User>();
            if (user == null)
            {
                result.StatusCode = Constant.STATUS_CODE_ERROR;
                result.Desc = Constant.TIP_PHONE_NOT_REGISTER;
            }
            else
            {
                if (new PasswordService(password).Hash() == user.PasswordHash)
                {
                    result.StatusCode = Constant.STATUS_CODE_OK;
                    result.Desc = "登录成功";
                    result.ResponseData = user;
                }
                else
                {
                    result.StatusCode = Constant.STATUS_CODE_ERROR;
                    result.Desc = "手机号与密码不匹配";
                }
            }
            return result;
        }

        public User GetUserByPhone(string phone)
        {
            return GetBy(from u in _dbContext.Users where phone == u.Phone select u).FirstOrDefault();
        }

        public BaseResultModel<string> RegisterUser(string phone, string password)
        {
            var result = new BaseResultModel<string>();
            var already = GetUserByPhone(phone);
            if (already == null)
            {
                var user = Add(new User { Phone = phone, PasswordHash = new PasswordService(password).Hash() });
                result.StatusCode = Constant.STATUS_CODE_OK;
                result.Desc = "注册成功";
            }
            else
            {
                result.StatusCode = Constant.STATUS_CODE_ERROR;
                result.Desc = "该手机号已注册";
            }
            return result;
        }
    }
}
