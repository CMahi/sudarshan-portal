<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Reporting_Manager.aspx.cs" Inherits="Reporting_Manager" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   	<meta charset="utf-8" />
	<title>Reporting Manager</title>
	<link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
	<link href="../../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
	<link href="../../assets/css/style.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/bootstrap-calendar/css/bootstrap_calendar.css" rel="stylesheet" />
    <link href="../../assets/plugins/DataTables/media/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/DataTables/extensions/Responsive/css/responsive.bootstrap.min.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
           <div class="row" style="margin-top:10px" id="div_AddCountryDetails">
               <div id="divIns" runat="server" style="display:none">
                <div style="background-color:#E6E6E6;position: absolute;top:0;left:0;width: 100%;height:300%;z-index:1001;-moz-opacity: 0.8;opacity:.80;filter: alpha(opacity=80);"><img src="../../images/loading_transparent.gif" style="background-color:transparent;position:fixed; top:40%; left:46%;"/></div>
            </div>
			<div class="col-md-12">
				<div class="panel panel-danger">
					<div class="panel-heading">
                        <div class="panel-heading-btn">
							 <a href="javascript:;" class="btn btn-sm btn-inverse m-b-3" onclick="show_List()" >View All</a>
						</div>
						<h4 class="panel-title"><b>Employee Master</b></h4>
					</div>
					<div class="panel-body">
                        <div class="col-md-12">
						<div class="form-horizontal bordered-group">
							<div class="form-group">
                                <div class="col-md-1"></div>
								<label class="col-md-2 control-label"><b>Employee Code</b></label>
								<div class="col-md-2 ui-sortable">
                                    <asp:TextBox ID="emp_code" runat="server" CssClass="form-control input-sm" onchange="check_exists()"></asp:TextBox>
								</div>
                                <div class="col-md-1"></div>
								<label class="col-md-2 control-label"><b>Employee Name</b></label>
								<div class="col-md-2 ui-sortable">
                                    <asp:TextBox ID="emp_name" runat="server" CssClass="form-control input-sm"></asp:TextBox>
								</div>
								</div>

                            <div class="form-group">
                                <div class="col-md-1"></div>
								<label class="col-md-2 control-label"><b>Level</b></label>
								<div class="col-md-2 ui-sortable">
                                    <%--<asp:TextBox ID="emp_level" runat="server" CssClass="form-control input-sm"></asp:TextBox>--%>
                                     <asp:DropDownList ID="emp_level" runat="server" CssClass="form-control input-sm">
                                    </asp:DropDownList>
								</div>
                                <div class="col-md-1"></div>
								<label class="col-md-2 control-label"><b>Designation</b></label>
								<div class="col-md-2 ui-sortable">
                                    <asp:DropDownList ID="emp_designation" runat="server" CssClass="form-control input-sm">
                                    </asp:DropDownList>
								</div>
								</div>

                            
                            <div class="form-group">
                                <div class="col-md-1"></div>
								<label class="col-md-2 control-label"><b>Reporting Employee Code</b></label>
								<div class="col-md-2 ui-sortable">
                                    <asp:TextBox ID="rep_emp_code" runat="server" CssClass="form-control input-sm" onchange="getrepname()"></asp:TextBox>
        						</div>
                                <div class="col-md-1"></div>
								<label class="col-md-2 control-label"><b>Reporting Employee Name</b></label>
								<div class="col-md-2 ui-sortable">
                                    <asp:TextBox ID="rep_emp_name" runat="server" CssClass="form-control input-sm"></asp:TextBox>
								</div>
								</div>

							</div>
                        </div>
							
						</div>
                    <div class="modal-footer">
								<div class="col-md-3"></div>
								<div class="col-md-6" style="text-align:center">
                                    <asp:Button ID="btn_AddNew" runat="server" Text="Add New" class="btn btn-grey btn-rounded m-b-5" OnClick="btn_AddNew_Click" OnClientClick="return checkControls();"/>
                                    <input id="btnClear" runat="server" Value="Clear" class="btn btn-danger btn-rounded m-b-5" type="button" onclick="return clearControls()"/>
								</div>
                        <div class="col-md-3"></div>
							</div>
					</div>
				</div>
			</div>
        
           <div class="modal fade" id="UpdRepPanel">
              <div id="divUpd" runat="server" style="display:none">
                <div style="background-color:#E6E6E6;position: absolute;top:0;left:0;width: 100%;height:300%;z-index:1001;-moz-opacity: 0.8;opacity:.80;filter: alpha(opacity=80);"><img src="../../images/loading_transparent.gif" style="background-color:transparent;position:fixed; top:40%; left:46%;"/></div>
            </div>
		<div class="modal-dialog"  style="height:auto; width:81%; margin-left:9%">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><font color="white">Update Employee Details </font></h4>
                </div>

                <div class="modal-body" id="div_header" runat="server">
                    <div class="col-md-12">
                            <div class="form-horizontal bordered-group">
							<div class="form-group">
                                <div class="col-md-1"></div>
								<label class="col-md-2 control-label"><b>Employee Code</b></label>
								<div class="col-md-2 ui-sortable">
                                    <asp:TextBox ID="ed_emp_code" runat="server" CssClass="form-control input-sm" ></asp:TextBox>
								</div>
                                <div class="col-md-1"></div>
								<label class="col-md-2 control-label"><b>Employee Name</b></label>
								<div class="col-md-2 ui-sortable">
                                    <asp:TextBox ID="ed_emp_name" runat="server" CssClass="form-control input-sm"></asp:TextBox>
								</div>
								</div>

                            <div class="form-group">
                                <div class="col-md-1"></div>
								<label class="col-md-2 control-label"><b>Level</b></label>
								<div class="col-md-2 ui-sortable">
                                    <asp:DropDownList ID="ed_emp_level" runat="server" CssClass="form-control input-sm">
                                    </asp:DropDownList>
								</div>
                                <div class="col-md-1"></div>
								<label class="col-md-2 control-label"><b>Designation</b></label>
								<div class="col-md-2 ui-sortable">
                                    <asp:DropDownList ID="ed_emp_designation" runat="server" CssClass="form-control input-sm">
                                    </asp:DropDownList>
								</div>
								</div>

                            
                            <div class="form-group">
                                <div class="col-md-1"></div>
								<label class="col-md-2 control-label"><b>Reporting Employee Code</b></label>
								<div class="col-md-2 ui-sortable">
                                    <asp:TextBox ID="ed_rep_code" runat="server" CssClass="form-control input-sm" onchange="getrepname1()"></asp:TextBox>
        						</div>
                                <div class="col-md-1"></div>
								<label class="col-md-2 control-label"><b>Reporting Employee Name</b></label>
								<div class="col-md-2 ui-sortable">
                                    <asp:TextBox ID="ed_rep_name" runat="server" CssClass="form-control input-sm"></asp:TextBox>
								</div>
								</div>

							</div>
                    </div>
                </div>
                <div class="modal-footer" style="text-align:center">
                    <a href="javascript:;" class="btn btn-sm btn-grey btn-rounded m-b-5" onclick="return checkControls1()" >Update</a>
                    <a href="javascript:;" class="btn btn-sm btn-danger btn-rounded m-b-5" data-dismiss="modal" id="btnClose">Close</a>
                </div>
            </div>
		</div>
	</div>

         <div class="modal fade" id="DeletePanel">
       <div id="divDel" runat="server" style="display:none">
            <div style="background-color:#E6E6E6;position: absolute;top:0;left:0;width: 100%;height:300%;z-index:1001;-moz-opacity: 0.8;opacity:.80;filter: alpha(opacity=80);"><img src="../../images/loading_transparent.gif" style="background-color:transparent;position:fixed; top:40%; left:46%;"/></div>
        </div>
		<div class="modal-dialog"  style="height:auto; width:36%; margin-left:25%">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title"><font color="white">Delete Employee Details </font></h4>
                </div>

                <div class="modal-body" id="div2" runat="server">
                    <div class="col-md-12">
                        <div class="col-md-1"></div>
                        <div class="col-md-10">
                            <font color="red"><b>You Want To Delete Selected Employee Details?</b></font>
                        </div>
                          <div class="col-md-1"></div>
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

        <div class="row">
           <div class="col-lg-12" id="div_ListCountryDetails" style="display:none; margin-top:10px">
				<div class="panel panel-danger">
					<div class="panel-heading">
                          <div class="panel-heading-btn">
							   <a href="javascript:;" class="btn btn-sm btn-inverse m-b-3" onclick="hide_List()" >Back</a>
						</div>
						<h4 class="panel-title"><b>Employee Details</b></h4>
					</div>
					<div class="panel-body">
                        <div class="row">
						    <div  id="div_Details" runat="server" class="table-responsive">
						    </div>
                      </div>
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

    <script src="../../JS/Reporting_Manager.js"></script>
	
             
    </form>
</body>

</html>
