
function change_Image(id,vendor_Code,PO,status)
{
    $("#div_popup").show();
    Vendor_Service_Invoice_Details_Report.fillDetail(id, vendor_Code, PO, status, callback_detail);
}

function callback_detail(response) {
    $("#div_popup").hide();
    $("#div_detail").html(response.value);
}


function submitvalidation() {
    var Vendor = $("#ddl_Vendor").val();
    var From = $("#txt_form_Date").val();
    var To = $("#text_To_Date").val();
    if (Vendor == "" && From == "" && To == "") {
        alert("Please select one filter...!");
        return false;
    }
    else if (Vendor != "" && (From != "" && To == "")) {
        alert("Please Select To Date...!");
        return false;
    }
    else if (Vendor != ""  && (From == "" && To != "")) {
        alert("Please Select From Date...!");
        return false;
    }
    else if (From == "" && To != "") {
        alert("Please Select From Date...!");
        return false;
    }
    else if (From != "" && To == "") {
        alert("Please Select To Date...!");
        return false;
    }
   // $("#divIns").show();
}

function Clear() {
     $("#txt_form_Date").val('');
    $("#text_To_Date").val('');
    $("#ddl_Vendor").val('');
    //$("#Expand").html('');
}

//8888535782