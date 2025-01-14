document.getElementsByClassName('.delete-trigger').forEach(Button => {
    Button.addEventListener('click', function () {
        const DeleteID = this.getAttribute('data-id');
        const Route = `/Categories/Delete/${DeleteID}`;

        swal({
            title: "Bạn có chắc chắn?",
            text: "Sau khi xóa, bạn sẽ không thể khôi phục mục này!",
            icon: "warning",
            buttons: ["Hủy", "Xòa"]
        }).then((willDelete) => {
            if (willDelete) {

            }
        })
    });
});