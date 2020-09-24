
$(document).ready(function () {
    $('#tabs').tabs();
    $('#focused').click();
});


//Metod för att göra passwordvalidering
$(document).ready(function () {
    $('#cPassword').on('keyup',
        function validation() {
            if ($('#pwInput').val() == $('#cPassword').val()) {
                $('#pwmessage').html('Match!').css('color', 'green');
  
            }
            else {
                $('#pwmessage').html('Lösenord matchar inte').css('color', 'red');

            }
        });
});

// Metod för att se till att rätt användare raderas
function ConfirmDelete() {
    //var deleteuser = document.getElementById('deleteUserInput').value;
    var confirm_value = document.createElement("INPUT");
    confirm_value.type = "hidden";
    confirm_value.name = "confirm_value";
    if (confirm("Vill du radera användare: ")) {
        confirm_value.value = "Ja";
    } else {
        confirm_value.value = "Nej";
    }
    document.forms[0].appendChild(confirm_value);
}




//function DeleteUserMessageBox(userName) {
//    var txt;

//    if (confirm('Vill du radera användare: ' + userName)) {
//        txt = "Användare raderad";
//    }
//    else {
//        txt = "Användare EJ raderad";
//    }
        
//};
