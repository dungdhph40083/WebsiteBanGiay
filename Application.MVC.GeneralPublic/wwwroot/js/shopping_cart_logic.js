// #,###,##0
function ThousandSeparator(MyNumber) {
    return MyNumber.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

// -1
function Remove1(ItemID) {
    var ProductAmount = document.getElementById("ProductAmount_" + ItemID);
    var PriceSingle = document.getElementById("PriceSingle_" + ItemID);
    var PriceTotal = document.getElementById("PriceTotal_" + ItemID);

    var CurrentQuantity = parseInt(ProductAmount.value == '' ? 0 : ProductAmount.value);

    if (!isNaN(CurrentQuantity)) {
        ProductAmount.value = Math.max(CurrentQuantity - 1, 1);
        PriceTotal.innerText =
            ThousandSeparator(parseInt(PriceSingle.innerText.replace(',', '')) * ProductAmount.value);
    }
}

function Edit1(ItemID) {
    var ProductAmount = document.getElementById("ProductAmount_" + ItemID);
    var PriceSingle = document.getElementById("PriceSingle_" + ItemID);
    var PriceTotal = document.getElementById("PriceTotal_" + ItemID);

    var CurrentQuantity = 1;

    if (ProductAmount.value < 0) ProductAmount.value = 1;

    if (ProductAmount.value <= 0 || ProductAmount.value == '') CurrentQuantity = 1;
    else CurrentQuantity = parseInt(ProductAmount.value);

    if (!isNaN(CurrentQuantity)) {
        PriceTotal.innerText =
            ThousandSeparator(parseInt(PriceSingle.innerText.replace(',', '')) * CurrentQuantity);
    }
}

// +1
function Add1(ItemID) {
    var ProductAmount = document.getElementById("ProductAmount_" + ItemID);
    var PriceSingle = document.getElementById("PriceSingle_" + ItemID);
    var PriceTotal = document.getElementById("PriceTotal_" + ItemID);

    var CurrentQuantity = parseInt(ProductAmount.value == '' ? 0 : ProductAmount.value);

    if (!isNaN(CurrentQuantity)) {
        ProductAmount.value = CurrentQuantity + 1;
        PriceTotal.innerText =
            ThousandSeparator(parseInt(PriceSingle.innerText.replace(',', '')) * ProductAmount.value);
    }
}

function Remove1Generic() {
    var ProductAmount = document.getElementById("ProductAmount");
    var CurrentQuantity = parseInt(ProductAmount.value == '' ? 0 : ProductAmount.value);

    if (!isNaN(CurrentQuantity)) {
        ProductAmount.value = Math.max(CurrentQuantity - 1, 1);
    }
}

function Edit1Generic() {
    var ProductAmount = document.getElementById("ProductAmount");
    var CurrentQuantity = parseInt(ProductAmount.value == '' ? 0 : ProductAmount.value);
    var Max = parseInt(ProductAmount.max);

    if (ProductAmount.value < 0) ProductAmount.value = 1;
    if (ProductAmount.value > Max) ProductAmount.value = Max;
}

// +1
function Add1Generic() {
    var ProductAmount = document.getElementById("ProductAmount");
    var CurrentQuantity = parseInt(ProductAmount.value == '' ? 0 : ProductAmount.value);
    var Max = ProductAmount.max;

    if (!isNaN(CurrentQuantity)) {
        if (CurrentQuantity < Max) {
            ProductAmount.value = CurrentQuantity + 1;
        }
        else ProductAmount.value = Max;
    }
}