g_serverpath = '/Sudarshan-Portal';
var details = "";
$(document).ready(function () {
    $("#div_main").hide();
    $("#div_DispatchDetails").hide();
    $("#div_DocumentAttach").hide();
    $("#div_Action").hide();
    $("#Mandatory_doc").hide();
    App.init();
    details = $("#checkValue").val();
   
});
function getheader() {
    details = $(".demodemo input:radio:checked").val();
    var vendor = $("#txt_Username").val();
    PODispatchDeatails.fillVendor(details, vendor, callback_Vendor);
}

function callback_Vendor(response) {
    var header = $("#div_Header").html(); 
    header = "";
    $("#txt_Invoice_No").val('');
    $("#txt_Invoice_Amount").val('');
    $("#txt_Delivery_Note").val('');

    $("#txt_transporter_Name").val('');
    $("#txt_Vehicle_No").val('');
    $("#txt_Contact_Person_Name").val('');
    $("#txt_Contact_No").val('');
    $("#txt_LR_NO").val('');
    $("#txt_LR_Date").val('');

    $("#uploadTable tbody").remove();

    $("#div_Header").html(response.value);
    $("#div_main").hide();
    $("#div_DispatchDetails").hide();
    $("#div_DocumentAttach").hide();
    $("#div_Action").hide();
    $("#Mandatory_doc").hide();
}



function getDetail() {
    //$("#txt_Invoice_Amount").prop('ReadOnly', true);
    var Item_Cat = $("#txt_Item_Category").val();
    if (Item_Cat == "9") {
        $("#div_DispatchDetails").hide();
        $("#div_main").show();
        $("#div_DocumentAttach").show();
        $("#div_Action").show();
	$("#Mandatory_doc").show();
    }
    else {
        $("#div_DispatchDetails").show();
        $("#div_main").show();
        $("#div_DocumentAttach").show();
        $("#div_Action").show();
	$("#Mandatory_doc").show();
    }
    PODispatchDeatails.fillDetail(details, callback_Vendor_Detail);
}

function callback_Vendor_Detail(response) {
        $("#div_detail").html(response.value);
}

function getIncoTerm() {

    PODispatchDeatails.fillInco(details, callback_Inco);
}

function callback_Inco(response) {
    $("#div_IcoTemSetPoNo").html(details);
    $("#div_Inco").html(response.value);
}

function getPaymentTerm() {
    PODispatchDeatails.fillPayment(details, callback_Payment);
}

function callback_Payment(response) {
    $("#div_PaymentTemSetPoNo").html(details);
    $("#div_Payment").html(response.value);
}

function getSchedule(mat) {
    var mat = $("#txt_Mat_no" + mat).val();
    PODispatchDeatails.fillschedule(details ,mat, callback_Schedule);

}

function callback_Schedule(response) {
    $("#div_SchedulePO").html(details);
    $("#div_Schedule").html(response.value);
  }

function isNumber(evt) {
    var iKeyCode = (evt.which) ? evt.which : evt.keyCode
    if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57)) {
        alert("Enter numeric value only.....!");

        return false;
    }
    return true;
}

function isNumberKey1(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode != 46 && charCode != 45 && charCode > 31
      && (charCode < 48 || charCode > 57))
    {
        alert("Enter numeric value only.....!");
        return false;
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
    else if (fileNameSplit[1] != "txt" && fileNameSplit[1] != "jpg" && fileNameSplit[1] != "jpeg" && fileNameSplit[1] != "pdf" && fileNameSplit[1] != "ods" && fileNameSplit[1] != "xlsx" && fileNameSplit[1] != "doc" && fileNameSplit[1] != "docx") {
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

function downloadfiles(index) {
    window.open(g_serverpath + '/Common/FileDownload.aspx?indentno=NA&filename=' + $("#uploadTable tr")[index].cells[1].innerText + '&filetag=', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
}

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

function isInvoice_Amt() {
    var lastRow = $('#tbl_Detail tr').length;
    var PO_GV = $("#txt_PO_GV").val();
    var Cumu_Amt = $("#txt_Cumulative_Amount").val();
    var Invoice_Amt = $("#txt_Invoice_Amount").val();

    var Tolerance_Amt = 0;

    for (var j = 0; j < lastRow - 1 ; j++) {
        var Tolerance_Amount = "";
        var Tolerance_Amount = $("#txt_Tolerance_Amount" + (j + 1) + "").val();
        Tolerance_Amt = parseFloat(Tolerance_Amt) + parseFloat(Tolerance_Amount);
    }

    var TTL_AMT = parseFloat(PO_GV) + parseFloat(Tolerance_Amt);
    var INV_COMU = parseFloat(Cumu_Amt) + parseFloat(Invoice_Amt);
    if (INV_COMU > TTL_AMT) {
        alert("Invoice amount is more than PO Gross Value...!");
        $("#txt_Invoice_Amount").val($("#Line_Item_Amount").val());
        $("#txt_Invoice_Amount").focus();
        return false;
    }
}

function isTolerance() {
 
    var tblDTL = $("#tbl_Detail");
    var lastRow = $('#tbl_Detail tr').length;
    var PO_GV1 = $("#txt_PO_GV").val();
    var Tolerance_Amt1 = 0;
    var Line_Item_Qty = 0;
    var TOL_Total_SUM = 0;
    var Dispatch_Qty_CAL = 0;
    var Per_ITEM= 0;

    
    var TTL_AMT1 = parseFloat(PO_GV1) + parseFloat(Tolerance_Amt1);
    var Net_Price_Invoice = 0;
    var Net_Price_Invoice1 = 0;
    var Total_Amount_Tolerance= 0;


    for (var j = 0; j < lastRow-1 ; j++) {

        var Dispatch_Qty = $("#txt_Dispatch_Qty" + (j + 1) + "").val();
	var NetPrice = $("#txt_AMOUNT" + (j + 1) + "").val();
	var TOL_Total = $("#txt_Tolerance_Total" + (j + 1) + "").val();
	var Tax = $("#txt_tax" + (j + 1) + "").val();
        if (Dispatch_Qty != "") {
            Dispatch_Qty_CAL = parseFloat(Dispatch_Qty_CAL) + parseFloat(Dispatch_Qty);
	    var Tolerance_Amount1 = "";
       	    var Tolerance_Amount1 = $("#txt_Tolerance_Amount" + (j + 1) + "").val();
            Tolerance_Amt1 = parseFloat(Tolerance_Amt1) + parseFloat(Tolerance_Amount1);
		
	    Total_Amount_Tolerance = (parseFloat(NetPrice) + parseFloat(Tax) + parseFloat(Tolerance_Amount1));
	    Per_ITEM = (parseFloat(Total_Amount_Tolerance) / parseFloat(TOL_Total));
	    Net_Price_Invoice = (parseFloat(Net_Price_Invoice) + parseFloat(Per_ITEM) * parseFloat(Dispatch_Qty)).toFixed(2);
	    Net_Price_Invoice1 = (parseFloat(Net_Price_Invoice) + parseFloat(Per_ITEM) * parseFloat(Dispatch_Qty)).toFixed(2);
        }
        var Dispatch_Qty_Sum = $("#txt_Cumulative_Dispatch" + (j + 1) + "").val();
        
        TOL_Total_SUM = parseFloat(TOL_Total_SUM) + parseFloat(TOL_Total);
        var TOL_Diff = $("#txt_Tolerance_Diff" + (j + 1) + "").val();
        var GR_Quantity = $("#txt_grn" + (j + 1) + "").val();
        var Quantity = $("#txt_Qunt" + (j + 1) + "").val();
       
        var Total = parseFloat(Dispatch_Qty) + parseFloat(Dispatch_Qty_Sum);

        if (Total > TOL_Total) {
            alert("Dispatch quantity is more than tolerance quantity...!");
            $("#txt_Dispatch_Qty" + (j + 1) + "").val('');
            $("#txt_Dispatch_Qty" + (j + 1) + "").focus();
            $("#txt_Invoice_Amount").val('');
            return false;
        }
    }
    var Per_Qty = parseFloat(TTL_AMT1) / parseFloat(TOL_Total_SUM);
    var Multi_Qty = parseFloat(Per_Qty) * parseFloat(Dispatch_Qty_CAL);
    Line_Item_Qty = (parseFloat(Line_Item_Qty) + parseFloat(Multi_Qty)).toFixed(2);
    $("#Line_Item_Amount").val(Net_Price_Invoice);
    $("#txt_Invoice_Amount").val(Net_Price_Invoice);
    return true;
}

function check_po() {
    var Invoice_no = $("#txt_Invoice_No").val();
    var Vendor_Code = $("#txt_Vendor_Code").val();
    PODispatchDeatails.fillInvoice(Invoice_no, Vendor_Code, callback_Invoice);
}

function callback_Invoice(response) {
    if (response.value == "true") {
        alert("Invoice number already generated...!");
        $("#txt_Invoice_No").val('');
        $("#txt_Invoice_No").focus();
        return false;
    }
}


function getXML() {
     
    $("#txt_Vendor").val($("#txt_Vendor_Code").val());
    $("#txt_PO").val($("#txt_PO_Number").val());
    $("#txt_Creation_Date").val($("#txt_Creation").val());
    $("#txt_VendorName").val($("#txt_Vendor_Name").val());	
    $("#txt_Plat_Email").val($("#txt_Plan").val());

    var name = $("#txt_transporter_Name").val();
    var vehicle = $("#txt_Vehicle_No").val();
    var person = $("#txt_Contact_Person_Name").val();
    var contact = $("#txt_Contact_No").val();
    var LR_NO = $("#txt_LR_NO").val();
    var LR_Date = $("#txt_LR_Date").val();
    var InvoiceNo = $("#txt_Invoice_No").val();
    var Invoicedate = $("#txt_Invoice_Date").val();
    var amount = $("#txt_Invoice_Amount").val();
    var Delivery_Note = $("#txt_Delivery_Note").val();
    var uptblcount = $('#uploadTable tr').length - 1;
    
    var headertbl = $("#tble");
    var hdrow = $('#tble tr').length;
    for (var m = 0; m < hdrow - 1; m++) {
        var PO_Typ = $("#txt_NO_SE_PO").val();
        var PO_NO = $("#txt_PO_Number").val();
        var Vendor_Code = $("#txt_Vendor_Code").val();
        if (Vendor_Code.length > 10) {
            alert('Vendor code length should not be greater than 10');
            return false;
        }
        else if (PO_NO.length > 10) {
            alert('PO number length should not be greater than 10');
            return false;
        }
    }

    var tblDTL1 = $("#tbl_Detail");
    var lastRow11 = $('#tbl_Detail tr').length;

    for (var j = 0; j < lastRow11-1 ; j++) {
        var Dispatch_Qty1 = $("#txt_Dispatch_Qty" + (j + 1) + "").val();
	if(Dispatch_Qty1 != "")
	{
        var Line_Item = $("#txt_line_Item_No" + (j + 1) + "").val();
        var Material = $("#txt_Material" + (j + 1) + "").val();
        var plant = $("#txt_plant" + (j + 1) + "").val();
        if (Line_Item.length > 5) {
            alert('Line item length should not be greater than 5');
            return false;
        }
        else if (Material.length > 18) {
            alert('Material number length should not be greater than 18');
            return false;
        }
        else if (plant.length > 4) {
            alert('Material number length should not be greater than 4');
            return false;
        }
        else if (Dispatch_Qty1.length > 13) {
            alert('Dispatch quantity length should not be greater than 13');
            $("#txt_Dispatch_Qty" + (j + 1) + "").focus();
            return false;
        }
	}
    }


    if (Vendor_Code == "") {
        alert("Vendor code is blank...!");
        return false;
    }
    else if (PO_NO == "") {
        alert("PO number is blank...!");
        return false;
    }
    else if (InvoiceNo == "") {
        alert("Please fill the invoice no...!");
        $("#txt_Invoice_No").focus();
        return false;
    }
    else if (InvoiceNo.length > 10) {
        alert("Invoice no length not more than 10...!");
        $("#txt_Invoice_No").focus();
        return false;
    }
    else if (Invoicedate == "") {
        alert("Please fill the invoice date...!");
        return false;
    }
    else if (amount == "") {
        alert("Please fill the amount...!");
        $("#txt_Invoice_Amount").focus();
        return false;
    }
    else if (Delivery_Note == "") {
        alert("Enter delivery note...!");
        $("#txt_Delivery_Note").focus();
        return false;
    }
    else if (Delivery_Note.length > 10) {
        alert("Delivery note length not more than 10...!");
        $("#txt_Delivery_Note").focus();
        return false;
    }
    //else if (Dispatch_Qty1 == "") {
    //   alert("Please fill the dispatch quantity...!");
   //    return false;
   // }
    else if (Material == "") {
        alert("Material number is blank...!");
        return false;
    }
    else if (plant == "") {
        alert("Plant location is blank...!");
        return false;
    }
    else if (Line_Item == "") {
        alert("Line item number is blank...!");
        return false;
    }
    else if (PO_Typ == "Material PO") {
        if (name == "") {
            alert("Please fill the transpoter name...!");
            return false;
        }
        if (name.length > 40) {
            alert("Transpoter name length not more than 40...!");
            $("#txt_transporter_Name").focus();
            return false;
        }
            else if (vehicle == "") {
                alert("Please fill the vehicle no...!");
                return false;
            }
        else if (vehicle.length > 12) {
            alert("Vehicle no length not more than 12...!");
            $("#txt_Vehicle_No").focus();
            return false;
        }
            else if (person == "") {
                alert("Please fill the person name...!");
                return false;
            }
        else if (person.length > 35) {
            alert("Contact person name length not more than 35...!");
            $("#txt_Contact_Person_Name").focus();
            return false;
        }
            else if (contact == "") {
                alert("Please fill the contact No...!");
                return false;
            }
        else if (contact.length > 16) {
            alert("Contact no length not more than 16...!");
            $("#txt_Contact_No").focus();
            return false;
        }
            else if (LR_NO == "") {
                alert("Please fill the LR number...!");
                return false;
            }
        else if (LR_NO.length > 20) {
            alert("LR number length not more than 20...!");
            $("#txt_LR_NO").focus();
            return false;
        }
            else if (LR_Date == "") {
                alert("Please fill the date...!");
                return false;
            }
        else if (uptblcount <= 2) {
            alert("Please Upload Mandatory Files");
            return false;
        }
        //else if (uptblcount == 4) {
        //    var firstRow = $("#uploadTable tr").find("td:nth-child(1)")[0].innerText;
        //    var secondRow = $("#uploadTable tr").find("td:nth-child(1)")[1].innerText;
        //    var thirdRow = $("#uploadTable tr").find("td:nth-child(1)")[2].innerText;
        //    var fourthRow = $("#uploadTable tr").find("td:nth-child(1)")[3].innerText;

        //    if (firstRow == "Delivery Challan" || firstRow == "Invoice" || firstRow == "LR" || firstRow == "Tax Invoice") {
        //        if (secondRow == "Delivery Challan" || secondRow == "Invoice" || secondRow == "LR" || secondRow == "Tax Invoice") {
        //            if (thirdRow == "Delivery Challan" || thirdRow == "Invoice" || thirdRow == "LR" || thirdRow == "Tax Invoice") {
        //                if (fourthRow == "Delivery Challan" || fourthRow == "Invoice" || fourthRow == "LR" || fourthRow == "Tax Invoice") {

        //                }
        //                else {
        //                        alert("Please Select Tax Invoice Document...!");
        //                        return false;
        //               }
                        
        //            }
        //            else {
        //                alert("Please Select LR Document...!");
        //                return false;
        //            }
        //        }
        //        else {
        //            alert("Please Select Invoice Document...!");
        //            return false;
        //        }
        //    }
        //    else {
        //        alert("Please Select Delivery Challan Document...!");
        //        return false;
        //    }

        //}
        //else if (uptblcount == 5) {
        //    var firstRow = $("#uploadTable tr").find("td:nth-child(1)")[0].innerText;
        //    var secondRow = $("#uploadTable tr").find("td:nth-child(1)")[1].innerText;
        //    var thirdRow = $("#uploadTable tr").find("td:nth-child(1)")[2].innerText;
        //    var fourthRow = $("#uploadTable tr").find("td:nth-child(1)")[3].innerText;
        //    var fifthRow = $("#uploadTable tr").find("td:nth-child(1)")[4].innerText;

        //    if (firstRow == "Delivery Challan" || firstRow == "Invoice" || firstRow == "LR" || firstRow == "Tax Invoice") {
        //        if (secondRow == "Delivery Challan" || secondRow == "Invoice" || secondRow == "LR" || secondRow == "Tax Invoice") {
        //            if (thirdRow == "Delivery Challan" || thirdRow == "Invoice" || thirdRow == "LR" || thirdRow == "Tax Invoice") {
        //                if (fourthRow == "Delivery Challan" || fourthRow == "Invoice" || fourthRow == "LR" || fourthRow == "Tax Invoice") {
        //                }
        //            else {
        //                if (fifthRow == "Delivery Challan" || fifthRow == "Invoice" || fifthRow == "LR" || fifthRow == "Tax Invoice") {
        //                }
        //                else {
        //                    alert("Please Select Tax Invoice Document...!");
        //                    return false;
        //                }
        //            }
        //            }
        //            else {
        //                alert("Please Select LR Document...!");
        //                return false;
        //            }
        //        }
        //        else {
        //            alert("Please Select Invoice Document...!");
        //            return false;
        //        }
        //    }
        //    else {
        //        alert("Please Select Delivery Challan Document...!");
        //        return false;
        //    }

        //}
    }
    else if (PO_Typ == "Service PO") {
        if (uptblcount <= 1) {
            alert("Please Upload  Mandatory Files");
            return false;
        }
    }
    var subm = confirm("Are you sure to submit this request.. ?");
    if (subm) {
    var XMLDATA = '';

    var tblDTL = $("#tbl_Detail");
    var lastRow1 =  $('#tbl_Detail tr').length;
    XMLDATA = '<ROWSET>';

    for (var j = 0; j < lastRow1 - 1 ; j++) {

        var Dispatch_Qty = $("#txt_Dispatch_Qty" + (j + 1) + "").val();
      	  if(Dispatch_Qty != ""){
           XMLDATA += "<ROW>";
           XMLDATA += "<FK_Dispatch_Note_HDR>#</FK_Dispatch_Note_HDR>";
           XMLDATA += "<Material_No>" + $("#txt_Material" + (j + 1) + "").val() + "</Material_No>";
           XMLDATA += "<Plant>" + $("#txt_plant" + (j + 1) + "").val() + "</Plant>";
           XMLDATA += "<Storage_Location>" + $("#txt_storage" + (j + 1) + "").val() + "</Storage_Location>";
           XMLDATA += "<Quantity>" + $("#txt_Qunt" + (j + 1) + "").val() + "</Quantity>";
           XMLDATA += "<UOM>" + $("#txt_UOM" + (j + 1) + "").val() + "</UOM>";
           XMLDATA += "<Net_Price>" + $("#txt_NET_Price" + (j + 1) + "").val() + "</Net_Price>";
           XMLDATA += "<Amount>" + $("#txt_AMOUNT" + (j + 1) + "").val() + "</Amount>";
         //XMLDATA += "<Material_Group>" + $("#txt_MAt_group" + (j + 1) + "").val() + "</Material_Group>";
           XMLDATA += "<Dispatch_Quantity>" + Dispatch_Qty + "</Dispatch_Quantity>";
           XMLDATA += "<GR_Quantity>" + $("#txt_grn" + (j + 1) + "").val() + "</GR_Quantity>";
           XMLDATA += "<PO_LINE_NUMBER>" + $("#txt_line_Item_No" + (j + 1) + "").val() + "</PO_LINE_NUMBER>";
	   XMLDATA += "</ROW>";
	}
	
    }

    var XMLRFC = '';
   
    for (var x = 0; x < lastRow1 - 1; x++) {

        var Dispatch_Qty = $("#txt_Dispatch_Qty" + (x + 1) + "").val();
        var PO = $("#txt_PO_Number").val();
        //if (Dispatch_Qty == "") {
        //    alert("Please Fill the Dispatch Quantity...!");
        //    return false;
        //}
	if(Dispatch_Qty != ""){
        XMLRFC += "" + PO + "$";
        XMLRFC += "" + $("#txt_line_Item_No" + (x + 1) + "").val() + "$";
        XMLRFC += "" + $("#txt_Material" + (x + 1) + "").val() + "$";
        XMLRFC += "" + $("#txt_plant" + (x + 1) + "").val() + "$";
        XMLRFC += "" + Dispatch_Qty + "|";
        }
    }

    $("#txt_RFC_Xml").val(XMLRFC);

     var XMLFILE = '';

    var lastRow1 = $('#uploadTable tr').length;
    XMLFILE = "<ROWSET>";
    if (lastRow1 > 1) {
        for (var l = 0; l < lastRow1 - 1; l++) {
            var firstCol = $("#uploadTable tr")[l+1].cells[0].innerText;
            var SecondCol = $("#uploadTable tr")[l+1].cells[1].innerText;
            if (firstCol == "Tax Invoice")
                {
                    XMLFILE += "<ROW>";
                    XMLFILE += "<OBJECT_TYPE>DISPATCH NOTE</OBJECT_TYPE>";
                    XMLFILE += "<OBJECT_VALUE>#</OBJECT_VALUE>";
                    XMLFILE += "<DOCUMENT_TYPE>" + firstCol + "</DOCUMENT_TYPE>";
                    XMLFILE += "<FILENAME>" + SecondCol + "</FILENAME>";
                    XMLFILE += "<Dept_flag>ACCOUNT</Dept_flag>";
                    XMLFILE += "</ROW>";
            }
            else {
                XMLFILE += "<ROW>";
                XMLFILE += "<OBJECT_TYPE>DISPATCH NOTE</OBJECT_TYPE>";
                XMLFILE += "<OBJECT_VALUE>#</OBJECT_VALUE>";
                XMLFILE += "<DOCUMENT_TYPE>" + firstCol + "</DOCUMENT_TYPE>";
                XMLFILE += "<FILENAME>" + SecondCol + "</FILENAME>";
                XMLFILE += "<Dept_flag>STORE</Dept_flag>";
                XMLFILE += "</ROW>";
            }
        }
        XMLFILE += "</ROWSET>";
        $("#txt_Document_Xml").val(XMLFILE);
    }

    XMLDATA += "</ROWSET>";
    $("#txt_XML_DTL").val(XMLDATA);
    return true;
   }
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

