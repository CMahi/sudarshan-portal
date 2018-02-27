	g_serverpath = '/Sudarshan-Portal';
var details = "";

$(document).ready(function () {
    if ($("#txt_approvar").val() == "NA" || $("#txt_approvar").val() == "") {
        alert("Approver Is Not Available! Unable To Claim Advance Request...!");
        $('input[Id="btn_Save"]').attr('disabled', 'disabled');
    }
    else {
        $('input[Id="btn_Save"]').removeAttr('disabled', 'disabled');
    }
    $("#txt_tplace").hide();
    $("#txt_fplace").hide();
    
    $(".datepicker-dropdown").datepicker({ format: 'dd-M-yyyy', todayBtn: 'linked', startDate: new Date() })
    if ($("#txt_adcount").val() == "0") {
        $("#div_details").hide();
    }
    else {
        $("#div_details").show();
    }
    var mode = $("#ddlPayMode").val();
    if (mode == 2) {
        $("#div_payment").hide();
    }
    else {
        $("#div_payment").show();
    }
    if ($("#txt_confirm").val() != "") {
        if ($("#txt_confirm").val() == "No") {
            $("#advance_travel").show();
            $("#advance_other").hide();
        }
    }
});
$("#ddladvancetype").change(function () {
    var openadv = 0;
    var advancefor = $("#ddladvancetype").val();
    var user = $("#txt_Username").val();
    if (advancefor == "1") {
        $("#advance_travel").show();
        $("#advance_other").hide();
        openadv = $('#txt_domeopen').val();
    }
    else if (advancefor == "3") {
        $("#advance_travel").hide();
        $("#advance_other").show();
        openadv = $('#txt_otheropen').val();
    }
    clear();
    Advance_Request.changetype(advancefor, user,openadv, callback_open_detail);

});
function clear() {

    $("#ddlFromPlace").val(0);
    $("#ddlToPlace").val(0);
    $("#txt_fdate").val('');
    $("#txt_tdate").val('');
    $("#lblallowedamt").html('');
    $("#txt_amount").val('');
    $("#txt_remark").val('');
    $("#txt_advance_date").val('');
    $("#txtx_other_amount").val('');
    $("#txt_other_remark").val('');
}
function callback_open_detail(response) {
    var flagcnt = 0;
    var flagday = 0;
    var arr = response.value.split("#");
    var advancefor = $("#ddladvancetype").val();
    $('#total_amount').val(arr[1]);
    if ($('#total_amount').val() == "" || $('#total_amount').val() == "NaN") {
        $('#total_amount').val(0);
        }
    $('#txt_pkexpenseid').val(arr[0]);
    $('#txt_opencount').val(arr[2]);
    var days = arr[3];
    if ($('#txt_pkexpenseid').val() == "31") {
        if ($("#txt_policycnt").val() == "NA" || $("#txt_policycnt").val() == "" || $("#txt_policycnt").val() == "0") {
            alert("Policy Is Not Available! Unable To Claim Advance Request...!");
            $('input[Id="btn_Save"]').attr('disabled', 'disabled');
            
        }
        else {
            $('input[Id="btn_Save"]').removeAttr('disabled', 'disabled');
        }
    }
    var opencnt = $('#txt_opencount').val();

    if (advancefor == "1") {
        var demopen = $('#txt_domeopen').val();
        var demdays = $('#txt_adperiod').val();
        if (parseInt(demopen) <= parseInt(opencnt)) {
            alert("Already three request open !Unable To Claim Advance Request...");
            window.open(g_serverpath + '/Portal/SCIL/Home.aspx', 'frmset_WorkArea');
            $('input[Id="btn_Save"]').attr('disabled', 'disabled');
            flagcnt = 1;
        }
        if (parseInt(demdays) <= parseInt(days)) {
            alert("Advance Request settle after next advance !Unable To Claim Advance Request...");
            $('input[Id="btn_Save"]').attr('disabled', 'disabled');
            flagday = 1;
        }
        else if (flagcnt == 0 && flagday == 0) {
            $('input[Id="btn_Save"]').removeAttr('disabled', 'disabled');
        }

    }
    else if (advancefor == "3") {
        var otheropen = $('#txt_otheropen').val();
        var otherdays = $('#txt_othperiod').val();
        if (parseInt(otheropen) <= parseInt(opencnt)) {
            alert("Already three request open !Unable To Claim Advance Request...");
            $('input[Id="btn_Save"]').attr('disabled', 'disabled');
            flagcnt = 1;
        }
        if (parseInt(otherdays) <= parseInt(days)) {
            alert("Advance Request settle after next advance !Unable To Claim Advance Request...");
            $('input[Id="btn_Save"]').attr('disabled', 'disabled');
            flagday = 1;
        }
        else if (flagcnt == 0 && flagday == 0) {
            $('input[Id="btn_Save"]').removeAttr('disabled', 'disabled');
        }
    }



//    var arr = response.value.split(",");
//    $('#txt_adamount').val(arr[0]);
//    if ($('#txt_adamount').val() == "" || $('#txt_adamount').val() == "NaN") {
//        $('#txt_adamount').val(0);
//    }
//    $('#txt_adperiod').val(arr[1]);
//    $('#txt_expire').val(arr[2]);
//    $('#total_amount').val(arr[3]);
//    if ($('#total_amount').val() == "" || $('#total_amount').val() == "NaN") {
//        $('#total_amount').val(0);
//    }
//    $('#txt_opcount').val(arr[4]);
//    $('#txt_opencount').val(arr[5]);
//    $('#txt_pkexpenseid').val(arr[6]);

//    if ($("#txt_opencount").val() == "1") {
//        alert("Your have already claimed 3 advances! Kindly settle the existing advances first to claim fresh advance...!");
//        $('input[Id="btn_Save"]').attr('disabled', 'disabled');
//    }
//    else {
//        $('input[Id="btn_Save"]').removeAttr('disabled', 'disabled');
//        if ($("#txt_expire").val() == "1") {
//            alert("Aging of existing advances is more than allowable limit! Unable to process New Advance Request...!");
//            $('input[Id="btn_Save"]').attr('disabled', 'disabled');
//        }
//        else {
//            $('input[Id="btn_Save"]').removeAttr('disabled', 'disabled');
//            if ($('#txt_pkexpenseid').val() == "31") {
//                if ($("#txt_policycnt").val() == "NA" || $("#txt_policycnt").val() == "" || $("#txt_policycnt").val() == "0") {
//                    alert("Policy Is Not Available! Unable To Claim Advance Request...!");
//                    $('input[Id="btn_Save"]').attr('disabled', 'disabled');
//                   
//                }
//                else {
//                    $('input[Id="btn_Save"]').removeAttr('disabled', 'disabled');
//                }
//            }

//        }
//    }
    Advance_Request.showall(advancefor, user, callback_all_detail);
}
function callback_all_detail(response) {
    $("#divalldata").html(response.value);
    $("#data-table1").dataTable();
}
function chk_class_From(i) {
    $("#txt_fplace").val('');
    if ($("#ddlFromPlace option:selected").text() == "Other") {
        $("#txt_fplace").show();
    }
    else {
        $("#txt_fplace").hide();
    }
}
function chk_class_To(i) {
    $("#txt_tplace").val("");
    if ($("#ddlToPlace option:selected").text() == "Other") {
        $("#txt_tplace").show();
    }
    else {
        $("#txt_tplace").hide();
    }
    var tocity = $("#ddlToPlace").val();
    var grade = $("#lbl_Grade").html();
    Advance_Request.fillAmountall(grade, tocity, callback_Detail);
}
//function chk_amt_other(i) {
//    var tocity = $("#ddlToPlace").val();
//    var desg = $("#lbl_desgnation").html();
//  //  Advance_Request.fillAmountall(desg, tocity, callback_Detail);
//}
$('#txt_fdate').datepicker({ format: 'dd-M-yyyy', autoclose: true, todayBtn: 'linked', startDate: new Date() }).on('changeDate', function (ev) {
    daydiffrence();
});

$('#txt_tdate').datepicker({ format: 'dd-M-yyyy', autoclose: true, todayBtn: 'linked', startDate: new Date() }).on('changeDate', function (ev) {
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
        $("#lblallowedamt").html('');
        return false;
    }
    else {
        var amt = 0;
        amt = $("#txt_amount").val();
        var tocity = $("#ddlToPlace").val();
        var grade = $("#lbl_Grade").html();
        if (tocity == 0) {
            alert("Plese Select City First...!");
            return false;
        }
        Advance_Request.fillAmountall(grade, tocity, callback_Detail);
    }
}
function enable_disable() {
    var mode = $("#ddlPayMode").val();
    if (mode == 2) {
        $("#div_payment").hide();
    }
    else {
        $("#div_payment").show();
    }
}
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
            if(parseInt(txt_adamount)< parseInt(p_amount)){
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
//    var advancefor = $("#ddladvancetype").val();
//    if ($("#txt_amount").val() != "" || $("#txt_amount").val() != undefined || $("#txtx_other_amount").val() != "" || $("#txtx_other_amount").val() != undefined) {
//        if (advancefor == "1") {
//            amt = $("#txt_amount").val();
//        }
//        else if (advancefor == "3") {
//            amt = $("#txtx_other_amount").val();
//        }
//        var open = $("#txt_opcount").val();
//        if (open == 1) {
//            if ($("#total_amount").val() != "" || $("#total_amount").val() != undefined) {
//                p_amount = parseInt($("#total_amount").val()) + parseInt(amt);
//            }
//            if ($("#txt_adamount").val() != "" || $("#txt_adamount").val() != undefined) {
//                total_amount = $("#txt_adamount").val();
//            }
//            if (parseInt(p_amount) > parseInt(total_amount)) {
//                alert("Total of existing advances is greater than allowable limit...!");
//                $('input[Id="btn_Save"]').attr('disabled', 'disabled');
//            }
//            else {
//                $('input[Id="btn_Save"]').removeAttr('disabled', 'disabled');
//            }
//        }
//        else {
//            if ($("#total_amount").val() != "" || $("#total_amount").val() != undefined) {
//                p_amount = parseInt($("#total_amount").val()) + parseInt(amt);
//            }
//            if ($("#txt_adamount").val() != "" || $("#txt_adamount").val() != undefined) {
//                total_amount = $("#txt_adamount").val();
//            }
//            if (parseInt(p_amount) > parseInt(total_amount)) {
//                alert("Total of existing advances is greater than allowable limit...!");
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
         days = ((end - start) / 1000 / 60 / 60 / 24)+1;       
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
        advancety = $("#ddladvancetype").val();
        var pay = document.getElementById("ddlPayMode").value
        var loc = document.getElementById("ddlLocation").value;
        if (advancety == 0) {
            alert("Please Select Advance For...!");
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
        else if (advancety == "1") {
            var xmlother = "";
            xmlother = "|ROWSET||";
            var ddlfcity = document.getElementById("ddlFromPlace").value;
            var pto = document.getElementById("txt_tplace").value;
            var pfrom = document.getElementById("txt_fplace").value;
            var ddltcity = document.getElementById("ddlToPlace").value;
            var fdate = document.getElementById("txt_fdate").value;
            var tdate = document.getElementById("txt_tdate").value;
            var rmk = document.getElementById("txt_remark").value;
            var amt = document.getElementById("txt_amount").value;
            var allamt = $("#lblallowedamt").html();
          

           
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
            xmlother += "|FK_ADVANCE_HDR_Id||#|/FK_ADVANCE_HDR_Id||";
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
            var rmk = document.getElementById("txt_other_remark").value;
            var amt = document.getElementById("txtx_other_amount").value;
            //  var allamt = document.getElementById("lblallowedamt").value;
//            var pay = document.getElementById("ddlPayMode").value
//            var loc = document.getElementById("ddlLocation").value;

//            if (pay == 0) {
//                alert('Please Select Payment Mode');
//                return false;
//            }
//            if (loc == 0 && pay == 1) {
//                alert('Please Select Payment Location');
//                return false;
//            }

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
            //var uptblcount = $('#uploadTable tr').length - 1;
            //if (uptblcount < 1) {
            //    alert("Please Upload Files");
            //    return false;
            //}

            xmlother += "|ROW||";
            xmlother += "|FK_ADVANCE_HDR_Id||#|/FK_ADVANCE_HDR_Id||";
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
                XMLFILE += "<OBJECT_VALUE>#</OBJECT_VALUE>";
                XMLFILE += "<DOCUMENT_TYPE>" + firstCol + "</DOCUMENT_TYPE>";
                XMLFILE += "<FILENAME>" + SecondCol + "</FILENAME>";
                XMLFILE += "</ROW>";
            }
        }
        XMLFILE += "</ROWSET>";
        $("#txt_Document_Xml").val(XMLFILE);
        $("#txt_xml_data_vehicle").val(xmlother);
        if ($("#lbl_EmpCode").html() != "4263" || $("#lbl_EmpCode").html() != "4262") {
            if ($("#txt_deviate").val() == "1") {
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("Are you sure to send your advance request for deviation approval....?")) {
                    $("#txt_confirm").val("Yes");
                    //  return true;
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
    window.open(g_serverpath + '/Common/FileDownload.aspx?indentno=NA&filename=' + $("#uploadTable tr")[index].cells[1].innerText + '&filetag=', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
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

