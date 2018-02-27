var diffDays = 0;
var division = $("#span_Division").html();

$(document).ready(function () {
    allowOnlyNumbers();

});

$('#travelFromDate').datepicker({ format: 'dd-M-yyyy', autoclose: true, todayBtn: 'linked', endDate:new Date()}).on('changeDate', function (ev) {
    chk_FromDate();
});

$('#travelToDate').datepicker({ format: 'dd-M-yyyy', autoclose: true, todayBtn: 'linked', endDate: new Date() }).on('changeDate', function (ev) {
   chk_ToDate();
});

function claim_expense()
{
    var fromDate = $("#travelFromDate").val();
    var toDate = $("#travelToDate").val();
    if (fromDate == "")
    {
        alert("Please Select Travel From Date");
        return false;
    }
    else if (toDate == "") {
        alert("Please Select Travel To Date");
        return false;
    }
    var firstDate = new Date(fromDate);
    var secondDate = new Date(toDate);
    if (firstDate > secondDate) {
        travelAdd("");
        $("#travelFromDate").val("");
        $("#travelToDate").val("");
        alert("From Date Should Not Be Greater Than To Date...!");
        return false;
    }
    chk_FromDate();
    chk_ToDate();
}

function allowOnlyNumbers() {
    $('.numbersOnly').keydown(function (e) {
        if (e.shiftKey || e.ctrlKey || e.altKey) {
            e.preventDefault();
        } else {
            var key = e.keyCode;
            
            if (key == 13) {
                e.preventDefault();
                var inputs = $(this).closest('form').find(':input:visible:enabled');
                if ((inputs.length - 1) == inputs.index(this))
                    $(':input:enabled:visible:first').focus();
                else
                    inputs.eq(inputs.index(this) + 1).focus();
            }

            if (!((key == 8) || (key == 9) || (key == 46) || (key >= 35 && key <= 40) || (key >= 48 && key <= 57) || (key >= 96 && key <= 105))) {
                e.preventDefault();
            }
            //if (key == 13) {
            //    var inputs = $(this).closest('form').find(':input:visible');
            //    inputs.eq(inputs.index(this) + 1).focus();
            //    e.preventDefault();
            //}
        }
    });
}

function check_PreDate(fromDate, toDate)
{
    var chk_dates = Domestic_Travel_Request.check_Dates(fromDate, toDate);
    if (chk_dates.value == "1") {
        travelAdd("");
        alert("Expense Already Claimed Between From and To Dates");
        return false;
    }
    else {
        var firstDate = new Date(fromDate);
        var secondDate = new Date(toDate);
        if (firstDate > secondDate)
        {
            travelAdd("");
            alert("From Date Should Not Be Greater Than To Date...!");
            return false;
        }
    }
}

function chk_FromDate() {
    var fromDate = $("#travelFromDate").val();
    var toDate = $("#travelToDate").val();
    if (fromDate == "" && toDate != "") {
        alert("Please Select Travel From Date");
        $("#travelToDate").val("");
        $("#preTDate").val("");
        return false;
    }
    else if (fromDate != "" && toDate == "") {
        $("#travelToDate").val("");
        $("#preTDate").val("");
        return false;
    }
    else {
        if (check_PreDate(fromDate, toDate) != false) {
            getSelectDate(fromDate, toDate);
        }
        else {
            $("#travelFromDate").val("");
            $("#preFDate").val("");
        }
    }

}

function chk_ToDate() {
    var fromDate = $("#travelFromDate").val();
    var toDate = $("#travelToDate").val();
    if (fromDate == "" && toDate != "") {
        alert("Please Select Travel From Date");
        $("#travelToDate").val("");
        $("#preTDate").val("");
        $("#travelToDate").blur();
        $("#travelFromDate").focus();
        return false;
    }
    else if (fromDate != "" && toDate == "") {
        alert("Please Select Travel To Date");
        $("#travelToDate").val("");
        $("#preTDate").val("");
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

function getDiffDays(fromDate, toDate) {
    if (fromDate != "" && toDate != "") {
        var fdate = getFormatedDate1(fromDate);
        var tdate = getFormatedDate1(toDate);
        var oneDay = 24 * 60 * 60 * 1000;
        var firstDate = new Date(fdate);
        var secondDate = new Date(tdate);
        diffDays = Math.round(Math.abs((firstDate.getTime() - secondDate.getTime()) / (oneDay)));
    }
    return diffDays;
}

function getSelectDate(fromDate, toDate) {
    try {
        if (fromDate != "" && toDate != "") {
            var fdate = getFormatedDate1(fromDate);
            var tdate = getFormatedDate1(toDate);

            var firstDate = new Date(fdate);
            var secondDate = new Date(tdate);
            var tableBody = "";


            //view_Progressbar();
            
            var diff_date1 = 0;
            if ($("#preFDate").val() != "" && $("#preTDate").val() != "") {
                diff_date1 = getDiffDays($("#preFDate").val(), $("#preTDate").val())+1;
            }
            
            $("#txt_pre_data").val($("#accordion").html());
            if ($("#preFDate").val() == "" && $("#preTDate").val() == "") {
                $("#preFDate").val(fromDate);
                $("#preTDate").val(toDate);
                Domestic_Travel_Request.get_Journey_Data(fromDate, toDate, AjaxSucceeded);
            }
            else if (($("#preFDate").val() == $("#preTDate").val() && fromDate != toDate) || ($("#preFDate").val() != $("#preTDate").val() && fromDate == toDate) || ($("#preFDate").val()!=fromDate)) {
                $("#preFDate").val(fromDate);
                $("#preTDate").val(toDate);
                Domestic_Travel_Request.get_Journey_Data(fromDate, toDate, AjaxSucceeded);
            }
            else if ($("#preFDate").val() != fromDate && $("#preTDate").val() != toDate) {
                $("#preFDate").val(fromDate);
                $("#preTDate").val(toDate);
                Domestic_Travel_Request.get_Journey_Data(fromDate, toDate, AjaxSucceeded);
            }
            else {
                var diff_date2 = getDiffDays(fromDate, toDate) + 1;

                var thdate = getFormatedDate1($("#preTDate").val());
                var thirdDate = new Date(thdate);

                $("#preFDate").val(fromDate);
                $("#preTDate").val(toDate);
                var index = 1;
                if (diff_date1 <= diff_date2) {
                    while (firstDate <= secondDate) {
                        firstDate.setDate(firstDate.getDate() + 1);
                        if (index > diff_date1) {
                            var ret_data = Domestic_Travel_Request.row_data(index, firstDate, fromDate, toDate);
                            $(ret_data.value).insertAfter($("#remove_" + (index-1)));
                            //copyData(index);
                        }
                        index = index + 1;
                    }
                }
                else {
                    index = diff_date2 + 1;
                    secondDate.setDate(secondDate.getDate() + 1);
                    while (secondDate <= thirdDate) {

                        if (index <= diff_date1) {
                            $("#remove_" + index).remove();
                            //alert((index + 1) + " - " + secondDate);

                        }
                        secondDate.setDate(secondDate.getDate() + 1);
                        index = index + 1;
                    }
                }
            }
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
}

function check_journey_Type(index, cnt) {
    var journeyType = $("#journey_Type" + index + " Option:selected").text().toUpperCase();
    var j_val = $("#journey_Type" + index + " Option:selected").val();

    $("#div_PlaceRoom" + index).hide();
    $("#div_HotelContact" + index).hide();
    $("#div_City" + index).hide();
    
    var date_diff1 = getDiffDays($("#travelFromDate").val(), $("#travelToDate").val()) + 1;
    //$("#div_Travel_Mode" + index).val(0);
    //$("#div_Travel_Class" + index).val(0);
    $("#From_Plant" + index).val(0);
    $("#To_Plant" + index).val(0);
    $("#Travel_Mode" + index).val(0);
    $("#Travel_Class" + index).val(0);
    $("#From_City" + index).val(0);
    $("#To_City" + index).val(0);
    $("#txt_f_city" + index).val("");
    $("#txt_t_city" + index).val("");
    $("#txt_f_city" + index).hide();
    $("#txt_t_city" + index).hide();

    $("#div_Travel_Mode" + index).hide();
    $("#div_Travel_Class" + index).hide();
    $("#div_FPlant" + index).hide();
    $("#div_TPlant" + index).hide();

    
    
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

    $("#div_PM" + index).hide();
    $("#chk_reach_" + index).prop("checked", false);
    $("#chk_reach_" + index).prop("disabled", true);
    $("#div_GH" + index).hide();
    $("#chk_guest_" + index).prop("checked", false);
    

    $("#From_City" + index).removeAttr("disabled");
    $("#To_City" + index).removeAttr("disabled");
    $("#dest_plant"+index).html("Plant From");
    if (journeyType == "---SELECT ONE---") {
        $("#div_Travel_Mode" + index).hide();
        $("#div_Travel_Class" + index).hide();
        $("#div_FPlant" + index).hide();
        $("#div_TPlant" + index).hide();


    } else if (journeyType == "OUTSIDE PLANT") {
        $("#div_Travel_Mode" + index).show();
        $("#div_Travel_Class" + index).hide();
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
        $("#div_PM" + index).show();
        if (date_diff1 == index) {
            $("#chk_reach_" + index).prop("disabled", false);
        }
    } else if (journeyType == "OVERNIGHT STAY WITHIN PLANT") {
        $("#div_Travel_Mode" + index).hide();
        $("#div_Travel_Class" + index).hide();
        $("#div_FPlant" + index).show();
        $("#div_TPlant" + index).hide();
        $("#div_PM" + index).show();
        if (date_diff1 == index) {
            $("#chk_reach_" + index).prop("disabled", false);
        }
        $("#div_GH" + index).show();
        $("#chk_guest_" + index).prop("disabled", false);
        $("#dest_plant"+index).html("Plant To");
        $("#From_City" + index).attr("disabled", "disabled");
        $("#To_City" + index).attr("disabled", "disabled");
    }
    else if (journeyType == "ONE DAY OUTSTATION") {
        $("#div_Travel_Mode" + index).show();
        $("#div_Travel_Class" + index).hide();
        $("#div_FPlant" + index).hide();
        $("#div_TPlant" + index).hide();
        $("#div_City" + index).show();
    }
    $("#exp_data" + index).html("");    
}

function copyData(row_index)
{
    var j1_index = $("#journey_Type1 option:selected").index();
    var jc_index = $("#journey_Type"+row_index+" option:selected").index();
    if (j1_index > 0 && jc_index == 0)
    {
        var j_val=$("#journey_Type1").val();
        $("#journey_Type" + row_index).val(j_val);
        check_journey_Type(row_index, 0)
        $("#remark_note" + row_index).val($("#remark_note1").val());
        $("#Travel_Mode" + row_index).val($("#Travel_Mode1").val());
        get_travel_class(row_index);
        $("#Travel_Class" + row_index).val($("#Travel_Class1").val());
        $("#From_Plant" + row_index).val($("#From_Plant1").val());
        $("#To_Plant" + row_index).val($("#To_Plant1").val());
        if (row_index > 1) {
            $("#From_City" + row_index).val($("#To_City1").val());
            $("#txt_f_city" + row_index).val($("#txt_t_city1").val());
        }
        else {
            $("#From_City" + row_index).val($("#From_City1").val());
            $("#txt_f_city" + row_index).val($("#txt_f_city1").val());
        }
        chk_class_From(row_index);
        $("#To_City" + row_index).val($("#To_City1").val());
        chk_class_To(row_index);
        $("#txt_t_city" + row_index).val($("#txt_t_city1").val());
        $("#cls" + row_index).val($("#cls1").val());
        $("#placeclass" + row_index).html($("#placeclass1").html());
        $("#city_class" + row_index).html($("#city_class1").html());
        $("#roomType" + row_index).val($("#roomType1").val());
        $("#hotel_name" + row_index).val($("#hotel_name1").val());
        $("#hotel_no" + row_index).val($("#hotel_no1").val());
        $("#dest_plant" + row_index).html($("#dest_plant1").html());
        get_exp_data(row_index);
    }
}

function get_exp_data(index)
{
    try {
        var desg = $("#txt_designation").val();
        var j_val = $("#journey_Type" + index).val();
        var pk_city = $("#To_City" + index).val();
        var travel_class_id = $("#Travel_Class" + index).val();
        var travel_mode_id = $("#Travel_Mode" + index).val();
        var fplant_id = $("#From_Plant" + index).val();
        var tplant_id = $("#To_Plant" + index).val();
        
        var chkbox = 0;
        if ($("#chk_reach_" + index).is(':checked'))
            chkbox = 1;
        
        Domestic_Travel_Request.get_Expense_Data(desg, j_val, travel_mode_id, travel_class_id, fplant_id, tplant_id, pk_city, index, chkbox, division, ExpenseSucceeded);
    }
    catch (ex)
    {
        alert(ex);
    }
}

function ExpenseSucceeded(response)
    {
        var data = (response.value).split("@@");
        $("#exp_data" + data[0]).html(data[1]);
        incidental_Base(data[0]);
        allowOnlyNumbers();
        calculate_Amount();
	 $("#chk_guest_" + data[0]).prop("checked", false);
    }

function get_dev_policy(index) {
        try {
            var desg = $("#txt_designation").val();
            var j_val = $("#journey_Type" + index).val();
            var travel_class_id = $("#Travel_Class" + index).val();
            var travel_mode_id = $("#Travel_Mode" + index).val();

            Domestic_Travel_Request.get_dev_policy(desg, j_val, travel_mode_id, travel_class_id, DeviationSucceeded);
        }
        catch (ex) {
            alert(ex);
        }
    }

function DeviationSucceeded(response) {
      //  $("#dev_travel_class").html(response.value);
    }

function check_Outside_Plant() {
        try {
            if ($("#empno").html() == "") {
                alert("User Data Not Found. Unable To Submit Request.");
                return false;
            }
            else {
                if ($("#ddl_Payment_Mode option:selected").index() == 0) {
                    alert("Please Select Payment Mode...!");
                    return false;
                }
                else if ($("#ddl_Payment_Mode option:selected").text().toUpperCase() == "CASH" && $("#ddlAdv_Location option:selected").index() == 0) {
                    alert("Please Select Cash Location...!");
                    return false;
                }
                else if ($("#travelFromDate").val() == "") {
                    alert("Please Select Travel From Date...!");
                    return false;
                }
                else if ($("#travelToDate").val() == "") {
                    alert("Please Select Travel To Date...!");
                    return false;
                }
                else {
                    if ($("#span_Approver").html() == "NA" || $("#span_Approver").html() == "") {
                        alert("Approver Not Found. Unable To Submit Request.");
                        return false;
                    }
                    else {
                        var fromDate = $("#travelFromDate").val();
                        var d = document.getElementsByName('jt');
                        var len = d.length;
                        if (len > 0) {
                            var cnt = 0;
                            var concat_data = "";
                            var cls_val = "";
                           
                            for (var j = 1; j <= len; j++) {
                                var span_val = $("#spn_date_" + j).text();
                                var journeyType = $("#journey_Type" + j + " Option:selected").text().toUpperCase();
                                if (journeyType == "---SELECT ONE---") {
                                    alert('Please Select Journey Type For ' + span_val);
                                    return false;
                                }
                                else {
                                    if ($("#remark_note" + j).val() == "")
                                    {
                                        alert("Please Enter Particular Note For " + span_val);
                                        return false;
                                    }
                                    var From_City = $("#From_City" + j + " Option:selected").text().toUpperCase();
                                    var To_City = $("#To_City" + j + " Option:selected").text().toUpperCase();
                                    var tfcity=$("#txt_f_city" + j).val().toUpperCase();
                                    var ttcity=$("#txt_t_city" + j).val().toUpperCase();

                                    if (journeyType == "OUTSIDE PLANT" || journeyType == "ONE DAY OUTSTATION") {
                                        var Travel_Mode = $("#Travel_Mode" + j + " Option:selected").text().toUpperCase();
                                        var Travel_Class = $("#Travel_Class" + j + " Option:selected").text().toUpperCase();

                                        if (Travel_Mode == "---SELECT ONE---") {
                                            alert('Please Select Travel Mode For ' + span_val);
                                            return false;
                                        }
                                        else if (Travel_Class == "---SELECT ONE---" && Travel_Mode!="OTHER") {
                                            alert('Please Select Travel Class For ' + span_val);
                                            return false;
                                        }
                                        else if (journeyType == "OUTSIDE PLANT" && $("#roomType" + j + " option:selected").text().toUpperCase()=="---SELECT ONE---") {
                                            alert('Please Select Room Type For ' + span_val);
                                            return false;
                                        }
                                        else if (journeyType == "OUTSIDE PLANT" && $("#hotel_name" + j).val() == "") {
                                            alert('Please Enter Hotel Name For ' + span_val);
                                            return false;
                                        }
                                        else if (journeyType == "OUTSIDE PLANT" && $("#hotel_no" + j).val() == "") {
                                            alert('Please Enter Hotel Contact No. For ' + span_val);
                                            return false;
                                        }

                                    }
                                    if (journeyType == "OVERNIGHT STAY WITHIN PLANT" || journeyType == "ONE DAY OUTSTATION WITHIN PLANT") {
                                        var From_Plant = $("#From_Plant" + j + " Option:selected").text().toUpperCase();
                                        var To_Plant = $("#To_Plant" + j + " Option:selected").text().toUpperCase();

                                        if (From_Plant == "---SELECT ONE---") {
                                            alert('Please Select Source Plant For ' + span_val);
                                            return false;
                                        }
                                        if (journeyType == "ONE DAY OUTSTATION WITHIN PLANT") {
                                            if (To_Plant == "---SELECT ONE---") {
                                                alert('Please Select Destination Plant For ' + span_val);
                                                return false;
                                            }
                                        }
                                        if (journeyType == "ONE DAY OUTSTATION WITHIN PLANT" && From_Plant == To_Plant) {
                                            alert('Source And Destination Plants Should Not Be Same For ' + span_val);
                                            return false;
                                        }
                                    }

                                    if ((journeyType == "OUTSIDE PLANT" || journeyType == "ONE DAY OUTSTATION") && From_City == "---SELECT ONE---" || (From_City == "OTHER" && $("#txt_f_city" + j).val() == "")) {
                                        alert('Select From City For ' + span_val);
                                        return false;
                                    }
                                    else if ((journeyType == "OUTSIDE PLANT" || journeyType == "ONE DAY OUTSTATION") && To_City == "---SELECT ONE---" || (To_City == "OTHER" && $("#txt_t_city" + j).val() == "")) {
                                        alert('Select To City For ' + span_val);
                                        return false;
                                    }
                                   
                                    if ((journeyType == "OUTSIDE PLANT" || journeyType == "ONE DAY OUTSTATION") && (To_City == tfcity || From_City == ttcity) && j==1) {
                                        alert('From And To Cities Should Not Be Same For ' + span_val);
                                        return false;
                                    }
                                    if ((journeyType == "OUTSIDE PLANT" || journeyType == "ONE DAY OUTSTATION") && (ttcity == tfcity && From_City == To_City) && j == 1) {
                                        alert('From And To Cities Should Not Be Same For ' + span_val);
                                        return false;
                                    }
                                }


                            }
                         
                            return true;
                        }
                        else {
                            alert('Rows Not Found');
                        }
                    }
                }
            }
        }
        catch (ex) {
            //$('#divIns').html("");
            alert(ex);
        }
    }

function prepare_data() {
    try {
        
            if ($("#empno").html() == "") {
                alert("User Data Not Found. Unable To Submit Request.");
                return false;
            }
            else {
                if ($("#span_bank_no").html() == "NA" || $("#span_bank_no").html() == "") {
                    alert("Bank Account Is Not Available! Unable To Claim Expense Request...!");
                    return false;
                }                
               
                if ($("#ddl_Payment_Mode option:selected").index() == 0) {
                    alert("Please Select Payment Mode...!");
                    return false;
                }
                else if ($("#ddl_Payment_Mode option:selected").text().toUpperCase() == "CASH" && $("#ddlAdv_Location option:selected").index() == 0) {
                    alert("Please Select Cash Location...!");
                    return false;
                }
                else if ($("#travelFromDate").val() == "") {
                    alert("Please Select Travel From Date...!");
                    return false;
                }
                else if ($("#travelToDate").val() == "") {
                    alert("Please Select Travel To Date...!");
                    return false;
                }
                else if ($("#req_remark").val() == "") {
                    alert("Please Enter Request Remark...!");
                    return false;
                }

                else {
                    var fdate=new Date($("#travelFromDate").val());
                    var tdate=new Date($("#travelToDate").val());
                    if (fdate > tdate) {
                        alert('From Date Should Not Be Greater Than To Date');
                        return false;
                    }
                   if ($("#span_Approver").html() == "NA" || $("#span_Approver").html() == "") {
                        alert("Approver Not Found. Unable To Submit Request.");
                        return false;
                    }
                    else {
                        var chk_dates = Domestic_Travel_Request.check_Dates($("#travelFromDate").val(), $("#travelToDate").val());
                        if (chk_dates.value == "1")
                        {
                            alert("Expense Already Claims Between From and To Dates");
                            return false;
                        }
                        var flag = check_Outside_Plant();
                        if (flag == false)
                        {
                            return false;
                        }
                        var msg = checkSupp_Docs();
                        if (msg != "")
                        {
                            alert(msg);
                            return false;
                        }
                        var IS_SUP = 0;
                        if ($("#Final_Amtt").html() != undefined && $("#Final_Amtt").html() != "" && parseFloat($("#Final_Amtt").html()) > 0) {
                            var hamt = 0;
                            var hname = "";
                            var hno = "";
                            var fdate = "";
                            var radio = document.getElementsByName("travel");
                            for (var rad = 1; rad <= radio.length; rad++) {
                                if (radio[rad - 1].checked) {
                                    var id = $("#PK_ADVANCE_ID" + rad).val();
 			                	    var advance_amount = $("#advance_amount" + rad).val();
                                    $("#txt_advance_id").val(id);
                                   $("#txt_advance_amount").val(advance_amount);
                                    $("#spn_adv_amt").text(advance_amount);
                                }
                            }

                            $("#dev_policy_amt").val(0);
                            $("#dev_travel_class").val(0);
                            $("#dev_supp_amt").val(0);
                            calculate_Amount();
                            //check_Policy_Deviate();
                            if ((parseFloat($("#txt_advance_amount").val()) > parseFloat($("#Final_Amtt").html())) && $("#ddl_Payment_Mode option:selected").text().toUpperCase() == "BANK") {
                                alert("Please Change Payment Mode and Select Payment Location...!");
                                return false;
                            }
                            var tbl = document.getElementsByName("jt");
                            var xmldoc = "<ROWSET>";
                            var xml_exp = "<ROWSET>";
                           
                            for (var i = 1; i <= tbl.length; i++) {
                                var IS_CR_BORDING, IS_CR_LODGING_WITHOUT_BILL, IS_CR_LODGING_WITH_BILL, IS_CR_LOCAL_TRAVEL, IS_CR_BOARD_INCD_PER_DAY, IS_CR_OTHER, IS_CR_FARE;
                                IS_CR_BORDING = IS_CR_LODGING_WITHOUT_BILL = IS_CR_LODGING_WITH_BILL = IS_CR_LOCAL_TRAVEL = IS_CR_BOARD_INCD_PER_DAY = IS_CR_OTHER = IS_CR_FARE = 0;

                                
                                //var dv = Domestic_Travel_Request.get_dev_policy($("#txt_designation").val(), $("#journey_Type" + i).val(), $("#Travel_Mode" + i).val(), $("#Travel_Class" + i).val());
                                //if (dv.value == 1)
                                //{
                                //    $("#dev_travel_class").val(1);
                                //}

                                var firstDate = $("#spn_date_" + i).html();
                                fdate = getFormatedDate1(firstDate);
                                xmldoc += "<ROW>";
                                xmldoc += "<fk_travel_expense_Hdr_Id>#</fk_travel_expense_Hdr_Id>";
                                xmldoc += "<travel_date>" + fdate + "</travel_date>";
                                xmldoc += "<journey_type>" + $("#journey_Type" + i).val() + "</journey_type>";
                                xmldoc += "<travel_mode>" + $("#Travel_Mode" + i).val() + "</travel_mode>";
                                xmldoc += "<travel_class>" + $("#Travel_Class" + i).val() + "</travel_class>";
                                xmldoc += "<plant_from>" + $("#From_Plant" + i).val() + "</plant_from>";
                                xmldoc += "<plant_to>" + $("#To_Plant" + i).val() + "</plant_to>";
                                xmldoc += "<From_city>" + $("#From_City" + i).val() + "</From_city>";
                                xmldoc += "<to_city>" + $("#To_City" + i).val() + "</to_city>";
                                xmldoc += "<place_class>" + $("#placeclass" + i).text() + "</place_class>";
                                xmldoc += "<Room_type>" + $("#roomType" + i).val() + "</Room_type>";

                                if ($("#hotel_name" + i).val() == undefined) {
                                    xmldoc += "<hotel_name></hotel_name>";
                                }
                                else {
                                    xmldoc += "<hotel_name>" + $("#hotel_name" + i).val() + "</hotel_name>";
                                }
                                if ($("#hotel_no" + i).val() == undefined) {
                                    xmldoc += "<hotel_no></hotel_no>";
                                }
                                else {
                                    xmldoc += "<hotel_no>" + $("#hotel_no" + i).val() + "</hotel_no>";
                                }
                           
                                xmldoc += "<Other_F_City>" + $("#txt_f_city" + i).val() + "</Other_F_City>";
                                xmldoc += "<Other_T_City>" + $("#txt_t_city" + i).val() + "</Other_T_City>";
                                xmldoc += "<remark_note>" + $("#remark_note" + i).val() + "</remark_note>";

                                var chkbox = 0;
                                if ($("#chk_reach_" + i).is(':checked'))
                                    chkbox = 1;
                                xmldoc += "<beyond_time>" + chkbox + "</beyond_time>";

                                if ($("#chk_guest_" + i).is(':checked')) {
                                    xmldoc += "<Stay_Guest_House>1</Stay_Guest_House>";
                                }
                                else {
                                    xmldoc += "<Stay_Guest_House>0</Stay_Guest_House>";
                                }

                                var exp = document.getElementsByName("eh"+i);

                                for (var eh = 1; eh <= exp.length; eh++) {
                                    var exp_name = $("#exp_name" + i + "" + eh).val();
                                    var e_name = $("#e_name" + i + "" + eh).val();
                                    var f_name = $("#" + $("#exp_name" + i + "" + eh).val() + "_" + i + "_" + eh);
                                    var e_fk_id = $("#e_fk_id" + i + "" + eh).val();
                                    //var sup = $("#IS_SUP" + e_fk_id + "_" + i + "_" + eh).html();
                                   
                                    if ($("#expenses" + i + "" + eh).val() != undefined) {
                                        xml_exp += "<ROW>";
                                        xml_exp += "<fk_travel_expense_Hdr_Id>#</fk_travel_expense_Hdr_Id>";
                                        xml_exp += "<fk_travel_date>" + fdate + "</fk_travel_date>";
                                        xml_exp += "<fk_expense_head_id>" + $("#expenses" + i + "" + eh).val() + "</fk_expense_head_id>";
                                        var exp_amt = $("#" + e_name + "_" + i + "_" + eh).val();
                                        if (exp_amt != undefined && exp_amt !="") {
                                            xml_exp += "<Amount>" + exp_amt + "</Amount>";
                                        }
                                        else {
                                            xml_exp += "<Amount>" + 0 + "</Amount>";
                                        }
                                        var IS_CR = $("#ddlrem_" + e_fk_id + "_" + i + "_" + eh).val();
                                        if (IS_CR != 0 && IS_CR!=undefined && IS_CR!="") {
                                            xml_exp += "<REIM_TYPE>" + IS_CR + "</REIM_TYPE>";
                                        }
                                        else {
                                            xml_exp += "<REIM_TYPE>" + 0 + "</REIM_TYPE>";
                                        }
                                        xml_exp += "<EXPENSE_TYPE>TRAVEL EXPENSE</EXPENSE_TYPE>";
                                        var IS_SUP = $("#IS_SUP" + e_fk_id + "_" + i + "_" + eh).html();
                                        if (IS_SUP != "" && IS_SUP != undefined) {
                                            if (IS_SUP == "Y")
                                            {
                                                xml_exp += "<IS_SUPPORT>" + 1 + "</IS_SUPPORT>";
                                            }
                                            else {
                                                xml_exp += "<IS_SUPPORT>" + 0 + "</IS_SUPPORT>";
                                            }
                                        }
                                        else {
                                            xml_exp += "<IS_SUPPORT>" + 0 + "</IS_SUPPORT>";
                                        }
                                        var SUPPORT_DOC = $("#ddl_SUP" + e_fk_id + "_" + i + "_" + eh).val();
                                        if (SUPPORT_DOC != "" && IS_SUP != undefined) {
                                            xml_exp += "<SUPPORT_DOC>" + SUPPORT_DOC + "</SUPPORT_DOC>";
                                        }
                                        else {
                                            xml_exp += "<SUPPORT_DOC>N</SUPPORT_DOC>";
                                        }

                                        var particular_SUP = $("#particular_SUP" + e_fk_id + "_" + i + "_" + eh).val();
                                        xml_exp += "<SUPPORTING_REMARK>" + particular_SUP + "</SUPPORTING_REMARK>";
                                        var OTHER_REMARK = $("#other_remark" + e_fk_id + "_" + i + "_" + eh).val();
                                        xml_exp += "<OTHER_REMARK>" + OTHER_REMARK + "</OTHER_REMARK>";
                                        
                                        xml_exp += "</ROW>";
                                    }
                                }


                                xmldoc += "</ROW>";
                            }
                            xml_exp += "</ROWSET>";
                            xmldoc += "</ROWSET>";


                            var XMLFILE = "";

                            var lastRow1 = $('#uploadTable tr').length;
                            XMLFILE = "<ROWSET>";
                            
                            if ($("#spn_supp_diff").text() != "" && $("#spn_supp_diff").text() != "0" && lastRow1 < 2) {
                                    alert("Please Attach Atleast One Supporting Document.");
                                    return false;
                            }
                            if (lastRow1 > 1) {
                                for (var l = 0; l < lastRow1 - 1; l++) {
                                    var SecondCol = $("#uploadTable tr")[l + 1].cells[1].innerText;
                                    var file_name = $("#f_name" + (l + 1)).html();
                                    XMLFILE += "<ROW>";
                                    XMLFILE += "<OBJECT_TYPE>TRAVEL EXPENSE</OBJECT_TYPE>";
                                    XMLFILE += "<OBJECT_VALUE>#</OBJECT_VALUE>";
                                    XMLFILE += "<FILENAME>" + SecondCol + "</FILENAME>";
                                    XMLFILE += "<DOCUMENT_TYPE>" + file_name + "</DOCUMENT_TYPE>";
                                    XMLFILE += "</ROW>";
                                }
                            }
                            XMLFILE += "</ROWSET>";

                            if (xml_exp == "<ROWSET></ROWSET>" || xml_exp == "" || xmldoc == "<ROWSET></ROWSET>" || xmldoc == "")
                            {
                                alert("Invalid Rows Data");
                                return false;
                            }
                            
                            $("#txt_Document_Xml").val(XMLFILE);
                            $("#txt_sub_xml_data").val(xml_exp);
                            $("#txt_xml_data").val(xmldoc);

                            
                            if (deviation_Alert()) {
                                //return true;
                                $('#modal_summary').modal('show');
                            }
                            else {
                                //return false;
                                $('#modal_summary').modal('hide');
                            }
                          
                        }
                        else {
                            alert("Total Amount Should be Greater Than Zero...");
                            return false;
                        }
                    }
                }
            }
        }
        catch (exception) {
            //$('#divIns').html("");
            alert(exception);
            xmldoc = "<ROWSEsT></ROWSET>";
            XMLFILE = "<ROWSET></ROWSET>";
            return false;
        }
}

function save_as_draft() {
    try {

        if ($("#empno").html() == "") {
            alert("User Data Not Found. Unable To Submit Request.");
            return false;
        }
        else {
            if ($("#span_bank_no").html() == "NA" || $("#span_bank_no").html() == "") {
                alert("Bank Account Is Not Available! Unable To Claim Expense Request...!");
                return false;
            }

            if ($("#ddl_Payment_Mode option:selected").index() == 0) {
                alert("Please Select Payment Mode...!");
                return false;
            }
            else if ($("#ddl_Payment_Mode option:selected").text().toUpperCase() == "CASH" && $("#ddlAdv_Location option:selected").index() == 0) {
                alert("Please Select Cash Location...!");
                return false;
            }
            else if ($("#travelFromDate").val() == "") {
                alert("Please Select Travel From Date...!");
                return false;
            }
            else if ($("#travelToDate").val() == "") {
                alert("Please Select Travel To Date...!");
                return false;
            }
            else if ($("#req_remark").val() == "") {
                alert("Please Enter Request Remark...!");
                return false;
            }

            else {
                var fdate = new Date($("#travelFromDate").val());
                var tdate = new Date($("#travelToDate").val());
                if (fdate > tdate) {
                    alert('From Date Should Not Be Greater Than To Date');
                    return false;
                }
                if ($("#span_Approver").html() == "NA" || $("#span_Approver").html() == "") {
                    alert("Approver Not Found. Unable To Submit Request.");
                    return false;
                }
                //else {
                    var chk_dates = Domestic_Travel_Request.check_Dates($("#travelFromDate").val(), $("#travelToDate").val());
                    if (chk_dates.value == "1") {
                        alert("Expense Already Claims Between From and To Dates");
                        return false;
                    }
                    
                    var IS_SUP = 0;
                    //if ($("#Final_Amtt").html() != undefined && $("#Final_Amtt").html() != "" && parseFloat($("#Final_Amtt").html()) > 0) {
                        var hamt = 0;
                        var hname = "";
                        var hno = "";
                        var fdate = "";
                        var radio = document.getElementsByName("travel");
                        for (var rad = 1; rad <= radio.length; rad++) {
                            if (radio[rad - 1].checked) {
                                var id = $("#PK_ADVANCE_ID" + rad).val();
                                var advance_amount = $("#advance_amount" + rad).val();
                                $("#txt_advance_id").val(id);
                                $("#txt_advance_amount").val(advance_amount);
                                $("#spn_adv_amt").text(advance_amount);
                            }
                        }

                        $("#dev_policy_amt").val(0);
                        $("#dev_travel_class").val(0);
                        $("#dev_supp_amt").val(0);
                        calculate_Amount();
                        //check_Policy_Deviate();
                        if ((parseFloat($("#txt_advance_amount").val()) > parseFloat($("#Final_Amtt").html())) && $("#ddl_Payment_Mode option:selected").text().toUpperCase() == "BANK") {
                            alert("Please Change Payment Mode and Select Payment Location...!");
                            return false;
                        }
                        var tbl = document.getElementsByName("jt");
                        var xmldoc = "<ROWSET>";
                        var xml_exp = "<ROWSET>";

                        for (var i = 1; i <= tbl.length; i++) {
                            var IS_CR_BORDING, IS_CR_LODGING_WITHOUT_BILL, IS_CR_LODGING_WITH_BILL, IS_CR_LOCAL_TRAVEL, IS_CR_BOARD_INCD_PER_DAY, IS_CR_OTHER, IS_CR_FARE;
                            IS_CR_BORDING = IS_CR_LODGING_WITHOUT_BILL = IS_CR_LODGING_WITH_BILL = IS_CR_LOCAL_TRAVEL = IS_CR_BOARD_INCD_PER_DAY = IS_CR_OTHER = IS_CR_FARE = 0;


                            //var dv = Domestic_Travel_Request.get_dev_policy($("#txt_designation").val(), $("#journey_Type" + i).val(), $("#Travel_Mode" + i).val(), $("#Travel_Class" + i).val());
                            //if (dv.value == 1) {
                            //    $("#dev_travel_class").val(1);
                            //}

                            var firstDate = $("#spn_date_" + i).html();
                            fdate = getFormatedDate1(firstDate);
                            xmldoc += "<ROW>";
                            xmldoc += "<fk_travel_expense_Hdr_Id>#</fk_travel_expense_Hdr_Id>";
                            xmldoc += "<travel_date>" + fdate + "</travel_date>";
                            xmldoc += "<journey_type>" + $("#journey_Type" + i).val() + "</journey_type>";
                            xmldoc += "<travel_mode>" + $("#Travel_Mode" + i).val() + "</travel_mode>";
                            xmldoc += "<travel_class>" + $("#Travel_Class" + i).val() + "</travel_class>";
                            xmldoc += "<plant_from>" + $("#From_Plant" + i).val() + "</plant_from>";
                            xmldoc += "<plant_to>" + $("#To_Plant" + i).val() + "</plant_to>";
                            xmldoc += "<From_city>" + $("#From_City" + i).val() + "</From_city>";
                            xmldoc += "<to_city>" + $("#To_City" + i).val() + "</to_city>";
                            xmldoc += "<place_class>" + $("#placeclass" + i).text() + "</place_class>";
                            xmldoc += "<Room_type>" + $("#roomType" + i).val() + "</Room_type>";

                            if ($("#hotel_name" + i).val() == undefined) {
                                xmldoc += "<hotel_name></hotel_name>";
                            }
                            else {
                                xmldoc += "<hotel_name>" + $("#hotel_name" + i).val() + "</hotel_name>";
                            }
                            if ($("#hotel_no" + i).val() == undefined) {
                                xmldoc += "<hotel_no></hotel_no>";
                            }
                            else {
                                xmldoc += "<hotel_no>" + $("#hotel_no" + i).val() + "</hotel_no>";
                            }

                            xmldoc += "<Other_F_City>" + $("#txt_f_city" + i).val() + "</Other_F_City>";
                            xmldoc += "<Other_T_City>" + $("#txt_t_city" + i).val() + "</Other_T_City>";
                            xmldoc += "<remark_note>" + $("#remark_note" + i).val() + "</remark_note>";

                            var chkbox = 0;
                            if ($("#chk_reach_" + i).is(':checked'))
                                chkbox = 1;
                            xmldoc += "<beyond_time>" + chkbox + "</beyond_time>";

                            if ($("#chk_guest_" + i).is(':checked')) {
                                xmldoc += "<Stay_Guest_House>1</Stay_Guest_House>";
                            }
                            else {
                                xmldoc += "<Stay_Guest_House>0</Stay_Guest_House>";
                            }

                            var exp = document.getElementsByName("eh" + i);

                            for (var eh = 1; eh <= exp.length; eh++) {
                                var exp_name = $("#exp_name" + i + "" + eh).val();
                                var e_name = $("#e_name" + i + "" + eh).val();
                                var f_name = $("#" + $("#exp_name" + i + "" + eh).val() + "_" + i + "_" + eh);
                                var e_fk_id = $("#e_fk_id" + i + "" + eh).val();
                                //var sup = $("#IS_SUP" + e_fk_id + "_" + i + "_" + eh).html();

                                if ($("#expenses" + i + "" + eh).val() != undefined) {
                                    xml_exp += "<ROW>";
                                    xml_exp += "<fk_travel_expense_Hdr_Id>#</fk_travel_expense_Hdr_Id>";
                                    xml_exp += "<fk_travel_date>" + fdate + "</fk_travel_date>";
                                    xml_exp += "<fk_expense_head_id>" + $("#expenses" + i + "" + eh).val() + "</fk_expense_head_id>";
                                    var exp_amt = $("#" + e_name + "_" + i + "_" + eh).val();
                                    if (exp_amt != undefined && exp_amt != "") {
                                        xml_exp += "<Amount>" + exp_amt + "</Amount>";
                                    }
                                    else {
                                        xml_exp += "<Amount>" + 0 + "</Amount>";
                                    }
                                    var IS_CR = $("#ddlrem_" + e_fk_id + "_" + i + "_" + eh).val();
                                    if (IS_CR != 0 && IS_CR != undefined && IS_CR != "") {
                                        xml_exp += "<REIM_TYPE>" + IS_CR + "</REIM_TYPE>";
                                    }
                                    else {
                                        xml_exp += "<REIM_TYPE>" + 0 + "</REIM_TYPE>";
                                    }
                                    xml_exp += "<EXPENSE_TYPE>TRAVEL EXPENSE</EXPENSE_TYPE>";
                                    var IS_SUP = $("#IS_SUP" + e_fk_id + "_" + i + "_" + eh).html();
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
                                    var SUPPORT_DOC = $("#ddl_SUP" + e_fk_id + "_" + i + "_" + eh).val();
                                    if (SUPPORT_DOC != "" && IS_SUP != undefined) {
                                        xml_exp += "<SUPPORT_DOC>" + SUPPORT_DOC + "</SUPPORT_DOC>";
                                    }
                                    else {
                                        xml_exp += "<SUPPORT_DOC>N</SUPPORT_DOC>";
                                    }

                                    var particular_SUP = $("#particular_SUP" + e_fk_id + "_" + i + "_" + eh).val();
                                    xml_exp += "<SUPPORTING_REMARK>" + particular_SUP + "</SUPPORTING_REMARK>";
                                    var OTHER_REMARK = $("#other_remark" + e_fk_id + "_" + i + "_" + eh).val();
                                    xml_exp += "<OTHER_REMARK>" + OTHER_REMARK + "</OTHER_REMARK>";

                                    xml_exp += "</ROW>";
                                }
                            }


                            xmldoc += "</ROW>";
                        }
                        xml_exp += "</ROWSET>";
                        xmldoc += "</ROWSET>";


                        var XMLFILE = "";

                        var lastRow1 = $('#uploadTable tr').length;
                        XMLFILE = "<ROWSET>";
                        //alert(lastRow1);
                        //if (lastRow1 < 2)
                        //{
                        //    alert("Please Attach Atleast One Document.");
                        //    return false;
                //}
                        
                        
                        if (lastRow1 > 1) {
                            for (var l = 0; l < lastRow1 - 1; l++) {
                                var SecondCol = $("#uploadTable tr")[l + 1].cells[1].innerText;
                                var file_name = $("#f_name" + (l + 1)).html();
                                XMLFILE += "<ROW>";
                                XMLFILE += "<OBJECT_TYPE>TRAVEL EXPENSE</OBJECT_TYPE>";
                                XMLFILE += "<OBJECT_VALUE>#</OBJECT_VALUE>";
                                XMLFILE += "<FILENAME>" + SecondCol + "</FILENAME>";
                                XMLFILE += "<DOCUMENT_TYPE>" + file_name + "</DOCUMENT_TYPE>";
                                XMLFILE += "</ROW>";
                            }
                        }
                        XMLFILE += "</ROWSET>";


                        $("#txt_Document_Xml").val(XMLFILE);
                        $("#txt_sub_xml_data").val(xml_exp);
                        $("#txt_xml_data").val(xmldoc);

                        //alert(xmldoc);
                        //alert(xml_exp);
                        //alert(XMLFILE);
                        //alert("Save");
                        $("#div_Load").show();
                        return true;

                   
            }
        }
    }
    catch (exception) {
        $("#divIns").hide();
        alert(exception);
        xmldoc = "<ROWSET></ROWSET>";
        XMLFILE = "<ROWSET></ROWSET>";
        return false;
    }
}

function check_Action()
{
    $("#divIns").show();
    //alert("Submit");
    return true;
//    if (deviation_Alert()) {
//        return false;
	
//    }
//    else {
//$("#divIns").hide();
//        return false;
//    }
}

function change_flag(exp, index, i)
    {
        try
        {
            var data = exp + "_" + index + "_" + i;
            var fk_id = $("#ddlrem_" + data).val();
            Domestic_Travel_Request.chagable_or_not("Domestic Travel Expenses", fk_id, data, changeText);
        }
        catch (ex)
        {
            alert(ex);
        }
    }

function changeText(response)
    {
        var data = (response.value).split("@@");
        $("#reim_val" + data[0]).val(data[1]);
        calculate_Amount();
    }

function check_Policy_Deviate()
    {
        var Hotel_Charge, Total, fTotal, non_supp, supp_tot, percent_val, deviate;
        Hotel_Charge = Total = fTotal = non_supp = supp_tot = percent_val = deviate = 0;
        $("#dev_policy_amt").val(0);
        var d = document.getElementsByName('jt');
        var exp_array_amt=new Array();
        var exp_array_hamt = new Array();
        var exp_array_name = new Array();
        var exp_name_array = new Array();
        for (var i = 1; i <= d.length; i++) {
            var travel_mode = $("#Travel_Mode"+i+" option:selected").text();
            var exp = document.getElementsByName("eh" + i);
            for (var eh = 1; eh <= exp.length; eh++) {
                var exp_name = $("#exp_name" + i + "" + eh).val();
                exp_array_name[eh - 1] = exp_name;
                var e_name = $("#e_name" + i + "" + eh).val();
                var c_name = $("#compare_name" + i + "" + eh).val();
                var chk_box = true;
                var fk_id = $("#e_fk_id" + i + "" + eh).val();
                if ($("#reim_val" + fk_id + "_" + i + "_" + eh).val() == 1) {
                    chk_box = false;
                }
                var reim_type = $("#ddlrem_" + fk_id + "_" + i + "_" + eh + " option:selected").text();
                var exp_amt = $("#" + e_name + "_" + i + "_" + eh).val();
                var h_exp_amt = $("#h" + e_name + "_" + i + "_" + eh).val();
                var d_allow = $("#D_ALLOW_" + e_name + "_" + i + "_" + eh).val();

                if (d_allow == 0) {
                    if (exp_amt != undefined && exp_amt != "") {
                        if (h_exp_amt != "") {
                            if (chk_box == false) {
                                Total += parseFloat(exp_amt);
                            }
                            if (exp_array_amt[eh - 1] == undefined || exp_array_amt[eh - 1] == "") {
                                exp_array_amt[eh - 1] = 0;
                            }
                            if (exp_array_hamt[eh - 1] == undefined || exp_array_hamt[eh - 1] == "") {
                                exp_array_hamt[eh - 1] = 0;
                            }
                            if (travel_mode == "Other" && reim_type == "Company Provided") {

                            }
                            else {
                                exp_array_amt[eh - 1] = parseFloat(exp_array_amt[eh - 1]) + parseFloat(exp_amt);
                                exp_array_hamt[eh - 1] = parseFloat(exp_array_hamt[eh - 1]) + parseFloat(h_exp_amt);
                            }
                        }
                    }
                }
            }
        }
        var exp1 = document.getElementsByName("eh1");
        for (var j = 0; j < exp1.length; j++) {
            if (exp_array_amt[j] != NaN && exp_array_amt[j] != undefined && exp_array_amt[j] != "") {
                var e_amt = parseFloat(exp_array_amt[j]);
                var h_amt = parseFloat(exp_array_hamt[j]);
                var e_name = exp_array_name[j];
                if (parseFloat(e_amt) > parseFloat(h_amt)) {
                    $("#dev_policy_amt").val(1);
                }
            }
        }
    //return true;
        alert($("#dev_policy_amt").val());
        return false;
    }

function calculate_Amount() {
    
        var Hotel_Charge, Total, fTotal, non_supp, supp_tot, percent_val;
        Hotel_Charge = Total = fTotal = non_supp = supp_tot = percent_val = 0;
        $("#dev_policy_amt").val(0);
        $("#dev_supp_amt").val(0);
        $("#dev_travel_class").val(0);
        var i_tot_exp = new Array();
        var i_tot_limit = new Array();
        var tot_limit = new Array();
        var tot_exp = new Array();
        tot_limit = calculate_Limit();
        tot_exp = calculate_Expense();

        var d = document.getElementsByName('jt');
        for (var i = 1; i <= d.length; i++) {
            percent_val = 0;
            var exp = document.getElementsByName("eh" + i);
            Hotel_Charge = 0;
            Total = 0;
            var travel_mode = $("#Travel_Mode" + i + " option:selected").text();

            for (var eh = 1; eh <= exp.length; eh++) {

                var exp_name = $("#exp_name" + i + "" + eh).val();
                var e_name = $("#e_name" + i + "" + eh).val();
                var c_name = $("#compare_name" + i + "" + eh).val();
                var chk_box = true;
                var fk_id = $("#e_fk_id" + i + "" + eh).val();
                if ($("#reim_val" + fk_id + "_" + i + "_" + eh).val() == 1) {
                    chk_box = false;
                }

                var reim_type = $("#ddlrem_" + fk_id + "_" + i + "_" + eh + " option:selected").text();
                var reim_val = $("#reim_val" + fk_id + "_" + i + "_" + eh).val();

                $("#supp_amt_tot").val(0);
                $("#supp_amt_no_cur").val(0);
                var exp_amt = $("#" + e_name + "_" + i + "_" + eh).val();
                var supp = $("#IS_SUP" + fk_id + "_" + i + "_" + eh).html();
                if (supp == "Y" && exp_amt != undefined && exp_amt != "" && reim_val==1) {

                    supp_tot += parseFloat(exp_amt);
                    if (supp_tot != undefined && supp_tot != "") {
                        $("#supp_amt_tot").val(supp_tot);
                    }

                }
                var non_s = $("#ddl_SUP" + fk_id + "_" + i + "_" + eh).val();
                if (supp == "Y" && non_s == "N" && exp_amt != undefined && exp_amt != "" && reim_val == 1) {
                    non_supp += parseFloat(exp_amt);
                    if (non_supp != undefined && non_supp != "") {
                        $("#supp_amt_no_cur").val(non_supp);
                    }
                }

                if (non_supp == 0 || supp_tot == 0) {
                    $("#supp_amt_no_db").val(0);
                }
                else {
                    percent_val = (non_supp * 100) / supp_tot;
                    $("#supp_amt_no_db").val(percent_val);
                }

                if ($("#supp_perc_no").val() != "" && $("#supp_perc_no").val() != undefined && $("#supp_amt_no_db").val() != "" && $("#supp_amt_no_db").val() != undefined) {
                    if (percent_val > parseFloat($("#supp_perc_no").val())) {
                        $("#dev_supp_amt").val(1);
                    }
                    else {
                        $("#dev_supp_amt").val(0);
                    }
                }
                else {
                    $("#dev_supp_amt").val(0);
                }

                var dv = Domestic_Travel_Request.get_dev_policy($("#txt_designation").val(), $("#journey_Type" + i).val(), $("#Travel_Mode" + i).val(), $("#Travel_Class" + i).val());
                if (dv.value == 1) {
                    if (e_name != "Incidentals" && e_name != "Board_Incd_Per_Day") {
                        if (travel_mode == "Other" && reim_type != "Company Provided" && exp_amt > 0 && e_name == "Fare") {
                            $("#dev_travel_class").val(1);
                        }
                        else if (travel_mode != "Other")
                        {
                            $("#dev_travel_class").val(1);
                        }

                    }

                }

                var h_exp_amt = $("#h" + e_name + "_" + i + "_" + eh).val();
                var d_allow = $("#D_ALLOW_" + e_name + "_" + i + "_" + eh).val();

                if ($("#compare_id" + i + "" + eh).val() != $("#expenses" + i + "" + eh).val()) {
                    var c_val = $("#" + c_name + "_" + i + "_" + $("#compare_id" + i + "" + eh).val()).val();
                    var e_val = $("#" + e_name + "_" + i + "_" + $("#expenses" + i + "" + eh).val()).val();
                    if (($("#" + e_name + "_" + i + "_" + $("#compare_id" + i + "" + eh).val()).val() != "" || $("#" + e_name + "_" + i + "_" + $("#compare_id" + i + "" + eh).val()).val() != undefined) && ($("#" + e_name + "_" + i + "_" + $("#expenses" + i + "" + eh).val()).val() != "" || $("#" + e_name + "_" + i + "_" + $("#expenses" + i + "" + eh).val()).val() != undefined)) {
                        if (parseFloat(c_val) > 0 && parseFloat(e_val) > 0) {
                            alert("Please Fill Either " + exp_name + " or " + c_name.replace(/_/gi, " "));
                            $("#" + c_name + "_" + i + "_" + $("#compare_id" + i + "" + eh).val()).val(0);
                            $("#" + e_name + "_" + i + "_" + $("#expenses" + i + "" + eh).val()).val(0);
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
                            if (isNaN(i_tot_exp[eh - 1])) {
                                i_tot_exp[eh - 1] = 0;
                            }
                            i_tot_exp[eh - 1] = parseFloat(i_tot_exp[eh - 1]) + parseFloat(exp_amt);

                            var amount_total = 0;
                            var amount_expense = 0;
                            amount_total = parseFloat(tot_limit[eh - 1]);
                            amount_expense = parseFloat(tot_exp[eh - 1]);
                            if (isNaN(amount_total)) {
                                amount_total = 0;
                            }
                            if (isNaN(amount_expense)) {
                                amount_expense = 0;
                            }
                            if (travel_mode == "Other" && reim_type == "Company Provided") {
                                $("#" + e_name + "_" + i + "_" + eh).css('color', 'black');
                            }
                            else {
                                if (e_name == "Boarding") {
                                    if (parseFloat(exp_amt) > parseFloat(h_exp_amt) && reim_type != "Company Provided") {
                                        $("#" + e_name + "_" + i + "_" + eh).css('color', '#FA564B');
                                        $("#dev_policy_amt").val(1);
                                    }
                                    else {
                                        $("#" + e_name + "_" + i + "_" + eh).css('color', 'black');
                                    }
                                }
                                else {
                                    if (parseFloat(amount_expense) > parseFloat(amount_total) && parseFloat(exp_amt) > 0 && reim_type != "Company Provided") {
                                        $("#" + e_name + "_" + i + "_" + eh).css('color', '#FA564B');
                                        $("#dev_policy_amt").val(1);
                                    }
                                    else {
                                        $("#" + e_name + "_" + i + "_" + eh).css('color', 'black');
                                    }
                                }
                            }
                        }
                        else {
                            if (exp_amt != undefined && exp_amt != "0") {
                                alert(exp_name + " Policy Not Available...");
                                $("#" + e_name + "_" + i + "_" + eh).val("");
                                return false;
                            }
                        }
                    }

                    else {
                        $("#" + e_name + "_" + i + "_" + eh).val("");
                    }
                }
                else {
                    if (exp_amt != undefined && exp_amt != "") {
                        if (chk_box == false) {
                            Total += parseFloat(exp_amt);
                        }
                    }
                    else {
                        $("#" + e_name + "_" + i + "_" + eh).val("");
                    }
                }
                $("#Total" + i).text(parseFloat(Total) + Hotel_Charge);
            }

            fTotal = parseFloat(fTotal) + parseFloat($("#Total" + i).text());
        }
        $("#Final_Amtt").text(fTotal);

        $("#spn_v_amt").text(fTotal);
        $("#spn_ns_amt").text(non_supp);
        $("#spn_sr_amt").text(supp_tot);
        var nr = parseFloat(fTotal) - parseFloat(supp_tot);
        $("#spn_snr_amt").text(nr);
        $("#spn_supp_diff").text(supp_tot - non_supp);
        if (non_supp == 0) {
            $("#spn_non_perc").text(0);
        }
        else {
            var perc = ((parseFloat(non_supp) / $("#spn_sr_amt").text()) * 100).toFixed(2);
            $("#spn_non_perc").text(perc);
        }

        var diff = 0;
        var tot = fTotal;
        var adv = $("#spn_adv_amt").text();
        diff = tot - parseFloat(adv);
        if (diff > 0) {
            $("#spn_Remark").text(diff + " - Pay To Requestor");
        }
        else if (diff < 0) {
            $("#spn_Remark").text((diff * (-1)) + " - Receive From Requestor");
        }
        else {
            $("#spn_Remark").text("NIL");
        }
        //alert("Policy - " + $("#dev_policy_amt").val() + " || Supporting - " + $("#dev_supp_amt").val() + " || Travel -" + $("#dev_travel_class").val());
    
    return true;
}

function calculate_Expense() {
    var Hotel_Charge, Total, fTotal, non_supp, supp_tot, percent_val;
    Hotel_Charge = Total = fTotal = non_supp = supp_tot = percent_val = 0;
    //  $("#dev_policy_amt").val(0);

    var i_tot_exp = new Array();
    var i_tot_limit = new Array();
    var d = document.getElementsByName('jt');
    for (var i = 1; i <= d.length; i++) {
        percent_val = 0;
        var exp = document.getElementsByName("eh" + i);
        Hotel_Charge = 0;
        Total = 0;
        for (var eh = 1; eh <= exp.length; eh++) {

            var exp_name = $("#exp_name" + i + "" + eh).val();
            var e_name = $("#e_name" + i + "" + eh).val();
            var c_name = $("#compare_name" + i + "" + eh).val();
            var chk_box = true;
            var fk_id = $("#e_fk_id" + i + "" + eh).val();

            var exp_amt = $("#" + e_name + "_" + i + "_" + eh).val();

            var h_exp_amt = $("#h" + e_name + "_" + i + "_" + eh).val();
            var d_allow = $("#D_ALLOW_" + e_name + "_" + i + "_" + eh).val();


            if (d_allow == 0) {
                if (exp_amt != undefined && exp_amt != "") {
                    if (h_exp_amt != "") {
                        if (isNaN(i_tot_limit[eh - 1])) {
                            i_tot_limit[eh - 1] = 0;
                        }
                        i_tot_limit[eh - 1] = parseFloat(i_tot_limit[eh - 1]) + parseFloat(exp_amt);

                    }
                }
            }

        }
    }

    return i_tot_limit;
}

function calculate_Limit() {
    var Hotel_Charge, Total, fTotal, non_supp, supp_tot, percent_val;
    Hotel_Charge = Total = fTotal = non_supp = supp_tot = percent_val = 0;
    //  $("#dev_policy_amt").val(0);

    var i_tot_exp = new Array();
    var i_tot_limit = new Array();
    var d = document.getElementsByName('jt');
    for (var i = 1; i <= d.length; i++) {
        percent_val = 0;
        var exp = document.getElementsByName("eh" + i);
        Hotel_Charge = 0;
        Total = 0;
        for (var eh = 1; eh <= exp.length; eh++) {

            var exp_name = $("#exp_name" + i + "" + eh).val();
            var e_name = $("#e_name" + i + "" + eh).val();
            var c_name = $("#compare_name" + i + "" + eh).val();
            var chk_box = true;
            var fk_id = $("#e_fk_id" + i + "" + eh).val();

            var exp_amt = $("#" + e_name + "_" + i + "_" + eh).val();

            var h_exp_amt = $("#h" + e_name + "_" + i + "_" + eh).val();
            var d_allow = $("#D_ALLOW_" + e_name + "_" + i + "_" + eh).val();


            if (d_allow == 0) {
                if (exp_amt != undefined && exp_amt != "") {
                    if (h_exp_amt != "") {
                        if (isNaN(i_tot_limit[eh - 1])) {
                            i_tot_limit[eh - 1] = 0;
                        }
                        i_tot_limit[eh - 1] = parseFloat(i_tot_limit[eh - 1]) + parseFloat(h_exp_amt);

                    }
                }
            }

        }
    }

    return i_tot_limit;
}

function deviation_Alert()
    {
        var dev_Policy, dev_supp, dev_Class, dev_string;
        dev_Policy = dev_supp = dev_Class = dev_string = "";

        var txt_po = $("#dev_policy_amt").val();
        var txt_su = $("#dev_supp_amt").val();
        var txt_tr = $("#dev_travel_class").val();

        if (txt_po == 1)
        {
            dev_Policy = "Policy Amount";
        }
        if (txt_su == 1)
        {
            dev_supp = "Supporting Amount";
        }
        if (txt_tr == 1)
        {
            dev_Class = "Travel Class";
        }

        if (dev_Policy != "")
        {
            dev_string = dev_Policy;
            if (dev_supp != "" && dev_Class == "")
            {
                dev_string += " and " + dev_supp;
            }
            else if (dev_supp != "" && dev_Class != "")
            {
                dev_string += ", " + dev_supp + " and " + dev_Class;
            }
            else if (dev_supp == "" && dev_Class != "")
            {
                dev_string += " and " + dev_Class;
            }
        }
        else
        {
            dev_string = dev_supp;
            if (dev_supp != "")
            {
                if (dev_Class != "")
                {
                    dev_string += " and " + dev_Class;
                }
            }
            else
            {
                dev_string = dev_Class;
            }
        }
        if (dev_string != "") {
            dev_string = "Request Deviated Due To " + dev_string + ". Confirm?";
            
            //dev_string = "Request Submitted Under Deviation. Confirm?";
            //alert(dev_string);
            if (confirm(dev_string)) {
                return true;
            }
            else {
                return false;
            }
        }
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
            $("#city_class" + i).html("Other");
            $("#txt_t_city" + i).show();
        }
        else {
            $("#txt_t_city" + i).hide();
            var pk_city = $("#To_City" + i).val();
            Domestic_Travel_Request.get_Travel_Class(pk_city, i, ClassSucceeded);
        }
    }

function ClassSucceeded(response) {
        var data = response.value;
        $("#placeclass" + data[0]).html(data[1]);
        var city_class="";
        if (data[1] == "A")
        {
            city_class = "Metro";
        }
        else if (data[1] == "B") {
            city_class = "Mini-Metro";
        }
        else  {
            city_class = "Other";
        }
        $("#city_class" + data[0]).html(city_class);
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
        var fileExt = '.' + filename.split('.').pop();

        if (parseFloat(filelength) == 0) {
            alert("Sorry Cannot Upload File ! File Is Empty Or File Does Not Exist");
        }
        else if (parseFloat(filelength) > 2097151) {
            alert("Sorry Cannot Upload File ! File Size Exceeded.");
        }
        else if (contentType == "application/octet-stream" && fileNameSplit[1] == "exe") {
            alert("Kindly Check File Type.");
        }
        else {
            var cnt = 0;
            if (fileExt.toUpperCase() == ".JPEG" || fileExt.toUpperCase() == ".JPG" || fileExt.toUpperCase() == ".PNG" || fileExt.toUpperCase() == ".PDF") {
                if ($("#ddlDocuments option:selected").index() < 1) {
                    alert("Please Select File Type");
                }
                else {

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

        var Document_Name = "";
        var tbl = $("#uploadTable");
        var lastRow = $('#uploadTable tr').length;
        var f_name = $("#ddlDocuments option:selected").text();
        //var html1 = "<tr><td style='display:none'><input class='hidden' type='text' name='txt_Country_Add" + lastRow + "' id='txt_Document_Name" + lastRow + "' value=" + Document_Name + " readonly ></input>" + Document_Name + "</td>";
        var html2 = "<tr><td><span id='f_name" + lastRow + "'>" + f_name + "</span></td><td><input class='hidden' type='text' name='txt_Region_Add" + lastRow + "' id='txt_Document_File" + lastRow + "' value=" + name + " readonly ></input><a id='a_downloadfiles" + lastRow + "' style='cursor: pointer' onclick=\"return downloadfiles('" + name + "');\" >" + name + "</a></td>";
        var html3 = "<td><i id='del" + lastRow + "' class='glyphicon glyphicon-trash' align='center' onclick=\"return deletefile(" + (lastRow) + ");\" ></td></tr>";
        var htmlcontent = $(html2 + "" + html3);
        $('#uploadTable').append(htmlcontent);


    }

function downloadfiles(name) {
        var app_path = $("#app_Path").val();
        window.open(app_path + '/Common/FileDownload.aspx?indentno=NA&filename=' + name + '&filetag=', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
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
        }
        catch (Exc) { }
    }

function callback_deletefile(response) {
        if (response.value != "") {
            alert("Document Removed Successfully..");
        }
    }

function get_travel_class(rno) {
    var Travel_Mode = $("#Travel_Mode" + rno + " option:selected").val();
    if (Travel_Mode == -1 || Travel_Mode==0)
    {
        $("#div_Travel_Class" + rno).hide();
    }
    else
    {
        $("#div_Travel_Class" + rno).show();
    }
    Domestic_Travel_Request.get_travel_class(Travel_Mode, rno, OnTravelClass);
    }

function OnTravelClass(response) {
        var data = response.value;
        var sp_data = data.split("||");
        var rno = sp_data[0];
        var rcds = sp_data[1].split("@@");
        $("#Travel_Class" + rno).html("");
        for (var i = 0; i < rcds.length; i++) {
            var crec = rcds[i].split("$$");
            var cval = crec[0];
            var ctext = crec[1];
            if (rno == 1) {
                $("#Travel_Class" + rno).append($("<option></option>").val(cval).html(ctext));
            }
            else {
                var j_val1 = $("#Travel_Class1").val();
                if (cval == j_val1) {
                    $("#Travel_Class" + rno).append($("<option selected='true'></option>").val(cval).html(ctext));
                }
                else {
                    $("#Travel_Class" + rno).append($("<option></option>").val(cval).html(ctext));
                }
            }
        }
    }

function enable_disable() {
        $("#ddlAdv_Location").prop('selectedIndex', 0);
        if ($("#ddl_Payment_Mode option:selected").text().toUpperCase() == "CASH") {
            $("#ddlAdv_Location").removeAttr('disabled');
            $("#lblPay").show();
            $("#ctrlPay").show();
        }
        else {
            $("#ddlAdv_Location").attr('disabled', 'disabled');
            $("#lblPay").hide();
            $("#ctrlPay").hide();
        }
    }

function show_record() {
        var search_data = $("#txt_search").val();
        var ddl = $("#ddlRecords option:selected").val();
        var desg = $("#txt_designation").val();
        var pgno = $("pageno").val();
        Domestic_Travel_Request.showall(search_data, 1, ddl, desg,division, OnshowAll);
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
            Domestic_Travel_Request.showall(strData, pgno, str, desg,division, OnshowAll);
        }
        catch (exception) {

        }
    }

function enable_disable_field(fk_id,index,id)
    {
        var ch = fk_id + "_" + index + "_" + id;
        $("#particular_SUP" + ch).val("");
        if ($("#ddl_SUP" + ch).val() == "Y") {
            $("#particular_SUP" + ch).show();
            $("#f_other" + ch).show();
        }
        else {
            $("#particular_SUP" + ch).hide();
            $("#f_other" + ch).hide();
        }
    }

function checkSupp_Docs() {
        var msg = "";
        var d = document.getElementsByName('jt');
        for (var i = 1; i <= d.length; i++) {
            var span_val = $("#spn_date_" + i).text();
            var exp = document.getElementsByName("eh" + i);
            for (var eh = 1; eh <= exp.length; eh++) {
                var fk_id = $("#e_fk_id" + i + "" + eh).val();
                var ename = $("#e_name" + i + "" + eh).val();

                var exp_amt = $("#" + ename + "_" + i + "_" + eh).val();
                var amount = 0;
                if (exp_amt != undefined && exp_amt != "")
                {
                    amount = parseInt(exp_amt);
                }
               
                var ddl_SUP = $("#ddl_SUP" + fk_id + "_" + i + "_" + eh + " option:selected").val();
                if (ddl_SUP == "Y")
                {
                    var particular_SUP = $("#particular_SUP" + fk_id + "_" + i + "_" + eh).val();
                    if (particular_SUP == "" && amount > 0)
                    {
                        msg="Enter Supporting Particulars For " + span_val + "...!";
                    }
                }
                var fk_other_remark = $("#fk_other_remark" + fk_id + "_" + i + "_" + eh).val();
                var e_name = $("#e_name" + i + "" + eh).val();
                var exp_amt = $("#" + e_name + "_" + i + "_" + eh).val();
                var other_remark = $("#other_remark" + fk_id + "_" + i + "_" + eh).val();
                if (other_remark == "" && fk_other_remark==1 && exp_amt!=undefined && exp_amt!="" && parseInt(exp_amt)>0)
                {
                    msg = "Enter Remark For " + ename + " Expense Head on " + span_val + "...!";
                }
            }
        }
        return msg;
}

function get_travel_deviate(rno) {
    var Travel_Mode = $("#Travel_Mode" + rno + " option:selected").val();
    var dev = Domestic_Travel_Request.get_travel_deviate(Travel_Mode);
    if (dev.value == 0) {
        $("#txt_dev_id" + rno).val(dev);
    }
}

function check_on_data(index)
{
    //get_exp_data(index);
    var from_loc, to_loc, journey_type;
    from_loc = to_loc = journey_type = "";
    journey_type = $("#journey_Type" + index + " option:selected").text();
    
    if (journey_type == "Overnight Stay Within Plant") {
        if (index == 1) {
            from_loc = $("#spn_base_location").html();
        }
        else {
            from_loc = $("#From_Plant" + (index - 1) + " option:selected").text();
        }
        //from_loc = $("#spn_base_location").html();
        to_loc = $("#From_Plant" + index + " option:selected").text();


        if (from_loc == "Roha" || from_loc == "Mahad" || from_loc == "Pune" || from_loc == "Sutarwadi") {
            $("#Boarding_" + index + "_2").val("");
            if ($("#chk_reach_" + index).prop("checked") == true) {
                $("#Boarding_" + index + "_2").prop("disabled", false);
            }
            else {
                $("#Boarding_" + index + "_2").prop("disabled", true);
            }
        }
        else {
            $("#Boarding_" + index + "_2").val("");
            $("#Boarding_" + index + "_2").prop("disabled", true);
        }

    }
    else if (journey_type == "One Day Outstation Within Plant") {
        from_loc = $("#From_Plant" + index+ " option:selected").text();
        to_loc = $("#spn_base_location").html();
        var to_loc1 = $("#To_Plant" + index + " option:selected").text();

        if ((from_loc == "Roha" || from_loc == "Mahad" || from_loc == "Pune" || from_loc == "Sutarwadi") && to_loc1 != to_loc) {
            $("#Boarding_" + index + "_2").val("");
            if ($("#chk_reach_" + index).prop("checked") == true) {
                $("#Boarding_" + index + "_2").prop("disabled", false);
            }
            else {
                $("#Boarding_" + index + "_2").prop("disabled", true);
            }
        }
        else {
            $("#Boarding_" + index + "_2").val("");
            $("#Boarding_" + index + "_2").prop("disabled", true);
        }

    }
}

function check_on_guest(index) {
    var from_loc, to_loc, journey_type;
    from_loc = to_loc = journey_type = "";
    journey_type = $("#journey_Type" + index + " option:selected").text();

    if (journey_type == "Overnight Stay Within Plant") {
        //if (index == 1) {
        //    from_loc = $("#spn_base_location").html();
        //}
        //else {
        //    from_loc = $("#From_Plant" + (index - 1) + " option:selected").text();
        //}
        from_loc = $("#spn_base_location").html();
        to_loc = $("#From_Plant" + index + " option:selected").text();


	//if ((from_loc == "Roha" || from_loc == "Mahad") && to_loc=="Pune") {
        if ((from_loc != "Pune" && from_loc != "Sutarwadi") && to_loc=="Pune") {
            $("#Boarding_" + index + "_2").val("");
            if ($("#chk_guest_" + index).prop("checked") == true) {
                $("#Boarding_" + index + "_2").prop("disabled", false);
            }
            else {
                $("#Boarding_" + index + "_2").prop("disabled", true);
            }
        }
        else {
            $("#Boarding_" + index + "_2").val("");
            $("#Boarding_" + index + "_2").prop("disabled", true);
        }

    }
}

function incidental_Base(index)
{
    var journey_type = $("#journey_Type" + index + " option:selected").text();
    var base_loc = $("#spn_base_location").html();
    if (journey_type == "Overnight Stay Within Plant") {
	var fp = $("#From_Plant" + index + " option:selected").text();
        if ((base_loc == "Roha" || base_loc == "Mahad" || base_loc == "Pune" || base_loc == "Sutarwadi") && fp!="Mktg") {
            $("#Incidentals_" + index + "_5").val(0);
        }
    }
else if (journey_type == "One Day Outstation Within Plant") {
	var fp = $("#From_Plant" + index + " option:selected").text();
        var tp = $("#To_Plant" + index + " option:selected").text();

        if (fp=="Mktg" && tp=="Mktg") {
          
        }
else
{
	 $("#Incidentals_" + index + "_5").val(0);
}
    }

}