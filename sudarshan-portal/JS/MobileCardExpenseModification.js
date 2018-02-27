g_serverpath = '/Sudarshan-Portal';
var details = "";
function value1billamt(index) {
    try {
        var mbillamt = parseInt($("#txt_mobile_bill_amt" + index).val());
        var reimamt = parseInt($("#txt_total" + index).val());

        if (parseInt(mbillamt) < parseInt(reimamt)) {
            alert('Bill Amount should be greater than Reimbursement Amount at row :' + index + '');
            $("#txt_total" + index).val('');
            return false;
        }
        if ($("#txt_ded_count").val() != "0") {
            var tbl = document.getElementById("tbldedu");
            document.getElementById("txt_check_Nos").value = "";
            document.getElementById("txt_ded_amount").value = 0;
           // $("#span_dedamount").html('');
            document.getElementById("txt_ded_total_amount").value = 0;
            var rowCount = "";
            rowCount = $('#tbldedu tr').length;
            var vals = "";
            var j = 1;
            for (var k = 1; k < rowCount; k++) {
                var checkboxes = document.getElementById("checkbox" + k + "");
                if (checkboxes.checked) {
                    document.getElementById("txt_ded_amount").value += $("#ded_amount" + k).val() + ";";
                    document.getElementById("txt_ded_total_amount").value = parseInt(document.getElementById("txt_ded_total_amount").value) + parseInt($("#ded_amount" + k).val());
                    vals += "," + checkboxes.value;
                    document.getElementById("txt_check_Nos").value = j++;
                }
                
            }
            if (document.getElementById("txt_check_Nos").value == "") {
                alert("Select Deduction Amount for Claim...!");
                $("#txt_total" + index).val('');
                return false;
            }
        }
        else {
            $("#txt_ded_total_amount").val(0);
        }
       
        var total = 0;
        var ded_Amount = 0;
        var index = 0;
      
        if ($("#txt_ded_total_amount").val() != "0" || $("#txt_ded_total_amount").val() != undefined || $("#txt_ded_total_amount").val() != NaN) {
            $("#span_dedamount").html(parseFloat($("#txt_ded_total_amount").val()) + parseFloat($("#txt_ded_tamount").val()))
            ded_Amount = $("#span_dedamount").html();
        }
        else {
            ded_Amount = $("#span_dedamount").html();
        }
        var lastRow = $('#tbl_Mobile tr').length;
 
        for (var q = 0; q < lastRow - 1; q++) {
            var amount = 0;
            index = q + 1;
            if ($("#txt_total" + (q + 1)).val() != "" && $("#txt_total" + (q + 1)).val() != undefined) {
                amount = parseFloat($("#txt_total" + (q + 1)).val());
                $("#txt_total" + (q + 1)).val(amount);
            }
            else {
                $("#txt_total" + (q + 1)).val(0);
            }
            total = total + amount;
        }
        $("#spn_Total").html(total);
        if (parseInt(total) > parseInt(ded_Amount)) {            
            alert("Reimbursement Amount should not be greater than Deduction Amount");
            $("#txt_total" + index).val('');
            return false;
        }
	$("#divIns").show();
        return true;
    }
    catch (ex)
    {
	$("#divIns").hide();
 }
}
function FillServicePro(id) {

    try {
        var str = document.getElementById("txt_serviceprovide").value;
        var ddl = document.getElementById(id);
        var mainarr = new Array();
        mainarr = str.split(";");
        for (var i = 0; i < mainarr.length; i++) {
            var subarr = new Array();
            subarr = mainarr[i].split("~");
            var res = ddl.value;
            var option = document.createElement("option");
            if (res == subarr[0]) {
            }
            else {
                option.value = subarr[0];
                option.text = subarr[1];
                ddl.add(option);
            }
        }
    }
    catch (Ex) {
        alert(Ex.description);
    }

}
function FillYe(id) {

    try {
        var str = document.getElementById("txt_year").value;
        var ddl = document.getElementById(id);
        var mainarr = new Array();
        mainarr = str.split(";");
        for (var i = 0; i < mainarr.length; i++) {
            var subarr = new Array();
            subarr = mainarr[i].split("~");
            var res = ddl.value;
            var option = document.createElement("option");
            if (res == subarr[0]) {
            }
            else {
                option.value = subarr[0];
                option.text = subarr[1];
                ddl.add(option);
            }

        }
    }
    catch (Ex) {
        alert(Ex.description);
    }

}
function FillSupp(id) {

    try {
        var str = document.getElementById("txt_ddlsupport").value;
        var ddl1 = document.getElementById(id);
        var mainarr1 = new Array();
        mainarr1 = str.split(";");
        for (var i = 0; i < mainarr1.length; i++) {
            var subarr = new Array();
            subarr = mainarr1[i].split("~");
            var res1 = ddl1.value;
            var option1 = document.createElement("option");
            if (res1 == subarr[1]) {
            }
            else {
                option1.value = subarr[0];
                option1.text = subarr[1];
                ddl1.add(option1);
            }
        }
    }
    catch (Ex) {
        alert(Ex.description);
    }

}
function FillSuppUser(id) {

    try {
        var str = document.getElementById("txt_ddlsupportuser").value;
        var ddl1 = document.getElementById(id);
        var mainarr1 = new Array();
        mainarr1 = str.split(";");
        for (var i = 0; i < mainarr1.length; i++) {
            var subarr = new Array();
            subarr = mainarr1[i].split("~");
            var res1 = ddl1.value;
            var option1 = document.createElement("option");
            if (res1 == subarr[1]) {
            }
            else {
                option1.value = subarr[0];
                option1.text = subarr[1];
                ddl1.add(option1);
            }
        }
    }
    catch (Ex) {
        alert(Ex.description);
    }

}
function FillMon(id) {

    try {
        var str = document.getElementById("txt_month").value;
        var ddl1 = document.getElementById(id);
        var mainarr1 = new Array();
        mainarr1 = str.split(";");
        for (var i = 0; i < mainarr1.length; i++) {
            var subarr = new Array();
            subarr = mainarr1[i].split("~");
            var res1 = ddl1.value;
            var option1 = document.createElement("option");
            if (res1 == subarr[0]) {
            }
            else {
                option1.value = subarr[0];
                option1.text = subarr[1];
                ddl1.add(option1);
            }
        }
    }
    catch (Ex) {
        alert(Ex.description);
    }

}
function createxml() {
    try {
        var mobile = $("#txt_cmobiole").val();
        var xmlmobile = "";
        var expense = $("#lblExpenseHead").text();
        var pk_id = document.getElementById("txt_pk_id").value;
        var request_no = document.getElementById("txt_Request").value;
        if (expense == "Mobile") {
            if (mobile != "") {
                //if (calculate_Total()) {
                    var TOOTALBILAMT = 0.00;
                    var TotalAmt = parseInt(0);
                    var supp_tot = parseInt(0);
                    var non_supp = parseInt(0);
                    /**************************Company Provided Mobile****************************/
                    var tblmobile = document.getElementsByName("tbl_Mobile");
                    var tbllength = $('#tbl_Mobile tr').length - 1;
                    xmlmobile = "|ROWSET||";
                    for (var i = 1; i <= tbllength; i++) {

                        var mno = $("#lbl_MobileNo").text();
                        var provi = document.getElementById("ddlServiceProvider" + i).value;
                        var year = document.getElementById("ddlYear" + i).value;
                        var mnt = document.getElementById("ddlMonth" + i).value;
                        var mbill = document.getElementById("txt_mobile_bill_no" + i).value;
                        var mbilldate = document.getElementById("txt_mobile_bill_date" + i).value;
                        var mbillamt = document.getElementById("txt_mobile_bill_amt" + i).value;
                       // var dedyear = document.getElementById("ddldedYear").value;
                       // var dedmonth = document.getElementById("ddlDeduMonth").value;
                        //var mdedamt = document.getElementById("txt_deductionamt").value;
                        var mdedamt = $("#txt_ded_tamount").val();
                        var remamt = document.getElementById("txt_total" + i).value;
                        var pay = document.getElementById("ddlPayMode").value
                        var loc = document.getElementById("ddlLocation").value;
                        var mrmk = document.getElementById("txt_Mremark" + i).value;
                        var supp = document.getElementById("ddl_support" + i).value;
                       // var supprmk = document.getElementById("txt_supremark" + i).value;
                        if (supp == "No") {
                            supp = "N";
                        }
                        if (supp == "Yes") {
                            supp = "Y";
                        }
                        var rebuiseamt = parseInt($("#txt_total" + i).val());
                        TotalAmt = TotalAmt + rebuiseamt;

                        if (pay == 0) {
                            alert('Please Select Payment Mode');
                            return false;
                        }
                        if (loc == 0 && pay == 1) {
                            alert('Please Select Payment Location');
                            return false;
                        }
                        //if (mno != "") {
                        //    if (dedyear == 0) {
                        //        alert('Please Select Deduction Year');
                        //        return false;
                        //    }
                        //    if (dedmonth == 0) {
                        //        alert('Please Select Deduction Month');
                        //        return false;
                        //    }
                        //    if (mbillamt == "") {
                        //        alert('Please Enter Deduction Amount');
                        //        return false;
                        //    }
                        //    var dyearss = parseInt($("#ddldedYear").val());
                        //    var yearss = parseInt($("#ddlYear" + i).val());
                        //    if (dyearss < yearss) {
                        //        alert("Year should be less than deduction Year");
                        //        return false;
                        //    }
                        //    var dyears = parseInt($("#ddldedYear").val());
                        //    var years = parseInt($("#ddlYear" + i).val());
                        //    var dmonths = parseInt($("#ddlDeduMonth").val());
                        //    var months = parseInt($("#ddlMonth" + i).val());
                        //    if (dyears <= years) {
                        //        if (dmonths < months) {
                        //            alert("Month should be less than deduction Month");
                        //            return false;
                        //        }
                        //    }
                        //}
                        if (provi == 0) {
                            alert('Please Select Service Provider at row :' + i + '');
                            return false;
                        }
                        if (year == 0) {
                            alert('Please Select Year at row :' + i + '');
                            return false;
                        }
                        if (mnt == 0) {
                            alert('Please Select Month at row :' + i + '');
                            return false;
                        }
                        if (mbill == "") {
                            alert('Please Enter Bill No at row :' + i + '');
                            return false;
                        }
                        if (mbilldate == 0) {
                            alert('Please Select Bill Date at row :' + i + '');
                            return false;
                        }
                        if (mbillamt == "") {
                            alert('Please Enter Bill Amount at row :' + i + '');
                            return false;
                        }
                        if (remamt == "") {
                            alert('Please Enter Reimbursement Amount at row :' + i + '');
                            return false;
                        }
                        if (mbillamt != "" && remamt != "") {
                            if (parseInt(mbillamt) < parseInt(remamt)) {
                                alert('Bill Amount should be greater than Reimbursement Amount at row :' + i + '');
                                return false;
                            }
                        }
                        if (mrmk == "") {
                            alert('Please Enter Remark at row :' + i + '');
                            return false;
                        }
                        
                        var uptblcount = $('#uploadTable tr').length - 1;
                        if (uptblcount < 1) {
                            alert("Please Upload Files");
                            return false;
                        }
                        if (supp == 0) {
                            alert('Please Select Supporting Document at row :' + i + '');
                            return false;
                        }
                        //if (supp == "Y") {
                        //    if (supprmk == "") {
                        //        alert('Please Enter Supporting Particulars at row :' + i + '');
                        //        return false;
                        //    }
                        //}
                        var Total, percent_val;
                        Total = 0;
                        percent_val = 0;
                        var is_supp = $("#span_support2").html()
                        var pertage = $('#supp_perc_no').val();
                        if (is_supp == "Y") {
                            if (supp == "Yes") {
                                supp_tot = supp_tot + parseFloat(mbillamt);
                                if (supp_tot != undefined && supp_tot != "") {
                                    $("#supp_amt_tot").val(supp_tot);
                                }
                            }

                            if (supp == "No") {
                                non_supp = non_supp + parseFloat(mbillamt);
                                if (non_supp != undefined && non_supp != "") {
                                    $("#supp_amt_no_tot").val(non_supp);
                                }
                            }

                        }

                        //TOOTALBILAMT = parseFloat(TOOTALBILAMT) + parseFloat($("#amount" + i).val());
                        xmlmobile += "|ROW||";
                        xmlmobile += "|FK_MobileCard_Expense_HDR_Id||" + pk_id + "|/FK_MobileCard_Expense_HDR_Id||";
                        xmlmobile += "|MOBILE_CARD_NO||" + mno + "|/MOBILE_CARD_NO||";
                        xmlmobile += "|FK_SERVICEPROVIDER_ID||" + provi + "|/FK_SERVICEPROVIDER_ID||";
                        xmlmobile += "|YEAR||" + year + "|/YEAR||";
                        xmlmobile += "|MONTH||" + mnt + "|/MONTH||";
                        xmlmobile += "|BILL_NO||" + mbill + "|/BILL_NO||";
                        xmlmobile += "|BILL_DATE||" + mbilldate + "|/BILL_DATE||";
                        xmlmobile += "|BILL_AMOUNT||" + mbillamt + "|/BILL_AMOUNT||";
                        xmlmobile += "|REIMBURSEMENT_AMOUNT||" + remamt + "|/REIMBURSEMENT_AMOUNT||";
                        xmlmobile += "|REMARK||" + mrmk + "|/REMARK||";
                        xmlmobile += "|SUPPORT_DOC||" + supp + "|/SUPPORT_DOC||";
                        xmlmobile += "|SUPPORTING_PARTICULARS||" + "" + "|/SUPPORTING_PARTICULARS||";
                        xmlmobile += "|/ROW||";
                    }
                    xmlmobile += "|/ROWSET||";
                    
                    //percent_val = (parseFloat($("#spn_Total").html()) * (parseFloat($("#supp_perc_no").val())) / 100);
                    //$("#supp_amt_no_db").val(percent_val);
                    //if ($("#supp_amt_no_tot").val != "" && $("#supp_amt_no_tot").val != undefined && $("#supp_amt_no_db").val != "" && $("#supp_amt_no_db").val != undefined) {
                    //    if (parseFloat($("#supp_amt_no_db").val()) < parseFloat($("#supp_amt_no_tot").val())) {
                    //        $("#dev_supp_amt").val(1);
                    //    }
                    //    else {
                    //        $("#dev_supp_amt").val(0);
                    //    }
                    //}
                    //else {
                    //    $("#dev_supp_amt").val(0);
                //}
                    if ($("#txt_ded_count").val() != "0") {
                        var tbl = document.getElementById("tbldedu");
                        document.getElementById("txt_check_Nos").value = "";
                        document.getElementById("txt_ded_amount").value = 0;
                        document.getElementById("txt_ded_total_amount").value = 0;
                        var rowCount = $('#tbldedu tr').length;
                        var vals = "";
                        var j = 1;
                        for (var k = 1; k < rowCount; k++) {
                            var checkboxes = document.getElementById("checkbox" + k + "");
                            if (checkboxes.checked) {
                                document.getElementById("txt_ded_year").value += $("#ded_year" + k).val() + ";";
                                document.getElementById("txt_ded_month").value += $("#ded_month" + k).val() + ";";
                                document.getElementById("txt_ded_amount").value += $("#ded_amount" + k).val() + ";";
                                document.getElementById("txt_ded_total_amount").value = parseInt(document.getElementById("txt_ded_total_amount").value) + parseInt($("#ded_amount" + k).val());
                                vals += "," + checkboxes.value;
                                document.getElementById("txt_check_Nos").value = j++;
                            }
                        }
                        //$("#span_dedamount").html($("#txt_ded_total_amount").val())
                        if ($("#txt_ded_total_amount").val() != "0" || $("#txt_ded_total_amount").val() != undefined || $("#txt_ded_total_amount").val() != NaN) {
                            $("#span_dedamount").html(parseFloat($("#txt_ded_total_amount").val()) + parseFloat($("#txt_ded_tamount").val()))
                            ded_Amount = $("#span_dedamount").html();
                        }
                    }
                    else {
                       // $("#txt_ded_total_amount").val(0);
                    }
                    
                    //if (TotalAmt > parseInt(ded_Amount)) {
                    //    alert('Reimbursement amount cannot be greater than deduction amount!');
                    //    return false;
                    //}
               // }
            }
            else {
                /**************************Own Provided Mobile****************************/
                xmlmobile = "";
                var supp_tot = parseInt(0);
                var non_supp = parseInt(0);
                var tblmobileUser = document.getElementsByName("tbl_UserMobile");
                var tbllength = $('#tbl_UserMobile tr').length - 1;
                xmlmobile = "|ROWSET||";
                for (var q = 1; q <= tbllength; q++) {
                      var mno = document.getElementById("txt_User_MobileNo" + q).value;
                    var provi = document.getElementById("ddlServiceProviderUser" + q).value;
                    // var year = document.getElementById("ddlYearUser" + q).value;
                    //var mnt = document.getElementById("ddlMonthUser" + q).value;
                    var mbill = document.getElementById("txt_user_mobile_bill_no" + q).value;
                    var mbilldate = document.getElementById("txt_mobile_user_bill_date" + q).value;
                    var mbillamt = document.getElementById("txt_mobile_user_bill_amt" + q).value;
                    var remamt = document.getElementById("txt_user_total" + q).value;
                    var mrmk = document.getElementById("txt_user_Mremark" + q).value;
                    var pay = document.getElementById("ddlPayMode").value
                    var loc = document.getElementById("ddlLocation").value;
                    var tax = document.getElementById("txt_user_tax" + q).value;
                    var supp = document.getElementById("ddlusersupp" + q).value;
                  //  var supprmk = document.getElementById("txtusersupport" + q).value;
                    var limt = $("#txt_user_Mreimbursement").html();
                    if (supp == "No") {
                        supp = "N";
                    }
                    if (supp == "Yes") {
                        supp = "Y";
                    }
                    if (pay == 0) {
                        alert('Please Select Payment Mode');
                        return false;
                    }
                    if (loc == 0 && pay == 1) {
                        alert('Please Select Payment Location');
                        return false;
                    }
                    if (mno == "") {
                        alert('Please Enter Mobile No at row :' + q + '');
                        return false;
                    }
                    if (mno.length < 10) {
                        alert('Mobile number must be 10 digits at row :' + q + '');
                        return false;
                    }

                    if (provi == 0) {
                        alert('Please Select Service Provider at row :' + q + '');
                        return false;
                    }
                    //if (year == 0) {
                    //    alert('Please Select Year at row :' + q + '');
                    //    return false;
                    //}
                    //if (mnt == 0) {
                    //    alert('Please Select Month at row :' + q + '');
                    //    return false;
                    //}
                    if (mbill == "") {
                        alert('Please Enter Bill No at row :' + q + '');
                        return false;
                    }
                    if (mbilldate == 0) {
                        alert('Please Select Bill Date at row :' + q + '');
                        return false;
                    }
                    if (mbillamt == "") {
                        alert('Please Enter Bill Amount at row :' + q + '');
                        return false;
                    }

                    if (remamt == "") {
                        alert('Please Enter Reimbursement Amount at row :' + q + '');
                        return false;
                    }
                    if (parseInt(mbillamt) < parseInt(remamt)) {
                        alert('Bill Amount should be greater than Reimbursement Amount at row :' + q + '');
                        return false;
                    }
                    if (tax == "") {
                        alert('Please Enter Tax at row :' + q + '');
                        return false;
                    }
                    if (mrmk == "") {
                        alert('Please Enter Remark at row :' + q + '');
                        return false;
                    }
                    if (remamt != "" && limt != "") {
                        if (parseInt(remamt) > parseInt(limt)) {
                            $("#dev_supp_limit").val(1);
                        }
                        else {
                            $("#dev_supp_limit").val(0);
                        }
                    }
                    var uptblcount = $('#uploadTable tr').length - 1;
                    if (uptblcount < 1) {
                        alert("Please Upload Files");
                        return false;
                    }
                    var mbilldate = document.getElementById("txt_mobile_user_bill_date" + q).value;
                    if (mbilldate != 0) {
                        var year = mbilldate.slice(-4);
                        var t = mbilldate.indexOf("-");
                        var month = mbilldate.substring(t + 1);
                        var d = mbilldate.indexOf(year);
                        var ss = mbilldate.substring(t + 1, d - 1);
                        var mnt = convertMonthNameToNumber(ss);
                    }
                    else {
                        year = "0";
                        mnt = "0";
                    }
                    var Total, percent_val;
                    Total = 0;

                    percent_val = 0;
                    var is_supp = $("#span_support1").html()
                    var pertage = $('#supp_perc_no').val();
                    if (is_supp == "Y") {
                        if (supp == "Y") {
                            supp_tot = supp_tot + parseInt(mbillamt);
                            if (supp_tot != undefined && supp_tot != "") {
                                $("#supp_amt_tot").val(supp_tot);
                            }
                        }

                        if (supp == "N") {
                            non_supp = non_supp + parseInt(mbillamt);
                            if (non_supp != undefined && non_supp != "") {
                                $("#supp_amt_no_tot").val(non_supp);
                            }
                        }
                    }             
                    xmlmobile += "|ROW||";
                    xmlmobile += "|FK_MobileCard_Expense_HDR_Id||" + pk_id + "|/FK_MobileCard_Expense_HDR_Id||";
                    xmlmobile += "|MOBILE_CARD_NO||" + mno + "|/MOBILE_CARD_NO||";
                    xmlmobile += "|FK_SERVICEPROVIDER_ID||" + provi + "|/FK_SERVICEPROVIDER_ID||";
                    xmlmobile += "|YEAR||" + year + "|/YEAR||";
                    xmlmobile += "|MONTH||" + mnt + "|/MONTH||";
                    xmlmobile += "|BILL_NO||" + mbill + "|/BILL_NO||";
                    xmlmobile += "|BILL_DATE||" + mbilldate + "|/BILL_DATE||";
                    xmlmobile += "|BILL_AMOUNT||" + mbillamt + "|/BILL_AMOUNT||";
                    xmlmobile += "|REIMBURSEMENT_AMOUNT||" + remamt + "|/REIMBURSEMENT_AMOUNT||";
                    xmlmobile += "|TAX||" + tax + "|/TAX||";
                    xmlmobile += "|REMARK||" + mrmk + "|/REMARK||";
                    xmlmobile += "|SUPPORT_DOC||" + supp + "|/SUPPORT_DOC||";
                    xmlmobile += "|SUPPORTING_PARTICULARS||" + "" + "|/SUPPORTING_PARTICULARS||";
                    xmlmobile += "|/ROW||";
                }
                xmlmobile += "|/ROWSET||";
                percent_val = (parseFloat($("#span_usertotal").html()) * (parseFloat($("#supp_perc_no").val())) / 100);
                $("#supp_amt_no_db").val(percent_val);
                if ($("#supp_amt_no_tot").val != "" && $("#supp_amt_no_tot").val != undefined && $("#supp_amt_no_db").val != "" && $("#supp_amt_no_db").val != undefined) {
                    if (parseFloat($("#supp_amt_no_db").val()) < parseFloat($("#supp_amt_no_tot").val())) {
                        $("#dev_supp_amt").val(1);
                    }
                    else {
                        $("#dev_supp_amt").val(0);
                    }
                }
                else {
                    $("#dev_supp_amt").val(0);
                }
            }
        }
        else if (expense == "DataCard") {
            xmlmobile = "";
            var dcard = document.getElementById("txtCardNo").value;
            var dprovi = document.getElementById("ddlCardProvider").value;
            var dyear = document.getElementById("ddlCardYear").value;
            var dmnt = document.getElementById("ddlCardMonth").value;
            var bill = document.getElementById("txt_card_billno").value;
            var billdate = document.getElementById("txt_card_billdate").value;
            var billamt = document.getElementById("txt_card_billamt").value;
            var dremamt = $("#txt_Reimbursement").text();
            var rmk = document.getElementById("txt_remark").value;
            var tx = document.getElementById("txt_datacardtax").value;
            var expense = $("#lblExpenseHead").text();
            var pay = document.getElementById("ddlPayMode").value
            var loc = document.getElementById("ddlLocation").value;

            if (expense == 0) {
                alert("Please Select Expense Type...!");
                return false;
            }
            if (pay == 0) {
                alert('Please Select Payment Mode');
                return false;
            }
            if (loc == 0 && pay == 1) {
                alert('Please Select Payment Location');
                return false;
            }
            if (dcard == "") {
                alert("Please Enter Card No...!");
                return false;
            }
            if (dprovi == 0) {
                alert("Please Select Service Provider...!");
                return false;
            }
            if (dyear == 0) {
                alert("Please Select Year...!");
                return false;
            }
            if (dmnt == 0) {
                alert("Please Select Month...!");
                return false;
            }
            if (bill == "") {
                alert("Please Enter Bill No...!");
                return false;
            }
            if (billdate == 0) {
                alert("Please Select Bill Date...!");
                return false;
            }
            if (billamt == "") {
                alert("Please Enter Bill Amount...!");
                return false;
            }
            if (tx == "") {
                alert("Please Enter Tax..!");
                return false;
            }
            if (rmk == "") {
                alert("Please Enter Remark..!");
                return false;
            }

            var uptblcount = $('#uploadTable tr').length - 1;
            if (uptblcount < 1) {
                alert("Please Upload Files");
                return false;
            }
            //var ss = Mobile_DataCard_Expense.checkDuplicate(dyear, dmnt, bill);
            //if (ss.value == "true") {
            //    alert("You Already Submit Bill For This Month..!");
            //    return false;
            //}
            xmlmobile = "|ROWSET||";
            xmlmobile += "|ROW||";
            xmlmobile += "|FK_MobileCard_Expense_HDR_Id||" + pk_id  + "|/FK_MobileCard_Expense_HDR_Id||";
            xmlmobile += "|MOBILE_CARD_NO||" + dcard + "|/MOBILE_CARD_NO||";
            xmlmobile += "|FK_SERVICEPROVIDER_ID||" + dprovi + "|/FK_SERVICEPROVIDER_ID||";
            xmlmobile += "|YEAR||" + dyear + "|/YEAR||";
            xmlmobile += "|MONTH||" + dmnt + "|/MONTH||";
            xmlmobile += "|BILL_NO||" + bill + "|/BILL_NO||";
            xmlmobile += "|BILL_DATE||" + billdate + "|/BILL_DATE||";
            xmlmobile += "|BILL_AMOUNT||" + billamt + "|/BILL_AMOUNT||";
            xmlmobile += "|REIMBURSEMENT_AMOUNT||" + dremamt + "|/REIMBURSEMENT_AMOUNT||";
            xmlmobile += "|TAX||" + tx + "|/TAX||";
            xmlmobile += "|REMARK||" + rmk + "|/REMARK||";
            xmlmobile += "|SUPPORT_DOC||" + "0" + "|/SUPPORT_DOC||";
            xmlmobile += "|SUPPORTING_PARTICULARS||" + "" + "|/SUPPORTING_PARTICULARS||";
            xmlmobile += "|/ROW||";
            xmlmobile += "|/ROWSET||";
        }
        if (xmlmobile != "" && xmlmobile != "<ROWSET></ROWSET>") {
            var XMLFILE = "";
            var lastRow1 = $('#uploadTable tr').length;
            XMLFILE = "<ROWSET>";
            if (lastRow1 > 1) {
                for (var l = 0; l < lastRow1 - 1; l++) {
                    var firstCol = $("#uploadTable tr")[l + 1].cells[0].innerText;
                    var SecondCol = $("#uploadTable tr")[l + 1].cells[1].innerText;
                    XMLFILE += "<ROW>";
                    XMLFILE += "<OBJECT_TYPE>MOBILE DATACARD EXPENSE</OBJECT_TYPE>";
                    XMLFILE += "<OBJECT_VALUE>" + request_no + "</OBJECT_VALUE>";
                    XMLFILE += "<DOCUMENT_TYPE>" + firstCol + "</DOCUMENT_TYPE>";
                    XMLFILE += "<FILENAME>" + SecondCol + "</FILENAME>";
                    XMLFILE += "</ROW>";
                }
            }
            XMLFILE += "</ROWSET>";
            $("#txt_Document_Xml").val(XMLFILE);
            $("#txt_xml_data_mobile").val(xmlmobile);
if ($("#lbl_EmpCode").html() != "4263" || $("#lbl_EmpCode").html() != "4264") {
            if ($("#dev_supp_limit").val() == "1" || $("#dev_supp_amt").val() == "1") {
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("Are you sure to send your mobile request for deviation approval....?")) {
                    $("#txt_confirm").val("Yes");
                } else {
                    $("#txt_confirm").val("No");
                }
}
            }
	$("#divIns").show();
            return true;
        }
        else {
	$("#divIns").hide();
            xmlmobile = "<ROWSET></ROWSET>";
            return false;
        }
    }
    catch (exception) {
	$("#divIns").hide();
        XMLFILE = "<ROWSET></ROWSET>";
        return false;
    }
}
function convertMonthNameToNumber(monthName) {
    var myDate = new Date(monthName + " 1, 2000");
    var monthDigit = myDate.getMonth();
    return isNaN(monthDigit) ? 0 : (monthDigit + 1);
}
//**********************************************************************************
function isNumber(evt) {
    var iKeyCode = (evt.which) ? evt.which : evt.keyCode
    if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57)) {
        alert("Enter numeric value only.....!");

        return false;
    }
    return true;
}

function uploadError(sender, args) {
    alert(args.get_errorMessage());
    var uploadText = document.getElementById('FileUpload1').getElementsByTagName("input");
    for (var i = 0; i < uploadText.length; i++) {
        if (uploadText[i].type == 'text') {
            uploadText[i].value = '';
        }
    }
}
function StartUpload(sender, args) {

}

function UploadComplete(sender, args) {
    var filename = args.get_fileName();
    var n = filename.replace(/[&\/\\#,+()$~%'":*?<>{} ]/g, "");
    var fileNameSplit = n.split('.');
    var contentType = args.get_contentType();
    var filelength = args.get_length();
    var Document_Name = $("#txt_description").val();
    if (Document_Name == "") {
        alert("Please enter desciption first...!");
        return false;
    }
    if (parseInt(filelength) == 0) {
        alert("Sorry cannot upload file ! File is Empty or file does not exist");
    }
    else if (parseInt(filelength) > 20000000) {
        alert("Sorry cannot upload file ! File Size Exceeded.");
    }
    else if (contentType == "application/octet-stream" && fileNameSplit[1] == "exe") {
            alert("Kindly Check File Type.");
        }
    else {
         if (fileExt.toUpperCase() == ".JPEG" || fileExt.toUpperCase() == ".JPG" || fileExt.toUpperCase() == ".PNG" || fileExt.toUpperCase() == ".PDF") {
                addToClientTable(n, args.get_length());
            }
            else {
                alert("Unsupported File");
            }
            var fileInputElement = sender.get_inputFile();
            fileInputElement.value = "";
    }
    var uploadText = document.getElementById('FileUpload1').getElementsByTagName("input");
    for (var i = 0; i < uploadText.length; i++) {
        if (uploadText[i].type == 'text') {
            uploadText[i].value = '';
        }
    }
    document.forms[0].target = "";
}

function addToClientTable(name, size) {

    var Document_Name = $("#txt_description").val();
    var tbl = $("#uploadTable");
    var lastRow = $('#uploadTable tr').length;
    // if (Document_Name == "") {
    //     alert("Please select document type first...!");
    //     return false;
    // }
    //else 
    //if (lastRow > 1) {
    //    for (var k = 0; k < lastRow - 1; k++) {
    //        if (Document_Name == $('#uploadTable tr')[k + 1].cells[0].innerText) {
    //            alert("Document type already selected....!");
    //            return false;
    //        }
    //    }
    //}

    var html1 = "<tr><td><input class='hidden' type='text' name='txt_Country_Add" + lastRow + "' id='txt_Document_Name" + lastRow + "' value=" + Document_Name + " readonly ></input>" + Document_Name + "</td>";
    var html2 = "<td><input class='hidden' type='text' name='txt_Region_Add" + lastRow + "' id='txt_Document_File" + lastRow + "' value=" + name + " readonly ></input><a id='a_downloadfiles" + lastRow + "' style='cursor: pointer' onclick=\"return downloadfiles('" + lastRow + "');\" >" + name + "</a></td>";
    var html3 = "<td><i id='del" + lastRow + "' class='glyphicon glyphicon-trash' align='center' onclick=\"return deletefile(" + (lastRow) + ");\" ></td></tr>";
    var htmlcontent = $(html1 + "" + html2 + "" + html3);
    $('#uploadTable').append(htmlcontent);

    $("#ddl_Document").val('');

}

function downloadfiles(index) {
    window.open(g_serverpath + '/Common/FileDownload.aspx?indentno=NA&filename=' + $("#uploadTable tr")[index].cells[1].innerText + '&filetag=', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
}

function deletefile(RowIndex) {
    try {
        var tbl = $("#uploadTable");
        var lastRow = $('#uploadTable tr').length;
        var filename = $('#uploadTable tr')[RowIndex].cells[1].innerText;

        if (lastRow <= 1)
            return false;
        for (var contolIndex = RowIndex; contolIndex < lastRow - 1; contolIndex++) {
            $("#del" + (contolIndex + 1)).onclick = new Function("deletefile(" + contolIndex + ")");
            $("#del" + (contolIndex + 1)).id = "del" + contolIndex;
            $("#a_downloadfiles" + (contolIndex + 1)).onclick = new Function("downloadfiles(" + contolIndex + ")");
            $("#a_downloadfiles" + (contolIndex + 1)).id = "a_downloadfiles" + contolIndex;
            $("#txt_Document_Name" + (contolIndex + 1)).value = contolIndex;
            $("#txt_Document_Name" + (contolIndex + 1)).id = "txt_Document_Name" + contolIndex;

        }
        $('#uploadTable tr').eq(RowIndex).remove();
        deletephysicalfile(filename);
    }
    catch (Exc) { }
}
function deletephysicalfile(filename) {
    ContractualInstruction.DeleteFile(filename, callback_deletefile);
}
function callback_deletefile(response) {
    if (response.value != "") {
        alert("Document Removed Successfully..");
    }

    function viewData(index) {
        try {
            var app_path = $("#app_Path").val();
            var po_val = $("#encrypt_po_" + (index)).val();
            window.open(app_path + '/Common/ViewDocument.aspx?pono=' + po_val + '&type=PO', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
        }
        catch (exception) {

        }
    }
}
//function getDeductAmount() {
//    if ($("#ddldedYear option:selected").index() > 0) {
//        if ($("#ddlDeduMonth option:selected").index() > 0) {
//            var yr = $("#ddldedYear option:selected").text();
//            var mn = $("#ddlDeduMonth option:selected").val();
//            if (mn < 10) {
//                mn = "0" + mn;
//            }
//            var data = yr + "" + mn;
//            var username = $("#lbl_EmpCode").html();
//            Mobile_DataCard_Expense_Modification.getDeductionAmount(username, data, callback_Amount);
//        }
//        else {
//            $("#txt_deductionamt").html(0);
//        }
//    }
//    else {
//        $("#txt_deductionamt").html(0);
//    }
//}

function callback_Amount(response) {
    $("#txt_deductionamt").html(response.value);
}
