window.addEventListener('load', function () {

    var elements = document.getElementsByClassName('StatusHeader');


    for (var i = 0; i < elements.length; i++) {
        elements[i].innerHTML = "Status " + (i + 1);
    }
});

document.getElementById("statusInfo").addEventListener("click", function () {

    document.getElementById("talkgroupButton").hidden = true;
    document.getElementById("urlButton").hidden = true;
    document.getElementById("quickButton").hidden = true;
    document.getElementById("portButton").hidden = true;
    document.getElementById("shortNumberButton").hidden = true;
    document.getElementById("quickButton").hidden = true;
    document.getElementById("adminButton").hidden = true;
    document.getElementById("addUserButton").hidden = true;


    document.getElementById("statusButton").hidden = false;

});
