using System;

namespace Entity
{
    public class Cost : ModelBase
    {
        public string ActivityId { get; set; }
        public string CostSummary{ get; set; }
        public double Money { get; set; }
        public string Detail { get; set; }
        public DateTime CostTime { get; set; }
        public void Modify(Cost another)
        {
            Money = another.Money;
            CostSummary = another.CostSummary;
            Detail = another.Detail;
            CostTime = another.CostTime;
        }
    }
}
