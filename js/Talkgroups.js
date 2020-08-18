window.addEventListener('load', function() {

    var elements2 = document.getElementsByClassName('TgHeader');


    for (var i = 0; i < elements2.length; i++) {
        elements2[i].innerHTML = "Talgrupp " + (i + 1);
    }
});

document.getElementById("talkgroupInfo").addEventListener("click", function () {

    document.getElementById("statusButton").hidden = true;
    document.getElementById("urlButton").hidden = true;
    document.getElementById("quickButton").hidden = true;
    document.getElementById("portButton").hidden = true;
    document.getElementById("shortNumberButton").hidden = true;
    document.getElementById("quickButton").hidden = true;
    document.getElementById("talkgroupButton").hidden = false;
});
