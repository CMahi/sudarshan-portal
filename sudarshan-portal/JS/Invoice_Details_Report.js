
function setSelectedNote(req_note) {
    try {
        Invoice_Details_Report.GetCurrentTime(req_note, OnSuccess);
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
        var po_val = $("#encrypt_po" + (index)).val();
        var app_path = $("#app_Path").val();
        window.open(app_path + '/Common/ViewDocument.aspx?pono=' + po_val + '&type=PO', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no,directories=no');

    }
    catch (exception) {

    }
}

function downloadfiles(index) {
    try {
        Invoice_Details_Report.GetFileNames(index, OnSuccess1);
    }
    catch (exception) {

    }
}

function OnSuccess1(response) {
    var app_path = $("#app_Path").val();
    var unique_no = $("#txt_Unique_Number").val();
    window.open(app_path + '/Common/FileDownload.aspx?indentno='+ unique_no + '&filename=' + response.value + '&filetag=', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
}

function gotopage(objButton, str) {
    try {
        var pgno = objButton.value;
        var status = $("#ddlStatus").val();
        var strData = $("#txt_Search").val();
        if (strData == undefined || strData == null) {
            strData = "";
        }
        Invoice_Details_Report.fillGoToPage1(strData, pgno, str,status, callback_Inco);
    }
    catch (exception) {

    }
}

function callback_Inco(response) {
    document.getElementById("div_reportDetails").innerHTML = response.value;
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
    var status = $("#ddlStatus").val();

    Invoice_Details_Report.fillSearch(str, rpp, status, callback_Inco);
}

function change_Image(id, vendor_Code, PO, status)
{
    var apath = $("#app_Path").val();
    var auth = $("#app_Authority").val();
    if ($("#pk_img" + id + "").val() == "1") {
        $("#pk_img" + id + "").val("0");
        $("#img" + id).attr('src', '../../Img/plus.png');
        $("#Expand" + id).hide();
    }
    else {
        $("#pk_img" + id + "").val("1");
        $("#img" + id).attr('src', '../../Img/minus.png');
        $("#Expand" + id).show();
        Invoice_Details_Report.fillDetail(id, vendor_Code, PO, status, callback_detail);
    }

}

function callback_detail(response) {
    var splitResult = response.value.split("*");
    var res = splitResult[0];
    var res1 = splitResult[1];
    document.getElementById("Expand" + res1).innerHTML = res;
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

function submitvalidation() {

    var From = $("#txt_form_Date").val();
    var To = $("#text_To_Date").val();
    if (From == "" && To == "") {
        alert("Please select date filter...!");
        return false;
    }
    else if (From == "" && To != "") {
        alert("Please Select From Date...!");
        return false;
    }
    else if (From != "" && To == "") {
        alert("Please Select To Date...!");
        return false;
    }
}

function Clear() {
    $("#txt_form_Date").val('');
    $("#text_To_Date").val('');
 }

