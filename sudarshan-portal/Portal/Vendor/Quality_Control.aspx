<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" Async="true"
    CodeFile="Quality_Control.aspx.cs" Inherits="Quality_Control" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta charset="utf-8" />
	<title>Quality Control</title>
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
	<div id="page-loader" class="page-loader fade in"><span class="spinner">Loading...</span></div>
	<div id="page-container" class="fade page-container page-header-fixed page-sidebar-fixed page-with-two-sidebar page-with-footer">
		<div id="header" class="header navbar navbar-default navbar-fixed-top">
			<div class="container-fluid">
				<div class="navbar-header"><a href="../../Workitem.aspx" class="text-muted"><img src="../../Img/133649.png" height="20" style="margin-top:15px;" /></a></div>
				<ul class="nav navbar-nav navbar-right">
					<li class="dropdown navbar-user">
						<a href="javascript:;" class="dropdown-toggle" data-toggle="dropdown">
							<span class="hidden-xs" id="sp_LoginUser" runat="server">Admin</span> <b class="caret"></b>
						</a>
						<ul class="dropdown-menu pull-right">
							<%--<li><a href="javascript:;">Edit Profile</a></li>
							<li class="divider"></li>--%>
							<li><a href="../../Login.aspx">Log Out</a></li>
						</ul>
					</li>
				</ul>
			</div>
		</div>
		<div class="row">
			<div class="col-md-12">
				<div class="panel panel-danger">
					<div class="panel-heading">
						<div class="panel-heading-btn">
							<a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-warning" data-click="panel-collapse"><i class="fa fa-minus"></i></a>
							<a href="javascript:;" class="btn btn-xs btn-icon btn-circle btn-danger" data-click="panel-remove"><i class="fa fa-times"></i></a>
						</div>
						<h3 class="panel-title"><b>Quality Control</b></h3>
					</div>
				</div>
			</div>
			<div class="col-md-12" style="margin-top:-8px;">
				<div class="panel panel-success">
					<div class="panel-heading">
						<h4 class="panel-title">PO Header</h4>
					</div>
					<div class="panel-body">
						<div class="table-responsive">
							<table class="table table-bordered table-striped">
								<thead>
									<tr class="grey">
										<th>Vendor Name</th>
										<th>Vendor Code</th>
										<th>PO No</th>
										<th>Date</th>
										<th>Currency</th>
										<th>Created By</th>
										<th>PO Type</th>
										<th>INCO Terms</th>
										<th>PO Value</th>
										<th>Tax</th>
										<th>PO GV</th>
										<th>Payment Terms</th>
									</tr>
								</thead>
								<tbody>
									<tr>
										<td>QUALIGENS</td>
										<td>63360</td>
										<td><a>5500000191</a></td>
										<td>01-Mar-2002</td>
										<td>INR</td>
										<td>WRAMESH</td>
										<td>Service PO</td>
										<td style="width:120px"><a href="#incoterm" data-toggle="modal">INCO Terms</a></td>
										<td style="text-align: right;">92590.55</td>
										<td style="text-align: right;">4500.00</td>
										<td style="text-align: right;">97090.00</td>
										<td style="width:120px"><a href="#paymentterm" data-toggle="modal">P005</a></td>
									</tr>
								</tbody>
							</table>
						</div>
					</div>
				</div>
			</div>
			<div class="col-md-12" style="margin-top:-8px;">
				<div class="panel panel-success">
					<div class="panel-heading">
						<h4 class="panel-title">PO Details</h4>
					</div>
					<div class="panel-body">
						<div class="table-responsive">
							<table class="table table-bordered table-striped">
								<thead>
									<tr class="grey">
										<th>Sr.NO</th>
										<th>Material NO</th>
										<th>Plant</th>
										<th>Storage Location</th>
										<th>Quantity</th>
										<th>UOM</th>
										<th>Net Price</th>
										<th>Amount</th>
										<th>Material Group</th>
										<th>Dispatch Qty</th>
										<th>GR Qty</th>
										<th>QC Approved Qty</th>
										<th>QC Rejected Qty</th>
										<th>Is Deviated</th>
										<th>% Deviation</th>
										<th>Remark</th>
									</tr>
								</thead>
								<tbody>
									<tr>
										<td>1</td>
										<td>A0036</td>
										<td>ROHA</td>
										<td>R001</td>
										<td style="text-align: right;">51155</td>
										<td>KG</td>
										<td style="text-align: right;">1.81</td>
										<td style="text-align: right;">92590.55</td>
										<td style="text-align: right;">CP03</td>
										<td style="text-align: right;">1000</td>
										<td style="text-align: right;">1000</td>
										<td><input class="form-control" type="text" style="width:120px"></td>
										<td><input class="form-control" type="text" style="width:120px"></td>
										<td>
											<select class="form-control" style="width:140px">
												<option>---SELECT ONE---</option>
												<option>YES</option>
												<option>NO</option>
											</select>
										</td>
										<td><input class="form-control" type="text" style="width:120px"></td>
										<td><input class="form-control" type="text" style="width:180px"></td>
									</tr>
								</tbody>
							</table>
						</div>
					</div>
				</div>
			</div>
			<div class="col-md-6" style="margin-top:-8px;" id="div_DispatchDetails">
				<div class="panel panel-success">
					<div class="panel-heading">
						<h4 class="panel-title">Penalty Details</h4>
					</div>
					<div class="panel-body">
						<form class="form-horizontal bordered-group">
							<div class="form-group">
								<label class="col-md-3 control-label ui-sortable">Is Penalty Applicable</label>
								<div class="col-md-9 ui-sortable">
									<select class="form-control">
										<option>---SELECT ONE---</option>
										<option>YES</option>
										<option>NO</option>
									</select>
								</div>
							</div>
							<div class="form-group">
								<label class="col-md-3 control-label ui-sortable">Penalty Amount</label>
								<div class="col-md-9 ui-sortable">
									<input class="form-control" type="text">
								</div>
							</div>
							<div class="form-group">
								<label class="col-md-3 control-label ui-sortable">Remark</label>
								<div class="col-md-9 ui-sortable">
									<textarea class="form-control" rows="2" placeholder="Remark"></textarea>
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
						<table class="table table-bordered table-striped">
							<thead>
								<tr class="grey">
									<th>Document Name</th>
									<th>File Name</th>
									<th>Delete</th>
								</tr>
							</thead>
							<tbody>
								<tr>
									<td>Delivery Challan</td>
									<td>DC-PO-123</td>
									<td><i class="glyphicon glyphicon-trash"></i></td>
								</tr>
								<tr>
									<td>Invoice</td>
									<td>INV-1-PO-123</td>
									<td><i class="glyphicon glyphicon-trash"></i></td>
								</tr>
								<tr>
									<td>LR</td>
									<td>LR-PO-123</td>
									<td><i class="glyphicon glyphicon-trash"></i></td>
								</tr>
								<tr>
									<td>Road Permit</td>
									<td>RP-PO-123</td>
									<td><i class="glyphicon glyphicon-trash"></i></td>
								</tr>
							</tbody>
						</table>
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
							<button id="btn_updatedispatch" type="submit" class="btn btn-sm btn-danger">Quality Control Approval</button>
							<button id="Button1" type="submit" class="btn btn-sm btn-success">Cancel</button>
						</div>
					</div>
				</div>
			</div>
		</div>
	</div>
	<div class="modal fade" id="incoterm">
		<div class="modal-dialog">
			<div class="modal-content">
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
					<h4 class="modal-title">INCO Terms<font color="red">  >> PO Number : 5500000191</font></h4>
				</div>
				<div class="modal-body">
					<table class="table table-bordered table-striped">
						<thead>
							<tr class="grey">
								<th>Inco Terms (Part 1)</th>
								<th>Inco Terms (Part 2)</th>
							</tr>
						</thead>
						<tbody>
							<tr>
								<td>FOR</td>
								<td>OUR ROHA PLANT</td>
							</tr>
						</tbody>
					</table>
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
					<h4 class="modal-title">Payment Terms<font color="red">  >> PO Number : 5500000191</font></h4>
				</div>
				<div class="modal-body">
					<table class="table table-bordered table-striped">
						<thead>
							<tr class="grey">
								<th>Day Limit</th>
								<th>Calendar Day for Payment</th>
								<th>Days from Baseline Date for Payment</th>
							</tr>
						</thead>
						<tbody>
							<tr>
								<td style="text-align: right;">10</td>
								<td style="text-align: right;">31</td>
								<td style="text-align: right;">14</td>
							</tr>
						</tbody>
					</table>
				</div>
				<div class="modal-footer">
					<a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>
				</div>
			</div>
		</div>
	</div>
	<!-- ================== BEGIN BASE JS ================== -->
	<script src="../../JS/jquery-1.9.1.min.js"></script>
	<script src="../../JS/bootstrap.min.js"></script>
    <script src="../../JS/apps.min.js"></script>
	<!-- ================== END PAGE LEVEL JS ================== -->
	<script>
	    $(document).ready(function () {
	        App.init();
	    });
	</script>
</body>
</html>

