g_serverpath = '/Sudarshan-Portal';

function downloadfiles(index) {
    try {
        PODispatchDeatails_Approval.GetFileNames(index, OnSuccess1);
    }
    catch (exception) {

    }
}

function OnSuccess1(response) {
    window.open(g_serverpath + '/Common/FileDownload.aspx?indentno=NA&filename=' + response.value + '&filetag=', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
}


function viewData(index) {
    try {
        var app_path = $("#app_Path").val();
        var po_val = $("#encrypt_po_" + (index)).val();
        window.open(app_path + '/Common/ViewDocument.aspx?pono=' + po_val + '&type=PO', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
    }
    catch (exception) {

    }
}

function prepareData(){
    var text2 = $("#txtRemark").val();
    $("#txt_Remark").val(text2);
     var Approver_action = $("#ddl_Action option:selected").val();
    if (Approver_action == "") {
        alert("Please Select Action first...!");
        return false;
    }
    else if(Approver_action == "Reject" && text2 == ""){
	alert("Please fill rejection remark...!");
        return false;
    }
}

function getSchedule(mat) {

   var mat1 = $("#txt_Mat_no" + mat).val();
 var PO_NO = $("#txt_Po_Number").val();
   PODispatchDeatails_Approval.fillschedule(PO_NO,mat1, callback_Schedule);
}

function callback_Schedule(response) {
    $("#div_SchedulePO").html($("#txt_Po_Number").val());
    $("#div_Schedule").html(response.value);
}