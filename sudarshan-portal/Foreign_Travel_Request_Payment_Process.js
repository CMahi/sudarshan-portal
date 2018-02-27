
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
                Foreign_Travel_Request_Payment_Process.getDataRows(fromDate, toDate, country_id, wiid, callback_DataRows);

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
   var app_path = $("#app_Path").val();
 var spn_req= $("#spn_req_no").html();
    window.open(app_path + '/Common/FileDownload.aspx?indentno='+spn_req+'&filename=' + $("#uploadTable tr")[index].cells[0].innerText + '&filetag=', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
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
                    calculate_amount();
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

function calculate_diff()
{
    var balance = $("#balance").text();
    var ret_money = $("#return_money").val();
    //var return_exc = $("#return_exc").val();
    if (balance == "" || balance == undefined)
    {
        balance = 0;
    }
    if (ret_money == "" || ret_money == undefined) {
        ret_money = 0;
    }
    $("#span_diff").text(parseFloat(balance) - parseFloat(ret_money));
}

function calculate_amount()
{
    if ($("#return_money").val() != "" && $("#return_money").val() != undefined) {
        if ($("#return_exc").val() != "" && $("#return_exc").val() != undefined) {
            if ($("#return_exc").val() > 0) {
                var adv_amount = $("#adv_amount").text();
                var Tot_Amount = $("#Tot_Amount").text();
                var exc_rate = $("#exc_rate").text();
                var balance = $("#balance").text();
                var return_money = $("#return_money").val();
                var return_exc = $("#return_exc").val();
                var adv_in_inr = adv_amount * exc_rate;
                var return_in_inr = return_money * return_exc;
                var exp_in_inr = adv_in_inr - return_in_inr;

                $("#pk_bank_id").val($("#ddlBank").val());
                $("#Tot_Amount").val(Tot_Amount);
                $("#adv_amount").val(adv_amount);
                $("#exc_rate").val(exc_rate);
                $("#balance").val(balance);
                $("#return_money1").val(return_money);
                $("#return_exc1").val(return_exc);
                $("#adv_in_inr").val(adv_in_inr);
                $("#exp_in_inr").val(exp_in_inr);
                $("#return_in_inr").val(return_in_inr);
            }
            else {
                alert("Return Exchange Rate Should Be Greater Than Zero");
                return false;
            }
        }
        else {
            alert("Please Enter Return Exchange Rate");
            return false;
        }
    }
    else {
        alert("Please Enter Return Amount");
        return false;
    }
    return true;
}