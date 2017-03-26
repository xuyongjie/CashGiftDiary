using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Entity
{
    public class Activity:ModelBase
    {
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public DateTime CelebrateDate { get; set; }
        public string Address { get; set; }
        public List<Cost> Costs { get; set; }
        public List<CashGiftIn> CashGiftIns { get; set; }
        public void Modify(Activity another)
        {
            Title = another.Title;
            Summary = another.Summary;
            CelebrateDate = another.CelebrateDate;
            Address = another.Address;
        }
    }
}
