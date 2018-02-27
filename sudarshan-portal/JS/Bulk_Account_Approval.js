function searchData() {
    var str = document.getElementById("txt_Search").value;
    var rpp = "0";

    if ($("#ddlRecords option:selected").text() != undefined) {
        rpp = $("#ddlRecords :selected").text();
        $("#ddlText1").val($("#ddlRecords option:selected").text());
    }
    else {
        rpp = $("#ddlText1").val();
    }
    var pmode = $("#ddlPay_Mode :selected").text();
    Bulk_Account_Approval.fillSearch(str, rpp, pmode, callback_Inco);
    //Bulk_Account_Approval.fillSearchNo(str, rpp, pmode, callback_Inco1);
    //Bulk_Account_Approval.fillSearchRec(str, rpp, pmode, callback_Inco2);
}

function callback_Inco(response) {
    var data = response.value;
    var sp_data = data.split("@@");
    document.getElementById("div_InvoiceDetails").innerHTML = sp_data[0];
    if (sp_data[1] != "" && sp_data[1] != undefined) {
        $("#No_of_records").val(sp_data[1]);
    }
    else {
        $("#No_of_records").val(0);
    }

    if (sp_data[2] != "" && sp_data[2] != undefined) {
        $("#rec_data").val(sp_data[2]);
    }
    else {
        $("#rec_data").val("");
    }
    //$('#data-table1').dataTable();

    var oTable = $('#data-table1').dataTable({
        stateSave: true
    });

    var allPages = oTable.fnGetNodes();

    $('body').on('click', '#select_all', function () {
        if ($(this).hasClass('allChecked')) {
            $('input[type="checkbox"]', allPages).prop('checked', false);
            for (var index = 1; index <= $("#No_of_records").val() ; index++) {
                check_uncheck1(0, index);
            }

        } else {
            $('input[type="checkbox"]', allPages).prop('checked', true);
            for (var index = 1; index <= $("#No_of_records").val() ; index++) {
                check_uncheck1(1, index);
            }
        }
        $(this).toggleClass('allChecked');

    })
}


function gotopage(objButton, str) {
    try {
        var pgno = objButton.value;
        var strData = $("#txt_Search").val();
        var pmode = $("#ddlPay_Mode :selected").text();
        if (strData != undefined || strData != null) {
            Bulk_Account_Approval.fillGoToPage1(strData, pgno, str, pmode, callback_Inco);
        }
        else {
            Bulk_Account_Approval.fillGoToPage(pgno, str, pmode, callback_Inco);
        }
    }
    catch (exception) {

    }
}

function downloadfiles(req_no,file_name) {
    var app_path = $("#app_Path").val();
//    var req_no = $("#request_no_" + index).val();
    window.open(app_path + '/Common/FileDownload.aspx?indentno=' + req_no + '&filename=' + file_name + '&filetag=', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
}

function prepareData() {
    try {
        var chk = 0;
        var no_rec = $("#No_of_records").val();
        var PK_Dispatch_Note_ID = "";
        if ($("#ddlPay_Mode option:selected").text().toUpperCase() == "BANK") {
            PK_Dispatch_Note_ID = $("#chk_records").val();
            //alert($("#chk_records").val());
            //return false;

        }
        else {
            PK_Dispatch_Note_ID = $("#chk_records").val();
            //var d = document.getElementsByName('single');
            //for (var i = 0; i < d.length; i++) {
            //    if (d[i].checked) {
            //        chk = chk + 1;
            //        if (chk == 1) {
            //            PK_Dispatch_Note_ID = $("#PK_Dispatch_Note_ID" + (i + 1)).val() + "$" + $("#fk_process" + (i + 1)).val() + "$" + $("#iid" + (i + 1)).val() + "$" + $("#wiid" + (i + 1)).val() + "$" + $("#h_info" + (i + 1)).val();
            //        }
            //        else {
            //            PK_Dispatch_Note_ID = PK_Dispatch_Note_ID + "@" + $("#PK_Dispatch_Note_ID" + (i + 1)).val() + "$" + $("#fk_process" + (i + 1)).val() + "$" + $("#iid" + (i + 1)).val() + "$" + $("#wiid" + (i + 1)).val() + "$" + $("#h_info" + (i + 1)).val();
            //        }
            //    }
            //}
        }

        $("#split_data").val(PK_Dispatch_Note_ID);

        if (PK_Dispatch_Note_ID=="") {
            alert('Select Atleast One Expense Request...!');
            return false;
        }
        else {
            //alert(PK_Dispatch_Note_ID);
            //return false;
            $("#divIns").show();
            return true;
        }
    }
    catch (exception)
    { 
$("#divIns").hide();
alert(exception);
 }
}

function check_radio(obj,req_no,pid,iid,wiid)
{
    $("#chk_records").val("");
    if (obj.checked) {
        var data = req_no + "$" + pid + "$" + iid + "$" + wiid + "$"+req_no;
        $("#chk_records").val(data);
    }
}
function check_uncheck(obj,index)
{
    
    var selected = 0;
    var avail_data = $("#chk_records").val();
    var sp_data = $("#chk_records").val().split("@");
    var sp_data1 = $("#rec_data").val().split("@");
    //var data = $("#PK_Dispatch_Note_ID" + index).val() + "$" + $("#fk_process" + index).val() + "$" + $("#iid" + index).val() + "$" + $("#wiid" + index).val() + "$" + $("#h_info" + index).val();
    var data1 = sp_data1[index - 1].split("$");
    var data = data1[2] + "$" + data1[1] + "$" + data1[3] + "$" + data1[4] + "$" + data1[2];
    
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
            selected = selected + 1;
            if ($("#chk_records").val() == "") {
                $("#chk_records").val(sp_data[i]);
            }
            else {
                $("#chk_records").val($("#chk_records").val() + "@" + sp_data[i]);
            }
        }
    }
    else {
        $("#select_all").prop("checked",false);
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
    $("#selected_rec").val(selected);
    if (selected == $("#No_of_records").val() && selected != 0)
    {
        $("#select_all").prop("checked",true);
    }
    // alert($("#chk_records").val());
    //return false;
}

    function check_uncheck1(obj, index) {
        var avail_data = $("#chk_records").val();
        var sp_data = $("#chk_records").val().split("@");
        var sp_data1 = $("#rec_data").val().split("@");
        //var data = $("#PK_Dispatch_Note_ID" + index).val() + "$" + $("#fk_process" + index).val() + "$" + $("#iid" + index).val() + "$" + $("#wiid" + index).val() + "$" + $("#h_info" + index).val();
        var data1 = sp_data1[index - 1].split("$");
        var data = data1[2] + "$" + data1[1] + "$" + data1[3] + "$" + data1[4] + "$" + data1[2];

        if (obj==1) {
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
        //alert($("#chk_records").val());
        //return false;
    }

    function Bind_Documents(row_id) {
        var wiid = $("#header" + row_id).val();
        var proc_name = $("#pname" + row_id).val();
        Bulk_Account_Approval.fillDocumentDetails(proc_name, wiid, callback_Bind);
    }

    function callback_Bind(response) {
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
            Bulk_Account_Approval.getUserInfo(wiid, callback_BindData);
            Bulk_Account_Approval.fillAdvanceAmount(callback_BindAdvance);
            Bulk_Account_Approval.fillaudittrail($("#txtProcessID").val(), $("#txtInstanceID").val(), callback_bindAudit);

            var fromDate = $("#travelFromDate" + index).val();
            var toDate = $("#travelToDate" + index).val();
            getDiffDays(fromDate, toDate);
            getSelectDate(fromDate, toDate);
        }
        else if ($("#txtProcessID").val() == 17) {
            Bulk_Account_Approval.getOtherExpenseUser(wiid, callback_BindData);
            Bulk_Account_Approval.fillRequest_data(wiid, callback_BindOther);
            Bulk_Account_Approval.fillaudittrail($("#txtProcessID").val(), $("#txtInstanceID").val(), callback_bindAudit);
        }
        else {
            $("#div_header").html("");
        }
    }

    function callback_bindAudit(response) {
        $("#div_Audit").html(response.value);
    }

    function callback_BindOther(response) {
        var data = (response.value).split("@@");
        $("#div_req_data").html(data[0]);
        $("#spn_Total").html(data[1]);
    }

    function callback_BindAdvance(response) {
        var rec_data = (response.value).split("@");
        $("#div_Advance").html(rec_data[0]);
        var adv = rec_data[1];
        var exp = rec_data[2];
        if (exp > adv) {
            $("#spn_hdr").html("Payable Amount : ");
            $("#spn_val").html(exp - adv);
        }
        else {
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
        for (var i = 0; i < rec_data.length; i++) {
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

    function getRequestDetails(index)
    {
        var process_name = $("#pname" + index).val();
        var req_no = $("#h_info" + index).val();
        //alert(process_name + " - " + req_no);
        Bulk_Account_Approval.getDetails(req_no, process_name, callback_BindRequestData);
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

    function getexpensedtl1() {

        var html = "";
        var html1 = "";
        var html2 = "";
        var html3 = "";
        var html4 = "";
        var html5 = "";
        var html6 = "";
        var amt = 0;
        var amtf = 0;
        var amtm = 0;
        var amtt = 0;
        var amtd = 0;
        var amtb = 0;

        $('#tbl_expns tbody').remove();

        var lastRow = $('#tblfuel tr').length;
        var lastRow1 = $('#tblfuel tr').length - 1;
        if (lastRow > 1) {
            for (var n = 1; n <= lastRow1; n++) {
                var fueldate = $("#txtfueldate" + n).val();
                var fuelamount = $("#txtfuelamount" + n).val();

                amtf = parseFloat(amtf) + parseFloat($("#txtfuelamount" + n).val());

            }
            html += "<tr>";
            html += "<td>" + $("#fgl1").val() + "</td>";
            html += "<td>Fuel</td>";
            html += "<td align='right'>" + amtf + "</td></tr>";
            amt = parseFloat(amt) + parseFloat(amtf);
            $('#tbl_expns').append(html);
        }

        var lastRowm = $('#tblmaintenance tr').length;
        var lastRow1m = $('#tblmaintenance tr').length - 1;
        if (lastRowm > 1) {
            for (var j = 1; j <= lastRow1m; j++) {
                var maintainancedate = $("#txtmaintenancedate" + j).val();
                var maintainanceamount = $("#txtmaintenanceamount" + j).val();

                amtm = parseFloat(amtm) + parseFloat($("#txtmaintenanceamount" + j).val());
            }
            html1 += "<tr>";
            html1 += "<td>" + $("#mgl1").val() + "</td>";
            html1 += "<td>Maintenance</td>";
            html1 += "<td align='right'>" + amtm + "</td></tr>";
            amt = parseFloat(amt) + parseFloat(amtm);
            $('#tbl_expns').append(html1);
        }
        var lastRow = $('#tbldriver tr').length;
        var lastRow1d = $('#tbldriver tr').length - 1;
        if (lastRow > 1) {
            for (var k = 1; k <= lastRow1d; k++) {
                var driverdate = $("#txtdriverdate" + k).val();
                var driveramount = $("#txtdriveramount" + k).val();

                amtd = parseFloat(amtd) + parseFloat($("#txtdriveramount" + k).val());
            }
            html2 += "<tr>";
            html2 += "<td>" + $("#dgl1").val() + "</td>";
            html2 += "<td>Driver</td>";
            html2 += "<td align='right'>" + amtd + "</td></tr>";
            amt = parseFloat(amt) + parseFloat(amtd);
            $('#tbl_expns').append(html2);
        }

        var lastRowb = $('#tblbattery tr').length;
        var lastRow1b = $('#tblbattery tr').length - 1;
        if (lastRowb > 1) {
            for (var j = 1; j <= lastRow1b; j++) {
                var amount = $("#txtbamount" + j).val();
                if (amount != "") {

                    amtb = parseFloat(amtb) + parseFloat($("#txtbamount" + j).val());
                }
            }
            html6 += "<tr>";
            html6 += "<td>" + $("#bgl1").val() + "</td>";
            html6 += "<td>Battery</td>";
            html6 += "<td align='right'>" + amtb + "</td></tr>";
            amt = parseFloat(amt) + parseFloat(amtb);
            $('#tbl_expns').append(html6);
        }

        var lastRowm = $('#tbltyre tr').length;
        var lastRow1m = $('#tbltyre tr').length - 1;
        if (lastRowm > 1) {
            for (var j = 1; j <= lastRow1m; j++) {
                var amount = $("#txtamount" + j).val();
                if (amount != "") {

                    amtt = parseFloat(amtt) + parseFloat($("#txtamount" + j).val());
                }
            }
            html5 += "<tr>";
            html5 += "<td>" + $("#tgl1").val() + "</td>";
            html5 += "<td>Tyre</td>";
            html5 += "<td align='right'>" + amtt + "</td></tr>";
            amt = parseFloat(amt) + parseFloat(amtt);
            $('#tbl_expns').append(html5);
        }



        html4 += "<tr><td></td>";
        html4 += "<td align='right'>Total</td>";
        html4 += "<td align='right'>" + amt + "</td></tr>";
        $('#tbl_expns').append(html4);
    }
