using CashGiftDiary.Web.Repo;
using Entity;
using Entity.ResultModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CashGiftDiary.Web.Controllers
{
    public class CashGiftOutsController:Controller
    {
        private readonly ICashGiftOutRepository _cashGiftOutRepo;
        private readonly IUserRepository _userRepo;
        public CashGiftOutsController(ICashGiftOutRepository cashGiftInRepo, IUserRepository userRepo)
        {
            _cashGiftOutRepo = cashGiftInRepo;
            _userRepo = userRepo;
        }
        [HttpGet]
        [ActionName("all")]
        public BaseResultModel<IEnumerable<CashGiftOut>> GetAll()
        {
            return new BaseResultModel<IEnumerable<CashGiftOut>>(Constant.STATUS_CODE_OK, "获取成功") { ResponseData = _cashGiftOutRepo.GetAll(_userRepo.GetUserByPhone(User.Identity.Name).Id) };
        }


        [HttpPost]
        [ActionName("add")]
        public BaseResultModel<CashGiftOut> CreateOut([FromBody] CashGiftOut model)
        {
            if (!ModelState.IsValid)
            {
                return new BaseResultModel<CashGiftOut>(Constant.STATUS_CODE_ERROR, "模型参数校验失败");
            }
            else
            {
                model.UserId = _userRepo.GetUserByPhone(User.Identity.Name).Id;
                return new BaseResultModel<CashGiftOut>(Constant.STATUS_CODE_OK, "创建成功") { ResponseData = _cashGiftOutRepo.Add(model) };
            }
        }

        [HttpPost]
        [ActionName("modify")]
        public BaseResultModel<CashGiftOut> ModifyOut([FromBody] CashGiftOut model)
        {
            if (string.IsNullOrEmpty(model.Id) || !ModelState.IsValid)
            {
                return new BaseResultModel<CashGiftOut>(Constant.STATUS_CODE_ERROR, "模型参数校验失败");
            }
            else
            {
                var giftOut = _cashGiftOutRepo.GetByKey(model.Id);
                if (giftOut == null)
                {
                    return new BaseResultModel<CashGiftOut>(Constant.STATUS_CODE_ERROR, "该笔记录不存在");
                }
                else
                {
                    giftOut.Modify(model);
                    _cashGiftOutRepo.Modify(giftOut);
                    return new BaseResultModel<CashGiftOut>(Constant.STATUS_CODE_OK, "修改成功") { ResponseData = giftOut };
                }
            }
        }

        [HttpPost]
        [ActionName("delete")]
        public BaseResultModel<string> DeleteOut([FromBody]string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new BaseResultModel<string>(Constant.STATUS_CODE_ERROR, "id不能为空");
            }
            var giftOut = _cashGiftOutRepo.GetByKey(id);
            if (giftOut == null)
            {
                return new BaseResultModel<string>(Constant.STATUS_CODE_ERROR, "该笔记录不存在");
            }
            else
            {
                var deleted = _cashGiftOutRepo.Delete(id);
                return new BaseResultModel<string>(deleted ? Constant.STATUS_CODE_OK : Constant.STATUS_CODE_ERROR, deleted ? "删除成功" : "未知错误");
            }
        }
    }
}
