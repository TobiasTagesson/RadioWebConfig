
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