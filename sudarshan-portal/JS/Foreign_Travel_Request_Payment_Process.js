
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


function validateFloatKey(el)
{
    var v = parseFloat(el.value);
    el.value = (isNaN(v)) ? '' : v.toFixed(2);
}

function allowOnlyNumbers() {
    $(".numbersOnly").on("keypress keyup blur", function (event) {
        //this.value = this.value.replace(/[^0-9\.]/g,'');
        $(this).val($(this).val().replace(/[^0-9]/g, ''));
        if ((event.which != 46  || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
            event.preventDefault();
        }
    });

    $(".numbersOnly1").on("keypress keyup blur", function (event) {
        //this.value = this.value.replace(/[^0-9\.]/g,'');
        $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
        if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
            event.preventDefault();
        }
    });

    $(".numbersOnly2").on("keypress keyup blur", function (event) {
        //this.value = this.value.replace(/[^0-9\.]/g,'');
        $(this).val($(this).val().replace(/[^0-9-\.]/g, ''));
        if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
            event.preventDefault();
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
                Foreign_Travel_Request_Payment_Process.getDataRows(fromDate, toDate, country_id, wiid,desg, callback_DataRows);

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
    $("#return_exc").val($("#txt_ERate").val());
    allowOnlyNumbers();
    $(".datepicker-dropdown").datepicker({ format: 'dd-M-yyyy', autoclose: true, todayBtn: 'linked' });
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
    var req_no = $("#spn_req_no").text();
    window.open(app_path + '/Common/FileDownload.aspx?indentno='+req_no+'&filename=' + index + '&filetag=', 'Download', 

'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
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
                    if (calculate_amount() == true) {
                        //return true;
                        $("#div_Popup").modal('show');
                        
                    }
                    else {
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
$("#div_Popup").modal('show');
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
    $("#pay_ref").text(Math.round((parseFloat(balance) - parseFloat(ret_money)).toFixed(2) * (-1)));
    $("#span_diff").text(Math.round(parseFloat(balance) - (parseFloat($("#return_money").val()) - parseFloat($("#pay_ref").text())).toFixed(2)));
    calculate_inr();
}

function calculate_inr()
{
    var ret_money = $("#return_money").val();
    var return_exc = $("#return_exc").val();
    if (isNaN(ret_money) || ret_money == undefined || ret_money == 0)
    {
        ret_money = 0;
    }
    if (isNaN(return_exc) || return_exc == undefined || return_exc == 0) {
        return_exc = 0;
    }
    $("#ret_exc_inr").html(Math.round(parseFloat(parseFloat(ret_money) * (parseFloat(return_exc)))));
}

function calculate_amount()
{
    if ($("#span_diff").html() == 0) {
        if ($("#return_money").val() != "" && $("#return_money").val() != undefined && !isNaN($("#return_money").val())) {
            if ($("#return_exc").val() != "" && $("#return_exc").val() != undefined && !isNaN($("#return_exc").val())) {
                if ($("#return_exc").val() > 0) {
                    var adv_amount = $("#adv_amount").text();
                    var Tot_Amount = $("#Tot_Amount").text();
                    var exc_rate = $("#exc_rate").text();
                    var balance = $("#balance").text();
                    var return_money = $("#return_money").val();
                    var return_exc = $("#return_exc").val(); 
                    var adv_in_inr = $("#advance_in_inr").html();
                    var return_in_inr = return_money * return_exc;
                    var exp_in_inr = adv_in_inr - return_in_inr;

                    $("#Tot_Amount1").val(Tot_Amount);
                    $("#adv_amount1").val(adv_amount);
                    $("#exc_rate1").val(exc_rate);
                    $("#balance1").val(balance);
                    $("#return_money1").val(return_money);
                    $("#return_exc1").val(return_exc);
                    $("#adv_in_inr1").val(adv_in_inr);
                    $("#exp_in_inr1").val(exp_in_inr);
                    $("#return_in_inr1").val(return_in_inr);
                    $("#pay_ref1").val($("#pay_ref").text());
                    $("#doc_no1").val($("#doc_no").val());
                    $("#doc_date1").val($("#doc_date").val());
                    $("#expense_amount1").val($("#expense_amount").val());

                    $("#tbl_adv").html(adv_in_inr);
                    $("#tbl_diff").html(Math.round(parseFloat(parseFloat(return_money) * (parseFloat(return_exc)))));
                    //$("#tbl_exp").html($("#expense_amount").val());
                    $("#tbl_exp").html(Math.round(parseFloat(parseFloat($("#tbl_adv").html()) - (parseFloat($("#tbl_diff").html())))));

                    $("#txt_Advance").val($("#tbl_adv").html());
                    $("#txt_ReturnMoney").val($("#tbl_diff").html());
                    $("#txt_Expense").val($("#tbl_exp").html());
                     return true;
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
    }
    else {
        alert("Difference Should Be always Zero");
        return false;
    }
}

function PopupData()
{
$("#divIns").show();
    return true;
}

function show_record() {
    var Desig = $("#span_grade").text();
    Foreign_Travel_Request_Payment_Process.show_Policy(Desig, getPolicySucceeded)
}

function getPolicySucceeded(response) {
    $("#div_header").html(response.value);
    $('#data-table1').dataTable();
}