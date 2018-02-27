//function searchData() {
//    var str = document.getElementById("txt_Search").value;
//    var rpp = "0";

//    if ($("#ddlRecords :selected").text() != undefined) {
//        rpp = $("#ddlRecords :selected").text();
//        $("#ddlText1").val($("#ddlRecords :selected").text());
//    }
//    else {
//        rpp = $("#ddlText1").val();
//    }

//    Early_Payment_Request_Approval_Multiple.fillSearch(str, rpp, callback_Inco);
//}

//function callback_Inco(response) {
//    document.getElementById("div_InvoiceDetails").innerHTML = response.value;
//}


function gotopage(objButton, str) {
    try {
        var pgno = objButton.value;
        var strData = $("#txt_Search").val();
        if (strData != undefined || strData != null) {
            Early_Payment_Request_Approval_Multiple.fillGoToPage1(strData, pgno, str, callback_Inco);
        }
        else {
            Early_Payment_Request_Approval_Multiple.fillGoToPage(pgno, str, callback_Inco);
        }
    }
    catch (exception) {

    }
}

function prepareData()
{
    try {
        var chk = 0;
        var PK_Dispatch_Note_ID = "";
        var d = document.getElementsByName('early');
        for (var i = 0; i < d.length; i++) {
            if (d[i].checked) {
                chk = chk + 1; 
                if (chk == 1) {
                    PK_Dispatch_Note_ID = $("#txt_Vendor_Code" + (i + 1)).val() + "$" + $("#txt_Vendor_Email" + (i + 1)).val() + "$" + $("#txt_Vendor_Password" + (i + 1)).val();
                }
                else {
                    PK_Dispatch_Note_ID = PK_Dispatch_Note_ID + "-" + $("#txt_Vendor_Code" + (i + 1)).val() + "$" + $("#txt_Vendor_Email" + (i + 1)).val() + "$" + $("#txt_Vendor_Password" + (i + 1)).val();
                }
            }
        }

        $("#split_data").val(PK_Dispatch_Note_ID);

        if (chk < 1) {
            alert('Select Atleast One Vendor...!');
            return false;
        }
        else {
            //alert(chk + ' Requests Selected');
            //alert(PK_Dispatch_Note_ID);
            return true;
        }
    }
    catch (exception)
    { alert(exception)}
}


