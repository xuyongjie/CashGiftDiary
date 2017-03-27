using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entity.ResultModel
{
    public class BaseResultModel<T>
    {
        public BaseResultModel() { }
        public BaseResultModel(int status,string desc=null)
        {
            StatusCode = status;
            Desc = desc;
        }
        public int StatusCode { get; set; }
        public string Desc { get; set; }
        public T ResponseData { get; set; }
    }
}
