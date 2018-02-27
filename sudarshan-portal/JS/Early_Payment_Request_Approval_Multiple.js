function searchData() {
    var str = document.getElementById("txt_Search").value;
    var rpp = "0";

    if ($("#ddlRecords :selected").text() != undefined) {
        rpp = $("#ddlRecords :selected").text();
        $("#ddlText1").val($("#ddlRecords :selected").text());
    }
    else {
        rpp = $("#ddlText1").val();
    }

    Early_Payment_Request_Approval_Multiple.fillSearch(str, rpp, callback_Inco);
}

function callback_Inco(response) {
    document.getElementById("div_InvoiceDetails").innerHTML = response.value;
}


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
        var maildata = "";
        var d = document.getElementsByName('early');
        for (var i = 0; i < d.length; i++) {
            if (d[i].checked) {
                chk = chk + 1; 
                if (chk == 1) {
                    PK_Dispatch_Note_ID = $("#PK_Dispatch_Note_ID" + (i + 1)).val() + "$" + $("#fk_process" + (i + 1)).val() + "$" + $("#iid" + (i + 1)).val() + "$" + $("#wiid" + (i + 1)).val() + "$" + $("#h_info" + (i + 1)).val();
                    maildata = $("#ven_code" + (i + 1)).val();
                }
                else {
                    PK_Dispatch_Note_ID = PK_Dispatch_Note_ID + "@" + $("#PK_Dispatch_Note_ID" + (i + 1)).val() + "$" + $("#fk_process" + (i + 1)).val() + "$" + $("#iid" + (i + 1)).val() + "$" + $("#wiid" + (i + 1)).val() + "$" + $("#h_info" + (i + 1)).val();
                    maildata = maildata + "|" + $("#ven_code" + (i + 1)).val();
                }
            }
        }

        $("#split_data").val(PK_Dispatch_Note_ID);
        $("#mail_data").val(maildata);

        if (chk < 1) {
            alert('Select Atleast One Early Payment Request...!');
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


