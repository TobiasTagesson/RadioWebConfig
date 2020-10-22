function AddStation() {

    var stationNr = prompt('Ange stationskod'); 

    $.ajax({
        type: "POST",
        url: "Stations.aspx/AddNewStation",
        data: "{'val':'" + stationNr + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (newValue) {
            if (newValue != null || newValue != "")
            alert("Station tillagd");
        }
    });
};


function goToAddTruck() {
var file = window.location.href;
var stationNo1 = String(file).split("?name=");
var stationNo2 = stationNo1[1];
    window.location.href = "AddVehicle.aspx?name=" + stationNo2 + "&Truck=mall";
}


window.addEventListener('load', function () {
    var b = $('[ID*="AdminHidden"]');

    if (b.val() != "1") {
        $(document).ready(function () {
            $('#addStationBtn').hide();
        })

    }
});