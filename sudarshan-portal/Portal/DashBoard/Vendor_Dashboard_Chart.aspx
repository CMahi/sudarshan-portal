<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Vendor_Dashboard_Chart.aspx.cs" Inherits="Vendor_Dashboard_Chart" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <meta charset="utf-8" />
	<title>SUDARSHAN</title>
	<meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" name="viewport" />

	
	<!-- ================== BEGIN BASE CSS STYLE ================== -->
	<link href="http://fonts.googleapis.com/css?family=Nunito:400,300,700" rel="stylesheet" id="fontFamilySrc" />
	<link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
	<link href="../../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
	<link href="../../assets/css/style.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/bootstrap-calendar/css/bootstrap_calendar.css" rel="stylesheet" />
	<link href="../../assets/plugins/DataTables/media/css/dataTables.bootstrap.min.css" rel="stylesheet" />
	<link href="../../assets/plugins/DataTables/extensions/Responsive/css/responsive.bootstrap.min.css" rel="stylesheet" />
       <link href="../../CSS/loading.css" rel="stylesheet" />
	<!-- ================== END BASE CSS STYLE ================== -->

    	<!-- ================== BEGIN BASE JS STYLE ================== -->
     <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="//cdn.jsdelivr.net/excanvas/r3/excanvas.js" type="text/javascript"></script>
    <script src="//cdn.jsdelivr.net/chart.js/0.2/Chart.js" type="text/javascript"></script>
    <!--<script src="../../JS/Chart_Report.js"></script>-->
    	<!-- ================== END BASE JS STYLE ================== -->

</head>
<body>
    <form id="form1" runat="server">
         <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
       
          <div class="row" style="margin-top:1%">
            <div class="col-md-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                         <div class="btn-group pull-right" style="margin-top:-5px;">
                            <asp:Button ID="btnCancel" runat="server" Text="Back" class="btn btn-sm btn-inverse" OnClick="btnCancel_Click" Font-Bold="true"/>
						</div>
                        <h3 class="panel-title"><b>Vendor Summary Report</b></h3>
                    </div>
                </div>
            </div>
              <div class="col-md-12">
                  <div class="col-md-10">&nbsp;</div>
                  <div class="col-md-2" style="text-align:right">
                      <a href="#NewPO" data-toggle="modal">New POs(<asp:Label ID="lblCount" runat="server" Text="0"></asp:Label>)</a>
                      
                      <a  href='#paymentterm' role='button' data-toggle='modal' >Reports</a>
                  </div>
              </div>
         <div class="modal fade" id="paymentterm" >
		  <div class="modal-dialog"  style="height:auto; width:98%; margin-left:1%">
			<div class="modal-content" style="background-color:ButtonFace">
				
				<div class="col-md-12">
			<div class="panel" style="background-color:#717b85">
				<ul class="nav nav-tabs nav-tabs-danger nav-justified">
					<li class="active"><a href="#tab1" data-toggle="tab"><b>Pending Deliveries</b></a></li>
					<li class=""><a href="#tab2" data-toggle="tab"><b>Material Recipet Status</b></a></li>
					<li class=""><a href="#tab3" data-toggle="tab"><b>Pending Payment</b></a></li>
				</ul>
				<div class="tab-content m-b-0">
					 <div class="tab-pane fade active in" id="tab1">
						<div class="panel pagination-danger">
						   <div id="div_PendingDeliveries" runat="server" class="table-responsive"></div>
						</div>
					 </div>
					 <div class="tab-pane fade" id="tab2">
						<div class="panel pagination-danger">
				          <div id="div_MaterialRecipet_Status" runat="server" class="table-responsive"></div>
						</div>
					 </div>
					 <div class="tab-pane fade" id="tab3">
						<div class="panel pagination-danger">
							<div id="div_PendingPayment" runat="server" class="table-responsive"></div>
                        <hr />
                        <div class="row">
                            <div class="col-md-2">
                                <span><b>RJ : </b></span><span>Rejected</span>
                            </div>
                            <div class="col-md-2">
                                <span><b>TR : </b></span><span>Transition</span>
                            </div>
                            <div class="col-md-2">
                                <span><b>GRN : </b></span><span>GRN Complete</span>
                            </div>
                            <div class="col-md-2">
                                <span><b>QC : </b></span><span>QC-Approved</span>
                            </div>
                            <div class="col-md-2">
                                <span><b>QCD : </b></span><span>QC-Approved under Deviation</span>
                            </div>
                           
                        </div>
                         <div class="row">
                             <div class="col-md-2">
                                <span><b>BB : </b></span><span>Bill Booked</span>
                            </div>
                            <div class="col-md-2">
                                <span><img src="../../images/2.png" /><b> : </b></span><span>Completed</span>
                            </div>
                            <div class="col-md-2">
                                <span><img src="../../images/1.png" /><b> : </b></span><span>Not Completed</span>
                            </div>
                            <div class="col-md-2">
                                <span><img src="../../images/0.png" /><b> : </b></span><span>Rejected</span>
                            </div>
                            <div class="col-md-2">
                                <span><img src="../../images/3.png" /><b> : </b></span><span>QC-Approved under Deviation</span>
                            </div>
                           
                        </div>
						</div>
					 </div>
				</div>
			</div>
		</div>
	</div>
           
                          
				</div>
                
				<div class="modal-footer">
					<a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal" >Close</a>
				</div>
			</div>
		</div>
	</div>
              <div class="col-md-12">&nbsp;</div>
            <div class="col-md-12" style="margin-top: -8px;">
                   

                  <!-- begin row -->
                <div class="row">
                     <!-- begin col-4 -->

                    <div class="col-md-6">
                        <!-- begin panel -->
                        <div class="panel p-20">
                              <div class="btn-group pull-right" style="margin-top:-5px;">
                                  <b>Chart Type : </b><asp:DropDownList ID="ddlPO" runat="server" onchange="LoadPOCount()">
                                      <asp:ListItem Value="1">Pie Chart</asp:ListItem>
                                      <asp:ListItem Value="2">Doughnut Chart</asp:ListItem>
                                  </asp:DropDownList>
						    </div>
                              <h5 class="m-t-0 m-b-15" style="color:grey">Total PO Summary</h5>
                            <hr />
                            <div id="bar-chart" class="height-xs">
                                       <div class="col-md-9">
                                     <div id="dvPOCnt">
                                            </div>
                                </div> 
                                  <div class="col-md-3">
                                     <div id="dvPOLegend">
                                            </div>
                                </div> 
                            </div>
                        </div>
                        <!-- end panel -->
                    </div>
                
                    <!-- end col-4 -->
                    <!-- begin col-4 -->
                    <div class="col-md-6">
                        <!-- begin panel -->
                        <div class="panel p-20">
                             <div class="btn-group pull-right" style="margin-top:-5px;">
                                <b>Chart Type : </b> 
                                 <asp:DropDownList ID="ddlInvoice" runat="server" onchange="LoadChart()">
                                      <asp:ListItem Value="1">Pie Chart</asp:ListItem>
                                      <asp:ListItem Value="2">Doughnut Chart</asp:ListItem>
                                  </asp:DropDownList>
						    </div>
                            <h5 class="m-t-0 m-b-15" style="color:grey">Invoice Status Summary</h5>
                            <hr />
                            <div id="line-chart" class="height-xs">
                                <div class="col-md-9">
                                     <div id="dvChart">
                                            </div>
                                </div> 
                                  <div class="col-md-3">
                                     <div id="dvLegend">
                                            </div>
                                </div> 
                                
                            </div>
                        </div>
                        <!-- end panel -->
                    </div>
                    <!-- end col-4 -->
                   
                      </div>
                <!-- end row -->
                        
                   
                <div class="row">&nbsp;</div>
                    <!-- begin row -->
                <div class="row" style="display:none">
                    <!-- begin col-4 -->
                    <div class="col-md-6">
                        <!-- begin panel -->
                        <div class="panel p-20">
                             <div class="btn-group pull-right" style="margin-top:-5px;">
                                 <b>Chart Type : </b> <asp:DropDownList ID="ddlPoAmt" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPO_SelectedIndexChanged">
                                      <asp:ListItem Value="1">Pie Chart</asp:ListItem>
                                      <asp:ListItem Value="2">Doughnut Chart</asp:ListItem>
                                  </asp:DropDownList>
						    </div>
                            <h5 class="m-t-0 m-b-15" style="color:grey">PO Amount Summary</h5>
                            <hr />
                            <div id="pie-chart" class="height-xs">
                                  <div class="col-md-9">
                                     <div id="dvAmtCnt">
                                     </div>
                                </div> 
                                  <div class="col-md-3">
                                     <div id="dvAmtLegend">
                                     </div>
                                </div> 
                            </div>
                        </div>
                        <!-- end panel -->
                    </div>
                    <!-- end col-4 -->

              </div>
            </div>
        </div>
            
            <div class="modal fade" id="NewPO">
            <div class="modal-dialog">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true" >×</button>
                        <h4 class="modal-title">New POs :</h4>
                    </div>

                    <div class="modal-body" style="overflow-y: scroll; height: auto;">
                     <asp:UpdatePanel ID="upModal" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                              <div id="div_po" runat="server" style="overflow-y: scroll; height: 296px"></div>
                           
                              <div id="div_remark" runat="server"><label id="remark" style="vertical-align: top;">Remark : </label><textarea rows='3' id='txt_remark' cols='60' runat='server' maxlength='200'></textarea></div>
                                <br /><div class="modaltestfooter">
                              
                                    <asp:Button ID="btn_Add" runat="server" Text="Acknowledge"  class="btn btn-sm btn-danger" OnClientClick="return editdatails();" OnClick="btnsubmit_click"></asp:Button>
                                    <asp:Button ID="btn_sendback" runat="server" Text="Send Back" class="btn btn-sm btn-danger" OnClientClick="return editsendbacktails();" OnClick="btn_sendback_click"></asp:Button>
                                    <a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>
                                    <br />
                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                </div>
                            </ContentTemplate>

                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_Add" />
                            </Triggers>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_sendback" />
                            </Triggers>
                        </asp:UpdatePanel>

                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upModal">
                            <ProgressTemplate>
                                <div class="modal1">
                                    <div class="center1">
                                        <img alt="Processing" id="img_Progress" src="../../images/loading_transparent.gif" />
                                    </div>
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>

                        <div id="conditionButton" style="display: none">
                            <asp:TextBox ID="txt_DomainName" runat="server" Width="0.5px"></asp:TextBox>
                            <asp:TextBox ID="txt_procedure" runat="server" Width="0.5px"></asp:TextBox>
                            <asp:TextBox ID="txt_SearchString" runat="server" Width="0.5px" Text="%"></asp:TextBox>
                            <asp:TextBox ID="txtCondition" runat="server" Width="0.5px"></asp:TextBox>
                            <asp:TextBox ID="txtParameterID" runat="server" Width="1px"></asp:TextBox>
                            <asp:TextBox ID="txtCreatedBy" runat="server" Width="1px"></asp:TextBox>
                            <asp:TextBox ID="txt_PONO" runat="server" Width="1px"></asp:TextBox>
                            <asp:TextBox ID="txt_ProjectXml" runat="server" Width="1px"></asp:TextBox>
                            <asp:TextBox ID="txt_CREATED_BY" runat="server" Width="1px"></asp:TextBox>
                            <asp:TextBox ID="txt_PO_VALUE" runat="server" Width="1px"></asp:TextBox>
                             <asp:TextBox ID="txt_GV_VALUE" runat="server" Width="1px"></asp:TextBox>
                            <asp:TextBox ID="txt_check_PO_Nos" runat="server" Width="1px"></asp:TextBox>
                            <asp:TextBox ID="txt_dt_count" runat="server" Width="1px"></asp:TextBox>
                            <asp:TextBox ID="txt_ack_value" runat="server" Width="1px"></asp:TextBox>
                            <asp:TextBox ID="app_Path" runat="server" Width="1px"></asp:TextBox>
                            <asp:TextBox ID="txt_sendback_po" runat="server" Width="1px"></asp:TextBox>
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>
            
        <div style="display:none">
            <asp:TextBox ID="txt_Username" runat="server"></asp:TextBox>
        </div>
    <!-- ================== BEGIN BASE JS ================== -->
	   <script src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>	
        <script src="../../assets/plugins/jquery/jquery-migrate-1.1.0.min.js"></script>
        <script src="../../assets/plugins/jquery-ui/ui/minified/jquery-ui.min.js"></script>
        <script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>

        <script src="../../assets/plugins/bootstrap-calendar/js/bootstrap_calendar.min.js"></script>
        <script src="../../assets/plugins/DataTables/media/js/jquery.dataTables.js"></script>
        <script src="../../assets/plugins/DataTables/media/js/dataTables.bootstrap.min.js"></script>
        <script src="../../assets/plugins/DataTables/extensions/Responsive/js/dataTables.responsive.min.js"></script>
        <script src="../../assets/js/page-table-manage-responsive.demo.min.js"></script>
        <script src="../../assets/js/demo.min.js"></script>
        <script src="../../assets/js/apps.min.js"></script>
        <script src="../../assets/plugins/slimscroll/jquery.slimscroll.min.js"></script>
   
        </form>

	<!-- ================== END BASE JS ================== -->
</body>
    <script language="javascript" type="text/javascript">
      
        $(document).ready(function () {
            $("#div_remark").hide();
            $("#tbl_PendingDeliveries").DataTable();
            $("#tbl_MaterialRecipet_Status").DataTable();
            $("#tbl_Status_Material_ReturnBack").DataTable();
            $("#tbl_PendingPayment").DataTable();            
        });
        $("#btn_sendback").click(function () {
            $("#div_remark").show();

        });
        $("#btnsubmit_click").click(function () {
            $("#div_remark").hide();

        });
        function editdatails() {
          
            $("#div_remark").hide();
            document.getElementById("txt_PONO").value = "";
            document.getElementById("txt_check_PO_Nos").value = "";
            var tbl = document.getElementById("mytable");
            var rowCount = $('#mytable tr').length;
            
            var vals = "";
            var j = 1;
            for (var i = 1; i < rowCount; i++) {               
                var checkboxes = document.getElementById("open_po" + i + "");
                if (checkboxes.checked) {                                     
                    document.getElementById("txt_PONO").value += $("#txt_pkid" + i).val() + ";";
                    document.getElementById("txt_ProjectXml").value += $("#txt_date" + i).val() + ";";
                    if ($("#txt_gross" + i).val() != "0") {
                        document.getElementById("txt_GV_VALUE").value += $("#txt_gross" + i).val() + ";";
                    }
                    else {
                        document.getElementById("txt_GV_VALUE").value += 0 + ";";
                    }
                    vals += "," + checkboxes.value;
                    document.getElementById("txt_check_PO_Nos").value = j++;

                }
            }
            var updateProgress = $get("<%= UpdateProgress1.ClientID %>");
            updateProgress.style.display = "block";
            return true;
        }

        function editsendbacktails() {
            $("#div_remark").show();
            var rem = document.getElementById("txt_remark").value;
            if (rem == "") {
                alert("Please Enter Remark");
                return false;
            }
            else {
                document.getElementById("txt_PONO").value = "";
                document.getElementById("txt_check_PO_Nos").value = "";
                var tbl = document.getElementById("mytable");
                var rowCount = $('#mytable tr').length;
                var vals = "";
                var j = 1;
                for (var i = 1; i < rowCount; i++) {
                    var checkboxes = document.getElementById("open_po" + i + "");
                    if (checkboxes.checked) {
                        document.getElementById("txt_PONO").value += $("#txt_pkid" + i).val() + ";";                       
                        document.getElementById("txt_ProjectXml").value += $("#txt_date" + i).val() + ";";
                        if ($("#txt_gross" + i).val() != "0") {
                            document.getElementById("txt_GV_VALUE").value += $("#txt_gross" + i).val() + ";";
                        }
                        else {
                            document.getElementById("txt_GV_VALUE").value += 0 + ";";
                        }
                        vals += "," + checkboxes.value;
                        document.getElementById("txt_check_PO_Nos").value = j++;

                    }
                }
               var updateProgress = $get("<%= UpdateProgress1.ClientID %>");
                updateProgress.style.display = "block";
               
            }
           
            return true;
           
        }
        function ShowMe() {
            $('#div_remark').hide();
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
        var po_val = $("#encrypt_po" + (index)).val();
        window.open(app_path + '/Common/ViewDocument.aspx?pono=' + po_val + '&type=PO', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
    }
    catch (exception) {
    }
}
</script>
          
</html>
