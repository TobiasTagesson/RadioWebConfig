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

function AdminButton(name, licenseNr, orgNr, issi) {
    this.adNamn = name;
    this.adLicenseNumber = licenseNr;
    this.adOrgNr = orgNr;
    this.adIssi = issi;
}


document.getElementById("downloadFile").addEventListener("click", function () {
//function downloadFile()  {
    var statusElements = document.getElementsByClassName("statusTB");
    var tgElements = document.getElementsByClassName("tgTB");
    var portElements = document.getElementsByClassName("portTB");
    var shortElements = document.getElementsByClassName("shortTB");
    var urlElements = document.getElementsByClassName("urlTB");
    var quickElements = document.getElementsByClassName("quickTB");
    var adminElements = document.getElementsByClassName("adminTB");

    var statusArray = [];
    var tgArray = [];
    var portArray = [];
    var shortArray = [];
    var linkArray = [];
    var quickArray = [];
    var adminArray = []; // TODO: gör om till objekt eftersom admin aldrig kommer vara mer än ETT objekt.
   // var adminStr;


    for (var i = 0; i < statusElements.length; i++) {
        //"MainContent_statusDataList_TextBox1_0"


        //includes funkar inte i IExplorer, därav indexOf > -1 som också returnerar bool

        if (statusElements[i].id.indexOf("TextBox1") > -1) {
        // if (statusElements[i].id.includes("TextBox1")) {
            var statusObject = new Status();
            statusObject.stName = statusElements[i].value;
        }
        else if (statusElements[i].id.indexOf("TextBox2") > -1) {
        //else if (statusElements[i].id.includes("TextBox2")) {
            statusObject.stStatus = statusElements[i].value;
        }
        else if (statusElements[i].id.indexOf("TextBox3") > -1) {
        //else if (statusElements[i].id.includes("TextBox3")) {
            statusObject.stDest1 = statusElements[i].value;
        }
        else if (statusElements[i].id.indexOf("TextBox4") > -1) {
        //else if (statusElements[i].id.includes("TextBox4")) {
            statusObject.stDest2 = statusElements[i].value;
            statusArray.push(statusObject); 
        }
    }



    for (var j = 0; j < tgElements.length; j++) {
        //"MainContent_tgDataList_TextBox1_0"
        if (tgElements[j].id.indexOf("TextBox1") > -1) {
        //if (tgElements[j].id.includes("TextBox1")) {
            var tgObject = new Talkgroup();
            tgObject.tgName = tgElements[j].value;
        }
        else if (tgElements[j].id.indexOf("TextBox2") > -1) {
        //else if (tgElements[j].id.includes("TextBox")) {
            tgObject.tgGissi = tgElements[j].value;
            tgArray.push(tgObject);
        }
    }



    for (var i = 0; i < portElements.length; i++) {
        //"MainContent_portDataList_TextBox1_0"
        if (portElements[i].id.indexOf("TextBox1") > -1) {
        //if (portElements[i].id.includes("TextBox1")) {
            var portObject = new Port();
            portObject.portName = portElements[i].value;
        }
        else if (portElements[i].id.indexOf("TextBox2") > -1) {
        //else if (portElements[i].id.includes("TextBox2")) {
            portObject.portStatus = portElements[i].value;
        }
        else if (portElements[i].id.indexOf("TextBox3") > -1) {
        //else if (portElements[i].id.includes("TextBox3")) {
            portObject.portDest = portElements[i].value;
            portArray.push(portObject);
        }
    }


    for (var j = 0; j < shortElements.length; j++) {
        //"MainContent_shortNrDataList_TextBox1_0"

        if (shortElements[j].id.indexOf("TextBox1") > -1) {
       // if (shortElements[j].id.includes("TextBox1")) {
            var shortObject = new ShortNumber();
            shortObject.shortName = shortElements[j].value;
            }
        else if (shortElements[j].id.indexOf("TextBox2") > -1) {
       // else if (shortElements[j].id.includes("TextBox2")) {
            shortObject.shortNr = shortElements[j].value;
            shortArray.push(shortObject);
        }

    }

    for (var j = 0; j < urlElements.length; j++) {
        //"MainContent_urlDataList_TextBox1_0"
        if (urlElements[j].id.indexOf("TextBox1") > -1) {
       // if (urlElements[j].id.includes("TextBox1")) {
            var linkObject = new Link();
            linkObject.linkName = urlElements[j].value;
        }
        else if (urlElements[j].id.indexOf("TextBox2") > -1) {
        //else if (urlElements[j].id.includes("TextBox2")) {
            linkObject.linkUrl = urlElements[j].value;
            linkArray.push(linkObject);
        }
    } 

    for (var i = 0; i < quickElements.length; i++) {
        //"MainContent_quickButtonDataList_TextBox1_0"
        if (quickElements[i].id.indexOf("TextBox1") > -1) {
        //if (quickElements[i].id.includes("TextBox1")) {
            var quickObject = new QuickButton();
            quickObject.qbName = quickElements[i].value;
        }
        else if (quickElements[i].id.indexOf("TextBox2") > -1) {
        //else if (quickElements[i].id.includes("TextBox2")) {
            quickObject.qbStatus = quickElements[i].value;
        }
        else if (quickElements[i].id.indexOf("TextBox3") > -1) {
        //else if (quickElements[i].id.includes("TextBox3")) {
            quickObject.qbDest1 = quickElements[i].value;
        }
        else if (quickElements[i].id.indexOf("TextBox4") > -1) {
        //else if (quickElements[i].id.includes("TextBox4")) {
            quickObject.qbDest2 = quickElements[i].value;
            quickArray.push(quickObject);
        }
    }

        for (var k = 0; k < adminElements.length; k++) {
            //"MainContent_quickButtonDataList_TextBox1_0"
            if (adminElements[k].id.indexOf("TextBox1") > -1) {
           // if (adminElements[k].id.includes("TextBox1")) {
                var adminObject = new AdminButton();
                adminObject.adNamn = adminElements[k].value;
            }
            else if (adminElements[k].id.indexOf("TextBox2") > -1) {
            //else if (adminElements[k].id.includes("TextBox2")) {
                adminObject.adLicenseNumber = adminElements[k].value;
            }
            else if (adminElements[k].id.indexOf("TextBox3") > -1) {
            //else if (adminElements[k].id.includes("TextBox3")) {
                adminObject.adOrgNr = adminElements[k].value;
            }
            else if (adminElements[k].id.indexOf("TextBox4") > -1) {
           //else if (adminElements[k].id.includes("TextBox4")) {
                adminObject.adIssi = adminElements[k].value;
                adminArray.push(adminObject);
            }
    }

    //if (adminElements[0].id.includes("TextBox1"))
    //{
    //var adminObj = new AdminButton(); 
    //adminObj.adNamn = adminElements.adNamn;
    //}
    //else if (adminElements[0].id.includes("TextBox2"))
    //{
    //adminObj.adLicenseNumber = adminElements.adLicenseNumber;
    //}
    //else if (adminElements[0].id.includes("TextBox3"))
    //{
    //adminObj.adOrgNr = adminElements.adOrgNr;
    //}
    //else if (adminElements[0].id.includes("TextBox4"))
    //{
    //adminObj.adIssi = adminElements.adIssi;
    //adminStr = adminObject;
    //}

    //"{statusArr:" + JSON.stringify(statusArray) + ", tgArr:" + JSON.stringify(tgArray) + ", portArr:" + JSON.stringify(portArray) + ", shortArr:" + JSON.stringify(shortArray) + ", linkArr:" + JSON.stringify(linkArray) + ", quickArr:" + JSON.stringify(quickArray) + "}",
    $.ajax({
        type: "POST",
        url: "Default.aspx/DownloadFile_Click",
        data: "{'statusArr':'" + JSON.stringify(statusArray) + "', 'tgArr':'" + JSON.stringify(tgArray) + "', 'portArr':'" + JSON.stringify(portArray) + "', 'shortArr':'" + JSON.stringify(shortArray) + "', 'linkArr':'" + JSON.stringify(linkArray) + "', 'quickArr':'" + JSON.stringify(quickArray) + "', 'adminArr':'" + JSON.stringify(adminArray) + "'}", 
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
        },
        success: function (result) {
            alert("We returned: " + result);
        }
    });
}
)
;



