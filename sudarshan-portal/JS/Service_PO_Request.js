g_serverpath = '/Sudarshan-Portal';
var details = "";
var rowflag = [];
var TOOTALBILAMT = 0.00;
$(document).ready(function () {
    $("#div_main").hide();
    $("#div_DispatchDetails").hide();
    $("#div_DocumentAttach").hide();
    $("#div_Action").hide();
    $("#Mandatory_doc").hide();
    App.init();
    details = $("#checkValue").val();
    $(".datepicker-dropdown").datepicker({ format: 'dd-M-yyyy', autoclose: true, todayBtn: 'linked' });


});
function getheader() {
    details = $(".demodemo input:radio:checked").val();
    rowflag = [];
    var vendor = $("#txt_Username").val();
    Service_PO_Request.fillVendor(details, vendor, callback_Vendor);
}

function callback_Vendor(response) {
    var header = $("#div_Header").html();
    header = "";
    $("#txt_invoice_no").val('');
    $("#txt_invoice_Date").val('');
    $("#txt_invoice_amount").val('');

    $("#txt_delivery_note").val('');
    $("#txt_Location").val('');
    $("#txt_From").val('');
    $("#txt_To").val('');
    $("#txt_Remark").val('');

    $("#uploadTable tbody").remove();

    $("#div_Header").html(response.value);
    $("#div_main").hide();
    $("#div_DocumentAttach").hide();
    $("#div_Action").hide();
    $("#Mandatory_doc").hide();
}


function getDetail() {
    
    var headertbl = $("#tble");
    var hdrow = $('#tble tr').length;
    $("#txt_invoice_amount").val(0.00);
    rowflag = [];
    if (hdrow == '1') {
        alert('PO Header Details Not Found.Please select another open PO');
        return false;
    }


    var Item_Cat = $("#txt_Item_Category").val();
    if (Item_Cat == "9") {
        $("#div_DispatchDetails").hide();
        $("#div_main").show();
        $("#div_DocumentAttach").hide();
        $("#div_Action").show();
        $("#Mandatory_doc").hide();
    }
    else {
        $("#div_DispatchDetails").show();
        $("#div_main").show();
        $("#div_DocumentAttach").hide();
        $("#div_Action").show();
        $("#Mandatory_doc").hide();
    }
    Service_PO_Request.fillDetail(details, callback_Vendor_Detail);
}

function callback_Vendor_Detail(response) {

    var PK_PRID = response.value.split('||');
    document.getElementById("txt_hdncount").value = PK_PRID[1];
    $("#div_detail").html(PK_PRID[0]);

    var tbl = $("#tbl_Detail tr").length;

    if (tbl == 1) {
        alert('PO Details Not Found');
        return false;
    }

    var hdr = $("#txt_hdr_cnt").val();

    $("#txt_hdncount").val(hdr);


}

function check_po() {
    var Invoice_no = $("#txt_invoice_no").val();
    var Vendor_Code = $("#txt_Vendor_Code").val();
    Service_PO_Request.fillInvoice(Invoice_no, Vendor_Code, callback_Invoice);
}

function callback_Invoice(response) {
    if (response.value == "true") {
        alert("Invoice number already generated...!");
        $("#txt_invoice_no").val('');
        $("#txt_invoice_no").focus();
        return false;
    }
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
      && (charCode < 48 || charCode > 57)) {
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
    var ext = filename.substr(filename.lastIndexOf('.') + 1);
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
   else if (ext != "pdf" && ext != "jpeg" && ext != "png" && ext != "jpg" && ext != "PDF" && ext != "JPEG" && ext != "PNG" && ext != "JPG") {
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
            if (name == $('#uploadTable tr')[k + 1].cells[1].innerText) {
                alert("Document already uploaded please rename the document ....!");
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
    var Invoice_Amt = $("#txt_invoice_amount").val();
    var TTL_AMT = parseFloat(PO_GV);
    var INV_COMU = parseFloat(Cumu_Amt) + parseFloat(Invoice_Amt);
    if (INV_COMU > TTL_AMT) {
        alert("Invoice amount is more than PO Gross Value...!");
        $("#txt_invoice_amount").val('');
        $("#txt_invoice_amount").focus();
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
    var Per_ITEM = 0;

    var TTL_AMT1 = parseFloat(PO_GV1) + parseFloat(Tolerance_Amt1);
    var Net_Price_Invoice = 0;
    var Net_Price_Invoice1 = 0;
    var Total_Amount_Tolerance = 0;

    for (var j = 0; j < lastRow - 1 ; j++) {
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
            alert("Dispatch quantity is more than total quantity...!");
            $("#txt_Dispatch_Qty" + (j + 1) + "").val('');
            $("#txt_Dispatch_Qty" + (j + 1) + "").focus();
            $("#txt_invoice_amount").val('');
            return false;
        }
    }
    var Per_Qty = parseFloat(TTL_AMT1) / parseFloat(TOL_Total_SUM);
    var Multi_Qty = parseFloat(Per_Qty) * parseFloat(Dispatch_Qty_CAL);
    Line_Item_Qty = (parseFloat(Line_Item_Qty) + parseFloat(Multi_Qty)).toFixed(2);
    $("#Line_Item_Amount").val(Net_Price_Invoice);
    $("#txt_invoice_amount").val(Net_Price_Invoice);
    return true;
}

function getXML() {
    var PO_Typ = $("#txt_PO_Type").val();
    var PO_NO = $("#txt_PO_Number").val();
    var Vendor_Code = $("#txt_Vendor_Code").val();
    var pocreatedby = $("#txt_PO_Created_By").val();
    var povalue = $("#txt_PO_Value").val();
    var pogv = $("#txt_PO_GV").val();
    var po_date = $("#txt_PO_Date").val();
    var popaymentterms = $("#txt_Payment_terms").val();
    var po_date = $("#txt_PO_Date").val();
    var remark = $("#txt_Remark").val();

    var invoiceno = $("#txt_invoice_no").val();
    var invoicedate = $("#txt_invoice_Date").val();
    var deliverynote = $("#txt_delivery_note").val();

    if (invoiceno == "") {
        alert("Please Enter Invoice No");
        return false;
    }
    if (invoicedate == "") {
        alert("Please Select Invoice Date");
        return false;
    }
    if (deliverynote == "") {
        alert("Please Enter Delivery Note");
        return false;
    }

    var lastRow1 = $('#uploadTable tr').length -1;
    if (lastRow1 < 2) {
        alert('Please Upload Mandatory Document');
        return false;
    }
    else if (lastRow1 > 0) {
        for (var k = 0; k < lastRow1; k++) {
            if ($('#uploadTable tr')[k + 1].cells[0].innerText == "Invoice") {
                var Inv = 1;
            }
            if ($('#uploadTable tr')[k + 1].cells[0].innerText == "Bill Summary") {
                var bill = 1;
            }
            if ($('#uploadTable tr')[k + 1].cells[0].innerText == "Attendance Sheet") {
                var att_sh = 1;
            }
            if ($('#uploadTable tr')[k + 1].cells[0].innerText == "Last Month PF/ESI Challan") {
                var Last_month = 1;
            }
        }
        if (Inv != "1") {
            alert("Please Upload Invoice...!");
            return false;
        }
        if (bill != "1") {
            alert("Please Upload Bill Summary...!");
            return false;
        }
       // PO_Typ = "ZFLB";
        if (PO_Typ == "ZFLB") {
            if (att_sh != "1") {
                alert("Please Upload Attendance Sheet...!");
                return false;
            }
            if (Last_month != "1") {
                alert("Please Upload Last Month PF/ ESI Challan...!");
                return false;
            }
        }
     }


    if (Vendor_Code.length > 10) {
        alert('Vendor code length should not be greater than 10');
        return false;
    }
    else if (PO_NO.length > 10) {
        alert('PO number length should not be greater than 10');
        return false;
    }

    if (PO_NO == "") {
        alert('PO No is Blank');
        return false;
    }
    else if (po_date == "") {
        alert('PO Date is Blank');
        return false;
    }
    else if (pocreatedby == "") {
        alert('Created By is Blank');
        return false;
    }
    else if (povalue == "") {
        alert('PO Value is Blank');
        return false;
    }
    else if (pogv == "") {
        alert('PO GV is Blank');
        return false;
    }

    $("#txt_serv_po_vendor_code").val(Vendor_Code);
    $("#txt_serv_po_po_no").val(PO_NO);
    $("#txt_serv_po_date").val(po_date);
    $("#txt_serv_po_created_by").val(pocreatedby);
    $("#txt_serv_po_type").val(PO_Typ);
    $("#txt_serv_po_value").val(povalue);
    $("#txt_serv_po_gv").val(pogv);
    $("#txt_serv_po_payterms").val(popaymentterms);
    $("#txt_serv_po_remark").val(remark);

    var xmlpodtl = "";
    xmlpodtl = "|ROWSET||";

    var tblpodtl = $("#tbl_Detail");
    var lastRow11 = $('#tbl_Detail tr').length;

    var hdrcnt = $("#txt_hdncount").val();
    if (lastRow11 == "1") {
        alert('PO Details Not Found');
        return false;
    }

    for (var x = 0; x < lastRow11 - 1 ; x++) {
        var box = ($("#chk" + (x + 1)).is(':checked'));
        if (box == true){
            var chkcount = "1"
        }
    }
    if (chkcount != "1") {
        alert('Please Select atleast one check box...!');
        return false;
    }

    var tblDTL1 = $("#tblservpodtl");
    var lastRow111= $('#tblservpodtl tr').length;
    if (lastRow111 == "0") {
        alert('Please Fill Up PO Service Item Details');
        return false;
    }

    var poservdispqty = 0;
    var lastRowser = $('#tblservpodtl tr').length;
    for (var p = 0; p < lastRow11 - 1 ; p++) {
        var box = ($("#chk" + (p + 1)).is(':checked'));
        if (box == true) {
            for (var m = 0; m < lastRowser - 1 ; m++) {
                var Disp = $("#txt_serv_po_itm_qty" + "_" + (p + 1) + "_" + (m + 1) + "").val();
                if (Disp == "" || Disp == "0") {
                    alert('Please Enter Quantity');
                    return false;
                }
            }
        }
    }


    var count = 0;
    var fkdtlid = 0;

    for (var j = 0; j < lastRow11 - 1 ; j++) {
        var box = ($("#chk" + (j + 1)).is(':checked'));
        if (box == true) {
            var hdr_id = $("#tbl_Detail_tr" + (j + 1) + "").val();
            var chkcount = "1";
            if (hdr_id != "" && hdr_id != undefined && hdr_id != "undefined") {

                var pono = $("#txt_po_no" + (j + 1) + "").val();
                var pomatno = $("#txt_Material" + (j + 1) + "").val();
                var poplant = $("#txt_plant" + (j + 1) + "").val();
                var poqty = $("#txt_Qunt" + (j + 1) + "").val();
                var pouom = $("#txt_UOM" + (j + 1) + "").val();
                var ponetprice = $("#txt_NET_Price" + (j + 1) + "").val();
                var matdescr = $("#txt_storage" + (j + 1) + "").val();
                var vendorcode = $("#txt_Username").val();

                //if (Dispatch_Qty1 != "") {
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

                if (pono == "") {
                    alert('PO NO is Blank');
                    return false;
                }
                else if (Line_Item == "") {
                    alert('Sr No. is Blank');
                    return false;
                }
                else if (matdescr == "") {
                    alert('Material Description is Blank');
                    return false;
                }
                else if (poqty == "") {
                    alert('Quantity is Blank');
                    return false;
                }

                xmlpodtl += "|ROW||";
                xmlpodtl += "|FK_SERV_PO_HDR_ID||#|/FK_SERV_PO_HDR_ID||";
                xmlpodtl += "|FK_VENDOR_CODE||" + vendorcode + "|/FK_VENDOR_CODE||";
                xmlpodtl += "|MATERIAL_NO||" + pomatno + "|/MATERIAL_NO||";
                xmlpodtl += "|PLANT||" + poplant + "|/PLANT||";
                xmlpodtl += "|QUANTITY||" + poqty + "|/QUANTITY||";
                xmlpodtl += "|UOM||" + pouom + "|/UOM||";
                xmlpodtl += "|NET_PRICE||" + ponetprice + "|/NET_PRICE||";
                xmlpodtl += "|FK_PO_NUMBER||" + pono + "|/FK_PO_NUMBER||";
                xmlpodtl += "|PO_LINE_NUMBER||" + Line_Item + "|/PO_LINE_NUMBER||";
                xmlpodtl += "|MATERIAL_DESC||" + matdescr + "|/MATERIAL_DESC||";
                xmlpodtl += "|FK_DTL_ID||" + fkdtlid++ + "|/FK_DTL_ID||";
                xmlpodtl += "|/ROW||";
                count++;
            }
        }
    }
    xmlpodtl += "|/ROWSET||";

    $("#txt_po_dtl_xml").val(xmlpodtl);

    var XMLDATA = '';

    var tblDTL = $("#tblservpodtl");
    var lastRow1 = $('#tblservpodtl tr').length;
   
    var TOOTALBILAMT1 = 0.00;
    var poservdispqty = 0;
    XMLDATA = "|ROWSET||";
    for (var k = 0; k < count ; k++) {
        for (var j = 0; j < lastRow1 - 1 ; j++) {
            var hdr_id_servitm = $("#txt_tr_serv_poitm" + "_" + (k + 1) + "_" + (j + 1) + "").val();
            if (hdr_id_servitm != "" && hdr_id_servitm != undefined && hdr_id_servitm != "undefined") {
                var poservno = $("#txt_po_serv_no" + "_" + (k + 1) + "_" + (j + 1) + "").val();
                var poshortxt = $("#txt_po_short_text" + "_" + (k + 1) + "_" + (j + 1) + "").val();
                var poservitmqty = $("#txt_serv_po_itm_qty" + "_" + (k + 1) + "_" + (j + 1) + "").val();
                var poservnetprc = $("#txt_po_net_price" + "_" + (k + 1) + "_" + (j + 1) + "").val();
                var poservgv = $("#txt_po_gv1" + "_" + (k + 1) + "_" + (j + 1) + "").val();
                var poservlinnoe = $("#txt_po_line_no" + "_" + (k + 1) + "_" + (j + 1) + "").val();
                var pono = $("#txt_po_no" + "_" + (k + 1) + "_" + (j + 1) + "").val();
                var pomenge2 = $("#txt_act_qty" + "_" + (k + 1) + "_" + (j + 1) + "").val();
                var poitmvalue = $("#txt_serv_po_item_value" + "_" + (k + 1) + "_" + (j + 1) + "").val();
                var per = $("#txt_po_total_price" + "_" + (k + 1) + "_" + (j + 1) + "").val();
                var poextrow = $("#txt_extrow" + "_" + (k + 1) + "_" + (j + 1) + "").val();
                var pomatnr = $("#txt_matnr" + "_" + (k + 1) + "_" + (j + 1) + "").val();
                var powerks = $("#txt_werks" + "_" + (k + 1) + "_" + (j + 1) + "").val();
                var pomenge = $("#txt_menge" + "_" + (k + 1) + "_" + (j + 1) + "").val();

                if (poservitmqty != "") {
                    TOOTALBILAMT1 = parseFloat(TOOTALBILAMT1) + parseFloat($("#txt_po_total_price" + "_" + (k + 1) + "_" + (j + 1) + "").val());
                }

                if (poshortxt == "" || poshortxt == " ") {
                    alert('Short Text is Blank');
                    return false;
                }

                if (poservitmqty != "") {
                    XMLDATA += "|ROW||";
                    XMLDATA += "|FK_SERV_PO_HDR_ID||#|/FK_SERV_PO_HDR_ID||";
                    XMLDATA += "|PO_NO||" + pono + "|/PO_NO||";
                    XMLDATA += "|PO_LINE_NO||" + poservlinnoe + "|/PO_LINE_NO||";
                    XMLDATA += "|SERVICE_NO||" + poservno + "|/SERVICE_NO||";
                    XMLDATA += "|SHORT_TEXT||" + poshortxt + "|/SHORT_TEXT||";
                    XMLDATA += "|QUANTITY||" + poservitmqty + "|/QUANTITY||";
                    XMLDATA += "|MENGE2||" + pomenge2 + "|/MENGE2||";
                    XMLDATA += "|NET_PRICE||" + poservnetprc + "|/NET_PRICE||";
                    XMLDATA += "|GROSS||" + poservgv + "|/GROSS||";
                    XMLDATA += "|VALUE||" + per + "|/VALUE||";
                    XMLDATA += "|FK_DTL_ID||" + k + "|/FK_DTL_ID||";
                    XMLDATA += "|MATNR||" + pomatnr + "|/MATNR||";
                    XMLDATA += "|WERKS||" + powerks + "|/WERKS||";
                    XMLDATA += "|EXTROW||" + poextrow + "|/EXTROW||";
                    XMLDATA += "|SERVPOS||" + poservno + "|/SERVPOS||";
                    XMLDATA += "|MENGE||" + pomenge + "|/MENGE||";
                    XMLDATA += "|/ROW||";
                    poservdispqty++;
                }
            }
        }
    }
    //if (poservdispqty == "" || poservdispqty == "0") {
    //    alert('Please Enter Quantity');
    //    return false;
    //}
    $("#txt_serv_po_item_total_value").val(TOOTALBILAMT1);

    XMLDATA += "|/ROWSET||";
    $("#txt_po_serv_dtl_xml").val(XMLDATA);

    var subm = confirm("Are you sure to submit this request.. ?");
    if (subm) {
        var XMLFILE = '';
        var lastRow1 = $('#uploadTable tr').length;
        XMLFILE = "<ROWSET>";
        if (lastRow1 > 1) {
            var taxinvoicecnt = 0;
            var delvrchlncnt = 0;
            var lrcnt = 0;
            for (var l = 0; l < lastRow1 - 1; l++) {
                var firstCol = $("#uploadTable tr")[l + 1].cells[0].innerText;
                var SecondCol = $("#uploadTable tr")[l + 1].cells[1].innerText;

                XMLFILE += "<ROW>";
                XMLFILE += "<OBJECT_TYPE>SERVICE PO</OBJECT_TYPE>";
                XMLFILE += "<OBJECT_VALUE>#</OBJECT_VALUE>";
                XMLFILE += "<DOCUMENT_TYPE>" + firstCol + "</DOCUMENT_TYPE>";
                XMLFILE += "<FILENAME>" + SecondCol + "</FILENAME>";
                XMLFILE += "</ROW>";
            }
            XMLFILE += "</ROWSET>";
            $("#txt_Document_Xml").val(XMLFILE);
        }
   
        XMLDATA += "</ROWSET>";
        $("#txt_XML_DTL").val(XMLDATA);
        var Inv_amount = $("#txt_invoice_amount").val();
        if (Inv_amount == "" || Inv_amount == "0") {
            alert("Please check invoice amount is 0 ...!");
            return false;
        }
        $("#divIns").show();

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

function imgChange(id, reqno) {
    var val = id;
    var box = ($("#chk" + id).is(':checked'));
    var div = document.getElementById(reqno + 'NewExpand' + id);
    if (box == true) {
        if (rowflag.indexOf(val) == -1) {
            rowflag.push(val);
            pttab = id;
            var div = document.getElementById(reqno + 'NewExpand' + id);
            var img = document.getElementById(reqno + 'NewimgExpand' + id);
            var div1 = document.getElementById(reqno + 'NewExpand1' + id);

            if (div.style.display == "none") {
                div.style.display = "";
                div1.style.display = "";
                var polineno = $("#txt_line_Item_No" + id).val();
                Service_PO_Request.getservicepodtl(reqno, id, polineno, callback_table);
            }
            else {
                div.style.display = "none";
                div1.style.display = "none";
            }
        }
        else {
            pttab = id;
            var div = document.getElementById(reqno + 'NewExpand' + id);
            var img = document.getElementById(reqno + 'NewimgExpand' + id);
            var div1 = document.getElementById(reqno + 'NewExpand1' + id);
            if (div.style.display == "none") {
                div.style.display = "";
                div1.style.display = "";
            }
            else {
                div.style.display = "none";
                div1.style.display = "none";

            }
        }
    }
    else {
        alert("Please select checkbox first...!");
        return false;
    }
}

function callback_table(response) {
    var PK_PRID = response.value.split('||');
    document.getElementById(PK_PRID[1] + 'NewExpand' + pttab).innerHTML = PK_PRID[0];
}

function allowOnlyNumbers(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode != 46 && charCode != 45 && charCode > 31
      && (charCode < 48 || charCode > 57)) {
        alert("Enter numeric value only.....!");
        return false;
    }
    else if (evt.keyCode === 13) {
        evt.preventDefault();
        return false;
    }
    return true;
}


function allowOnlyNumbersdot(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        alert("Enter numeric value only.....!");
        return false;
    }
    else if (evt.keyCode === 13) {
        evt.preventDefault();
        return false;
    }
    return true;
}

function getupdatevalue(id, index, avlqty) {
    var Net_Price_Invoice = 0.00;
    var servpoitmqty = $("#txt_serv_po_itm_qty" + "_" + id + "_" + index + "").val();
    var serpodispqty = $("#txt_DISPATCH_qty" + "_" + id + "_" + index + "").val();
    var totqty = parseFloat(servpoitmqty) + parseFloat(serpodispqty);

    if (totqty > avlqty) {
        alert('Entered Quantity is Exceeded');
        $("#txt_serv_po_itm_qty" + "_" + id + "_" + index + "").val('');
        $("#txt_po_total_price" + "_" + id + "_" + index + "").val('');
        return false;
    }
    else {
        var value = $("#txt_serv_po_item_value" + "_" + id + "_" + index + "").val();
        var total = (servpoitmqty * value) / avlqty;
        $("#txt_po_total_price" + "_" + id + "_" + index + "").val(total);
        var tbldtl = $("#tbl_Detail tr").length;
        var tbl = $("#tblservpodtl tr").length;

        for (var j = 1; j < tbldtl ; j++) {
            var box = ($("#chk" + (j)).is(':checked'));
            if (box == true) {
                for (var i = 1; i < tbl ; i++) {
                    if ($("#txt_serv_po_itm_qty" + "_" + j + "_" + i + "").val() != "" && $("#txt_serv_po_itm_qty" + "_" + j + "_" + i + "").val() != undefined) {
                        Net_Price_Invoice = parseFloat(Net_Price_Invoice) + parseFloat($("#txt_po_total_price" + "_" + j + "_" + i + "").val());
                    }
                }
            }
        }
        $("#txt_invoice_amount").val(Net_Price_Invoice);
    }
}

function callback_chkqty(response) {
    var PK_PRID = response.value.split('||');

    var servpoitmqty = $("#txt_serv_po_itm_qty" + "_" + PK_PRID[2] + "_" + PK_PRID[3] + "").val();
    var servqty = $("#txt_act_qty" + "_" + PK_PRID[2] + "_" + PK_PRID[3] + "").val();

    var avlqty = servqty - PK_PRID[0];
    if (servpoitmqty > avlqty) {
        alert('Entered Quantity is Exceeded');
        $("#txt_serv_po_itm_qty" + "_" + PK_PRID[2] + "_" + PK_PRID[3] + "").val('');
        return false;
    }
    else {
        var value = $("#txt_serv_po_item_value" + "_" + PK_PRID[2] + "_" + PK_PRID[3] + "").val();
        var total = servpoitmqty * value;
        $("#txt_po_total_price" + "_" + PK_PRID[2] + "_" + PK_PRID[3] + "").val(total);
        var tbl = $("#tblservpodtl tr").length;
        TOOTALBILAMT = parseFloat(TOOTALBILAMT) + parseFloat($("#txt_po_total_price" + "_" + PK_PRID[2] + "_" + PK_PRID[3] + "").val());
        $("#txt_invoice_amount").val(TOOTALBILAMT);
    }
}

function changeAmount(chk_id) {
    var box = ($("#chk" + (chk_id + 1)).is(':checked'));
    var Net_Price_Invoice = 0.00;
    var tbldtl = $("#tbl_Detail tr").length;
    var tbl = $("#tblservpodtl tr").length;

    for (var j = 1; j < tbldtl ; j++) {
        var box = ($("#chk" + (j)).is(':checked'));
        if (box == true) {
            for (var i = 1; i < tbl ; i++) {
                var servpoitmqty = $("#txt_serv_po_itm_qty" + "_" + j + "_" + i + "").val();
                var serpodispqty = $("#txt_DISPATCH_qty" + "_" + j + "_" + i + "").val();
                var value = $("#txt_serv_po_item_value" + "_" + j + "_" + i + "").val();
                var avlqty = $("#txt_act_qty" + "_" + j + "_" + i + "").val();

                var total = (servpoitmqty * value) / avlqty;
                $("#txt_po_total_price" + "_" + j + "_" + i + "").val(total);

                if ($("#txt_serv_po_itm_qty" + "_" + j + "_" + i + "").val() != "" && $("#txt_serv_po_itm_qty" + "_" + j + "_" + i + "").val() != undefined) {
                    Net_Price_Invoice = parseFloat(Net_Price_Invoice) + parseFloat($("#txt_po_total_price" + "_" + j + "_" + i + "").val());
                }
            }
        }
    }
    $("#txt_invoice_amount").val(Net_Price_Invoice);
}

function enter(evt){
	if (evt.keyCode === 13) {
        evt.preventDefault();
        return false;
    }
}