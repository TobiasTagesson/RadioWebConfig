window.addEventListener('load', function () {

    var elements = document.getElementsByClassName('ShortNrHeader');


    for (var i = 0; i < elements.length; i++) {
        elements[i].innerHTML = "Kortnummer " + (i + 1);
    }
});

document.getElementById("shortNumberInfo").addEventListener("click", function () {

    document.getElementById("statusButton").hidden = true;
    document.getElementById("urlButton").hidden = true;
    document.getElementById("quickButton").hidden = true;
    document.getElementById("talkgroupButton").hidden = true;
    document.getElementById("portButton").hidden = true;
    document.getElementById("quickButton").hidden = true;
    document.getElementById("shortNumberButton").hidden = false;
});
