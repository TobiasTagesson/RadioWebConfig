﻿window.addEventListener('load', function () {

    var elements = document.getElementsByClassName('QuickHeader');


    for (var i = 0; i < elements.length; i++) {
        elements[i].innerHTML = "Snabbknapp " + (i + 1);
    }
});

document.getElementById("quickButtonInfo").addEventListener("click", function () {

    document.getElementById("statusButton").hidden = true;
    document.getElementById("shortNumberButton").hidden = true;
    document.getElementById("quickButton").hidden = true;
    document.getElementById("talkgroupButton").hidden = true;
    document.getElementById("portButton").hidden = true;
    document.getElementById("urlButton").hidden = true;
    document.getElementById("quickButton").hidden = false;
});