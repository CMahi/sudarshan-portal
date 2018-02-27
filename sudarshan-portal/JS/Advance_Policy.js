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
        else {
            return true;
        }

    }
}

function getCityData(index) {
    var pk_id = $("#pk_city_id" + index).val();
    /*********************************************************************************/
    $("#txtResult").val(pk_id);
    City.getCityData(pk_id, OnSuccess1);

    /*********************************************************************************/

}

function OnSuccess1(response) {
    var res = response.value;
    var data = res.split("||");

    $("#txt_Edit_City").val(data[0]);
    $("#ddlEditCountry").val(data[1]);
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
        else {
            var pk_id = $("#txtResult").val();
            var cname = $("#txt_Edit_City").val();
            var ctype = $("#ddlEditCountry").val();
            City.updateCityData(pk_id, cname, ctype, 2, OnUpdate);
        }

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
    var pk_id = $("#pk_city_id" + index).val();
    $("#txtResult").val(pk_id);
}

function deleteData(index) {
    var pk_id = $("#txtResult").val();
    City.updateCityData(pk_id, "", 0, 3, OnDelete);
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