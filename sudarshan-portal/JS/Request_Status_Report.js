

function searchData() {
    var str = document.getElementById("txt_Search").value;
    var rpp = "0";

    if ($("#ddlRecords option:selected").text() != undefined) {
        rpp = $("#ddlRecords :selected").text();
        $("#ddlText1").val($("#ddlRecords option:selected").text());
    }
    else {
        rpp = $("#ddlText1").val();
    }

    Request_Status_Report.fillSearch(str, rpp, callback_Inco);
}

function callback_Inco(response) {
    document.getElementById("div_InvoiceDetails").innerHTML = response.value;
}

function downloadfiles(index) {
    var app_path = $("#app_Path").val();
    window.open(app_path + '/Common/FileDownload.aspx?indentno=NA&filename=' + index + '&filetag=', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
}

/*====================================================================================================================================*/
function Bind_Documents(req_no, process_id) {
    Request_Status_Report.fillDocumentDetails(req_no, process_id, callback_Bind);
}

function callback_Bind(response) {
    // $("#DivDocs").html(response.value);
    document.getElementById("DivDocs").innerHTML = response.value;
}


function gotopage1(objButton, str) {
    try {
        check_Controls();
        var pgno = objButton.value;
      
        $("ddlText1").val(pgno);
        var strData = $("#txt_Search").val();
        var status = $("#ddlStatus option:selected").val();
        var f_date = $("#txt_f_date").val();
        var t_date = $("#txt_t_date").val();
        Request_Status_Report.get_BulkRequests(strData, pgno, str, status, f_date, t_date, OngetData);
    }
    catch (exception) {

    }
}

function OngetData(response)
{
    $("#div_InvoiceDetails").html(response.value);
}
function get_data() {
    check_Controls();
    var search_data = $("#txt_Search").val();
    var ddl = $("#ddlRecords option:selected").val();
    var pgno = $("ddlText1").val();
    if (pgno == undefined)
    {
        pgno = 1;
    }
    var status = $("#ddlStatus option:selected").val();
    var f_date = $("#txt_f_date").val();
    var t_date = $("#txt_t_date").val();
    Request_Status_Report.get_BulkRequests(search_data, pgno, ddl,status,f_date,t_date, OngetData);
}
$(document).ready(function () {
    $(".datepicker-rtl").datepicker({
        format: 'dd-M-yyyy', autoclose: true, todayBtn: 'linked'
    })
    //get_data();

});
function check_Controls() {
    if ($("#txt_f_date").val() == "") {
        alert("Please Select From Date");
        return false;
    }
    else if ($("#txt_t_date").val() == "") {
        alert("Please Select To Date");
        return false;
    }
  
    var fdate = new Date($("#txt_f_date").val());
    var tdate = new Date($("#txt_t_date").val());
    if (fdate > tdate) {
        alert("Invalid Date Selection");
        return false;
    }
    return true;

}