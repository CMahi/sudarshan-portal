g_serverpath = '/Sudarshan-Portal';


function setSelectedNote(req_note) {
    try {
        Penalty_Report.GetCurrentTime(req_note, OnSuccess);
    }
    catch (exception) {

    }
}

function OnSuccess(response) {
    document.getElementById("div_header").innerHTML = response.value;
}

function downloadfiles(index) {
    try {
        Penalty_Report.GetFileNames(index, OnSuccess1);
    }
    catch (exception) {

    }
}

function OnSuccess1(response) {
    var app_path = $("#app_Path").val();
    window.open(app_path + '/Common/FileDownload.aspx?indentno=NA&filename=' + response.value + '&filetag=', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
}


function prepareData()
{
    var text2 = $("#txtRemark").val();
    $("#txt_Remark").val(text2);
}


function gotopage(objButton, str) {
    try {
        var pgno = objButton.value;
        var strData = $("#txt_Search").val();
        if (strData != undefined || strData != null) {
            Penalty_Report.fillGoToPage1(strData, pgno, str, callback_Inco);
        }
        else {
            Penalty_Report.fillGoToPage(pgno, str, callback_Inco);
        }
    }
    catch (exception) {

    }
}

function Show_Dtl()
{
    $("#div_dtl").show();
    $("#hidedtl").show();
    $("#showdtl").hide();
}

function Hide_Dtl() {
    $("#div_dtl").hide();
    $("#hidedtl").hide();
    $("#showdtl").show();
}

/*********************************************************************************************************/

function searchData() {
    var str = document.getElementById("txt_Search").value;
    var rpp = "0";
  
    if ($("#ddlRecords :selected").text() != undefined) {
        rpp = $("#ddlRecords :selected").text();
        $("#ddlText1").val($("#ddlRecords :selected").text());
    }
    else {
        rpp = $("#ddlText1").val();
    }

    Penalty_Report.fillSearch(str, rpp, callback_Inco);
}

function callback_Inco(response) {
    document.getElementById("div_InvoiceDetails").innerHTML = response.value;
}

function closePayterm() {
    $('#payterm').modal('hide');
}

function closeInco() {
    $('#incoterm').modal('hide');
}

function viewData1() {
    try {
        var po_val = "";
        po_val = $("#encrypt1").val();
        var app_path = $("#app_Path").val();
        window.open(app_path + '/Common/ViewDocument.aspx?pono=' + po_val + '&type=PO', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no,directories=no');

    }
    catch (exception) {

    }
}
function downloadfiles(index) {
    try {
        Penalty_Report.GetFileNames(index, OnSuccess1);
    }
    catch (exception) {

    }
}