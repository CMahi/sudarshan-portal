function hide_List() {
    $("#div_ListCountryDetails").hide();
    $("#div_AddCountryDetails").show();
}

function show_List() {
    $("#div_ListCountryDetails").show();
    $("#div_AddCountryDetails").hide();
    
    /*********************************************************************************/
    Country.bindCountryData(OnSuccess);
    /*********************************************************************************/
}

function OnSuccess(response) {
    document.getElementById("div_Details").innerHTML = response.value;
}

function checkControls() {
    if ($("#ddlCountry_Type option:selected").index() == 0) {
        alert('Please Select Country Type');
        return false;
    }
    else {
        if ($("#txt_Country_Name").val() == "") {
            alert('Please Enter Country');
            return false;
        }
        else {
            if ($("#ddlCurrency option:selected").index() == 0) {
                {
                    alert('Please Select Currency');
                    return false;
                }
            }
            else {
                return true;
            }
        }

    }
}

function checkControls1() {
    if ($("#ddlEditType option:selected").index() == 0) {
        alert('Please Select Country Type');
        return false;
    }
    else {
        if ($("#txt_Edit_Country").val() == "") {
            alert('Please Enter Country');
            return false;
        }
        else {
            if ($("#ddlEditCurrency option:selected").index() == 0) {
                {
                    alert('Please Select Currency');
                    return false;
                }
            }
            else {
                var pk_id = $("#txtResult").val();
                var cname = $("#txt_Edit_Country").val();
                var ctype = $("#ddlEditType :selected").text();
                var curr = $("#ddlEditCurrency :selected").text();
                Country.updateCountryData(pk_id,cname,ctype,curr,2, OnUpdate);
            }
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
function getCountryData(index) {
    var pk_id = $("#pk_country_id" + index).val();
    /*********************************************************************************/
    $("#txtResult").val(pk_id);
    Country.getCountryData(pk_id,OnSuccess1);

    /*********************************************************************************/

}

function OnSuccess1(response) {
    var res = response.value;
    var data = res.split("||");

    $("#txt_Edit_Country").val(data[0]);
    $("#ddlEditType option:contains(" + data[1] + ")").attr('selected', 'selected');
    $("#ddlEditCurrency option:contains(" + data[2] + ")").attr('selected', 'selected');
}

function deleteCountry(index) {
    var pk_id = $("#pk_country_id" + index).val();
    $("#txtResult").val(pk_id);
}

function deleteData(index) {
    var pk_id = $("#txtResult").val();
    Country.updateCountryData(pk_id, "", "", "", 3, OnDelete);
}

function OnDelete(response) {

    if (response.value == "true") {
        alert('Country Data Has Been Deleted...!');
        $("#deleteClose")[0].click();
        show_List();
        return true;
    }
    else {
        alert(response.value);
        return false;
    }
}