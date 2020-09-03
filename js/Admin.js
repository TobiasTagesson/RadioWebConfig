
window.addEventListener('load', function () {

    var elements = document.getElementsByClassName('AdminHeader')
    var adminknappar = ["Kundinfo", "Lägg till användare", "Övrigt"];

    //for (var i = 0; i < elements.length; i++) {
    //    elements[i].innerHTML = "Adminknapp " + (i + 1);
    //}
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
