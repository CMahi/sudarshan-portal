function callback_Inco(response) {
    $("#txt_Data").val(response.value);
    document.getElementById("div_ReportDetails").innerHTML = response.value;
}

function searchData() {
    var str = $("#txt_Search").val();
    if (checkControls()) {
        var rpp = "0";

        if ($("#ddlRecords :selected").text() != undefined) {
            rpp = $("#ddlRecords :selected").text();
            $("#ddlText1").val($("#ddlRecords :selected").text());
        }
        else {
            rpp = $("#ddlText1").val();
        }

        var jccode = $("#jccode").val();
        var jfdate = $("#jfdate").val();
        var jtdate = $("#jtdate").val();
        var jnoteline = $("#jnoteline").val();

        Vendor_Account_Statement.fillSearch(jccode, jfdate, jtdate, jnoteline, str, rpp, callback_Inco);
    }
}

function gotopage(objButton, str) {
    try {
        var pgno = objButton.value;
        var strData = $("#txt_Search").val();
        if (strData == undefined || strData == null) {
            strData = "";
        }
        var jccode = $("#jccode").val();
        var jfdate = $("#jfdate").val();
        var jtdate = $("#jtdate").val();
        var jnoteline = $("#jnoteline").val();
        Vendor_Account_Statement.fillGoToPage1(jccode, jfdate, jtdate, jnoteline, strData, pgno, str, callback_Inco);
    }
    catch (exception) {

    }
}