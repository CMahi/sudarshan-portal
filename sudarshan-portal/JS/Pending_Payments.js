function callback_Inco(response) {
    $("#txt_Data").val(response.value);
    document.getElementById("div_ReportDetails").innerHTML = response.value;
}

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
    var username = $("#txt_Username").val();
    Pending_Payments.fillSearch(str, rpp, username,callback_Inco);
}

function gotopage(objButton, str) {
    try {
        var pgno = objButton.value;
        var strData = $("#txt_Search").val();
        if (strData == undefined || strData == null) {
            strData = "";
        }
        var username = $("#txt_Username").val();
        Pending_Payments.fillGoToPage1(strData, pgno, str, username,callback_Inco);
    }
    catch (exception) {

    }
}

function setSelectedNote(req_note) {
    try {
        Pending_Payments.GetCurrentTime(req_note, OnSuccess);
    }
    catch (exception) {

    }
}

function OnSuccess(response) {
    document.getElementById("div_header").innerHTML = response.value;
}


function Show_Dtl() {
    $("#div_dtl").show();
    $("#hidedtl").show();
    $("#showdtl").hide();
}

function Hide_Dtl() {
    $("#div_dtl").hide();
    $("#hidedtl").hide();
    $("#showdtl").show();
}

function closePayterm() {
    $('#payterm').modal('hide');
}

function closeInco() {
    $('#incoterm').modal('hide');
}

function viewData(index) {
    try {
        var po_val = "";
        po_val = $("#encrypt_po" + index).val();
        var app_path = $("#app_Path").val();
        window.open(app_path + '/Common/ViewDocument.aspx?pono=' + po_val + '&type=PO', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no,directories=no');

    }
    catch (exception) {

    }
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
        Pending_Payments.GetFileNames(index, OnSuccess1);
    }
    catch (exception) {

    }
}

function OnSuccess1(response) {
    var app_path = $("#app_Path").val();
    window.open(app_path + '/Common/FileDownload.aspx?indentno=NA&filename=' + response.value + '&filetag=', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
}

function viewVendor(vcode) {
    try {
        var venCode = $("#venCode" + vcode).val();
        Pending_Payments.viewVendor(venCode, OnVendor);
    }
    catch (exception) {

    }
}

function OnVendor(response) {
    document.getElementById("div_vendor").innerHTML = response.value;
}