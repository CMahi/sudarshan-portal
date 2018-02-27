
function show_List() {
    /*********************************************************************************/
    Currency_Master.bindCountryData(OnSuccess);
    /*********************************************************************************/
}

function OnSuccess(response) {
    try {
        $("#div_Details").html(response.value);
        $("#data-table1").dataTable({ 'bSort': false });
    }
    catch (ex) {
        //alert(ex);
    }
}

function checkControls() {
    if ($("#currency_name").val() == "") {
        alert("Please Enter Currency.");
        return false;
    }
    if ($("#exc_rate").val() == "") {
        alert("Please Enter Exchange Rate.");
        return false;
    }
    else {
        var exc=$("#exc_rate").val();
        if (isNaN(exc))
        {
            alert("Please Enter valid Exchange Rate.");
            $("#exc_rate").val(0);
            return false;
        }
        if (parseFloat(exc) <= 0) {
            alert("Please Enter valid Exchange Rate.");
            $("#exc_rate").val(0);
            return false;
        }

    }
    
    $("#divIns").show();
    return true;
}

function checkControls1() {
    if ($("#ed_currency_name").val() == "") {
        alert("Please Enter Currency.");
        return false;
    }
    if ($("#ed_exc_rate").val() == "") {
        alert("Please Enter Exchange Rate.");
        return false;
    }
    else {
        var exc = $("#ed_exc_rate").val();
        if (isNaN(exc)) {
            alert("Please Enter valid Exchange Rate.");
            $("#ed_exc_rate").val(0);
            return false;
        }
        if (parseFloat(exc) <= 0) {
            alert("Please Enter valid Exchange Rate.");
            $("#ed_exc_rate").val(0);
            return false;
        }
    }

    var currency = $("#ed_currency_name").val();
    var exc_rate = $("#ed_exc_rate").val();
    var pk_id = $("#txt_pk_id").val();
    $("#divUpd").show();
    Currency_Master.updateReportingData(currency, exc_rate, 3, pk_id, OnUpdate);
}

function OnUpdate(response) {
    $("#divUpd").hide();
    $("#UpdRepPanel").modal('hide');
    if (response.value == "true") {
        alert('Data Updated Successfully...!');
        show_List();
        return true;
    }
    else {
        alert(response.value);
        return false;
    }
}

function getReportingData(pk_id) {

    /*********************************************************************************/
    Currency_Master.getReporting(pk_id, 1, OnSuccess1);

    /*********************************************************************************/
}

function OnSuccess1(response) {
    var res = response.value;
    
    $("#ed_currency_name").val(res[1]);
    $("#ed_exc_rate").val(res[2]);
    $("#txt_pk_id").val(res[3]);
}

function deleteReporting(pk_id) {
    $("#txtResult").val(pk_id);
}

function deleteData() {
    var pk_id = $("#txtResult").val();
    $("#divDel").show();
    Currency_Master.updateReportingData("", "0", 4, pk_id, OnDelete);
}

function OnDelete(response) {

    $("#divDel").hide();
    $("#DeletePanel").modal('hide');
    if (response.value == "true") {
        alert('Data Deleted Successfully...!');
        show_List();
        return true;
    }
    else {
        alert(response.value);
        return false;
    }
}

function clearControls() {
    $("#currency_name").val("");
    $("#exc_rate").val("");
    $("#currency_name").focus();
    return true;
}


$(document).ready(function () {
    App.init();
    Demo.init();
    PageDemo.init();
    Currency_Master.bindCountryData(OnSuccess);
});