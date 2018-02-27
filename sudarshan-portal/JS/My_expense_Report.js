
function downloadfiles(req_no,file_name) {
    var app_path = $("#app_Path").val();
    window.open(app_path + '/Common/FileDownload.aspx?indentno='+req_no+'&filename=' + file_name + '&filetag=', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
}

function Bind_Documents(req_no, process_id) {
    My_expense_Report.fillDocumentDetails(req_no, process_id, callback_Bind);
}

function callback_Bind(response) {
    // $("#DivDocs").html(response.value);
    document.getElementById("DivDocs").innerHTML = response.value;
}


/*====================================================================================================================================*/

function gotopage1(objButton, str) {
    try {
        check_Controls();
        var pgno = objButton.value;
        $("ddlText1").val(pgno);
        var strData = $("#txt_Search").val();
        var status = $("#ddlStatus option:selected").val();
        var f_date = $("#txt_f_date").val();
        var t_date = $("#txt_t_date").val();
        My_expense_Report.get_BulkRequests(strData, pgno, str, status,f_date,t_date, OngetData);
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
    My_expense_Report.get_BulkRequests(search_data, pgno, ddl, status,f_date,t_date, OngetData);
}
$(document).ready(function () {
    //get_data();
    $(".datepicker-rtl").datepicker({
        format: 'dd-M-yyyy', autoclose: true, todayBtn: 'linked'
    })
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

function getRequestDetails(index) {
    var pname = $("#pname" + index).val();
    var req_no = $("#h_info" + index).val();

    if (req_no.toLowerCase().includes("ft")) {
        var wiid = $("#wiid" + index).val();
        My_expense_Report.getRequestDetails(wiid, 0, 0,"exp", callback_BindRequestData);
    }
    else if (req_no.toLowerCase().includes("fa")) {
        var iid = $("#iid" + index).val();
        var pid = $("#fk_process" + index).val();
        My_expense_Report.getRequestDetails(0, iid, pid, "adv", callback_BindRequestData);
    }
    else {
        My_expense_Report.getDetails(req_no, pname, callback_BindRequestData);
    }
}


function callback_BindRequestData(response) {
    $("#divReq_Details").html(response.value);
    check_journey_Type1();
}

function check_journey_Type1() {

    var jt = document.getElementsByName("jt");
    var len = jt.length;
    var date_diff1 = getDiffDays($("#travelFromDate").html(), $("#travelToDate").html()) + 1;
    for (var index = 1; index <= len; index++) {
        var journeyType = $("#journey_Type" + index).html().toUpperCase();
        var jt = $("#journey_Type" + index).val();

        $("#div_PlaceRoom" + index).hide();
        $("#div_HotelContact" + index).hide();
        $("#div_City" + index).hide();

        $("#div_Travel_Mode" + index).val(0);
       
        $("#div_Travel_Mode" + index).hide();
        $("#div_FPlant" + index).hide();
        $("#div_TPlant" + index).hide();
        $("#dest_plant" + index).html("Plant From");


        $("#From_City" + index).removeAttr("disabled");
        $("#To_City" + index).removeAttr("disabled");

        if (journeyType == "---SELECT ONE---") {
            $("#div_Travel_Mode" + index).hide();
            $("#div_Travel_Class" + index).hide();
            $("#div_FPlant" + index).hide();
            $("#div_TPlant" + index).hide();


        } else if (journeyType == "OUTSIDE PLANT") {
            $("#div_Travel_Mode" + index).show();
            $("#div_FPlant" + index).hide();
            $("#div_TPlant" + index).hide();
            $("#div_PlaceRoom" + index).show();
            $("#div_HotelContact" + index).show();
            $("#div_City" + index).show();

        } else if (journeyType == "ONE DAY OUTSTATION WITHIN PLANT") {
            $("#div_Travel_Mode" + index).hide();
            $("#div_Travel_Class" + index).hide();
            $("#div_FPlant" + index).show();
            $("#div_TPlant" + index).show();
            $("#From_City" + index).attr("disabled", "disabled");
            $("#To_City" + index).attr("disabled", "disabled");
        } else if (journeyType == "OVERNIGHT STAY WITHIN PLANT") {
            $("#div_Travel_Mode" + index).hide();
            $("#div_Travel_Class" + index).hide();
            $("#div_FPlant" + index).show();
            $("#div_TPlant" + index).hide();
            $("#dest_plant" + index).html("Plant To");
            $("#From_City" + index).attr("disabled", "disabled");
            $("#To_City" + index).attr("disabled", "disabled");
            if ($("#chk_guest_" + index).prop("checked") == true) {
                $("#Boarding_" + index + "_2").prop("disabled", false);
            }
            else {
                $("#Boarding_" + index + "_2").prop("disabled", true);
            }
        }
        else if (journeyType == "ONE DAY OUTSTATION") {
            $("#div_Travel_Mode" + index).show();
            $("#div_FPlant" + index).hide();
            $("#div_TPlant" + index).hide();
            $("#div_City" + index).show();
        }
    }
}

function getDiffDays(fromDate, toDate) {
    if (fromDate != "" && toDate != "") {
        var fdate = getFormatedDate1(fromDate);
        var tdate = getFormatedDate1(toDate);
        var oneDay = 24 * 60 * 60 * 1000;
        var firstDate = new Date(fdate);
        var secondDate = new Date(tdate);
        diffDays = Math.round(Math.abs((firstDate.getTime() - secondDate.getTime()) / (oneDay)));
    }
    return diffDays;
}

function getFormatedDate1(date) {
    var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    var strDate = date.split("-");
    var mn = 0;
    for (var i = 1; i <= months.length; i++) {
        if (strDate[1] == months[i - 1]) {
            mn = i;
        }
    }
    return (mn + "/" + strDate[0] + "/" + strDate[2]);
}

function download_Link(link,req_no)
{
    var result=My_expense_Report.check_avail(req_no);
    if(result.value=='true')
{
    window.open(link, 'Download', 'toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
}
else
{
	alert('Request Is Still Pending For Approval.');
}
}