namespace SalesDatePrediction.Application.DTOs
{
    public class CreateOrderRequest
    {
        
        public int EmpId { get; set; }
        public int ShipperId { get; set; }
        public string ShipName { get; set; }=null!;
        public string ShipAddress { get; set; }=null!;
        public string ShipCity { get; set; }=null!;
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public string ShipCountry { get; set; }=null!;
        public decimal Freight { get; set; }

        
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public decimal Discount { get; set; }
    }
}
