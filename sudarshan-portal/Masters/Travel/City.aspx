﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="City.aspx.cs" Inherits="City" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   	<meta charset="utf-8" />
	<title>City Master</title>
	<link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
	<link href="../../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
	<link href="../../assets/css/style.min.css" rel="stylesheet" />

    <link href="../../assets/plugins/bootstrap-calendar/css/bootstrap_calendar.css" rel="stylesheet" />
    <link href="../../assets/plugins/DataTables/media/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/DataTables/extensions/Responsive/css/responsive.bootstrap.min.css" rel="stylesheet" />

            <script src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
        <script src="../../assets/plugins/jquery/jquery-migrate-1.1.0.min.js"></script>
        <script src="../../assets/plugins/jquery-ui/ui/minified/jquery-ui.min.js"></script>
        <script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>

        <script src="../../assets/plugins/DataTables/media/js/jquery.dataTables.js"></script>
        <script src="../../assets/plugins/DataTables/media/js/dataTables.bootstrap.min.js"></script>
        <script src="../../assets/plugins/DataTables/extensions/Responsive/js/dataTables.responsive.min.js"></script>
        <script src="../../assets/js/page-table-manage-responsive.demo.min.js"></script>
        <script src="../../assets/js/demo.min.js"></script>
        <script src="../../assets/js/apps.min.js"></script>
        <script src="../../assets/plugins/slimscroll/jquery.slimscroll.min.js"></script>


    <script src="../../JS/City.js"></script>
</head>
<body>
    <form id="form1" runat="server">
           <div class="row" style="margin-top:10px" id="div_AddCountryDetails">
			<div class="col-lg-12">
				<div class="panel panel-danger">
					<div class="panel-heading">
                        <div class="panel-heading-btn">
							 <a href="javascript:;" class="btn btn-sm btn-success m-b-3" onclick="show_List()" >View All</a>
						</div>
						<h4 class="panel-title"><b>City</b></h4>
					</div>
					<div class="panel-body">
                        <div class="col-md-12">
						<div class="form-horizontal bordered-group">
							<div class="form-group">
								<label class="col-md-2 control-label"><b>Country</b></label>
								<div class="col-md-2 ui-sortable">
                                    <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
								</div>
								<label class="col-md-1 control-label"><b>City</b></label>
								<div class="col-md-2 ui-sortable">
                                    <asp:TextBox ID="txt_City_Name" runat="server" CssClass="form-control"></asp:TextBox>
								</div>
                                <label class="col-md-2 control-label"><b>Classification</b></label>
								<div class="col-md-2 ui-sortable">
                                    <asp:DropDownList ID="ddl_Classify" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0">--Select One--</asp:ListItem>
                                        <asp:ListItem Value="A">Metro</asp:ListItem>
                                        <asp:ListItem Value="B">Mini Metro</asp:ListItem>
                                        <asp:ListItem Value="C">Other</asp:ListItem>
                                    </asp:DropDownList>
								</div>
								</div>
							</div>
                        </div>
						</div>
                    <div class="modal-footer">
								<div class="col-md-3"></div>
								<div class="col-md-6" style="text-align:center">
                                    <asp:Button ID="btn_AddNew" runat="server" Text="Add New" class="btn btn-grey btn-rounded m-b-5" OnClick="btn_AddNew_Click" OnClientClick="return checkControls();"/>
                                    <asp:Button ID="btnClear" runat="server" Text="Clear" class="btn btn-danger btn-rounded m-b-5" OnClick="btnClear_Click"/>
								</div>
                        <div class="col-md-3"></div>
							</div>
					</div>
				</div>
			</div>
        
           <div class="modal fade" id="CountryPanel">
		<div class="modal-dialog"  style="height:auto; width:50%; margin-left:25%">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><font color="white">Update City Details </font></h4>
                </div>

                <div class="modal-body" id="div_header" runat="server">
                    <div class="col-md-12">
                        <div class="col-md-2"></div>
                        <div class="col-md-3">
                            <b>Country</b>
                        </div>
                        <div class="col-md-5">
                            <asp:DropDownList ID="ddlEditCountry" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                          <div class="col-md-2"></div>
                    </div>
                    <div class="row">&nbsp;</div>
                    <div class="col-md-12">
                          <div class="col-md-2"></div>
                        <div class="col-md-3">
                            <b>City Name</b>
                        </div>
                        <div class="col-md-5">
                            <asp:TextBox ID="txt_Edit_City" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                          <div class="col-md-2"></div>
                    </div>
                    <div class="row">&nbsp;</div>
                    <div class="col-md-12">
                          <div class="col-md-2"></div>
                        <div class="col-md-3">
                            <b>Classification</b>
                        </div>
                        <div class="col-md-5">
                            <asp:DropDownList ID="ddl_Edit_Classify" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0">--Select One--</asp:ListItem>
                                        <asp:ListItem Value="A">Metro</asp:ListItem>
                                        <asp:ListItem Value="B">Mini Metro</asp:ListItem>
                                        <asp:ListItem Value="C">Other</asp:ListItem>
                                    </asp:DropDownList>
                        </div>
                          <div class="col-md-2"></div>
                    </div>                         
                    </div>
                  <div class="row">&nbsp;</div>
                <div class="modal-footer">
                    <a href="javascript:;" class="btn btn-sm btn-grey btn-rounded m-b-5" onclick="return checkControls1()" >Update</a>
                    <a href="javascript:;" class="btn btn-sm btn-danger btn-rounded m-b-5" data-dismiss="modal" id="btnClose">Close</a>
                </div>
            </div>
		</div>
	</div>

         <div class="modal fade" id="DeletePanel">
		<div class="modal-dialog"  style="height:auto; width:36%; margin-left:25%">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><font color="white">Delete City Details </font></h4>
                </div>

                <div class="modal-body" id="div2" runat="server">
                    <div class="col-md-12">
                        <div class="col-md-2"></div>
                        <div class="col-md-8">
                            <font color="red"><b>You Want To Delete Selected City?</b></font>
                        </div>
                          <div class="col-md-2"></div>
                    </div>
                    <div class="row">&nbsp;</div>
                </div>
                <div class="modal-footer">
                    <a href="javascript:;" class="btn btn-sm btn-grey btn-rounded m-b-5" onclick="return deleteData()" >Delete</a>
                    <a href="javascript:;" class="btn btn-sm btn-danger btn-rounded m-b-5" data-dismiss="modal" id="deleteClose">Close</a>
                </div>
            </div>
		</div>
	</div>

           <div class="col-md-12" id="div_ListCountryDetails" style="display:none; margin-top:10px">
				<div class="panel panel-danger">
					<div class="panel-heading">
                          <div class="panel-heading-btn">
							   <a href="javascript:;" class="btn btn-sm btn-success m-b-3" onclick="hide_List()" >Back</a>
						</div>
						<h4 class="panel-title"><b>City Details</b></h4>
					</div>
					<div class="panel-body">
                        
						<div class="panel pagination-danger" id="div_Details">
						
						</div>
					</div>
				</div>
			</div>

        <div style="display:none">
            <asp:TextBox ID="txtResult" runat="server"></asp:TextBox>
              <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Username" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtEmailID" runat="server"></asp:TextBox>
        </div>
    </form>
</body>
</html>
