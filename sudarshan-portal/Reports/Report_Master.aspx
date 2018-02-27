<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Report_Master.aspx.cs" Inherits="Reports_Report_Master" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta charset="utf-8" />
	<title></title>
	<meta content="width=device-width, initial-scale=1.0" name="viewport" />
	<meta content="" name="description" />
	<meta content="" name="author" />
	
	<!-- ================== BEGIN BASE CSS STYLE ================== -->
	<link href="../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
	<link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
	<link href="../assets/css/style.min.css" rel="stylesheet" />
	<!-- ================== END BASE CSS STYLE ================== -->

</head>
<body style="overflow-x:hidden">
	<form runat="server">
		<div class="row">
			<div class="col-md-12">
				<div class="panel panel-danger">
					<div class="panel-heading">
						 <div class="btn-group pull-right" style="margin-top:-5px;">
                            <asp:Button ID="btnCancel" runat="server" Text="Back" class="btn btn-sm btn-inverse" OnClick="btnCancel_Click" Font-Bold="true"/>
						</div>
						<h3 class="panel-title"><b>Report Details</b></h3>
					</div>
				</div>
			</div>
			<div class="col-md-3" style="margin-top:-5px;">
				<div class="panel panel-grey">
					<div class="panel-heading">
						<h4 class="panel-title">Vendor Report</h4>
					</div>
					<div class="panel-body">
						<div data-scrollbar="true" data-height="185px">
							<ul class="feeds">
								<li>
									<a href="Vendor Reports/Po_List_Status_Report.aspx">
										<div class="icon bg-danger-light"><i class="fa fa-file-text"></i></div>
										<div class="desc">PO Listing Report</div>
									</a>
								</li>
								<li>
									<a href="Vendor Reports/Invoice_Details_Report.aspx">
										<div class="icon bg-danger-light"><i class="fa fa-file-text"></i></div>
										<div class="desc">Invoice Details Report</div>
									</a>
								</li>
									<li>
									<a href="Vendor Reports/Vendor_Account_Statement.aspx">
										<div class="icon bg-danger-light"><i class="fa fa-file-text"></i></div>
										<div class="desc">Vendor Account Statement Details</div>
									</a>
								</li>
                                <li>
									<a href="Vendor Reports/Penalty_Report.aspx">
										<div class="icon bg-danger-light"><i class="fa fa-file-text"></i></div>
										<div class="desc">Penalty Report</div>
									</a>
								</li>
				 <li>
									<a href="Vendor Reports/Vendor_Advance_Report.aspx">
										<div class="icon bg-danger-light"><i class="fa fa-file-text"></i></div>
										<div class="desc">Advance Request Details Report</div>
									</a>
								</li>
								 <li>
									<a href="Vendor Reports/Service_Invoice_Detail_Report.aspx">
										<div class="icon bg-danger-light"><i class="fa fa-file-text"></i></div>
										<div class="desc">Service Invoice Detail Report</div>
									</a>
								</li>
							</ul>
						</div>	
					</div>
				</div>
			</div>
		
		</div>
	<!-- ================== BEGIN BASE JS ================== -->
	<script src="../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
	<script src="../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
	<script src="../assets/plugins/slimscroll/jquery.slimscroll.min.js"></script>
    <script src="../assets/js/apps.min.js"></script>
    </form>
</body>

</html>
