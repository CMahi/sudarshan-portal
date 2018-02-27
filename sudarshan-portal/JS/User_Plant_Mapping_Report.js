
function setSelectedNote(req_note) {
    try {
        User_Plant_Mapping_Report.GetCurrentTime(req_note, OnSuccess);
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

function viewData(index) {
    try {
        var po_val = "";
        po_val=$("#encrypt_po" + index).val();
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
        User_Plant_Mapping_Report.GetFileNames(index, OnSuccess1);
    }
    catch (exception) {

    }
}

function OnSuccess1(response) {
    var app_path = $("#app_Path").val();
    var unique_no = $("#txt_Unique_Number").val();
    window.open(app_path + '/Common/FileDownload.aspx?indentno='+ unique_no + '&filename=' + response.value + '&filetag=', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
}

function viewVendor(vcode) {
    try {
        var venCode = $("#venCode"+vcode).val();
        User_Plant_Mapping_Report.viewVendor(venCode, OnVendor);
    }
    catch (exception) {

    }
}

function OnVendor(response) {
    document.getElementById("div_vendor").innerHTML = response.value;
}

function Dispatch_Detail() {
    var Vendor = $("#ddl_Vendor").val();
    var Plant = $("#ddl_Plant").val();
    var PO = $("#ddl_PO").val();
    var From = $("#txt_form_Date").val();
    var To = $("#text_To_Date").val();
    if (Vendor == "" && Plant == "" && PO == "" && (From == "" && To == ""))
    {
        alert("Please select atleast one filter...!");
        return false;
    }
    else if (Vendor != "" && Plant != "" && PO != "" && (From != "" && To == "")) {
        alert("Please Select To Date...!");
        return false;
    }
    else if (Vendor != "" && Plant != "" && PO != "" && (From == "" && To != "")) {
        alert("Please Select From Date...!");
        return false;
    }
    else if (Vendor != "" && Plant != "" && (From != "" && To == "")) {
        alert("Please Select To Date...!");
        return false;
    }
    else if (Vendor != "" && Plant != "" && (From == "" && To != "")) {
        alert("Please Select From Date...!");
        return false;
    }
    else if (Vendor != "" && PO != "" && (From == "" && To != "")) {
        alert("Please Select From Date...!");
        return false;
    }
    else if (Vendor != "" && PO != "" && (From != "" && To == "")) {
        alert("Please Select To Date...!");
        return false;
    }


    else if (Plant != "" && PO != "" && (From == "" && To != "")) {
        alert("Please Select From Date...!");
        return false;
    }
    else if (Plant != "" && PO != "" && (From != "" && To == "")) {
        alert("Please Select To Date...!");
        return false;
    }
    else if (Vendor != "" && (From == "" && To != "")) {
        alert("Please Select From Date...!");
        return false;
    }
    else if (Vendor != "" && (From != "" && To == "")) {
        alert("Please Select To Date...!");
        return false;
    }
    else if (Plant != "" && (From == "" && To != "")) {
        alert("Please Select From Date...!");
        return false;
    }
    else if (Plant != "" && (From != "" && To == "")) {
        alert("Please Select To Date...!");
        return false;
    }
    else if (PO != "" && (From != "" && To == "")) {
        alert("Please Select To Date...!");
        return false;
    }
    else if (PO != "" && (From == "" && To != "")) {
        alert("Please Select From Date...!");
        return false;
    }
    else if(From == "" && To != "")
    {
        alert("Please Select From Date...!");
        return false;
    }
    else if (From != "" && To == "") {
        alert("Please Select To Date...!");
        return false;
    }
$("#divIns").show();
}

function callback_POdetail(response) {
    document.getElementById("div_ReportDetails").innerHTML = response.value;
}

function Clear() {
    $("#ddl_Vendor").val('');
    $("#ddl_Plant").val('');
    $("#ddl_PO").val('');
    $("#txt_form_Date").val('');
    $("#text_To_Date").val('');
}