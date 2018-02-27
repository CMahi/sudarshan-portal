g_serverpath = '/Sudarshan-Portal';

function downloadfiles(index) {
    window.open(g_serverpath + '/Common/FileDownload.aspx?indentno=NA&filename=' + $("#uploadTable tr")[index].cells[1].innerText + '&filetag=', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
}



function getXML() {

    var uptblcount = $('#uploadTable tr').length - 1;

    if (uptblcount < 1) {
        alert("Please Upload Mandatory Files...!");
        return false;
    }

    var XMLFILE = '';
    var tbldoc = $("#uploadTable");
    var lastRow1 = $('#uploadTable tr').length;
    XMLFILE = "<ROWSET>";
    if (lastRow1 > 1) {
        for (var l = 0; l < lastRow1 - 1; l++) {
            var firstCol = $("#uploadTable tr")[l+1].cells[0].innerText;
            var SecondCol = $("#uploadTable tr")[l + 1].cells[1].innerText;
            var thirdCol = $("#uploadTable tr")[l + 1].cells[3].innerText;
            XMLFILE += "<ROW>";
            XMLFILE += "<OBJECT_VALUE>#</OBJECT_VALUE>";
            XMLFILE += "<OBJECT_TYPE>DISPATCH NOTE</OBJECT_TYPE>";
            XMLFILE += "<DOCUMENT_TYPE>" + firstCol + "</DOCUMENT_TYPE>";
            XMLFILE += "<FILENAME>" + SecondCol + "</FILENAME>";
            XMLFILE += "<Remark>" + thirdCol + "</Remark>";
            XMLFILE += "<Dept_flag>ACCOUNT</Dept_flag>";
            XMLFILE += "</ROW>";
           
        }
        XMLFILE += "</ROWSET>";
        $("#txt_Document_Xml").val(XMLFILE);
    }
    return true;
}

function uploadError(sender, args) {
    alert(args.get_errorMessage());
    var uploadText = document.getElementById('FileUpload1').getElementsByTagName("input");
    for (var i = 0; i < uploadText.length; i++) {
        if (uploadText[i].type == 'text') {
            uploadText[i].value = '';
        }
    }
}
function StartUpload(sender, args) {

}

function UploadComplete(sender, args) {
    var filename = args.get_fileName();
    var n = filename.replace(/[&\/\\#,+()$~%'":*?<>{} ]/g, "");
    var fileNameSplit = n.split('.');
    var contentType = args.get_contentType();
    var filelength = args.get_length();
    if (parseInt(filelength) == 0) {
        alert("Sorry cannot upload file ! File is Empty or file does not exist");
    }
    else if (parseInt(filelength) > 20000000) {
        alert("Sorry cannot upload file ! File Size Exceeded.");
    }
    else if (contentType == "application/octet-stream" && fileNameSplit[1] == "exe") {
        alert("Kindly Check File Type.");
        return false;
    }
    else if (fileNameSplit[1] != "pdf" && fileNameSplit[1] != "jpeg" && fileNameSplit[1] != "png" && fileNameSplit[1] != "jpg" && fileNameSplit[1] != "PDF" && fileNameSplit[1] != "JPEG" && fileNameSplit[1] != "PNG" && fileNameSplit[1] != "JPG") {
        alert("Kindly Check File Type....!");
        return false;
    }
    else {
        addToClientTable(n, args.get_length());

    }
    var fileInputElement = sender.get_inputFile();
    fileInputElement.value = "";
    var uploadText = document.getElementById('FileUpload1').getElementsByTagName("input");
    for (var i = 0; i < uploadText.length; i++) {
        if (uploadText[i].type == 'text') {
            uploadText[i].value = '';
        }
    }
    document.forms[0].target = "";
}

function addToClientTable(name, size) {

    var Document_Name = $("#ddl_Document").val();
    var tbl = $("#uploadTable");
    var lastRow = $('#uploadTable tr').length;
    if (Document_Name == "") {
        alert("Please select document type first...!");
        return false;
    }
    else if (lastRow > 1) {
        for (var k = 0; k < lastRow - 1; k++) {
            if (Document_Name == $('#uploadTable tr')[k + 1].cells[0].innerText) {
                alert("Document type already selected....!");
                return false;
            }
        }
    }

    var html1 = "<tr><td><input class='hidden' type='text' name='txt_Country_Add" + lastRow + "' id='txt_Document_Name" + lastRow + "' value=" + Document_Name + " readonly ></input>" + Document_Name + "</td>";
    var html2 = "<td><input class='hidden' type='text' name='txt_Region_Add" + lastRow + "' id='txt_Document_File" + lastRow + "' value=" + name + " readonly ></input><a id='a_downloadfiles" + lastRow + "' style='cursor: pointer' onclick=\"return downloadfiles('" + lastRow + "');\" >" + name + "</a></td>";
    var html3 = "<td><i id='del" + lastRow + "' class='glyphicon glyphicon-trash' align='center' onclick=\"return deletefile(" + (lastRow) + ");\" ></td></tr>";
    var htmlcontent = $(html1 + "" + html2 + "" + html3);
    $('#uploadTable').append(htmlcontent);

    $("#ddl_Document").val('');

}

//function downloadfiles(index) {
//    var UniqueNo = $("#txt_Unique_NO").val();
//    window.open(g_serverpath + '/Common/FileDownload.aspx?indentno=NA&filename=' + $("#uploadTable tr")[index].cells[1].innerText + '&filetag=', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
//}

function deletefile(RowIndex) {
    try {
        var lastRow = $('#uploadTable tr').length;
        var filename = $('#uploadTable tr')[RowIndex].cells[1].innerText;

        if (lastRow <= 1)
            return false;
        for (var contolIndex = RowIndex; contolIndex < lastRow - 1; contolIndex++) {
            $("#del" + (contolIndex + 1)).onclick = new Function("deletefile(" + contolIndex + ")");
            $("#del" + (contolIndex + 1)).id = "del" + contolIndex;
            $("#a_downloadfiles" + (contolIndex + 1)).onclick = new Function("downloadfiles(" + contolIndex + ")");
            $("#a_downloadfiles" + (contolIndex + 1)).id = "a_downloadfiles" + contolIndex;

            $("#txt_Document_Name" + (contolIndex + 1)).value = contolIndex;
            $("#txt_Document_Name" + (contolIndex + 1)).id = "txt_Document_Name" + contolIndex;

        }
        $('#uploadTable tr').eq(RowIndex).remove();
        deletephysicalfile(filename);
    }
    catch (Exc) { }
}
function deletephysicalfile(filename) {
    ContractualInstruction.DeleteFile(filename, callback_deletefile);
}
function callback_deletefile(response) {
    if (response.value != "") {
        alert("Document Removed Successfully..");
    }
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
        Hard_Copy_Document_Modification.GetCurrentTime(PK_ID, OnSuccess);
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
