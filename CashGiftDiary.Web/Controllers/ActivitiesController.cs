using CashGiftDiary.Web.Models.ResultModel;
using CashGiftDiary.Web.Repo;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CashGiftDiary.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class ActivitiesController : Controller
    {
        private readonly IActivityRepository _activityRepo;
        private readonly IUserRepository _userRepo;
        public ActivitiesController(IActivityRepository activityRepo, IUserRepository userRepo)
        {
            _activityRepo = activityRepo;
            _userRepo = userRepo;
        }

        [HttpGet]
        [ActionName("all")]
        public BaseResultModel<IEnumerable<Activity>> GetActivities([FromQuery]bool withDetail=false)
        {
            return _activityRepo.GetActivities(User.Identity.Name,withDetail);
        }
        [HttpPost]
        [ActionName("add")]
        public BaseResultModel<Activity> CreateActivity([FromBody] Activity model)
        {
            if (!ModelState.IsValid)
            {
                return new BaseResultModel<Activity>(Constant.STATUS_CODE_ERROR, "模型参数校验失败");
            }
            else
            {
                var user = _userRepo.GetUserByPhone(User.Identity.Name);
                if (user != null)
                {
                    model.UserId = user.Id;
                    return new BaseResultModel<Activity>(Constant.STATUS_CODE_OK, "创建成功") { ResponseData = _activityRepo.Add(model) };
                }
                else
                {
                    return new BaseResultModel<Activity>(Constant.STATUS_CODE_ERROR, "用户不存在");
                }
            }
        }

        [HttpPost]
        [ActionName("modify")]
        public BaseResultModel<Activity> ModifyActivity([FromBody] Activity model)
        {
            if (string.IsNullOrEmpty(model.Id)||!ModelState.IsValid)
            {
                return new BaseResultModel<Activity>(Constant.STATUS_CODE_ERROR, "模型参数校验失败");
            }
            else
            {
                var ac = _activityRepo.GetByKey(model.Id);
                if(ac==null||ac.UserId!=_userRepo.GetUserByPhone(User.Identity.Name).Id)
                {
                    return new BaseResultModel<Activity>(Constant.STATUS_CODE_ERROR, "活动不存在或者活动不属于本用户");
                }
                else
                {
                    ac.Modify(model);
                    _activityRepo.Modify(ac);
                    return new BaseResultModel<Activity>(Constant.STATUS_CODE_OK, "修改成功") { ResponseData = ac };
                }
            }
        }
        [HttpPost]
        [ActionName("delete")]
        public BaseResultModel<string> DeleteActivity([FromBody]string id)
        {
            if(string.IsNullOrEmpty(id))
            {
                return new BaseResultModel<string>(Constant.STATUS_CODE_ERROR, "活动id不能为空");
            }
            var ac = _activityRepo.GetByKey(id);
            if (ac == null || ac.UserId != _userRepo.GetUserByPhone(User.Identity.Name).Id)
            {
                return new BaseResultModel<string>(Constant.STATUS_CODE_ERROR, "活动不存在或者活动不属于本用户");
            }
            else
            {
                var deleted=_activityRepo.Delete(id);
                return new BaseResultModel<string>(deleted?Constant.STATUS_CODE_OK:Constant.STATUS_CODE_ERROR,deleted?"删除成功":"未知错误");
            }
        }
    }
}
