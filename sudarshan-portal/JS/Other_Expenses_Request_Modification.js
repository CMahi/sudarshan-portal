$(document).ready(function () {
    Load_DatePicker();
    allowOnlyNumbers();
});

function Load_DatePicker() {
    $(".datepicker-rtl").datepicker({
        format: 'dd-M-yyyy', autoclose: true, todaybtn: 'linked', endDate: new Date()
    });
}

function allowOnlyNumbers() {
    //jQuery('.numbersOnly').keyup(function () {
    //    this.value = this.value.replace(/[^0-9\.]/g, '');
    //});
    $('.numbersOnly').keydown(function (e) {
        if (e.shiftKey || e.ctrlKey || e.altKey) {
            e.preventDefault();
        } else {
            var key = e.keyCode;
            if (!((key == 8) || (key == 46) || (key >= 35 && key <= 40) || (key >= 48 && key <= 57) || (key >= 96 && key <= 105))) {
                e.preventDefault();
            }
        }
    });
}

function addnewrow() {

    var adv = $("[name='travel']");
    var chk = 0;
    for (var a = 0; a < adv.length; a++) {
        if ($("#radio" + (a + 1)).is(':checked')) {
            chk = chk + 1;
        }
    }
    if (chk < 1) {
        alert("Advance Not Taken. Not Allowed To Take Multiple Expenses");
        return false;
    }
    var lastRow = $('#tbl_Data tr').length;
    var lastRow1 = $('#tbl_Data tr').length - 1;

    for (var q = 0; q < lastRow - 1; q++) {

        var edate = $("#date" + (q + 1)).val();
        var billno = $("#bill_no" + (q + 1)).val();
        var billdate = $("#bill_date" + (q + 1)).val();
        var remrk = $("#remark" + (q + 1)).val();
        var amount = $("#amount" + (q + 1)).val();

        if ($("#ddlExp_Head" + (q + 1) + " option:selected").index() < 1) {
            alert('Please Select Expense Head At Row :' + (q + 1) + '');
            return false;
        }
        if (edate == "") {
            alert('Please Select Date At Row :' + (q + 1) + '');
            return false;
        }
        if (billno == "") {
            alert('Please Enter Bill No At Row :' + (q + 1) + '');
            return false;
        }
        if (billdate == "") {
            alert('Please Select Bill Date At Row :' + (q + 1) + '');
            return false;
        }
        if (remrk == "") {
            alert('Please Enter Remark At Row :' + (q + 1) + '');
            return false;
        }
        if (amount == "") {
            alert('Please Enter amount At Row :' + (q + 1) + '');
            return false;
        }
        else if (amount < 1) {
            alert('Amount Should Not Be Less Than One At Row :' + (q + 1) + '');
            $("#amount" + (q + 1)).val(0);
            return false;
        }
       /* if ($("#supp_doc" + (q + 1) + " option:selected").index() < 1) {
            alert('Please Select Supporting Attachment At Row :' + (q + 1) + '');
            return false;
        }
        if ($("#supp_doc" + (q + 1) + " option:selected").val() == "Y" && $("#supp_rem" + (q + 1)).val() == "") {
            alert('Please Enter Supporting Particulars At Row :' + (q + 1) + '');
            return false;
        }*/
        $("#add" + (q + 1)).hide();
        $("#rem" + (q + 1)).show();
    }
    //check_Controls();
    var data = Other_Expenses_Request_Modification.dropDownData();

    var html = "<tr><td><span id='index" + lastRow + "'>" + lastRow + "</span></td>";
    html += "<td><select ID='ddlExp_Head" + lastRow + "' runat='server' class='form-control input-sm'>" + data.value + "</select></td>";
    html += "<td><input class='form-control input-sm datepicker-rtl' type='text' id='date" + lastRow + "' readonly></input></td>";
    html += "<td><input class='form-control input-sm' type='text' id='bill_no" + lastRow + "'></input></td>";
    html += "<td><input class='form-control input-sm datepicker-rtl ' type='text' id='bill_date" + lastRow + "' readonly ></input></td>";
    html += "<td><input class='form-control input-sm' type='text' id='remark" + lastRow + "'></input></td>";
    html += "<td><input class='form-control input-sm numbersOnly' type='text' value='0' style='text-align:right; padding-right:10px' id='amount" + lastRow + "' onkeyup='calculate_Total()'></input></td>";
    html += "<td id='add" + lastRow + "'><a id='add_row" + lastRow + "' onclick='addnewrow()'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i></a></td>";
    html += "<td id='rem" + lastRow + "' style='display:none'><a id='del_row" + lastRow + "' onclick='delete_row(" + lastRow + ")'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-trash'></i></a></td></tr>";


    var htmlcontent = $(html);
    $('#tbl_Data').append(htmlcontent);
    Load_DatePicker();
    allowOnlyNumbers();
}

function delete_row(RowIndex) {
    var tbl = document.getElementById("tbl_Data");
    var lastRow = tbl.rows.length;
    if (lastRow <= 2) {
        alert("Validation Error:You have to Enter atleast one record..!");
        return false;
    }
    for (var contolIndex = RowIndex; contolIndex < lastRow - 1; contolIndex++) {
        if (contolIndex == RowIndex) {
            tbl.deleteRow(RowIndex);
        }

        document.getElementById("index" + (parseInt(contolIndex) + 1)).id = "index" + contolIndex;
        $("#index" + (parseInt(contolIndex))).html(contolIndex);
        document.getElementById("ddlExp_Head" + (parseInt(contolIndex) + 1)).id = "ddlExp_Head" + contolIndex;
        document.getElementById("date" + (parseInt(contolIndex) + 1)).id = "date" + contolIndex;
        document.getElementById("bill_no" + (parseInt(contolIndex) + 1)).id = "bill_no" + contolIndex;
        document.getElementById("bill_date" + (parseInt(contolIndex) + 1)).id = "bill_date" + contolIndex;
        document.getElementById("remark" + (parseInt(contolIndex) + 1)).id = "remark" + contolIndex;
        document.getElementById("amount" + (parseInt(contolIndex) + 1)).id = "amount" + contolIndex;
        /*
        document.getElementById("supp_doc" + (parseInt(contolIndex) + 1)).onchange = new Function("enable_disable_field(" + contolIndex + ")");
        document.getElementById("supp_doc" + (parseInt(contolIndex) + 1)).id = "supp_doc" + contolIndex;
        document.getElementById("supp_rem" + (parseInt(contolIndex) + 1)).id = "supp_rem" + contolIndex;
        */
        document.getElementById("add_row" + (parseInt(contolIndex) + 1)).onclick = new Function("addnewrow()");
        document.getElementById("add_row" + (parseInt(contolIndex) + 1)).id = "add_row" + contolIndex;
        document.getElementById("del_row" + (parseInt(contolIndex) + 1)).onclick = new Function("delete_row(" + contolIndex + ")");
        document.getElementById("del_row" + (parseInt(contolIndex) + 1)).id = "del_row" + contolIndex;
        document.getElementById("add" + (parseInt(contolIndex) + 1)).id = "add" + contolIndex;
        document.getElementById("rem" + (parseInt(contolIndex) + 1)).id = "rem" + contolIndex;
        calculate_Total();
    }
}

function enable_disable() {
    $("#ddlAdv_Location").prop('selectedIndex', 0);
    if ($("#ddl_Payment_Mode option:selected").text().toUpperCase() == "CASH") {
        $("#ddlAdv_Location").removeAttr('disabled');
        $("#pl").show();
        $("#pld").show();
    }
    else {
        $("#pl").hide();
        $("#pld").hide();
        $("#ddlAdv_Location").attr('disabled', 'disabled');
    }
}

function PrepareData() {
    try {
        //if ($("#span_bank_no").html() == "NA" || $("#span_bank_no").html() == "") {
        //    alert("Current Bank Account Is Not Available! Please Open Bank Account And Update Your Profile...!");
        //    return false;
        //}
        if ($("#ddl_Payment_Mode option:selected").index() == 0) {
            alert("Please Select Payment Mode...!");
            return false;
        }
        else if ($("#ddl_Payment_Mode option:selected").text().toUpperCase() == "CASH" && $("#ddlAdv_Location option:selected").index() == 0) {
            alert("Please Select Cash Location...!");
            return false;
        }
        if ($("#req_remark").val() == "") {
            alert("Please Enter Remark...!");
            return false;
        }
        if (check_Controls() == true) {

            if ($("#spn_Total").html() == 0 || $("#spn_Total").html() == "" || $("#spn_Total").html() == undefined) {
                alert("Total Amount Should Be Greater Than Zero...!");
                return false;
            }

            var radio = document.getElementsByName("travel");
            for (var rad = 1; rad <= radio.length; rad++) {
                if (radio[rad - 1].checked) {
                    var id = $("#PK_ADVANCE_ID" + rad).val();
                    var advance_amount = $("#advance_amount" + rad).val();
                    $("#txt_advance_id").val(id);
                    $("#txt_advance_amount").val(advance_amount);
                }
            }

            if ((parseFloat($("#txt_advance_amount").val()) > parseFloat($("#spn_Total").html())) && $("#ddl_Payment_Mode option:selected").text().toUpperCase() == "BANK") {
                alert("Please Change Payment Mode and Select Payment Location...!");
                return false;
            }

            calculate_Total();
            if ($("#dev_total").val() == "1") {
                if (confirm("Request Submitted Under Deviation.Confirm?")) {

                }
                else {
                    return false;
                }
            }

            var lastRow = $('#tbl_Data tr').length - 1;
            var fk_id = $("#txt_pk_id").val();
            var xmldata = "<ROWSET>";
            for (var q = 0; q < lastRow; q++) {

                var edate = $("#date" + (q + 1)).val();
                var billno = $("#bill_no" + (q + 1)).val();
                var billdate = $("#bill_date" + (q + 1)).val();
                var remrk = $("#remark" + (q + 1)).val();
                var amount = $("#amount" + (q + 1)).val();
                var exp_head = $("#ddlExp_Head" + (q + 1)).val();
                var supp_doc = $("#supp_doc" + (q + 1)).val();
                var supp_rem = $("#supp_rem" + (q + 1)).val();

                xmldata += "<ROW>";
                xmldata += "<fk_other_expense_Hdr_Id>"+fk_id+"</fk_other_expense_Hdr_Id>";
                xmldata += "<date>" + edate + "</date>";
                xmldata += "<billno>" + billno + "</billno>";
                xmldata += "<bill_date>" + billdate + "</bill_date>";
                xmldata += "<remark>" + remrk + "</remark>";
                xmldata += "<amount>" + amount + "</amount>";
                xmldata += "<pk_exp_head_id>" + exp_head + "</pk_exp_head_id>";
               /* xmldata += "<supp_doc>" + supp_doc + "</supp_doc>";
                xmldata += "<supp_particulars>" + supp_rem + "</supp_particulars>";*/
                xmldata += "</ROW>";
            }
            xmldata += "</ROWSET>";

            var lastRow = $('#uploadTable tr').length - 1;
            var voucher_id = $("#spn_req_no").html();
            var xmlFile = "<ROWSET>";
            for (var q = 0; q < lastRow; q++) {

                var descr = $("#descr" + (q + 1)).val();
                var fname = $("#txt_Document_File" + (q + 1)).val();

                xmlFile += "<ROW>";
                xmlFile += "<OBJECT_TYPE>OTHER EXPENSES</OBJECT_TYPE>";
                xmlFile += "<OBJECT_VALUE>" + voucher_id + "</OBJECT_VALUE>";
                xmlFile += "<FILENAME>" + fname + "</FILENAME>";
                xmlFile += "<DOCUMENT_TYPE>" + descr + "</DOCUMENT_TYPE>";
                xmlFile += "</ROW>";
            }
            xmlFile += "</ROWSET>";
            $("#txt_xml_data").val(xmldata);
            $("#txt_Document_Xml").val(xmlFile);
$("#divIns").show();
            return true;
            //alert(xmldata);
        }
        else { $("#divIns").hide(); return false; }
    }
    catch (exception) {
$("#divIns").hide();
        xmldata = "<ROWSET></ROWSET>";
        $("#txt_xml_data").val(xmldata);
        $("#txt_Document_Xml").val(xmldata);
        alert("Validation Error : " + exception);
        return false;
    }
}

function check_Controls() {
    var lastRow = $('#tbl_Data tr').length;
    for (var q = 0; q < lastRow - 1; q++) {

        var edate = $("#date" + (q + 1)).val();
        var billno = $("#bill_no" + (q + 1)).val();
        var billdate = $("#bill_date" + (q + 1)).val();
        var remrk = $("#remark" + (q + 1)).val();
        var amount = $("#amount" + (q + 1)).val();

        if (edate == "") {
            alert('Please Select Date At Row :' + (q + 1) + '');
            return false;
        }
        if (billno == "") {
            alert('Please Enter Bill No At Row :' + (q + 1) + '');
            return false;
        }
        if (billdate == "") {
            alert('Please Select Bill Date At Row :' + (q + 1) + '');
            return false;
        }
        if (remrk == "") {
            alert('Please Enter Remark At Row :' + (q + 1) + '');
            return false;
        }
        if (amount == "") {
            alert('Please Enter amount At Row :' + (q + 1) + '');
            return false;
        }
        else if (amount < 1) {
            alert('Amount Should Not Be Less Than One At Row :' + (q + 1) + '');
            $("#amount" + (q + 1)).val(0);
            return false;
        }
    }
    return true;
}

function calculate_Total() {
    try {
        $("#dev_total").val(0);
        var total = 0;
        var lastRow = $('#tbl_Data tr').length;
        for (var q = 0; q < lastRow - 1; q++) {
            var amount = 0;
            if ($("#amount" + (q + 1)).val() != "" && $("#amount" + (q + 1)).val() != undefined) {
                amount = parseFloat($("#amount" + (q + 1)).val());
                $("#amount" + (q + 1)).val(amount);
            }
            else {
                $("#amount" + (q + 1)).val(0);
            }
            total = total + amount;
        }
        $("#spn_Total").html(total);
        if (parseInt(total) > 10000) {
            $("#dev_total").val(1);
        }
    }
    catch (ex)
    { }
}


/*===============================================================================================================================*/

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
    var fileExt = '.' + filename.split('.').pop();
    var filelength = args.get_length();
    if (parseFloat(filelength) == 0) {
        alert("Sorry Cannot Upload File ! File Is Empty Or File Does Not Exist");
    }
    else if (parseFloat(filelength) > 10000000) {
        alert("Sorry Cannot Upload File ! File Size Exceeded.");
    }
    else if (contentType == "application/octet-stream" && fileNameSplit[1] == "exe") {
        alert("Kindly Check File Type.");
    }
    else {
        if (fileExt.toUpperCase() == ".JPEG" || fileExt.toUpperCase() == ".JPG" || fileExt.toUpperCase() == ".PNG" || fileExt.toUpperCase() == ".PDF") {
            addToClientTable(n, args.get_length());
        }
        else {
            alert("Unsupported File");
        }
        $("#txt_desc").val("");
        var fileInputElement = sender.get_inputFile();
        fileInputElement.value = "";

    }

    //  document.forms[0].target = "";
}

function addToClientTable(name, size) {

    var Document_Name = "";
    if ($("#txt_desc").val() == "") {
        alert("Enter Description");
        return false;
    }
    var tbl = $("#uploadTable");
    var lastRow = $('#uploadTable tr').length;

    var html2 = "<tr><td><input type='text' id='descr" + lastRow + "' value='" + $("#txt_desc").val() + "' style='display:none'/>" + $("#txt_desc").val() + "</td><td><input class='hidden' type='text' name='txt_Region_Add" + lastRow + "' id='txt_Document_File" + lastRow + "' value=" + name + " readonly ></input><a id='a_downloadfiles" + lastRow + "' style='cursor: pointer' onclick=\"return downloadfiles('" + name + "');\" >" + name + "</a></td>";
    var html3 = "<td><i id='del" + lastRow + "' class='glyphicon glyphicon-trash' align='center' onclick=\"return deletefile(" + (lastRow) + ");\" ></td></tr>";
    var htmlcontent = $(html2 + "" + html3);
    $('#uploadTable').append(htmlcontent);
}

function downloadfiles(index) {
    var app_path = $("#app_Path").val();
    var req_no = $("#spn_req_no").text();
    window.open(app_path + '/Common/FileDownload.aspx?indentno=' + req_no + '&filename=' + index + '&filetag=', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
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


function changeGL(index) {
    var pk_id = $("#ddlExp_Head" + index).val();
    if (pk_id == 0) {
        $("#spn_GL" + index).html("---");
    }
    else {
        var glcode = Other_Expenses_Request_Modification.getGLCode(pk_id);
        $("#spn_GL" + index).html(glcode.value);
    }
}

function enable_disable_field(index) {
    $("#supp_rem" + index).val("");
    if ($("#supp_doc" + index).val() == "Y") {
        $("#supp_rem" + index).show();
    }
    else {
        $("#supp_rem" + index).hide();
    }
}