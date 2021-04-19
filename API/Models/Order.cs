namespace API.Models
{
    public class Order
    {
        public int OrderId{get;set;}
        public int OrderQuantity{get;set;}
        public decimal OrderTotal{get;set;}
        public string UserName{get;set;}
    }
}