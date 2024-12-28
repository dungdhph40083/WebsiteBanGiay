namespace Application.Data.Enums
{
    public enum OrderStatus
    {
        Canceled = 0,           // Hủy đơn

        Refunding = 1,          // Đã yêu cầu hoàn lại đơn
        RefundingAgain = 2,     // Đã yêu cầu hoàn lại đơn lần 2+
        RefundProcessed = 3,    // Đã chấp nhận hoàn đơn
        RefundDelivered = 4,    // Đã giao hoàn đơn cho ĐV VC
        Refunded = 5,           // Đã nhận & hoàn lại đơn

        DeliveryFailure = 6,    // Giao hàng thất bại (hủy tạm thời)
        DeliveryIsDead = 66,    // Giao hàng thất bại (CHẾT HẲN)

        Created = 100,          // Đã tạo đơn
        Processed = 101,        // Đã xác nhận đơn
        Delivered = 102,        // Đã giao đơn cho ĐV vận chuyển
        Arrived = 103,          // Đơn hàng đã đến

        Received = 200,         // Đã nhận hàng
        ReceivedAgain = 201,    // Đã nhận hàng lại lần 2+
        ReceivedCompleted = 202 // Đã nhận hàng (ko đổi trả được nữa) - thay thế CantRefund

        /* 
         * mã trạng thái:
         * 
         * 0xx: hủy hóa đơn & đổi trả
         * 1xx: giao hàng
         * 2xx: nhận hàng
         * 
         */
    }

    /*
     *  TODO:
     *  
     *  1: xác nhận khách hàng đặt hàng thành công = thêm nút in hóa đơn
     *  -- nút hóa đơn sẽ gửi sang view truyền dữ liệu xong rồi cho phép "IN" sau khi đã rà soát lại thông tin kỹ càng
     *  -- ctrl + P
     *  
     *  2: thêm nút giao hàng thất bại
     *  -- khi đang đc giao đến khách: có nút giao hàng thất bại (nếu giao hàng ko thành = bấm nút)
     *  -- bấm nút = chưa giao được
     *  
     *  -- chưa giao được hàng là quay lại về nút: đang giao tới khách (quay về)
     *  -- có thể lặp đi lặp lại cho đến khi đến 3 lần = hủy
     *  
     *  -- lần 1 & 2 hiện nút thành công & thử lại, lần 3 là hiện 2 nút thành công & THẤT BẠI
     *  
     *  -- sau khi THẤT BẠI: đơn hàng đang được chuyển về shop
     *  3: bộ lọc cho các trạng thái: chờ xác nhận, đang giao hàng, giao hàng thành công/thất bại/chưa giao được = dùng nút
     *  4: tìm kiếm mã đơn hàng = txtbox
     *  5: bỏ nút "để chờ"
     *  6: trang "theo dõi đơn hàng" chuyển thành "đơn hoàn trả"
     *  7: đơn hàng đến nơi = trừ số lượng, nếu sau đó hoàn lại = cộng số lượng
     *  
     */

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
