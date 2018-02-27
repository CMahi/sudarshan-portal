var diffDays = 0;
g_serverpath = '/Sudarshan-Portal';




function createxml() {
    var modal = $find("MP_Loading");
  
    try {
        var paymode = $("#ddl_Payment_Mode").val();
        var payloc = $("#ddlAdv_Location").val();

        if (paymode == 0) {
            alert('Please Select Payment Mode');
            return false;
        }
        if (payloc == 0 && paymode == 1) {
            alert('Please Select Payment Location');
            return false;
        }

               
        var cnt = 0;
        var TOOTALBILAMT = 0.00;
        var tblfuel = document.getElementsByName("tbl_Fuel");
        var tbllength=$('#tbl_Fuel tr').length-1;
            var xmlfuel = "|ROWSET||";          
            for (var i = 1; i <= tbllength; i++) {
                var fueldate = $("#fuelDate" + i).val();

                var fuelperticulares = $("#perticulars" + i).val();
                var fuelrate = $("#rate" + i).val();
                var fuellitre = $("#litre" + i).val();
                var fuelamount = $("#amount" + i).val();

                if (fueldate != "" && fuelperticulares != "" && fuelrate != "" && fuellitre != "" && fuelamount != "") {
                    TOOTALBILAMT = parseFloat(TOOTALBILAMT) + parseFloat($("#amount" + i).val());
                    xmlfuel += "|ROW||";
                    xmlfuel += "|FK_CAR_EXPNS_ID||#|/FK_CAR_EXPNS_ID||";
                    xmlfuel += "|FUEL_DATE||" + fueldate + "|/FUEL_DATE||";
                    xmlfuel += "|PETROL_PUMP||" + fuelperticulares + "|/PETROL_PUMP||";
                    xmlfuel += "|FUEL_RATE||" + fuelrate + "|/FUEL_RATE||";
                    xmlfuel += "|FUEL_LITRE||" + fuellitre + "|/FUEL_LITRE||";
                    xmlfuel += "|AMOUNT||" + fuelamount + "|/AMOUNT||";
                    xmlfuel += "|FK_EXPENSE_HEAD_ID||15|/FK_EXPENSE_HEAD_ID||";
                    xmlfuel += "|/ROW||";
                    cnt = 1;
                }                 
            }
            xmlfuel += "|/ROWSET||";
            $("#txt_xml_data_fuel").val(xmlfuel);

            var tblmaitainance = document.getElementsByName("tbl_Maintenance");
            var tblmlength = $('#tbl_Maintenance tr').length-1;
            var xmlmaitainance = "|ROWSET||";
            for (var j = 1; j <= tblmlength; j++) {
                var maintainancedate = $("#maitainancedate" + j).val();
                var maintainancevehical = $("#vehical" + j).val();
                var maintainancepurchasedate = $("#purachasedate" + j).val();
                var maintainanceamount = $("#maintamount" + j).val();
                var maintainance_car_age = $("#main_car_age" + j).val();
                var maintainance_particulars = $("#main_particulars" + j).val();

                if (maintainancedate != "" && maintainancevehical != "" && maintainancepurchasedate != "" && maintainanceamount != "") {
                TOOTALBILAMT = parseFloat(TOOTALBILAMT) + parseFloat($("#maintamount" + j).val());

                xmlmaitainance += "|ROW||";
                xmlmaitainance += "|FK_CAR_EXPNS_ID||#|/FK_CAR_EXPNS_ID||";
                xmlmaitainance += "|MAINTAINCE_DATE||" + maintainancedate + "|/MAINTAINCE_DATE||";
                xmlmaitainance += "|VEHICLE_NO||" + maintainancevehical + "|/VEHICLE_NO||";
                xmlmaitainance += "|DATE_OF_PURCHASE||" + maintainancepurchasedate + "|/DATE_OF_PURCHASE||";
                xmlmaitainance += "|AMOUNT||" + maintainanceamount + "|/AMOUNT||";
                xmlmaitainance += "|FK_EXPENSE_HEAD_ID||44|/FK_EXPENSE_HEAD_ID||";
                xmlmaitainance += "|CAR_AGE||"+maintainance_car_age+"|/CAR_AGE||";
                xmlmaitainance += "|BILL_DETAILS||"+maintainance_particulars+"|/BILL_DETAILS||";
                xmlmaitainance += "|/ROW||";
                cnt = 1;
            }      
            }
            xmlmaitainance += "|/ROWSET||";
            $("#txt_xml_data_maitainance").val(xmlmaitainance);
       
            var tbldriver = document.getElementsByName("tbl_Driver");
            var tbldlength = $('#tbl_Driver tr').length;
            var xmldriver = "|ROWSET||";
       
              
            if ($("#chk1")[0].checked == true) {
                    var driverdate = $("#driver_date1").val();
                    var driveramount = $("#driver_amt1").val();

                    if (driverdate == "") {
                        alert('Please select Salary Date');
                        return false;
                        
                    }
                    if (driveramount == "") {
                        alert('Please Enter Salary Amount');
                        return false;
                       
                    }
                    xmldriver += "|ROW||";
                    xmldriver += "|FK_CAR_EXPNS_ID||#|/FK_CAR_EXPNS_ID||";
                    xmldriver += "|DRIVER_TYPE||Salary|/DRIVER_TYPE||";
                    xmldriver += "|DATE||" + driverdate + "|/DATE||";
                    xmldriver += "|AMOUNT||" + driveramount + "|/AMOUNT||";
                    xmldriver += "|FK_EXPENSE_HEAD_ID||14|/FK_EXPENSE_HEAD_ID||";
                    xmldriver += "|/ROW||";
                    TOOTALBILAMT = parseFloat(TOOTALBILAMT) + parseFloat($("#driver_amt1").val());
                    cnt = 1;
                }
                 
            if ($("#chk2")[0].checked == true) {
                    var driverdate1 = $("#driver_date2").val();
                    var driveramount1 = $("#driver_amt2").val();

                    if (driverdate1 == "") {
                        alert('Please select Uniform Date');
                        return false;
                        
                    }
                    if (driveramount1 == "") {
                        alert('Please Enter Uniform Amount');
                        return false;
                        
                    }
                    TOOTALBILAMT = parseFloat(TOOTALBILAMT) + parseFloat($("#driver_amt2").val());
                    xmldriver += "|ROW||";
                    xmldriver += "|FK_CAR_EXPNS_ID||#|/FK_CAR_EXPNS_ID||";
                    xmldriver += "|DRIVER_TYPE||Uniform|/DRIVER_TYPE||";
                    xmldriver += "|DATE||" + driverdate1 + "|/DATE||";
                    xmldriver += "|AMOUNT||" + driveramount1 + "|/AMOUNT||";
                    xmldriver += "|FK_EXPENSE_HEAD_ID||14|/FK_EXPENSE_HEAD_ID||";
                    xmldriver += "|/ROW||";
                    cnt = 1;
                }

                if ($("#chk3")[0].checked == true) {
                    var drivergradate1 = $("#txt_exdate2").val();
                    var drivegraamt1 = $("#txt_driver_gra2").val();

                    if (drivergradate1 == "") {
                        alert('Please select Ex-Gratia  Date');
                        return false;
                        
                    }
                    if (drivegraamt1 == "") {
                        alert('Please Enter Ex-Gratia Amount');
                        return false;
                        
                    }
                    TOOTALBILAMT = parseFloat(TOOTALBILAMT) + parseFloat($("#txt_driver_gra2").val());
                    xmldriver += "|ROW||";
                    xmldriver += "|FK_CAR_EXPNS_ID||#|/FK_CAR_EXPNS_ID||";
                    xmldriver += "|DRIVER_TYPE||Ex-Gratia|/DRIVER_TYPE||";
                    xmldriver += "|DATE||" + drivergradate1 + "|/DATE||";
                    xmldriver += "|AMOUNT||" + drivegraamt1 + "|/AMOUNT||";
                    xmldriver += "|FK_EXPENSE_HEAD_ID||14|/FK_EXPENSE_HEAD_ID||";
                    xmldriver += "|/ROW||";
                    cnt = 1;
                }
            
            xmldriver += "|/ROWSET||";
           

            var tbltyre= $('#tbl_tyre tr').length - 1;
            var xmltyre = "|ROWSET||";
            for (var i = 1; i <= tbltyre; i++) {
                var date = $("#txt_tyredate_" + i).val();
                var amount = $("#txt_amount_" + i).val();
                var km = $("#txt_km" + i).val();
                var carage = $("#txt_car_Age").val();
                var bill_det = $("#tyre_details" + i).val();
                var kmthrehold;
		if (km == "")
		{
			km="0";
		}
                if ($("#km_chk" + i)[0].checked == true) {
                    kmthrehold = "Yes"
                }
                else {
                    if (km > 39999 && carage < 4) {
                        if ($("#km_chk" + i)[0].checked == false) {
                            alert('Please select check at row :' + w + '')
                            return false;
                            
                        }
                    }
                    kmthrehold = "No"
                }
                if (date != "" && amount != "") {
                    TOOTALBILAMT = parseFloat(TOOTALBILAMT) + parseFloat($("#txt_amount_" + i).val());
                    xmltyre += "|ROW||";
                    xmltyre += "|FK_CAR_EXPNS_ID||#|/FK_CAR_EXPNS_ID||";
                    xmltyre += "|TYRE_DATE||" + date + "|/TYRE_DATE||";
                    xmltyre += "|AMOUNT||" + amount + "|/AMOUNT||";
                    xmltyre += "|KM_THREHOLD_CROSEED||" + kmthrehold + "|/KM_THREHOLD_CROSEED||";
                    xmltyre += "|KILO_METRES||" + km + "|/KILO_METRES||";
                    xmltyre += "|FK_EXPENSE_HEAD_ID||44|/FK_EXPENSE_HEAD_ID||";
                    xmltyre += "|BILL_DETAILS||"+bill_det+"|/BILL_DETAILS||";
                    xmltyre += "|/ROW||";
                    cnt = 1;
                }
            }
            xmltyre += "|/ROWSET||";
            $("#txt_xml_data_tyre").val(xmltyre);

             var tblbattery= $('#tbl_battery tr').length - 1;
            var xmlbattery = "|ROWSET||";
            for (var i = 1; i <= tblbattery; i++) {
                var date = $("#txt_batterydt" + i).val();
                var amount = $("#txt_batteryamt" + i).val();
                var battery_particulars = $("#battery_particulars" + i).val();

                if (date != "" && amount != "") {
                    TOOTALBILAMT = parseFloat(TOOTALBILAMT) + parseFloat($("#txt_batteryamt" + i).val());
                    xmlbattery += "|ROW||";
                    xmlbattery += "|FK_CAR_EXPNS_ID||#|/FK_CAR_EXPNS_ID||";
                    xmlbattery += "|BATTERY_DATE||" + date + "|/BATTERY_DATE||";
                    xmlbattery += "|AMOUNT||" + amount + "|/AMOUNT||";
                    xmlbattery += "|FK_EXPENSE_HEAD_ID||44|/FK_EXPENSE_HEAD_ID||";
                    xmlbattery += "|BILL_DETAILS||"+battery_particulars+"|/BILL_DETAILS||";
                    xmlbattery += "|/ROW||";
                    cnt = 1;
                }
            }
            xmlbattery += "|/ROWSET||";
            $("#txt_xml_data_battery").val(xmlbattery);

             if (cnt == 0) {
                alert("Please add any one expense for car policy.");
                return false;
               
            }
            var XMLFILE = "";

            var lastRow1 = $('#tbl_DocumentDtl tr').length;
            XMLFILE = "|ROWSET||";
            if (lastRow1 > 1) {
                for (var l = 0; l < lastRow1 - 1; l++) {
                    var SecondCol = $("#tbl_DocumentDtl tr")[l + 1].cells[0].innerText;
                    var ThirdCol = $("#tbl_DocumentDtl tr")[l + 1].cells[1].innerText;
                    XMLFILE += "|ROW||";
                    XMLFILE += "|OBJECT_TYPE||CAR POLICY|/OBJECT_TYPE||";
                    XMLFILE += "|OBJECT_VALUE||#|/OBJECT_VALUE||";
                    XMLFILE += "|FILENAME||" + SecondCol + "|/FILENAME||";
                    XMLFILE += "|DOCUMENT_TYPE||" + ThirdCol + "|/DOCUMENT_TYPE||";
                    XMLFILE += "|/ROW||";
                }
            }
	else
		{
                alert("Please add any one attachment.");
                return false;
               
            }
            XMLFILE += "|/ROWSET||";
            $("#txt_xml_data_driver").val(xmldriver);
            $("#txtexpnsamt").val(TOOTALBILAMT);
            $("#Txt_File_xml").val(XMLFILE);

            modal.show();
        return true;
            //return false;
       
    }
    catch (exception) {
      
        XMLFILE = "<ROWSET></ROWSET>";
        return false;
    }
}

/***************************************************************************************************************************/

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
    filename = filename.replace(/[&\/\\#,+()$~%'":*?<>{} ]/g, "");

    var fileNameSplit = filename.split('.');

    var contentType = args.get_contentType();
    var filelength = args.get_length();
    if (parseInt(filelength) == 0) {
        alert("Sorry cannot upload file ! File is Empty or file does not exist");
    }
    else if (parseInt(filelength) > 8000000) {
        alert("Sorry cannot upload file ! File Size Exceeded.");
    }
    else if (contentType == "application/octet-stream" && fileNameSplit[1] == "exe") {
        alert("Kindly Check File Type.");
    }
    else {
        addToClientTable(filename, args.get_length());
    }
    var uploadText = document.getElementById('FileUpload1');
    if (uploadText) {
        uploadText = uploadText.getElementsByTagName("input");
        for (var i = 0; i < uploadText.length; i++) {
            if (uploadText[i].type == 'text') {
                uploadText[i].value = '';
            }
        }
    }
    document.forms[0].target = "";
}

function addToClientTable(name, size) {

    var tbl = $("#tbl_DocumentDtl");
    var lastRow = $('#tbl_DocumentDtl tr').length;
    var doctype = $("#doctype").val();
    var doctype1;
    if (doctype == 1) {
         doctype1 = document.getElementById('doctype').children['1'].innerText;
    }
    else if (doctype == 2) {
        doctype1 = document.getElementById('doctype').children['2'].innerText;
    }
    else if (doctype == 3) {
        doctype1 = document.getElementById('doctype').children['3'].innerText;
    }
    else if (doctype == 4) {
        doctype1 = document.getElementById('doctype').children['4'].innerText;
    }
    else if (doctype == 5) {
        doctype1 = document.getElementById('doctype').children['5'].innerText;
    }
    var html2 = "<tr><td><a id='a_downloadfiles" + lastRow + "' style='cursor: pointer' onclick=\"return downloadfiles('" + lastRow + "');\" >" + name + "</a></td>";
    var html3 = "<td><input id='textdesc" + lastRow + "' type='hidden' value='" + doctype + "'  class='form-control' style='vertical-align:middle; cursor:pointer;'/>"+doctype1+"</td>"
    var html4 = "<td><i id='del" + lastRow + "' class='glyphicon glyphicon-trash' align='center' onclick=\"return deletefile(" + (lastRow) + ");\" ></td></tr>";
    var htmlcontent = $(html2 + html3 + html4);
    $('#tbl_DocumentDtl').append(htmlcontent);

 
}

function downloadfiles(index) {
    var tbl = document.getElementById("tbl_DocumentDtl");
    var app_path = $("#app_Path").val();
    //var lastRow = tbl.rows.length;
    window.open(app_path+'/Common/FileDownload.aspx?enquiryno=NA&filename=' + tbl.rows[index].cells[0].innerText + '&filetag=', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
}

//to delete file from list
function deletefile(RowIndex) {
    try {
        var tbl = document.getElementById("tbl_DocumentDtl");
        var lastRow = tbl.rows.length;
        var filename = tbl.rows[RowIndex].cells[1].innerText;
        if (lastRow <= 1)
            return false;
        for (var contolIndex = RowIndex; contolIndex < lastRow - 1; contolIndex++) {
            document.getElementById("del" + (contolIndex + 1)).onclick = new Function("deletefile(" + contolIndex + ")");
            document.getElementById("del" + (contolIndex + 1)).id = "del" + contolIndex;
            document.getElementById("a_downloadfiles" + (contolIndex + 1)).onclick = new Function("downloadfiles(" + contolIndex + ")");
            document.getElementById("a_downloadfiles" + (contolIndex + 1)).id = "a_downloadfiles" + contolIndex;
        }
        tbl.deleteRow(RowIndex);
        deletephysicalfile(filename);
    }
    catch (Exc) { }
}

function checkf(id) {
    calculate(id);
    var tbllength = $('#tbl_Fuel tr').length - 1;
    var fuellitre = $("#litre" + id).val();
    var grade = $("#span_grade").html();
    var adid = $("#txt_Username").val();
    var date = $("#fuelDate" + id).val();
    var desig = $("#txt_designation").val();
    var ltr = 0;
    for (var i = 1; i <= tbllength; i++) {
        ltr = parseFloat(ltr) + parseFloat($("#litre" + i).val());
    }
    Car_Expense.checkfule(ltr, grade, adid, desig,date,id, OnSuccess);
}

function OnSuccess(response) {
    var result = response.value.split('#');
    var id = result[1];
    if (result[0] == "false") {
        alert("You have exceeded limit of fuel as per policy");
        $("#txt_fule_Dev").val('Y');
        $("#fuelDate" + id).val('');
        $("#rate" + id).val('');
        $("#litre" + id).val('');
        $("#perticulars" + id).val('');
        $("#amount" + id).val('');
    }
}

function checkm(id) {
    var tbllength = $('#tbl_Maintenance tr').length - 1;
    var mainamt = $("#maintamount" + id).val();
    var grade = $("#span_grade").html();
    var adid = $("#txt_Username").val();
    var date = $("#maitainancedate" + id).val();
    var desig = $("#txt_designation").val();
    var empno = $("#empno").html();
    var carage = $("#txt_car_Age").val();
    var amt = 0;
    for (var i = 1; i <= tbllength; i++) {
        amt = parseFloat(amt) + parseFloat($("#maintamount" + i).val());
    }
    Car_Expense.checkm(amt, grade, adid, desig, date, empno, id,carage, OnMSuccess);
}

function OnMSuccess(response) {
    var result = response.value.split('#');
    var id = result[1];
    if (result[0] == "false") {
        alert("You have exceeded limit of maintenance as per policy");
        $("#txt_maintain_Dev").val('Y');
        $("#maitainancedate" + id).val('');
        $("#maintamount" + id).val('');
    }
}

function checks(id) {
    var date = $("#driver_date1").val();
    var grade = $("#span_grade").html();
    var adid = $("#txt_Username").val();
    var desig = $("#txt_designation").val();
    var amt = parseFloat($("#driver_amt1").val());
    var driversalary = parseFloat($("#txt_driversalary").val());

    if (amt > driversalary) {
        alert("Driver's salary cannot be greater than " + driversalary);
        $("#driver_amt1").val('');
        $('#chk1').prop('checked', false);
        return false;
    }

    Car_Expense.checks(date, grade, adid, desig, OnSSuccess);
}

function checkg(id) {
    var date = $("#txt_exdate2").val();
    var grade = $("#span_grade").html();
    var adid = $("#txt_Username").val();
    var desig = $("#txt_designation").val();
    var amt = parseFloat($("#txt_driver_gra2").val());
    var driversalary = parseFloat($("#txt_driversalary").val());

    if (amt > driversalary) {
        alert("Ex-Gratia cannot be greater than " + driversalary);
        $("#txt_driver_gra2").val('');
        $('#chk3').prop('checked', false);
        return false;
    }

    Car_Expense.checkg(date, grade, adid, desig, OnGSuccess);
}

function OnGSuccess(response) {

    if (response.value == "false") {
        alert("Ex-Gratia given already.");
        $("#txt_exdate2").val('');
        $("#txt_driver_gra2").val('');
        $('#chk3').prop('checked', false);
    }
}

function OnSSuccess(response) {
  
    if (response.value == "false") {
        alert("Salary given already.");
        $("#driver_date1").val('');
        $("#driver_amt1").val('');
        $('#chk1').prop('checked', false);
    }
}

function checku(id) {
    var date = $("#driver_date2").val();
    var grade = $("#span_grade").html();
    var adid = $("#txt_Username").val();
    var desig = $("#txt_designation").val();

    Car_Expense.checku(date, grade, adid, desig, OnUSuccess);
}

function OnUSuccess(response) {
   
    if (response.value == "false") {
        alert("Uniform already given.");
        $("#driver_date2").val('');
        $("#driver_amt2").val('');
        $('#chk2').prop('checked', false);
    }
}

function chknumeric(id) {
    
    var a = isNaN($("#" + id).val());
    if (a == true) {

        alert("Enter Only Numbers...");
        $("#" + id).val('');
       
        // return false;
    }
}


function calculate(id) {
    var amtf = 0;

    var fuellitre = $("#litre" + id).val();
    var fuelrate = $("#rate" + id).val();
    if (fuellitre != "" && fuelrate != "" && fuellitre != "0" && fuelrate != "0") {
            amtf = parseFloat(amtf) + (parseFloat(fuelrate) * parseFloat(fuellitre));
            amtf = Math.round(amtf);
            $("#amount" + id).val(amtf);
         }

    }

    function checkb(id) {
        var date = $("#txt_batterydt" + id).val();
        var adid = $("#txt_Username").val();
        var amt = $("#txt_batteryamt"+id).val();

        Car_Expense.checkb(date, adid, amt,id, OnBSuccess);
    }

    function OnBSuccess(response) {

        var result = response.value.split('#');
        var id = result[1];
        if (result[0] == "false") {
            alert("Battery can be replaced once in 2 years");
            $("#txt_batterydt" + id).val('');
            $("#txt_batteryamt" + id).val('');
        }
    }

    function checkt(id) {
        var km = $("#txt_km" + id).val();
        var carage = $("#txt_car_Age").val();
        if (carage < 4) {
            if ($("#km_chk" + id)[0].checked == false) {
                alert('Please select checkbox at row :' + id + '');
                $("#txt_km" + id).val('');
                $("#txt_amount_" + id).val('');
                return false;
            }
            else {
                if (km == "") {
                    alert('Please enter kilometer at row :' + id + '');
                    $("#txt_km" + id).val('');
                    $("#txt_amount_" + id).val('');
                }
            }
        }
// else {
//                if (km == "") {
//                    alert('Please enter kilometer at row :' + id + '');
//                    $("#txt_km" + id).val('');
 //                   $("#txt_amount_" + id).val('');
 //               }
 //           }
    }

    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;
    }