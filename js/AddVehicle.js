
// Kollar längd och tecken på läggatillfordon
$(document).ready(function () {
    $('.truckInput').on('keyup',
        function validation() {
            var text = document.getElementsByClassName("truckInput")[0].value;
            var re = /^\d{7}$/;
    if (re.test(text)) {
        $('#submitBtn').attr('disabled', false).css('color', 'red');
        $('#valmessage').html('');
            }
    else {
        $('#submitBtn').attr('disabled', true).css('color: lightgray;', 'border: 2px solid #6d5454;');
        $('#valmessage').html('Numret måste bestå av sju siffror').css('color', 'red');
        
    }
});
});

// Kolla längd och tecken på läggatillstation
$(document).ready(function () {
    $('.stationInput').on('keyup',
        function validation() {
            var text = document.getElementsByClassName("stationInput")[0].value;
            var re = /^\d{7}$/;
            if (re.test(text)) {
                $('#submitBtn').attr('disabled', false).css('color', 'red');
                $('#valmessage').html('');
            }
            else {
                $('#submitBtn').attr('disabled', true).css('color: lightgray;', 'border: 2px solid #6d5454;');
                $('#valmessage').html('Numret måste bestå av sju siffror').css('color', 'red');

            }
        });
});


function DeleteTruck(station, truck) {
    var respons = confirm('Vill du radera fordon: ' + truck);

    if (respons == true) {

        $.ajax({
            type: "POST",
            url: "ShowTrucks.aspx/DeleteTruck",
            data: "{'station':'" + station + "', truck:'" + truck + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            success: function (newValue) {
                if (newValue != null || newValue != "")
                    alert("Fordon raderat");

            }
        });
    }
    else {
        alert("Fordonet raderas ej");
    }

}