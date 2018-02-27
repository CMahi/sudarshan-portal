

function downloadfiles(index) {
    var app_path = $("#app_Path").val();
    window.open(app_path + '/Common/FileDownload.aspx?indentno=NA&filename=' + index + '&filetag=', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
}

function Bind_Documents(req_no, process_id) {
    Corporate_Credit_Card_Expense.fillDocumentDetails(req_no, process_id, callback_Bind);
}

function callback_Bind(response) {
    // $("#DivDocs").html(response.value);
    document.getElementById("DivDocs").innerHTML = response.value;
}


/*====================================================================================================================================*/

function gotopage1(objButton, str) {
    try {
        var pgno = objButton.value;
        

        check_Controls();
        var search_data = $("#txt_Search").val();
        var ddl = $("#ddlRecords option:selected").val();
        if (pgno == undefined) {
            pgno = 1;
        }
        var status = $("#ddlStatus option:selected").val();
        var f_date = $("#txt_f_date").val();
        var t_date = $("#txt_t_date").val();
        var reim_type = $("#ddl_Reim_Type option:selected").val();
        Corporate_Credit_Card_Expense.get_BulkRequests(search_data, pgno, ddl, status, f_date, t_date, reim_type, OngetData);
    }
    catch (exception) {

    }
}

function OngetData(response) {
    $("#div_InvoiceDetails").html(response.value);
}
function get_data() {
    check_Controls();
    var search_data = $("#txt_Search").val();
    var ddl = $("#ddlRecords option:selected").val();
    var pgno = $("ddlText1").val();
    if (pgno == undefined) {
        pgno = 1;
    }
    var status = $("#ddlStatus option:selected").val();
    var f_date = $("#txt_f_date").val();
    var t_date = $("#txt_t_date").val();
    var reim_type = $("#ddl_Reim_Type option:selected").val();

    $("#split_data").val($("#ddl_Reim_Type option:selected").text());

    Corporate_Credit_Card_Expense.get_BulkRequests(search_data, pgno, ddl, status, f_date, t_date, reim_type, OngetData);
}
$(document).ready(function () {
   // get_data();
    $(".datepicker-rtl").datepicker({
        format: 'dd-M-yyyy', autoclose: true, todayBtn: 'linked'
    })
});

function check_Controls()
{
    if ($("#txt_f_date").val() == "")
    {
        alert("Please Select From Date");
        return false;
    }
    else if ($("#txt_t_date").val() == "") {
        alert("Please Select To Date");
        return false;
    }
    else if ($("#ddl_Reim_Type option:selected").index() < 1) {
        alert("Please Select Reimbursement Type");
        return false;
    }

    var fdate = new Date($("#txt_f_date").val());
    var tdate = new Date($("#txt_t_date").val());
    if (fdate > tdate)
    {
        alert("Invalid Date Selection");
        return false;
    }
    return true;

}