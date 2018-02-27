g_serverpath = '/Sudarshan-Portal';
var details = "";
$(document).ready(function () {
   
    App.init();
    details = $("#checkValue").val();
  
});
function getheader() {
    details = $(".demodemo input:radio:checked").val();
    var vendor = $("#txt_Username").val();
    Vendor_Advance_Request.fillVendor(details, vendor, callback_Vendor);
}

function callback_Vendor(response) {
    var header = $("#div_Header").html(); 
    header = "";
    $("#txt_Invoice_No").val('');
    $("#txt_Invoice_Amount").val('');
    $("#div_Header").html(response.value);
}

function isNumber(evt) {
    var iKeyCode = (evt.which) ? evt.which : evt.keyCode
    if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57)) {
        alert("Enter numeric value only.....!");

        return false;
    }
   return true;
}

function isNumber1(evt) {
    var DPAMT = $("#txt_Advance_PO_Amount").val();
    var Adv_Amount = $("#txt_advance_amount").val();
    var Cumu_Amount = $("#txt_Cumu_Amount").val();
    var Total = parseFloat(Adv_Amount) + parseFloat(Cumu_Amount);

    if (parseFloat(Total) > parseFloat(DPAMT)) {
        alert("Advance amount is more than advance limit...!");
        $("#txt_advance_amount").val('');
        return false;
    }
    return true;
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


function getXML() {

    var processid = $("#txtProcessID").val();
    var Stepid = $("#txt_StepId").val();
    var name = $("#txt_Vendor_Name").val();
    var code = $("#txt_Vendor_Code").val();
    var PO = $("#txt_PO_Number").val();
    var Date = $("#txt_PO_Date").val();
    var Currency = $("#txt_PO_Currency").val();
    var creater = $("#txt_PO_Created_By").val();
    var type = $("#txt_PO_Type").val();
    var Inco = $("#txt_PO_Inco").val();
    var PO_Value = $("#txt_PO_Value").val();
    var Tax = $("#txt_tax").val();
    var PO_GV = $("#txt_PO_GV").val();
    var Payment = $("#txt_Payment_terms").val();

    var Appmail = $("#txt_Approver_Mail").val();
    var venmail = $("#txt_Vendor_Mail").val();

    var Curr_Date = $("#txt_Current_Date").val();
    var Advance_Date = $("#txt_Advance_Amount_Date").val();
    var Advance_Amt = $("#txt_advance_amount").val();
    var Remark = $("#txt_Remark").val();
    //var cont_date = new Date(Date);
    if (Advance_Amt == "") {
        alert("Please Enter Advance Amount...!");
        return false;
    }
    else if (Remark == "") {
        alert("Please Enter Remark...!");
        return false;
    }
    //else if (Date > Curr_Date) {
    //    alert("Due date is not correct...!");
    //    return false;
    //}

    Vendor_Advance_Request.save(processid, Stepid, name, code, PO, Date, Currency, creater, type, Inco, PO_Value, Tax, PO_GV, Payment, Advance_Amt, Remark,Appmail,venmail, callback_save);
    
}

function callback_save(response) {
   if(response.value != "" && response.value != null){
   alert(("Advance Request Send Successfully and Your Request No.is " + response.value ),window.open('../../portal/HomePage.aspx','frmset_WorkArea'));

   }
   else{
	alert(("Advance Request Not Send...!"),window.open('../../portal/HomePage.aspx','frmset_WorkArea'));
  }
}


