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

// Metod för att skickas till läggatillnyttfordon (från html-knappen)
function goToAddTruck() {
var file = window.location.href;
var stationNo1 = String(file).split("?name=");
var stationNo2 = stationNo1[1];
    window.location.href = "AddVehicle.aspx?name=" + stationNo2 + "&Truck=createNew";
}


window.addEventListener('load', function () {
    var b = $('[ID*="AdminHidden"]');

    if (b.val() != "1") {
        $(document).ready(function () {
            $('#addStationBtn').hide();
        })

    }
});

function DeleteStation(station) {
    var respons = confirm('Vill du radera station: ' + station);

    if (respons == true) {

        $.ajax({
            type: "POST",
            url: "Stations.aspx/DeleteStation",
            data: "{'val':'" + station + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            success: function (newValue) {
                if (newValue != null || newValue != "")
                alert("Station raderad");
                   
            }
        });
    }
    else {
        alert("Stationen raderas ej");
    }
    
}