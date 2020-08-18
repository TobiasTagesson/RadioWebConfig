var tl;
loadTruckList();

function loadTruckList() {

    $.ajax({
        type: "POST",
        url: "Default.aspx/GetTruckList",
        data: "",
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
        },
        success: function (truckList) {
            if (truckList.d !== null) {
                //alert("We returned: " + truckList);
                //ul = document.createElement('ul');
                //ul.setAttribute('id', 'fileList');
                tl = truckList.d;
                //var element = document.getElementById("dropDown").appendChild(ul);
                var ul = document.getElementById('dropDown');

                for (var i = 0; i < tl.length; i++) {
                    var li = document.createElement("li");
                    var a = document.createElement("a");
                    li.setAttribute('id', 'tl' + i);
                    //li.setAttribute('runat', 'server');
                    a.setAttribute('href', '#');
                    ul.appendChild(li);
                    li.appendChild(a);
                    a.innerHTML = a.innerHTML + tl[i];

                    document.getElementById('tl' + i).addEventListener('click', function (e) {
                        //if (e.target && e.target.innerHTML == tl[i]) {
                        var fileConfig = e.currentTarget.innerText;
                        $.ajax({
                            type: "POST",
                            url: "Default.aspx/OpenFile_Click",
                            data: "{'fileToOpen':'" + fileConfig + "'}",
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
                            },
                            success: function (lists) {
                                alert("" + lists);

                                //"MainContent_statusDataList_TextBox2_0"
                                //var lastFive = id.substr(id.length - 5); 
                                //var lastChar = id.substr(id.length - 1);
                                //var tabId = id.split("_").pop();

                                //var ti = statusTB[i].id.substr(35, 3);

                                //if (ti == textID) {


                                document.getElementById("check").checked = true;

                                var k = 0;

                                var statusTB = document.getElementsByClassName("statusTB");
                                var tgTB = document.getElementsByClassName("tgTB");
                                var portTB = document.getElementsByClassName("portTB");
                                var shortTB = document.getElementsByClassName("shortTB");
                                var urlTB = document.getElementsByClassName("urlTB");
                                var quickTB = document.getElementsByClassName("quickTB");

                                //var i = 0;


                                for (var x in lists.d[0].statusList) {

                                    var textID = ("_" + x);
                                    var tabId = statusTB[x].id.split("_").pop();

                                    for (var k = k; k < statusTB.length; k++) {

                                        statusTB[k].value = lists.d[0].statusList[x].stName;
                                        k++;
                                        statusTB[k].value = lists.d[0].statusList[x].stStatus;
                                        k++;
                                        statusTB[k].value = lists.d[0].statusList[x].stDest1;
                                        k++;
                                        statusTB[k].value = lists.d[0].statusList[x].stDest2;
                                        k++;

                                        break;
                                    }
                                }

                                k = 0;
                                for (var x in lists.d[0].tgList) {


                                    for (var k = k; k < tgTB.length; k++) {

                                        tgTB[k].value = lists.d[0].tgList[x].tgName;
                                        k++;
                                        tgTB[k].value = lists.d[0].tgList[x].tgGissi;
                                        k++;

                                        break;
                                    }
                                }
                                k = 0;
                                for (var x in lists.d[0].portList) {


                                    for (var k = k; k < portTB.length; k++) {

                                        portTB[k].value = lists.d[0].portList[x].portName;
                                        k++;
                                        portTB[k].value = lists.d[0].portList[x].portStatus;
                                        k++;
                                        portTB[k].value = lists.d[0].portList[x].portDest;
                                        k++;

                                        break;
                                    }
                                }
                                k = 0;
                                for (var x in lists.d[0].shortList) {


                                    for (var k = k; k < shortTB.length; k++) {

                                        shortTB[k].value = lists.d[0].shortList[x].shortName;
                                        k++;
                                        shortTB[k].value = lists.d[0].shortList[x].shortNr;
                                        k++;

                                        break;
                                    }
                                }
                                k = 0;
                                for (var x in lists.d[0].linkList) {


                                    for (var k = k; k < urlTB.length; k++) {

                                        urlTB[k].value = lists.d[0].linkList[x].linkName;
                                        k++;
                                        urlTB[k].value = lists.d[0].linkList[x].linkUrl;
                                        k++;

                                        break;
                                    }
                                }
                                k = 0;
                                for (var x in lists.d[0].quickList) {

                                    for (var k = k; k < quickTB.length; k++) {

                                        quickTB[k].value = lists.d[0].quickList[x].qbName;
                                        k++;
                                        quickTB[k].value = lists.d[0].quickList[x].qbStatus;
                                        k++;
                                        quickTB[k].value = lists.d[0].quickList[x].qbDest1;
                                        k++;
                                        quickTB[k].value = lists.d[0].quickList[x].qbDest2;
                                        k++;

                                        break;
                                    }
                                }
                            }
                        });
                    });
                }
            }
            else {
                alert("Det finns inga configfiler att ändra för denna användare.");
            }
        }
    });
};


//var j = 0;
//var k = 0;

//var statusTB = document.getElementsByClassName("statusTB");

//var statusArr = [].slice.call(statusTB);
//statusArr.sort();

//for (var x in statusArr) {

//    var tabId = statusTB[x].id.split("_").pop();

//    if (tabId == j) {

//        if (k == 0) {
//            statusTB[x].innerHTML = lists.d[0].statusList[x].stName;
//            k++;
//        }
//        else if (k == 1) {
//            statusTB[x].innerHTML = lists.d[0].statusList[x].stStatus;
//            k++;
//        }
//        else if (k == 2) {
//            statusTB[x].innerText = lists.d[0].statusList[x].stDest1;
//            k++;
//        }
//        else if (k == 3) {
//            statusTB[x].innerText = lists.d[0].statusList[x].stDest2;
//            k = 0;
//            j++;
//        }

//    }
//}

