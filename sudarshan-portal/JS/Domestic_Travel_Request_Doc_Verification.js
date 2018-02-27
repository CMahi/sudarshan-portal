var diffDays = 0;
var division = $("#span_Division").html();
$(document).ready(function () {

    var fromDate = $("#travelFromDate").html();
    var toDate = $("#travelToDate").html();
    getDiffDays(fromDate, toDate);
    getSelectDate(fromDate, toDate);
});


function chk_FromDate() {
    var fromDate = $("#travelFromDate").val();
    var toDate = $("#travelToDate").val();
    if (fromDate == "" && toDate != "") {
        alert("Please Select Travel From Date");
        $("#travelToDate").val("");
        return false;
    }
    else if (fromDate != "" && toDate == "") {
        getDiffDays(fromDate, toDate);
        getSelectDate(fromDate, toDate);
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
        getDiffDays(fromDate, toDate);
        getSelectDate(fromDate, toDate);
    }
}

function getDiffDays(fromDate, toDate) {
    if (fromDate != "" && toDate != "") {
        var fdate = getFormatedDate1(fromDate);
        var tdate = getFormatedDate1(toDate);
        var oneDay = 24 * 60 * 60 * 1000;
        var firstDate = new Date(fdate);
        var secondDate = new Date(tdate);
        diffDays = Math.round(Math.abs((firstDate.getTime() - secondDate.getTime()) / (oneDay)));
    }
}

function getSelectDate(fromDate, toDate) {
    try {
        if (fromDate != "" && toDate != "") {
            var fdate = getFormatedDate1(fromDate);
            var tdate = getFormatedDate1(toDate);

            var firstDate = new Date(fdate);
            var secondDate = new Date(tdate);
            var wiid = $("#txtWIID").val();
            Domestic_Travel_Request_Doc_Verification.get_Journey_Data(fromDate, toDate, wiid, AjaxSucceeded);
        }
    }
    catch (ex) {
        // $('#divIns').html("");
    }
}

function AjaxSucceeded(result) {
    travelAdd(result.value);
    //$('#divIns').html("");
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

function travelAdd(tableBody) {
    $("#accordion").html(tableBody);
    check_journey_Type();
}

function check_journey_Type() {

    var jt = document.getElementsByName("jt");
    var len = jt.length;
    for (var index = 1; index <= len; index++) {
        var journeyType = $("#journey_Type" + index).html().toUpperCase();

        $("#div_PlaceRoom" + index).hide();
        $("#div_HotelContact" + index).hide();
        $("#div_City" + index).hide();

        $("#div_Travel_Mode" + index).val(0);
        $("#div_Travel_Class" + index).val(0);
        $("#From_Plant" + index).val(0);
        $("#To_Plant" + index).val(0);

        $("#div_Travel_Mode" + index).hide();
        $("#div_Travel_Class" + index).hide();
        $("#div_FPlant" + index).hide();
        $("#div_TPlant" + index).hide();


        $("#From_City" + index).removeAttr("disabled");
        $("#To_City" + index).removeAttr("disabled");

        if (journeyType == "---SELECT ONE---") {
            $("#div_Travel_Mode" + index).hide();
            $("#div_Travel_Class" + index).hide();
            $("#div_FPlant" + index).hide();
            $("#div_TPlant" + index).hide();


        } else if (journeyType == "OUTSIDE PLANT") {
            $("#div_Travel_Mode" + index).show();
            $("#div_Travel_Class" + index).show();
            $("#div_FPlant" + index).hide();
            $("#div_TPlant" + index).hide();
            $("#div_PlaceRoom" + index).show();
            $("#div_HotelContact" + index).show();
            $("#div_City" + index).show();

        } else if (journeyType == "ONE DAY OUTSTATION WITHIN PLANT") {
            $("#div_Travel_Mode" + index).hide();
            $("#div_Travel_Class" + index).hide();
            $("#div_FPlant" + index).show();
            $("#div_TPlant" + index).show();
            $("#From_City" + index).attr("disabled", "disabled");
            $("#To_City" + index).attr("disabled", "disabled");
        } else if (journeyType == "OVERNIGHT STAY WITHIN PLANT") {
            $("#div_Travel_Mode" + index).hide();
            $("#div_Travel_Class" + index).hide();
            $("#div_FPlant" + index).show();
            $("#div_TPlant" + index).hide();
            $("#From_City" + index).attr("disabled", "disabled");
            $("#To_City" + index).attr("disabled", "disabled");
        }
        else if (journeyType == "ONE DAY OUTSTATION") {
            $("#div_Travel_Mode" + index).show();
            $("#div_Travel_Class" + index).show();
            $("#div_FPlant" + index).hide();
            $("#div_TPlant" + index).hide();
            $("#div_City" + index).show();
        }
    }
    //$("#exp_data" + index).html("");

}

function get_exp_data(index) {
    try {
        var desg = $("#txt_designation").val();
        var j_val = $("#journey_Type" + index).val();
        var pk_city = $("#To_City" + index).val();
        var travel_class_id = $("#Travel_Class" + index).val();
        var travel_mode_id = $("#Travel_Mode" + index).val();
        var fplant_id = $("#From_Plant" + index).val();
        var tplant_id = $("#To_Plant" + index).val();

        Domestic_Travel_Request_Doc_Verification.get_Expense_Data(desg, j_val, travel_mode_id, travel_class_id, fplant_id, tplant_id, pk_city, index, division, ExpenseSucceeded);
    }
    catch (ex) {
        alert(ex);
    }
}

function ExpenseSucceeded(response) {
    var data = (response.value).split("@@");
    $("#exp_data" + data[0]).html(data[1]);
    allowOnlyNumbers();
    calculate_Amount();
}

function get_dev_policy(index) {
    try {
        var desg = $("#txt_designation").val();
        var j_val = $("#journey_Type" + index).val();
        var travel_class_id = $("#Travel_Class" + index).val();
        var travel_mode_id = $("#Travel_Mode" + index).val();

        Domestic_Travel_Request_Doc_Verification.get_dev_policy(desg, j_val, travel_mode_id, travel_class_id, DeviationSucceeded);
    }
    catch (ex) {
        alert(ex);
    }
}

function DeviationSucceeded(response) {
    $("#dev_travel_class").html(response.value);
}

function prepare_data() {
    try {
        if ($("#empno").html() == "") {
            alert("User Data Not Found...!");
            return false;
        }
        else {
            if ($("#ddlAction option:selected").index() > 0) {
                if ($("#ddlAction option:selected").index() == 1) {
                   
                }
                else {
                    if ($("#txt_Remark").val() == "") {
                        alert("Please Enter Remark...!");
                        return false;
                    }

                }
            }
            else {
                alert("Please Select Action...!");
                return false;
            }
            return true;
        
        }
    }
    catch (exception) {
        alert(exception);
        return false;
    }
}
function change_flag(exp, index, i) {
    try {
        var data = exp + "_" + index + "_" + i;
        var fk_id = $("#ddlrem_" + data).val();
        Domestic_Travel_Request_Doc_Verification.chagable_or_not("Domestic Travel Expenses", fk_id, data, changeText);
    }
    catch (ex) {
        alert(ex);
    }
}

function changeText(response) {
    var data = (response.value).split("@@");
    $("#reim_val" + data[0]).val(data[1]);
    calculate_Amount();
}
function calculate_Amount() {
    var Hotel_Charge, Total, fTotal, non_supp, supp_tot, percent_val;
    Hotel_Charge = Total = fTotal = non_supp = supp_tot = 0;

    var d = document.getElementsByName('jt');
    for (var i = 1; i <= d.length; i++) {
        percent_val = 0;
        var exp = document.getElementsByName("eh");
        Hotel_Charge = 0;
        Total = 0;
        for (var eh = 1; eh <= exp.length; eh++) {
            var exp_name = $("#exp_name" + eh).val();
            var e_name = $("#e_name" + eh).val();
            var c_name = $("#compare_name" + eh).val();
            var chk_box = true;
            var fk_id = $("#e_fk_id" + eh).val();
            if ($("#reim_val" + fk_id + "_" + i + "_" + eh).val() == 1) {
                chk_box = false;
            }
            var supp = $("#IS_SUP" + fk_id + "_" + i + "_" + eh).html();
            if (supp == "Y") {
                supp_tot += parseFloat(exp_amt);
                if (supp_tot != undefined && supp_tot != "") {
                    $("#supp_amt_tot").val(supp_tot);
                }
            }
            var non_s = $("#ddl_SUP" + fk_id + "_" + i + "_" + eh).val();
            if (supp == "Y" && non_s == "Y") {
                non_supp += parseFloat(exp_amt);
                if (non_supp != undefined && non_supp != "") {
                    $("#supp_amt_no_cur").val(non_supp);
                }
            }

            percent_val = (parseFloat($("#supp_amt_no_cur").val()) * 100) / parseFloat($("#supp_amt_tot").val());
            $("#supp_amt_no_db").val(percent_val);
            if ($("#supp_perc_no").val != "" && $("#supp_perc_no").val != undefined && $("#supp_amt_no_db").val != "" && $("#supp_amt_no_db").val != undefined) {
                if (parseFloat($("#supp_amt_no_cur").val()) > parseFloat($("#supp_perc_no").val())) {
                    $("#dev_supp_amt").val(1);
                }
                else {
                    $("#dev_supp_amt").val(0);
                }
            }
            else {
                $("#dev_supp_amt").val(0);
            }

            var exp_amt = $("#" + e_name + "_" + i + "_" + eh).val();
            var h_exp_amt = $("#h" + e_name + "_" + i + "_" + eh).val();
            var d_allow = $("#D_ALLOW_" + e_name + "_" + i + "_" + eh).val();

            if ($("#compare_id" + eh).val() != $("#expenses" + eh).val()) {
                var c_val = $("#" + c_name + "_" + i + "_" + $("#compare_id" + eh).val()).val();
                var e_val = $("#" + e_name + "_" + i + "_" + $("#expenses" + eh).val()).val();
                if (($("#" + e_name + "_" + i + "_" + $("#compare_id" + eh).val()).val() != "" || $("#" + e_name + "_" + i + "_" + $("#compare_id" + eh).val()).val() != undefined) && ($("#" + e_name + "_" + i + "_" + $("#expenses" + eh).val()).val() != "" || $("#" + e_name + "_" + i + "_" + $("#expenses" + eh).val()).val() != undefined)) {
                    if (parseFloat(c_val) > 0 && parseFloat(e_val) > 0) {
                        alert("Please Fill Either " + exp_name + " or " + c_name.replace(/_/gi, " "));
                        $("#" + c_name + "_" + i + "_" + $("#compare_id" + eh).val()).val(0);
                        $("#" + e_name + "_" + i + "_" + $("#expenses" + eh).val()).val(0);
                        return false;
                    }
                }
            }

            if (d_allow == 0) {
                if (exp_amt != undefined && exp_amt != "") {
                    if (h_exp_amt != "") {
                        if (chk_box == false) {
                            Total += parseFloat(exp_amt);
                            if (h_exp_amt > exp_amt) {
                                $("#dev_policy_amt").val(1);
                            }
                        }
                    }
                    else {
                        if (exp_amt != undefined && exp_amt != "0") {
                            alert(exp_name + " Policy Not Available...");
                            $("#" + e_name + "_" + i + "_" + eh).val(0);
                            return false;
                        }
                    }
                }

                else {
                    $("#" + e_name + "_" + i + "_" + eh).val(0);
                }
            }
            else {
                if (exp_amt != undefined && exp_amt != "") {
                    if (chk_box == false) {
                        Total += parseFloat(exp_amt);
                    }
                }
                else {
                    $("#" + e_name + "_" + i + "_" + eh).val(0);
                }
            }
            $("#Total" + i).text(parseFloat(Total) + Hotel_Charge);
        }

        fTotal = parseFloat(fTotal) + parseFloat($("#Total" + i).text());
    }
    $("#Final_Amtt").text(fTotal);

    return true;
}

function chk_class_From(i) {
    $("#txt_f_city" + i).val("");
    if ($("#From_City" + i + " option:selected").text() == "Other") {
        $("#txt_f_city" + i).show();
    }
    else {
        $("#txt_f_city" + i).hide();
    }
}

function chk_class_To(i) {
    $("#txt_t_city" + i).val("");
    if ($("#To_City" + i + " option:selected").text() == "Other") {
        $("#placeclass" + i).html("C");
        $("#txt_t_city" + i).show();
    }
    else {
        $("#txt_t_city" + i).hide();
        var pk_city = $("#To_City" + i).val();
        Domestic_Travel_Request_Doc_Verification.get_Travel_Class(pk_city, i, ClassSucceeded);
    }
}

function ClassSucceeded(response) {
    var data = response.value;
    $("#placeclass" + data[0]).html(data[1]);
}

/***************************************************************************************************************************/

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
    if (parseFloat(filelength) == 0) {
        alert("Sorry Cannot Upload File ! File Is Empty Or File Does Not Exist");
    }
    else if (parseFloat(filelength) > 10000000) {
        alert("Sorry Cannot Upload File ! File Size Exceeded.");
    }
    else if (contentType == "application/octet-stream" && fileNameSplit[1] == "exe") {
        alert("Kindly Check File Type.");
    }
    else {
        addToClientTable(n, args.get_length());
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

    var Document_Name = "";
    var tbl = $("#uploadTable");
    var lastRow = $('#uploadTable tr').length;

    //var html1 = "<tr><td style='display:none'><input class='hidden' type='text' name='txt_Country_Add" + lastRow + "' id='txt_Document_Name" + lastRow + "' value=" + Document_Name + " readonly ></input>" + Document_Name + "</td>";
    var html2 = "<tr><td><input class='hidden' type='text' name='txt_Region_Add" + lastRow + "' id='txt_Document_File" + lastRow + "' value=" + name + " readonly ></input><a id='a_downloadfiles" + lastRow + "' style='cursor: pointer' onclick=\"return downloadfiles('" + lastRow + "');\" >" + name + "</a></td>";
    var html3 = "<td><i id='del" + lastRow + "' class='glyphicon glyphicon-trash' align='center' onclick=\"return deletefile(" + (lastRow) + ");\" ></td></tr>";
    var htmlcontent = $(html2 + "" + html3);
    $('#uploadTable').append(htmlcontent);


}

function downloadfiles(index) {
    var app_path = $("#app_Path").val();
    var req_no = $("#spn_req_no").text();
    window.open(app_path + '/Common/FileDownload.aspx?indentno=' + req_no + '&filename=' + $("#uploadTable tr")[index].cells[1].innerText + '&filetag=', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
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

function get_travel_class(rno) {
    var Travel_Mode = $("#Travel_Mode" + rno + " option:selected").val();
    Domestic_Travel_Request_Doc_Verification.get_travel_class(Travel_Mode, rno, OnTravelClass);
}

function OnTravelClass(response) {
    var data = response.value;
    sp_data = data.split("||");
    var rno = sp_data[0];
    var rcds = sp_data[1].split("@@");
    $("#Travel_Class" + rno).html("");
    for (var i = 0; i < rcds.length; i++) {
        var crec = rcds[i].split("$$");
        var cval = crec[0];
        var ctext = crec[1];
        $("#Travel_Class" + rno).append($("<option></option>").val(cval).html(ctext));
    }
}

function enable_disable() {
    $("#ddlAdv_Location").prop('selectedIndex', 0);
    if ($("#ddl_Payment_Mode option:selected").text().toUpperCase() == "CASH") {
        $("#ddlAdv_Location").removeAttr('disabled');
    }
    else {
        $("#ddlAdv_Location").attr('disabled', 'disabled');
    }
}

function show_record() {
    var search_data = $("#txt_search").val();
    var ddl = $("#ddlRecords option:selected").val();
    var desg = $("#txt_designation").val();
    var pgno = $("pageno").val();
    Domestic_Travel_Request_Doc_Verification.showall(search_data, 1, ddl, desg,division, OnshowAll);
}

function OnshowAll(response) {
    $("#div_header").html(response.value);
}

function gotopage(objButton, str) {
    try {
        var pgno = objButton.value;
        $("pageno").val(pgno);
        var strData = $("#txt_search").val();
        var desg = $("#txt_designation").val();
        Domestic_Travel_Request_Doc_Verification.showall(strData, pgno, str, desg, division, OnshowAll);
    }
    catch (exception) {

    }
}

function enable_disable_field(fk_id, index, id) {
    var ch = fk_id + "_" + index + "_" + id;
    $("#particular_SUP" + ch).val("");
    if ($("#ddl_SUP" + ch).val() == "Y") {
        $("#particular_SUP" + ch).show();
    }
    else {
        $("#particular_SUP" + ch).hide();
    }
}

function hide_show_remark() {
    $("#txt_Remark").val("");
    if ($("#ddlAction option:selected").index() < 2) {
        $("#div_remark").hide();
    }
    else {
        $("#div_remark").show();
    }
}