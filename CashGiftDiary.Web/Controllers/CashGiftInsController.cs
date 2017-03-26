using CashGiftDiary.Web.Models.ResultModel;
using CashGiftDiary.Web.Repo;
using Entity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CashGiftDiary.Web.Controllers
{
    public class CashGiftInsController:Controller
    {
        private readonly ICashGiftInRepository _cashGiftInRepo;
        private readonly IActivityRepository _activityRepo;
        public CashGiftInsController(ICashGiftInRepository cashGiftInRepo,IActivityRepository activityRepo)
        {
            _cashGiftInRepo = cashGiftInRepo;
            _activityRepo = activityRepo;
        }
        [HttpGet]
        [ActionName("all")]
        public BaseResultModel<IEnumerable<CashGiftIn>> GetAll([FromQuery]string activityId)
        {
            if (string.IsNullOrEmpty(activityId))
            {
                return new BaseResultModel<IEnumerable<CashGiftIn>>(Constant.STATUS_CODE_ERROR, "activityId为空");
            }
            return new BaseResultModel<IEnumerable<CashGiftIn>>(Constant.STATUS_CODE_OK, "获取成功") { ResponseData = _cashGiftInRepo.GetAll(activityId) };
        }


        [HttpPost]
        [ActionName("add")]
        public BaseResultModel<CashGiftIn> CreateIn([FromBody] CashGiftIn model)
        {
            if (!ModelState.IsValid)
            {
                return new BaseResultModel<CashGiftIn>(Constant.STATUS_CODE_ERROR, "模型参数校验失败");
            }
            else
            {
                var activity = _activityRepo.GetByKey(model.ActivityId);
                if (activity == null)
                {
                    return new BaseResultModel<CashGiftIn>(Constant.STATUS_CODE_ERROR, "活动不存在");
                }
                return new BaseResultModel<CashGiftIn>(Constant.STATUS_CODE_OK, "创建成功") { ResponseData = _cashGiftInRepo.Add(model) };
            }
        }

        [HttpPost]
        [ActionName("modify")]
        public BaseResultModel<CashGiftIn> ModifyIn([FromBody] CashGiftIn model)
        {
            if (string.IsNullOrEmpty(model.Id) || !ModelState.IsValid)
            {
                return new BaseResultModel<CashGiftIn>(Constant.STATUS_CODE_ERROR, "模型参数校验失败");
            }
            else
            {
                var giftIn = _cashGiftInRepo.GetByKey(model.Id);
                if (giftIn == null)
                {
                    return new BaseResultModel<CashGiftIn>(Constant.STATUS_CODE_ERROR, "该笔记录不存在");
                }
                else
                {
                    giftIn.Modify(model);
                    _cashGiftInRepo.Modify(giftIn);
                    return new BaseResultModel<CashGiftIn>(Constant.STATUS_CODE_OK, "修改成功") { ResponseData = giftIn };
                }
            }
        }

        [HttpPost]
        [ActionName("delete")]
        public BaseResultModel<string> DeleteIn([FromBody]string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new BaseResultModel<string>(Constant.STATUS_CODE_ERROR, "id不能为空");
            }
            var giftIn = _cashGiftInRepo.GetByKey(id);
            if (giftIn == null)
            {
                return new BaseResultModel<string>(Constant.STATUS_CODE_ERROR, "该笔记录不存在");
            }
            else
            {
                var deleted = _cashGiftInRepo.Delete(id);
                return new BaseResultModel<string>(deleted ? Constant.STATUS_CODE_OK : Constant.STATUS_CODE_ERROR, deleted ? "删除成功" : "未知错误");
            }
        }
    }
}
