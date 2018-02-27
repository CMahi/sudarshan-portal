g_serverpath = '/Sudarshan-Portal';
function downloadfiles(index) {
    try {
        Early_Payment_Request_Approval.GetFileNames(index, OnSuccess1);
    }
    catch (exception) {

    }
}

function OnSuccess1(response) {
    var app_path = $("#app_Path").val();
    window.open(app_path + '/Common/FileDownload.aspx?indentno=NA&filename=' + response.value + '&filetag=', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
}


function viewData(index) {
    try {
        var app_path = $("#app_Path").val();
        var po_val = $("#encrypt_po" + (index)).val();
        window.open(app_path + '/Common/ViewDocument.aspx?pono=' + po_val + '&type=PO', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
    }
    catch (exception) {

    }
}

function Show_Dtl() {
    $("#div_dtl").show();
    $("#hidedtl").show();
    $("#showdtl").hide();
}

function Hide_Dtl() {
    $("#div_dtl").hide();
    $("#hidedtl").hide();
    $("#showdtl").show();
}

function prepareData() {
   // view_Progress();
    var text2 = $("#txtRemark").val();
    $("#txt_Remark").val(text2);
}

//function view_Progress() {
//    $('#DisableDiv').fadeTo('slow', .6);
//    $('#DisableDiv').append('<div style="background-color:#E6E6E6;position: absolute;top:0;left:0;width: 100%;height:300%;z-index:1001;-moz-opacity: 0.8;opacity:.80;filter: alpha(opacity=80);"><img src="../../images/loading_transparent2.gif" style="background-color:Aqua;position:fixed; top:40%; left:46%;"/></div>');
//    setTimeout(function () { }, 100);
//}

