function AddStation() {

    var stationNr = prompt('Ange stationskod'); 

    $.ajax({
        type: "POST",
        url: "Stations.aspx/AddNewStation",
        data: "{'val':'" + stationNr + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (newValue) {
            
            alert("Station tillagd");
        }
    });
};

//function addTruckFolder() {
//    var truckNr = prompt('Ange fordonskod');
//    $.ajax({
//        type: "POST",
//        url: "ShowTrucks.aspx/AddTruckNo",
//        data: "{'val':'" + truckNr + "'}",
//        contentType: "application/json; charset=utf-8",
//        dataType: 'json',
            

//    })
//};

function goToAddTruck() {
var file = window.location.href;
var stationNo1 = String(file).split("?name=");
var stationNo2 = stationNo1[1];
    window.location.href = "AddVehicle.aspx?name=" + stationNo2;
}