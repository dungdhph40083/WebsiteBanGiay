// #,###,##0
function ThousandSeparator(MyNumber) {
    return MyNumber.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}
var Sum = 0;
var PriceTotalItemsArray = [];

function AddEmUp(ItemPrice) {
    Sum += parseInt(ItemPrice.innerText.replace(/[$,.\s]/g, ''));
};

function Notify() {
    let Notify = document.getElementById("WARNING_WARNING_ALARM");
    Notify.innerHTML = '<p class="text-danger"><i class="fa fa-exclamation"></i> Thay đổi chưa được lưu!</p>';
}

function DisplayTotalPriceOnSidebar() {
    Sum = 0;

    var PriceTotalSidebar = document.getElementById("PriceTotalSidebar");
    var ShipFeeTotalSidebar = document.getElementById("ShipFeeTotalSidebar");
    var GrandTotalSidebar = document.getElementById("GrandTotalSidebar");

    PriceTotalItemsArray = Array.from(document.getElementsByClassName("PriceTotalItems"));
    PriceTotalItemsArray.forEach(AddEmUp);

    PriceTotalSidebar.innerText = ThousandSeparator(Sum);
    ShipFeeTotalSidebar.innerText = ThousandSeparator(parseInt(Sum * 0.01));
    GrandTotalSidebar.innerText = ThousandSeparator(parseInt(Sum * 1.01));
}


// -1
function Remove1(ItemID) {
    let ProductAmount = document.getElementById("ProductAmount_" + ItemID);
    let PriceSingle = document.getElementById("PriceSingle_" + ItemID);
    let PriceTotal = document.getElementById("PriceTotal_" + ItemID);

    Notify();

    let CurrentQuantity = parseInt(ProductAmount.value == '' ? 0 : ProductAmount.value);

    if (!isNaN(CurrentQuantity)) {
        ProductAmount.value = Math.max(CurrentQuantity - 1, 1);
        PriceTotal.innerText =
            ThousandSeparator(parseInt(PriceSingle.innerText.replace(/[$,.\s]/g, '')) * ProductAmount.value);

            DisplayTotalPriceOnSidebar();
    }
}

function Edit1(ItemID) {
    let ProductAmount = document.getElementById("ProductAmount_" + ItemID);
    let PriceSingle = document.getElementById("PriceSingle_" + ItemID);
    let PriceTotal = document.getElementById("PriceTotal_" + ItemID);

    Notify();

    let CurrentQuantity = 1;

    if (ProductAmount.value < 0) ProductAmount.value = 1;

    if (ProductAmount.value <= 0 || ProductAmount.value == '') CurrentQuantity = 1;
    else CurrentQuantity = parseInt(ProductAmount.value);

    if (!isNaN(CurrentQuantity)) {
        PriceTotal.innerText =
            ThousandSeparator(parseInt(PriceSingle.innerText.replace(/[$,.\s]/g, '')) * CurrentQuantity);

        DisplayTotalPriceOnSidebar();
    }
}

// +1
function Add1(ItemID) {
    let ProductAmount = document.getElementById("ProductAmount_" + ItemID);
    let PriceSingle = document.getElementById("PriceSingle_" + ItemID);
    let PriceTotal = document.getElementById("PriceTotal_" + ItemID);

    let CurrentQuantity = parseInt(ProductAmount.value == '' ? 0 : ProductAmount.value);
    Notify();

    if (!isNaN(CurrentQuantity)) {
        ProductAmount.value = CurrentQuantity + 1;
        PriceTotal.innerText =
            ThousandSeparator(parseInt(PriceSingle.innerText.replace(/[$,.\s]/g, '')) * ProductAmount.value);

            DisplayTotalPriceOnSidebar();
    }
}


function Remove1Generic() {
    let ProductAmount = document.getElementById("ProductAmount");
    let CurrentQuantity = parseInt(ProductAmount.value == '' ? 0 : ProductAmount.value);

    if (!isNaN(CurrentQuantity)) {
        ProductAmount.value = Math.max(CurrentQuantity - 1, 1);
    }
}

function Edit1Generic() {
    let ProductAmount = document.getElementById("ProductAmount");
    let CurrentQuantity = parseInt(ProductAmount.value == '' ? 0 : ProductAmount.value);
    let Max = parseInt(ProductAmount.max);

    if (ProductAmount.value < 0) ProductAmount.value = 1;
    if (ProductAmount.value > Max) ProductAmount.value = Max;
}

// +1
function Add1Generic() {
    let ProductAmount = document.getElementById("ProductAmount");
    let CurrentQuantity = parseInt(ProductAmount.value == '' ? 0 : ProductAmount.value);
    let Max = ProductAmount.max;

    if (!isNaN(CurrentQuantity)) {
        if (CurrentQuantity < Max) {
            ProductAmount.value = CurrentQuantity + 1;
        }
        else ProductAmount.value = Max;
    }
}