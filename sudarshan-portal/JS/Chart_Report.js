$(function () {
    LoadPOCount();
    LoadChart();
    LoadPOAmount();
});


function LoadChart() {
    var vcode = $("#txt_Username").val();
    Vendor_Dashboard_Chart.GetChart(vcode,2, OnInvoice);
}

function OnInvoice(response) {
    //var chartType = 1;
    var chartType = parseInt($("#ddlInvoice").val());
    $("#dvChart").html("");
    $("#dvLegend").html("");
    var data = eval(response.value);
    var el = document.createElement('canvas');
    $("#dvChart")[0].appendChild(el);

    var ctx = el.getContext('2d');
    var userStrengthsChart;
    switch (chartType) {
        case 1:
            userStrengthsChart = new Chart(ctx).Pie(data);
            break;
        case 2:
            userStrengthsChart = new Chart(ctx).Doughnut(data);
            break;
    }
    for (var i = 0; i < data.length; i++) {
        var div = $("<div />");
        div.css("margin-bottom", "10px");
        div.html("<span style = 'display:inline-block;height:10px;width:10px;background-color:" + data[i].color + "'></span> " + data[i].text + " - (" + data[i].value + ")");
        $("#dvLegend").append(div);
    }
}

function LoadPOCount() {
    var vcode = $("#txt_Username").val();
    Vendor_Dashboard_Chart.GetChart(vcode, 1, OnPOCount);
}

function OnPOCount(response) {
    //var chartType = 1;
    var chartType = parseInt($("#ddlPO").val());
    $("#dvPOCnt").html("");
    $("#dvPOLegend").html("");
    var data = eval(response.value);
    var el = document.createElement('canvas');
    $("#dvPOCnt")[0].appendChild(el);

    var ctx = el.getContext('2d');
    var userStrengthsChart;
    switch (chartType) {
        case 1:
            userStrengthsChart = new Chart(ctx).Pie(data);
            break;
        case 2:
            userStrengthsChart = new Chart(ctx).Doughnut(data);
            break;
        
    }
    for (var i = 0; i < data.length; i++) {
        var div = $("<div />");
        div.css("margin-bottom", "10px");
        div.html("<span style = 'display:inline-block;height:10px;width:10px;background-color:" + data[i].color + "'></span> " + data[i].text + " - (" + data[i].value + ")");
        $("#dvPOLegend").append(div);
    }
}

function LoadPOAmount() {
    var vcode = $("#txt_Username").val();
    Vendor_Dashboard_Chart.GetChart(vcode, 3, OnPOAmount);
}

function OnPOAmount(response) {
    //var chartType = 1;
    var chartType = parseInt($("#ddlPoAmt").val());
    $("#dvAmtCnt").html("");
    $("#dvAmtLegend").html("");
    var data = eval(response.value);
    var el = document.createElement('canvas');
    $("#dvAmtCnt")[0].appendChild(el);

    var ctx = el.getContext('2d');
    var userStrengthsChart;
    switch (chartType) {
        case 1:
            userStrengthsChart = new Chart(ctx).Pie(data);
            break;
        case 2:
            userStrengthsChart = new Chart(ctx).Doughnut(data);
            break;
    }
    for (var i = 0; i < data.length; i++) {
        var div = $("<div />");
        div.css("margin-bottom", "10px");
        div.html("<span style = 'display:inline-block;height:10px;width:10px;background-color:" + data[i].color + "'></span> " + data[i].text + " - (" + data[i].value+")");
        $("#dvAmtLegend").append(div);
    }
}


/********************************************************************Arti Madam**********************************************************************************/
    function editdatails() {
        document.getElementById("txt_PONO").value = "";
        document.getElementById("txt_check_PO_Nos").value = "";
        var tbl = document.getElementById("mytable");
        var rowCount = $('#mytable tr').length;
        var vals = "";
        var j = 1;
        for (var i = 1; i < rowCount; i++) {

            var checkboxes = document.getElementById("open_po" + i + "");
            if (checkboxes.checked) {


                document.getElementById("txt_PONO").value += tbl.rows[i].cells[1].innerText + ";";
                document.getElementById("txt_ProjectXml").value += tbl.rows[i].cells[2].innerText + ";";
                if (tbl.rows[i].cells[3].innerText != "0") {
                    document.getElementById("txt_PO_VALUE").value += tbl.rows[i].cells[3].innerText + ";";
                }
                else {
                    document.getElementById("txt_PO_VALUE").value += 0 + ";";
                }

                vals += "," + checkboxes.value;
                document.getElementById("txt_check_PO_Nos").value = j++;

            }
        }
        var updateProgress = $get("<%= UpdateProgress1.ClientID %>");
        updateProgress.style.display = "block";
    }

var count = $("#txt_dt_count").val();
$("#div_NewPO").click(function () {
    if (count == 0) {
        $('div#NewPO').attr('id', 'highlight')
        alert("New PO's are not Found");
    } else {
        $("#NewPO").show();
    }
});
function viewData(index) {
    try {
        var app_path = $("#app_Path").val();
        var po_val = $("#encrypt_po_" + (index)).val();
        window.open(app_path + '/Common/ViewDocument.aspx?pono=' + po_val + '&type=PO', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
    }
    catch (exception) {
    }
}

