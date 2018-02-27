g_serverpath = '/Sudarshan-Portal';

//function view_Progress() {
//    $('#DisableDiv').fadeTo('slow', .6);
//    $('#DisableDiv').append('<div style="background-color:#E6E6E6;position: absolute;top:0;left:0;width: 100%;height:300%;z-index:1001;-moz-opacity: 0.8;opacity:.80;filter: alpha(opacity=80);"><img src="../../images/loading_transparent2.gif" style="background-color:Aqua;position:fixed; top:40%; left:46%;"/></div>');
//    setTimeout(function () { }, 100);
//}

function chkData() {
    try{
        var tbl = document.getElementById("tbl_InvoiceDtl");
        if (tbl != null) {
            var lastRow = tbl.rows.length;
            if (lastRow > 1) {
                var chk = 0;
               
                var d = document.getElementsByName('early');
                for (var i = 0; i < d.length; i++) {
                    if (d[i].checked) {
                        var id = "Fk_dispatch_no" + (i + 1);
                        var disp = document.getElementById(id).value;
                        $("#txt_Dispatch").val(disp);
                        chk = 1;
                    }
                }
                if (chk < 1) {
                    //$('#DisableDiv').html("");
                    alert('Please Select Dispatch Request No...!!');
                    return false;
                }            
            }
          
        }
        else {
            //$('#DisableDiv').html("");
            alert('No Records Available...!!');
            return false;
        }
        //view_Progress();
        return true;
    }
    catch (exception)
    {
       // $('#DisableDiv').html("");
    }
}

function setSelectedNote(req_note) {
    try {
        Early_Payment_Request.GetCurrentTime(req_note, OnSuccess);
    }
    catch (exception) {

    }
}

function OnSuccess(response) {
    document.getElementById("div_header").innerHTML = response.value;
}

function downloadfiles(index) {
    try {
        Early_Payment_Request.GetFileNames(index, OnSuccess1);  
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
        var po_val = $("#encrypt_po" + (index)).val();
        var app_path = $("#app_Path").val();
        window.open(app_path + '/Common/ViewDocument.aspx?pono=' + po_val + '&type=PO', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no,directories=no');
        
    }
    catch (exception) {

    }
}

function prepareData()
{
    var text2 = $("#txtRemark").val();
    $("#txt_Remark").val(text2);
}


function gotopage(objButton, str) {
    try {
        //view_Progress();
        var pgno = objButton.value;
        var strData = $("#txt_Search").val();
        if (strData != undefined || strData != null) {
            Early_Payment_Request.fillGoToPage1(strData, pgno, str, callback_Inco);
        }
        else {
            Early_Payment_Request.fillGoToPage(pgno, str, callback_Inco);
        }
    }
    catch (exception) {
        
    }
}

function Show_Dtl()
{
    $("#div_dtl").show();
    $("#hidedtl").show();
    $("#showdtl").hide();
}

function Hide_Dtl() {
    $("#div_dtl").hide();
    $("#hidedtl").hide();
    $("#showdtl").show();
}

/*********************************************************************************************************/

function searchData() {
    var str = document.getElementById("txt_Search").value;
    var rpp = "0";
  
    if ($("#ddlRecords :selected").text() != undefined) {
        rpp = $("#ddlRecords :selected").text();
        $("#ddlText1").val($("#ddlRecords :selected").text());
    }
    else {
        rpp = $("#ddlText1").val();
    }
    //view_Progress();
    Early_Payment_Request.fillSearch(str, rpp, callback_Inco);
}

function callback_Inco(response) {
    document.getElementById("div_InvoiceDetails").innerHTML = response.value;
    //$('#DisableDiv').html("");
}

function closePayterm() {
    $('#payterm').modal('hide');
}

function closeInco() {
    $('#incoterm').modal('hide');
}


function viewData1() {
    try {
        var po_val = "";
        po_val = $("#encrypt1").val();
        var app_path = $("#app_Path").val();
        window.open(app_path + '/Common/ViewDocument.aspx?pono=' + po_val + '&type=PO', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no,directories=no');

    }
    catch (exception) {

    }
}