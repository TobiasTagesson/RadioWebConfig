﻿
window.addEventListener('load', function () {
// TODO Slipa bort arrayen med allt utom kundinfo? Ett fält kanske räcker?
    var elements = document.getElementsByClassName('AdminHeader')
    var adminknappar = ["Kundinfo", "Lägg till användare", "Övrigt"];

    var b = $('[ID*="AdminHidden"]');

    if (b.val() != "1") {
        $(document).ready(function () {
            //$('#userInfo, #adminInfo').hide();
            $('#userInfo').hide();

        })

    }

     for (var i = 0; i < elements.length; i++) {
         elements[i].innerHTML = adminknappar[i];
     }
});


document.getElementById("adminInfo").addEventListener("click", function () {

    document.getElementById("statusButton").hidden = true;
    document.getElementById("shortNumberButton").hidden = true;
    document.getElementById("quickButton").hidden = true;
    document.getElementById("talkgroupButton").hidden = true;
    document.getElementById("portButton").hidden = true;
    document.getElementById("quickButton").hidden = true;
    document.getElementById("urlButton").hidden = true;
    document.getElementById("addUserButton").hidden = true;

    document.getElementById("adminButton").hidden = false;
});
