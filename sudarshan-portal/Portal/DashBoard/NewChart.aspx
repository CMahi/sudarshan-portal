<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewChart.aspx.cs" Inherits="NewChart" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <script type="text/javascript">
            window.onload = function () {
                view_chart();                
            }
  </script>
    	<meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" name="viewport" />

	
	<!-- ================== BEGIN BASE CSS STYLE ================== -->
	<link href="http://fonts.googleapis.com/css?family=Nunito:400,300,700" rel="stylesheet" id="fontFamilySrc" />
	<link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
	<link href="../../assets/css/style.min.css" rel="stylesheet" />
	<!-- ================== END BASE CSS STYLE ================== -->
 <script src="canvasjs.min.js"></script>
 <script src="../../JS/Show_Chart.js"></script>
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
                          <div class="col-md-6">
                              <asp:TextBox ID="txt_UserName" runat="server" CssClass="form-control" placeholder="Vendor Code"  onkeyup="view_chart();"></asp:TextBox>
                          </div>
                             <div class="col-md-6">
                                 <asp:Button ID="btnCancel" runat="server" Text="Back" class="btn btn-sm btn-inverse" OnClick="btnCancel_Click" Font-Bold="true"/>
                          </div>
						</div>
                        <h3 class="panel-title"><b>Summary Report</b></h3>
                    </div>
                </div>
            </div>
            <div class="col-md-12" style="margin-top: -8px;">
                  <!-- begin row -->
                <div class="row">
                     <!-- begin col-4 -->
                    <div class="col-md-12">
                        <!-- begin panel -->
                        <div class="panel p-20">
                            <h5 class="m-t-0 m-b-15" style="color:grey">PO Summary</h5>
                            <hr />
                                <div id="chartContainer" class="table-responsive" style="height: 225px;">
                                </div> 
                        </div>
                        <!-- end panel -->
                    </div>
                    <!-- end col-4 -->
                
                   
                      </div>
                <!-- end row -->
             
            </div>
        </div>
         <!-- ================== BEGIN BASE JS ================== -->
	<script src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
	<script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
	<!-- ================== END BASE JS ================== -->
    </form>
</body>
</html>
