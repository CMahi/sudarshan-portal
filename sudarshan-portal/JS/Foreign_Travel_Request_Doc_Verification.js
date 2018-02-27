
$(document).ready(function () {
    // allowOnlyNumbers();
    var fromDate = $("#travelFromDate").html();
    var toDate = $("#travelToDate").html();
    getSelectDate(fromDate, toDate);

});

$('#travelFromDate').datepicker({ format: 'dd-M-yyyy', autoclose: true, todayBtn: 'linked', endDate: new Date() }).on('changeDate', function (ev) {
    chk_FromDate();
});

$('#travelToDate').datepicker({ format: 'dd-M-yyyy', autoclose: true, todayBtn: 'linked', endDate: new Date() }).on('changeDate', function (ev) {
    chk_ToDate();
});

function allowOnlyNumbers() {
    $('.numbersOnly').keydown(function (e) {
        if (e.shiftKey || e.ctrlKey || e.altKey) {
            e.preventDefault();
        } else {
            var key = e.keyCode;
            if (!((key == 8) || (key == 46) || (key >= 35 && key <= 40) || (key >= 48 && key <= 57) || (key >= 96 && key <= 105))) {
                e.preventDefault();
            }
        }
    });
}

function check_PreDate(fromDate, toDate) {
    //var chk_dates = Domestic_Travel_Request.check_Dates(fromDate, toDate);
    var chk_dates = "";
    if (chk_dates.value == "1") {
        alert("Expense Already Claimed Between From and To Dates");
        return false;
    }
    else {
        var firstDate = new Date(fromDate);
        var secondDate = new Date(toDate);
        if (firstDate > secondDate) {
            alert("From Date Should Not Be Greater Than To Date...!");
            return false;
        }
    }
}

function chk_FromDate() {
    var fromDate = $("#travelFromDate").val();
    var toDate = $("#travelToDate").val();
    if (fromDate == "" && toDate != "") {
        alert("Please Select Travel From Date");
        $("#travelToDate").val("");
        return false;
    }
    else if (fromDate != "" && toDate == "") {
        $("#travelToDate").val("");
        return false;
    }
    else {
        if (check_PreDate(fromDate, toDate) != false) {
            getSelectDate(fromDate, toDate);
        }
        else {
            $("#travelFromDate").val("");
        }
    }

}

function chk_ToDate() {
    var fromDate = $("#travelFromDate").val();
    var toDate = $("#travelToDate").val();
    if (fromDate == "" && toDate != "") {
        alert("Please Select Travel From Date");
        $("#travelToDate").val("");
        $("#travelToDate").blur();
        $("#travelFromDate").focus();
        return false;
    }
    else if (fromDate != "" && toDate == "") {
        alert("Please Select Travel To Date");
        $("#travelToDate").val("");
        $("#travelFromDate").blur();
        $("#travelToDate").focus();
        return false;
    }
    else {
        if (check_PreDate(fromDate, toDate) != false) {
            getSelectDate(fromDate, toDate);
        }
        else {
            $("#travelToDate").val("");
        }
    }
}

function SelectData() {
    var fromDate = $("#travelFromDate").val();
    var toDate = $("#travelToDate").val();
    getSelectDate(fromDate, toDate);
}

function getSelectDate(fromDate, toDate) {
    try {
        if (fromDate != "" && toDate != "") {
            var fdate = getFormatedDate1(fromDate);
            var tdate = getFormatedDate1(toDate);

            var firstDate = new Date(fdate);
            var secondDate = new Date(tdate);
            if ($("#pk_country_id").val() != 0 && $("#pk_country_id").val() != undefined) {
                var country_id = $("#pk_country_id").val();
                var wiid = $("#txtWIID").val();
                var desg = $("#span_grade").html();
                Foreign_Travel_Request_Doc_Verification.getDataRows(fromDate, toDate, country_id, wiid,desg, callback_DataRows);

            }
            else {
                $("#accordion").html("");
            }
        }
    }
    catch (ex) {

    }
}

function callback_DataRows(response) {
    $("#accordion").html(response.value);
    allowOnlyNumbers();
}

function getFormatedDate(date) {
    var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    return (date.getDate() + "-" + months[date.getMonth()] + "-" + date.getFullYear());
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

function enable_disable() {
    $("#ddlAdv_Location").prop('selectedIndex', 0);
    if ($("#ddl_Payment_Mode option:selected").text().toUpperCase() == "CASH") {
        $("#ddlAdv_Location").removeAttr('disabled');
        $("#lblPay").show();
        $("#ctrlPay").show();
    }
    else {
        $("#ddlAdv_Location").attr('disabled', 'disabled');
        $("#lblPay").hide();
        $("#ctrlPay").hide();
    }
}

function hide_show_remark() {
    $("#txt_Remark").val("");
    if ($("#ddlAction option:selected").index() < 2) {
        $("#div_remark").hide();
    }
    else {
        $("#div_remark").show();
    }
}

function downloadfiles(index) {
    try {
        var app_path = $("#app_Path").val();
        var req_no = $("#spn_req_no").text();
        window.open(app_path + '/Common/FileDownload.aspx?indentno=' + req_no + '&filename=' + index + '&filetag=', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
    }

    catch (Ex) {
        alert(Ex);
    }
}


function prepare_data() {
    try {
        if ($("#empno").html() == "") {
            alert("User Data Not Found. Unable To Proceed Request Further.");
            return false;
        }
        else {
            if ($("#ddlAction option:selected").index() > 0) {
                if ($("#ddlAction option:selected").index() == 1) {
			if($("#chk_original").prop("checked")==false)
			{
			    alert("Original Documents Not Submitted");
                            return false;
			}
                    
			
                }
                else {
                    if ($("#txt_Remark").val() == "") {
                        alert("Please Enter Remark...!");
                        return false;
                    }

                }
            }
            else {
                alert("Please Select Action...!");
                return false;
            }
            return true;
        }
    }
    catch (exception) {
        alert(exception);
        return false;
    }
}


function show_record() {
    var Desig = $("#span_grade").text();
    Foreign_Travel_Request_Doc_Verification.show_Policy(Desig, getPolicySucceeded)
}

function getPolicySucceeded(response) {
    $("#div_header").html(response.value);
    $('#data-table1').dataTable();
}