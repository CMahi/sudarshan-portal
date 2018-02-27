g_serverpath = '/Sudarshan-Portal';
var details = "";
var currency_data = "";
$(document).ready(function () {
    
    var data = Foreign_Advance_Request.getCurrencyData();
    currency_data = data.value;
    $("#ddlCurrency1").html(currency_data);
    $("#advance_Foreign").show();
    $(".datepicker-dropdown").datepicker({ format: 'dd-M-yyyy', todayBtn: 'linked', startDate: new Date() })
    var user = $("#txt_Username").val();
    var desgid = $("#lbl_Grade").html();
    Foreign_Advance_Request.changetype('2', user, desgid, callback_open_detail);
});

function callback_open_detail(response) {
    if (response.value != null) {
        var arr = response.value.split(",");
        $('#txt_adamount').val(arr[0]);
        if ($('#txt_adamount').val() == "" || $('#txt_adamount').val() == "NaN") {
            $('#txt_adamount').val(0);
        }
        $('#txt_adperiod').val(arr[1]);
        $('#txt_expire').val(arr[2]);
        $('#total_amount').val(arr[3]);
        if ($('#total_amount').val() == "" || $('#total_amount').val() == "NaN") {
            $('#total_amount').val(0);
        }
        $('#txt_opcount').val(arr[4]);
        $('#txt_opencount').val(arr[5]);
        //$('#txt_pkexpenseid').val(arr[6]);
        $('#txt_openadvance').val(arr[7]);

        if (arr[9] == "False") {
            alert("You Can Not Apply Advance For Foreign Request...!");
            $("#advance_Foreign").hide();
            $('input[Id="btn_Save"]').attr('disabled', 'disabled');
        }

        var openbal = 3;
        var avlbal = openbal - arr[8];
        var avlbal1 = parseInt(avlbal) + parseInt(arr[7]);


        if (avlbal1 == "0") {
            $('input[Id="btn_Save"]').attr('disabled', 'disabled');
            alert("Your have already claimed 3 advances! Kindly settle the existing advances first to claim fresh advance...");
            $("#advance_Foreign").hide();
        }
        else {


            if ($("#txt_expire").val() == "1") {
                alert("Aging of existing advances is more than allowable limit! Unable to process New Advance Request...!");
                $('input[Id="btn_Save"]').attr('disabled', 'disabled');
            }
            else {
                $('input[Id="btn_Save"]').removeAttr('disabled', 'disabled');
            }

        }
    }
}

function chk_class_From() {
    $("#txt_fplace").val('');
    if ($("#ddl_city_from option:selected").text() == "Other") {
        $("#txt_fplace").show();
    }
    else {
        $("#txt_fplace").hide();
    }
}

function chk_class_To() {
    $("#txt_tother").val("");
    if ($("#ddl_city_to option:selected").text() == "Other") {
        $("#txt_tother").show();
    }
    else {
        $("#txt_tother").hide();
    }
    var tocity = $("#ddlToPlace").val();
    var desg = $("#lbl_Grade").html();
    //Foreign_Advance_Request.fillAmountall(desg, tocity, callback_Detail);
}

function chk_amt_other(i) {
    var tocity = $("#ddlToPlace").val();
    var desg = $("#lbl_Grade").html();
    Foreign_Advance_Request.fillAmountall(desg, tocity, callback_Detail);
}

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
        var desg = $("#lbl_Grade").html();
        if (tocity == 0) {
            alert("Plese Select City First...!");
            return false;
        }
        Foreign_Advance_Request.fillAmountall(desg, tocity, callback_Detail);
    }
}

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;

    return true;
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

function validateFloatKey(el) {
    var v = parseFloat(el.value);
    el.value = (isNaN(v)) ? '' : v.toFixed(3);
}

function createxml() {
    try {

        if ($("#txt_approvar").val() == "NA" || $("#txt_approvar").val() == "") {
            alert("Approver Not Found. Unable To Submit Request.");
            return false;
        }

        var xmlother = "";
        xmlother = "<ROWSET>";

        var ddlfcountry = $("#ddl_country_from").val();
        var ddlfcity = $("#ddl_city_from").val();
        var ddltcountry = $("#ddl_country_to").val();
        var ddltcity = $("#ddl_city_to").val();
        var ffromdate = $("#txt_form_Date").val();
        var ttodate = $("#txt_to_date").val();
        var rmk = $("#txt_foreign_remark").val();
        var amt = $("#req_base_currency").html();
        var alwdamt = $("#lbl_allowedamt").html();
        var pto = $("#txt_tother").val();
        var pfrom = $("#txt_fplace").val();

        $("#tcity").val(ddltcity);
        $("#fcity").val(ddlfcity);
        $("#txt_Deviate").val(0);
        if (parseInt(amt) > parseInt(alwdamt)) {
            var con = confirm("Request Submitted Under Deviation. Confirm?.");
            if (con == true) {
                $("#txt_Deviate").val(1);
            }
            else {
                return false;
            }
        }


        if (ddlfcountry == "0" || ddlfcountry == "") {
            alert('Please Select Source Region');
            return false;
        }
        if (ddlfcity == "0" || ddlfcity == "") {
            alert('Please Select Source City');
            return false;
        }
        if (ddltcountry == "0" || ddltcountry == "") {

            alert('Please Select Destination Region');
            return false;

        }
        if (ddltcity == "0" || ddltcity == "") {

            alert('Please Select Destination City');
            return false;

        }
        if (ddlfcity == "-1") {
            if (pfrom == "") {
                alert('Please Enter Source City');
                return false;
            }
        }
        if (ddltcity == "-1") {
            if (pto == "") {
                alert('Please Enter Destination City');
                return false;
            }
        }

        if (ffromdate == "") {
            alert('Please Select From Date');
            return false;
        }
        if (ttodate == "") {
            alert('Please Select To Date');
            return false;
        }
        if ($("#base_currency").html() == "")
        {
            alert("Base Currency Not Determined");
            return false;
        }
        if (rmk == "") {
            alert("Please Enter Remark");
            return false;
        }

        $("#pk_base_id").val($("#pk_base_currency").html());
        $("#base_curr_amt").val($("#base_currency_rate").html());

        if ($("#req_base_currency").html() == "" || $("#req_base_currency").html() == 0)
        {
            alert("Required Base Currency Should be Greater Than Zero.");
            return false;
        }

        var lastRow = $('#tbl_Data tr').length;
        var lastRow1 = $('#tbl_Data tr').length - 1;

        for (var q = 0; q < lastRow - 1; q++) {
            var fk_currency = $("#ddlCurrency" + (q + 1)).val();
            var base_currency_rate = $("#base_currency_rate").html();
            if (base_currency_rate == "" || isNaN(base_currency_rate))
            {
                base_currency_rate = 0;
            }

            var forex = $("#forex" + (q + 1)).val();
            var cash = $("#cash" + (q + 1)).val();
            var exc_in_inr = $("#spn_inr" + (q + 1)).html();
	
            if(cash=="" || cash==undefined || isNaN(cash))
            {
		cash=0;
	    }
            if(forex=="" || forex==undefined || isNaN(forex))
            {
		forex=0;
	    }
           if(exc_in_inr=="" || exc_in_inr==undefined || isNaN(exc_in_inr))
            {
		exc_in_inr=0;
	    }

            var eq_forex = 0;
            var eq_cash = 0;
            
            if (base_currency_rate == 0) {
                eq_forex = 0;
                eq_cash = 0;
            }
            else {
                eq_forex = parseInt(parseFloat(forex) * parseFloat(exc_in_inr) / parseFloat(base_currency_rate));
                eq_cash = parseInt(parseFloat(cash) * parseFloat(exc_in_inr) / parseFloat(base_currency_rate));
            }
            var forex_inr = parseInt(eq_forex * base_currency_rate);
            var cash_inr = parseInt(eq_cash * base_currency_rate);
            
	    if(forex_inr=="" || forex_inr==undefined || isNaN(forex_inr))
            {
		exc_in_inr=0;
	    }
            if(cash_inr=="" || cash_inr==undefined || isNaN(cash_inr))
            {
		cash_inr=0;
	    }
	    
            //if (eq_cash > 0) {
                xmlother += "<ROW>";
                xmlother += "<FK_FTA_HDR_ID>#</FK_FTA_HDR_ID>";
                xmlother += "<FK_CURRENCY>" + fk_currency + "</FK_CURRENCY>";
                xmlother += "<CURRENCY_MODE>CASH</CURRENCY_MODE>";
                xmlother += "<CURRENCY_AMOUNT>" + cash + "</CURRENCY_AMOUNT>";
                xmlother += "<EXC_RATE_IN_INR>" + exc_in_inr + "</EXC_RATE_IN_INR>";
                xmlother += "<EQ_BASE_AMOUNT>" + eq_cash + "</EQ_BASE_AMOUNT>";
                xmlother += "<EQ_INR_AMOUNT>" + cash_inr + "</EQ_INR_AMOUNT>";
                xmlother += "<SERV_CHARGES>0</SERV_CHARGES>";
                xmlother += "<TOTAL_AMOUNT>" + cash_inr + "</TOTAL_AMOUNT>";
                xmlother += "</ROW>";
            //}
            //if (eq_forex > 0) {
                xmlother += "<ROW>";
                xmlother += "<FK_FTA_HDR_ID>#</FK_FTA_HDR_ID>";
                xmlother += "<FK_CURRENCY>" + fk_currency + "</FK_CURRENCY>";
                xmlother += "<CURRENCY_MODE>FOREX CARD</CURRENCY_MODE>";
                xmlother += "<CURRENCY_AMOUNT>" + forex + "</CURRENCY_AMOUNT>";
                xmlother += "<EXC_RATE_IN_INR>" + exc_in_inr + "</EXC_RATE_IN_INR>";
                xmlother += "<EQ_BASE_AMOUNT>" + eq_forex + "</EQ_BASE_AMOUNT>";
                xmlother += "<EQ_INR_AMOUNT>" + forex_inr + "</EQ_INR_AMOUNT>";
                xmlother += "<SERV_CHARGES>0</SERV_CHARGES>";
                xmlother += "<TOTAL_AMOUNT>" + forex_inr + "</TOTAL_AMOUNT>";
                xmlother += "</ROW>";
            //}
            
        }
        xmlother += "</ROWSET>";
        var XMLFILE = "";
        var lastRow1 = $('#uploadTable tr').length;
        XMLFILE = "<ROWSET>";
        if (lastRow1 == 1) {
            alert('Please Upload Supporting Document');
            return false;
        }
        var passportcnt = 0;
        var airticketcnt = 0;
        var reqletcnt = 0;
        if (lastRow1 > 1) {
            for (var l = 0; l < lastRow1 - 1; l++) {
                var firstCol = $("#uploadTable tr")[l + 1].cells[0].innerText;
                var SecondCol = $("#uploadTable tr")[l + 1].cells[1].innerText;
                if (firstCol == 'Passport') {
                    passportcnt++;
                }
                if (firstCol == 'Air Ticket') {
                    airticketcnt++;
                }
                if (firstCol == 'Request Letter') {
                    reqletcnt++;
                }


                XMLFILE += "<ROW>";
                XMLFILE += "<OBJECT_TYPE>ADVANCE</OBJECT_TYPE>";
                XMLFILE += "<OBJECT_VALUE>#</OBJECT_VALUE>";
                XMLFILE += "<DOCUMENT_TYPE>" + firstCol + "</DOCUMENT_TYPE>";
                XMLFILE += "<FILENAME>" + SecondCol + "</FILENAME>";
                XMLFILE += "</ROW>";
            }

            //if (passportcnt == 0) {
            //    alert('Please Upload Passport Document');
            //    return false;
            //}
            //if (airticketcnt == 0) {
            //    alert('Please Upload Air Ticket Document');
            //    return false;
            //}
            if (reqletcnt == 0) {
                alert('Please Upload Request Letter Document');
                return false;
            }

        }
        XMLFILE += "</ROWSET>";
        $("#txt_Document_Xml").val(XMLFILE);
        $("#txt_xml_data_vehicle").val(xmlother);
        $("#divIns").show();
        //alert(XMLFILE);
        //alert(xmlother);
        return true;
    }
    catch (exception) {
        alert(exception);
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
    //var Document_Name = $("#txt_description").val();
    var fileExt = '.' + filename.split('.').pop();
    var Document_Name = $("#doctype").val();
    if (Document_Name == "0" || Document_Name == "") {
        alert("Please Select File Type...!");
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
        var cnt = 0;
        if (fileExt.toUpperCase() == ".JPEG" || fileExt.toUpperCase() == ".JPG" || fileExt.toUpperCase() == ".PNG" || fileExt.toUpperCase() == ".PDF") {
            var tbl = $("#uploadTable");
            var lastRow = $('#uploadTable tr').length;
            for (var i = 0; i < lastRow - 1; i++) {
                if ($("#uploadTable tr")[i + 1].cells[1].innerText == n) {
                    cnt = 1;
                }
            }
            if (cnt == 0) {
                addToClientTable(n, args.get_length());
            }
            else {
                alert("File with same name already exists,so please file name rename");
                return false;
            }
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
    var Document_Name = $("#doctype").val();
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

    var tbl = document.getElementById("uploadTable");

    window.open('../../Common/FileDownload.aspx?enquiryno=NA&filename=' + tbl.rows[index].cells[1].innerText + '&filetag=', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
}

function deletefile(RowIndex) {
    try {
        var lastRow = $('#uploadTable tr').length;
        //var filename = $('#uploadTable tr')[RowIndex].cells[1].innerText;

        if (lastRow <= 1)
            return false;
        for (var contolIndex = RowIndex; contolIndex <= lastRow - 1; contolIndex++) {
            if (contolIndex == RowIndex)
            {
                $('#uploadTable tr').eq(RowIndex).remove();
            }
            $("#del" + (contolIndex + 1)).onclick = new Function("deletefile(" + contolIndex + ")");
            $("#del" + (contolIndex + 1)).id = "del" + contolIndex;
            $("#a_downloadfiles" + (contolIndex + 1)).onclick = new Function("downloadfiles(" + contolIndex + ")");
            $("#a_downloadfiles" + (contolIndex + 1)).id = "a_downloadfiles" + contolIndex;

            $("#txt_Document_Name" + (contolIndex + 1)).value = contolIndex;
            $("#txt_Document_Name" + (contolIndex + 1)).id = "txt_Document_Name" + contolIndex;

        }
        
    }
    catch (Exc) { }
}

function chk_country_From() {

    var ddlcntfrm = $("#ddl_country_from").val();
    $("#txt_fplace").hide();
    Foreign_Advance_Request.getCountryFromWiseCityFrom(ddlcntfrm, callback_CountryFrom);

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
    $("#txt_tother").hide();
    Foreign_Advance_Request.getCountryFromWiseCityFrom(ddlcntto, callback_CountryTo);
}

function chk_city_To() {

    var tocountry = $("#ddl_country_to").val();
    var desgid = $("#lbl_Grade").html();
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
    Foreign_Advance_Request.fillAmountForeign(desgid, tocountry, currency, callback_Detail_Foreign);

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
    if ($("#ddl_country_to option:selected").index() > 0) {
        var pk_id = $("#ddl_country_to").val();
        var currency = Foreign_Advance_Request.getCountryCurrency(pk_id);
        var data = currency.value.split("@@");
        $("#base_currency").html(data[0]);
        $("#base_currency_rate").html(data[1]);
        $("#pk_base_currency").html(data[2]);
    }
    else {
        $("#base_currency").html("");
        $("#base_currency_rate").html(0.00);
        $("#pk_base_currency").html("");
    }
    daydiffrenceForeign();
    //calculate_Total();
    var lastRow = $('#tbl_Data tr').length;
    var lastRow1 = $('#tbl_Data tr').length - 1;
    for (var q = 0; q < lastRow - 1; q++) {
        change_Currency(q + 1);
    }
}

$('#txt_form_Date').datepicker({ format: 'dd-M-yyyy', autoclose: true, todayBtn: 'linked', startDate: new Date() }).on('changeDate', function (ev) {
    daydiffrenceForeign();
});

$('#txt_to_date').datepicker({ format: 'dd-M-yyyy', autoclose: true, todayBtn: 'linked', startDate: new Date() }).on('changeDate', function (ev) {
    daydiffrenceForeign();
});

function daydiffrenceForeign() {
    if ($('#txt_form_Date').value == "") {
        $("#lbl_allowedamt").html(0);
        return false;
    }
    if ($('#txt_to_date').value == "") {
        $("#lbl_allowedamt").html(0);
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
        $("#lbl_allowedamt").html(0);
        return false;
    }
    else {
        var amt = 0;
        amt = $("#txt_foreign_amt").val();
        var tocountry = $("#ddl_country_to").val();
        var desgid = $("#lbl_Grade").html();
        var currency = $("#ddl_currency").val();
        var division = $("#lbl_division").html();
        if (tocountry == 0) {
            $("#lbl_allowedamt").html(0);
            alert("Plese Select Country First...!");
            return false;
        }
        else {
            Foreign_Advance_Request.fillAmountForeign(desgid, tocountry, currency, division, callback_Detail_Foreign);
        }
        
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

function addnewrow() {

    var lastRow = $('#tbl_Data tr').length;
    var lastRow1 = $('#tbl_Data tr').length - 1;

    for (var q = 0; q < lastRow - 1; q++) {
        var forex = $("#forex" + (q + 1)).val();
        var cash = $("#cash" + (q + 1)).val();

        if ($("#ddlCurrency" + (q + 1) + " option:selected").index() < 1) {
            alert('Please Select Currency At Row :' + (q + 1) + '');
            return false;
        }
        if (forex == "") {
            alert('Please Enter Currency in Forex Card At Row :' + (q + 1) + '');
            return false;
        }
        if (cash == "") {
            alert('Please Enter Currency in Cash Mode At Row :' + (q + 1) + '');
            return false;
        }

        $("#add" + (q + 1)).hide();
        $("#rem" + (q + 1)).show();
    }

    var html = "<tr><td><span id='index" + lastRow + "'>" + lastRow + "</span></td>";
    html += "<td><select ID='ddlCurrency" + lastRow + "' runat='server' class='form-control input-sm' onchange='change_Currency(" + lastRow + ")'>"+currency_data+"</select><span id='spn_inr" + lastRow + "' style='display:none'>0</span></td>";
    html += "<td><input class='form-control input-sm' type='text' id='forex" + lastRow + "' value='' style='text-align:right' onkeyup='calculate_Total()' onchange='validateFloatKey(this);'></input></td>";
    html += "<td><input class='form-control input-sm' type='text' id='cash" + lastRow + "' value='' style='text-align:right' onkeyup='calculate_Total()' onchange='validateFloatKey(this);'></input></td>";
    html += "<td style='text-align:right'><span id='total_cash" + lastRow + "'>0</span></td>";
    html += "<td style='text-align:right'><span id='eq_curr" + lastRow + "'>0</span></td>";
    html += "<td id='add" + lastRow + "'><a id='add_row" + lastRow + "' onclick='addnewrow()'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i></a></td>";
    html += "<td id='rem" + lastRow + "' style='display:none'><a id='del_row" + lastRow + "' onclick='delete_row(" + lastRow + ")'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-trash'></i></a></td></tr>";

    var htmlcontent = $(html);
    $('#tbl_Data').append(htmlcontent);
}

function delete_row(RowIndex) {
    var tbl = document.getElementById("tbl_Data");
    var lastRow = tbl.rows.length;
    if (lastRow <= 2) {
        alert("You have to Enter atleast one record..!");
        return false;
    }
    for (var contolIndex = RowIndex; contolIndex < lastRow - 1; contolIndex++) {
        if (contolIndex == RowIndex) {
            tbl.deleteRow(RowIndex);
        }

        document.getElementById("index" + (parseInt(contolIndex) + 1)).id = "index" + contolIndex;
        $("#index" + (parseInt(contolIndex))).html(contolIndex);
        document.getElementById("ddlCurrency" + (parseInt(contolIndex) + 1)).id = "ddlCurrency" + contolIndex;
        //document.getElementById("ddlCurrency" + (parseInt(contolIndex) + 1)).onchange = new Function("change_Currency(" + contolIndex + ")");
        $("#ddlCurrency" + (parseInt(contolIndex) + 1)).on('change', 'select', function () { change_Currency(contolIndex) });
        document.getElementById("forex" + (parseInt(contolIndex) + 1)).id = "forex" + contolIndex;
        document.getElementById("cash" + (parseInt(contolIndex) + 1)).id = "cash" + contolIndex;
        document.getElementById("total_cash" + (parseInt(contolIndex) + 1)).id = "total_cash" + contolIndex;
        document.getElementById("eq_curr" + (parseInt(contolIndex) + 1)).id = "eq_curr" + contolIndex;
        document.getElementById("spn_inr" + (parseInt(contolIndex) + 1)).id = "spn_inr" + contolIndex;
        document.getElementById("add_row" + (parseInt(contolIndex) + 1)).onclick = new Function("addnewrow()");
        document.getElementById("add_row" + (parseInt(contolIndex) + 1)).id = "add_row" + contolIndex;
        document.getElementById("del_row" + (parseInt(contolIndex) + 1)).onclick = new Function("delete_row(" + contolIndex + ")");
        document.getElementById("del_row" + (parseInt(contolIndex) + 1)).id = "del_row" + contolIndex;
        document.getElementById("add" + (parseInt(contolIndex) + 1)).id = "add" + contolIndex;
        document.getElementById("rem" + (parseInt(contolIndex) + 1)).id = "rem" + contolIndex;
        calculate_Total();
    }
}

function calculate_Total() {
    var lastRow = $('#tbl_Data tr').length;
    var lastRow1 = $('#tbl_Data tr').length - 1;
    var tot_amount = 0;
    for (var q = 0; q < lastRow - 1; q++) {
        var forex = $("#forex" + (q + 1)).val();
        var cash = $("#cash" + (q + 1)).val();
        var row_rate = $("#spn_inr" + (q + 1)).html();
        var spn_Exc_Rate = $("#base_currency_rate").html();
        if (isNaN(forex) || forex == "") {
            //$("#forex" + (q + 1)).val(0);
            forex = 0;
        }
        if (isNaN(cash) || cash == "") {
            //$("#cash" + (q + 1)).val(0);
            cash = 0;
        }
        if (isNaN(row_rate)) {
            row_rate = 0;
        }
        if (isNaN(spn_Exc_Rate)) {
            spn_Exc_Rate = 0;
        }
        var tot = parseFloat(forex) + parseFloat(cash);
        var eq_curr = 0;
        if (parseFloat(tot) > 0 && parseFloat(row_rate) > 0 && parseFloat(spn_Exc_Rate) > 0) {
            eq_curr = parseInt((parseFloat(tot) * parseFloat(row_rate)) / parseFloat(spn_Exc_Rate));
        }

        $("#total_cash" + (q + 1)).html(tot);
        $("#eq_curr" + (q + 1)).html(eq_curr);
        tot_amount = tot_amount + eq_curr;
    }
    $("#req_base_currency").html(tot_amount);

}

function change_Currency(index) {
    
    if ($("#base_currency").html() == "INR" && $("#ddlCurrency" + index + " option:selected").text() != "INR") {
        //$("#ddlCurrency" + index + " option:selected").val(0);
        $("#ddlCurrency" + index).prop('selectedIndex', 0);
        $("#forex" + index).val("");
        $("#cash" + index).val("");
        $("#total_cash" + index).html(0);
        $("#eq_curr" + index).html(0);
        $("#spn_inr" + index).html(0);
        calculate_Total();
        $("#forex" + index).attr("disabled", true);
        alert("Select Only INR Currency");
        return false;
    }
    else {
        $("#forex" + index).attr("disabled", false);
        var spn_curr = $("#ddlCurrency" + index + " option:selected").val();
        var spn_inr = Foreign_Advance_Request.getCurrencyRate(spn_curr);
        $("#spn_inr" + index).html(spn_inr.value);
        calculate_Total();
    }
    
}



