
function chk_Data(){

    var selectedValue = $("#rblMSMED :selected").text();
        var is_chng = $("#is_changed").val();
        if (selectedValue == "Yes") {
            if (is_chng == 1) {
                $("#div_msmed_fileupload").show();
                $("#div_fileUpload").show();
                $("#txt_msmed_rno").attr("readonly", false);
                //$("#rblMSMED").prop("disabled", false);
                $("#lblUpload").show();
                $("#btnUpload").show();
            }
            else {
                $("#div_msmed_fileupload").show();
                $("#div_fileUpload").hide();
                $("#txt_msmed_rno").attr("readonly", true);
                //$("#rblMSMED").prop("disabled", true);
                $("#lblUpload").hide();
                $("#FileUpload1").hide();
                $("#btnUpload").hide();
                
            }
            
        }
        else {
            $("#div_msmed_fileupload").hide();
            $("#div_fileUpload").hide();
            $('#txt_msmed_rno').attr('readonly', true);
            $("#lblUpload").hide();
        }
        if ($("#txt_msmed_rno").val() != "") {
            $("#divchk").show();
            $("#lblchk").show();
        }
        else {
            $("#divchk").hide();
            $("#lblchk").hide();
        }
        $("#uploaded_file").text($("#txt_uploaded").val());
 
}


function chkValidation()
{
    try
    {
        if ($("#txt_Username").val() == "") {
            alert("Invalid Vendor...!");
            return false;
        }
        else {
            if ($("#Email").val() != "")
            {
                if ($("#txt_Email").val() == "")
                {
                    alert("Enter Email Address...!");
                    return false;
                }
            }
            if ($("#Website").val() != "") {
                if ($("#txt_Website").val() == "") {
                    alert("Enter Website Url...!");
                    return false;
                }
            }
            if ($("#Mobile").val() != "") {
                if ($("#txt_Mobile").val() == "") {
                    alert("Enter Mobile No...!");
                    return false;
                }
            }
            if ($("#Contact").val() != "") {
                if ($("#txt_Contact").val() == "") {
                    alert("Enter Contact No...!");
                    return false;
                }
            }
            if ($("#Fax").val() != "") {
                if ($("#txt_Fax").val() == "") {
                    alert("Enter Fax No...!");
                    return false;
                }
            }
            if ($("#PAN").val() != "") {
                if ($("#txt_PAN").val() == "") {
                    alert("Enter PAN No...!");
                    return false;
                }
            }
            if ($("#Address").val != "") {
                if ($("#txt_Address").val() == "") {
                    alert("Enter Address...!");
                    return false;
                }
            }
            if ($("#ECC").val() != "") {
                if ($("#txt_ECC").val() == "") {
                    alertMsg("Enter ECC NO...!");
                    return false;
                }
            }
            if ($("#City").val() != "") {
                if ($("#txt_City").val() == "") {
                    alert("Enter City...!");
                    return false;
                }
            }
            if ($("#CentralSales").val() != "") {
                if ($("#txt_Central").val() == "") {
                    alert("Enter Central Sales Tax No...!");
                    return false;
                }
            }
            if ($("#State").val() != "") {
                if ($("#txt_State").val() == "") {
                    alert("Enter State...!");
                    return false;
                }
            }
            if ($("#LocalSales").val() != "") {
                if ($("#txt_Local").val() == "") {
                    alert("Enter Local Sales Tax No...!");
                    return false;
                }
            }
            if ($("#Pin").val() != "") {
                if ($("#txt_Pin").val() == "") {
                    alert("Enter Pin Code...!");
                    return false;
                }
            }
            if ($("#Excise").val() != "") {
                if ($("#txt_Excise").val() == "") {
                    alert("Enter Excise Reg No...!");
                    return false;
                }
            }
            if ($("#bankid").val() !="") {
                if ($('#txt_BankName').val() == 0) {
                    alert("Enter Bank Name...!");
                    return false;
                }
            }
            if ($("#accountno").val() != "") {
                if ($("#txt_accno").val() == "") {
                    alert("Enter Account No...!");
                    return false;
                }
            }
            if ($("#ifsc").val() != "") {
                if ($("#txt_ifsc").val() == "") {
                    alert("Enter IFSC Code...!");
                    return false;
                }
            }
            if ($("#branch").val() != "") {
                if ($("#txt_branch").val() == "") {
                    alert("Enter Branch...!");
                    return false;
                }
            }

            var selectedValue = $("#rblMSMED :selected").text();
            var chng=$("#is_changed").val();
            if (selectedValue == "Yes") {              
                if (chng == 1) {
                    if ($("#txt_msmed_rno").val() == "") {
                        alert("Enter MSMED Registration Number...!");
                        return false;
                    }
                    else {
                        if ($("#txt_uploaded").val() == "") {
                            if ($("#txt_filename").val()) {
                                return true;
                            }
                            else {
                                alert("Select MSMED Registration Certificate...!");
                                return false;
                            }
                        }
                    }
                }
                
            }
            else {
                return true;
            }

          
        }
    }
    catch (Exc) {
        return false;
    }

}

function alertMsg(msg)
{
    $("#lblAlert").html(msg);
    $("#alertid").click();
}

function downloadfiles() {
    try {
        var app_path = $("#app_Path").val();
        var fn = $("#txt_uploaded").val();
        window.open(app_path + '/Common/FileDownload.aspx?indentno=NA&filename=' + fn + '&filetag=', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
    }
    catch (exception) {

    }
}

