namespace Application.Data.Models
{
    public class CategorizedOrdersCountModel
    {
        public int PendingOrders { get; set; }
        public int OngoingOrders { get; set; }
        public int SucceededOrders { get; set; }
        public int FailedOrders { get; set; }
    }
}
