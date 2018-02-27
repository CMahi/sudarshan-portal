var Dollar_Currecny = "";
var tab_Counter_id = "";

$(document).ready(function () {
    $(".datepicker-dropdown").datepicker({ format: 'dd-M-yyyy', todayBtn: 'linked', autoclose: true, endDate: new Date() })
    var fromDate = $("#travelFromDate").val();
    var toDate = $("#travelToDate").val();
    getSelectDate(fromDate, toDate);
    allowOnlyNumbers();

});

$('#travelFromDate').datepicker({ format: 'dd-M-yyyy', autoclose: true, todayBtn: 'linked' }).on('changeDate', function (ev) {
    chk_FromDate();
});

$('#travelToDate').datepicker({ format: 'dd-M-yyyy', autoclose: true, todayBtn: 'linked' }).on('changeDate', function (ev) {
    chk_ToDate();
});

function allowOnlyNumbers() {

    $(".numbersOnly").on("keypress keyup blur", function (event) {
        $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
        if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
            event.preventDefault();
        }
    });
}

function check_PreDate(fromDate, toDate) {
    //var chk_dates = Domestic_Travel_Request.check_Dates(fromDate, toDate);
    var chk_dates = "";
    if (chk_dates.value == "1") {
        alert("Expense Already Claimed Between From and To Dates");
        return false;
    }
    else {
        var firstDate = new Date(fromDate);
        var secondDate = new Date(toDate);
        if (firstDate > secondDate) {
            alert("From Date Should Not Be Greater Than To Date...!");
            return false;
        }
    }
}

function getCity(row_index) {
    var pk_city = $("#ddlCity" + row_index).val();
    $("#other_f_city" + row_index).val("");
    if (pk_city == -1) {
        $("#other_f_city" + row_index).show();
    }
    else {
        $("#other_f_city" + row_index).hide();
    }

    if ($("#ddlCountry").val() == "3") {
        var rowIndex = 0;
        rowIndex = document.getElementById("tab_Exp" + (row_index) + "").rows.length;
        for (var j = 0; j < rowIndex - 1; j++) {
            if (j > 1) {
                get_Limit(row_index, (j + 1));
            }
        }
    }
}

function chk_FromDate() {
    var fromDate = $("#travelFromDate").val();
    var toDate = $("#travelToDate").val();
    if (fromDate == "" && toDate != "") {
        alert("Please Select Travel From Date");
        $("#travelToDate").val("");
        return false;
    }
    else if (fromDate != "" && toDate == "") {
        $("#travelToDate").val("");
        return false;
    }
    else {
        if (check_PreDate(fromDate, toDate) != false) {
            getSelectDate(fromDate, toDate);
        }
        else {
            $("#travelFromDate").val("");
        }
    }

}

function chk_ToDate() {
    var fromDate = $("#travelFromDate").val();
    var toDate = $("#travelToDate").val();
    if (fromDate == "" && toDate != "") {
        alert("Please Select Travel From Date");
        $("#travelToDate").val("");
        $("#travelToDate").blur();
        $("#travelFromDate").focus();
        return false;
    }
    else if (fromDate != "" && toDate == "") {
        alert("Please Select Travel To Date");
        $("#travelToDate").val("");
        $("#travelFromDate").blur();
        $("#travelToDate").focus();
        return false;
    }
    else {
        if (check_PreDate(fromDate, toDate) != false) {
            getSelectDate(fromDate, toDate);
        }
        else {
            $("#travelToDate").val("");
        }
    }
}

function SelectData() {
    var fromDate = $("#travelFromDate").val();
    var toDate = $("#travelToDate").val();
    getSelectDate(fromDate, toDate);
}

function getSelectDate(fromDate, toDate) {
    try {
        if (fromDate != "" && toDate != "") {
            var check_FromDate = check_TODate = "";
            check_FromDate = fromDate;
            check_TODate = toDate;
            var fdate = getFormatedDate1(fromDate);
            var tdate = getFormatedDate1(toDate);

            var firstDate = new Date(fdate);
            var secondDate = new Date(tdate);
            var base_currency = 'USD';
            var rws = $("[name='travel']");
            for (var index = 1; index <= rws.length; index++) {
                if ($("#radio" + index).is(':checked')) {
                    base_currency = $("#currency_adv" + index).val();
                }
            }
            if ($("#ddlCountry").val() != 0 && $("#ddlCountry").val() != undefined) {
                var ofdate = $("#ofdate").val();
                var otdate = $("#otdate").val();
                var wiid = $("#txtWIID").val();
                var country_id = $("#ddlCountry").val();
                var exc_rate = $("#exch_rate").val();
                Foreign_Travel_Request_Modification.getDataRows(fromDate, toDate, country_id, ofdate, otdate, wiid, $("#span_grade").text(), $("#txt_Class").val(), exc_rate, base_currency, callback_DataRows);

            }
            else {
                $("#accordion").html("");
            }
        }
    }
    catch (ex) {

    }
}

function check_Exist(fromDate, toDate) {
    Foreign_Travel_Request_Modification.check_exist(fromDate, toDate, $("#txt_Username").val(), $("#ddlCountry").val(), callback_Exist);
}

function callback_Exist(response) {
    if (response.value == "True") {
        alert("Travel Request is already given by this Date!");
        $("#travelFromDate").val("");
        return false
    }
}

function callback_DataRows(response) {
    $("#accordion").html(response.value);

    var adv = $("[name='travel']");
    var chk = 0;
    for (var a = 0; a < adv.length; a++) {
        if ($("#radio" + (a + 1)).is(':checked')) {

            change_radio(a + 1);
        }
    }
    allowOnlyNumbers();
    //calculate_Amount();
}

function getFormatedDate(date) {
    var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    return (date.getDate() + "-" + months[date.getMonth()] + "-" + date.getFullYear());
}

function getFormatedDate1(date) {
    var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    var strDate = date.split("-");
    var mn = 0;
    for (var i = 1; i <= months.length; i++) {
        if (strDate[1] == months[i - 1]) {
            mn = i;
        }
    }
    return (mn + "/" + strDate[0] + "/" + strDate[2]);
}

function deletephysicalfile(filename) {
    ContractualInstruction.DeleteFile(filename, callback_deletefile);
}

function callback_deletefile(response) {
    if (response.value != "") {
        alert("Document Removed Successfully..");
    }
}

function enable_disable_field(tab_id, Row_id) {
    if ($("#ddl_SUP_" + tab_id + "_" + Row_id + "").val() == "N") {
        $("#particular_SUP_" + tab_id + "_" + Row_id + "").hide();
        $("#f_other_" + tab_id + "_" + Row_id + "").hide();
    }
    else {
        $("#particular_SUP_" + tab_id + "_" + Row_id + "").show();
        $("#f_other_" + tab_id + "_" + Row_id + "").show();
    }
}

function prepare_data() {
    var XMLFILE = "<ROWSET>";
    var xml_dtl = "<ROWSET>";
    var xml_exp = "<ROWSET>";
    var Local_travel = "<ROWSET>";
    var rowIndex = Loc_rowIndex = 0;
    try {
        if (validate_Controls() == true) {
            var rws = $("[name='rs']");
            for (var i = 1; i <= rws.length; i++) {
                var firstDate = $("#spn_date" + i).html();
                fdate = getFormatedDate1(firstDate);
                xml_dtl += "<ROW>";
                xml_dtl += "<fk_FOREIGN_expense_Hdr_Id>#</fk_FOREIGN_expense_Hdr_Id>";
                xml_dtl += "<travel_date>" + fdate + "</travel_date>";
                xml_dtl += "<FK_CITY>" + $("#ddlCity" + i).val() + "</FK_CITY>";
                xml_dtl += "<TRAVEL_CLASS>" + $("#ddlTravel_class" + i).val() + "</TRAVEL_CLASS>";

                if ($("#hotel_name" + i).val() == undefined) {
                    xml_dtl += "<hotel_name></hotel_name>";
                }
                else {
                    xml_dtl += "<hotel_name>" + $("#hotel_name" + i).val() + "</hotel_name>";
                }
                if ($("#contact" + i).val() == undefined) {
                    xml_dtl += "<hotel_no></hotel_no>";
                }
                else {
                    xml_dtl += "<hotel_no>" + $("#contact" + i).val() + "</hotel_no>";
                }

                xml_dtl += "<Other_City>" + $("#other_f_city" + i).val() + "</Other_City>";
                xml_dtl += "<remark_note>" + $("#txt_Particulars" + i).val() + "</remark_note>";
                xml_dtl += "</ROW>";

                var rowIndex = Loc_rowIndex = 0;
                rowIndex = document.getElementById("tab_Exp" + (i) + "").rows.length;
                for (var eh = 1; eh <= rowIndex - 1; eh++) {
                    var e_name = $("#expense_id" + i + "_" + eh).find("option:selected").text();
                    if ($("#expense_id" + i + "_" + eh).val() != undefined && $("#expense_id" + i + "_" + eh).val() != "0") {
                        xml_exp += "<ROW>";
                        xml_exp += "<fk_FOREIGN_expense_Hdr_Id>#</fk_FOREIGN_expense_Hdr_Id>";
                        xml_exp += "<fk_expense_head_id>" + $("#expense_id" + i + "_" + eh).val() + "</fk_expense_head_id>";
                        var exp_amt = $("#txt_Expense_Amt" + i + "_" + eh).val();
                        if (exp_amt != undefined) {
                            xml_exp += "<COUNTRY_CURRENCY>" + exp_amt + "</COUNTRY_CURRENCY>";
                        }
                        else {
                            xml_exp += "<COUNTRY_CURRENCY>" + 0 + "</COUNTRY_CURRENCY>";
                        }
                        var exc_rate = $("#cur_" + i + "_" + eh).val();
                        if (exc_rate != undefined) {
                            xml_exp += "<EXC_RATE>" + exc_rate + "</EXC_RATE>";
                        }
                        else {
                            xml_exp += "<EXC_RATE>" + 0 + "</EXC_RATE>";
                        }

                        if (($("#ddlrem_" + i + "_" + eh).find("option:selected").text() == "Corporate Credit Card") || ($("#ddlrem_" + i + "_" + eh).find("option:selected").text() == "Personal Credit Card")) {
                            var rupees = $("#rupees_txt" + i + "_" + eh).val();
                            if (rupees != undefined) {
                                xml_exp += "<INDIAN_CURRENCY>" + rupees + "</INDIAN_CURRENCY>";
                            }
                            else {
                                xml_exp += "<INDIAN_CURRENCY>" + 0 + "</INDIAN_CURRENCY>";
                            }
                            var dollar = $("#dollar_txt" + i + "_" + eh).val();
                            if (dollar != undefined) {
                                xml_exp += "<DOLLAR_CURRENCY>" + dollar + "</DOLLAR_CURRENCY>";
                            }
                            else {
                                xml_exp += "<DOLLAR_CURRENCY>" + 0 + "</DOLLAR_CURRENCY>";
                            }
                        }
                        else {
                            var rupees = $("#rupees_" + i + "_" + eh).html();
                            if (rupees != "") {
                                xml_exp += "<INDIAN_CURRENCY>" + rupees + "</INDIAN_CURRENCY>";
                            }
                            else {
                                xml_exp += "<INDIAN_CURRENCY>" + 0 + "</INDIAN_CURRENCY>";
                            }
                            var dollar = $("#dollar_" + i + "_" + eh).html();
                            if (dollar != "") {
                                xml_exp += "<DOLLAR_CURRENCY>" + dollar + "</DOLLAR_CURRENCY>";
                            }
                            else {
                                xml_exp += "<DOLLAR_CURRENCY>" + 0 + "</DOLLAR_CURRENCY>";
                            }
                        }
                        var IS_CR = $("#ddlrem_" + i + "_" + eh).val();
                        if (IS_CR != 0 && IS_CR != undefined && IS_CR != "") {
                            xml_exp += "<REIM_TYPE>" + IS_CR + "</REIM_TYPE>";
                        }
                        else {
                            xml_exp += "<REIM_TYPE>" + 0 + "</REIM_TYPE>";
                        }
                        var IS_SUP = $("#IS_SUP" + i + "" + eh).html();
                        if (IS_SUP != "" && IS_SUP != undefined) {
                            if (IS_SUP == "Y") {
                                xml_exp += "<IS_SUPPORT>" + 1 + "</IS_SUPPORT>";
                            }
                            else {
                                xml_exp += "<IS_SUPPORT>" + 0 + "</IS_SUPPORT>";
                            }
                        }
                        else {
                            xml_exp += "<IS_SUPPORT>" + 0 + "</IS_SUPPORT>";
                        }

                        if (eh != 2) {
                            var SUPPORT_DOC = $("#ddl_SUP_" + i + "_" + eh).val();
                            if (SUPPORT_DOC != "") {
                                xml_exp += "<SUPPORT_DOC>" + SUPPORT_DOC + "</SUPPORT_DOC>";
                            }
                            else {
                                xml_exp += "<SUPPORT_DOC>N</SUPPORT_DOC>";
                            }
                        }
                        else {
                            xml_exp += "<SUPPORT_DOC>N</SUPPORT_DOC>";
                        }

                        var particular_SUP = $("#particular_SUP_" + i + "_" + eh).val();
                        xml_exp += "<SUPPORTING_REMARK>" + particular_SUP + "</SUPPORTING_REMARK>";
                        var OTHER_REMARK = $("#other_remark_" + i + "_" + eh).val();
                        xml_exp += "<OTHER_REMARK>" + OTHER_REMARK + "</OTHER_REMARK>";
                        xml_exp += "<fk_travel_date>" + fdate + "</fk_travel_date>";
                        if (e_name != "Incidental") {
                            xml_exp += "<TAX>" + $("#Tax_" + i + "_" + eh).val() + "</TAX>";
                        }
                        xml_exp += "</ROW>";
                    }
                }
                var xml_Local_data = getLocalRows(i, fdate);
                //alert(xml_Local_data);
                xml_exp += xml_Local_data;
                //Local Travel XML
                var Loc_rowIndex = document.getElementById("tbl_Local_Travel" + (i) + "").rows.length;
                for (var j = 0; j < Loc_rowIndex - 1; j++) {
                    if ($("#txt_Date" + (i) + "" + (j + 1) + "").val().trim() != "" && $("#txt_From" + (i) + "" + (j + 1) + "").val().trim() != "") {
                        //if ($("#txt_Date" + (i) + "" + (j + 1) + "").val().trim() == "") {
                        //    alert("Please Select Date at Row" + (j + 1) + ".!");
                        //    document.getElementById("txt_Date" + (i) + "" + (j + 1) + "").focus();
                        //    return false;
                        //}
                        
                        if ($("#txt_ReimType" + (i) + "_" + (j + 1) + " option:selected").index() < 1) {
                            alert("Please Select Reimbursement Type For Conveyance on "+firstDate+" at Row No : " + (j + 1) + ".!");
                            document.getElementById("txt_ReimType" + (i) + "_" + (j + 1) + "").focus();
                            return false;
                        }
                        else if ($("#txt_From" + (i) + "" + (j + 1) + "").val().trim() == "") {
                            alert("Please Enter Route Travelled From on " + firstDate + " at Row No : " + (j + 1) + ".!");
                            document.getElementById("txt_Travel_Mode" + (i) + "" + (j + 1) + "").focus();
                            return false;
                        }
                        else if ($("#txt_To" + (i) + "" + (j + 1) + "").val().trim() == "") {
                            alert("Please Enter Route Travelled To on " + firstDate + " at Row No : " + (j + 1) + ".!");
                            document.getElementById("txt_To" + (i) + "" + (j + 1) + "").focus();
                            return false;
                        }
                        else if ($("#txt_Travel_Mode" + (i) + "" + (j + 1) + "").val().trim() == "") {
                            alert("Please Enter Mode of Travel at on " + firstDate + " at Row No : " + (j + 1) + ".!");
                            document.getElementById("txt_Travel_Mode" + (i) + "" + (j + 1) + "").focus();
                            return false;
                        }
                        else if ($("#txt_Expenses" + (i) + "" + (j + 1) + "").val().trim() == "") {
                            if ($("#txt_ReimType" + (i) + "_" + (j + 1) + "").val().trim() != "Corporate Credit Card" && $("#txt_ReimType" + (i) + "_" + (j + 1) + "").val().trim() != "Personal Credit Card") {
                                alert("Please Enter Expense amount in currency spent on " + firstDate + " at Row No : " + (j + 1) + ".!");
                                document.getElementById("txt_Expenses" + (i) + "" + (j + 1) + "").focus();
                                return false;
                            }
                        }
                        else if ($("#txt_Bill" + (i) + "" + (j + 1) + "").val().trim() == "0") {
                            alert("Please Select Bills attached Yes/No on " + firstDate + " at Row No : " + (j + 1) + ".!");
                            document.getElementById("txt_Bill" + (i) + "" + (j + 1) + "").focus();
                            return false;
                        }
                    }

                    Local_travel += "<ROW>";
                    Local_travel += "<FK_HDR_ID>" + $("#txt_pk_id").val() + "</FK_HDR_ID>";
                    Local_travel += "<TRAVEL_DATE>" + $("#txt_Date" + (i) + "" + (j + 1) + "").val().trim() + "</TRAVEL_DATE>";
                    Local_travel += "<TRAVEL_FROM>" + $("#txt_From" + (i) + "" + (j + 1) + "").val().trim() + "</TRAVEL_FROM>";
                    Local_travel += "<TRAVEL_TO>" + $("#txt_To" + (i) + "" + (j + 1) + "").val().trim() + "</TRAVEL_TO>";
                    Local_travel += "<TRAVEL_MODE>" + $("#txt_Travel_Mode" + (i) + "" + (j + 1) + "").val().trim() + "</TRAVEL_MODE>";
                    if ($("#txt_Expenses" + (i) + "" + (j + 1) + "").val().trim() != "") {
                        Local_travel += "<EXPENSES>" + $("#txt_Expenses" + (i) + "" + (j + 1) + "").val().trim() + "</EXPENSES>";
                    }
                    else {
                        Local_travel += "<EXPENSES>0</EXPENSES>";
                    }
                    if ($("#txt_L_Tax" + (i) + "" + (j + 1) + "").val().trim() != "") {
                        Local_travel += "<TAX>" + $("#txt_L_Tax" + (i) + "" + (j + 1) + "").val().trim() + "</TAX>";
                    }
                    else {
                        Local_travel += "<TAX>0</TAX>";
                    }

                    Local_travel += "<REMARK>" + $("#txt_Remark" + (i) + "" + (j + 1) + "").val().trim() + "</REMARK>";
                    Local_travel += "<BILL_STATUS>" + $("#txt_Bill" + (i) + "" + (j + 1) + "").val().trim() + "</BILL_STATUS>";
                    Local_travel += "<FK_TRAVEL_DATE>" + fdate + "</FK_TRAVEL_DATE>";
                    /****************************************************************************************/
                    if ($("#txt_ReimType" + (i) + "_" + (j + 1) + "").val().trim() != "") {
                        Local_travel += "<REIM_TYPE>" + $("#txt_ReimType" + (i) + "_" + (j + 1) + "").val().trim() + "</REIM_TYPE>";
                    }
                    else {
                        Local_travel += "<REIM_TYPE>0</REIM_TYPE>";
                    }

                    if ($("#txt_L_Rate" + (i) + "" + (j + 1) + "").val().trim() != "" && !isNaN($("#txt_L_Rate" + (i) + "" + (j + 1) + "").val())) {
                        Local_travel += "<CONVERSION_RATE>" + $("#txt_L_Rate" + (i) + "" + (j + 1) + "").val().trim() + "</CONVERSION_RATE>";
                    }
                    else {
                        Local_travel += "<CONVERSION_RATE>0</CONVERSION_RATE>";
                    }

                    if ($("#txt_ReimType" + (i) + "_" + (j + 1) + " option:selected").text() == "Corporate Credit Card" || $("#txt_ReimType" + (i) + "_" + (j + 1) + " option:selected").text() == "Personal Credit Card") {
                        var base_e_amt = $("#txt_L_BAmount" + (i) + "" + (j + 1) + "").val();
                        var inr_e_amt = $("#txt_L_Amount" + (i) + "" + (j + 1) + "").val();
                        if (base_e_amt == "" || base_e_amt == undefined) {
                            base_e_amt = 0;
                        }
                        if (inr_e_amt == "" || inr_e_amt == undefined) {
                            inr_e_amt = 0;
                        }
                        Local_travel += "<BASE_EXP_AMOUNT>" + base_e_amt + "</BASE_EXP_AMOUNT>";
                        Local_travel += "<INR_EXP_AMOUNT>" + inr_e_amt + "</INR_EXP_AMOUNT>";
                    }
                    else {
                        var base_e_amt = $("#spn_L_BAmount" + (i) + "" + (j + 1) + "").html();
                        var inr_e_amt = $("#spn_L_Amount" + (i) + "" + (j + 1) + "").html();
                        if (base_e_amt == "" || base_e_amt == undefined) {
                            base_e_amt = 0;
                        }
                        if (inr_e_amt == "" || inr_e_amt == undefined) {
                            inr_e_amt = 0;
                        }
                        Local_travel += "<BASE_EXP_AMOUNT>" + base_e_amt + "</BASE_EXP_AMOUNT>";
                        Local_travel += "<INR_EXP_AMOUNT>" + inr_e_amt + "</INR_EXP_AMOUNT>";
                    }
                    /****************************************************************************************/
                    Local_travel += "</ROW>";
                }
            }

            Local_travel += "</ROWSET>";
            xml_dtl += "</ROWSET>";
            xml_exp += "</ROWSET>";
            $("#txt_LocTrav_XML").val(Local_travel);
            var lastRow1 = $('#uploadTable tr').length;
            XMLFILE = "<ROWSET>";
            //alert(lastRow1);
            //if (lastRow1 < 2 && $("#Non_supp_Yes_no").val()==1)
            //{
            //    alert("Please Attach Atleast One Document.");
            //    return false;
            //}
            if (lastRow1 > 1) {
                for (var l = 0; l < lastRow1 - 1; l++) {
                    var firstCol = $("#uploadTable tr")[l + 1].cells[0].innerText;
                    var SecondCol = $("#uploadTable tr")[l + 1].cells[1].innerText;
                    XMLFILE += "<ROW>";
                    XMLFILE += "<OBJECT_TYPE>FOREIGN TRAVEL EXPENSE</OBJECT_TYPE>";
                    XMLFILE += "<OBJECT_VALUE>" + $("#spn_req_no").html() + "</OBJECT_VALUE>";
                    XMLFILE += "<DOCUMENT_TYPE>" + firstCol + "</DOCUMENT_TYPE>";
                    XMLFILE += "<FILENAME>" + SecondCol + "</FILENAME>";
                    XMLFILE += "</ROW>";
                }
            }
            XMLFILE += "</ROWSET>";
            $("#txt_LocTrav_XML").val(Local_travel);
            $("#txt_Document_Xml").val(XMLFILE);
            $("#txt_sub_xml_data").val(xml_exp);
            $("#txt_xml_data").val(xml_dtl);
            $("#Return_Money_Div").modal('show');
        }
        else {
            return false;
        }
        return false;
    }
    catch (ex) {
        xml_exp = "<ROWSET></ROWSET>";
        xml_dtl = "<ROWSET></ROWSET>";
        XMLFILE = "<ROWSET></ROWSET>";
        Local_travel = "<ROWSET></ROWSET>";
    }
}

function validate_Controls() {
    if (($("#txt_Username").val()).trim() != "") {

        if ($("#travelFromDate").val().trim() != "") {
            if ($("#travelToDate").val().trim() != "") {
                if ($("#ddlCountry option:selected").index() != 0) {
                    if ($("#span_Approver").html().trim() != "") {
                        if ($("#req_remark").val().trim() != "") {
                            var adv = $("[name='travel']");
                            var chk = 0;
                            $("#txt_advance_id").val(0);
                            for (var a = 0; a < adv.length; a++) {
                                if ($("#radio" + (a + 1)).is(':checked')) {
                                    chk = chk + 1;
                                    $("#txt_advance_id").val($("#PK_ADVANCE_ID" + (a + 1)).val());
                                }
                            }
                            if (chk < 1) {
                                alert("Please Select Advance!!!");
                                return false;
                            }

                            var rows = $("[name='rs']");
                            var Class = Support_tot = Non_Support_tot = percent_val = DO_Support_Counter = Rupess = 0;
                            var exp_round = 0;
                            $("#Non_supp_Yes_no").val("0");
                            for (var i = 1; i <= rows.length; i++) {
                                var row_date = $("#spn_date" + i).html();
                                if ($("#ddlCity" + i).val() == -1 && $("#other_f_city" + i).val().trim() == "") {
                                    alert("Please Enter City For " + row_date);
                                    $("#other_f_city" + i).focus();
                                    return false;
                                }
                                else if ($("#ddlCity" + i).val() == 0 || $("#ddlCity" + i).val() == undefined || $("#ddlCity" + i + " option:selected").index() < 1) {
                                    alert("Please Select City For " + row_date);
                                    $("#ddlCity" + i).focus();
                                    return false;
                                }
                                else if ($("#txt_Particulars" + i).val().trim() == "") {
                                    alert("Please Enter Particulars Remark For " + row_date);
                                    $("#txt_Particulars" + i).focus();
                                    return false;
                                }
                                else if ($("#hotel_name" + i).val().trim() == "") {
                                    alert("Please Enter Hotel Name For " + row_date);
                                    $("#hotel_name" + i).focus();
                                    return false;
                                }
                                else if ($("#contact" + i).val().trim() == "") {
                                    alert("Please Enter Contact Number For " + row_date);
                                    $("#contact" + i).focus();
                                    return false;
                                }

                                else if ($("#ddlTravel_class" + i).val().trim() == "0") {
                                    alert("Please Select Travel Class For " + row_date);
                                    $("#ddlTravel_class" + i).focus();
                                    return false;
                                }
                                calculate_Amount();
                                var row_index = 0;
                                var row_index = document.getElementById("tab_Exp" + (i) + "").rows.length
                                for (var eh = 1; eh <= row_index - 1; eh++) {
                                    var exp_amt = 0;
                                    if (($("#ddlrem_" + i + "_" + eh).find("option:selected").text() == "Personal Credit Card") || ($("#ddlrem_" + i + "_" + eh).find("option:selected").text() == "Corporate Credit Card")) {
                                        if ($("#dollar_txt" + i + "_" + eh).val().trim() != "") {
                                            exp_amt = $("#dollar_txt" + i + "_" + eh).val();
                                        }
                                        else {
                                            exp_amt = 0
                                        }

                                    }
                                    else {
                                        if ($("#txt_Expense_Amt" + i + "_" + eh).val().trim() != "") {
                                            exp_amt = $("#txt_Expense_Amt" + i + "_" + eh).val().trim();
                                        }
                                        else {
                                            exp_amt = 0
                                        }
                                    }
                                    if (eh != 2) {
                                        var IS_Supp = $("#IS_SUP" + i + eh).html();
                                        var IS_Supp_NO = $("#ddl_SUP_" + i + "_" + eh).val();
                                        if ($("#expense_id" + i + "_" + eh).val() != undefined && $("#expense_id" + i + "_" + eh).val() != "0") {

                                            if ($("#cur_" + i + "_" + eh).val() == "") {
                                                //alert("Please Enter Exchange Rate For " + row_date);
                                                //$("#cur_" + i + "_" + eh).focus();
                                                //return false;
                                                $("#cur_" + i + "_" + eh).val(0);
                                            }

                                            if ($("#rupees_" + i + "_" + eh).text() != "0.00" && $("#rupees_" + i + "_" + eh).text() != "0") {
                                                if ($("#ddl_SUP_" + i + "_" + eh).val() == "Y") {
                                                    if ($("#particular_SUP_" + i + "_" + eh).val() == "") {
                                                        alert("Please Enter Supporting Particulars For " + row_date);
                                                        $("#particular_SUP_" + i + "_" + eh).focus();
                                                        return false;
                                                    }
                                                }
                                                else {
                                                    if ($("#expense_id" + i + "_" + eh + " option:selected").text().toUpperCase() == "BOARDING") {
                                                        alert("As per International Travel Policy, food expenses without bill not allowed For " + row_date);
                                                        return false;
                                                    }
                                                }
                                            }
                                            if ((IS_Supp == "Y") && (exp_amt != "0") && (IS_Supp_NO == "Y")) {
                                                if ($("#dollar_" + i + "_" + eh).html() != "0.00") {
                                                    Rupess = $("#dollar_" + i + "_" + eh).html();
                                                    Support_tot += parseFloat(Rupess);
                                                    $("#Non_supp_Yes_no").val("1");
                                                }
                                            }
                                            if ((IS_Supp == "Y") && (IS_Supp_NO == "N")) {
                                                if (eh == 1) {
                                                    $("#dollar_" + i + "_" + eh).html(Supporting_Doc_Check(i));
                                                    Rupess = $("#dollar_" + i + "_" + eh).html();
                                                    Non_Support_tot += parseFloat(Rupess);

                                                }
                                                else {
                                                    if ($("#dollar_" + i + "_" + eh).html() != "0.00") {
                                                        Rupess = $("#dollar_" + i + "_" + eh).html();
                                                        Non_Support_tot += parseFloat(Rupess);
                                                    }
                                                }

                                            }
                                            if ((Support_tot != "0" || Non_Support_tot != "0") && $("#txt_Supp_Counter").val() != "" && $("#txt_Supp_Counter").val() != "1") {
                                                percent_val = parseFloat(parseFloat(parseFloat(parseFloat(Non_Support_tot) * 100) / parseFloat(Support_tot)) / parseFloat(Support_tot)).toFixed(3);
                                                if (parseFloat(percent_val) > parseFloat($("#txt_Supporting_Limit").val())) {
                                                    DO_Support_Counter++;
                                                    $("#txt_Supp_Counter").val(DO_Support_Counter);
                                                }
                                            }
                                        }
                                    }

                                }

                                if ($("#txt_Supp_Counter").val() == "0" || $("#txt_Supp_Counter").val() == "") {
                                    var row_index = document.getElementById("tbl_Local_Travel" + (i) + "").rows.length;
                                    for (var eh = 1; eh <= row_index - 1; eh++) {
                                        var base_amount = 0;
                                        if ($("#txt_ReimType" + i + "_" + eh + " option:selected").text() == "Personal Credit Card" || $("#txt_ReimType" + i + "_" + eh + " option:selected").text() == "Corporate Credit Card") {
                                            base_amount = $("#txt_L_BAmount" + i + "" + eh).val();
                                        }
                                        else {
                                            base_amount = $("#spn_L_Amount" + i + "" + eh).html();
                                        }
                                        var ddl_supp_next = $("#txt_Bill" + i + "" + eh).val();
                                        if (base_amount == undefined || base_amount == "" || isNaN(base_amount))
                                        {
                                            base_amount = 0;
                                        }
                                        if (parseFloat(base_amount) > 0 && ddl_supp_next == "N")
                                        {
                                            $("#txt_Supp_Counter").val("1");
                                        }
                                    }
                                }

                                if (($("#txt_Class").val() == "False") && ($("#ddlTravel_class" + i).val().trim() == "Business Class")) {
                                    Class++;
                                }
                            }
                            if ($("#txt_Amount").val() == "0.00") {
                                alert("Voucher Amount can't be Zero.");
                                return false;
                            }
                            var Reason = "";
                            if (Class != "0") {
                                Reason += "Deviation due to Travel Class,"
                            }
                            if ($("#txt_Do_Limit").val() != "0") {
                                Reason += "Deviation due to Allowable limits exceed,"
                            }
                            if ($("#txt_Supp_Counter").val() != "0" && $("#txt_Supp_Counter").val() != "") {
                                Reason += "Deviation due to Supporting document not attached"
                            }
                            
                            $("#txt_Deviation_Reason").val(Reason);

                            var lastRow1 = $('#uploadTable tr').length;

                            if (lastRow1 < 2 && $("#Non_supp_Yes_no").val() == "1") {
                                alert("Please Attach Atleast One Document.");
                                return false;
                            }

                            if (Class != "0" || $("#txt_Do_Limit").val() != "0" || $("#txt_Supp_Counter").val() != "0") {
                                var yes = confirm("Request will be Submitted under Deviation, Do you want to continue?");
                                if (yes == true) {
                                    $("#txt_DO_Status").val("Yes");
                                    return true;
                                }
                                else {
                                    $("#txt_DO_Status").val("No");
                                    return false;
                                }
                                return false;
                            }
                            return true;
                        }
                        else {
                            alert("Please Enter Remark!");
                            return false;
                        }
                    }
                    else {
                        alert("Approver Not Found!");
                        return false;
                    }
                }
                else {
                    alert("Please Select Country!");
                    return false;
                }
            }
            else {
                alert("Please Select To Date!");
                return false;
            }
        }
        else {
            alert("Please Select From Date!");
            return false;
        }
    }
    else {
        alert("User Data Not Found!");
        return false;
    }
}

function copyData(row_index) {
    var j1_index = $("#ddlCity1 option:selected").index();
    var jc_index = $("#ddlCity" + row_index + " option:selected").index();
    if (j1_index > 0 && jc_index == 0) {
        var j_val = $("#ddlCity1").val();
        $("#ddlCity" + row_index).val(j_val);
        $("#txt_Particulars" + row_index).val($("#txt_Particulars1").val());
        $("#hotel_name" + row_index).val($("#hotel_name1").val());
        $("#contact" + row_index).val($("#contact1").val());
    }
}

function change_radio(row_index) {
    var S_Amount = S_Forex = S_Advance = 0;
    Dollar_Currecny = $("#currency_adv" + row_index).val();
    $("#exch_rate").val($("#exc_rate" + row_index).val());

    var rws = $("[name='rs']");
    for (var i = 1; i <= rws.length; i++) {
        $("#spn_currency" + i).text($("#currency_adv" + row_index).val());
        $("#spn_currency_amt" + i).text($("#currency_adv" + row_index).val());
    }
    $("#S_Currency").text($("#currency_adv" + row_index).val());
    S_Amount = parseFloat($("#txt_Currency_Amount" + row_index).val());
    S_Forex = parseFloat($("#txt_Forex_card" + row_index).val());
    S_Advance = parseFloat(S_Amount + S_Forex).toFixed(3);
    $("#S_Advance").text(S_Advance);
    calculate_Amount();
}

function calculate_Amount() {
    try {
        var adv_currency = $("#exch_rate").val();
        var Expense = S_Advance = S_Difference = 0;
        var rws = $("[name='rs']");
        $("#txt_Supp_Counter").val(0);
        $("#txt_Do_Limit").val(0);
        calculate_Local_Amount();
        for (var i = 1; i <= rws.length; i++) {
            var hotel_total = 0;
            var boarding_total = 0;
            var internet_total = 0;
            var row_Total = Amount = Rupees = Dollar_addition = USD = INR = rowIndex = E_Tax = 0;
            rowIndex = document.getElementById("tab_Exp" + (i) + "").rows.length;
            for (var eh = 1; eh <= rowIndex - 1; eh++) {
                var e_name = $("#expense_id" + i + "_" + eh).find("option:selected").text();
                var h_limit = $("#hlimit" + i + "_" + eh).val();
                if ($("#expense_id" + i + "_" + eh).val() != undefined && $("#expense_id" + i + "_" + eh).val() != "0") {
                    var exp_amt = 0;
                    //Local Travel
                    if (e_name == "Conveyance") {
                        var loc_expense_amt = tax = conversion_rate = local_tab = loc_counter = 0;
                        local_tab = document.getElementById("tbl_Local_Travel" + (i) + "").rows.length;
                        for (var j = 0; j < local_tab - 1; j++) {
                            if ($("#txt_Expenses" + (i) + "" + (j + 1) + "").val() != "") {
                                loc_expense_amt = (parseFloat(loc_expense_amt) + parseFloat($("#txt_Expenses" + (i) + "" + (j + 1) + "").val())).toFixed(3);
                            }
                            if ($("#txt_L_Tax" + (i) + "" + (j + 1) + "").val() != "") {
                                tax = parseFloat(parseFloat(tax) + parseFloat($("#txt_L_Tax" + (i) + "" + (j + 1) + "").val())).toFixed(3);
                            }
                            if ($("#txt_Bill" + (i) + "" + (j + 1) + "").val() != "0" && $("#txt_Bill" + (i) + "" + (j + 1) + "").val() == "N") {
                                loc_counter++;
                            }
                        }
                        $("#ddl_SUP_" + (i) + "_" + (eh) + "").attr("disabled", true);
                        if (loc_counter != "0") {
                            $("#ddl_SUP_" + (i) + "_" + (eh) + "").val("N");
                            enable_disable_field(i, eh);

                        }
                        else {
                            $("#ddl_SUP_" + (i) + "_" + (eh) + "").val("Y");
                            enable_disable_field(i, eh);
                        }
                        $("#txt_Expense_Amt" + i + "_" + eh).attr('readonly', true);
                        $("#Tax_" + i + "_" + eh).attr('readonly', true);
                        if (loc_expense_amt != 0 && loc_expense_amt != "") {
                            $("#txt_Expense_Amt" + i + "_" + eh).val(loc_expense_amt);
                        }
                        if (tax != 0 && tax != "") {
                            $("#Tax_" + i + "_" + eh).val(tax);
                        }
                    }
                    if (($("#ddlrem_" + i + "_" + eh).find("option:selected").text() == "Personal Credit Card")) {
                        if ($("#dollar_txt" + i + "_" + eh).val().trim() != "") {
                            exp_amt = $("#dollar_txt" + i + "_" + eh).val();
                        }
                    }
                    else {
                        if ($("#txt_Expense_Amt" + i + "_" + eh).val().trim() != "") {
                            exp_amt = $("#txt_Expense_Amt" + i + "_" + eh).val();
                        }

                    }


                    var exc_rate = $("#cur_" + i + "_" + eh).val();
                    if (exp_amt == NaN || exp_amt == undefined || exp_amt == '') {
                        exp_amt = 0;
                    }
                    if (exc_rate == NaN || exc_rate == undefined || exc_rate == '') {
                        exc_rate = 0;
                    }

                    var rem_amount = 0;
                    if (($("#ddlrem_" + i + "_" + eh).find("option:selected").text() != "Personal Credit Card") && ($("#ddlrem_" + i + "_" + eh).find("option:selected").text() != "Corporate Credit Card")) {
                        if ($("#txt_Expense_Amt" + i + "_" + eh).val().trim() != "" && $("#txt_Expense_Amt" + i + "_" + eh).val().trim() != "0" && exc_rate != "0" && exc_rate != "") {
                            if ($("#Tax_" + i + "_" + eh).val().trim() != "") {
                                E_Tax = $("#Tax_" + i + "_" + eh).val().trim();
                            }
                            else {
                                E_Tax = 0;
                            }
                            Amount = parseFloat((parseFloat(exp_amt) + parseFloat(E_Tax)) / parseFloat(exc_rate)).toFixed(3);
                        }
                        else {
                            Amount = 0;
                        }
                        rem_amount = parseFloat(parseFloat(exp_amt) / parseFloat(exc_rate)).toFixed(3); 
                    }



                    //************Change*********//
                    if (($("#ddlrem_" + i + "_" + eh).find("option:selected").text() == "Corporate Credit Card") || ($("#ddlrem_" + i + "_" + eh).find("option:selected").text() == "Personal Credit Card")) {
                        $("#txt_Expense_Amt" + i + "_" + eh).attr('readonly', true);
                        $("#cur_" + i + "_" + eh).attr('readonly', true);
                        $("#dollar_" + i + "_" + eh).hide();
                        $("#dollar_" + i + "_" + eh).html();
                        $("#rupees_" + i + "_" + eh).hide();
                        $("#rupees_" + i + "_" + eh).html();
                        $("#dollar_txt" + i + "_" + eh).show();
                        $("#rupees_txt" + i + "_" + eh).show();
                        $("#txt_Expense_Amt" + i + "_" + eh).val("0");
                        if (($("#dollar_txt" + i + "_" + eh).val() != "") && ($("#rupees_txt" + i + "_" + eh).val() != "")) {
                            USD = parseFloat($("#dollar_txt" + i + "_" + eh).val());
                            INR = parseFloat($("#rupees_txt" + i + "_" + eh).val());
                            $("#cur_" + i + "_" + eh).val((INR / USD).toFixed(3));
                        }
                        else {
                            $("#cur_" + i + "_" + eh).val(0);
                        }
                        rem_amount = USD;
                    }

                    if (e_name == "Hotel") {
                        hotel_total = parseFloat(hotel_total) + parseFloat(rem_amount);
                    }
                    else if (e_name == "Boarding") {
                        boarding_total = parseFloat(boarding_total) + parseFloat(rem_amount);
                    }
                    else if (e_name == "Internet") {
                        internet_total = parseFloat(internet_total) + parseFloat(rem_amount);
                    }

                    if (($("#ddlrem_" + i + "_" + eh).find("option:selected").text() == "Regular Reimbursement") || ($("#ddlrem_" + i + "_" + eh).find("option:selected").text() == "Forex Card") || ($("#ddlrem_" + i + "_" + eh).find("option:selected").text() == "Personal Credit Card")) {
                        if (($("#txt_Expense_Amt" + i + "_" + eh).val().trim() != "") && ($("#txt_Expense_Amt" + i + "_" + eh).val().trim() != "0")) {
                            Dollar_addition = parseFloat(parseFloat(parseFloat(Dollar_addition) + parseFloat(Amount))).toFixed(3);
                        }
                        else {
                            if ($("#dollar_txt" + i + "_" + eh).val().trim() != "0" && $("#dollar_txt" + i + "_" + eh).val().trim() != "") {
                                Dollar_addition = parseFloat(parseFloat(Dollar_addition) + parseFloat($("#dollar_txt" + i + "_" + eh).val())).toFixed(3);
                            }
                        }
                    }

                    if (($("#ddlrem_" + i + "_" + eh).find("option:selected").text() != "Corporate Credit Card") && ($("#ddlrem_" + i + "_" + eh).find("option:selected").text() != "Personal Credit Card")) {
                        $("#dollar_" + i + "_" + eh).show();
                        $("#rupees_" + i + "_" + eh).show();
                        $("#dollar_txt" + i + "_" + eh).hide();
                        $("#dollar_txt" + i + "_" + eh).val();
                        $("#rupees_txt" + i + "_" + eh).hide();
                        $("#rupees_txt" + i + "_" + eh).val();
                        if (e_name.toUpperCase() != "INCIDENTAL") {
                            $("#cur_" + i + "_" + eh).attr('readonly', false);
                        }
                    }

                    $("#dollar_" + i + "_" + eh).html(Amount);
                    var dollar = $("#dollar_" + i + "_" + eh).html();
                    $("#dollar_" + i + "_" + eh).html(Amount);
                    if (($("#ddlrem_" + i + "_" + eh).find("option:selected").text() == "Corporate Credit Card") || ($("#ddlrem_" + i + "_" + eh).find("option:selected").text() == "Personal Credit Card")) {
                        var dollar = $("#dollar_txt" + i + "_" + eh).val();
                    }
                    else {
                        var dollar = $("#dollar_" + i + "_" + eh).html();
                    }
                    //************END*********//
                    if (dollar == NaN || dollar == undefined || dollar == '') {
                        dollar = 0;
                    }
                    Rupees = Math.round(parseFloat(dollar) * parseFloat(adv_currency));
                    $("#rupees_" + i + "_" + eh).html(Rupees);
                    row_Total = row_Total + parseFloat($("#rupees_" + i + "_" + eh).html());
                    if (Rupees == "0") {
                        Rupees = "0.00";
                    }

                    if (e_name == "Incidental") {
                        $("#dollar_" + i + "_" + eh).css('color', 'black');
                    }
                    else if (e_name == "Boarding" && h_limit != "") {
                        var red_amount = $("#dollar_" + i + "_" + eh).html();
                        if (red_amount == "" || parseFloat(red_amount) < 0) {
                            red_amount = 0;
                        }
                        if (parseFloat(boarding_total) > parseFloat(h_limit) && boarding_total > 0 && h_limit != "" && red_amount > 0) {
                            $("#dollar_" + i + "_" + eh).css('color', '#FA564B');
                            $("#txt_Do_Limit").val(1);
                        }
                        else {
                            $("#dollar_" + i + "_" + eh).css('color', 'black');
                        }
                    }
                    else if (e_name == "Hotel" && h_limit != "") {
                        var red_amount = $("#dollar_" + i + "_" + eh).html();
                        if (red_amount == "" || parseFloat(red_amount) < 0) {
                            red_amount = 0;
                        }
                        if (parseFloat(hotel_total) > parseFloat(h_limit) && hotel_total > 0 && h_limit != "" && red_amount > 0) {
                            $("#dollar_" + i + "_" + eh).css('color', '#FA564B');
                            $("#txt_Do_Limit").val(1);
                        }
                        else {
                            $("#dollar_" + i + "_" + eh).css('color', 'black');
                        }
                    }
                    else if (e_name == "Internet" && h_limit != "") {
                        var red_amount = $("#dollar_" + i + "_" + eh).html();
                        if (red_amount == "" || parseFloat(red_amount) < 0) {
                            red_amount = 0;
                        }
                        if (parseFloat(internet_total) > parseFloat(h_limit) && internet_total > 0 && h_limit != "" && red_amount > 0) {
                            $("#dollar_" + i + "_" + eh).css('color', '#FA564B');
                            $("#txt_Do_Limit").val(1);
                        }
                        else {
                            $("#dollar_" + i + "_" + eh).css('color', 'black');
                        }
                    }
                }
            }
            
            var row_local = 0;
            row_local = $("#span_Local_Amount" + i).html();
            if ($("#span_Local_Amount" + i).html() == "" || $("#span_Local_Amount" + i).html() == undefined) {
                row_local = 0;
            }
            row_Total = (row_Total).toFixed(3);
            if (row_Total != "0.00") {
                $("#txt_Amount").val(Dollar_addition);
            }
            $("#row_Total" + i).html(parseFloat(parseFloat(Dollar_addition) + parseFloat(row_local)).toFixed(3));
            $("#txt_Amount").val(Dollar_addition);
            Expense += parseFloat(Dollar_addition) + parseFloat(row_local);
        }
        $("#S_Expense").text(parseFloat(Expense).toFixed(3));
        S_Advance = parseFloat($("#S_Advance").text()).toFixed(3);
        S_Difference = parseFloat(S_Advance - Expense).toFixed(3);
        if (S_Advance > Expense) {
            $("#S_Diff").text(S_Difference + " (Receive from Requester.)");
            $("#Money_Div").show();
        }
        else {
            $("#S_Diff").text(S_Difference + " (Paid to Requester.)");
            $("#Money_Div").hide();
        }

    }
    catch (ex) {
        alert(ex);
    }
}

function checkSupp_Docs() {
    var msg = "";
    var d = document.getElementsByName('rs');
    for (var i = 1; i <= d.length; i++) {
        var span_val = $("#spn_date" + i).text();
        var exp = document.getElementsByName("eh" + i);
        for (var eh = 1; eh <= exp.length; eh++) {
            var fk_id = $("#e_fk_id" + i + "" + eh).val();
            var ename = $("#e_name" + i + "" + eh).val();
            var ddl_SUP = $("#ddl_SUP" + fk_id + "_" + i + "_" + eh + " option:selected").val();
            if (ddl_SUP == "Y") {
                var particular_SUP = $("#particular_SUP" + fk_id + "_" + i + "_" + eh).val();
                if (particular_SUP == "") {
                    msg = "Enter Supporting Particulars For " + span_val + "...!";
                }
            }
            var fk_other_remark = $("#fk_other_remark" + fk_id + "_" + i + "_" + eh).val();
            var e_name = $("#e_name" + i + "" + eh).val();
            var exp_amt = $("#" + i + "_" + eh).val();
            var other_remark = $("#other_remark" + fk_id + "_" + i + "_" + eh).val();
            if (other_remark == "" && fk_other_remark == 1 && exp_amt != undefined && exp_amt != "" && parseInt(exp_amt) > 0) {
                msg = "Enter Remark For " + ename + " Expense Head on " + span_val + "...!";
            }
        }
    }
    return msg;
}

function change_flag(tab_id, row_id) {
    try {
        if (row_id != "1") {
            var rowIndex = count = 0;
            var data = "";
            data = tab_id + "_" + row_id;
            var firstDate = $("#spn_date" + tab_id).html();
            rowIndex = document.getElementById("tab_Exp" + (tab_id) + "").rows.length;
	    $("#Tax_" + tab_id + "_" + row_id + "").attr("readonly",false);
	    if($("#ddlrem_" + tab_id + "_" + row_id + " option:selected").text()=="Corporate Credit Card" || $("#ddlrem_" + tab_id + "_" + row_id +  " option:selected").text()=="Forex Card")
	    {
		$("#Tax_" + tab_id + "_" + row_id + "").val(0);
                $("#Tax_" + tab_id + "_" + row_id + "").attr("readonly",true);
            }
            for (var j = 0; j < rowIndex - 1; j++) {
                if ($("#expense_id" + tab_id + "_" + (j + 1) + "").val() == $("#expense_id" + tab_id + "_" + row_id + "").val() && (j + 1) != row_id) {
                    if ($("#ddlrem_" + tab_id + "_" + (j + 1) + "").val() == $("#ddlrem_" + tab_id + "_" + row_id + "").val() && (j + 1) != row_id) {
                        count++;
                    }
                }
            }
            if (count != "0") {
                alert("Please Select other Reimbursement Type on "+firstDate+" at Row No : " + (row_id) + "!")
                $("#ddlrem_" + tab_id + "_" + row_id + "").val(0);
                return false;
            }

            if ($("#ddlrem_" + tab_id + "_" + row_id + "").val() != "0") {
                Foreign_Travel_Request_Modification.chagable_or_not("International Travel Expenses", $("#ddlrem_" + tab_id + "_" + row_id + "").val(), data, changeText);
            }
        }

    }
    catch (ex) {
        alert(ex);
    }
}

function change_detail_flag(tab_id, row_id) {
    try {
        //if (row_id != "1") {
        var rowIndex = count = 0;
        var data = "";
        data = tab_id + "_" + row_id;
        var firstDate = $("#spn_date" + tab_id).html();
        rowIndex = document.getElementById("tbl_Local_Travel" + (tab_id) + "").rows.length;

        if (($("#txt_ReimType" + tab_id + "_" + row_id).find("option:selected").text() == "Regular Reimbursement") || ($("#txt_ReimType" + tab_id + "_" + row_id).find("option:selected").text() == "Company Provided") || ($("#txt_ReimType" + tab_id + "_" + row_id).find("option:selected").text() == "Forex Card")) {
            $("#txt_Expenses" + tab_id + "" + row_id).attr('readonly', false);

            if ($("#txt_ReimType" + tab_id + "_" + row_id).find("option:selected").text() == "Forex Card") {
                $("#txt_L_Tax" + tab_id + "" + row_id).attr('readonly', true);
            }
            else {
                $("#txt_L_Tax" + tab_id + "" + row_id).attr('readonly', false);
            }
            $("#txt_L_Rate" + tab_id + "" + row_id).attr('readonly', false);
            $("#spn_L_BAmount" + tab_id + "" + row_id).show();
            $("#spn_L_Amount" + tab_id + "" + row_id).show();
            $("#txt_L_BAmount" + tab_id + "" + row_id).hide();
            $("#txt_L_Amount" + tab_id + "" + row_id).hide();
            $("#spn_L_BAmount" + tab_id + "" + row_id).html("0");
            $("#spn_L_Amount" + tab_id + "" + row_id).html("0");
            $("#txt_L_BAmount" + tab_id + "" + row_id).val("");
            $("#txt_L_Amount" + tab_id + "" + row_id).val("");
            $("#txt_L_Tax" + tab_id + "" + row_id).val("");

        }
        else if (($("#txt_ReimType" + tab_id + "_" + row_id).find("option:selected").text() == "Corporate Credit Card")) {
            $("#txt_Expenses" + tab_id + "" + row_id).attr('readonly', true);
            $("#txt_L_Tax" + tab_id + "" + row_id).attr('readonly', true);
            $("#txt_L_Rate" + tab_id + "" + row_id).attr('readonly', true);
            $("#spn_L_BAmount" + tab_id + "" + row_id).hide();
            $("#spn_L_Amount" + tab_id + "" + row_id).hide();
            $("#txt_L_BAmount" + tab_id + "" + row_id).show();
            $("#txt_L_Amount" + tab_id + "" + row_id).show();
            $("#spn_L_BAmount" + tab_id + "" + row_id).html("0");
            $("#spn_L_Amount" + tab_id + "" + row_id).html("0");
            $("#txt_L_BAmount" + tab_id + "" + row_id).val("");
            $("#txt_L_Amount" + tab_id + "" + row_id).val("");
            $("#txt_L_Tax" + tab_id + "" + row_id).val("");
            $("#txt_Expenses" + tab_id + "" + row_id).val("");
            $("#txt_L_Rate" + tab_id + "" + row_id).val("");
        }
        else if (($("#txt_ReimType" + tab_id + "_" + row_id).find("option:selected").text() == "Personal Credit Card")) {
            $("#txt_Expenses" + tab_id + "" + row_id).attr('readonly', true);
            $("#txt_L_Tax" + tab_id + "" + row_id).attr('readonly', false);
            $("#txt_L_Rate" + tab_id + "" + row_id).attr('readonly', true);
            $("#spn_L_BAmount" + tab_id + "" + row_id).hide();
            $("#spn_L_Amount" + tab_id + "" + row_id).hide();
            $("#txt_L_BAmount" + tab_id + "" + row_id).show();
            $("#txt_L_Amount" + tab_id + "" + row_id).show();
            $("#spn_L_BAmount" + tab_id + "" + row_id).html("0");
            $("#spn_L_Amount" + tab_id + "" + row_id).html("0");
            $("#txt_L_BAmount" + tab_id + "" + row_id).val("");
            $("#txt_L_Amount" + tab_id + "" + row_id).val("");
            $("#txt_L_Tax" + tab_id + "" + row_id).val("");
            $("#txt_Expenses" + tab_id + "" + row_id).val("");
            $("#txt_L_Rate" + tab_id + "" + row_id).val("");
        }


        for (var j = 0; j < rowIndex - 1; j++) {
            if ($("#txt_Expenses" + tab_id + "_" + (j + 1) + "").val() == $("#txt_Expenses" + tab_id + "_" + row_id + "").val() && (j + 1) != row_id) {
                if ($("#txt_ReimType" + tab_id + "_" + (j + 1) + "").val() == $("#txt_ReimType" + tab_id + "_" + row_id + "").val() && $("#txt_Bill" + tab_id + "" + (j + 1) + "").val() == $("#txt_Bill" + tab_id + "" + row_id + "").val() && (j + 1) != row_id) {
                    count++;
                }
            }
        }
        calculate_Amount();
        if (count != "0") {
            alert("Please Select other Reimbursement Type on "+firstDate+" at Row No : " + (row_id) + "!")
            $("#txt_ReimType" + tab_id + "_" + row_id + "").val(0);
            return false;
        }


    }
    catch (ex) {
        alert(ex);
    }
}

function changeText(response) {
    var data = (response.value).split("@@");
    $("#reim_val" + data[0] + "").val(data[1]);
    calculate_Amount();
}

$("#btn_Save").click(function () {
    if ((parseFloat($("#S_Advance").text())) > (parseFloat($("#S_Expense").text()))) {
        if ($("#txt_Return_Money").val().trim() == "") {
            alert("Please Enter Retrun Money!")
            $("#txt_Return_Money").focus();
            return false;
        }
    }
    else {
        $("#txt_Return_Money").val("0");
    }
    $("#divIns").show();
    return true;
});

function Save_As_Draft() {
    var XMLFILE = "<ROWSET>";
    var xml_dtl = "<ROWSET>";
    var xml_exp = "<ROWSET>";
    var Local_travel = "<ROWSET>";
    try {
        //if (validate_DraftControls() == true) {
        if ($("#travelFromDate").val().trim() == "") {
            alert("Please Select From Date.");
            return false;
        }
        if ($("#travelToDate").val().trim() == "") {
            alert("Please Select To Date.");
            return false;
        }
        if ($("#ddlCountry option:selected").index() == 0) {
            alert("Please Select Region.");
            return false;
        }
        if ($("#span_Approver").html().trim() == "") {
            alert("Approver Not Found.");
            return false;
        }
        if ($("#req_remark").val().trim() == "") {
            alert("Please Enter Remark");
            return false;
        }

        var adv = $("[name='travel']");
        var chk = 0;
        $("#txt_advance_id").val(0);
        for (var a = 0; a < adv.length; a++) {
            if ($("#radio" + (a + 1)).is(':checked')) {
                chk = chk + 1;
                $("#txt_advance_id").val($("#PK_ADVANCE_ID" + (a + 1)).val());
            }
        }
        if (chk < 1) {
            alert("Please Select Advance!");
            return false;
        }

        var rws = $("[name='rs']");
        for (var i = 1; i <= rws.length; i++) {
            var firstDate = $("#spn_date" + i).html();
            fdate = getFormatedDate1(firstDate);
            xml_dtl += "<ROW>";
            xml_dtl += "<fk_FOREIGN_expense_Hdr_Id>" + $("#txt_pk_id").val() + "</fk_FOREIGN_expense_Hdr_Id>";
            xml_dtl += "<travel_date>" + fdate + "</travel_date>";
            xml_dtl += "<FK_CITY>" + $("#ddlCity" + i).val() + "</FK_CITY>";

            if ($("#hotel_name" + i).val() == undefined) {
                xml_dtl += "<hotel_name></hotel_name>";
            }
            else {
                xml_dtl += "<hotel_name>" + $("#hotel_name" + i).val() + "</hotel_name>";
            }
            if ($("#contact" + i).val() == undefined) {
                xml_dtl += "<hotel_no></hotel_no>";
            }
            else {
                xml_dtl += "<hotel_no>" + $("#contact" + i).val() + "</hotel_no>";
            }
            xml_dtl += "<TRAVEL_CLASS>" + $("#ddlTravel_class" + i).val() + "</TRAVEL_CLASS>";
            xml_dtl += "<Other_City>" + $("#other_f_city" + i).val() + "</Other_City>";
            xml_dtl += "<remark_note>" + $("#txt_Particulars" + i).val() + "</remark_note>";
            xml_dtl += "</ROW>";

            var rowindex = rowIndex = 0;
            rowindex = document.getElementById("tab_Exp" + (i) + "").rows.length;
            for (var eh = 1; eh <= rowindex - 1; eh++) {
                var e_name = $("#expense_id" + i + "_" + eh).find("option:selected").text();
                if ($("#expense_id" + i + "_" + eh).val() != undefined && $("#expense_id" + i + "_" + eh).val() != "0") {
                    xml_exp += "<ROW>";
                    xml_exp += "<fk_FOREIGN_expense_Hdr_Id>" + $("#txt_pk_id").val() + "</fk_FOREIGN_expense_Hdr_Id>";
                    xml_exp += "<fk_expense_head_id>" + $("#expense_id" + i + "_" + eh).val() + "</fk_expense_head_id>";
                    var exp_amt = $("#txt_Expense_Amt" + i + "_" + eh).val();
                    if (exp_amt != undefined && exp_amt != "0") {
                        xml_exp += "<COUNTRY_CURRENCY>" + exp_amt + "</COUNTRY_CURRENCY>";
                    }
                    else {
                        xml_exp += "<COUNTRY_CURRENCY>" + 0 + "</COUNTRY_CURRENCY>";
                    }
                    var exc_rate = $("#cur_" + i + "_" + eh).val();
                    if (exc_rate != undefined) {
                        xml_exp += "<EXC_RATE>" + exc_rate + "</EXC_RATE>";
                    }
                    else {
                        xml_exp += "<EXC_RATE>" + 0 + "</EXC_RATE>";
                    }
                    if (($("#ddlrem_" + i + "_" + eh).find("option:selected").text() == "Corporate Credit Card") || ($("#ddlrem_" + i + "_" + eh).find("option:selected").text() == "Personal Credit Card")) {
                        var rupees = $("#rupees_txt" + i + "_" + eh).val();
                        if (rupees != undefined) {
                            xml_exp += "<INDIAN_CURRENCY>" + rupees + "</INDIAN_CURRENCY>";
                        }
                        else {
                            xml_exp += "<INDIAN_CURRENCY>" + 0 + "</INDIAN_CURRENCY>";
                        }
                        var dollar = $("#dollar_txt" + i + "_" + eh).val();
                        if (dollar != undefined) {
                            xml_exp += "<DOLLAR_CURRENCY>" + dollar + "</DOLLAR_CURRENCY>";
                        }
                        else {
                            xml_exp += "<DOLLAR_CURRENCY>" + 0 + "</DOLLAR_CURRENCY>";
                        }
                    }
                    else {
                        var rupees = $("#rupees_" + i + "_" + eh).html();
                        if (rupees != undefined) {
                            xml_exp += "<INDIAN_CURRENCY>" + rupees + "</INDIAN_CURRENCY>";
                        }
                        else {
                            xml_exp += "<INDIAN_CURRENCY>" + 0 + "</INDIAN_CURRENCY>";
                        }
                        var dollar = $("#dollar_" + i + "_" + eh).html();
                        if (dollar != undefined) {
                            xml_exp += "<DOLLAR_CURRENCY>" + dollar + "</DOLLAR_CURRENCY>";
                        }
                        else {
                            xml_exp += "<DOLLAR_CURRENCY>" + 0 + "</DOLLAR_CURRENCY>";
                        }
                    }
                    var IS_CR = $("#ddlrem_" + i + "_" + eh).val();
                    if (IS_CR != 0 && IS_CR != undefined && IS_CR != "") {
                        xml_exp += "<REIM_TYPE>" + IS_CR + "</REIM_TYPE>";
                    }
                    else {
                        xml_exp += "<REIM_TYPE>" + 0 + "</REIM_TYPE>";
                    }
                    var IS_SUP = $("#IS_SUP" + i + "" + eh).html();
                    if (IS_SUP != "" && IS_SUP != undefined) {
                        if (IS_SUP == "Y") {
                            xml_exp += "<IS_SUPPORT>" + 1 + "</IS_SUPPORT>";
                        }
                        else {
                            xml_exp += "<IS_SUPPORT>" + 0 + "</IS_SUPPORT>";
                        }
                    }
                    else {
                        xml_exp += "<IS_SUPPORT>" + 0 + "</IS_SUPPORT>";
                    }
                    if (eh != 2) {
                        var SUPPORT_DOC = $("#ddl_SUP_" + i + "_" + eh).val();
                        if (SUPPORT_DOC != "") {
                            xml_exp += "<SUPPORT_DOC>" + SUPPORT_DOC + "</SUPPORT_DOC>";
                        }
                        else {
                            xml_exp += "<SUPPORT_DOC>N</SUPPORT_DOC>";
                        }
                    }
                    else {
                        xml_exp += "<SUPPORT_DOC>0</SUPPORT_DOC>";
                    }


                    var particular_SUP = $("#particular_SUP_" + i + "_" + eh).val();
                    xml_exp += "<SUPPORTING_REMARK>" + particular_SUP + "</SUPPORTING_REMARK>";
                    var OTHER_REMARK = $("#other_remark_" + i + "_" + eh).val();
                    xml_exp += "<OTHER_REMARK>" + OTHER_REMARK + "</OTHER_REMARK>";
                    xml_exp += "<fk_travel_date>" + fdate + "</fk_travel_date>";
                    if (e_name != "Incidental") {
                        xml_exp += "<TAX>" + $("#Tax_" + i + "_" + eh).val() + "</TAX>";
                    }
                    xml_exp += "</ROW>";
                }
            }
            var xml_Local_data = getLocalRows(i, fdate);
            xml_exp += xml_Local_data;
            //Local Travel XML

            var Loc_rowIndex = document.getElementById("tbl_Local_Travel" + (i) + "").rows.length;
            for (var j = 0; j < Loc_rowIndex - 1; j++) {
                if ($("#txt_Date" + (i) + "" + (j + 1) + "").val().trim() != "" || $("#txt_From" + (i) + "" + (j + 1) + "").val().trim() != "") {
                    Local_travel += "<ROW>";
                    Local_travel += "<FK_HDR_ID>" + $("#txt_pk_id").val() + "</FK_HDR_ID>";
                    Local_travel += "<TRAVEL_DATE>" + $("#txt_Date" + (i) + "" + (j + 1) + "").val().trim() + "</TRAVEL_DATE>";
                    Local_travel += "<TRAVEL_FROM>" + $("#txt_From" + (i) + "" + (j + 1) + "").val().trim() + "</TRAVEL_FROM>";
                    Local_travel += "<TRAVEL_TO>" + $("#txt_To" + (i) + "" + (j + 1) + "").val().trim() + "</TRAVEL_TO>";
                    Local_travel += "<TRAVEL_MODE>" + $("#txt_Travel_Mode" + (i) + "" + (j + 1) + "").val().trim() + "</TRAVEL_MODE>";
                    if ($("#txt_Expenses" + (i) + "" + (j + 1) + "").val().trim() != "") {
                        Local_travel += "<EXPENSES>" + $("#txt_Expenses" + (i) + "" + (j + 1) + "").val().trim() + "</EXPENSES>";
                    }
                    else {
                        Local_travel += "<EXPENSES>0</EXPENSES>";
                    }
                    if ($("#txt_L_Tax" + (i) + "" + (j + 1) + "").val().trim() != "") {
                        Local_travel += "<TAX>" + $("#txt_L_Tax" + (i) + "" + (j + 1) + "").val().trim() + "</TAX>";
                    }
                    else {
                        Local_travel += "<TAX>0</TAX>";
                    }
                    Local_travel += "<REMARK>" + $("#txt_Remark" + (i) + "" + (j + 1) + "").val().trim() + "</REMARK>";
                    Local_travel += "<BILL_STATUS>" + $("#txt_Bill" + (i) + "" + (j + 1) + "").val().trim() + "</BILL_STATUS>";
                    Local_travel += "<FK_TRAVEL_DATE>" + fdate + "</FK_TRAVEL_DATE>";
                    /****************************************************************************************/
                    if ($("#txt_ReimType" + (i) + "_" + (j + 1) + "").val().trim() != "") {
                        Local_travel += "<REIM_TYPE>" + $("#txt_ReimType" + (i) + "_" + (j + 1) + "").val().trim() + "</REIM_TYPE>";
                    }
                    else {
                        Local_travel += "<REIM_TYPE>0</REIM_TYPE>";
                    }

                    if ($("#txt_L_Rate" + (i) + "" + (j + 1) + "").val().trim() != "" && !isNaN($("#txt_L_Rate" + (i) + "" + (j + 1) + "").val())) {
                        Local_travel += "<CONVERSION_RATE>" + $("#txt_L_Rate" + (i) + "" + (j + 1) + "").val().trim() + "</CONVERSION_RATE>";
                    }
                    else {
                        Local_travel += "<CONVERSION_RATE>0</CONVERSION_RATE>";
                    }

                    if ($("#txt_ReimType" + (i) + "_" + (j + 1) + " option:selected").text() == "Corporate Credit Card" || $("#txt_ReimType" + (i) + "_" + (j + 1) + " option:selected").text() == "Personal Credit Card") {
                        var base_e_amt = $("#txt_L_BAmount" + (i) + "" + (j + 1) + "").val();
                        var inr_e_amt = $("#txt_L_Amount" + (i) + "" + (j + 1) + "").val();
                        if (base_e_amt == "" || base_e_amt == undefined) {
                            base_e_amt = 0;
                        }
                        if (inr_e_amt == "" || inr_e_amt == undefined) {
                            inr_e_amt = 0;
                        }
                        Local_travel += "<BASE_EXP_AMOUNT>" + base_e_amt + "</BASE_EXP_AMOUNT>";
                        Local_travel += "<INR_EXP_AMOUNT>" + inr_e_amt + "</INR_EXP_AMOUNT>";
                    }
                    else {
                        var base_e_amt = $("#spn_L_BAmount" + (i) + "" + (j + 1) + "").html();
                        var inr_e_amt = $("#spn_L_Amount" + (i) + "" + (j + 1) + "").html();
                        if (base_e_amt == "" || base_e_amt == undefined) {
                            base_e_amt = 0;
                        }
                        if (inr_e_amt == "" || inr_e_amt == undefined) {
                            inr_e_amt = 0;
                        }
                        Local_travel += "<BASE_EXP_AMOUNT>" + base_e_amt + "</BASE_EXP_AMOUNT>";
                        Local_travel += "<INR_EXP_AMOUNT>" + inr_e_amt + "</INR_EXP_AMOUNT>";
                    }
                    /****************************************************************************************/
                    Local_travel += "</ROW>";
                }
            }
        }

        Local_travel += "</ROWSET>";
        $("#txt_LocTrav_XML").val(Local_travel);
        xml_dtl += "</ROWSET>";
        xml_exp += "</ROWSET>";
        var lastRow1 = $('#uploadTable tr').length;
        XMLFILE = "<ROWSET>";
        if (lastRow1 > 1) {
            for (var l = 0; l < lastRow1 - 1; l++) {
                var firstCol = $("#uploadTable tr")[l + 1].cells[0].innerText;
                var SecondCol = $("#uploadTable tr")[l + 1].cells[1].innerText;
                XMLFILE += "<ROW>";
                XMLFILE += "<OBJECT_TYPE>FOREIGN TRAVEL EXPENSE</OBJECT_TYPE>";
                XMLFILE += "<OBJECT_VALUE>" + $("#spn_req_no").html() + "</OBJECT_VALUE>";
                XMLFILE += "<DOCUMENT_TYPE>" + firstCol + "</DOCUMENT_TYPE>";
                XMLFILE += "<FILENAME>" + SecondCol + "</FILENAME>";
                XMLFILE += "</ROW>";
            }
        }
        XMLFILE += "</ROWSET>";


        $("#txt_Document_Xml").val(XMLFILE);
        $("#txt_sub_xml_data").val(xml_exp);
        $("#txt_xml_data").val(xml_dtl);
        $("#divIns").show();
        //alert('Done');
        return true;

        //        }
        //        else {
        //$("#divIns").hide();
        //            return false;
        //        }
    }
    catch (ex) {
        alert(ex);
        $("#divIns").hide();
        xml_exp = "<ROWSET></ROWSET>";
        xml_dtl = "<ROWSET></ROWSET>";
        XMLFILE = "<ROWSET></ROWSET>";
        Local_travel = "<ROWSET></ROWSET>";
    }
}

function show_record() {
    var Desig = $("#span_grade").text();
    Foreign_Travel_Request_Modification.show_Policy(Desig, getPolicySucceeded)
}

function getPolicySucceeded(response) {
    $("#div_header").html(response.value);
    $('#data-table1').dataTable();
}

//Add Quotation Details
function newRow(table_row_id) {
    try {
        var Html = "";
        var rowid = "";
        var firstDate = $("#spn_date" + table_row_id).html();
        var rowIndex = document.getElementById("tbl_Local_Travel" + (table_row_id) + "").rows.length;
        for (var i = 0; i < rowIndex - 1; i++) {
            //if ($("#txt_Date" + (table_row_id) + "" + (i + 1) + "").val() == "") {
            //    alert("Please Select Date at Row" + (i + 1) + ".!");
            //    $("#txt_Date" + (table_row_id) + "" + (i + 1) + "").focus();
            //    return false;
            //}
            if ($("#txt_ReimType" + (table_row_id) + "_" + (i + 1) + " option:selected").index() < 1) {
                alert("Please Select Reimbursement Type For Conveyance Detail on " + firstDate + " at Row No : " + (i + 1) + ".!");
                $("#txt_ReimType" + (table_row_id) + "_" + (i + 1) + "").focus();
                return false;
            }
            else if ($("#txt_From" + (table_row_id) + "" + (i + 1) + "").val() == "") {
                alert("Please Enter Route Travelled From on " + firstDate + " at Row No : " + (i + 1) + ".!");
                $("#txt_From" + (table_row_id) + "" + (i + 1) + "").focus();
                return false;
            }
            else if ($("#txt_To" + (table_row_id) + "" + (i + 1) + "").val() == "") {
                alert("Please Enter Route Travelled To on " + firstDate + " at Row No : " + (i + 1) + ".!");
                $("#txt_To" + (table_row_id) + "" + (i + 1) + "").focus();
                return false;
            }

            else if ($("#txt_Travel_Mode" + (table_row_id) + "" + (i + 1) + "").val() == "") {
                alert("Please Enter Mode of Travel on " + firstDate + " at Row No : " + (i + 1) + ".!");
                $("#txt_Travel_Mode" + (table_row_id) + "" + (i + 1) + "").focus();
                return false;
            }
            else if ($("#txt_Expenses" + (table_row_id) + "" + (i + 1) + "").val() == "" && $("#txt_ReimType" + (table_row_id) + "_" + (i + 1) + " option:selected").text() != "Corporate Credit Card" && $("#txt_ReimType" + (table_row_id) + "_" + (i + 1) + " option:selected").text() != "Personal Credit Card") {
                alert("Please Enter Expense amount in currency spent on " + firstDate + " at Row No :" + (i + 1) + ".!");
                $("#txt_Expenses" + (table_row_id) + "" + (i + 1) + "").focus();
                return false;
            }
            //else if ($("#txt_L_Tax" + (table_row_id) + "" + (i + 1) + "").val() == "") {
            //    alert("Please Enter Tax at Row" + (i + 1) + ".!");
            //    $("#txt_L_Tax" + (table_row_id) + "" + (i + 1) + "").focus();
            //    return false;
            //}
            else if ($("#txt_ReimType" + (table_row_id) + "_" + (i + 1) + " option:selected").text() != "Corporate Credit Card" && $("#txt_ReimType" + (table_row_id) + "_" + (i + 1) + " option:selected").text() != "Personal Credit Card" && $("#txt_L_Rate" + (table_row_id) + "" + (i + 1) + "").val() == "") {
                alert("Please Enter Exchange For Conveyance Detail on " + firstDate + " at Row No :" + (i + 1) + ".!");
                $("#txt_L_Rate" + (table_row_id) + "_" + (i + 1) + "").focus();
                return false;
            }
            else if (($("#txt_ReimType" + (table_row_id) + "_" + (i + 1) + " option:selected").text() == "Corporate Credit Card" || $("#txt_ReimType" + (table_row_id) + "_" + (i + 1) + " option:selected").text() == "Personal Credit Card") && $("#txt_L_BAmount" + (table_row_id) + "" + (i + 1) + "").val() == "") {
                alert("Please Enter Exchange Amount For Conveyance Detail  on " + firstDate + " at Row No :" + (i + 1) + ".!");
                $("#txt_L_BAmount" + (table_row_id) + "_" + (i + 1) + "").focus();
                return false;
            }
            else if (($("#txt_ReimType" + (table_row_id) + "_" + (i + 1) + " option:selected").text() == "Corporate Credit Card" || $("#txt_ReimType" + (table_row_id) + "_" + (i + 1) + " option:selected").text() == "Personal Credit Card") && $("#txt_L_Amount" + (table_row_id) + "" + (i + 1) + "").val() == "") {
                alert("Please Enter INR Amount For Conveyance Detail  on " + firstDate + " at Row No :" + (i + 1) + ".!");
                $("#txt_L_Amount" + (table_row_id) + "_" + (i + 1) + "").focus();
                return false;
            }

        }
        var dropData = Foreign_Travel_Request.getDropDownData(1);
        var Selected_Date = $("#txt_Date" + (table_row_id) + "1").val();
        Html = '<tr>';
        Html += '<td><i id=add' + (table_row_id) + '' + (rowIndex) + ' class="fa fa-fw m-r-10 pull-left f-s-18 fa-plus" onclick=newRow(' + (table_row_id) + ')></i></td>';
        Html += '<td><i id=delete' + (table_row_id) + '' + (rowIndex) + ' class="fa fa-fw m-r-10 pull-left f-s-18 fa-trash" onclick=delete_Row(' + (table_row_id) + ',' + (rowIndex) + ')></i></td>';
        Html += '<td style="display:none"><input type="text"  id=txt_Date' + (table_row_id) + '' + (rowIndex) + '  readonly class="form-control input-sm" style="width:121%;background-color:#ffffff;" /></td>';
        Html += '<td><select id=txt_ReimType' + (table_row_id) + '_' + (rowIndex) + ' class="form-control input-sm" onchange="return change_detail_flag(' + table_row_id + ',' + rowIndex + ');" >' + dropData.value + '</select></td>';
        Html += '<td><input type="text"  id=txt_From' + (table_row_id) + '' + (rowIndex) + ' class="form-control input-md"  /></td>';
        Html += '<td><input type="text"  id=txt_To' + (table_row_id) + '' + (rowIndex) + ' class="form-control input-md"  /></td>';
        Html += '<td><input type="text"  id=txt_Travel_Mode' + (table_row_id) + '' + (rowIndex) + ' class="form-control input-md"  /></td>';
        Html += '<td><input type="text"  id=txt_Expenses' + (table_row_id) + '' + (rowIndex) + ' class="form-control input-sm numbersOnly" onkeyup="calculate_Amount();"   /></td>';
        Html += '<td><input type="text"  id=txt_L_Tax' + (table_row_id) + '' + (rowIndex) + ' class="form-control input-sm numbersOnly" onkeyup="calculate_Amount();"  /></td>';
        Html += "<td><input id='txt_L_Rate" + (table_row_id) + "" + rowIndex + "' type='text' class='form-control input-sm numbersOnly' onkeyup='calculate_Amount();'/></td>";
        Html += "<td><span id='spn_L_BAmount" + table_row_id + "" + rowIndex + "'>0</span><input id='txt_L_BAmount" + (table_row_id) + "" + rowIndex + "' type='text' class='form-control input-sm numbersOnly' onkeyup='calculate_Amount()' style='display:none'/></td>";
        Html += "<td><span id='spn_L_Amount" + table_row_id + "" + rowIndex + "'>0</span><input id='txt_L_Amount" + (table_row_id) + "" + rowIndex + "' type='text' class='form-control input-sm numbersOnly' onkeyup='calculate_Amount()' style='display:none'/></td>";
        Html += '<td><input type="text"  id=txt_Remark' + (table_row_id) + '' + (rowIndex) + ' class="form-control input-md"  /></td>';
        Html += '<td><select id=txt_Bill' + (table_row_id) + '' + (rowIndex) + '  class="form-control input-md" onchange="return change_detail_flag(' + table_row_id + ',' + rowIndex + ');" ><option Value="Y">Yes</option><option Value="N">No</option></select></td>';
        Html += '</tr>';
        $("#tbl_Local_Travel" + table_row_id).append(Html);
        $("#txt_Date" + (table_row_id) + "" + (rowIndex) + "").val($("#txt_Date" + (table_row_id) + "1").val());
        $("#txt_Remark" + (table_row_id) + "" + (rowIndex) + "").val("NA");
        allowOnlyNumbers();
        change_detail_flag(table_row_id, rowIndex);
    }
    catch (Ex) {
        alert(Ex);
    }
}

function delete_Row(table_index, RowIndex) {
    try {
        var Row_c = 0;
        var Row_c = RowIndex;
        var tbl = document.getElementById("tbl_Local_Travel" + (table_index) + "");
        var lastRow = tbl.rows.length;
        if (lastRow <= 2) {
            alert("You have to Enter atleast one record..!");
            return false;
        }
        for (var contolIndex = RowIndex; contolIndex < lastRow - 1; contolIndex++) {
            document.getElementById("add" + (table_index) + "" + (parseInt(contolIndex) + 1)).onclick = new Function("newRow(" + table_index + ")");
            document.getElementById("add" + (table_index) + "" + (parseInt(contolIndex) + 1)).id = "Add" + (table_index) + "" + contolIndex;
            document.getElementById("delete" + (table_index) + "" + (parseInt(contolIndex) + 1)).onclick = new Function("deleteRow(" + (table_index) + "," + (Row_c) + ")");
            document.getElementById("delete" + (table_index) + "" + (parseInt(contolIndex) + 1)).id = "delete" + (table_index) + "" + contolIndex;
            document.getElementById("txt_Date" + (table_index) + "" + (parseInt(contolIndex) + 1)).id = "txt_Date" + (table_index) + "" + contolIndex;
            document.getElementById("txt_From" + (table_index) + "" + (parseInt(contolIndex) + 1)).id = "txt_From" + (table_index) + "" + contolIndex;
            document.getElementById("txt_To" + (table_index) + "" + (parseInt(contolIndex) + 1)).id = "txt_To" + (table_index) + "" + contolIndex;
            document.getElementById("txt_Travel_Mode" + (table_index) + "" + (parseInt(contolIndex) + 1)).id = "txt_Travel_Mode" + (table_index) + "" + contolIndex;
            document.getElementById("txt_Expenses" + (table_index) + "" + (parseInt(contolIndex) + 1)).onkeyup = new Function("calculate_Amount()");
            document.getElementById("txt_Expenses" + (table_index) + "" + (parseInt(contolIndex) + 1)).id = "txt_Expenses" + (table_index) + "" + contolIndex;
            document.getElementById("txt_L_Tax" + (table_index) + "" + (parseInt(contolIndex) + 1)).onkeyup = new Function("calculate_Amount()");
            document.getElementById("txt_L_Tax" + (table_index) + "" + (parseInt(contolIndex) + 1)).id = "txt_L_Tax" + (table_index) + "" + contolIndex;
            document.getElementById("txt_Remark" + (table_index) + "" + (parseInt(contolIndex) + 1)).id = "txt_Remark" + (table_index) + "" + contolIndex;
            document.getElementById("txt_Bill" + (table_index) + "_" + (parseInt(contolIndex) + 1)).onchange = new Function("set_Do_Flag(" + (table_index) + "," + Row_c + ")");
            document.getElementById("txt_Bill" + (table_index) + "" + (parseInt(contolIndex) + 1)).id = "txt_Bill" + (table_index) + "" + contolIndex;
        }
        tbl.deleteRow(RowIndex);
        calculate_Amount();
    }
    catch (Ex) { alert(Ex); }
}

//Multiple row for Multiple Expenses

function Add_Multiple_Row(row_id) {
    try {
        tab_Counter_id = row_id;
        var rowIndex = document.getElementById("tab_Exp" + (row_id) + "").rows.length;
        var exc_rate = 'a';
        var rws = $("[name='travel']");
        for (var index = 1; index <= rws.length; index++) {
            if ($("#radio" + index).is(':checked')) {
                exc_rate = $("#exc_rate" + index).val();
            }
        }
        if ($("#ddlCountry").val() != 0 && $("#ddlCountry").val() != undefined) {
            var country_id = $("#ddlCountry").val();
            Foreign_Travel_Request_Modification.getmultiplerows(row_id, rowIndex, country_id, $("#span_grade").text(), $("#ddlCity" + (row_id) + "").val(), callback_MultipleDataRows);
        }
    }
    catch (Ex) {
        alert(Ex);
    }
}

function callback_MultipleDataRows(response) {
    try {
        if (response.value != "") {
            $("#tab_Exp" + tab_Counter_id + "").append(response.value);
        }
    }
    catch (Ex) {
        alert(Ex);
    }
}

function Delete_Multiple_Row(table_index, RowIndex) {
    var counter = 0;
    counter = RowIndex;
    try {
        var tbl = document.getElementById("tab_Exp" + (table_index) + "");
        var lastRow = tbl.rows.length;
        if (lastRow <= 4) {
            alert("You have to Enter atleast one record..!");
            return false;
        }
        for (var contolIndex = RowIndex; contolIndex < lastRow - 1; contolIndex++) {
            document.getElementById("Madd" + (table_index) + "" + (parseInt(contolIndex) + 1)).onclick = new Function("Add_Multiple_Row(" + table_index + ")");
            document.getElementById("Madd" + (table_index) + "" + (parseInt(contolIndex) + 1)).id = "Madd" + (table_index) + "" + contolIndex;
            document.getElementById("Mdelete" + (table_index) + "" + (parseInt(contolIndex) + 1)).onclick = new Function("Delete_Multiple_Row(" + (table_index) + "," + counter + ")");
            document.getElementById("Mdelete" + (table_index) + "" + (parseInt(contolIndex) + 1)).id = "Mdelete" + (table_index) + "" + contolIndex;
            document.getElementById("expense_id" + (table_index) + "_" + (parseInt(contolIndex) + 1)).id = "expense_id" + (table_index) + "_" + contolIndex;
            document.getElementById("IS_SUP" + (table_index) + "" + (parseInt(contolIndex) + 1)).id = "IS_SUP" + (table_index) + "" + contolIndex;
            document.getElementById("ddlrem_" + (table_index) + "_" + (parseInt(contolIndex) + 1)).onchange = new Function("return change_flag(" + (table_index) + "," + counter + ")");
            document.getElementById("ddlrem_" + (table_index) + "_" + (parseInt(contolIndex) + 1)).id = "ddlrem_" + (table_index) + "_" + contolIndex;
            document.getElementById("reim_val" + (table_index) + "_" + (parseInt(contolIndex) + 1)).id = "reim_val" + (table_index) + "_" + contolIndex;
            document.getElementById("D_ALLOW_" + (table_index) + "_" + (parseInt(contolIndex) + 1)).id = "D_ALLOW_" + (table_index) + "_" + contolIndex;
            document.getElementById("txt_Expense_Amt" + (table_index) + "_" + (parseInt(contolIndex) + 1)).id = "txt_Expense_Amt" + (table_index) + "_" + contolIndex;
            document.getElementById("hlimit" + (table_index) + "_" + (parseInt(contolIndex) + 1)).id = "hlimit" + (table_index) + "_" + contolIndex;
            document.getElementById("Tax_" + (table_index) + "_" + (parseInt(contolIndex) + 1)).id = "Tax_" + (table_index) + "_" + contolIndex;
            document.getElementById("cur_" + (table_index) + "_" + (parseInt(contolIndex) + 1)).id = "cur_" + (table_index) + "_" + contolIndex;
            document.getElementById("dollar_" + (table_index) + "_" + (parseInt(contolIndex) + 1)).id = "dollar_" + (table_index) + "_" + contolIndex;
            document.getElementById("dollar_txt" + (table_index) + "_" + (parseInt(contolIndex) + 1)).id = "dollar_txt" + (table_index) + "_" + contolIndex;
            document.getElementById("rupees_" + (table_index) + "_" + (parseInt(contolIndex) + 1)).id = "rupees_" + (table_index) + "_" + contolIndex;
            document.getElementById("rupees_txt" + (table_index) + "_" + (parseInt(contolIndex) + 1)).id = "rupees_txt" + (table_index) + "_" + contolIndex;
            document.getElementById("ddl_SUP_" + (table_index) + "_" + (parseInt(contolIndex) + 1)).onchange = new Function("enable_disable_field(" + (table_index) + "," + counter + ")");
            document.getElementById("ddl_SUP_" + (table_index) + "_" + (parseInt(contolIndex) + 1)).id = "ddl_SUP_" + (table_index) + "_" + contolIndex;
            document.getElementById("particular_SUP_" + (table_index) + "_" + (parseInt(contolIndex) + 1)).id = "particular_SUP_" + (table_index) + "_" + contolIndex;
            document.getElementById("f_other_" + (table_index) + "_" + (parseInt(contolIndex) + 1)).id = "f_other_" + (table_index) + "_" + contolIndex;
            document.getElementById("other_remark_" + (table_index) + "_" + (parseInt(contolIndex) + 1)).id = "other_remark_" + (table_index) + "_" + contolIndex;
            document.getElementById("fk_other_remark_" + (table_index) + "_" + (parseInt(contolIndex) + 1)).id = "fk_other_remark_" + (table_index) + "_" + contolIndex;
        }
        tbl.deleteRow(RowIndex);
        calculate_Amount();
    }
    catch (Ex) { alert(Ex); }
}

function get_Limit(tab_id, row_index) {
    try {

        var data = "";
        data = tab_id + "_" + row_index;
        var rowIndex = count = index = 0;
        rowIndex = document.getElementById("tab_Exp" + (tab_id) + "").rows.length;
        for (var j = 0; j < rowIndex - 1; j++) {
            if ($("#expense_id" + tab_id + "_" + (j + 1) + "").val() == $("#expense_id" + tab_id + "_" + row_index + "").val() && (j + 1) != row_index) {
                if ($("#ddlrem_" + tab_id + "_" + (j + 1) + "").val() == $("#ddlrem_" + tab_id + "_" + row_index + "").val() && (j + 1) != row_index) {
                    count++;
                }
            }
        }
        if (count != "0") {
            alert("Please Select other Reimbursement Type at Row " + (row_index) + "!")
            $("#ddlrem_" + tab_id + "_" + row_index + "").val(0);
            return false;
        }
        if ($("#expense_id" + tab_id + "_" + row_index + "").val() != "0" && ($("#ddlCountry").val() != "0")) {
            Foreign_Travel_Request_Modification.get_inc_limits($("#ddlCountry").val(), $("#span_grade").text(), $("#expense_id" + tab_id + "_" + row_index + "").find('option:selected').text(), data, $("#ddlCity" + tab_id + "").val(), getexp_limit);
        }
    }
    catch (Ex) {
        alert(Ex);
    }
}

function getexp_limit(response) {
    var data = (response.value).split("@@");
    $("#hlimit" + data[0] + "").val(data[1]);
}

function downloadfiles(index) {
    try {
        var app_path = $("#app_Path").val();
        var req_no = $("#spn_req_no").text();
        window.open(app_path + '/Common/FileDownload.aspx?indentno=' + req_no + '&filename=' + index + '&filetag=', 'Download','left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
    }
    catch (Ex) {
        alert(Ex);
    }
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
    var Document_Name = $("#doctype").find("option:selected").text();
    var tbl = $("#uploadTable");
    var lastRow = $('#uploadTable tr').length;
    var html1 = "<tr><td><input class='hidden' type='text' name='txt_Country_Add" + lastRow + "' id='txt_Document_Name" + lastRow + "' value=" + Document_Name + " readonly ></input>" + Document_Name + "</td>";
    var html2 = "<td><input class='hidden' type='text' name='txt_Region_Add" + lastRow + "' id='txt_Document_File" + lastRow + "' value=" + name + " readonly ></input><a id='a_downloadfiles" + lastRow + "' style='cursor: pointer' onclick=\"return downloadfiles('" + name + "');\" >" + name + "</a></td>";
    var html3 = "<td><i id='del" + lastRow + "' class='glyphicon glyphicon-trash' align='center' onclick=\"return deletefile(" + (lastRow) + ");\" ></td></tr>";
    var htmlcontent = $(html1 + "" + html2 + "" + html3);
    $('#uploadTable').append(htmlcontent);
    $("#ddl_Document").val('');
}

function download_files(index) {

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
            if (contolIndex == RowIndex) {
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

function set_Do_Flag(tab_id, row_ID) {
    if ($("#txt_Bill" + tab_id + "" + row_ID + "").val() == "N" && $("#txt_Bill" + tab_id + "" + row_ID + "").val() != "") {
        $("#ddl_SUP_" + tab_id + "_1").val("N");
        $("#particular_SUP_" + tab_id + "_1").hide();
        $("#f_other_" + tab_id + "_1").hide();
        $("#ddl_SUP_" + (tab_id) + "_1").attr("disabled", true);
    }
    else {
        $("#ddl_SUP_" + tab_id + "_1").val("Y");
        $("#particular_SUP_" + tab_id + "_1").show();
        $("#f_other_" + tab_id + "_1").show();
        $("#ddl_SUP_" + (tab_id) + "_1").attr("disabled", true);
    }
}

function Supporting_Doc_Check(rowid) {
    try {
        var loc_expense_amt = conversion_rate = local_tab = 0;
        local_tab = document.getElementById("tbl_Local_Travel" + (rowid) + "").rows.length;
        for (var j = 0; j < local_tab - 1; j++) {
            if (($("#txt_Bill" + (rowid) + "" + (j + 1) + "").val() != "0") && ($("#txt_Bill" + (rowid) + "" + (j + 1) + "").val() == "N") && ($("#txt_Expenses" + (rowid) + "" + (j + 1) + "").val() != "") && (($("#txt_Expenses" + (rowid) + "" + (j + 1) + "").val() != "0"))) {
                loc_expense_amt = parseFloat(parseFloat(loc_expense_amt) + parseFloat($("#txt_Expenses" + (rowid) + "" + (j + 1) + "").val())).toFixed(3);
            }
        }
        conversion_rate = parseFloat(parseFloat(loc_expense_amt) / parseFloat($("#cur_" + (rowid) + "_1").val())).toFixed(3);
        return conversion_rate;
    }
    catch (Ex) {
        alert(Ex);
    }

}

function calculate_Local_Amount()
{
    var base_rate = $("#exch_rate").val();
    var rws = $("[name='rs']");
    for (var tab_id = 1; tab_id <= rws.length; tab_id++) {
        local_tab = document.getElementById("tbl_Local_Travel" + (tab_id) + "").rows.length;
        var day_total = 0;
        for (var row_id = 1; row_id <= local_tab - 1; row_id++) {
            var txt_Expenses = $("#txt_Expenses" + tab_id + "" + row_id).val();
            var txt_L_Tax = $("#txt_L_Tax" + tab_id + "" + row_id).val();
            var txt_L_Rate = $("#txt_L_Rate" + tab_id + "" + row_id).val();
            if (txt_Expenses == "" || isNaN(txt_Expenses) || txt_Expenses == undefined)
            {
                txt_Expenses = 0;
            }
            if (txt_L_Tax == "" || isNaN(txt_L_Tax) || txt_L_Tax == undefined) {
                txt_L_Tax = 0;
            }
            if (txt_L_Rate == "" || isNaN(txt_L_Rate) || txt_L_Rate == undefined) {
                txt_L_Rate = 0;
            }

            var t_exp = parseFloat(txt_Expenses) + parseFloat(txt_L_Tax);
            var tot_base_exp = 0;
            var total_inr_exp = 0;
            if (parseFloat(t_exp) > 0 && parseFloat(txt_L_Rate) > 0) {
                tot_base_exp = parseFloat(parseFloat(t_exp) / parseFloat(txt_L_Rate)).toFixed(2);
                total_inr_exp = Math.round(parseFloat(tot_base_exp) * parseFloat(base_rate));
            }
         
            $("#spn_L_BAmount" + tab_id + "" + row_id).text(tot_base_exp);
            $("#spn_L_Amount" + tab_id + "" + row_id).text(total_inr_exp);

            if ($("#txt_ReimType" + tab_id + "_" + row_id + " option:selected").text() != "Company Provided") {
                day_total = parseFloat(day_total) + parseFloat(tot_base_exp);
            }
            

        }
        $("#span_Local_Amount" + tab_id).html(day_total);
    }
}

function getLocalRows(tab_id,row_date)
{
    var data = "";

    try {
        var reim_data = Foreign_Travel_Request_Modification.getReimTypes(0);
        var reim_types = reim_data.value;
        var base_rate = $("#exch_rate").val();
        //var rws = $("[name='rs']");
        for (var row_Index = 1; row_Index <= reim_types.length; row_Index++) {
            local_tab = document.getElementById("tbl_Local_Travel" + (tab_id) + "").rows.length;
            var rtype=reim_types[row_Index-1];
            var day_total_base = 0;
            var day_total_inr = 0;
            var day_total_base_non = 0;
            var day_total_inr_non = 0;
            for (var row_id = 1; row_id <= local_tab - 1; row_id++) {
                var tot_base_exp = 0;
                var total_inr_exp = 0;
                var tot_base_exp_non = 0;
                var total_inr_exp_non = 0;
                if (rtype == $("#txt_ReimType" + tab_id + "_" + row_id + " option:selected").val()) {
                    if ($("#txt_ReimType" + tab_id + "_" + row_id + " option:selected").text() == "Corporate Credit Card" || $("#txt_ReimType" + tab_id + "_" + row_id + " option:selected").text() == "Personal Credit Card") {
                        if ($("#txt_Bill" + tab_id + "" + row_id).val() == "Y") {
                            tot_base_exp = $("#txt_L_BAmount" + tab_id + "" + row_id).val();
                            total_inr_exp = $("#txt_L_Amount" + tab_id + "" + row_id).val();
                        }
                        else {
                            tot_base_exp_non = $("#txt_L_BAmount" + tab_id + "" + row_id).val();
                            total_inr_exp_non = $("#txt_L_Amount" + tab_id + "" + row_id).val();
                        }
                    }
                    else {
                        var txt_Expenses = $("#txt_Expenses" + tab_id + "" + row_id).val();
                        var txt_L_Tax = $("#txt_L_Tax" + tab_id + "" + row_id).val();
                        var txt_L_Rate = $("#txt_L_Rate" + tab_id + "" + row_id).val();
                        if (txt_Expenses == "" || isNaN(txt_Expenses) || txt_Expenses == undefined) {
                            txt_Expenses = 0;
                        }
                        if (txt_L_Tax == "" || isNaN(txt_L_Tax) || txt_L_Tax == undefined) {
                            txt_L_Tax = 0;
                        }
                        if (txt_L_Rate == "" || isNaN(txt_L_Rate) || txt_L_Rate == undefined) {
                            txt_L_Rate = 0;
                        }

                        var t_exp = parseFloat(txt_Expenses) + parseFloat(txt_L_Tax);
                        if ($("#txt_Bill" + tab_id + "" + row_id).val() == "Y") {
                            if (parseFloat(t_exp) > 0 && parseFloat(txt_L_Rate) > 0) {
                                tot_base_exp = parseFloat(parseFloat(t_exp) / parseFloat(txt_L_Rate)).toFixed(2);
                                total_inr_exp = Math.round(parseFloat(tot_base_exp) * parseFloat(base_rate));
                            }
                        }
                        else {
                            if (parseFloat(t_exp) > 0 && parseFloat(txt_L_Rate) > 0) {
                                tot_base_exp_non = parseFloat(parseFloat(t_exp) / parseFloat(txt_L_Rate)).toFixed(2);
                                total_inr_exp_non = Math.round(parseFloat(tot_base_exp_non) * parseFloat(base_rate));
                            }
                        }
                    }
                }
                day_total_base = parseFloat(day_total_base) + parseFloat(tot_base_exp);
                day_total_inr = parseFloat(day_total_inr) + parseFloat(total_inr_exp);
                day_total_base_non = parseFloat(day_total_base_non) + parseFloat(tot_base_exp_non);
                day_total_inr_non = parseFloat(day_total_inr_non) + parseFloat(total_inr_exp_non);
            }
            if (parseFloat(day_total_base) > 0 || parseFloat(day_total_inr) > 0) {
                var xml_supp = generateXML(day_total_base, day_total_inr, rtype, "Y", row_date);
                data += xml_supp;
            }
            if (parseFloat(day_total_base_non) > 0 || parseFloat(day_total_inr_non) > 0) {
                var xml_supp_non = generateXML(day_total_base_non, day_total_inr_non, rtype, "N", row_date);
                data += xml_supp_non;
            }
        }
    }
    catch(ex)
    {
        data = "";
    }
    return data;
}

function generateXML(dollar,rupees,reimtype,is_supp,travel_date)
{
    var xml_exp="";
    xml_exp += "<ROW>";
    xml_exp += "<fk_FOREIGN_expense_Hdr_Id>#</fk_FOREIGN_expense_Hdr_Id>";
    xml_exp += "<fk_expense_head_id>40</fk_expense_head_id>";
    xml_exp += "<COUNTRY_CURRENCY>0</COUNTRY_CURRENCY>";
    xml_exp += "<EXC_RATE>0</EXC_RATE>";
    xml_exp += "<DOLLAR_CURRENCY>" + dollar + "</DOLLAR_CURRENCY>";
    xml_exp += "<INDIAN_CURRENCY>" + rupees + "</INDIAN_CURRENCY>";
    xml_exp += "<REIM_TYPE>" + reimtype + "</REIM_TYPE>";
    xml_exp += "<IS_SUPPORT>1</IS_SUPPORT>";
    xml_exp += "<SUPPORT_DOC>" + is_supp + "</SUPPORT_DOC>";
    xml_exp += "<SUPPORTING_REMARK>NA</SUPPORTING_REMARK>";
    xml_exp += "<fk_travel_date>" + travel_date + "</fk_travel_date>";
    xml_exp += "<TAX>0</TAX>";
    xml_exp += "</ROW>";
    return xml_exp;
}