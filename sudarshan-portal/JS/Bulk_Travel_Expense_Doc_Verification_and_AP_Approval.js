function searchData() {
    var str = document.getElementById("txt_Search").value;
    var rpp = "0";
    var p_mode = $("#ddlPay_mode option:selected").text();
    if ($("#ddlRecords option:selected").text() != undefined) {
        rpp = $("#ddlRecords :selected").text();
        $("#ddlText1").val($("#ddlRecords option:selected").text());
    }
    else {
        rpp = $("#ddlText1").val();
    }

    Bulk_Travel_Expense_Doc_Verification_and_AP_Approval.fillSearch(p_mode, rpp, callback_Inco);
}

function callback_Inco(response) {
    document.getElementById("div_InvoiceDetails").innerHTML = response.value;
    $('#data-table1').dataTable();
}


function gotopage(objButton, str) {
    try {
        var pgno = objButton.value;
        var strData = $("#txt_Search").val();
        if (strData != undefined || strData != null) {
            Bulk_Travel_Expense_Doc_Verification_and_AP_Approval.fillGoToPage1(strData, pgno, str, callback_Inco);
        }
        else {
            Bulk_Travel_Expense_Doc_Verification_and_AP_Approval.fillGoToPage(pgno, str, callback_Inco);
        }
    }
    catch (exception) {

    }
}
function downloadfiles(req_no,file_name) {
    var app_path = $("#app_Path").val();
    //var req_no = $("#request_no_" + index).val();
    window.open(app_path + '/Common/FileDownload.aspx?indentno=' + req_no + '&filename=' + file_name + '&filetag=', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
}
function prepareData() {
    try {
        var chk = 0;
        var PK_Dispatch_Note_ID = "";
        //var d = document.getElementsByName('bulk');
        PK_Dispatch_Note_ID = $("#chk_records").val();

        $("#split_data").val(PK_Dispatch_Note_ID);

        if (PK_Dispatch_Note_ID =="") {
            alert('Select Atleast One Expense Request...!');
            return false;
        }
        else {
            //alert(PK_Dispatch_Note_ID);
            return true;
        }
    }
    catch (exception)
    { alert(exception) }
}

function Bind_Documents(row_id)
{
    var wiid = $("#header" + row_id).val();
    var proc_name = $("#pname" + row_id).val();
    Bulk_Travel_Expense_Doc_Verification_and_AP_Approval.fillDocumentDetails(proc_name,wiid, callback_Bind);
}

function callback_Bind(response)
{
   // $("#DivDocs").html(response.value);
    document.getElementById("DivDocs").innerHTML = response.value;
}


/*====================================================================================================================================*/

function getData(index) {
    $("#txtProcessID").val($("#fk_process" + index).val());
    $("#txtInstanceID").val($("#iid" + index).val());
    $("#txtWIID").val($("#wiid" + index).val());
    var wiid = $("#txtWIID").val();
    if ($("#txtProcessID").val() == 14) {      
        Bulk_Travel_Expense_Doc_Verification_and_AP_Approval.getUserInfo(wiid, callback_BindData);
        Bulk_Travel_Expense_Doc_Verification_and_AP_Approval.fillAdvanceAmount(callback_BindAdvance);
        Bulk_Travel_Expense_Doc_Verification_and_AP_Approval.fillaudittrail($("#txtProcessID").val(), $("#txtInstanceID").val(), callback_bindAudit);

        var fromDate = $("#travelFromDate" + index).val();
        var toDate = $("#travelToDate" + index).val();
        getDiffDays(fromDate, toDate);
        getSelectDate(fromDate, toDate);
    }
    else if ($("#txtProcessID").val() == 17) {
        Bulk_Travel_Expense_Doc_Verification_and_AP_Approval.getOtherExpenseUser(wiid, callback_BindData);
        Bulk_Travel_Expense_Doc_Verification_and_AP_Approval.fillRequest_data(wiid, callback_BindOther);        
        Bulk_Travel_Expense_Doc_Verification_and_AP_Approval.fillaudittrail($("#txtProcessID").val(), $("#txtInstanceID").val(), callback_bindAudit);
    }
    else {
        $("#div_header").html("");
    }
}
function callback_bindAudit(response)
{
    $("#div_Audit").html(response.value);
}

function callback_BindOther(response)
{
    var data=(response.value).split("@@");
    $("#div_req_data").html(data[0]);
    $("#spn_Total").html(data[1]);
}


function callback_BindAdvance(response) {
    var rec_data = (response.value).split("@");
    $("#div_Advance").html(rec_data[0]);
    var adv = rec_data[1];
    var exp = rec_data[2];
    if (exp > adv)
    {
        $("#spn_hdr").html("Payable Amount : ");
        $("#spn_val").html(exp - adv);        
    }
    else
    {
        $("#spn_hdr").html("Recovery Amount : ");
        $("#spn_val").html(adv - exp);
    }
}

function callback_BindMode(response) {
    var rec_data = (response.value).split("@");
    for (var i = 0; i < rec_data.length; i++) {
        var data = rec_data[i].split("||");
        if ($("#pmode").val() == data[0]) {
            $("#ddl_Payment_Mode").append($("<option selected='true'></option>").val(data[0]).html(data[1]));
        }
        else {
            $("#ddl_Payment_Mode").append($("<option></option>").val(data[0]).html(data[1]));
        }
    }
}

function callback_BindLocation(response) {
    var rec_data = (response.value).split("@");
    for (var i = 0; i < rec_data.length; i++)
    {
        var data = rec_data[i].split("||");
        if ($("#plocation").val() == data[0]) {
            $("#ddlAdv_Location").append($("<option selected='true'></option>").val(data[0]).html(data[1]));
        }
        else {
            $("#ddlAdv_Location").append($("<option></option>").val(data[0]).html(data[1]));
        }
    }
}
function callback_BindData(response) {

    var data = (response.value).split("@@");
    $("#div_header").html(data[0]);
    var sp_data = data[1].split("||");
    $("#txt_pk_id").val(sp_data[0]);
    $("#txt_Initiator").val(sp_data[1]);
    $("#Init_Email").val(sp_data[2]);
    $("#Desg_Name").html(sp_data[3]);
    $("#txt_designation").val(sp_data[4]);
    $("#txt_Approver_Email").html(sp_data[5]);
    $("#plocation").val(sp_data[6]);
    $("#pmode").val(sp_data[7]);
   
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
    if (fromDate != "" && toDate != "") {
        var fdate = getFormatedDate1(fromDate);
        var tdate = getFormatedDate1(toDate);

        var firstDate = new Date(fdate);
        var secondDate = new Date(tdate);
        var tableBody = "";
        var wiid = $("#txtWIID").val();
        Bulk_Travel_Expense_Doc_Verification_and_AP_Approval.get_Journey_Data(fromDate, toDate, wiid, AjaxSucceeded);
    }
}

function AjaxSucceeded(result) {
    travelAdd(result.value);
    check_journey_Type1();
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

function check_journey_Type1() {

    var d = document.getElementsByName("jt");
    for (var index = 1; index <= d.length; index++) {
        var journeyType = $("#journey_Type" + index + " Option:selected").text().toUpperCase();

        $("#Travel_Mode" + index).hide();
        $("#Travel_Class" + index).hide();
        $("#From_Plant" + index).hide();
        $("#To_Plant" + index).hide();

        $("#default_From" + index).show();
        $("#default_To" + index).show();

        var c = 0;
        var d = document.getElementsByName('jt');
        for (var i = 0; i < d.length; i++) {
            if (d[i].value > 0) {
                cnt = 1;
            }
        }
        if (cnt == 1) {
            $(".modetravel").show();
        }
        else {
            $(".modetravel").hide();
        }


        if (journeyType == "---SELECT ONE---") {
            $("#Travel_Mode" + index).hide();
            $("#Travel_Class" + index).hide();
            $("#From_Plant" + index).hide();
            $("#To_Plant" + index).hide();

            $("#default_From" + index).show();
            $("#default_To" + index).show();

        } else if (journeyType == "OUTSIDE PLANT") {
            $("#Travel_Mode" + index).show();
            $("#Travel_Class" + index).show();
            $("#From_Plant" + index).hide();
            $("#To_Plant" + index).hide();
            $("#default_From" + index).hide();
            $("#default_To" + index).hide();

        } else if (journeyType == "ONE DAY OUTSTATION WITHIN PLANT") {
            $("#Travel_Mode" + index).hide();
            $("#Travel_Class" + index).hide();
            $("#From_Plant" + index).show();
            $("#To_Plant" + index).show();
            $("#default_From" + index).hide();
            $("#default_To" + index).hide();
        } else if (journeyType == "OVERNIGHT STAY WITHIN PLANT") {
            $("#Travel_Mode" + index).hide();
            $("#Travel_Class" + index).hide();
            $("#From_Plant" + index).show();
            $("#To_Plant" + index).hide();
            $("#default_From" + index).hide();
            $("#default_To" + index).show();
        }
        else if (journeyType == "ONE DAY OUTSTATION") {
            $("#Travel_Mode" + index).show();
            $("#Travel_Class" + index).show();
            $("#From_Plant" + index).hide();
            $("#To_Plant" + index).hide();
            $("#default_From" + index).hide();
            $("#default_To" + index).hide();
        }
    }

}

function travelAdd(tableBody) {
    var tableHeader = "";
    tableHeader += "<th>Date</th>";
    tableHeader += "<th>Journey Type</th>";
    tableHeader += "<th class='modetravel'>Travel Mode/Plant From</th>";
    tableHeader += "<th class='modetravel'>Travel Class/Plant To</th>";
    tableHeader += "<th>From City</th>";
    tableHeader += "<th>To City</th>";
    var table = "<table class='table table-striped table-bordered table-hover' id='tbl_AddGrid'>" +
          "<thead><tr class='grey'>" + tableHeader + "</tr></thead>" +
          "<tbody>" + tableBody + "</tbody>" +
          "</table>";
    $("#div_travelDetails").empty();
    $("#div_travelDetails").append(table);

}

function check_journey_Type(index, cnt) {
    var journeyType = $("#journey_Type" + index + " Option:selected").text().toUpperCase();


    $("#Travel_Mode" + index).val(0);
    $("#Travel_Class" + index).val(0);
    $("#From_Plant" + index).val(0);
    $("#To_Plant" + index).val(0);

    $("#Travel_Mode" + index).hide();
    $("#Travel_Class" + index).hide();
    $("#From_Plant" + index).hide();
    $("#To_Plant" + index).hide();

    $("#default_From" + index).show();
    $("#default_To" + index).show();

    var c = 0;
    var d = document.getElementsByName('jt');
    for (var i = 0; i < d.length; i++) {
        if (d[i].value > 0) {
            cnt = 1;
        }
    }
    if (cnt == 1) {
        $(".modetravel").show();
    }
    else {
        $(".modetravel").hide();
    }

    $("#From_City" + index).val(0);
    $("#To_City" + index).val(0);

    $("#From_City" + index).removeAttr("disabled");
    $("#To_City" + index).removeAttr("disabled");

    if (journeyType == "---SELECT ONE---") {
        $("#Travel_Mode" + index).hide();
        $("#Travel_Class" + index).hide();
        $("#From_Plant" + index).hide();
        $("#To_Plant" + index).hide();

        $("#default_From" + index).show();
        $("#default_To" + index).show();

    } else if (journeyType == "OUTSIDE PLANT") {
        $("#Travel_Mode" + index).show();
        $("#Travel_Class" + index).show();
        $("#From_Plant" + index).hide();
        $("#To_Plant" + index).hide();
        $("#default_From" + index).hide();
        $("#default_To" + index).hide();

    } else if (journeyType == "ONE DAY OUTSTATION WITHIN PLANT") {
        $("#Travel_Mode" + index).hide();
        $("#Travel_Class" + index).hide();
        $("#From_Plant" + index).show();
        $("#To_Plant" + index).show();
        $("#default_From" + index).hide();
        $("#default_To" + index).hide();
        $("#From_City" + index).attr("disabled", "disabled");
        $("#To_City" + index).attr("disabled", "disabled");
    } else if (journeyType == "OVERNIGHT STAY WITHIN PLANT") {
        $("#Travel_Mode" + index).hide();
        $("#Travel_Class" + index).hide();
        $("#From_Plant" + index).show();
        $("#To_Plant" + index).show();
        $("#default_From" + index).hide();
        $("#default_To" + index).hide();
        $("#From_City" + index).attr("disabled", "disabled");
        $("#To_City" + index).attr("disabled", "disabled");
    }
    else if (journeyType == "ONE DAY OUTSTATION") {
        $("#Travel_Mode" + index).show();
        $("#Travel_Class" + index).show();
        $("#From_Plant" + index).hide();
        $("#To_Plant" + index).hide();
        $("#default_From" + index).hide();
        $("#default_To" + index).hide();
    }
}

function check_Outside_Plant() {

    var fromDate = $("#travelFromDate").html();
    var d = document.getElementsByName('jt');
    var len = d.length;
    if (len > 0) {
        var cnt = 0;
        var concat_data = "";
        var cls_val = "";
        for (var i = 1; i <= d.length; i++) {
            var c_var = $("#cls" + i).val();
            cls_val += c_var + "@";
            var id = $("#journey_Type" + i + " Option:selected").text().toUpperCase();
            if (id == "OUTSIDE PLANT") {
                cnt = cnt + 1;
                concat_data += i + "@";
            }
        }

        $("#tab_TrvelRequert").hide();
        $("#tab_HotelDetails").show();
        $("#tab_Expense").hide();
        var wiid = $("#txtWIID").val();
        Bulk_Travel_Expense_Doc_Verification_and_AP_Approval.get_Hotel_Booking(fromDate, len, concat_data, cls_val, wiid, hotelSucceeded);
        return true;
    }
    else {
        alert('Rows Not Found');
    }


}

function hotelSucceeded(response) {
    document.getElementById("div_hoteldetails").innerHTML = response.value;
}

function check_Final() {
    var fromDate = $("#travelFromDate").html();
    var toDate = $("#travelToDate").html();

    var d = document.getElementsByName('htl');
    var jr = document.getElementsByName('jt');
    var jt_data = "";
    var jt_val = "";
    var jt_amt = "";
    var concat_data = "";
    var to_city = "";
    var desg = $("#txt_designation").val();
    var travel_class_id = "";
    var plant_id = "";
    for (var j = 1; j <= jr.length; j++) {

        travel_class_id += $("#Travel_Class" + j).val() + "@";
        plant_id += $("#From_Plant" + j).val() + "@";
        var ct = $("#To_City" + j).val();
        to_city += ct + "@";

        var span_val = $("#spn" + j).text();
        var room = $("#roomType" + j + " option:selected").text();
     
        var journeyType = $("#journey_Type" + j + " Option:selected").text();
        var journey_val = $("#journey_Type" + j + " Option:selected").val();
        var amtt = $("#hotel_amount" + j).val();
        if ($("#hotel_amount" + j).val() != undefined && $("#hotel_amount" + j).val() != 0) {
            try {
                jt_amt += $("#hotel_amount" + j).val() + "@";
            }
            catch (ex) {
                jt_amt += "0@";
            }
        }
        else {
            jt_amt += "0@";
        }
        jt_val += journey_val + "@";

        if (j == 1) {
            jt_data = journeyType;
        }
        else {
            jt_data += "@" + journeyType;
        }
    }

    for (var i = 1; i <= d.length; i++) {

        var hotel_rno = "";
        var amt = 0;
        try {

            hotel_rno = $("#hotel_rno" + i).val();
            amt = $("#hotel_amount" + i).val();
            if (hotel_rno != undefined && amt != undefined) {
                if (i == 1) {
                    concat_data = hotel_rno + "|" + amt;
                }
                else {
                    concat_data += "@" + hotel_rno + "|" + amt;
                }
            }


        }
        catch (ex) { }
    }
    var wiid = $("#txtWIID").val();
    Bulk_Travel_Expense_Doc_Verification_and_AP_Approval.get_Final_Data(fromDate, toDate, jr.length, jt_data, concat_data, jt_val, jt_amt, to_city, desg, wiid, travel_class_id, plant_id, mainSucceeded);
    $("#tab_TrvelRequert").hide();
    $("#tab_HotelDetails").hide();
    $("#tab_Expense").show();
}

function mainSucceeded(response) {
    //alert(response.value);
    var result = response.value;
    var res = result.split("||");
    document.getElementById("div_finaldetails").innerHTML = res[0];
    $("#doa").val(res[1]);
    calculate_Amount();
}

function goto1() {
    $("#tab_TrvelRequert").show();
    $("#tab_HotelDetails").hide();
    $("#tab_Expense").hide();
    $("#tab_Doc").hide();
}

function goto2() {
    $("#tab_TrvelRequert").hide();
    $("#tab_HotelDetails").show();
    $("#tab_Expense").hide();
    $("#tab_Doc").hide();
}

function goto3() {
    $("#tab_TrvelRequert").hide();
    $("#tab_HotelDetails").hide();
    $("#tab_Expense").show();
    $("#tab_Doc").hide();
}

function goto4() {
 
    if (calculate_Amount()) {
        if (parseFloat($("#Final_Amtt").text()) > 0) {
            $("#tab_TrvelRequert").hide();
            $("#tab_HotelDetails").hide();
            $("#tab_Expense").hide();
            $("#tab_Doc").show();
        }
        else {
            alert("Final Amount Should be Greater Than zero...");
        }

    }
    
}

function prepare_data() {
    try {
        if ($("#empno").html() == "") {
            alert("User Data Not Found...!");
            return false;
        }
        else {
            if ($("#doa").val() != "" && ($("#doa_user").val() == "NA" || $("#doa_user").val() == "")) {
                alert("DOA Approver Not Found...");
                return false;
            }
            else {
                if ($("#txt_Remark").val() != "") {
                    if ($("#ddlAction").val() != undefined && $("#ddlAction").val() != 0) {
                        var hamt = 0;
                        var hname = "";
                        var hno = "";
                        var fdate = "";

                        var XMLFILE = "";
                        var pk_id = $("#txt_pk_id").val();
                        var lastRow1 = $('#uploadTable tr').length;
                        XMLFILE = "<ROWSET>";
                        if (lastRow1 > 1) {
                            for (var l = 0; l < lastRow1 - 1; l++) {
                                var SecondCol = $("#uploadTable tr")[l + 1].cells[0].innerText;
                                XMLFILE += "<ROW>";
                                XMLFILE += "<OBJECT_TYPE>TRAVEL EXPENSE</OBJECT_TYPE>";
                                XMLFILE += "<OBJECT_VALUE>" + pk_id + "</OBJECT_VALUE>";
                                XMLFILE += "<FILENAME>" + SecondCol + "</FILENAME>";
                                XMLFILE += "</ROW>";
                            }
                        }
                        XMLFILE += "</ROWSET>";
                        $("#txt_Document_Xml").val(XMLFILE);
                        return true;
                    }
                    else {
                        alert("Please Select Action...");
                        return false;
                    }
                }
                else {
                    alert("Please Enter Remark...");
                    return false;
                }
            }
        }
    }
    catch (exception) {
        XMLFILE = "<ROWSET></ROWSET>";
        return false;
    }
}

function calculate_Amount() {
    var Hotel_Charge, Total, fTotal;
    Hotel_Charge = Total = fTotal = 0;
    var d = document.getElementsByName('jt');
    for (var i = 1; i <= d.length; i++) {
        var exp = document.getElementsByName("eh");
        Hotel_Charge = 0;
        Total = 0;
        for (var eh = 1; eh <= exp.length; eh++) {
            var exp_name = $("#exp_name" + eh).val();
            var e_name = $("#e_name" + eh).val();
            var c_name = $("#compare_name" + eh).val();
            var chk_box = false;
            if ($("#IS_CR_" + e_name + "_" + i + "_" + eh).is(":checked") == true) {
                chk_box = true;
            }
            var exp_amt = $("#" + e_name + "_" + i + "_" + eh).val();
            var h_exp_amt = $("#h" + e_name + "_" + i + "_" + eh).val();
            var d_allow = $("#D_ALLOW_" + e_name + "_" + i + "_" + eh).val();


            if ($("#compare_id" + eh).val() != $("#expenses" + eh).val()) {
                var c_val = $("#" + c_name + "_" + i + "_" + $("#compare_id" + eh).val()).val();
                var e_val = $("#" + e_name + "_" + i + "_" + $("#expenses" + eh).val()).val();
                if (($("#" + e_name + "_" + i + "_" + $("#compare_id" + eh).val()).val() != "" || $("#" + e_name + "_" + i + "_" + $("#compare_id" + eh).val()).val() != undefined) && ($("#" + e_name + "_" + i + "_" + $("#expenses" + eh).val()).val() != "" || $("#" + e_name + "_" + i + "_" + $("#expenses" + eh).val()).val() != undefined)) {
                    if (parseFloat(c_val) > 0 && parseFloat(e_val) > 0) {
                        alert("Please Fill either " + exp_name + " or " + c_name.replace(/_/gi, " "));
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

            if ($("#Hotel_Charge" + i).text() != undefined && $("#Hotel_Charge" + i).text() != "") {
                Hotel_Charge = parseFloat($("#Hotel_Charge" + i).text());
            }
            else {
                Hotel_Charge = 0;
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
        $("#cls" + i).val("C");
        $("#txt_t_city" + i).show();
    }
    else {
        $("#txt_t_city" + i).hide();
        var pk_city = $("#To_City" + i).val();
        if (pk_city == 0) {
            $("#cls" + i).val(0);
        }
        else {
            Bulk_Travel_Expense_Doc_Verification_and_AP_Approval.get_Travel_Class(pk_city, i, ClassSucceeded);
        }
    }
}

function ClassSucceeded(response) {
    var data = response.value;
    $("#cls" + data[0]).val(data[1]);
}

function show_record() {
    var search_data = $("#txt_per_Search").val();
    var ddl = $("#ddlPerPage option:selected").val();
    var desg = $("#txt_designation").val();
    var pgno = $("pageno").val();
    Bulk_Travel_Expense_Doc_Verification_and_AP_Approval.showall(search_data, 1, ddl, desg, OnshowAll);
}

function OnshowAll(response) {
    $("#div_header_all").html(response.value);
}

function gotopage(objButton, str) {
    try {
        var pgno = objButton.value;
        $("pageno").val(pgno);
        var strData = $("#txt_per_Search").val();
        var desg = $("#txt_designation").val();
        Bulk_Travel_Expense_Doc_Verification_and_AP_Approval.showall(strData, pgno, str, desg, OnshowAll);
    }
    catch (exception) {

    }
}

function getRequestDetails(index) {
    var process_name = $("#pname" + index).val();
    var req_no = $("#h_info" + index).val();
    //alert(process_name + " - " + req_no);
    Bulk_Travel_Expense_Doc_Verification_and_AP_Approval.getDetails(req_no, process_name, callback_BindRequestData);
    
}

function callback_BindRequestData(response) {
    $("#divReq_Details").html(response.value);
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

function check_uncheck(obj, index) {
    var avail_data = $("#chk_records").val();
    var sp_data = $("#chk_records").val().split("@");
    var data = $("#PK_Dispatch_Note_ID" + index).val() + "$" + $("#fk_process" + index).val() + "$" + $("#iid" + index).val() + "$" + $("#wiid" + index).val() + "$" + $("#h_info" + index).val();
    if (obj.checked) {
        var ch = 0;
        for (var i = 0; i < sp_data.length; i++) {
            if (data == sp_data[i]) {
                ch = 1;
            }
        }
        if (ch == 0) {
            sp_data[sp_data.length] = data;
        }
        $("#chk_records").val("");
        for (var i = 0; i < sp_data.length; i++) {
            if ($("#chk_records").val() == "") {
                $("#chk_records").val(sp_data[i]);
            }
            else {
                $("#chk_records").val($("#chk_records").val() + "@" + sp_data[i]);
            }
        }
    }
    else {
        var ch = 0;
        for (var i = 0; i < sp_data.length; i++) {
            if (data == sp_data[i]) {
                sp_data[i] = "";
                ch = 1;
            }
        }
        $("#chk_records").val("");
        for (var i = 0; i < sp_data.length; i++) {
            if (sp_data[i] != "") {
                if ($("#chk_records").val() == "") {
                    $("#chk_records").val(sp_data[i]);
                }
                else {
                    $("#chk_records").val($("#chk_records").val() + "@" + sp_data[i]);
                }
            }
        }
    }
}