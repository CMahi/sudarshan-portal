g_serverpath = '/Sudarshan-Portal';
var details = "";

function createxml() {
    try {
                var xmlvehicle = "";
                var tblLocal = document.getElementsByName("tbl_Local");
                var lastRow = $('#tbl_Local tr').length - 1;
                xmlvehicle = "|ROWSET||";
                for (var q = 0; q < lastRow; q++) {
                    var rmk = document.getElementById("txt_remark").value;
                    var vtype = document.getElementById("ddlVehicleType" + (q + 1)).value;
                    var from = document.getElementById("txt_fromloc" + (q + 1)).value;
                    var to = document.getElementById("txt_toloc" + (q + 1)).value;
                    var kms = document.getElementById("txt_kms" + (q + 1)).value;
                    var fdate = document.getElementById("txt_fdate" + (q + 1)).value;
                    var tdate = document.getElementById("txt_tdate" + (q + 1)).value;
                    var mbillamt = document.getElementById("txt_amount" + (q + 1)).value;
                    var pay = document.getElementById("ddlPayMode").value
                    var loc = document.getElementById("ddlLocation").value;
  
                    if (rmk =="") {
                        alert("Please Enter Remark...!");
                        return false;
                    }
                    if (pay == 0) {
                        alert('Please Select Payment Mode');
                        return false;
                    }
                    if (loc == 0 && pay==1) {
                        alert('Please Select Payment Location');
                        return false;
                    }                 
                    if (vtype == 0) {
                        alert('Please Select Vehicle Type at row :' + (q + 1) + '');
                        return false;
                    }
                    if (from == "") {
                        alert('Please Enter From Location at row :' + (q + 1) + '');
                        return false;
                    }
                    if (to == "") {
                        alert('Please Select To Location at row :' + (q + 1) + '');
                        return false;
                    }
                    if (fdate == 0) {
                        alert('Please Select From Date at row :' + (q + 1) + '');
                        return false;
                    }
                    if (tdate == 0) {
                        alert('Please Select To Date at row :' + (q + 1) + '');
                        return false;
                    }

                    //if (mbilldate != 0) {                      
                    //        var date = new Date();
                    //        var year = date.getFullYear();
                    //        var month = (1 + date.getMonth()).toString();
                    //        month = month.length > 1 ? month : '0' + month;
                    //        var day = date.getDate().toString();
                    //        day = day.length > 1 ? day : '0' + day;
                    //        var dt1 = month + '/' + day + '/' + year;
                    //        ////////////////////////////////////////////
                    //        var year1 = mbilldate.slice(-4);
                    //        var t = mbilldate.indexOf("-");
                    //        var month1 = mbilldate.substring(t + 1);
                    //        var d = mbilldate.indexOf(year1);
                    //        var ss = mbilldate.substring(t + 1, d - 1);
                    //        var mnt = convertMonthNameToNumber(ss);
                    //        if (mnt < 10) {
                    //            mnt = "0" + mnt;
                    //        }
                    //        else {
                    //            mnt = mnt;
                    //        }
                    //        var day1 = mbilldate.substring(0, t);
                    //        var dt2 = mnt + '/' + day1 + '/' + year1;
                    //        if (dt2 > dt1) {
                    //            alert("Selected date should be less than today's date");
                    //            //str = "check";
                    //            return false;
                    //        }
                    //    }
                   
                    if (vtype != "Other") {
                        if (kms == "") {
                            alert('Please Enter KMS at row :' + (q + 1) + '');
                            return false;
                        }
                    }
                    if (mbillamt == "") {
                        alert('Please Enter Amount at row :' + (q + 1) + '');
                        return false;
                    }
                    //var uptblcount = $('#uploadTable tr').length - 1;
                    //if (uptblcount < 1) {
                    //    alert("Please Upload Files");
                    //    return false;
                    //}
                    if (kms == "" && vtype == "Other") {
                        kms = "0";
                    }
                
                    xmlvehicle += "|ROW||";
                    xmlvehicle += "|FK_Local_Conveyance_HDR_Id||#|/FK_Local_Conveyance_HDR_Id||";
                    xmlvehicle += "|VEHICLE_TYPE||" + vtype + "|/VEHICLE_TYPE||";
                    xmlvehicle += "|FROM_LOACATION||" + from + "|/FROM_LOACATION||";
                    xmlvehicle += "|TO_LOACATION||" + to + "|/TO_LOACATION||";
                    xmlvehicle += "|KMS||" + kms + "|/KMS||";
                    xmlvehicle += "|FROM_DATE||" + fdate + "|/FROM_DATE||";
                    xmlvehicle += "|TO_DATE||" + tdate + "|/TO_DATE||";
                    xmlvehicle += "|BILL_AMOUNT||" + mbillamt + "|/BILL_AMOUNT||";
                    xmlvehicle += "|REMARK||" + rmk + "|/REMARK||";
                    xmlvehicle += "|/ROW||";
                }
                xmlvehicle += "|/ROWSET||";
                var radio = document.getElementsByName("local");
                for (var rad = 1; rad <= radio.length; rad++) {
                    if (radio[rad - 1].checked) {
                        var id = $("#PK_ADVANCE_ID" + rad).val();
                        var advance_amount = $("#advance_amount" + rad).val();
                        $("#txt_advance_id").val(id);
                        $("#txt_advance_amount").val(advance_amount);
                    }
                }

		 if((parseFloat($("#txt_advance_amount").val())>parseFloat($("#spn_Total").html())) && $("#ddlPayMode option:selected").text().toUpperCase() == "BANK")
			    {
                              alert("Please Change Payment Mode and Select Payment Location...!");
				return false;
			    }

        var XMLFILE = "";
        var lastRow1 = $('#uploadTable tr').length;
        XMLFILE = "<ROWSET>";
        if (lastRow1 > 1) {
            for (var l = 0; l < lastRow1 - 1; l++) {
                var firstCol = $("#uploadTable tr")[l + 1].cells[0].innerText;
                var SecondCol = $("#uploadTable tr")[l + 1].cells[1].innerText;
                XMLFILE += "<ROW>";
                XMLFILE += "<OBJECT_TYPE>LOCAL CONVEYANCE</OBJECT_TYPE>";
                XMLFILE += "<OBJECT_VALUE>#</OBJECT_VALUE>";
                XMLFILE += "<DOCUMENT_TYPE>" + firstCol + "</DOCUMENT_TYPE>";
                XMLFILE += "<FILENAME>" + SecondCol + "</FILENAME>";
                XMLFILE += "</ROW>";
            }          
        }
        XMLFILE += "</ROWSET>";
        $("#txt_Document_Xml").val(XMLFILE);
        $("#txt_xml_data_vehicle").val(xmlvehicle);
	$("#divIns").show();
	return true;
    }
    catch (exception) {
	$("#divIns").hide();
        XMLFILE = "<ROWSET></ROWSET>";
        return false;
    }
}
function convertMonthNameToNumber(monthName) {
    var myDate = new Date(monthName + " 1, 2000");
    var monthDigit = myDate.getMonth();
    return isNaN(monthDigit) ? 0 : (monthDigit + 1);
}
//**********************************************************************************
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
    var Document_Name = $("#txt_description").val();
    if (Document_Name == "") {
            alert("Please enter desciption first...!");
            return false;
        }
    if (parseInt(filelength) == 0) {
        alert("Sorry cannot upload file ! File is Empty or file does not exist");
    }
    else if (parseInt(filelength) > 20000000) {
        alert("Sorry cannot upload file ! File Size Exceeded.");
    }
    else if (fileNameSplit[1] != "pdf" && fileNameSplit[1] != "jpeg" && fileNameSplit[1] != "png" && fileNameSplit[1] != "jpg") {
        alert("Kindly Check File Type....!");
        return false;
    }
    else {
        addToClientTable(n, args.get_length());
    }
    var uploadText = document.getElementById('FileUpload1').getElementsByTagName("input");
    for (var i = 0; i < uploadText.length; i++) {
        if (uploadText[i].type == 'text') {
            uploadText[i].value = '';
        }
    }
    document.forms[0].target = "";
}

function addToClientTable(name, size) {
    var Document_Name = $("#txt_description").val();
    var tbl = $("#uploadTable");
    var lastRow = $('#uploadTable tr').length;
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

