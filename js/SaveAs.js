var fileName;

function saveFunction() {

    if (saveBool == "False")
        saveFile();
    else if (saveBool == "True")
        saveDocumentAs();

};

// TODO gör en regex-funktion a la Daniel

var file = window.location.href;
var truckNo = String(file).split("&Truck=");
var truckNo2 = truckNo[1];
var saveSplit = String(truckNo2).split("&SaveAs=");
var saveBool = saveSplit[1];



function saveDocumentAs() {
    
    fileName = prompt("Skriv in fordonskod");
   // alert('save as-funktion');

    $.ajax({
        type: "POST",
        url: "Default.aspx/SaveAsFileName",
        data: "{'val':'" + fileName +  "'}",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (newValue) {
            saveFile();
          //  alert("Value set in session is " + newValue);
        }
    });

};
function validateSave() {
    var orgnr = document.getElementById('adminOrgNr').
    if()
}
