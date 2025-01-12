var data = {
    "productId": $('#color').data('productid'),
    "data": ""
};
$.ajax({
    url: "https://localhost:7187/api/LoadData/GetColor",
    dataType: "json",
    method: "POST",
    data: JSON.stringify(data),
    headers: {
        "Content-Type": "application/json"
    },
    success: (rs) => {
        let str = "";
        for (let item of rs) {
            if (item.colorID.trim() != '') {
                console.log(item.colorID);
                str += "<option value='" + item.colorID + "'>" + item.colorName + "</option>";
            }
        }
        $('#color').html(str);
    },
    error: (rs) => {
        console.log(rs);
    }
});
$.ajax({
    url: "https://localhost:7187/api/LoadData/GetSize",
    dataType: "json",
    method: "POST",
    data: JSON.stringify(data),
    headers: {
        "Content-Type": "application/json"
    },
    success: (rs) => {
        console.log(rs);
        let str = "";
        for (let item of rs) {
            if (item.sizeID.trim() != '') {
                str += "<option value='" + item.sizeID + "'>" + item.name + "</option>";
            }
        }
        $('#size').html(str);
    },
    error: (rs) => {
        console.log(rs);
    }
});
setTimeout(() => {
    var dataQuantity = {
        "productId": $('#color').data('productid'),
        "color": $('#color').val(),
        "size": $('#size').val(),
    };
    $.ajax({
        url: "https://localhost:7187/api/LoadData/GetQuantity",
        dataType: "json",
        method: "POST",
        data: JSON.stringify(dataQuantity),
        headers: {
            "Content-Type": "application/json"
        },
        success: (rs) => {
            $('#quantity').html(rs);
        },
        error: (rs) => {
            if (rs.status == 200) {
                $('#quantity').html(rs.responseText);
            }
            console.log(rs);
        }
    });
}, 1000);
$('#size').off('change').on('change', (e) => {
    e.preventDefault();
    var dataQuantity = {
        "productId": $('#color').data('productid'),
        "color": $('#color').val(),
        "size": $('#size').val(),
    };
    $.ajax({
        url: "https://localhost:7187/api/LoadData/GetQuantity",
        dataType: "json",
        method: "POST",
        data: JSON.stringify(dataQuantity),
        headers: {
            "Content-Type": "application/json"
        },
        success: (rs) => {
            $('#quantity').html(rs);
        },
        error: (rs) => {
            if (rs.status == 200) {
                $('#quantity').html(rs.responseText);
            }
            console.log(rs);
        }
    });
})
document.addEventListener("DOMContentLoaded", function () {
    // Lấy phần tử ô nhập số lượng
    const quantityInput = document.getElementById("txtQuantity");

    // Hàm giảm số lượng sản phẩm
    function Remove1Generic() {
        let currentValue = parseInt(quantityInput.value) || 1;
        if (currentValue > 1) {
            quantityInput.value = currentValue - 1;
        }
    }

    // Hàm tăng số lượng sản phẩm
    function Add1Generic() {
        let currentValue = parseInt(quantityInput.value) || 1;
        quantityInput.value = currentValue + 1; // Không có giới hạn tối đa
    }

    // Hàm kiểm tra và chỉnh sửa số lượng hợp lệ khi người dùng nhập trực tiếp
    function Edit1Generic() {
        let currentValue = parseInt(quantityInput.value);
        if (isNaN(currentValue) || currentValue < 1) {
            quantityInput.value = 1; // Gán giá trị tối thiểu là 1 nếu nhập sai
        }
    }

    // Gắn sự kiện cho các nút và ô nhập
    document.querySelector(".btn-minus").addEventListener("click", Remove1Generic);
    document.querySelector(".btn-plus").addEventListener("click", Add1Generic);
    quantityInput.addEventListener("input", Edit1Generic);
    quantityInput.addEventListener("blur", Edit1Generic);
});
$('#color').off('change').on('change', (e) => {
    e.preventDefault();
    var dataQuantity = {
        "productId": $('#color').data('productid'),
        "color": $('#color').val(),
        "size": $('#size').val(),
    };
    $.ajax({
        url: "https://localhost:7187/api/LoadData/GetQuantity",
        dataType: "json",
        method: "POST",
        data: JSON.stringify(dataQuantity),
        headers: {
            "Content-Type": "application/json"
        },
        success: (rs) => {
            $('#quantity').html(rs);
        },
        error: (rs) => {
            if (rs.status == 200) {
                $('#quantity').html(rs.responseText);
            }
            console.log(rs);
        }
    });
})
$('#btnAddCart').off('click').on('click', (e) => {
    e.preventDefault();
    if (parseInt($('#quantity').html().split(' ')[0]) < parseInt($('#txtQuantity').val())) {
        alert('Số lượng trong kho không đủ');
    } else {
        var dataQuantity = {
            "productId": $('#color').data('productid'),
            "color": $('#color').val(),
            "size": $('#size').val(),
        };
        $.ajax({
            url: "https://localhost:7187/api/LoadData/GetProductDetail",
            dataType: "json",
            method: "POST",
            data: JSON.stringify(dataQuantity),
            headers: {
                "Content-Type": "application/json"
            },
            success: (rs) => {
                document.location.href = "/UserCart/Add2Cart?id=" + rs + "&AdditionMode=true&Quantity=" + $('#txtQuantity').val();
            },
            error: (rs) => {
                if (rs.status == 200) {
                    document.location.href = "/UserCart/Add2Cart?id=" + rs.responseText + "&AdditionMode=true&Quantity=" + $('#txtQuantity').val();
                }
                console.log(rs);
            }
        });
    }
})
$('.addCart').off('click').on('click', function(e){
    e.preventDefault();
    var btn = $(this);
    var dataQuantity = {
        "productId": btn.data('productid')
    };
    $.ajax({
        url: "https://localhost:7187/api/LoadData/CheckQuantity",
        dataType: "json",
        method: "POST",
        data: JSON.stringify(dataQuantity),
        headers: {
            "Content-Type": "application/json"
        },
        success: (rs) => {
            if (parseInt(rs) > 0) {
                $.ajax({
                    url: "https://localhost:7187/api/LoadData/GetProductDetail",
                    dataType: "json",
                    method: "POST",
                    data: JSON.stringify(dataQuantity),
                    headers: {
                        "Content-Type": "application/json"
                    },
                    success: (rsDetail) => {
                        document.location.href = "/UserCart/Add2Cart?id=" + rsDetail + "&AdditionMode=true&Quantity=1";
                    },
                    error: (rsDetail) => {
                        if (rsDetail.status == 200) {
                            document.location.href = "/UserCart/Add2Cart?id=" + rsDetail.responseText + "&AdditionMode=true&Quantity=1";
                        }
                        console.log(rsDetail);
                    }
                });
            } else {
                alert('số lượng trong kho không đủ');
            }
        },
        error: (rs) => {
            if (rs.status == 200) {
                if (parseInt(rs.responseText) > 0) {
                    $.ajax({
                        url: "https://localhost:7187/api/LoadData/GetProductDetail",
                        dataType: "json",
                        method: "POST",
                        data: JSON.stringify(dataQuantity),
                        headers: {
                            "Content-Type": "application/json"
                        },
                        success: (rsDetail) => {
                            document.location.href = "/UserCart/Add2Cart?id=" + rsDetail + "&AdditionMode=true&Quantity=1";
                        },
                        error: (rsDetail) => {
                            if (rsDetail.status == 200) {
                                document.location.href = "/UserCart/Add2Cart?id=" + rsDetail.responseText + "&AdditionMode=true&Quantity=1";
                            }
                            console.log(rsDetail);
                        }
                    });
                } else {
                    alert('số lượng trong kho không đủ');
                }
            }
            console.log(rs);
        }
    });
})