namespace Application.Data.Enums
{
    public enum OrderStatus
    {
        Canceled = 0,       // Hủy đơn
        Refunded = 1,       // Đã hoàn lại đơn
        CantRefund = 2,     // Không đổi trả được nữa
        RefundingAgain = 3, // Đã yêu cầu hoàn lại đơn lần 2+
        Refunding = 4,      // Đã yêu cầu hoàn lại đơn

        Created = 5,        // Đã tạo đơn
        Processed = 6,      // Đã xác nhận đơn
        Delivered = 7,      // Đã giao đơn cho ĐV vận chuyển
        Arrived = 8,        // Đơn hàng thành công
        Received = 9,       // Khách đã nhận hàng
        ReceivedAgain = 10, // Khách đã nhận hàng sau khi đổi trả thất bải
        Bepis = 11          // Bepis
    }

    public enum VisibilityStatus
    {
        Locked = 0,
        Available = 1
    }

    public class DefaultValues
    {
        public const string UserRoleGUID = "1bfa7246-60e1-4d82-a469-cdecf867fd01";
    }

    public class Metadata
    {
        public const string UserID = "UserID";
        public const string Username = "Username";
    }

    public class PaymentMethods
    {
        public const string CashOnDelivery = "CashOnDelivery";
        public const string VNPay = "VNPay";
        public const string MoMo = "MoMo";
    }
}
