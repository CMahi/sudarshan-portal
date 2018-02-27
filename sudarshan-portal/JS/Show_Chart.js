
function view_chart()
{
    //var vcode = "0000007672";
    var vcode = $("#txt_UserName").val();
    NewChart.BindChart(vcode, callback_Inco);
    
}


function callback_Inco(response) {

    //var chartType = $("#DropDownList1").val();
    var str = response.value;
    var allData = str.split("$$");


    var plants = allData[0].split("||");
    var strData = allData[1].split("||");
    var plantData1 = allData[2].split(";;");

    var yourDataPoints = new Array();
    var tagNames = [strData[0], strData[1], strData[2], strData[3], strData[4], strData[5], strData[6], strData[7], strData[8], strData[9], strData[10], strData[11]];

    /*****************************************************************************************************/

    for (var i = 0; i < plantData1.length ; i++) {
        var p_name = plants[i];
        var str_Cnt = plantData1[i].split("||");
        var tagRptTotalCnt = [parseInt(str_Cnt[0]), parseInt(str_Cnt[1]), parseInt(str_Cnt[2]), parseInt(str_Cnt[3]), parseInt(str_Cnt[4]), parseInt(str_Cnt[5]), parseInt(str_Cnt[6]), parseInt(str_Cnt[7]), parseInt(str_Cnt[8]), parseInt(str_Cnt[9]), parseInt(str_Cnt[10]), parseInt(str_Cnt[11])];
        
        yourDataPoints.push({

            name: "Plant",
            type: "stackedColumn",
            legendText: p_name,
            showInLegend: "false",
           
            dataPoints: [

                    { y: tagRptTotalCnt[0], label: tagNames[0] },
                    { y: tagRptTotalCnt[1], label: tagNames[1] },
                    { y: tagRptTotalCnt[2], label: tagNames[2] },
                    { y: tagRptTotalCnt[3], label: tagNames[3] },
                    { y: tagRptTotalCnt[4], label: tagNames[4] },
                    { y: tagRptTotalCnt[5], label: tagNames[5] },
                    { y: tagRptTotalCnt[6], label: tagNames[6] },
                    { y: tagRptTotalCnt[7], label: tagNames[7] },
                    { y: tagRptTotalCnt[8], label: tagNames[8] },
                    { y: tagRptTotalCnt[9], label: tagNames[9] },
                    { y: tagRptTotalCnt[10], label: tagNames[10] },
                    { y: tagRptTotalCnt[11], label: tagNames[11] }

            ]
        }

        );
    }
    /*****************************************************************************************************/

    if (plantData1.length <2) {
        CanvasJS.addColorSet("greenShades",
           [
           "#3EA0DD"
           ]);
    }
    else {
        CanvasJS.addColorSet("greenShades",
     [//colorSet Array
     "#4661EE",
     "#EC5657",
     "#1BCDD1",
     "#8FAABB",
     "#B08BEB",
     "#3EA0DD",
     "#F5A52A",
     "#23BFAA",
     "#FAA586",
     "#EB8CC6",
    ]); 
    }


    var chart = new CanvasJS.Chart("chartContainer",
    {
        colorSet: "greenShades",

        animationEnabled: true,
        backgroundColor: "#DEDEDE",
        title: {
            text: "Monthwise & Plantwise Po's",
        },
        
        data: yourDataPoints
    });

    chart.render();

}

/*
function callback_Inco(response) {
    
    var str = response.value;
    var allData = str.split("$$");


    var plants = allData[0].split("||");
    var strData = allData[1].split("||");
    var plantData1 = allData[2].split("||");
    var plantData2 = allData[3].split("||");

    var yourDataPoints = new Array();
    var tagRptTotalCnt = [parseInt(plantData1[0]), parseInt(plantData1[1]), parseInt(plantData1[2]), parseInt(plantData1[3]), parseInt(plantData1[4]), parseInt(plantData1[5]), parseInt(plantData1[6]), parseInt(plantData1[7]), parseInt(plantData1[8]), parseInt(plantData1[9]), parseInt(plantData1[10]), parseInt(plantData1[11])];
    var tagRptTotalCnt1 = [parseInt(plantData2[0]), parseInt(plantData2[1]), parseInt(plantData2[2]), parseInt(plantData2[3]), parseInt(plantData2[4]), parseInt(plantData2[5]), parseInt(plantData2[6]), parseInt(plantData2[7]), parseInt(plantData2[8]), parseInt(plantData2[9]), parseInt(plantData2[10]), parseInt(plantData2[11])];
    var tagNames = [strData[0], strData[1], strData[2], strData[3], strData[4], strData[5], strData[6], strData[7], strData[8], strData[9], strData[10], strData[11]];

    //for (var i = 0; i < tagRptTotalCnt.length ; i++) {
        yourDataPoints.push({

          //y: tagRptTotalCnt[i],
           // label: tagNames[i]
            type: "stackedColumn",
            dataPoints: [
                      
                    { y: tagRptTotalCnt[0], label: tagNames[0] },
                    { y: tagRptTotalCnt[1], label: tagNames[1] },
                    { y: tagRptTotalCnt[2], label: tagNames[2] },
                    { y: tagRptTotalCnt[3], label: tagNames[3] },
                    { y: tagRptTotalCnt[4], label: tagNames[4] },
                    { y: tagRptTotalCnt[5], label: tagNames[5] },
                    { y: tagRptTotalCnt[6], label: tagNames[6] },
                    { y: tagRptTotalCnt[7], label: tagNames[7] },
                    { y: tagRptTotalCnt[8], label: tagNames[8] },
                    { y: tagRptTotalCnt[9], label: tagNames[9] },
                    { y: tagRptTotalCnt[10], label: tagNames[10] },
                    { y: tagRptTotalCnt[11], label: tagNames[11] }

            ]
        },
        {
            type: "stackedColumn",
            dataPoints: [
                    { y: tagRptTotalCnt1[0], label: tagNames[0] },
                    { y: tagRptTotalCnt1[1], label: tagNames[1] },
                    { y: tagRptTotalCnt1[2], label: tagNames[2] },
                    { y: tagRptTotalCnt1[3], label: tagNames[3] },
                    { y: tagRptTotalCnt1[4], label: tagNames[4] },
                    { y: tagRptTotalCnt1[5], label: tagNames[5] },
                    { y: tagRptTotalCnt1[6], label: tagNames[6] },
                    { y: tagRptTotalCnt1[7], label: tagNames[7] },
                    { y: tagRptTotalCnt1[8], label: tagNames[8] },
                    { y: tagRptTotalCnt1[9], label: tagNames[9] },
                    { y: tagRptTotalCnt1[10], label: tagNames[10] },
                    { y: tagRptTotalCnt1[11], label: tagNames[11] }
       
            ]
       }
       
        );
    
   
    var chart = new CanvasJS.Chart("chartContainer",
    {
        colorSet: "greenShades",

        animationEnabled: true,
        backgroundColor: "#DEDEDE",
        title: {
            text: "Monthwise & Plantwise Po's"
        },
        data: yourDataPoints
    });

    chart.render();

}
*/