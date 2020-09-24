document.getElementById('loggingOut').addEventListener('click', function () {

    $.ajax({
        type: "POST",
        url: "Default.aspx/ClearSession",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function () {
          
        }
    });
    window.location.href = '/LogIn.aspx';

});

// testfunktion för adminmeny
function loggingOut() {

   

    window.location.href = '/Login.aspx';
}
