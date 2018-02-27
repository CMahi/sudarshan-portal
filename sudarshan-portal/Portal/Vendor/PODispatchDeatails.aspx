<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" Async="true"
    CodeFile="PODispatchDeatails.aspx.cs" Inherits="PODispatchDeatails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta charset="utf-8" />
	<title>Vendor Payment Details</title>
	<meta content="width=device-width, initial-scale=1.0" name="viewport" />
	<meta content="" name="description" />
	<meta content="" name="author" />
	
	<!-- ================== BEGIN BASE CSS STYLE ================== -->
	<link href="../../CSS/font-awesome.min.css" rel="stylesheet" />
    <link href="../../CSS/bootstrap.min.css" rel="stylesheet" />
	<link href="../../CSS/style.min.css" rel="stylesheet" />
	<!-- ================== END BASE CSS STYLE ================== -->
</head>
<body style="overflow-x:hidden">
 <%--  	<div id="page-loader" class="page-loader fade in"><span class="spinner">Loading...</span></div>
  	<div id="page-container" class="fade page-container page-header-fixed page-sidebar-fixed page-with-two-sidebar page-with-footer">--%>
	
		<div class="row">
			<div class="col-md-12">
				<div class="panel panel-danger">
					<div class="panel-heading">
						<%--<div class="panel-heading-btn">
							<a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-warning" data-click="panel-collapse"><i class="fa fa-minus"></i></a>
							<a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-danger" data-click="panel-remove"><i class="fa fa-times"></i></a>
						</div>--%>
						<h3 class="panel-title"><b>Vendor Payment Details</b></h3>
					</div>
				</div>
			  </div>
			<div class="col-md-2" style="margin-top:-8px;">
				<div class="panel panel-success">
					<div class="panel-heading">
						<h4 class="panel-title">List of Open PO</h4>
					</div>
					<div class="panel-body">
						<div data-scrollbar="true" data-height="196px">
							<form class="form-horizontal bordered-group">
								<div id="div_po" runat="server" ></div>
                            </form>
						</div>	
					</div>
				</div>
			</div>
			<div class="col-md-10" style="margin-top:-8px;">
				<div class="panel panel-success">
					<div class="panel-heading">
						<h4 class="panel-title">PO Header</h4>
					</div>
                    <div class="panel-body">
						<div id="div_Header" runat="server"></div>
                        <div class='form-group'>
                            <div style='text-align:center'>
                                <button id='btn_updatedispatch' type='submit' class='btn btn-sm btn-danger demotest' onclick="getDetail()">Update Dispatch Details</button>
                            </div>
                        </div>
                   </div>
				</div>
			</div>
           	<div class="col-md-12" style="margin-top:-8px;" id="div_main">
				<div class="panel panel-success">
					<div class="panel-heading">
						<h4 class="panel-title">PO Details</h4>
					</div>
                    
					<div class="panel-body">
						<div class="table-responsive">
                            <div id="div_detail" runat="server"></div>
						</div>
					</div>
				</div>
			</div>
            
			<div class="col-md-6" style="margin-top:-8px;" id="div_DispatchDetails">
				<div class="panel panel-success">
					<div class="panel-heading">
						<h4 class="panel-title">Dispatch Details</h4>
					</div>
					<div class="panel-body">
						<form class="form-horizontal bordered-group">
							<div class="form-group">
								<label class="col-md-3 control-label ui-sortable">Transporter Name</label>
								<div class="col-md-9 ui-sortable">
									<input class="form-control" type="text" runat="server" id="txt_transporter_Name"/>
								</div>
							</div>
							<div class="form-group">
								<label class="col-md-3 control-label ui-sortable">Vehicle No</label>
								<div class="col-md-9 ui-sortable">
									<input class="form-control" type="text" runat="server" id="txt_Vehicle_No"/>
								</div>
							</div>
							<div class="form-group">
								<label class="col-md-3 control-label ui-sortable">Contact Person Name</label>
								<div class="col-md-9 ui-sortable">
									<input class="form-control" type="text" runat="server" id="txt_Contact_Person_Name"/>
								</div>
							</div>
							<div class="form-group">
								<label class="col-md-3 control-label ui-sortable">Contact No</label>
								<div class="col-md-9 ui-sortable">
									<input class="form-control" type="text" runat="server" id="txt_Contact_No"/>
								</div>
							</div>
							<div class="form-group">
								<label class="col-md-3 control-label ui-sortable">LR No.</label>
								<div class="col-md-9 ui-sortable">
									<input class="form-control" type="text" runat="server" id="txt_LR_NO"/>
                                </div>
							</div>
							<div class="form-group">
								<label class="col-md-3 control-label ui-sortable">LR Date</label>
								<div class="col-md-9 ui-sortable">
									<input class="form-control" type="text" runat="server" id="txt_LR_Date"/>
								</div>
							</div>
						</form>
					</div>
				</div>
			</div>
            <div class="col-md-6" style="margin-top:-8px;" id="div_DocumentAttach">
				<div class="panel panel-success">
					<div class="panel-heading">
						<h4 class="panel-title">Document Upload</h4>
					</div>
					<div class="panel-body">
                        <select name="ddl_Document" id="ddl_Document" runat="server"></select>
                        <span class="btn btn-success fileinput-button">
                            <input name="files[]" id="uploadBtn" type="file" multiple />
						</span>
                         <div id="div_Doc" runat="server"></div>
						<%--<table class="table table-bordered" id="uploadTable" style="visibility:hidden">
							<thead>
								<tr class="grey">
									<th>Document Name</th>
									<th>File Name</th>
									<th>Delete</th>
								</tr>
							</thead>
							<tbody>
                                <div id="div_Doc" runat="server"></div>
								<tr id="Delivery" style="visibility:hidden">
									<td ><input type="text" name="Delivery_Challan" id="Delivery_Challan"/></td>
									<td><input type="text" name="fileName" id="fileName" /></td>
									<td><i class="glyphicon glyphicon-trash"></i></td>
								</tr>
								<tr id="Invoice_Cha" style="visibility:hidden">
									<td><input type="text" name="Invoice" id="Invoice"/></td>
									<td><input type="text" name="fileName1" id="fileName1"/></td>
									<td><i class="glyphicon glyphicon-trash"></i></td>
								</tr>
								<tr id="L_R" style="visibility:hidden">
									<td><input type="text" name="LR" id="LR"/></td>
									<td><input type="text" name="fileName2" id="fileName2"/></td>
									<td><i class="glyphicon glyphicon-trash"></i></td>
								</tr>
								<tr id="Road" style="visibility:hidden">
									<td><input type="text" name="Road_Permit" id="Road_Permit"/></td>
									<td><input type="text" name="fileName3" id="fileName3"/></td>
									<td><i class="glyphicon glyphicon-trash"></i></td>
								</tr>
                                <tr id="Tax" style="visibility:hidden">
									<td><input type="text" name="Tax_Invoice" id="Tax_Invoice"/></td>
									<td><input type="text" name="fileName4" id="fileName4"/></td>
									<td><i class="glyphicon glyphicon-trash"></i></td>
								</tr>
							</tbody>
						</table>--%>
					</div>
				</div>
			</div>
           	<div class="col-md-12" style="margin-top:-8px;" id="div_Action">
				<div class="panel panel-success">
					<div class="panel-heading">
						<h4 class="panel-title">Action</h4>
					</div>
					<div class="panel-body">
						<div style="text-align:center">
							<button id="btn_Early_Payment" type="submit" class="btn btn-sm btn-success">Early Payment Request</button>
							<button id="btn_Submit" type="submit" class="btn btn-sm btn-success" onclick="getXML()">Submit</button>
							<button id="btn_Cancel" type="submit" class="btn btn-sm btn-danger">Cancel</button>
						</div>
					</div>
				</div>
			</div>
              
		</div>
	<%--</div>--%>
	<div class="modal fade" id="incoterm">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
					<h4 class="modal-title">INCO Terms <font color="red">  >> PO Number : <span  id="div_IcoTemSetPoNo"></span></font></h4>
				</div>
				<div class="modal-body">
                    <div id="div_Inco" runat="server"></div>
					
				</div>
				<div class="modal-footer">
					<a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>
				</div>
			</div>
		</div>
	</div>
	<div class="modal fade" id="paymentterm">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
					<h4 class="modal-title">Payment Terms<font color="red">  >> PO Number : <span  id="div_PaymentTemSetPoNo"></span></font></h4>
				</div>
				<div class="modal-body">
                    <div id="div_Payment" runat="server"></div>
				</div>
				<div class="modal-footer">
					<a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>
				</div>
			</div>
		</div>
	</div>
	<div class="modal fade" id="schedule">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
					<h4 class="modal-title">Schedule<font color="red">  >> PO Number : <span id="div_SchedulePO"></span></font></h4>
				</div>
				<div class="modal-body">
                    <div id="div_Schedule" runat="server"></div>
				</div>
				<div class="modal-footer">
					<a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>
                  
				</div>
			</div>
		</div>
        <form class="form-horizontal bordered-group" runat="server">
            <div id="div_txt" style="display:none">
                <asp:TextBox ID="txt_Username" runat="server"></asp:TextBox>
                <asp:TextBox ID="txt_Po_No" runat="server"></asp:TextBox>
                <asp:TextBox ID="txt_Header_PO" runat="server"></asp:TextBox>
            </div>
        </form>
	</div>

	<!-- ================== BEGIN BASE JS ================== -->
    <script src="../../JS/jquery-1.9.1.min.js"></script>
	<script src="../../JS/jquery-migrate-1.1.0.min.js"></script>
	<script src="../../JS/jquery-ui.min.js"></script>
	<script src="../../JS/bootstrap.min.js"></script>
	<script src="../../JS/jquery.slimscroll.min.js"></script>
	<script src="../../JS/jquery.cookie.js"></script>
    <script src="../../JS/demo.min.js"></script>
    <script src="../../JS/apps.min.js"></script>
    <script src="../../JS/PODispatchDeatails.js"></script>
	<!-- ================== END PAGE LEVEL JS ================== -->
	<script language="javascript" type="text/javascript">
	   
	</script>
</body>
</html>

