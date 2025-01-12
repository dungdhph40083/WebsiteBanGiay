namespace Application.Data.Enums
{
    public enum OrderStatus
    {
        Canceled = 0,              // Hủy đơn
                                   
        Refunding = 1,             // Đã yêu cầu hoàn lại đơn
        RefundProcessed = 2,       // Đã chấp nhận hoàn đơn
        RefundDelivered = 3,       // Đã giao hoàn đơn cho ĐV VC
        RefundReceived = 4,        // Đã lấy hàng thành công & đang chuyển về
        Refunded = 5,              // Đã nhận & hoàn lại đơn
                                   
        DeliveryFailure = 6,       // Giao hàng thất bại (hủy tạm thời)
        DeliveryIsDead = 66,       // Giao hàng thất bại (CHẾT HẲN)
                                   
        Created = 100,             // Đã tạo đơn
        Processed = 101,           // Đã xác nhận đơn
        Delivered = 102,           // Đã giao đơn cho ĐV vận chuyển
        Arrived = 103,             // Đơn hàng đã đến
                                   
        Received = 200,            // Đã nhận hàng
        ReceivedAgain = 201,       // Đã nhận hàng lại lần 2+
        ReceivedCompleted = 202,   // Đã nhận hàng (ko đổi trả được nữa) - thay thế CantRefund
        ReceivedRefundFail = 203   // Đã nhận hàng nhưng hoàn trả thất bại
    }

    public enum VoucherStatus
    {
        Disabled = 0,
        Active = 1,

        DisabledPrivate = 100,
        ActivePrivate = 101
    }


    public class OrderFilters
    {
        // Chờ xác nhận
        public const string ORDERS_PENDING = "ORDERS_PENDING";

        // Đang giao hàng
        public const string ORDERS_ONGOING = "ORDERS_ONGOING";

        // Giao hàng thành công
        public const string ORDERS_SUCCEEDED = "ORDERS_SUCCEEDED";

        // Giao hàng thất bại
        public const string ORDERS_FAILED = "ORDERS_FAILED";
    }

    public class VoucherFilters
    {
        
        // Sắp ra mắt
        public const string VOUCHER_COMING_SOON = "VOUCHER_COMING_SOON";
        
        // Đang diễn ra
        public const string VOUCHER_ONGOING = "VOUCHER_ONGOING";
        
        // Chết rồi
        public const string VOUCHER_DIED = "VOUCHER_DIED";

        ////////////////////////////////////////////////////////////////

        // Voucher công khai
        public const string VOUCHER_PUBLIC = "VOUCHER_PUBLIC";

        // Voucher không công khai
        public const string VOUCHER_PRIVATE = "VOUCHER_PRIVATE";
    }

    public class Reasoning
    {
        public const string Generic = "Generic";
        public const string UsedProduct = "UsedProduct";
        public const string BrokenProduct = "BrokenProduct";
        public const string FakeProduct = "FakeProduct";
        public const string Other = "Other";
    }

    public enum VisibilityStatus
    {
        Locked = 0,
        Available = 1
    }

    public class DefaultValues
    {
        public const string UserRoleGUID = "1bfa7246-60e1-4d82-a469-cdecf867fd01";
        public const string CoDGUID = "4c205562-9c3f-418b-8d89-9fb7176dd10e";
        public const string VNPayGUID = "ab2ed960-8765-47e4-8053-7143ecbe824a";
        public const string MoMoGUID = "ab2ed960-8765-47e4-8053-7143ddddd24a";
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
