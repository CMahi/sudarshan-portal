g_serverpath = '/Sudarshan-Portal';
var details = "";

$(document).ready(function () {
    $("#txt_tplace").hide();
    $("#txt_fplace").hide();

    $(".datepicker-dropdown").datepicker({ format: 'dd-M-yyyy', todayBtn: 'linked' })
//    if ($("#txt_adcount").val() == "0") {
//        $("#div_details").hide();
//    }
//    else {
//        $("#div_details").show();
//    }
    //if ($("#txt_opencount").val() == "1") {
    //    alert("Your have claim for old requests! Unable To Claim New Advance Request...!");
    //    $('input[Id="btn_Save"]').attr('disabled', 'disabled');
    //}
    //else {
    //    $('input[Id="btn_Save"]').removeAttr('disabled', 'disabled');
    //    if ($("#txt_expire").val() == "1") {
    //        alert("Your already have advance! Unable To Claim New Advance Request...!");
    //        $('input[Id="btn_Save"]').attr('disabled', 'disabled');
    //    }
    //    else {
    //        $('input[Id="btn_Save"]').removeAttr('disabled', 'disabled');
    //    }
    //}
    var mode = $("#ddl_Payment_Mode").val();

    var adva = $("#span_advance").html();
    if (adva == "Other Expense Advance") {
        $("#divpol").hide();
    }
    else {
        $("#divpol").show();
    }
    if (mode == 2) {
        $("#div_payment").hide();
    }
    else {
        $("#div_payment").show();
    }
});
$("#ddl_Payment_Mode").change(function () {
    var mode = $("#ddl_Payment_Mode").val();
    if (mode == 2) {
        $("#div_payment").hide();
    }
    else {
        $("#div_payment").show();
    }

});
//$("#ddladvancetype").change(function () {
//var advancefor = $("#ddladvancetype").val();
////var advancef = getSelectedText('ddladvancetype');
//if (advancefor == "1") {
//    $("#advance_other").hide();
//    $("#advance_travel").show();
//}
//else if (advancefor == "3") {
//    $("#advance_travel").hide();
//    $("#advance_other").show();
//}
//Advance_Request.fillGLCode(advancefor, callback_Detail);
//});

function chk_class_From(i) {
    $("#txt_fplace").val("");
  
    if ($("#From_City option:selected").text() == "Other") {
        $("#txt_f_city").show();
    }
    else {
        $("#txt_f_city").hide();
    }
}
function chk_class_To(i) {
    $("#txt_tplace").val("");
    if ($("#To_City option:selected").text() == "Other") {
        $("#txt_t_city").show();
    }
    else {
        $("#txt_t_city").hide();
    }
    var tocity = $("#To_City").val();
    var grade = $("#span_grade").html();
    Advance_Request_Modification.fillAmountall(grade, tocity, callback_Detail);
}
$('#txt_fdate').datepicker({ format: 'dd-M-yyyy', autoclose: true, todayBtn: 'linked' }).on('changeDate', function (ev) {
   daydiffrence();
});

$('#txt_tdate').datepicker({ format: 'dd-M-yyyy', autoclose: true, todayBtn: 'linked' }).on('changeDate', function (ev) {
    daydiffrence();
});

function daydiffrence() {
    if (document.getElementById('txt_fdate').value == "") {
        return false;
    }
    if (document.getElementById('txt_tdate').value == "") {
        return false;
    }
    var start = $('#txt_fdate').val();
    var stard = start.substring(0, 2);
    var starm = start.substring(3, 6);
    var stary = start.substring(7, 11);

    var end = $('#txt_tdate').val();
    var endd = end.substring(0, 2);
    var endm = end.substring(3, 6);
    var endy = end.substring(7, 11);
    var d1 = new Date(stary, getMonthFromString(starm) - 1, stard);
    var d2 = new Date(endy, getMonthFromString(endm) - 1, endd);
    if (d1 > d2) {
        alert('To Date must be greater than from Date');
        $('#txt_tdate').focus();
        $('#txt_tdate').val('');
        return false;
    }
    else {
        var amt = 0;
        amt = $("#txt_amount").val();
        var tocity = $("#To_City").val();
        var grade = $("#span_grade").html();
        Advance_Request_Modification.fillAmountall(grade, tocity, callback_Detail);
    }
}
$("#ddlPayMode").change(function () {
    var mode = $("#ddlPayMode").val();
    if (mode == 2) {
        $("#div_payment").hide();
    }
    else {
        $("#div_payment").show();
    }

});
//function chk_amt_other(i) {
//    var tocity = $("#To_City").val();
//    var desg = $("#span_designation").html();
//    Advance_Request_Modification.fillAmountall(desg, tocity, callback_Detail);
//}
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;

    return true;
}
function checkamount() {

    var p_amount = 0;
    var amt = 0;
    var txt_adamount = $("#txt_adamount").val(); ;
    var advancefor = $("#ddladvancetype").val();

    if ($("#txt_amount").val() != "" || $("#txt_amount").val() != undefined || $("#txtx_other_amount").val() != "" || $("#txtx_other_amount").val() != undefined) {
        if (advancefor == "1") {
            amt = $("#txt_amount").val();
            p_amount = parseInt($("#total_amount").val()) + parseInt(amt);
            if (parseInt(txt_adamount) < parseInt(p_amount)) {
                alert("Total of existing advances is greater than allowable limit...!");
                $('input[Id="btn_Save"]').attr('disabled', 'disabled');
            }
            else {
                $('input[Id="btn_Save"]').removeAttr('disabled', 'disabled');
            }
        }
        else if (advancefor == "3") {
            amt = $("#txtx_other_amount").val();
            p_amount = parseInt($("#total_amount").val()) + parseInt(amt);
            if (parseInt(txt_adamount) < parseInt(p_amount)) {
                alert("Total of existing advances is greater than allowable limit...!");
                $('input[Id="btn_Save"]').attr('disabled', 'disabled');
            }
            else {
                $('input[Id="btn_Save"]').removeAttr('disabled', 'disabled');
            }
        }
    }

//    var p_amount, total_amount = 0;
//    var amt = 0;
//    if ($("#txt_amount").val() != "" || $("#txt_amount").val() != undefined) {
//        var advancefor = $("#txt_adv_type").val();
//        //if (advancefor == "1") {
//        amt = $("#txt_amount").val();
//        var open = $("#txt_opcount").val();
//        if ($("#total_amount").val() == "" || $("#total_amount").val() == undefined || $("#total_amount").val() == "NaN") {
//            $("#total_amount").val(0);
//        }
//        if (open == 1) {
//            if ($("#total_amount").val() != "" || $("#total_amount").val() != undefined) {
//                p_amount = parseInt($("#total_amount").val()) + parseInt(amt);
//            }
//            if ($("#txt_adamount").val() != "" || $("#txt_adamount").val() != undefined) {
//                total_amount = $("#txt_adamount").val();
//            }
//            if (parseInt(p_amount) > parseInt(total_amount)) {
//                alert("Amount is greater than advance policy limit...!");
//                $('input[Id="btn_Save"]').attr('disabled', 'disabled');
//            }
//            else {
//                $('input[Id="btn_Save"]').removeAttr('disabled', 'disabled');
//            }
//        }
//        else {
//            if ($("#total_amount").val() != "" || $("#total_amount").val() != undefined) {
//                p_amount = parseInt($("#total_amount").val());
//            }
//            if ($("#txt_adamount").val() != "" || $("#txt_adamount").val() != undefined) {
//                total_amount = $("#txt_adamount").val();
//            }
//            if (parseInt(p_amount) > parseInt(total_amount)) {
//                alert("Amount is greater than advance policy limit...!");
//                $('input[Id="btn_Save"]').attr('disabled', 'disabled');
//            }
//            else {
//                $('input[Id="btn_Save"]').removeAttr('disabled', 'disabled');
//            }
//        }
//    }
}

function callback_Detail(response) {
    var amoun = response.value;
    var aalamt = 0;
    var days = 0;
    var start = $('#txt_fdate').datepicker('getDate');
    var end = $('#txt_tdate').datepicker('getDate');
    if (start < end) {
        days = ((end - start) / 1000 / 60 / 60 / 24) + 1;
        aalamt = Math.round(parseFloat(days) * parseFloat(amoun));
    }
    else {
        days = (1);
        aalamt = Math.round(parseFloat(days) * parseFloat(amoun));
    }
    $("#lblallowedamt").html(aalamt);
}
$(function () {
    $("#txt_fdate").datepicker({ dateFormat: 'dd-mm-yy' });
    $("#txt_tdate").datepicker({ dateFormat: 'dd-mm-yy' });
});

function createxml() {
    try {
        var advancety = "";
        var pk_id = document.getElementById("txt_pk_id").value;
        var request_no = document.getElementById("txt_Request").value;
        advancety = $("#txt_adv_type").val();
        var pay = document.getElementById("ddl_Payment_Mode").value
        var loc = document.getElementById("ddlAdv_Location").value;

        if (pay == 0) {
            alert('Please Select Payment Mode');
            return false;
        }
        if (loc == 0 && pay == 1) {
            alert('Please Select Payment Location');
            return false;
        }
        if (advancety == "1") {
            var xmlother = "";
            xmlother = "|ROWSET||";
            var ddlfcity = document.getElementById("From_City").value;
            var pto = document.getElementById("txt_t_city").value;
            var pfrom = document.getElementById("txt_f_city").value;
            var ddltcity = document.getElementById("To_City").value;
            var fdate = document.getElementById("txt_fdate").value;
            var tdate = document.getElementById("txt_tdate").value;
            var rmk = document.getElementById("txt_remark").value;
            var amt = document.getElementById("txt_amount").value;
            var allamt = $("#lblallowedamt").html();
            $("#txt_rmk").val(rmk);
            if (ddlfcity == "Other") {
                if (pfrom == "") {
                    alert('Please Enter From Place');
                    return false;
                }
            }
            if (ddltcity == "Other") {
                if (pto == "") {
                    alert('Please Enter To Place');
                    return false;
                }
            }
            if (ddltcity != "-1" && ddlfcity != "-1") {
                if (ddltcity == ddlfcity) {
                    alert('From city and To city should be different');
                    return false;
                }
            }
            if (fdate == 0) {
                alert('Please Select From Date');
                return false;
            }
            if (tdate == 0) {
                alert('Please Select To Date');
                return false;
            }

            if (amt == "") {
                alert('Please Enter Amount');
                return false;
            }
            if (rmk == "") {
                alert("Please Enter Remark");
                return false;
            }
            if (amt != "" && allamt != "") {
                if (parseInt(amt) > parseInt(allamt)) {
                    $("#txt_deviate").val(1);
                }
                else {
                    $("#txt_deviate").val(0);
                }
            }
            //var uptblcount = $('#uploadTable tr').length - 1;
            //if (uptblcount < 1) {
            //    alert("Please Upload Files");
            //    return false;
            //}

            xmlother += "|ROW||";
            xmlother += "|FK_ADVANCE_HDR_Id||" + pk_id + "|/FK_ADVANCE_HDR_Id||";
            xmlother += "|ADVANCE_FOR||" + advancety + "|/ADVANCE_FOR||";
            xmlother += "|FROM_CITY||" + ddlfcity + "|/FROM_CITY||";
            xmlother += "|TO_CITY||" + ddltcity + "|/TO_CITY||";
            xmlother += "|OTHER_F_CITY||" + pfrom + "|/OTHER_F_CITY||";
            xmlother += "|OTHER_T_CITY||" + pto + "|/OTHER_T_CITY||";
            xmlother += "|FROM_DATE||" + fdate + "|/FROM_DATE||";
            xmlother += "|TO_DATE||" + tdate + "|/TO_DATE||";
            xmlother += "|AMOUNT||" + amt + "|/AMOUNT||";
            xmlother += "|ALLOWED_AMOUNT||" + allamt + "|/ALLOWED_AMOUNT||";
            xmlother += "|REMARK||" + rmk + "|/REMARK||";
            xmlother += "|ADVANCE_DATE||" + "" + "|/ADVANCE_DATE||";
            xmlother += "|/ROW||";

            xmlother += "|/ROWSET||";
        }
        else if (advancety == "3") {
            var xmlother = "";
            xmlother = "|ROWSET||";
            var advandate = document.getElementById("txt_advance_date").value;
            var rmk = document.getElementById("txt_remark").value;
            var amt = document.getElementById("txt_amount").value;
            $("#txt_rmk").val(rmk);
            //  var allamt = document.getElementById("lblallowedamt").value;
//            var pay = document.getElementById("ddl_Payment_Mode").value
//            var loc = document.getElementById("ddlAdv_Location").value;

//            if (pay == 0) {
//                alert('Please Select Payment Mode');
//                return false;
//            }
//            if (loc == 0 && pay == 1) {
//                alert('Please Select Payment Location');
//                return false;
//            }
   
            if (advandate == 0) {
                alert('Please Select Advance Date');
                return false;
            }

            if (amt == "") {
                alert('Please Enter Amount');
                return false;
            }
            if (rmk == "") {
                alert("Please Enter Remark");
                return false;
            }
            //var uptblcount = $('#uploadTable tr').length - 1;
            //if (uptblcount < 1) {
            //    alert("Please Upload Files");
            //    return false;
            //}

            xmlother += "|ROW||";
            xmlother += "|FK_ADVANCE_HDR_Id||" + pk_id + "|/FK_ADVANCE_HDR_Id||";
            xmlother += "|ADVANCE_FOR||" + advancety + "|/ADVANCE_FOR||";
            xmlother += "|FROM_CITY||" + "" + "|/FROM_CITY||";
            xmlother += "|TO_CITY||" + "" + "|/TO_CITY||";
            xmlother += "|OTHER_F_CITY||" + "" + "|/OTHER_F_CITY||";
            xmlother += "|OTHER_T_CITY||" + "" + "|/OTHER_T_CITY||";
            xmlother += "|FROM_DATE||" + "" + "|/FROM_DATE||";
            xmlother += "|TO_DATE||" + "" + "|/TO_DATE||";
            xmlother += "|AMOUNT||" + amt + "|/AMOUNT||";
            xmlother += "|ALLOWED_AMOUNT||" + 0 + "|/ALLOWED_AMOUNT||";
            xmlother += "|REMARK||" + rmk + "|/REMARK||";
            xmlother += "|ADVANCE_DATE||" + advandate + "|/ADVANCE_DATE||";
            xmlother += "|/ROW||";

            xmlother += "|/ROWSET||";

        }
        var XMLFILE = "";
        var lastRow1 = $('#uploadTable tr').length;
        XMLFILE = "<ROWSET>";
        if (lastRow1 > 1) {
            for (var l = 0; l < lastRow1 - 1; l++) {
                var firstCol = $("#uploadTable tr")[l + 1].cells[0].innerText;
                var SecondCol = $("#uploadTable tr")[l + 1].cells[1].innerText;
                XMLFILE += "<ROW>";
                XMLFILE += "<OBJECT_TYPE>ADVANCE</OBJECT_TYPE>";
                XMLFILE += "<OBJECT_VALUE>" + request_no + "</OBJECT_VALUE>";
                XMLFILE += "<DOCUMENT_TYPE>" + firstCol + "</DOCUMENT_TYPE>";
                XMLFILE += "<FILENAME>" + SecondCol + "</FILENAME>";
                XMLFILE += "</ROW>";
            }
        }
        XMLFILE += "</ROWSET>";
        $("#txt_Document_Xml").val(XMLFILE);
        $("#txt_xml_data_vehicle").val(xmlother);
        if ($("#lbl_EmpCode").html() != "4262" || $("#lbl_EmpCode").html() != "4263") {
            if ($("#txt_deviate").val() == "1") {
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("Are you sure to send your advance request for deviation approval....?")) {
                    $("#txt_confirm").val("Yes");
                } else {
                    $("#txt_confirm").val("No");
                    return false;
                }

            }
            else {
                $("#txt_confirm").val("");
            }
        }
        $("#divIns").show();
        return true;
    }
    catch (exception) {
        $("#divIns").hide();
        XMLFILE = "<ROWSET></ROWSET>";
        return false;
    }
}


//**********************************************************************************
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
        var fileExt = '.' + filename.split('.').pop();
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
    var html1 = "<tr><td><input class='hidden' type='text' name='txt_Country_Add" + lastRow + "' id='txt_Document_Name" + lastRow + "' value=" + Document_Name + " readonly ></input>" + Document_Name + "</td>";
    var html2 = "<td><input class='hidden' type='text' name='txt_Region_Add" + lastRow + "' id='txt_Document_File" + lastRow + "' value=" + name + " readonly ></input><a id='a_downloadfiles" + lastRow + "' style='cursor: pointer' onclick=\"return downloadfiles('" + lastRow + "');\" >" + name + "</a></td>";
    var html3 = "<td><i id='del" + lastRow + "' class='glyphicon glyphicon-trash' align='center' onclick=\"return deletefile(" + (lastRow) + ");\" ></td></tr>";
    var htmlcontent = $(html1 + "" + html2 + "" + html3);
    $('#uploadTable').append(htmlcontent);
    $("#ddl_Document").val('');
}

function downloadfiles(index) {
    window.open(g_serverpath + '/Common/FileDownload.aspx?indentno=' + $("#txt_Request").val() + '&filename=' + $("#uploadTable tr")[index].cells[1].innerText + '&filetag=', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
}

function deletefile(RowIndex) {
    try {
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
}

