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

    /*
     *  TODO:
     *  1: voucher là dùng tất cho 1 đơn -- DONE
     *  2: sửa icon tăng giảm số lượng sp khi thêm vào giỏ
     *  3: đẩy các đơn bị hoàn trả sang trang hoàn trả -- DONE
     *  4: quy trình trả: xnhận đơn hoàn => đã liên hệ đvvc đến lấy hàng => đã lấy hàng thành công => đang chuyển hàng về => hoàn hàng thành công -- DONE
     *  5: hiện số trên bộ lọc trạng thái (HINT: <span class="badge badge-light"> xxx </span>) -- DONE
     *  6: voucher áp dụng vào sẽ trừ % tổng giá trị đơn (có yêu cầu giá đơn tối thiểu để dùng voucher) -- DONE
     *  7: ghép nhánh JWT (LÂU)
     */

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
