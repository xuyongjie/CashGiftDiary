using CashGiftDiary.Web.Models;
using CashGiftDiary.Web.Repo;
using CashGiftDiary.Web.Services;
using Entity;
using Entity.ResultModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CashGiftDiary.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepo;
        public AccountController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpPost]
        [ActionName("logout")]
        public async Task<BaseResultModel<string>> Logout()
        {
            await HttpContext.Authentication.SignOutAsync(Constant.PROJECT_SCHEMA);
            return new BaseResultModel<string>(Constant.STATUS_CODE_OK, "退出登录成功");
        }

        [HttpPost]
        [AllowAnonymous]
        [ActionName("register")]
        public BaseResultModel<string> Register([FromBody]RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                return _userRepo.RegisterUser(model.Phone, model.Password);
            }
            else
            {
                StringBuilder builder = new StringBuilder();
                foreach (var key in ModelState.Keys)
                {
                    builder.Append(key).Append(":");
                    foreach (var item in ModelState[key].Errors)
                    {
                        builder.Append(item.ErrorMessage).AppendLine();
                    }
                }
                return new BaseResultModel<string>() { StatusCode = Constant.STATUS_CODE_ERROR, Desc = builder.ToString() };
            }
        }

        [HttpGet]
        [ActionName("userinfo")]
        public BaseResultModel<User> GetUserInfo()
        {
            return new BaseResultModel<User>(Constant.STATUS_CODE_OK, "获取用户信息成功") { ResponseData = _userRepo.GetUserByPhone(User.Identity.Name) };
        }

        [HttpPost]
        [ActionName("modifyuserinfo")]
        public BaseResultModel<User> ModifyUserInfo([FromBody]User user)
        {
            BaseResultModel<User> result = new BaseResultModel<User>();
            if (!ModelState.IsValid)
            {
                result.StatusCode = Constant.STATUS_CODE_ERROR;
                result.Desc = "模型参数校验失败";
                return result;
            }
            if (HttpContext.User.Identity.Name != user.Phone)
            {
                result.StatusCode = Constant.STATUS_CODE_ERROR;
                result.Desc = "您不能修改非当前用户的用户信息，请检查手机号是否正确";
                return result;
            }
            var former = _userRepo.GetUserByPhone(user.Phone);
            former.ModifyInfo(user);
            _userRepo.Modify(former);
            result.StatusCode = Constant.STATUS_CODE_OK;
            result.Desc = "修改信息成功";
            result.ResponseData = former;
            return result;
        }
        [HttpPost]
        [ActionName("changepassword")]
        public BaseResultModel<string> ChangePassword([FromBody]ChangePasswordModel model)
        {
            BaseResultModel<string> result = new BaseResultModel<string>();
            if (!ModelState.IsValid)
            {
                result.StatusCode = Constant.STATUS_CODE_ERROR;
                result.Desc = "模型参数校验失败";
                return result;
            }
            var user = _userRepo.GetUserByPhone(HttpContext.User.Identity.Name);
            if (user != null)
            {
                PasswordService passService = new PasswordService(model.FormerPassword);
                if (passService.Hash() == user.PasswordHash)
                {
                    user.PasswordHash = new PasswordService(model.NewPassword).Hash();
                    _userRepo.Modify(user);
                    result.StatusCode = Constant.STATUS_CODE_OK;
                    result.Desc = "密码修改成功";
                    return result;
                }
                else
                {
                    result.StatusCode = Constant.STATUS_CODE_ERROR;
                    result.Desc = "原密码错误";
                    return result;
                }
            }
            else
            {
                result.StatusCode = Constant.STATUS_CODE_ERROR;
                result.Desc = "当前用户不存在";
                return result;
            }
        }
    }
}
