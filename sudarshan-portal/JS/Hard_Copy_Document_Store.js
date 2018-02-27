g_serverpath = '/Sudarshan-Portal';


function getXML() {
    var tbldocrem = $("#uploadTable");
    var lastRowrem1 = $('#uploadTable tr').length;

    if (lastRowrem1 > 1) {
        for (var i = 0; i < lastRowrem1 - 1; i++) {

            var numberOfChecked = $('input:checkbox:checked').length;
            
            if ($("#chkk_" + (i + 1) + "").prop("checked") == true) {
                var remark1 = $("#txt_Remark" + (i + 1) + "").val();
                if (remark1 == "") {
                    alert("Please enter the remark...!");
                    return false;
                }
            }
            else if (numberOfChecked == "0")
            {
                alert("Please select atleast one document...!");
                return false;
            }
        }
    }

    var XMLFILE = '';
    var tbldoc = $("#uploadTable");
    var lastRow1 = $('#uploadTable tr').length;
    XMLFILE = "<ROWSET>";
    if (lastRow1 > 1) {
        for (var l = 0; l < lastRow1 - 1; l++) {
            var firstCol = $("#uploadTable tr")[l + 1].cells[1].innerText;
            var SecondCol = $("#uploadTable tr")[l + 1].cells[2].innerText;
            var remark = $("#txt_Remark" + (l + 1) + "").val();
         
            
                XMLFILE += "<ROW>";
                XMLFILE += "<OBJECT_VALUE>#</OBJECT_VALUE>";
                XMLFILE += "<OBJECT_TYPE>DISPATCH NOTE</OBJECT_TYPE>";
                XMLFILE += "<DOCUMENT_TYPE>" + firstCol + "</DOCUMENT_TYPE>";
                XMLFILE += "<FILENAME>" + SecondCol + "</FILENAME>";
                XMLFILE += "<Remark>" + remark + "</Remark>";
                XMLFILE += "<Dept_flag>STORE</Dept_flag>";
                XMLFILE += "</ROW>";
            
           
        }
        XMLFILE += "</ROWSET>";
        $("#txt_Document_Xml").val(XMLFILE);
    }
    return true;
}

function downloadfiles(index) {
    var UniqueNo = $("#txt_Unique_NO").val();
    window.open(g_serverpath + '/Common/FileDownload.aspx?indentno=NA&filename=' + $("#uploadTable tr")[index].cells[2].innerText + '&filetag=', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
}

function viewData() {
    try {
        var app_path = $("#app_Path").val();
        var po_val = $("#txt_PO_NO")[0].innerText;

        window.open(app_path + '/Common/ViewDocument.aspx?pono=' + po_val + '&type=PO', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
    }
    catch (exception) {

    }
}

function setSelectedNote() {
    var PK_ID = $("#txt_PKID")[0].defaultValue;
    try {
        Hard_Copy_Document_Approval_Store.GetCurrentTime(PK_ID, OnSuccess);
    }
    catch (exception) {

    }
}

function OnSuccess(response) {
    document.getElementById("div_header1").innerHTML = response.value;
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

function closePayterm() {
    $('#payterm').modal('hide');
}

function closeInco() {
    $('#incoterm').modal('hide');
}

