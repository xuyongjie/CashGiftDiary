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
    public class CostsController : Controller
    {
        private readonly ICostRepository _costRepo;
        private readonly IActivityRepository _activityRepo;
        private readonly IUserRepository _userRepo;
        public CostsController(ICostRepository costRepo,IActivityRepository activityRepo,IUserRepository userRepo)
        {
            _costRepo = costRepo;
            _activityRepo = activityRepo;
            _userRepo = userRepo;
        }
        [HttpGet]
        [ActionName("all")]
        public BaseResultModel<IEnumerable<Cost>> GetCosts([FromQuery]string activityId)
        {
            if(string.IsNullOrEmpty(activityId))
            {
                return new BaseResultModel<IEnumerable<Cost>>(Constant.STATUS_CODE_ERROR, "ActivityId 不能为空");
            }
            return new BaseResultModel<IEnumerable<Cost>>(Constant.STATUS_CODE_OK, "获取成功") { ResponseData = _costRepo.GetAll(activityId) };
        }
        [HttpPost]
        [ActionName("add")]
        public BaseResultModel<Cost> CreateCost([FromBody] Cost model)
        {
            if (!ModelState.IsValid)
            {
                return new BaseResultModel<Cost>(Constant.STATUS_CODE_ERROR, "模型参数校验失败");
            }
            else
            {
                var activity = _activityRepo.GetByKey(model.ActivityId);
                if(activity==null||activity.UserId!=_userRepo.GetUserByPhone(User.Identity.Name)?.Id)
                {
                    return new BaseResultModel<Cost>(Constant.STATUS_CODE_ERROR, "活动不存在或者活动不属于本用户");
                }
                return new BaseResultModel<Cost>(Constant.STATUS_CODE_OK, "创建成功") { ResponseData = _costRepo.Add(model) };
            }
        }

        [HttpPost]
        [ActionName("modify")]
        public BaseResultModel<Cost> ModifyActivity([FromBody] Cost model)
        {
            if (string.IsNullOrEmpty(model.Id) || !ModelState.IsValid)
            {
                return new BaseResultModel<Cost>(Constant.STATUS_CODE_ERROR, "模型参数校验失败");
            }
            else
            {
                var cost = _costRepo.GetByKey(model.Id);
                if (cost == null)
                {
                    return new BaseResultModel<Cost>(Constant.STATUS_CODE_ERROR, "该开销项不存在");
                }
                else
                {
                    cost.Modify(model);
                    _costRepo.Modify(cost);
                    return new BaseResultModel<Cost>(Constant.STATUS_CODE_OK, "修改成功") { ResponseData = cost };
                }
            }
        }
        [HttpPost]
        [ActionName("delete")]
        public BaseResultModel<string> DeleteActivity([FromBody]string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new BaseResultModel<string>(Constant.STATUS_CODE_ERROR, "开销项id不能为空");
            }
            var cost = _costRepo.GetByKey(id);
            if (cost == null)
            {
                return new BaseResultModel<string>(Constant.STATUS_CODE_ERROR, "该开销项不存在");
            }
            else
            {
                var deleted = _costRepo.Delete(id);
                return new BaseResultModel<string>(deleted ? Constant.STATUS_CODE_OK : Constant.STATUS_CODE_ERROR, deleted ? "删除成功" : "未知错误");
            }
        }
    }
}
