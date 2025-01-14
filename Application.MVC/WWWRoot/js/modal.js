// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(".delete_trigger").on("click", (event) => {
    event.preventDefault(); // do not accidentally refresh

    target = event.target;  // after click, get the "targeted button's data"???
    var id = $(target).data('id');                  // self explainatory
    var controller = $(target).data('controller');  // self explainatory
    var action = $(target).data('action');          // self explainatory

    redirect_url = $(target).data('redirect-url'); // this is probably for redirecting to a page after triggering the function (but since we don't have one this one is probably useless for now)

    url = "/" + controller + "/" + action + "/" + id; // link structure

    document.getElementById('confirm_deletion').setAttribute('href', url);
});

$("#confirm_deletion").on("click", () => {
    var ButtonTxt = document.getElementById("confirm_deletion");
    var ButtonClose = document.getElementById("close-btn");
    var ButtonCancel = document.getElementById("cancel-btn");
    ButtonTxt.className = "btn btn-info";
    ButtonTxt.innerText = "Đang xóa...";
    ButtonTxt.setAttribute("disabled", "disabled");
    ButtonClose.setAttribute("disabled", "disabled");
    ButtonCancel.setAttribute("disabled", "disabled");
});

// all i need in life is jquery and nothing else