<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Master.aspx.cs" Inherits="Portal_SCIL_Sub_Menu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta charset="utf-8" />
	<title>SUDARSHAN</title>
	<meta content="width=device-width, initial-scale=1.0" name="viewport" />
	<meta content="" name="description" />
	<meta content="" name="author" />
	
	<!-- ================== BEGIN BASE CSS STYLE ================== -->
	<link href="assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
	<link href="assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
	<link href="assets/css/style.min.css" rel="stylesheet" />
	<!-- ================== END BASE CSS STYLE ================== -->
</head>
<%--<body data-scrollbar="true">--%>
    <body>
    <form id="form1" runat="server" class="form-horizontal">
		<div class="row" style="margin-top:10px">
			<div class="col-md-12">
				<div class="panel panel-danger">
					<div class="panel-heading">
                         <div class="btn-group pull-right" style="margin-top:-5px;">
                            <asp:Button ID="btnCancel" runat="server" Text="Back" class="btn btn-sm btn-inverse" OnClick="btnCancel_Click" Font-Bold="true"/>
						</div>
						<h3 class="panel-title"><b>
                            <asp:Label ID="lblHeader" runat="server" Text=""></asp:Label>
                        </b></h3>
					</div>					
				</div>
			
            <div style="text-align:center" >
                <asp:DataList ID="dlMenu" runat="server" OnItemDataBound="dlMenu_ItemDataBound" RepeatColumns="3" RepeatDirection="Horizontal" RepeatLayout="Flow" Width="100%" HorizontalAlign="Justify" >
                    <ItemTemplate>
                 
                   <div class="col-md-3" style="text-align:left" >
				    <div class="panel panel-grey">
					    <div class="panel-heading">
                            <asp:Label ID="lblUser" Text='<%# Eval("obj_id") %>' runat="server" style="display:none"/>
						    <h4 class="panel-title"><%# Eval("obj_name") %></h4>
					    </div>
					    <div class="panel-body">
						    <div style="height:185px; overflow:auto">
							    <ul class="feeds">
                                     <asp:Repeater ID="dlRpt" runat="server" OnItemCommand="Repeater1_ItemCommand">
                                         <ItemTemplate>
                                             <li>
                                                  <a href="#" onclick="RedirectToPages('<%#Eval("obj_url") %>')" title='<%# Eval("obj_name") %>'>
										                                <div class="icon bg-danger-light"><i class="fa fa-file-text"></i></div>
										                                <div class="desc"><%# Eval("obj_name") %></div>
                                                            </a>
								            </li>
                                         </ItemTemplate>
                                     </asp:Repeater>
							    </ul>
						    </div>	
					    </div>
				    </div>
			    </div>
                    </ItemTemplate>
                </asp:DataList>
			</div>
			</div>
		</div>
	<div style="display:none">
        <asp:TextBox ID="txtMId" runat="server"></asp:TextBox>
          <asp:TextBox ID="txt_Path" runat="server"></asp:TextBox>
	</div>
	</form>

    <!-- ================== BEGIN BASE JS ================== -->
    <script src="assets/plugins/jquery/jquery-2.1.1.min.js"></script>
	<script src="assets/plugins/bootstrap/js/bootstrap.min.js"></script>
	<script src="assets/plugins/slimscroll/jquery.slimscroll.min.js"></script>
    <script src="assets/js/apps.min.js"></script>
	<!-- ================== END PAGE LEVEL JS ================== -->
    <script>
        $(document).ready(function () {
            App.init();
        });

        function RedirectToPages(url) {
            var str_path = $("#txt_Path").val();
            window.location.href = (str_path + "/" + $("#lblHeader").html() + url);
        }
	</script>

</body>
</html>
