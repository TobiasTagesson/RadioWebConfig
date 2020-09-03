window.addEventListener('load', function () {
    var elements2 = document.getElementsByClassName("AddUserHeader")

    //var ad = document.getElementById('AdminHidden').value;

    var b = $('[ID*="AdminHidden"]');

    if (b.val() != "1") {
            $(document).ready(function () {
                $('#userInfo, #adminInfo').hide();
            })

        }
      
        for (var i = 0; i < elements2.length; i++) {
            elements2[i].innerHTML = "Lägg till användare";
        }
    
});

document.getElementById("userInfo").addEventListener("click", function () {

    document.getElementById("talkgroupButton").hidden = true;
    document.getElementById("urlButton").hidden = true;
    document.getElementById("quickButton").hidden = true;
    document.getElementById("portButton").hidden = true;
    document.getElementById("shortNumberButton").hidden = true;
    document.getElementById("quickButton").hidden = true;
    document.getElementById("adminButton").hidden = true;
    document.getElementById("statusButton").hidden = true;
    document.getElementById("addUserButton").hidden = false;
});