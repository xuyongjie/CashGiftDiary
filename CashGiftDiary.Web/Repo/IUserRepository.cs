using CashGiftDiary.Web.Models.ResultModel;
using Entity;

namespace CashGiftDiary.Web.Repo
{
    public interface IUserRepository:IBaseRepo<User,string>
    {
        /// <summary>
        /// 用户认证，返回user
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        BaseResultModel<User> CheckUser(string phone,string password);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="verifyCode"></param>
        /// <returns></returns>
        BaseResultModel<string> RegisterUser(string phone, string password);
        User GetUserByPhone(string phone);
    }
}
