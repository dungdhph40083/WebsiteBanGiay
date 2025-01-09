var ListCount = 0;
var AtlasRaw = document.getElementById('DATA_TABLE');
var Atlas = AtlasRaw.innerHTML;
AtlasRaw.childNodes[1].lastElementChild.setAttribute('hidden', '');

function DoAddNewRow() {
    ListCount++;
    let IndexedAtlas =
        Atlas.replaceAll(('_0_'), ('_' + ListCount + '_'))
            .replaceAll(('[0]'), ('[' + ListCount + ']'));


    document.getElementById('DATA_TABLE').insertAdjacentHTML('beforeend', IndexedAtlas);
}

function DoRemoveExistingRow(IndexOf) {
    try {
        document.getElementById('DATA_TABLE').removeChild(IndexOf);
    }
    catch (Exception) {

    }
}

function replaceNumbersInArray(arr, replacement) { return arr.map(str => str.replace(/_\d+_/g, replacement)); }
let inputArray = ["_54_", "_5_", "_95_", "_549_"];
let replacementString = "_X_";

// Replace with your desired string
let resultArray = replaceNumbersInArray(inputArray, replacementString); console.log

