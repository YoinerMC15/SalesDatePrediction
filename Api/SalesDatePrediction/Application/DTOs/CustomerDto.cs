namespace SalesDatePrediction.Application.DTOs
{
    public class CustomerDto
    {
        public int CustId { get; set; }
        public string CompanyName { get; set; } = null!;
        public DateTime? LastOrderDate { get; set; }
        public DateTime? NextPredictedOrder { get; set; }
    }
}
