
function setSelectedNote(req_note) {
    try {
        Service_Dispatch_Note_Report.GetCurrentTime(req_note, OnSuccess);
    }
    catch (exception) {

    }
}
$(document).ready(function () {
    $(".datepicker-dropdown").datepicker({ format: 'dd-M-yyyy', autoclose: true, todayBtn: 'linked' });
});
$('#txt_f_date').datepicker({ format: 'dd-M-yyyy', autoclose: true, todayBtn: 'linked' }).on('changeDate', function (ev) {
    daydiffrence();
});

$('#txt_t_date').datepicker({ format: 'dd-M-yyyy', autoclose: true, todayBtn: 'linked' }).on('changeDate', function (ev) {
    daydiffrence();
});
function daydiffrence() {
    if (document.getElementById('txt_f_date').value == "") {
        return false;
    }
    if (document.getElementById('txt_t_date').value == "") {
        return false;
    }
    var start = $('#txt_f_date').val();
    var stard = start.substring(0, 2);
    var starm = start.substring(3, 6);
    var stary = start.substring(7, 11);

    var end = $('#txt_t_date').val();
    var endd = end.substring(0, 2);
    var endm = end.substring(3, 6);
    var endy = end.substring(7, 11);
    var d1 = new Date(stary, getMonthFromString(starm) - 1, stard);
    var d2 = new Date(endy, getMonthFromString(endm) - 1, endd);
    if (d1 > d2) {
        alert('To Date must be greater than from Date');
        $('#txt_t_date').focus();
        $('#txt_t_date').val('');
        return false;
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
        Service_Dispatch_Note_Report.GetFileNames(index, OnSuccess1);
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
        Service_Dispatch_Note_Report.viewVendor(venCode, OnVendor);
    }
    catch (exception) {

    }
}

function OnVendor(response) {
    document.getElementById("div_vendor").innerHTML = response.value;
}

function check() {
    var fdate = $("#txt_f_date").val();
    var tdate = $("#txt_t_date").val();
    var vendo = $("#ddl_Vendor").val();
    var po = $("#ddl_PO").val();
    var plant = $("#ddl_Plant").val();
    if (tdate != "") {
        if (fdate =="") {
            alert("Please Select From Date");
            return false;
        }
    }
    if (fdate != "") {
        if (tdate == "") {
            alert("Please Select To Date");
            return false;
        }
    }
    if (fdate == "" && tdate == "" && vendo == 0 && po == 0 && plant==0) {
        alert("Please Select Dates or Vendor Name");
        return false;
    }
    $("#divIns").show();
    return true;
}

function callback_POdetail(response) {
    document.getElementById("div_ReportDetails").innerHTML = response.value;
}

function Clear() {
    Service_Dispatch_Note_Report.btnClear_Click(Onclear);
}
function Onclear(response) {
    $("#txt_f_date").val('');
    $("#txt_t_date").val('');   
    document.getElementById("ddl_Vendor").selectedIndex = "0";
    document.getElementById("ddl_PO").selectedIndex = "0";
    document.getElementById("ddl_Plant").selectedIndex = "0";
    document.getElementById("div_ReportDetails").innerHTML = "";
}
//$("#ddl_Vendor").change(function () {
//    var vendor = $("#ddl_Vendor").val();
//    Service_Dispatch_Note_Report.selectedpo(vendor, OnSuccce);
//});


//function OnSuccce(response) {
//    alert("hi");
//    alert(response.value);
//    document.getElementById('ddl_PO').value = response.value;
//    //$("#ddl_PO").innerHTML(response.value);
//    //document.getElementById("div_ReportDetails").innerHTML = response.value;
//}