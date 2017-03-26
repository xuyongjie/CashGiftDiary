namespace Entity
{
    public class CashGiftIn : ModelBase
    {
        public string ActivityId { get; set; }
        public string Name { get; set; }
        public double CashAmount { get; set; }
        public string Extra { get; set; }

        public void Modify(CashGiftIn another)
        {
            Name = another.Name;
            CashAmount = another.CashAmount;
            Extra = another.Extra;
        }
    }
}
