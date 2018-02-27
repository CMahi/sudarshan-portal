
var flag = true;

$(document).ready(function () {
    var mode = $("#ddl_Payment_Mode").html();
    var adva = $("#span_advance").html();
    if (adva == "Other Expense Advance") {
        $("#divpol").hide();
    }
    else {
        $("#divpol").show();
    }
    if (mode == "BANK") {
        $("#div_payment").hide();
    }
    else {
        $("#div_payment").show();
    }
    if (adva == "Foreign Travel Advance") {

        $("#div_payment").hide();
        $("#div_payment_mode").hide();
        document.getElementById('div_payment_Dtl').style.display = "none";
    }


    var currdate = new Date();
    var backdate = new Date(currdate.getFullYear(), currdate.getMonth(), currdate.getDate() - 60);
    var tempback = new Date(backdate.getFullYear(), backdate.getMonth(), '01');
    var tempcurmonth = currdate.getMonth() + 1;
    var tempcuryear = currdate.getFullYear();
    var tempdaysincurrmon = numberOfDays(tempcuryear, tempcurmonth);
    var temend = new Date(currdate.getFullYear(), currdate.getMonth(), currdate.getDate());

    function numberOfDays(tempcuryear, tempcurmonth) {
        var d = new Date(tempcuryear, tempcurmonth, 0);
        return d.getDate();
    }

    $("#txt_vendor_date").datepicker({ format: 'dd-M-yyyy', autoclose: true, todayBtn: 'linked', endDate: temend });

});

function prepareData123() {
    var ddlaction = $("#ddlAction").val();
    var processid = $("#txtProcessID").val();
    var instanceid = $("#txtInstanceID").val();
    var username = $("#txt_Username").val();
    var txtremark = $("#txtRemark").val();
    var xmlother = "<ROWSET></ROWSET>";
    var requestno = $("#txt_Request").val();
    var txtwiid = $("#txtWIID").val();
    var advtype = $("#txt_adv_type").val();
    var Init_Email = $("#Init_Email").val();
    var txt_Initiator = $("#txt_Initiator").val();
    var txtaudit = $("#txt_Audit").val();


    var act = document.getElementById("ddlAction").value;
    if (act == "") {
        alert("Please Select Action first...!");
        return false;
    }
    var remrk = document.getElementById("txtRemark").value;
    if (act != "Approve") {
        if (remrk == "") {
            alert("Please Enter Remark...!");
            return false;
        }


        //     Advance_Request_AcccountApproval.DataSave(ddlaction, processid, instanceid, username, txtremark, xmlother, requestno, txtwiid, advtype, Init_Email, txt_Initiator, callBack_Request);

    }


    if ($("#txt_adv_type").val() == "2") {

        if (act == "Approve") {

            var tbl = document.getElementById("tbl_vendor").value
            var txtexchgrate = $("#txt_Exchng_Rate").val();
            var txtamtrate = $("#txt_Amt_INR").val();
            var finyear = $("#ddl_fin_year").val();
            var vendorcode = $("#ddl_vendor_code").val();
            var vendorbilno = $("#txt_vendor_bill_no").val();
            var vendordate = $("#txt_vendor_date").val();
            var fkhdrid = $("#txt_pk_id").val();
            var pkdtlid = $("#txt_pk_dtl_id").val();
            var paymentmethod = $("#txt_payment_method").val();
            var taxcode = $("#txt_tax_code").val();
            var servcharge = $("#txt_service_charges").val();


            if (txtexchgrate == "" || txtexchgrate == "0") {
                alert('Please Enter Exchange Rate');
                return false;
            }
            if (finyear == "0" || finyear == "") {
                alert('Please Select Financial Year');
                return false;
            }
            if (vendorcode == "0" || vendorcode == "") {
                alert('Please Select Vendor Code');
                return false;
            }
            if (vendorbilno == "") {

                alert('Please Enter Vendor Bill No.');
                return false;

            }

            if (vendordate == "") {
                alert('Please Select Vendor Date');
                return false;
            }
            if (paymentmethod == "") {
                alert('Please Enter Payment Method');
                return false;
            }
            if (taxcode == ""  ) {
                alert('Please Enter Taxcode');
                return false;
            }
            if (servcharge == "") {
                alert('Please Enter Service Charges');
                return false;
            }
            $("#txt_fin_year").val(finyear);
            $("#txt_vendor_code").val(vendorcode);
            $("#txt_vendor_bill_no_add").val(vendorbilno);
            $("#txt_exchange_Rate").val(txtexchgrate);
            $("#txt_amount_inr").val(txtamtrate);
            $("#txt_bill_date").val(vendordate);
            $("#txt_payment_method_1").val(paymentmethod);
            $("#txt_tax_code_1").val(taxcode);
            $("#txt_service_charges_1").val(servcharge);

            Advance_Request_AcccountApproval.GetDuplicate(finyear, vendorbilno, vendorcode, callBack_Duplicate);

        }
        else {
            Advance_Request_AcccountApproval.DataSave(ddlaction, processid, instanceid, username, txtremark, xmlother, requestno, txtwiid, advtype, Init_Email, txt_Initiator, callBack_Request);
        }
    }
    else {
        Advance_Request_AcccountApproval.DataSave(ddlaction, processid, instanceid, username, txtremark, xmlother, requestno, txtwiid, advtype, Init_Email, txt_Initiator, callBack_Request);
    }

    //Advance_Request_AcccountApproval.DataSave(ddlaction, processid, instanceid, username, txtremark, xmlother, requestno, txtwiid, advtype, Init_Email, txt_Initiator, callBack_Request);

}

function allowOnlyNumbers() {
    $(".numbersOnly").on("keypress keyup blur", function (event) {
        $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
        if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
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
}

function validateFloatKey(el) {
    var v = parseFloat(el.value);
    el.value = (isNaN(v)) ? '' : v.toFixed(2);
}

function callBack_Duplicate(response) {
    var resp = response.value;
    if (resp == "Entered Bill No. Already Exist") {
        alert("Bill No. Already Exist");
        $("#txt_vendor_bill_no").val('');
        var txtfinyear = $("#txt_fin_year").val();
        return false;

  
    }
    else {
        var txtexchgrate = $("#txt_Exchng_Rate").val();
        var txtamtrate = $("#txt_Amt_INR").val();
        var finyear = $("#ddl_fin_year").val();
        var vendorcode = $("#ddl_vendor_code").val();
        var vendorbilno = $("#txt_vendor_bill_no").val();
        var vendordate = $("#txt_vendor_date").val();
        var fkhdrid = $("#txt_pk_id").val();
        var pkdtlid = $("#txt_pk_dtl_id").val();
        var paymentmethod = $("#txt_payment_method").val();
        var taxcode = $("#txt_tax_code").val();
        var servcharge = $("#txt_service_charges").val();

     
        var ddlaction = $("#ddlAction").val();
        var condition = $("#txt_Condition").val(1);
        var processid = $("#txtProcessID").val();
        var instanceid = $("#txtInstanceID").val();
        var txtaudit = $("#txt_Audit").val();
        var txtremark = $("#txtRemark").val();
        var username = $("#txt_Username").val();
        var requestno = $("#txt_Request").val();
        var txtwiid = $("#txtWIID").val();
        var advtype = $("#txt_adv_type").val();
        var Init_Email = $("#Init_Email").val();
        var txt_Initiator = $("#txt_Initiator").val();

        var xmlother = "";
        xmlother = "|ROWSET||";
        xmlother += "|ROW||";
        xmlother += "|PK_ADVANCE_DTL_Id||" + pkdtlid + "|/PK_ADVANCE_DTL_Id||";
        xmlother += "|FK_ADVANCE_HDR_Id||" + fkhdrid + "|/FK_ADVANCE_HDR_Id||";
        xmlother += "|EXCHANGE_RATE||" + txtexchgrate + "|/EXCHANGE_RATE||";
        xmlother += "|AMT_IN_INR||" + txtamtrate + "|/AMT_IN_INR||";
        xmlother += "|FK_VENDOR_CODE||" + vendorcode + "|/FK_VENDOR_CODE||";
        xmlother += "|VENDOR_BILL_NO||" + vendorbilno + "|/VENDOR_BILL_NO||";
        xmlother += "|VENDOR_BILL_DATE||" + vendordate + "|/VENDOR_BILL_DATE||";
        xmlother += "|FIN_YEAR||" + finyear + "|/FIN_YEAR||";
        xmlother += "|PAYMENT_METHOD||" + paymentmethod + "|/PAYMENT_METHOD||";
        xmlother += "|TAX_CODE||" + taxcode + "|/TAX_CODE||";
        xmlother += "|SERVICE_CHARGES||" + servcharge + "|/SERVICE_CHARGES||";
        xmlother += "|/ROW||";

        xmlother += "|/ROWSET||";


        Advance_Request_AcccountApproval.DataSave(ddlaction, processid, instanceid, username, txtremark, xmlother, requestno, txtwiid, advtype, Init_Email, txt_Initiator, callBack_Request);


    }

}

function callBack_Request(response) {
    if (response.value == "Advance Request has been Approved...!") {
        //alert('Advance Request has been Approved...!');
        $(location).attr('href', '../../Portal/SCIL/Home.aspx');

    }
    else if (response.value == "Advance Request has been Rejected...!") {
      //  alert('Advance Request has been Rejected...!', function () { $(location).attr('href', '../../Portal/SCIL/Home.aspx'); });
        
       
       $(location).attr('href', '../../Portal/SCIL/Home.aspx');
    }
    else if (response.value == "Advance Request has been Sent back to Initiator...!") {
        //alert('Advance Request has been Sent back to Initiator...!');
        $(location).attr('href', '../../Portal/SCIL/Home.aspx');

    }
    else if (response.value == "Error") {
       alert('Error While Processing...Please Try Later');
       

    }
}



function hide_show_remark() {
    $("#txt_Remark").val("");
    if ($("#ddlAction option:selected").index() < 2) {
        $("#div_remark").hide();
        $("#lbl_rmk").hide();
    }
    else {
        $("#div_remark").show();
        $("#lbl_rmk").show();
    }
}

g_serverpath = '/Sudarshan-Portal';
function downloadfiles(index) {
    var tbl = document.getElementById("uploadTable");

    window.open('../../Common/FileDownload.aspx?enquiryno=NA&filename=' + tbl.rows[index].cells[2].innerText + '&filetag=', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
}

function checkamount() {
    var curnc = $("#txt_curn_Fc").val();
    var perdayamt = $("#txt_per_day_amt").val();
    var excngrate = $("#txt_Exchng_Rate").val();
    var amtfc = $("#txt_Amt_Fc").val();
    var indamtcurn = Math.round(parseFloat(parseFloat(amtfc) * parseFloat(excngrate)));   

    $("#txt_Amt_INR").val(indamtcurn);
}


function prepareData() {

    var act = document.getElementById("ddlAction").value;
    if (act == "") {
        alert("Please Select Action first...!");
        return false;
    }
    var remrk = document.getElementById("txtRemark").value;
    if (act != "Approve") {
        if (remrk == "") {
            alert("Please Enter Remark...!");
            return false;
        }
    }



        if (act == "Approve") {
            var xmlother = "";
            xmlother = "|ROWSET||";
            var tbl = document.getElementById("tbl_vendor").value
            var txtexchgrate = $("#txt_Exchng_Rate").val();
            var txtamtrate = $("#txt_Amt_INR").val();
            var finyear = $("#ddl_fin_year").val();
            var vendorcode = $("#ddl_vendor_code").val();
            var vendorbilno = $("#txt_vendor_bill_no").val();
            var vendordate = $("#txt_vendor_date").val();
            var fkhdrid = $("#txt_pk_id").val();
            var pkdtlid = $("#txt_pk_dtl_id").val();
            var paymentmethod = $("#txt_payment_method").val();
            var taxcode = $("#txt_tax_code").val();
            var servcharge = $("#txt_service_charges").val();


            if (txtexchgrate == "" || txtexchgrate == "0") {
                alert('Please Enter Exchange Rate');
                return false;
            }
            if (finyear == "0" || finyear == "") {
                alert('Please Select Financial Year');
                return false;
            }
            if (vendorcode == "0" || vendorcode == "") {
                alert('Please Select Vendor Code');
                return false;
            }
            if (vendorbilno == "") {

                alert('Please Enter Vendor Bill No.');
                return false;

            }

            if (vendordate == "") {
                alert('Please Select Vendor Date');
                return false;
            }

            if (paymentmethod == "") {
                alert('Please Enter Payment Method');
                return false;
            }
            if (taxcode == "") {
                alert('Please Enter Taxcode');
                return false;
            }
            if (servcharge == "") {
                alert('Please Enter Service Charges');
                return false;
            }

            $("#txt_fin_year").val(finyear);
            $("#txt_vendor_code").val(vendorcode);
            $("#txt_vendor_bill_no_add").val(vendorbilno);
            $("#txt_exchange_Rate").val(txtexchgrate);
            $("#txt_amount_inr").val(txtamtrate);
            $("#txt_bill_date").val(vendordate);
            $("#txt_payment_method_1").val(paymentmethod);
            $("#txt_tax_code_1").val(taxcode);
            $("#txt_service_charges_1").val(servcharge);
           // checkDuplicate();
           
                xmlother += "|ROW||";
                xmlother += "|PK_ADVNC_FOREIGN_ID||" + pkdtlid + "|/PK_ADVNC_FOREIGN_ID||";
                xmlother += "|FK_ADVANCE_HDR_ID||" + fkhdrid + "|/FK_ADVANCE_HDR_ID||";
                xmlother += "|EXCHANGE_RATE||" + txtexchgrate + "|/EXCHANGE_RATE||";
                xmlother += "|AMT_IN_INR||" + txtamtrate + "|/AMT_IN_INR||";
                xmlother += "|FK_VENDOR_CODE||" + vendorcode + "|/FK_VENDOR_CODE||";
                xmlother += "|VENDOR_BILL_NO||" + vendorbilno + "|/VENDOR_BILL_NO||";
                xmlother += "|VENDOR_BILL_DATE||" + vendordate + "|/VENDOR_BILL_DATE||";
                xmlother += "|FIN_YEAR||" + finyear + "|/FIN_YEAR||";
                xmlother += "|PAYMENT_METHOD||" + paymentmethod + "|/PAYMENT_METHOD||";
                xmlother += "|TAX_CODE||" + taxcode + "|/TAX_CODE||";
                xmlother += "|SERVICE_CHARGES||" + servcharge + "|/SERVICE_CHARGES||";
                xmlother += "|/ROW||";

                xmlother += "|/ROWSET||";
           
        }
$("#divIns").show();
        $("#txt_update_xmldata").val(xmlother);

}

function checkDuplicate() {
    var finyear = $("#ddl_fin_year").val();
    var vendorcode = $("#ddl_vendor_code").val();
    var vendorbilno = $("#txt_vendor_bill_no").val();
    $("#txt_fin_year").val(finyear);
    $("#txt_vendor_code").val(vendorcode);
    $("#txt_vendor_bill_no").val(vendorbilno);

    Advance_Request_AcccountApproval.GetDuplicate(finyear, vendorbilno, vendorcode, callBack_Duplicate);
}

function callBack_Duplicate(response) {
    var resp = response.value;
    if (resp == "Entered Bill No. Already Exist") {
        alert("Entered Vendor Bill No. Already Exist");
        $("#txt_vendor_bill_no").val('');
        flag = false;

    }
    else {
        flag = true;

    }

}

function PopupData()
{

    var act = document.getElementById("ddlAction").value;
    if (act == "") {
        alert("Please Select Action first...!");
        return false;
    }
    var remrk = document.getElementById("txtRemark").value;
    if (act != "Approve") {
        if (remrk == "") {
            alert("Please Enter Remark...!");
            return false;
        }
    }



    if (act == "Approve") {
        var xmlother = "";
        xmlother = "|ROWSET||";
        var tbl = document.getElementById("tbl_vendor").value
        var txtexchgrate = $("#txt_Exchng_Rate").val();
        var txtamtrate = $("#txt_Amt_INR").val();
        var finyear = $("#ddl_fin_year").val();
        var vendorcode = $("#ddl_vendor_code").val();
        var vendorbilno = $("#txt_vendor_bill_no").val();
        var vendordate = $("#txt_vendor_date").val();
        var fkhdrid = $("#txt_pk_id").val();
        var pkdtlid = $("#txt_pk_dtl_id").val();
        var paymentmethod = $("#txt_payment_method").val();
        var taxcode = $("#txt_tax_code").val();
        var servcharge = $("#txt_service_charges").val();


        if (txtexchgrate == "" || txtexchgrate == "0") {
            alert('Please Enter Exchange Rate');
            return false;
        }
        if (finyear == "0" || finyear == "") {
            alert('Please Select Financial Year');
            return false;
        }
        if (vendorcode == "0" || vendorcode == "") {
            alert('Please Select Vendor Code');
            return false;
        }
        if (vendorbilno == "") {

            alert('Please Enter Vendor Bill No.');
            return false;

        }

        if (vendordate == "") {
            alert('Please Select Vendor Date');
            return false;
        }

        if (paymentmethod == "") {
            alert('Please Enter Payment Method');
            return false;
        }
        if (taxcode == "") {
            alert('Please Enter Taxcode');
            return false;
        }
        if (servcharge == "") {
            alert('Please Enter Service Charges');
            return false;
        }

        $("#txt_fin_year").val(finyear);
        $("#txt_vendor_code").val(vendorcode);
        $("#txt_vendor_bill_no_add").val(vendorbilno);
        $("#txt_exchange_Rate").val(txtexchgrate);
        $("#txt_amount_inr").val(txtamtrate);
        $("#txt_bill_date").val(vendordate);
        $("#txt_payment_method_1").val(paymentmethod);
        $("#txt_tax_code_1").val(taxcode);
        $("#txt_service_charges_1").val(servcharge);
        // checkDuplicate();

        /**************************************************************************************************************/
        
        $("#tbl_Currency").html($("#txt_curn_Fc").val());
        $("#tbl_Exc").html(txtexchgrate);
        $("#tbl_adv_curr").html($("#txt_Amt_Fc").val());
        $("#tbl_adv_inr").html(txtamtrate);
        $("#tbl_Ser").html(servcharge);
        $("#tbl_Total").html(parseInt(txtamtrate) + parseInt(servcharge));

        /**************************************************************************************************************/
        xmlother += "|ROW||";
        xmlother += "|PK_ADVNC_FOREIGN_ID||" + pkdtlid + "|/PK_ADVNC_FOREIGN_ID||";
        xmlother += "|FK_ADVANCE_HDR_ID||" + fkhdrid + "|/FK_ADVANCE_HDR_ID||";
        xmlother += "|EXCHANGE_RATE||" + txtexchgrate + "|/EXCHANGE_RATE||";
        xmlother += "|AMT_IN_INR||" + txtamtrate + "|/AMT_IN_INR||";
        xmlother += "|FK_VENDOR_CODE||" + vendorcode + "|/FK_VENDOR_CODE||";
        xmlother += "|VENDOR_BILL_NO||" + vendorbilno + "|/VENDOR_BILL_NO||";
        xmlother += "|VENDOR_BILL_DATE||" + vendordate + "|/VENDOR_BILL_DATE||";
        xmlother += "|FIN_YEAR||" + finyear + "|/FIN_YEAR||";
        xmlother += "|PAYMENT_METHOD||" + paymentmethod + "|/PAYMENT_METHOD||";
        xmlother += "|TAX_CODE||" + taxcode + "|/TAX_CODE||";
        xmlother += "|SERVICE_CHARGES||" + servcharge + "|/SERVICE_CHARGES||";
        xmlother += "|/ROW||";

        xmlother += "|/ROWSET||";

    }
    $("#txt_update_xmldata").val(xmlother);

}