// document.querySelectorAll('input[name="statusFilter"]').forEach(radio => {
//     radio.addEventListener('change', function () {
//         const selectedStatus = this.value;
//         const url = `/ProductDetail/Index?Page=1&PageSize=@ViewBag.PageSize&Status=${selectedStatus}`;
//         window.location.href = url;
//     });
// });

async function toggleStatus(productDetailId) {
    const url = `/ProductDetail/ToggleStatus/${productDetailId}`;
    try {
        const response = await fetch(url, { method: 'PUT' });
        if (response.ok) {
            swal({
                title: "Thành công!",
                text: "Đổi trạng thái thành công!",
                icon: "success",
                timer: 2000,
                buttons: false,
            }).then(() => location.reload());
        } else {
            swal({ title: "Lỗi!", text: "Không thể đổi trạng thái.", icon: "error" });
        }
    } catch {
        swal({ title: "Lỗi!", text: "Đã xảy ra lỗi khi đổi trạng thái.", icon: "error" });
    }
}


document.querySelectorAll('.delete_trigger').forEach(button => {
    button.addEventListener('click', function () {
        const productDetailId = this.getAttribute('data-id');
        const controller = this.getAttribute('data-controller');
        const action = this.getAttribute('data-action');
        const deleteUrl = `/${controller}/${action}/${productDetailId}`;

        // Hiển thị cảnh báo xác nhận xóa với SweetAlert
        swal({
            title: "Bạn có chắc chắn?",
            text: "Sau khi xóa, bạn sẽ không thể khôi phục mục này!",
            icon: "warning",
            buttons: ["Hủy", "Xóa"],
            dangerMode: true,
        }).then((willDelete) => {
            if (willDelete) {
                // Gửi yêu cầu xóa
                fetch(deleteUrl, {
                    method: 'DELETE'
                })
                .then(response => {
                    if (response.ok) {
                        // Thông báo thành công
                        swal({
                            title: "Đã xóa!",
                            text: "Chi tiết sản phẩm đã được xóa thành công.",
                            icon: "success",
                            timer: 2000,
                            buttons: false,
                        }).then(() => {
                            location.reload(); // Tải lại trang sau khi xóa
                        });
                    } else if (response.status === 409) { // Lỗi ràng buộc dữ liệu
                        swal({
                            title: "Không thể xóa!",
                            text: "Sản phẩm này có liên quan đến các dữ liệu khác trong hệ thống.",
                            icon: "error",
                        });
                    } else {
                        // Xử lý lỗi khác (như 400, 500)
                        swal({
                            title: "Lỗi!",
                            text: "Không thể xóa. Vui lòng thử lại.",
                            icon: "error",
                        });
                    }
                })
                .catch(error => {
                    console.error("Error:", error);

                    // Thông báo lỗi hệ thống
                    swal({
                        title: "Lỗi!",
                        text: "Đã xảy ra lỗi trong quá trình xóa.",
                        icon: "error",
                    });
                });
            }
        });
    });
});