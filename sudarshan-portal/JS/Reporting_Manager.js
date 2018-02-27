function hide_List() {
    $("#div_ListCountryDetails").hide();
    $("#div_AddCountryDetails").show();
}

function show_List() {
    $("#div_ListCountryDetails").show();
    $("#div_AddCountryDetails").hide();

    /*********************************************************************************/
    Reporting_Manager.bindCountryData(OnSuccess);
    /*********************************************************************************/
}

function OnSuccess(response) {
    try{
        $("#div_Details").html(response.value);
        $("#data-table1").dataTable({ 'bSort': false});
    }
    catch(ex)
    {
        //alert(ex);
    }
}

function checkControls() {
    if ($("#emp_code").val() == "") {
        alert("Please Enter Employee Code.");
        return false;
    }
    if (isNaN($("#emp_code").val()) && parseInt($("#emp_code").val())==0) {
        alert("Please Enter Valid Employee Code.");
        return false;
    }
    if ($("#emp_name").val() == "") {
        alert("Please Enter Employee Name.");
        return false;
    }
    if ($("#emp_level option:selected").index() < 1) {
        alert("Please Select Employee Level.");
        return false;
    }
    else {
        //if ($("#emp_level").val().toUpperCase() == "L1" || $("#emp_level").val().toUpperCase() == "L2" || $("#emp_level").val().toUpperCase() == "L3" || $("#emp_level").val().toUpperCase() == "L4" || $("#emp_level").val().toUpperCase() == "L5" || $("#emp_level").val().toUpperCase() == "DIRECTORS" || $("#emp_level").val().toUpperCase() == "ACTIVE") {
        //}
        //else {
        //    alert("Please Enter Valid Employee Level.");
        //    return false;
        //}
    }
    if ($("#emp_designation option:selected").index() < 1) {
        alert("Please Select Employee Designation.");
        return false;
    }
    if ($("#rep_emp_code").val() == "") {
        alert("Please Enter Reporting Employee Code.");
        return false;
    }
    if ($("#rep_emp_name").val() == "") {
        alert("Please Enter Reporting Employee Name.");
        return false;
    }
    $("#divIns").show();
    return true;
}

function checkControls1() {
    if ($("#ed_emp_code").val() == "") {
        alert("Please Enter Employee Code.");
        return false;
    }
    if ($("#ed_emp_name").val() == "") {
        alert("Please Enter Employee Name.");
        return false;
    }
    if ($("#ed_emp_level option:selected").index() < 1) {
        alert("Please Select Employee Level.");
        return false;
    }
    else {
    }
    if ($("#ed_emp_designation option:selected").index() < 1) {
        alert("Please Select Employee Designation.");
        return false;
    }
    if ($("#ed_rep_code").val() == "") {
        alert("Please Enter Reporting Employee Code.");
        return false;
    }
    if ($("#ed_rep_name").val() == "") {
        alert("Please Enter Reporting Employee Name.");
        return false;
    }
    var ecode = $("#ed_emp_code").val();
    var ename = $("#ed_emp_name").val();
    var elevel = $("#ed_emp_level").val();
    var edesg = $("#ed_emp_designation").val();
    var ercode = $("#ed_rep_code").val();
    $("#divUpd").show();
    Reporting_Manager.updateReportingData(ecode, ename, elevel, edesg, ercode, 4, OnUpdate);
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

function getReportingData(emp_code) {
    
    /*********************************************************************************/
    Reporting_Manager.getReporting(emp_code,1, OnSuccess1);

    /*********************************************************************************/

}

function OnSuccess1(response) {
    var res = response.value;
    //var data = res.split("||");

    $("#ed_emp_name").val(res[1]);
    $("#ed_emp_code").val(res[2]);
    $("#ed_rep_code").val(res[5]);
    $("#ed_rep_name").val(res[6]);
    $("#ed_emp_level option:contains(" + res[3] + ")").attr('selected', 'selected');
    $("#ed_emp_designation option:contains(" + res[4] + ")").attr('selected', 'selected');
}

function deleteReporting(emp_code) {
    $("#txtResult").val(emp_code);
}

function deleteData() {
    var emp_code = $("#txtResult").val();
    $("#divDel").show();
    Reporting_Manager.updateReportingData(emp_code, "", "", "", "", 6, OnDelete);
}

function OnDelete(response) {

    $("#divDel").hide();
    $("#DeletePanel").modal('hide');
    if (response.value == "true") {
        alert('Reporting Data Has Been Deleted...!');
        show_List();
        return true;
    }
    else {
        alert(response.value);
        return false;
    }
}

function clearControls()
{
    $("#emp_code").val("");
    $("#emp_name").val(""); 
    $("select#emp_level").prop('selectedIndex', 0);
    $("#rep_emp_name").val("");
    $("#rep_emp_code").val("");
    $("select#emp_designation").prop('selectedIndex', 0);
    $("#emp_code").focus();
    return true;
}

function getrepname()
{
    var ecode = $("#rep_emp_code").val();
    if (ecode == "") {
        $("#rep_emp_name").val("");
    }
    else {
        Reporting_Manager.getReporting(ecode,1, OnReporting);
    }
}

function OnReporting(response)
{
    var ret_data = response.value;
    if (ret_data[0] == 0) {
        alert("Invalid Reporting Employee Code.");
        $("#rep_emp_code").val("");
        $("#rep_emp_name").val("");
        $("#rep_emp_code").focus();
    }
    else {
        $("#rep_emp_name").val(ret_data[1]);
    }
}

function getrepname1() {
    var ecode = $("#ed_rep_code").val();
    if (ecode == "") {
        $("#ed_rep_code").val("");
    }
    else {
        Reporting_Manager.getReporting(ecode,1, OnReportingEdit);
    }
}

function OnReportingEdit(response) {
    var ret_data = response.value;
    if (ret_data[0] == 0) {
        alert("Invalid Reporting Employee Code.");
        $("#ed_rep_code").val("");
        $("#ed_rep_name").val("");
        $("#ed_rep_code").focus();
    }
    else {
        $("#ed_rep_name").val(ret_data[1]);
    }
}

function check_exists() {
    //alert(1);
    var ecode = $("#emp_code").val();
    if (ecode != "") {
        if (!isNaN($("#emp_code").val()) && parseInt($("#emp_code").val()) == 0) {
            alert("Please Enter Valid Employee Code.");
            $("#emp_code").val("");
            $("#emp_code").focus();
            return false;
        }
        else {
            Reporting_Manager.getInMaster(ecode, 5, OnExistsMaster);
        }
    }
}

function OnExistsMaster(response) {
    var ret_data = response.value;
    if (ret_data[0] == 0) {
        alert("Employee Does Not Exists In Master.");
        $("#emp_code").val("");
        $("#emp_code").focus();
        return false;
    }
    else {
        Reporting_Manager.getReporting($("#emp_code").val(), 1, OnExists);
    }
}

function OnExists(response) {
    var ret_data = response.value;
    if (ret_data[0] > 0) {
        alert("Employee Number Allready Exists.");
        $("#emp_code").val("");
        $("#emp_code").focus();
        return false;
    }
}

$(document).ready(function () {
    App.init();
    Demo.init();
    PageDemo.init();
});