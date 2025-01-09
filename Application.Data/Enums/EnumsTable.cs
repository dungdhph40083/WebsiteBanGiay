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
     *  TODO 2:
     *  1. get all: hiển thị các sản phẩm biến thể theo product ID -- DONE
     *  2. get by ID: theo product detail ID để sửa biến thể nếu có (có thể truyền ra list các thay đổi) -- DONE
     *  3. edit: bấm vào sẽ hiển thị hàng loạt biến thể để sửa & các trường thông tin sản phẩm khác (hãng, xuất xứ...) -- DONE
     *  4. delete: xóa theo product ID -- DONE
     *  5. sửa lại view quản lý bên admin
     */


    /*
     *  TODO:
     *  2: sau khi đặt đơn: được phép đổi tăng giảm số lượng mặt hàng & thêm/xóa sản phẩm khác (chỉ trước trạng thái đang giao) [TODO]
     *  5: hoàn đơn về không cộng lại (lưu ở bên bảng hoàn trả - thêm thông tin số lượng hàng đã hoàn trả & giá tiền) -- 50%: THÔNG TIN CHƯA CÓ
     *
     *  Lý do: 
     *  - Hàng đã qua sử dụng
     *  - Hàng giả hàng nhái
     *  - Hàng lỗi
     *  - Khác (cho nhập)
     *
     *  6.5: cho phép gửi ảnh với chọn số lượng muốn hoàn trả (cần thêm bảng ReturnDetail)
     *
     *  7: voucher giảm giá theo % giá trị đơn hàng và/hoặc giảm giá theo số tiền (điều kiện: đơn hàng tối thiểu số tiền tổng trong đơn)
     *  8: phải có size & màu giày (n size/color - n giày) [TODO]
     *  9: sửa icon tăng giảm số lượng sp khi thêm vào giỏ
     *  10: ghép nhánh JWT (LÂU)
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
