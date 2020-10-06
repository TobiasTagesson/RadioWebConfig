
$(document).ready(function () {
    $('#tabs').tabs();
    $('#focused').click();
});


//Metod för att göra passwordvalidering
$(document).ready(function () {
    $('#cPassword').on('keyup',
        function validation() {
            if ($('#pwInput').val() == $('#cPassword').val()) {
                $('#pwmessage').html('Match!').css('color', 'lightgreen');
                $('.addBtn').attr('disabled', false).css('color', 'white');
            }
            else {
                $('#pwmessage').html('Lösenord matchar inte').css('color', 'red');
                $('.addBtn').attr('disabled', true).css('color', 'lightgray', 'border: 2px solid #f57f7f', 'border', '#6d5454');
            }
        });
});

// Metod för att se till att rätt användare raderas
function ConfirmDelete() {
    var userName = $('.deleteUserClass').val();
    var confirm_value = document.createElement("INPUT");
    confirm_value.type = "hidden";
    confirm_value.name = "confirm_value";
    if (confirm("Vill du radera användaren: " + userName)) {
        confirm_value.value = "Ja";
    } else {
        confirm_value.value = "Nej";
    }
    document.forms[0].appendChild(confirm_value);
}

