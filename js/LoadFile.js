//TODO felhantering vid inladdning av fil
// Ta fram fordonsnummer
var file = window.location.href;
var truckNo = String(file).split("&Truck=");
var truckNo2 = truckNo[1];
document.addEventListener('DOMContentLoaded', function (truckNo2) {
    $.ajax({
        type: "POST",
        url: "Default.aspx/OpenFile_Click",
        data: "{'fileToOpen':'" + truckNo2 + "'}",
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
        },
        success: function (lists) {

            document.getElementById("check").checked = true;

            var statusTB = document.getElementsByClassName("statusTB");
            var tgTB = document.getElementsByClassName("tgTB");
            var portTB = document.getElementsByClassName("portTB");
            var shortTB = document.getElementsByClassName("shortTB");
            var urlTB = document.getElementsByClassName("urlTB");
            var quickTB = document.getElementsByClassName("quickTB");
            var adminTB = document.getElementsByClassName("adminTB");
            var urlHL = document.getElementsByClassName("urlHL");
            //var licenseTB = document.getElementsByClassName("licenseTB")
            //var orgNrTB = document.getElementsByClassName("orgNrTB");
            //var userOrgNrTB = document.getElementsByClassName("orgNrTB");
            //var adminIssiTB = document.getElementsByClassName("IssiTB");


            var k = 0;


            for (var x in lists.d[0].statusList) {
                if (k >= statusTB.length - 1)
                    break;
                statusTB[k].value = lists.d[0].statusList[x].stName;
                k++;
                statusTB[k].value = lists.d[0].statusList[x].stStatus;
                k++;
                statusTB[k].value = lists.d[0].statusList[x].stDest1;
                k++;
                statusTB[k].value = lists.d[0].statusList[x].stDest2;
                k++;
            }


            k = 0;

            for (var x in lists.d[0].tgList) {
                if (k >= tgTB.length - 1)
                    break;
                tgTB[k].value = lists.d[0].tgList[x].tgName;
                k++;
                tgTB[k].value = lists.d[0].tgList[x].tgGissi;
                k++;
            }
            k = 0;

            for (var x in lists.d[0].portList) {
                if (k >= portTB.length - 1)
                    break;
                portTB[k].value = lists.d[0].portList[x].portName;
                k++;
                portTB[k].value = lists.d[0].portList[x].portStatus;
                k++;
                portTB[k].value = lists.d[0].portList[x].portDest;
                k++;
            }

            k = 0;

            for (var x in lists.d[0].shortList) {
                if (k >= shortTB.length - 1)
                    break;

                shortTB[k].value = lists.d[0].shortList[x].shortName;
                k++;
                shortTB[k].value = lists.d[0].shortList[x].shortNr;
                k++;

            }


            k = 0;
            l = 0;

            for (var x in lists.d[0].linkList) {
                if (k >= urlTB.length - 1)
                    break;

                urlHL[l].value = lists.d[0].linkList[x].linkUrl;
                l++;
                urlTB[k].value = lists.d[0].linkList[x].linkName;
                k++;
                urlTB[k].value = lists.d[0].linkList[x].linkUrl;
                k++;
            }

            k = 0;

            for (var x in lists.d[0].quickList) {
                if (k >= quickTB.length - 1)
                    break;

                quickTB[k].value = lists.d[0].quickList[x].qbName;
                k++;
                quickTB[k].value = lists.d[0].quickList[x].qbStatus;
                k++;
                quickTB[k].value = lists.d[0].quickList[x].qbDest1;
                k++;
                quickTB[k].value = lists.d[0].quickList[x].qbDest2;
                k++;

            }

            adminTB[0].value = lists.d[0].adminInfo.adNamn;

            adminTB[1].value = lists.d[0].adminInfo.adLicenseNumber;

            adminTB[2].value = lists.d[0].adminInfo.adOrgNr;

            adminTB[3].value = lists.d[0].adminInfo.adIssi;

        }
    });
});
         
