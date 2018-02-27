function hide_List() {
    $("#div_ListCountryDetails").hide();
    $("#div_AddCountryDetails").show();
}

function show_List() {
    $("#div_ListCountryDetails").show();
    $("#div_AddCountryDetails").hide();

    /*********************************************************************************/
    City.bindCityData(OnSuccess);
    /*********************************************************************************/
}

function OnSuccess(response) {
    document.getElementById("div_Details").innerHTML = response.value;
    $('#data-table1').dataTable();
}

function checkControls() {
    if ($("#ddlCountry option:selected").index() == 0) {
        alert('Please Select Country');
        return false;
    }
    else {
        if ($("#txt_City_Name").val() == "") {
            alert('Please Enter City');
            return false;
        }
        if ($("#ddl_Classify option:selected").index() < 1) {
            alert('Please Select City Class');
            return false;
        }
        //else {
            return true;
        //}

    }
}

function getCityData(index) {
    //var pk_id = $("#pk_city_id" + index).val();
    /*********************************************************************************/
    $("#txtResult").val(index);
    City.getCityData(index, OnSuccess1);

    /*********************************************************************************/

}

function OnSuccess1(response) {
    var res = response.value;
    var data = res.split("||");

    $("#txt_Edit_City").val(data[0]);
    $("#ddlEditCountry").val(data[1]);
    $("#ddl_Edit_Classify").val(data[2]);
}

function checkControls1() {
    if ($("#ddlEditCountry option:selected").index() == 0) {
        alert('Please Select Country');
        return false;
    }
    else {
        if ($("#txt_Edit_City").val() == "") {
            alert('Please Enter City');
            return false;
        }
        if ($("#ddl_Edit_Classify option:selected").index() < 1) {
            alert('Please Select City Class');
            return false;
        }
        //else {
            var pk_id = $("#txtResult").val();
            var cname = $("#txt_Edit_City").val();
            var ctype = $("#ddlEditCountry").val();
            var classify = $("#ddl_Edit_Classify").val();
            City.updateCityData(pk_id, cname, ctype, 2, classify, OnUpdate);
        //}

    }
}

function OnUpdate(response) {
    if (response.value == "true") {
        alert('Country Data Has Been Updated...!');
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
    City.updateCityData(pk_id, "", 0, 3,"", OnDelete);
}

function OnDelete(response) {

    if (response.value == "true") {
        alert('City Data Has Been Deleted...!');
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