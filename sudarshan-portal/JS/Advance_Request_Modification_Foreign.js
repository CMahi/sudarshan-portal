g_serverpath = '/Sudarshan-Portal';
var details = "";

$(document).ready(function () {

    $(".datepicker-dropdown").datepicker({ format: 'dd-M-yyyy', todayBtn: 'linked' })
    if ($("#txt_adcount").val() == "0") {
        $("#div_details").hide();
    }
    else {
        $("#div_details").show();
    }


});


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
    $("#txt_fplace").val('');
    if ($("#ddl_city_from option:selected").text() == "Other") {
        $("#txt_fplace").show();
    }
    else {
        $("#txt_fplace").hide();
    }
    // var tocity = $("#To_City").val();
    //  var desg = $("#span_designation").html();
    //  Advance_Request_Modification.fillAmountall(desg, tocity, callback_Detail);
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
        var desg = $("#span_designation").html();
        Advance_Request_Modification_Foreign.fillAmountall(desg, tocity, callback_Detail);
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
function chk_amt_other(i) {
    var tocity = $("#To_City").val();
    var desg = $("#span_designation").html();
    Advance_Request_Modification_Foreign.fillAmountall(desg, tocity, callback_Detail);
}
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;

    return true;
}
function checkamount() {
    var p_amount, total_amount = 0;
    var amt = 0;
    if ($("#txt_amount").val() != "" || $("#txt_amount").val() != undefined) {

        amt = $("#txt_amount").val();
        if ($("#total_amount").val() == "" || $("#total_amount").val() == undefined || $("#total_amount").val() == "NaN") {
            $("#total_amount").val(0);
        }

        amt = $("#txt_f_amount").val();
        lblalwdamt = $("#lbl_allowedamt").html();
        var open = $("#txt_opcount").val();
        if (open == 1) {
            if ($("#total_amount").val() != "" || $("#total_amount").val() != undefined) {
                p_amount = parseInt($("#total_amount").val()) + parseInt(amt);
            }
            if ($("#txt_adamount").val() != "" || $("#txt_adamount").val() != undefined) {
                total_amount = $("#txt_adamount").val();
            }
            if (parseInt(amt) >= parseInt(lblalwdamt)) {
                alert("Entered Amount is greater than Advance Policy Limit");

            }

            if (parseInt(p_amount) >= parseInt(total_amount)) {
                alert("Entered Amount is greater than Foreign Advance Amount");
                $('input[Id="btn_Save"]').attr('disabled', 'disabled');
                return false;
            }
            $("#txt_Curncy_amount").val('');
            $("#txt_forexcard_amount").val('');
        }
        else {
            if ($("#total_amount").val() != "" || $("#total_amount").val() != undefined) {
                p_amount = parseInt($("#total_amount").val());
            }
            if ($("#txt_adamount").val() != "" || $("#txt_adamount").val() != undefined) {
                total_amount = $("#txt_adamount").val();
            }
            if (parseInt(amt) >= parseInt(lblalwdamt)) {
                alert("Entered Amount is greater than Advance Policy Limit");

            }
            if (parseInt(p_amount) >= parseInt(total_amount)) {
                alert("Entered Amount is greater than Foreign Advance Amount");
                $('input[Id="btn_Save"]').attr('disabled', 'disabled');
                return false;
            }
            else {
                $('input[Id="btn_Save"]').removeAttr('disabled', 'disabled');
            }

            $("#txt_Curncy_amount").val('');
            $("#txt_forexcard_amount").val('');
        }

    }
}

function callback_Detail(response) {
    var amoun = response.value;
    var start = $('#txt_fdate').datepicker('getDate');
    var end = $('#txt_tdate').datepicker('getDate');
    if (start == end) {
        var days = (1) / 1000 / 60 / 60 / 24;
        var aalamt = Math.round(parseFloat(days) * parseFloat(amoun));
    }
    else {
        var days = ((end - start) / 1000 / 60 / 60 / 24) + 1;
        var aalamt = Math.round(parseFloat(days) * parseFloat(amoun));
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

        //advancr for foreign

        var xmlother = "";
        xmlother = "|ROWSET||";

        var ddlfcountry = document.getElementById("ddl_country_from").value;
        var ddlfcity = document.getElementById("ddl_city_from").value;
        var ddltcountry = document.getElementById("ddl_country_to").value;
        var ddltcity = document.getElementById("ddl_city_to").value;
        var ffromdate = document.getElementById("txt_form_Date").value;
        var ttodate = document.getElementById("txt_to_date").value;
        var rmk = document.getElementById("txt_F_remark").value;
        var amt = document.getElementById("txt_f_amount").value;
        var alwdamt = document.getElementById("lbl_allowedamt").innerHTML;
        var currency = document.getElementById("ddl_currency").value;
        var curnamt = document.getElementById("txt_Curncy_amount").value;
        var forexamt = document.getElementById("txt_forexcard_amount").value;
        var pto = document.getElementById("txt_tother").value;
        var pfrom = document.getElementById("txt_fplace").value;

        $("#txt_remark_hdr").val(rmk);

        if (ddlfcountry == "0" || ddlfcountry == "") {

            alert('Please Select Country From');
            return false;

        }
        if (ddlfcity == "0" || ddlfcity == "") {

            alert('Please Select City From');
            return false;

        }
        if (ddltcountry == "0" || ddltcountry == "") {

            alert('Please Select Country To');
            return false;

        }
        if (ddltcity == "0" || ddltcity == "") {

            alert('Please Select City To');
            return false;

        }
        if (ddlfcity == "-1") {
            if (pfrom == "") {
                alert('Please Enter City From');
                return false;
            }
        }
        if (ddltcity == "-1") {
            if (pto == "") {
                alert('Please Enter City To');
                return false;
            }
        }
        if (currency == "0" || currency == "") {

            alert('Please Select Currency');
            return false;

        }
        if (ffromdate == "") {
            alert('Please Select From Date');
            return false;
        }
        if (ttodate == "") {
            alert('Please Select To Date');
            return false;
        }

        if (amt == "") {
            alert('Please Enter Foreign Currency');
            return false;
        }
        if (curnamt == "") {
            alert('Please Enter Currency Amount');
            return false;
        }
        if (forexamt == "") {
            alert('Please Enter Forex Card Amount');
            return false;
        }
        if (rmk == "") {
            alert("Please Enter Remark");
            return false;
        }



        xmlother += "|ROW||";
        xmlother += "|FK_ADVANCE_HDR_ID||" + pk_id + "|/FK_ADVANCE_HDR_ID||";
        xmlother += "|FK_COUNTRY_FRM_ID||" + ddlfcountry + "|/FK_COUNTRY_FRM_ID||";
        xmlother += "|FK_COUNTRY_TO_ID||" + ddltcountry + "|/FK_COUNTRY_TO_ID||";
        xmlother += "|CURRENCY||" + currency + "|/CURRENCY||";
        xmlother += "|FK_CITY_FRM_ID||" + ddlfcity + "|/FK_CITY_FRM_ID||";
        xmlother += "|FK_CITY_TO_ID||" + ddltcity + "|/FK_CITY_TO_ID||";
        xmlother += "|FROM_DATE||" + ffromdate + "|/FROM_DATE||";
        xmlother += "|TO_DATE||" + ttodate + "|/TO_DATE||";
        xmlother += "|AMOUNT||" + amt + "|/AMOUNT||";
        xmlother += "|ALLOWED_AMOUNT||" + alwdamt + "|/ALLOWED_AMOUNT||";
        xmlother += "|CURRENCY_AMOUNT||" + curnamt + "|/CURRENCY_AMOUNT||";
        xmlother += "|FOREX_CARD||" + forexamt + "|/FOREX_CARD||";
        xmlother += "|PLACE_FROM_OTHER||" + pfrom + "|/PLACE_FROM_OTHER||";
        xmlother += "|PLACE_TO_OTHER||" + pto + "|/PLACE_TO_OTHER||";
        xmlother += "|/ROW||";

        xmlother += "|/ROWSET||";


        var XMLFILE = "";
        var lastRow1 = $('#uploadTable tr').length;
        XMLFILE = "<ROWSET>";
        if (lastRow1 > 1) {
            for (var l = 0; l < lastRow1 - 1; l++) {
                var newfile = $("#txtnewfile" + (l + 1)).val();
                if (newfile == "New") {
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
        }
        XMLFILE += "</ROWSET>";
        $("#txt_Document_Xml").val(XMLFILE);
        $("#txt_xml_data_vehicle").val(xmlother);
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
    var Document_Name = $("#doctype").val();
    if (Document_Name == "" || Document_Name=="0") {
        alert("Please Select File Type...!");
        return false;
    }
    if (parseInt(filelength) == 0) {
        alert("Sorry cannot upload file ! File is Empty or file does not exist");
    }
    else if (parseInt(filelength) > 20000000) {
        alert("Sorry cannot upload file ! File Size Exceeded.");
    }
    else if (fileNameSplit[1] != "pdf" && fileNameSplit[1] != "jpeg" && fileNameSplit[1] != "png" && fileNameSplit[1] != "jpg") {
        alert("Kindly Check File Type....!");
        return false;
    }
    else {
        addToClientTable(n, args.get_length());
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
    var Document_Name = $("#doctype").val();
    var tbl = $("#uploadTable");
    var lastRow = $('#uploadTable tr').length;
    var html1 = "<tr><td><input class='hidden' type='text' name='txt_Country_Add" + lastRow + "' id='txt_Document_Name" + lastRow + "' value=" + Document_Name + " readonly ></input>" + Document_Name + "</td>";
    var html2 = "<td><input class='hidden' type='text' name='txt_Region_Add" + lastRow + "' id='txt_Document_File" + lastRow + "' value=" + name + " readonly ></input><a id='a_downloadfiles" + lastRow + "' style='cursor: pointer' onclick=\"return downloadfiles('" + lastRow + "');\" >" + name + "</a></td>";
    var html3 = "<td><i id='del" + lastRow + "' class='glyphicon glyphicon-trash' align='center' onclick=\"return deletefile(" + (lastRow) + ",'');\" ><input class='hidden' type='text' id='txtnewfile" + lastRow + "' value='New' readonly ></input><input class='hidden' type='text' id='txtreqno" + lastRow + "' value='New' readonly ></input></td></tr>";
    var htmlcontent = $(html1 + "" + html2 + "" + html3);
    $('#uploadTable').append(htmlcontent);
    $("#ddl_Document").val('');
}

function downloadfiles(index) {


    var tbl = document.getElementById("uploadTable");
    var reqno = document.getElementById("txt_Request").value;

    window.open('../../Common/FileDownload.aspx?enquiryno=' + reqno + '&filename=' + tbl.rows[index].cells[1].innerText + '&filetag=', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
}
var pkid;
function deletefile(RowIndex,pkid) {
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

            $("#doctype" + (contolIndex + 1)).value = contolIndex;
            $("#doctype" + (contolIndex + 1)).id = "doctype" + contolIndex;

        }
        $('#uploadTable tr').eq(RowIndex).remove();
        deletephysicalfile(pkid);
    }
    catch (Exc) { }
}

function deletephysicalfile(pkid) {
    Advance_Request_Modification_Foreign.deletedoctbl(pkid, callback_deletefile);
}
function callback_deletefile(response) {
    if (response.value != "") {
        alert("Document Removed Successfully..");
    }
}

//*******************added by pradeep*********************************

function chk_country_From() {

    var ddlcntfrm = $("#ddl_country_from").val();
    Advance_Request_Modification_Foreign.getCountryFromWiseCityFrom(ddlcntfrm, callback_CountryFrom);

}

function callback_CountryFrom(response) {


    var data = response.value;
    var sp_data = data.split("@@");
    $("#ddl_city_from").html("");
    for (var i = 0; i < sp_data.length; i++) {
        var crec = sp_data[i].split("$$");
        var cval = crec[0];
        var ctext = crec[1];
        var ccurn = ctext.split("||");
        var ctext1 = ccurn[0];
        var ctext2 = ccurn[1];
        $("#ddl_city_from").append($("<option></option>").val(cval).html(ctext1));

    }


}

function chk_country_To() {

    var ddlcntto = $("#ddl_country_to").val();
    Advance_Request_Modification_Foreign.getCountryFromWiseCityFrom(ddlcntto, callback_CountryTo);

}

function chk_city_To() {

    var tocountry = $("#ddl_country_to").val();
    var desgid = $("#txt_designation").val();
    var currency = $("#ddl_currency").val();
    if (tocountry == 0) {
        alert("Plese Select Country First...!");
        return false;
    }
    $("#txt_tother").val("");
    if ($("#ddl_city_to option:selected").text() == "Other") {
        $("#txt_tother").show();
    }
    else {
        $("#txt_tother").hide();
    }
    Advance_Request_Modification_Foreign.fillAmountForeign(desgid, tocountry, currency, callback_Detail_Foreign);

}

function callback_CountryTo(response) {


    var data = response.value;
    var sp_data = data.split("@@");
    $("#ddl_city_to").html("");
    for (var i = 0; i < sp_data.length; i++) {
        var crec = sp_data[i].split("$$");
        var cval = crec[0];
        var ctext = crec[1];

        $("#ddl_city_to").append($("<option></option>").val(cval).html(ctext));

    }


}

$('#txt_form_Date').datepicker({ format: 'dd-M-yyyy', autoclose: true, todayBtn: 'linked' }).on('changeDate', function (ev) {
    daydiffrenceForeign();
});

$('#txt_to_date').datepicker({ format: 'dd-M-yyyy', autoclose: true, todayBtn: 'linked' }).on('changeDate', function (ev) {
    daydiffrenceForeign();
});

function daydiffrenceForeign() {
    if (document.getElementById('txt_form_Date').value == "") {
        return false;
    }
    if (document.getElementById('txt_to_date').value == "") {
        return false;
    }
    var start = $('#txt_form_Date').val();
    var stard = start.substring(0, 2);
    var starm = start.substring(3, 6);
    var stary = start.substring(7, 11);

    var end = $('#txt_to_date').val();
    var endd = end.substring(0, 2);
    var endm = end.substring(3, 6);
    var endy = end.substring(7, 11);
    var d1 = new Date(stary, getMonthFromString(starm) - 1, stard);
    var d2 = new Date(endy, getMonthFromString(endm) - 1, endd);
    if (d1 > d2) {
        alert('To Date must be greater than from Date');
        $('#txt_to_date').focus();
        $('#txt_to_date').val('');
        $("#lbl_allowedamt").html('');
        return false;
    }
    else {
        var amt = 0;
        amt = $("#txt_foreign_amt").val();
        var tocountry = $("#ddl_country_to").val();
        var desg = $("#lbl_desgnation").html();
        var desgid = $("#txt_designation").val();
        var currency = $("#ddl_currency").val();
        if (tocountry == 0) {
            alert("Plese Select Country First...!");
            return false;
        }
        Advance_Request_Modification_Foreign.fillAmountForeign(desgid, tocountry, currency, callback_Detail_Foreign);
    }
}

function callback_Detail_Foreign(response) {
    var amoun = response.value;
    var start = $('#txt_form_Date').datepicker('getDate');
    var end = $('#txt_to_date').datepicker('getDate');
    if (start == end) {
        var days = (1) / 1000 / 60 / 60 / 24;
        var aalamt = parseInt(days) * parseInt(amoun);
    }
    else {
        var days = ((end - start) / 1000 / 60 / 60 / 24) + 1;
        var aalamt = parseInt(days) * parseInt(amoun);
    }
    $("#lbl_allowedamt").html(aalamt);
}

function checkamount1() {
    var p_amount, total_amount = 0;
    var amt = 0;

    amt = $("#txt_f_amount").val();
    alwdamt = $("#lbl_allowedamt").html();
    curnamt = $("#txt_Curncy_amount").val();
    forexamt = $("#txt_forexcard_amount").val();
    if (curnamt == "") {
        curnamt = "0";
    }
    if (forexamt == "") {
        forexamt = "0";
    }

    var totalamt = parseInt(curnamt) + parseInt(forexamt);

    if (Math.round(amt) == totalamt) {

    }
    else {
        alert("Entered Currency & Forex Card Amount Does not match With Advance Amount");

        $("#txt_forexcard_amount").val('');
        return false;
    }


}


//***************************************

