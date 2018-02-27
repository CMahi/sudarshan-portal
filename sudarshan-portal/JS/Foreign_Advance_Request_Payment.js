g_serverpath = '/Sudarshan-Portal';
var details = "";
var currency_data = "";

$(document).ready(function () {
    if ($("#base_currency").html() == "INR") {
        $("#div_other").hide();
        $("#div_dtl").hide();
        $("#div_inr").show();
    }
    else {
        $("#div_other").show();
        $("#div_dtl").show();
        $("#div_inr").hide();
    }
});


$('#vendor_bill').datepicker({ format: 'dd-M-yyyy', autoclose: true, todayBtn: 'linked', endDate: new Date() }).on('changeDate', function (ev) {

});

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;

    return true;
}

function validateFloatKey(el) {
    var v = parseFloat(el.value);
    el.value = (isNaN(v)) ? '' : v.toFixed(3);
}

function calculate_Total() {
    var lastRow = $('#tbl_Data tr').length;
    var lastRow1 = $('#tbl_Data tr').length - 1;
    var tot_amount = 0;
    var new_exc, new_tot;
    new_exc = new_tot = 0;
    var new_base = $("#base_currency").html();
    var btm_curr_total, btm_exc_total, btm_base_total, btm_inr_total, btm_serv_total, btm_total;
    btm_curr_total = btm_exc_total = btm_base_total = btm_inr_total = btm_serv_total = btm_total = 0;
    for (var q = 0; q < lastRow - 2; q++) {

        var row_rate = $("#exc_rate" + (q + 1)).val();
        if (isNaN(row_rate) || row_rate == "") {
            row_rate = 0;
            //$("#exc_rate" + (q + 1)).val(0);
        }
        //if ($("#span_cur_name" + (q + 1)).html() == new_base)
        //{
        //    new_tot = parseFloat(new_tot) + parseFloat(row_rate);
        //    new_exc = parseFloat(new_tot) / parseFloat(q + 1);
        //    $("#txt_inr_rate").val(parseFloat(new_exc).toFixed(3));
        //}

        var row_total = 0;
        var forex = $("#spn_amount" + (q + 1)).html();
        
        var spn_Exc_Rate = $("#txt_inr_rate").val().trim();
        var ser_charge = $("#ser_charge" + (q + 1)).val().trim();
        if (ser_charge == "")
        {
            ser_charge = 0;
            //$("#ser_charge" + (q + 1)).val(0);
        }
        
        if (isNaN(spn_Exc_Rate) || spn_Exc_Rate=="") {
            spn_Exc_Rate = 0;
        }
        var tot = parseFloat(forex);
        var eq_curr = 0;
        if (parseFloat(tot) > 0 && parseFloat(row_rate) > 0 && parseFloat(spn_Exc_Rate) > 0) {
            eq_curr = Math.round(parseFloat(tot) * (parseFloat(row_rate) / parseFloat(spn_Exc_Rate)));
        }

        $("#eq_base_currency" + (q + 1)).html(eq_curr);
        var inr_amount = Math.round(parseFloat(eq_curr) * parseFloat(spn_Exc_Rate));
        $("#eq_inr_amount" + (q + 1)).html(inr_amount);
        row_total = inr_amount + parseInt(ser_charge);
        $("#total_Line" + (q + 1)).html(row_total);
        tot_amount = tot_amount + row_total;

        btm_curr_total = parseFloat(btm_curr_total) + parseFloat(forex);
        if (forex != undefined && forex != "" && forex!=0) {
            btm_exc_total = parseFloat(btm_exc_total) + parseFloat(row_rate);
        }
        btm_base_total = parseFloat(btm_base_total) + parseFloat(eq_curr);
        btm_inr_total = parseFloat(btm_inr_total) + parseFloat(inr_amount);
        btm_serv_total = parseFloat(btm_serv_total) + parseFloat(ser_charge);
    }
    $("#btm_curr_total").html(btm_curr_total.toFixed(3));
    $("#btm_exc_total").html(btm_exc_total.toFixed(3));
    $("#btm_base_total").html(btm_base_total.toFixed(3));
    $("#bottom_amount").val(btm_base_total);
    $("#btm_inr_total").html(btm_inr_total.toFixed(3));
    $("#btm_serv_total").html(btm_serv_total.toFixed(3));
    $("#btm_total").html(tot_amount.toFixed(3));

}

function calculate_Total_INR() {
    var lastRow = $('#tbl_Data tr').length;
    var lastRow1 = $('#tbl_Data tr').length - 1;
    var tot_amount = 0;
    var new_exc, new_tot,counter;
    new_exc = new_tot = counter = 0;
    var new_base = $("#base_currency").html();
    var btm_curr_total, btm_exc_total, btm_base_total, btm_inr_total, btm_serv_total, btm_total;
    btm_curr_total = btm_exc_total = btm_base_total = btm_inr_total = btm_serv_total = btm_total = 0;
    for (var q = 0; q < lastRow - 2; q++) {

        var row_rate = $("#exc_rate" + (q + 1)).val();
        if (isNaN(row_rate) || row_rate == "") {
            row_rate = 0;
            //$("#exc_rate" + (q + 1)).val(0);
        }
        var spn_amount = $("#spn_amount" + (q + 1)).html();
        if (spn_amount == undefined || spn_amount=="")
        {
            spn_amount = 0;
        }
        if ($("#span_cur_name" + (q + 1)).html() == new_base && spn_amount!=0) {
	        counter=counter+1;
            new_tot = parseFloat(new_tot) + parseFloat(row_rate);
            new_exc = parseFloat(new_tot) / parseFloat(counter);
            $("#txt_inr_rate").val(parseFloat(new_exc));
        }
    }
	calculate_Total();
}

function prepareData()
{
    var act = $("#ddlAction option:selected").index();
    if (act < 1) {
        alert("Please Select Action first...!");
        return false;
    }
    var remrk = document.getElementById("txtRemark").value;
    if (act > 1) {
        if (remrk == "") {
            alert("Please Enter Remark...!");
            return false;
        }
	else
        {
		 $("#tbl_adv_inr").html(0);
            $("#tbl_Ser").html(0);
            $("#tbl_Total").html(0);
            $("#div_Popup").modal('show');
        }
    }
    if (act == 1) {
        if ($("#base_currency").html() == "INR") {
            if ($("#ddlApprover option:selected").index() < 1) {
                alert('Please Select Approver Person');
                return false;
            }
            $("#txt_xml_data_vehicle").val("")
            $("#acc_approver").val($("#ddlApprover").val());
            $("#txt_inp_rate").val(1);
            $("#txt_release").val(1);
            var curr_tot = parseInt($("#btm_curr_total").html());
            var ser_tot = parseInt($("#btm_serv_total").html());
            var tot_am = parseInt(curr_tot) + parseInt(ser_tot);
            $("#tbl_adv_inr").html(curr_tot);
            $("#tbl_Ser").html(ser_tot);
            $("#tbl_Total").html(tot_am);
            $("#div_Popup").modal('show');
        }
        else {

            if ($("#ddlFin_Year option:selected").index() < 1) {
                alert('Please Select Financial Year');
                return false;
            }
            if ($("#ddlVendor option:selected").index() < 1) {
                alert('Please Select Vendor');
                return false;
            }
            if ($("#vendor_no").val() == "") {
                alert('Please Enter Vendor Bill No');
                return false;
            }
            if ($("#vendor_bill").val() == "") {
                alert('Please Enter Vendor Bill Date');
                return false;
            }

            if ($("#payment_method").val() == "") {
                alert('Please Enter payment Method');
                return false;
            }
            if ($("#tax_code").val() == "") {
                alert('Please Enter Tax Code');
                return false;
            }
            if ($("#txt_inr_rate").val() == "") {
                $("#txt_inr_rate").val(0);
                alert('Please Enter Exchange Rate');
                return false;
            }
            else {
                if (isNaN($("#txt_inr_rate").val()) || $("#txt_inr_rate").val() <= 0) {
                    alert('Please Enter Valid Exchange Rate');
                    return false;
                }
            }

            $("#inr_amount").html(parseInt(btm_inr_total));
            $("#serv_amount").html(parseInt(btm_serv_total));

            var fin_year = $("#ddlFin_Year option:selected").text();
            var vendorcode = $("#ddlVendor").val();
            var bill_no = $("#vendor_no").val();
            var dup = Foreign_Advance_Request_Payment.GetDuplicate(fin_year, vendorcode, bill_no);
            if (parseInt(dup.value) > 0) {
                alert('Vendor Bill No. Allready Exists.');
                $("#vendor_no").val("");
                $("#vendor_no").focus();
                return false;
            }

            var lastRow = $('#tbl_Data tr').length;
            var lastRow1 = $('#tbl_Data tr').length - 1;

            var pk_id = $("#pk_id").val();
            var xmlother = "<ROWSET>";
            try {
                for (var q = 0; q < lastRow - 2; q++) {
                    var fk_currency = $("#fk_currency" + (q + 1)).html();
                    var curr_mode = $("#currency_mode" + (q + 1)).html();
                    var base_currency_rate = $("#base_currency_rate").html();
                    if (base_currency_rate == "" || isNaN(base_currency_rate)) {
                        base_currency_rate = 0;
                    }

                    var forex = $("#spn_amount" + (q + 1)).html();
                    var exc_in_inr = $("#exc_rate" + (q + 1)).val();
                    var eq_base_currency = $("#eq_base_currency" + (q + 1)).html();
                    var eq_inr_amount = $("#eq_inr_amount" + (q + 1)).html();
                    var ser_charge = $("#ser_charge" + (q + 1)).val();
                    var total_Line = $("#total_Line" + (q + 1)).html();
                    if (exc_in_inr == "") {
                        exc_in_inr = 0;
                    }
                    if (ser_charge == "") {
                        ser_charge = 0;
                    }
                    xmlother += "<ROW>";
                    xmlother += "<FK_FTA_HDR_ID>" + pk_id + "</FK_FTA_HDR_ID>";
                    xmlother += "<FK_CURRENCY>" + fk_currency + "</FK_CURRENCY>";
                    xmlother += "<CURRENCY_MODE>" + curr_mode + "</CURRENCY_MODE>";
                    xmlother += "<CURRENCY_AMOUNT>" + forex + "</CURRENCY_AMOUNT>";
                    xmlother += "<EXC_RATE_IN_INR>" + exc_in_inr + "</EXC_RATE_IN_INR>";
                    xmlother += "<EQ_BASE_AMOUNT>" + eq_base_currency + "</EQ_BASE_AMOUNT>";
                    xmlother += "<EQ_INR_AMOUNT>" + eq_inr_amount + "</EQ_INR_AMOUNT>";
                    xmlother += "<SERV_CHARGES>" + ser_charge + "</SERV_CHARGES>";
                    xmlother += "<TOTAL_AMOUNT>" + total_Line + "</TOTAL_AMOUNT>";
                    xmlother += "</ROW>";
                }
                xmlother += "</ROWSET>";
                var total_inr = $("#btm_inr_total").html();
                var total_ser = $("#btm_serv_total").html();
                $("#inr_amount").val(total_inr);
                $("#serv_amount").val(total_ser);
                $("#txt_xml_data_vehicle").val(xmlother);
                $("#acc_approver").val($("#txt_Username").val());
                $("#txt_inp_rate").val($("#txt_inr_rate").val());
		$("#txt_pay_mode_sap").val($("#payment_method").val());
                if (xmlother == "<ROWSET></ROWSET>" || xmlother == "") {
                    alert('Rows Not Found');
                    return false;
                }
                var curr_tot = parseInt($("#btm_inr_total").html());
                var ser_tot = parseInt($("#btm_serv_total").html());
                var tot_am = parseInt(curr_tot) + parseInt(ser_tot);
                $("#tbl_adv_inr").html(curr_tot);
                $("#tbl_Ser").html(ser_tot);
                $("#tbl_Total").html(tot_am);
                $("#div_Popup").modal('show');
             
            }
            catch (ex) {
                //alert(ex);
                return false;
            }
        }
        
    }
}

function check_action()
{
    $("#divIns").show();
    return true;
}
