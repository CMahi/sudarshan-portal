function downloadfiles(index) {
    try {
        // Car_Expense_Approval.GetFileNames(index, OnSuccess1);
        var app_path = $("#app_Path").val();
        var expnsno = $("#txt_Request").val();
        var tbl = document.getElementById("uploadTable");

        window.open(app_path + '/Common/FileDownload.aspx?indentno=' + expnsno + '&filename=' + tbl.rows[index].cells[2].innerText + '&filetag=', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
    }
    catch (exception) {

    }
}

function OnSuccess1(response) {
    var app_path = $("#app_Path").val();
    window.open(app_path + '/Common/FileDownload.aspx?indentno=NA&filename=' + response.value + '&filetag=', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
}

function getexpensedtl() {

    var html = "";
    var html1 = "";
    var html2 = "";
    var html3 = "";
    var html4 = "";
    var html5 = "";
    var amt = 0;
    var amtf = 0;
    var amtm = 0;
    var amtt = 0;
    var amtd = 0;

    $('#tbl_expns tbody').remove();

    var lastRow = $('#tblfuel tr').length;
    var lastRow1 = $('#tblfuel tr').length - 1;
    for (var n = 1; n <= lastRow1; n++) {
        var fueldate = $("#txtfueldate" + n).val();
        var fuelamount = $("#txtfuelamount" + n).val();

        amtf = parseFloat(amtf) + parseFloat($("#txtfuelamount" + n).val());

    }
    html += "<tr><td></td>";
    html += "<td>Fuel</td>";
    html += "<td align='left'>" + amtf + "</td></tr>";
    amt = parseFloat(amt) + parseFloat(amtf);

    $('#tbl_expns').append(html);


    var lastRowm = $('#tblmaintenance tr').length;
    var lastRow1m = $('#tblmaintenance tr').length - 1;
    for (var j = 1; j <= lastRow1m; j++) {
        var maintainancedate = $("#txtmaintenancedate" + j).val();
        var maintainanceamount = $("#txtmaintenanceamount" + j).val();

        amtm = parseFloat(amtm) + parseFloat($("#txtmaintenanceamount" + j).val());
    }
    html1 += "<tr><td></td>";
    html1 += "<td>Maintenance</td>";
    html1 += "<td align='left'>" + amtm + "</td></tr>";
    amt = parseFloat(amt) + parseFloat(amtm);
    $('#tbl_expns').append(html1);

    var lastRow = $('#tbldriver tr').length;
    var lastRow1d = $('#tbldriver tr').length - 1;

    for (var k = 1; k <= lastRow1d; k++) {
        var driverdate = $("#txtdriverdate" + k).val();
        var driveramount = $("#txtdriveramount" + k).val();

        amtd = parseFloat(amtd) + parseFloat($("#txtdriveramount" + k).val());
    }
    html2 += "<tr><td></td>";
    html2 += "<td>Driver</td>";
    html2 += "<td align='left'>" + amtd + "</td></tr>";
    amt = parseFloat(amt) + parseFloat(amtd);
    $('#tbl_expns').append(html2);

    var lastRowm = $('#tbltyre tr').length;
    var lastRow1m = $('#tbltyre tr').length - 1;
    for (var j = 1; j <= lastRow1m; j++) {
        var amount = $("#txtamount" + j).val();
        if (amount != "") {

            amtt = parseFloat(amtt) + parseFloat($("#txtamount" + j).val());
        }
    }
    html5 += "<tr><td></td>";
    html5 += "<td>Tyre/Battery</td>";
    html5 += "<td align='left'>" + amtt + "</td></tr>";
    amt = parseFloat(amt) + parseFloat(amtt);
    $('#tbl_expns').append(html5);

    html4 += "<tr><td></td>";
    html4 += "<td align='right'>Total</td>";
    html4 += "<td align='left'>" + amt + "</td></tr>";
    $('#tbl_expns').append(html4);
}

function hide_show_remark() {
    $("#txt_Remark").val("");
    if ($("#ddlAction option:selected").index() < 2) {
        $("#div_remark").hide();
        $("#lbl_rmk").hide();
    }
    else {
        $("#div_remark").show();
        $("#lbl_rmk").show();
    }
}

function prepareData() {

    var act = document.getElementById("ddlAction").value;
    if (act == "" || act == "0") {
        alert("Please Select Action first...!");
        return false;
    }
    var remrk = document.getElementById("txtRemark").value;
    if (act != "Approve") {
        if (remrk == "") {
            alert("Please Enter Remark...!");
            return false;
        }
    }
    var modal = $find("MP_Loading");
    modal.show();
    return true;

}

function prepareData1() {

    var paymode = document.getElementById("ddl_Payment_Mode").value;
    var location = document.getElementById("ddlAdv_Location").value;

    if (paymode == "" || paymode == "0" || paymode == "---Select One---") {
        alert("Please Select Payment Mode...!");
        return false;
    }

    if (paymode == 1) {
        if (location == "" || location == "0" || location == "---Select One---") {

            alert("Please Select Payment Location...!");
            return false;
        }
    }

    return true;

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
        html += "<tr><td></td>";
        html += "<td>" + $("#txt_glcodef").val() + "</td>";
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
        html1 += "<tr><td></td>";
        html1 += "<td>" + $("#txt_glcodem").val() + "</td>";
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
        html2 += "<tr><td></td>";
        html2 += "<td>" + $("#txt_glcoded").val() + "</td>";
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
        html6 += "<tr><td></td>";
        html6 += "<td>" + $("#txt_glcodet").val() + "</td>";
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
        html5 += "<tr><td></td>";
        html5 += "<td>" + $("#txt_glcodet").val() + "</td>";
        html5 += "<td>Tyre</td>";
        html5 += "<td align='right'>" + amtt + "</td></tr>";
        amt = parseFloat(amt) + parseFloat(amtt);
        $('#tbl_expns').append(html5);
    }

   

    html4 += "<tr><td></td>";
    html4 += "<td></td>";
    html4 += "<td align='right'>Total</td>";
    html4 += "<td align='right'>" + amt + "</td></tr>";
    $('#tbl_expns').append(html4);
}

