function Status(name, status, dest1, dest2) {
    this.stName = name;
    this.stStatus = status;
    this.stDest1 = dest1;
    this.stDest2 = dest2;
}

function Talkgroup(name, gissi) {
    this.tgName = name;
    this.tgGissi = gissi;
}

function Port(name, status, dest) {
    this.portName = name;
    this.portStatus = status;
    this.portDest = dest;
}

function ShortNumber(name, number) {
    this.shortName = name;
    this.shortNr = number;
}

function Link(name, url) {
    this.linkName = name;
    this.linkUrl = url;
}

function QuickButton(name, status, dest1, dest2) {
    this.qbName = name;
    this.qbStatus = status;
    this.qbDest1 = dest1;
    this.qbDest2 = dest2;
}

document.getElementById("downloadFile").addEventListener("click", function () {

    var statusElements = document.getElementsByClassName("statusTB");
    var tgElements = document.getElementsByClassName("tgTB");
    var portElements = document.getElementsByClassName("portTB");
    var shortElements = document.getElementsByClassName("shortTB");
    var urlElements = document.getElementsByClassName("urlTB");
    var quickElements = document.getElementsByClassName("quickTB");

    var statusArray = [];
    var tgArray = [];
    var portArray = [];
    var shortArray = [];
    var linkArray = [];
    var quickArray = [];

    for (var i = 0; i < statusElements.length; i++) {
        //"MainContent_statusDataList_TextBox1_0"

        if (statusElements[i].id.charAt(34).match("1")) {
            var statusObject = new Status();
            statusObject.stName = statusElements[i].value;
        }
        else if (statusElements[i].id.charAt(34).match("2")) {
            statusObject.stStatus = statusElements[i].value;
        }
        else if (statusElements[i].id.charAt(34).match("3")) {
            statusObject.stDest1 = statusElements[i].value;
        }
        else if (statusElements[i].id.charAt(34).match("4")) {
            statusObject.stDest2 = statusElements[i].value;
            statusArray.push(statusObject);
        }
    }

    
    for (var j = 0; j < tgElements.length; j++) {
        //"MainContent_tgDataList_TextBox1_0"

        if (tgElements[j].id.charAt(30).match("1")) {
            var tgObject = new Talkgroup();
            tgObject.tgName = tgElements[j].value;
        }
        else if (tgElements[j].id.charAt(30).match("2")) {
            tgObject.tgGissi = tgElements[j].value;
            tgArray.push(tgObject);
        }
    }

    for (var i = 0; i < portElements.length; i++) {
        //"MainContent_portDataList_TextBox1_0"

        if (portElements[i].id.charAt(32).match("1")) {
            var portObject = new Port();
            portObject.portName = portElements[i].value;
        }
        else if (portElements[i].id.charAt(32).match("2")) {
            portObject.portStatus = portElements[i].value;
        }
        else if (portElements[i].id.charAt(32).match("3")) {
            portObject.portDest = portElements[i].value;
            portArray.push(portObject);
        }
    }

    for (var j = 0; j < shortElements.length; j++) {
        //"MainContent_shortNrDataList_TextBox1_0"

        if (shortElements[j].id.charAt(35).match("1")) {
            var shortObject = new ShortNumber();
            shortObject.shortName = shortElements[j].value;
        }
        else if (shortElements[j].id.charAt(35).match("2")) {
            shortObject.shortNr = shortElements[j].value;
            shortArray.push(shortObject);
        }
    }

    for (var j = 0; j < urlElements.length; j++) {
        //"MainContent_urlDataList_TextBox1_0"

        if (urlElements[j].id.charAt(31).match("1")) {
            var linkObject = new Link();
            linkObject.linkName = urlElements[j].value;
        }
        else if (urlElements[j].id.charAt(31).match("2")) {
            linkObject.linkUrl = urlElements[j].value;
            linkArray.push(linkObject);
        }
    }

    for (var i = 0; i < quickElements.length; i++) {
        //"MainContent_quickButtonDataList_TextBox1_0"

        if (quickElements[i].id.charAt(39).match("1")) {
            var quickObject = new QuickButton();
            quickObject.qbName = quickElements[i].value;
        }
        else if (quickElements[i].id.charAt(39).match("2")) {
            quickObject.qbStatus = quickElements[i].value;
        }
        else if (quickElements[i].id.charAt(39).match("3")) {
            quickObject.qbDest1 = quickElements[i].value;
        }
        else if (quickElements[i].id.charAt(39).match("4")) {
            quickObject.qbDest2 = quickElements[i].value;
            quickArray.push(quickObject);
        }
    }
    //"{statusArr:" + JSON.stringify(statusArray) + ", tgArr:" + JSON.stringify(tgArray) + ", portArr:" + JSON.stringify(portArray) + ", shortArr:" + JSON.stringify(shortArray) + ", linkArr:" + JSON.stringify(linkArray) + ", quickArr:" + JSON.stringify(quickArray) + "}",
    $.ajax({
        type: "POST",
        url: "Default.aspx/DownloadFile_Click",
        data: "{'statusArr':'" + JSON.stringify(statusArray) + "', 'tgArr':'" + JSON.stringify(tgArray) + "', 'portArr':'" + JSON.stringify(portArray) + "', 'shortArr':'" + JSON.stringify(shortArray) + "', 'linkArr':'" + JSON.stringify(linkArray) + "', 'quickArr':'" + JSON.stringify(quickArray) + "'}", 
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
        },
        success: function (result) {
            alert("We returned: " + result);
        }
    });
});



