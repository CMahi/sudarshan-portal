function hide_List() {
    $("#div_ListCountryDetails").hide();
    $("#div_AddCountryDetails").show();
}

function show_List() {
    $("#div_ListCountryDetails").show();
    $("#div_AddCountryDetails").hide();

    /*********************************************************************************/
    Vendor_Master.bindCityData(OnSuccess);
    /*********************************************************************************/
}

function OnSuccess(response) {
    document.getElementById("div_Details").innerHTML = response.value;
    $('#data-table1').dataTable();
}

function checkControls() {
    if ($("vendor_code").val() == "") {
        alert('Please Enter Vendor Code');
        return false;
    }
    else {
        if ($("#vendor_name").val() == "") {
            alert('Please Enter Vendor Name');
            return false;
        }
        //else {
        return true;
        //}

    }
}

function getCityData(index) {
    /*********************************************************************************/
    $("#txtResult").val(index);
    Vendor_Master.getCityData(index, OnSuccess1);

    /*********************************************************************************/

}

function OnSuccess1(response) {
    var res = response.value;
    var data = res.split("||");

    $("#edit_vendor_code").val(data[0]);
    $("#edit_vendor_name").val(data[1]);
}

function checkControls1() {
    if ($("#edit_vendor_code").val() == "") {
        alert('Please Enter Vendor Code');
        return false;
    }
    else {
        if ($("#edit_vendor_name").val() == "") {
            alert('Please Enter Vendor Name');
            return false;
        }
        //else {
        var pk_id = $("#txtResult").val();
        var vcode = $("#edit_vendor_code").val();
        var vname = $("#edit_vendor_name").val();
        Vendor_Master.updateCityData(pk_id, vcode, vname, 2, OnUpdate);
        //}

    }
}

function OnUpdate(response) {
    if (response.value == "true") {
        alert('Vendor Data Has Been Updated...!');
        $("#btnClose")[0].click();
        show_List();
        return true;
    }
    else {
        alert(response.value);
        return false;
    }
}

function deleteCity(index) {
    //var pk_id = $("#pk_city_id" + index).val();
    $("#txtResult").val(index);
}

function deleteData(index) {
    var pk_id = $("#txtResult").val();
    Vendor_Master.updateCityData(pk_id, "", "", 3, OnDelete);
}

function OnDelete(response) {

    if (response.value == "true") {
        alert('Vendor Data Has Been Deleted...!');
        $("#deleteClose")[0].click();
        show_List();
        return true;
    }
    else {
        alert(response.value);
        return false;
    }
}

$(document).ready(function () {
    App.init();
    Demo.init();
    PageDemo.init();

});