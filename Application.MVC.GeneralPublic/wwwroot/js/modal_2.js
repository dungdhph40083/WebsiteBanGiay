var url = ""
function GetCartItemDataToDie(ItemID) {
    url = "UserCart/Add2Cart/" + ItemID + "?Quantity=0&AdditionMode=false"
}

$(".delete_trigger").on("click", (event) => function GetCartItemDataToDie(ItemID) {
    event.preventDefault(); // do not accidentally refresh

    // link structure
    url = "UserCart/Add2Cart/" + ItemID + "?Quantity=0&AdditionMode=false"

});

$("#confirm_deletion").on("click", () => {
    var ButtonTxt = document.getElementById("confirm_deletion");
    var ButtonClose = document.getElementById("close-btn");
    var ButtonCancel = document.getElementById("cancel-btn");
    ButtonTxt.className = "btn btn-info";
    ButtonTxt.innerText = "Đang bỏ...";
    $.post(url)
        .always(() => { // ALWAYS DO THIS WHEN GET
            $("#ModalDelete").modal("hide"); // self explainatory - hides the modal

            location = location;

            ButtonTxt.className = "btn btn-danger";
            ButtonTxt.innerText = "Xóa";
            ButtonTxt.removeAttribute("disabled")
            ButtonClose.removeAttribute("disabled");
            ButtonCancel.removeAttribute("disabled");
        });
});