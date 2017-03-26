using System;

namespace Entity
{
    public class CashGiftOut : ModelBase
    {
        public string UserId { get; set; }
        public string ActivityTitle { get; set; }
        public DateTime ActivityCelebrateDate { get; set; }
        public string ActivityCelebrateAddress { get; set; }
        public double CashAmount { get; set; }
        public string Extra { get; set; }
        public void Modify(CashGiftOut another)
        {
            ActivityTitle = another.ActivityTitle;
            ActivityCelebrateDate = another.ActivityCelebrateDate;
            ActivityCelebrateAddress = another.ActivityCelebrateAddress;
            CashAmount = another.CashAmount;
            Extra = another.Extra;
        }
    }
}
