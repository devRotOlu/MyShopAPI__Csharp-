namespace MyShopAPI.Core.Models
{
    public class CreateOrder
    {
        public string intent { get;private set; } = "CAPTURE";
        public List<PurchaseUnit> purchase_units { get; set; } = new List<PurchaseUnit>();
    }

    public class PurchaseUnit
    {
        public string reference_id { get;set; } = null!;
        public Amount amount { get; set; } = new Amount();
    }


    public class Amount
    {
        
        public string currency_code { get; private set; } = "USD";
        public string value { get; set; } = null!;
    }

}
