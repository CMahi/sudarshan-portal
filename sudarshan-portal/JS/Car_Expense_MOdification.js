var diffDays = 0;
g_serverpath = '/Sudarshan-Portal';
$(document).ready(function () {

    $(".datepicker-rtl").datepicker({
        format: 'dd-M-yyyy', autoclose: false, todayBtn: 'linked'
    })
});

$('body').on('click', '.datepicker-dropdown', function () {
    $(".datepicker-dropdown").datepicker({ format: 'dd-M-yyyy', todayBtn: 'linked', autoclose: true, endDate: new Date() })
});

$('body').on('click', '.add_Fuel', function () {
    addNewRowFuel();
});



function addNewRowFuel() {
    var lastRow = $('#tblfuel tr').length;
    var lastRow1 = $('#tblfuel tr').length - 1;
    var html = "";
    for (var q = 1; q <= lastRow1; q++) {

        var fueldate = $("#fuelDate" + q).val();

        var fuelperticulares = $("#fuelperticulars" + q).val();
        var rate = $("#rate" + q).val();
        var fuellitre = $("#fuellitre" + q).val();
        var fuelamount = $("#txtfuelamount" + q).val();

        if (fueldate == "") {
            alert('Please select Date at row :' + q + '');
            return false;
        }
        if (fuelperticulares == "") {
            alert('Please Enter perticulars at row :' + q + '');
            return false;
        }
        if (rate == "") {
            alert('Please Enter Rate at row :' + q + '');
            return false;
        }
        if (fuellitre == "") {
            alert('Please Enter Litre at row :' + q + '');
            return false;
        }
        if (fuelamount == "") {
            alert('Please Enter amount at row :' + q + '');
            return false;
        }

    }
    html += "<tr><td>" + (lastRow1 + 1) + "</td>";
    html += "<td><div class='input-group'><input type='text' class='form-control input-sm datepicker-dropdown' id='fuelDate" + (lastRow1 + 1) + "' readonly /><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button></span></div></td>";
    html += "<td><input class='form-control input-sm' type='text' id='fuelperticulars" + (lastRow1 + 1) + "'></input></td>";
    html += "<td><input class='form-control input-sm' type='text' style='text-align:right' onkeypress='return isNumberKey(event)' id='rate" + (lastRow1 + 1) + "' onchange='calculate(" + (lastRow1 + 1) + ")' ></input></td>";
    html += "<td><input class='form-control input-sm' type='text' style='text-align:right' onkeypress='return isNumberKey(event)' id='fuellitre" + (lastRow1 + 1) + "' onchange='checkf(" + (lastRow1 + 1) + ")' ></input></td>";
    html += "<td><input class='form-control input-sm' type='text' style='text-align:right' onkeypress='return isNumberKey(event)' id='txtfuelamount" + (lastRow1 + 1) + "' readonly='readonly'></input></td>";
    html += "<td class='add_Fuel'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i></td>";
    html += "<td class='delete_Fuel' ><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-trash'></i><input class='form-control input-sm' type='hidden' id='txtaddnewrow" + (lastRow1 + 1) + "' value='newRow'></input></td></tr>";
    $('#tblfuel tr:last').after(html);
    $(".datepicker-dropdown").datepicker({ format: 'dd-M-yyyy', todayBtn: 'linked', autoclose: true, endDate: new Date() })
}

$("#tblfuel").on('click', '.delete_Fuel', function () {
    $(this).closest('tr').remove();
});


$("#add_Maintenance").click(function () {
    addNewRowMaintenance();
});

$('body').on('click', '.add_Maintenance', function () {
    addNewRowMaintenance();
});


function addNewRowMaintenance() {
    var lastRow = $('#tblmaintenance tr').length;
    var lastRow1 = $('#tblmaintenance tr').length - 1;

    for (var w = 1; w <= lastRow1; w++) {
        var maintainancedate = $("#maitainancedate" + w).val();
        var maintainancevehical = $("#vehical" + w).val();
        var maintainancepurchasedate = $("#purachasedate" + w).val();
        var maintainanceamount = $("#maintamount" + w).val();

        if (maintainancedate == "") {
            alert('Please select Date at row :' + w + '');
            return false;
        }
        if (maintainancevehical == "") {
            alert('Please Enter Vehical No. at row :' + w + '');
            return false;
        }
        if (maintainancepurchasedate == "") {
            alert('Please Select Purchase date at row :' + w + '');
            return false;
        }
        if (maintainanceamount == "") {
            alert('Please Enter amount at row :' + w + '');
            return false;
        }
    }
    var html1 = "";
    html1 += "<tr><td>" + (lastRow1 + 1) + "</td>";
    html1 += "<td><div class='input-group'><input type='text' class='form-control input-sm datepicker-dropdown ' id='maitainancedate" + (lastRow1 + 1) + "' readonly /><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button></span></div></td>";
    html1 += "<td><input class='form-control input-sm' type='text' id='main_car_age" + (lastRow1 + 1) + "' value='" + $("#txt_car_Age").val() + "' readonly></input></td>";
    html1 += "<td><input class='form-control input-sm' type='text' id='main_particulars" + (lastRow1 + 1) + "' value=''></input></td>";
    html1 += "<td><input class='form-control input-sm' type='text' id='vehical" + (lastRow1 + 1) + "' value='" + maintainancevehical + "'></input></td>";
    html1 += "<td><div class='input-group'><input type='text' class='form-control input-sm datepicker-dropdown ' id='purachasedate" + (lastRow1 + 1) + "' value='" + maintainancepurchasedate + "' readonly /><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button></span></div></td>";
    html1 += "<td><input class='form-control input-sm' type='text' style='text-align:right' onkeypress='return isNumberKey(event)' id='maintamount" + (lastRow1 + 1) + "'></input></td>";
    html1 += "<td class='add_Maintenance'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i></td>";
    html1 += "<td class='delete_Maitainance'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-trash'></i><input class='form-control input-sm' type='hidden' id='txtaddnewrowm" + (lastRow1 + 1) + "' value='newRowm'></input></td></tr>";

    $('#tblmaintenance tr:last').after(html1);
    $(".datepicker-dropdown").datepicker({ format: 'dd-M-yyyy', todayBtn: 'linked', autoclose: true, endDate: new Date() })

}


$("#tblmaintenance").on('click', '.delete_Maitainance', function () {
    $(this).closest('tr').remove();
});

$("#add_tyre").click(function () {
    addTyre();
});

$('body').on('click', '.add_tyre', function () {
    addTyre();
});



function addTyre() {
    var lastRow = $('#tbltyre tr').length;
    var lastRow1 = $('#tbltyre tr').length - 1;

    for (var w = 1; w <= lastRow1; w++) {
        var date = $("#txt_tyredate_" + w).val();
        var amount = $("#txt_amount_" + w).val();
        var km = $("#txt_amount_" + w).val();

        if (date == "") {
            alert('Please select Date at row :' + w + '');
            return false;
        }
        if (km == "") {
            alert('Please select Kilometers at row :' + w + '');
            return false;
        }
        if (amount == "") {
            alert('Please Enter amount at row :' + w + '');
            return false;
        }
    }

    var html = "<tr><td>" + (lastRow1 + 1) + "</td>";
    var html7 = "<td><input class='form-control input-sm' type='text' value='" + $("#txt_car_Age").val() + "'  readonly ></input></td>";
    var html1 = "<td><div class='input-group'><input type='text' class='form-control input-sm datepicker-dropdown ' id='txt_tyredate_" + (lastRow1 + 1) + "' readonly /><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button></span></div></td>";
    var htmltyre = "<td style='text-align:center'><input id='tyre_details" + (lastRow1 + 1) + "' class='form-control input-sm' value='' type='textbox'></input></td>";
    var html6 = "<td style='text-align:center'><input id='km_chk" + (lastRow1 + 1) + "' value='' type='checkbox'></input></td>";
    var html5 = "<td><input class='form-control input-sm' type='text' style='text-align:right' id='txt_km" + (lastRow1 + 1) + "' onkeypress='chknumeric(this.id);' onblur='checkt(" + (lastRow1 + 1) + ");' ></input></td>";
    var html2 = "<td><input class='form-control input-sm' type='text' style='text-align:right' id='txt_amount_" + (lastRow1 + 1) + "' onblur='checkt(" + (lastRow1 + 1) + ");' onkeypress='chknumeric(this.id);' ></input></td>";
    var html3 = "<td class='add_tyre'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i></td>";
    var html4 = "<td class='delete_tyre'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-trash'></i></td></tr>";
    var htmlcontent = $(html + "" + html7 + "" + html1 + htmltyre + "" + html6 + "" + html5 + "" + html2 + "" + html3 + "" + html4);
    $('#tbltyre tr:last').after(htmlcontent);
    $(".datepicker-dropdown").datepicker({ format: 'dd-M-yyyy', todayBtn: 'linked', autoclose: true, endDate: new Date() })

}

$("#tbltyre").on('click', '.delete_tyre', function () {
    $(this).closest('tr').remove();
});

function getexpensedtl() {

    var html = "";
    var html1 = "";
    var html2 = "";
    var html3 = "";
    var html4 = "";
    var html5 = "";
    var html6 = "";
    var amt = 0;
    var amtf = 0;
    var amtm = 0;
    var amtt = 0;
    var amtd = 0;
    var amtb = 0;

    $('#tbl_expns tbody').remove();

    var lastRow = $('#tblfuel tr').length;
    var lastRow1 = $('#tblfuel tr').length - 1;
    if (lastRow > 1) {
        for (var n = 1; n <= lastRow1; n++) {
            var fueldate = $("#fuelDate" + n).val();
            var fuelamount = $("#txtfuelamount" + n).val();
            if (fueldate != "" && fuelamount != "") {
                amtf = parseFloat(amtf) + parseFloat($("#txtfuelamount" + n).val());
            }


        }
        html += "<tr><td></td>";
        html += "<td>Fuel</td>";
        html += "<td align='left'>" + amtf + "</td></tr>";
        amt = parseFloat(amt) + parseFloat(amtf);
        $('#tbl_expns').append(html);
    }

    var lastRowm = $('#tblmaintenance tr').length;
    var lastRow1m = $('#tblmaintenance tr').length - 1;
    if (lastRow > 1) {
        for (var j = 1; j <= lastRow1m; j++) {
            var maintainancedate = $("#maitainancedate" + j).val();
            var maintainanceamount = $("#maintamount" + j).val();
            if (maintainancedate != "" && maintainanceamount != "") {
                amtm = parseFloat(amtm) + parseFloat($("#maintamount" + j).val());
            }

        }
        html1 += "<tr><td></td>";
        html1 += "<td>Maintenance</td>";
        html1 += "<td align='left'>" + amtm + "</td></tr>";
        amt = parseFloat(amt) + parseFloat(amtm);
        $('#tbl_expns').append(html1);
    }
    var lastRow = $('#tbldriver tr').length;
    var lastRow1d = $('#tbldriver tr').length - 1;
    if (lastRow > 1) {
        for (var k = 1; k <= lastRow1d; k++) {
            var driverdate = $("#driverdate" + k).val();
            var driveramount = $("#txtdriveramount" + k).val();
            if (driverdate != "" && driveramount != "" && driveramount != undefined) {

                amtd = parseFloat(amtd) + parseFloat($("#txtdriveramount" + k).val());
            }

        }
        html2 += "<tr><td></td>";
        html2 += "<td>Driver</td>";
        html2 += "<td align='left'>" + amtd + "</td></tr>";
        amt = parseFloat(amt) + parseFloat(amtd);
        $('#tbl_expns').append(html2);
    }
    var lastRowb = $('#tblbattery tr').length;
    var lastRow1b = $('#tblbattery tr').length - 1;
    if (lastRowb > 1) {
        for (var j = 1; j <= lastRow1b; j++) {
            var batteryamount = $("#txt_batteryamt" + j).val();
            if (batteryamount != "") {

                amtb = parseFloat(amtb) + parseFloat($("#txt_batteryamt" + j).val());
            }
        }
        html6 += "<tr><td></td>";
        html6 += "<td>Battery</td>";
        html6 += "<td align='left'>" + amtb + "</td></tr>";
        amt = parseFloat(amt) + parseFloat(amtb);
        $('#tbl_expns').append(html6);
    }
    var lastRowm = $('#tbltyre tr').length;
    var lastRow1m = $('#tbltyre tr').length - 1;
    if (lastRowm > 1) {
        for (var j = 1; j <= lastRow1m; j++) {
            var maintainanceamount = $("#txt_amount_" + j).val();
            if (maintainanceamount != "") {

                amtt = parseFloat(amtt) + parseFloat($("#txt_amount_" + j).val());
            }
        }
        html5 += "<tr><td></td>";
        html5 += "<td>Tyre</td>";
        html5 += "<td align='left'>" + amtt + "</td></tr>";
        amt = parseFloat(amt) + parseFloat(amtt);
        $('#tbl_expns').append(html5);
    }
    html4 += "<tr><td></td>";
    html4 += "<td align='right'>Total</td>";
    html4 += "<td align='left'>" + amt + "</td></tr>";
    $('#tbl_expns').append(html4);
}

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


        var tblremark = $('#tblremark').val();
        var apprremark = $("#txtRemark").val();
        $("#txt_Remark").val(apprremark);

        var TOOTALBILAMT = 0.00;
        var tblfuel = document.getElementsByName("tblfuel");
        var tbllength = $('#tblfuel tr').length - 1;
        var xmlfuel = "|ROWSET||";
        var xmlfuelupd = "|ROWSET||";

        for (var i = 1; i <= tbllength; i++) {
            var tblnewrow = $("#txtaddnewrow" + i).val();
            if (tblnewrow != "newRow") {
                var fueldate = $("#fuelDate" + i).val();
                var fuelperticulares = $("#fuelperticulars" + i).val();
                var fuellitre = $("#fuellitre" + i).val();
                var fuelamount = $("#txtfuelamount" + i).val();
                var pkhdrid = $("#txtpk" + i).val();
                var fkhdrid = $("#txtfk" + i).val();
                var fuelrate = $("#rate" + i).val();
                $("#txt_fk_hdr_id").val(fkhdrid);
                if (fueldate == "") {
                    alert('Please select Date at row :' + i + '');
                    return false;
                    
                }
                if (fuelperticulares == "") {
                    alert('Please Enter perticulars at row :' + i + '');
                    return false;
                   
                }
                if (fuellitre == "") {
                    alert('Please Enter Litre at row :' + i + '');
                    return false;
                    
                }
                if (fuelamount == "") {
                    alert('Please Enter amount at row :' + i + '');
                    return false;
                    
                }

                TOOTALBILAMT = parseFloat(TOOTALBILAMT) + parseFloat($("#txtfuelamount" + i).val());
                xmlfuelupd += "|ROW||";
                xmlfuelupd += "|PK_CAR_FUEL_ID||" + pkhdrid + "|/PK_CAR_FUEL_ID||";
                xmlfuelupd += "|FK_CAR_EXPNS_ID||" + fkhdrid + "|/FK_CAR_EXPNS_ID||";
                xmlfuelupd += "|FUEL_DATE||" + fueldate + "|/FUEL_DATE||";
                xmlfuelupd += "|PETROL_PUMP||" + fuelperticulares + "|/PETROL_PUMP||";
                xmlfuelupd += "|FUEL_RATE||" + fuelrate + "|/FUEL_RATE||";
                xmlfuelupd += "|FUEL_LITRE||" + fuellitre + "|/FUEL_LITRE||";
                xmlfuelupd += "|AMOUNT||" + fuelamount + "|/AMOUNT||";
                xmlfuelupd += "|FK_EXPENSE_HEAD_ID||15|/FK_EXPENSE_HEAD_ID||";
                xmlfuelupd += "|/ROW||";
            }
            else {

                var fueldate = $("#fuelDate" + i).val();
                var fuelperticulares = $("#fuelperticulars" + i).val();
                var fuellitre = $("#fuellitre" + i).val();
                var fuelamount = $("#txtfuelamount" + i).val();
                var fuelrate = $("#rate" + i).val();

               
                if (fueldate != "" && fuelperticulares != "" && fuellitre != "" && fuelamount != "") {

                    TOOTALBILAMT = parseFloat(TOOTALBILAMT) + parseFloat($("#txtfuelamount" + i).val());
                    xmlfuel += "|ROW||";
                    xmlfuel += "|FK_CAR_EXPNS_ID||" + fkhdrid + "|/FK_CAR_EXPNS_ID||";
                    xmlfuel += "|FUEL_DATE||" + fueldate + "|/FUEL_DATE||";
                    xmlfuel += "|PETROL_PUMP||" + fuelperticulares + "|/PETROL_PUMP||";
                    xmlfuel += "|FUEL_RATE||" + fuelrate + "|/FUEL_RATE||";
                    xmlfuel += "|FUEL_LITRE||" + fuellitre + "|/FUEL_LITRE||";
                    xmlfuel += "|AMOUNT||" + fuelamount + "|/AMOUNT||";
                    xmlfuel += "|FK_EXPENSE_HEAD_ID||15|/FK_EXPENSE_HEAD_ID||";
                    xmlfuel += "|/ROW||";
                }
            }
        }
        xmlfuel += "|/ROWSET||";
        xmlfuelupd += "|/ROWSET||";
        $("#txt_xml_data_fuel").val(xmlfuel);
        $("#txt_xml_data_fuel_upd").val(xmlfuelupd);


        var tblmaitainance = document.getElementsByName("tblmaintenance");
        var tblmlength = $('#tblmaintenance tr').length - 1;
        var xmlmaitainance = "|ROWSET||";
        var xmlmaitainanceupd = "|ROWSET||";
        for (var j = 1; j <= tblmlength; j++) {
            var tblnewrowm = $("#txtaddnewrowm" + j).val();
            if (tblnewrowm != "newRowm") {
                var maintainancedate = $("#maitainancedate" + j).val();
                var maintainancevehical = $("#vehical" + j).val();
                var maintainancepurchasedate = $("#purachasedate" + j).val();
                var maintainanceamount = $("#maintamount" + j).val();
                var pkmaitainid = $("#txtpkmaintan" + j).val();
                var fkmaintaid = $("#txtfkmaintan" + j).val();
                var main_car_age = $("#main_car_age" + j).val();
                var main_particulars = $("#main_particulars" + j).val();
                $("#txt_fk_hdr_id").val(fkmaintaid);
                if (maintainancedate == "") {
                    alert('Please select Date at row :' + j + '');
                    return false;
                   
                }
                if (maintainancevehical == "") {
                    alert('Please Enter Vehical No. at row :' + j + '');
                    return false;
                    
                }
                if (maintainancepurchasedate == "") {
                    alert('Please Select Purchase date at row :' + j + '');
                    return false;
                   
                }
                if (maintainanceamount == "") {
                    alert('Please Enter amount at row :' + j + '');
                    return false;
                    
                }

                TOOTALBILAMT = parseFloat(TOOTALBILAMT) + parseFloat($("#maintamount" + j).val());

                xmlmaitainanceupd += "|ROW||";
                xmlmaitainanceupd += "|PK_CAR_MAINTENANCE_ID||" + pkmaitainid + "|/PK_CAR_MAINTENANCE_ID||";
                xmlmaitainanceupd += "|FK_CAR_EXPNS_ID||" + fkmaintaid + "|/FK_CAR_EXPNS_ID||";
                xmlmaitainanceupd += "|MAINTAINCE_DATE||" + maintainancedate + "|/MAINTAINCE_DATE||";
                xmlmaitainanceupd += "|VEHICLE_NO||" + maintainancevehical + "|/VEHICLE_NO||";
                xmlmaitainanceupd += "|DATE_OF_PURCHASE||" + maintainancepurchasedate + "|/DATE_OF_PURCHASE||";
                xmlmaitainanceupd += "|AMOUNT||" + maintainanceamount + "|/AMOUNT||";
                xmlmaitainanceupd += "|FK_EXPENSE_HEAD_ID||44|/FK_EXPENSE_HEAD_ID||";
                xmlmaitainanceupd += "|CAR_AGE||" + $("#txt_car_Age").val() + "|/CAR_AGE||";
                xmlmaitainanceupd += "|BILL_DETAILS||"+main_particulars+"|/BILL_DETAILS||";
                xmlmaitainanceupd += "|/ROW||";
            }
            else {

                var maintainancedate = $("#maitainancedate" + j).val();
                var maintainancevehical = $("#vehical" + j).val();
                var maintainancepurchasedate = $("#purachasedate" + j).val();
                var maintainanceamount = $("#maintamount" + j).val();
                var main_car_age = $("#main_car_age" + j).val();
                var main_particulars = $("#main_particulars" + j).val();
                
                if (maintainancedate != "" && maintainancevehical != "" && maintainancepurchasedate != "" && maintainanceamount != "") {
                    TOOTALBILAMT = parseFloat(TOOTALBILAMT) + parseFloat($("#maintamount" + j).val());

                    xmlmaitainance += "|ROW||";
                    xmlmaitainance += "|FK_CAR_EXPNS_ID||" + fkmaintaid + "|/FK_CAR_EXPNS_ID||";
                    xmlmaitainance += "|MAINTAINCE_DATE||" + maintainancedate + "|/MAINTAINCE_DATE||";
                    xmlmaitainance += "|VEHICLE_NO||" + maintainancevehical + "|/VEHICLE_NO||";
                    xmlmaitainance += "|DATE_OF_PURCHASE||" + maintainancepurchasedate + "|/DATE_OF_PURCHASE||";
                    xmlmaitainance += "|AMOUNT||" + maintainanceamount + "|/AMOUNT||";
                    xmlmaitainance += "|FK_EXPENSE_HEAD_ID||44|/FK_EXPENSE_HEAD_ID||";
                    xmlmaitainanceupd += "|CAR_AGE||" + $("#txt_car_Age").val() + "|/CAR_AGE||";
                    xmlmaitainanceupd += "|BILL_DETAILS||" + main_particulars + "|/BILL_DETAILS||";
                    xmlmaitainance += "|/ROW||";
                }
            }
        }
        xmlmaitainance += "|/ROWSET||";
        xmlmaitainanceupd += "|/ROWSET||";
        $("#txt_xml_data_maitainance_upd").val(xmlmaitainanceupd);
        $("#txt_xml_data_maitainance").val(xmlmaitainance);

        var tbldriver = document.getElementsByName("tbldriver");
        var tbldlength = $('#tbldriver tr').length - 1;
        var xmldriver = "|ROWSET||";
        var xmldrivernew = "|ROWSET||";
        for (var k = 1; k <= tbldlength; k++) {
            var tblnewrowd = $("#txtaddnewrowd" + k).val();
            if (tblnewrowd != 'newRowd') {
                var drivertype = $("#txtdrivertype" + k).val();
                var driverdate = $("#driverdate" + k).val();
                var driveramount = $("#txtdriveramount" + k).val();
                var pkdrvid = $("#txtpkdrv" + k).val();
                var fkdrvid = $("#txtfkdrv" + k).val();

                if (driverdate != "" && driveramount != "" && driveramount!=undefined) {

                    xmldriver += "|ROW||";
                    xmldriver += "|PK_CAR_DRIVER_DTL_ID||" + pkdrvid + "|/PK_CAR_DRIVER_DTL_ID||";
                    xmldriver += "|FK_CAR_EXPNS_ID||" + fkdrvid + "|/FK_CAR_EXPNS_ID||";
                    xmldriver += "|DRIVER_TYPE||" + drivertype + "|/DRIVER_TYPE||";
                    xmldriver += "|DATE||" + driverdate + "|/DATE||";
                    xmldriver += "|AMOUNT||" + driveramount + "|/AMOUNT||";
                    xmldriver += "|FK_EXPENSE_HEAD_ID||14|/FK_EXPENSE_HEAD_ID||";
                    xmldriver += "|/ROW||";
                    TOOTALBILAMT = parseFloat(TOOTALBILAMT) + parseFloat($("#txtdriveramount" + k).val());
                }
            }
            else {
                var drivertype = $("#txtdrivertype" + k).val();
                var driverdate = $("#driverdate" + k).val();
                var driveramount = $("#txtdriveramount" + k).val();
                var fkdrvid = $("#txt_fk_hdr_id").val();
                if (driverdate != "" && driveramount != "" && driveramount != undefined) {

                    xmldrivernew += "|ROW||";
                    xmldrivernew += "|FK_CAR_EXPNS_ID||" + fkdrvid + "|/FK_CAR_EXPNS_ID||";
                    xmldrivernew += "|DRIVER_TYPE||" + drivertype + "|/DRIVER_TYPE||";
                    xmldrivernew += "|DATE||" + driverdate + "|/DATE||";
                    xmldrivernew += "|AMOUNT||" + driveramount + "|/AMOUNT||";
                    xmldrivernew += "|FK_EXPENSE_HEAD_ID||14|/FK_EXPENSE_HEAD_ID||";
                    xmldrivernew += "|/ROW||";
                    TOOTALBILAMT = parseFloat(TOOTALBILAMT) + parseFloat($("#txtdriveramount" + k).val());
                }

            }

        }

        xmldriver += "|/ROWSET||";
        xmldrivernew += "|/ROWSET||";
        $("#txt_xml_data_driver_upd").val(xmldriver);
        $("#txt_xml_data_driver").val(xmldrivernew);

        var tbltyre = $('#tbltyre tr').length - 1;
        var xmltyre = "|ROWSET||";
        for (var i = 1; i <= tbltyre; i++) {
            var date = $("#txt_tyredate_" + i).val();
            var amount = $("#txt_amount_" + i).val();
            var km = $("#txt_km" + i).val();
            var carage = $("#txt_car_Age").val();
            var bill_det = $("#tyre_details" + i).val();

            var kmthrehold;
            if ($("#km_chk" + i)[0].checked == true) {
                kmthrehold = "Yes"
            }
            else {
                if (km > 39999 && carage < 4) {
                    if ($("#km_chk" + i)[0].checked == false) {
                        alert('Please select check at row :' + i + '')
                        return false;
                        
                    }
                }
                kmthrehold = "No"
            }
            if (date != "" && amount != "" && km != "") {
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

        var tblbattery = $('#tblbattery tr').length - 1;
        var xmlbattery = "|ROWSET||";
        for (var i = 1; i <= tblbattery; i++) {
            var date = $("#txt_batterydt" + i).val();
            var amount = $("#txt_batteryamt" + i).val();
            var battery_par = $("#battery_particulars" + i).val();

            if (date != "" && amount != "") {
                TOOTALBILAMT = parseFloat(TOOTALBILAMT) + parseFloat($("#txt_batteryamt" + i).val());
                xmlbattery += "|ROW||";
                xmlbattery += "|FK_CAR_EXPNS_ID||#|/FK_CAR_EXPNS_ID||";
                xmlbattery += "|BATTERY_DATE||" + date + "|/BATTERY_DATE||";
                xmlbattery += "|AMOUNT||" + amount + "|/AMOUNT||";
                xmlbattery += "|FK_EXPENSE_HEAD_ID||44|/FK_EXPENSE_HEAD_ID||";
                xmlbattery += "|BILL_DETAILS||"+battery_par+"|/BILL_DETAILS||";
                xmlbattery += "|/ROW||";
                cnt = 1;
            }
        }
        xmlbattery += "|/ROWSET||";
        $("#txt_xml_data_battery").val(xmlbattery);


        var XMLFILE = "";

        var lastRow1 = $('#tbl_DocumentDtl tr').length;
        XMLFILE = "|ROWSET||";
        if (lastRow1 > 1) {
            for (var l = 0; l < lastRow1 - 1; l++) {
                var newfile = $("#txtnewfile" + (l + 1)).val();
                if (newfile == "New") {
                    var SecondCol = $("#tbl_DocumentDtl tr")[l + 1].cells[0].innerText;
                    var ThirdCol = $("#tbl_DocumentDtl tr")[l + 1].cells[1].innerText;
                    XMLFILE += "|ROW||";
                    XMLFILE += "|OBJECT_TYPE||CAR POLICY|/OBJECT_TYPE||";
                    XMLFILE += "|OBJECT_VALUE||#|/OBJECT_VALUE||";
                    XMLFILE += "|FILENAME||" + ThirdCol + "|/FILENAME||";
                    XMLFILE += "|DOCUMENT_TYPE||" + SecondCol + "|/DOCUMENT_TYPE||";
                    XMLFILE += "|/ROW||";
                }
            }
        }
else
		{
                alert("Please add any one attachment.");
                return false;
               
            }
        XMLFILE += "|/ROWSET||";

        $("#txtexpnsamt").val(TOOTALBILAMT);
        $("#Txt_File_xml").val(XMLFILE);

        modal.show();
        return true;

    }
    catch (exception) {

        XMLFILE = "<ROWSET></ROWSET>";
        return false;
    }
}

$("#ddl_Payment_Mode").change(function () {

    var paymode = $("#ddl_Payment_Mode").val();
    if (paymode == 2) {
        $("#div_loc").hide();
    }
    else {
        $("#div_loc").show();
    }


});



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
    else {
        alert('Please Select File Type');
        return false;
    }
    var html2 = "<tr><td><input id='textdesc" + lastRow + "' type='hidden' value='" + doctype + "'  class='form-control' style='vertical-align:middle; cursor:pointer;'/>" + doctype1 + "</td>";
    var html3 = "<td><a id='a_downloadfiles" + lastRow + "' style='cursor: pointer' onclick=\"return downloadfiles('" + lastRow + "');\" >" + name + "</a></td>"
    var html4 = "<td><i id='del" + lastRow + "' class='glyphicon glyphicon-trash' align='center' onclick=\"return deletefile(" + (lastRow) + ",'');\" ><input id='txtnewfile" + lastRow + "' type='hidden' value='New'  class='form-control'/></td></tr>";
    var htmlcontent = $(html2 + html3 + html4);
    $('#tbl_DocumentDtl').append(htmlcontent);


}

function downloadfiles(index) {
    var tbl = document.getElementById("tbl_DocumentDtl");
    var expnsno = $("#txt_Request").val();
    var app_path = $("#app_Path").val();
    //var lastRow = tbl.rows.length;
    window.open(app_path + '/Common/FileDownload.aspx?enquiryno=' + expnsno + '&filename=' + tbl.rows[index].cells[1].innerText + '&filetag=', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
}

//to delete file from list
var pkid;
function deletefile(RowIndex, pkid) {
    try {
        var tbl = document.getElementById("tbl_DocumentDtl");
        var lastRow = tbl.rows.length;
        if (lastRow <= 1)
            return false;
        for (var contolIndex = RowIndex; contolIndex < lastRow - 1; contolIndex++) {
            document.getElementById("del" + (contolIndex + 1)).onclick = new Function("deletefile(" + contolIndex + ")");
            document.getElementById("del" + (contolIndex + 1)).id = "del" + contolIndex;
            document.getElementById("a_downloadfiles" + (contolIndex + 1)).onclick = new Function("downloadfiles(" + contolIndex + ")");
            document.getElementById("a_downloadfiles" + (contolIndex + 1)).id = "a_downloadfiles" + contolIndex;
        }
        tbl.deleteRow(RowIndex);
        deletefiletbl(pkid);
    }
    catch (Exc) { }
}

function deletefiletbl(pkid) {
    Car_Expense_MOdification.deletedoctbl(pkid, callback_doc);
}


function callback_doc(response) {
    if (response.value == "true") {
        alert('Document Deleted Successfully...!');
    }
}


function checkf(id) {
    calculate(id);
    var tbllength = $('#tblfuel tr').length - 1;
    var fuellitre = $("#fuellitre" + id).val();
    var grade = $("#span_grade").html();
    var adid = $("#txt_Username").val();
    var date = $("#fuelDate" + id).val();
    var desig = $("#txt_designation").val();
    var ltr = 0;
    for (var i = 1; i <= tbllength; i++) {
        ltr = parseFloat(ltr) + parseFloat($("#fuellitre" + i).val());
    }
    Car_Expense_MOdification.checkfule(ltr, grade, adid, desig, date, id, OnSuccess);
}

function OnSuccess(response) {
    var result = response.value.split('#');
    var id = result[1];
    if (result[0] == "false") {
        alert("You have exceeded limit of fuel as per policy");
        var x = 'Y';
        $("#txt_fuel_flag").val(x);
        $("#fuelDate" + id).val('');
        $("#fuellitre" + id).val('');
        $("#rate" + id).val('');
        $("#fuelperticulars" + id).val('');
        $("#txtfuelamount" + id).val('');
    }
    else {
        var y = 'N';
        $("#txt_fuel_flag").val(y);
    }
}

function checkm(id) {
    var tbllength = $('#tblmaintenance tr').length - 1;
    var mainamt = $("#maintamount" + id).val();
    var grade = $("#span_grade").html();
    var adid = $("#txt_Username").val();
    var date = $("#maitainancedate" + id).val();
    var desig = $("#txt_designation").val();
    var empno = $("#empno").html();
    var amt = 0;
    for (var i = 1; i <= tbllength; i++) {
        amt = parseFloat(amt) + parseFloat($("#maintamount" + i).val());
    }
    Car_Expense_MOdification.checkm(amt, grade, adid, desig, date, empno, id, OnMSuccess);
}

function OnMSuccess(response) {
    var result = response.value.split('#');
    var id = result[1];
    if (result[0] == "false") {
        alert("You have exceeded limit of maintenance as per policy");
        var a = 'Y';
        $("#txt_maintain_flag").val(a);
        $("#maitainancedate" + id).val('');
        $("#maintamount" + id).val('');
    }
    else {
        var b = 'N';
        $("#txt_maintain_flag").val(b);
    }
}

function checkg(id) {
    var date = $("#txt_exdate2").val();
    var grade = $("#span_grade").html();
    var adid = $("#txt_Username").val();
    var desig = $("#txt_designation").val();
    var amt = parseFloat($("#txtdriveramount3").val());
    var driversalary = parseFloat($("#txt_driversalary").val());

    if (amt > driversalary) {
        alert("Ex-Gratia cannot be greater than " + driversalary);
        $("#txtdriveramount3").val('');
        //$('#chk3').prop('checked', false);
        return false;
    }

    Car_Expense.checkg(date, grade, adid, desig, OnGSuccess);
}

function OnGSuccess(response) {

    if (response.value == "false") {
        alert("Ex-Gratia given already.");
        $("#driverdate3").val('');
        $("#txtdriveramount3").val('');
        //$('#chk3').prop('checked', false);
    }
}

function checks(id) {
    var date = $("#driverdate" + id).val();
    var grade = $("#span_grade").html();
    var adid = $("#txt_Initiator").val();
    var desig = $("#txt_designation").val();
    var amt = $("#txtdriveramount" + id).val();
    var driversalary = parseFloat($("#txt_driversalary").val());

    if (amt > driversalary) {
        alert("Driver's salary cannot be greater than " + driversalary);
        $("#txtdriveramount" + id).val('');
        return false;
    }
    Car_Expense_MOdification.checks(date, grade, adid, desig, OnSSuccess);
}

function OnSSuccess(response) {
    if (response.value == "false") {
        alert("Salary already given.");
        $("#driverdate1").val('');
        $("#txtdriveramount1").val('');
    }
}

function checka() {

    var amt = parseFloat($("#driver_amt1").val());
    var driversalary = parseFloat($("#txt_driversalary").val());

    if (amt > driversalary) {
        alert("Driver's salary cannot be greater than " + driversalary);
        $("#driver_amt1").val('');
        $('#chk1').prop('checked', false);
        return false;
    }
}

function calculate(id) {
    var amtf = 0;

    var fuellitre = $("#fuellitre" + id).val();
    var fuelrate = $("#rate" + id).val();
    if (fuellitre != "" && fuelrate != "") {
        amtf = parseFloat(amtf) + (parseFloat(fuelrate) * parseFloat(fuellitre));
        amtf = Math.round(amtf);
        $("#txtfuelamount" + id).val(amtf);
    }

}

$("#add_battery").click(function () {
    addBattery();
});

$('body').on('click', '.add_battery', function () {
    addBattery();
});



function addBattery() {
    var lastRow = $('#tblbattery tr').length;
    var lastRow1 = $('#tblbattery tr').length - 1;

    for (var w = 1; w <= lastRow1; w++) {
        var date = $("#txt_batterydt" + w).val();
        var amount = $("#txt_batteryamt" + w).val();

        if (date == "") {
            alert('Please select Date at row :' + w + '');
            return false;
        }

        if (amount == "") {
            alert('Please Enter amount at row :' + w + '');
            return false;
        }
    }

    var html = "<tr><td>" + (lastRow1 + 1) + "</td>";
    var html1 = "<td><div class='input-group'><input type='text' class='form-control input-sm datepicker-dropdown ' id='txt_batterydt" + (lastRow1 + 1) + "' readonly /><span class='input-group-btn'><button class='btn btn-danger input-sm' type='button'><i class='fa fa-calendar'></i></button></span></div></td>";
    var html5 = "<td><input class='form-control input-sm' style='text-align:right' type='text' id='battery_particulars" + (lastRow1 + 1) + "' ></input></td>";
    var html2 = "<td><input class='form-control input-sm' style='text-align:right' type='text' id='txt_batteryamt" + (lastRow1 + 1) + "' onkeypress='chknumeric(this.id);' onblur='checkb(" + (lastRow1 + 1) + ");' ></input></td>";
    var html3 = "<td class='add_battery'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i></td>";
    var html4 = "<td class='delete_battery'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-trash'></i></td></tr>";
    var htmlcontent = $(html + "" + html1 + "" + html5 + "" + html2 + "" + html3 + "" + html4);
 
    $('#tblbattery tr:last').after(htmlcontent);
    $(".datepicker-dropdown").datepicker({ format: 'dd-M-yyyy', todayBtn: 'linked', autoclose: true, endDate: new Date() })

}

$("#tblbattery").on('click', '.delete_battery', function () {
    $(this).closest('tr').remove();
});


function checkb(id) {
    var date = $("#txt_batterydt" + id).val();
    var adid = $("#txt_Username").val();
    var amt = $("#txt_batteryamt" + id).val();

    Car_Expense_MOdification.checkb(date, adid, amt, id, OnBSuccess);
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
}