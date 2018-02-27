<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" Async="true"
    CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta charset="utf-8" />
	<title>Dashboard</title>
	<meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" name="viewport" />
	<meta content="" name="description" />
	<meta content="" name="author" />
    <link href="../../assets/css/style.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
<link href="../../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/jquery-ui/themes/base/minified/jquery-ui.min.css" rel="stylesheet" />

</head>
<body style="overflow:hidden">
		
		<div class="row" style="margin-top:15%">
             <div class="col-sm-2 col-lg-2"></div>
			<div class="col-sm-2 col-lg-2">
				<div class="widget widget-stat widget-stat-right bg-inverse text-white">
					<div class="widget-stat-icon"><i class="fa fa-chrome"></i></div>
					<div class="widget-stat-info">
						<div class="widget-stat-title">New POs</div>
						<div class="widget-stat-number">10</div>
					</div>
					<div class="widget-stat-progress">
						<div class="progress">
							<div class="progress-bar" style="width: 80%"></div>
						</div>
					</div>
					<div class="widget-stat-footer text-left"></div>
				</div>
			</div>
			<div class="col-sm-2 col-lg-2">
				<div class="widget widget-stat widget-stat-right bg-success text-white">
					<div class="widget-stat-icon"><i class="fa fa-diamond"></i></div>
					<div class="widget-stat-info">
						<div class="widget-stat-title">Balance Statement Summary</div>
						<div class="widget-stat-number">71</div>
					</div>
					<div class="widget-stat-progress">
						<div class="progress">
							<div class="progress-bar" style="width: 60%"></div>
						</div>
					</div>
					<div class="widget-stat-footer"></div>
				</div>
			</div>
			<div class="col-sm-2 col-lg-2">
				<div class="widget widget-stat widget-stat-right bg-primary text-white">
					<div class="widget-stat-icon"><i class="fa fa-hdd-o"></i></div>
					<div class="widget-stat-info">
						<div class="widget-stat-title">Total Amount</div>
						<div class="widget-stat-number">100000.00</div>
					</div>
					<div class="widget-stat-progress">
						<div class="progress">
							<div class="progress-bar" style="width: 70%"></div>
						</div>
					</div>
					<div class="widget-stat-footer"></div>
				</div>
			</div>
			<div class="col-sm-2 col-lg-2">
				<div class="widget widget-stat widget-stat-right bg-info text-white">
					<div class="widget-stat-icon"><i class="fa fa-file"></i></div>
					<div class="widget-stat-info">
						<div class="widget-stat-title">Pending Invoice</div>
						<div class="widget-stat-number">29</div>
					</div>
					<div class="widget-stat-progress">
						<div class="progress">
							<div class="progress-bar" style="width: 70%"></div>
						</div>
					</div>
					<div class="widget-stat-footer"></div>
				</div>
			</div>
            <div class="col-sm-2 col-lg-2"></div>
		</div>
		
	<!-- ================== BEGIN BASE JS ================== -->
        <script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
        <script src="../../assets/js/apps.min.js"></script>
        <script src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
	<script src="../../assets/plugins/slimscroll/jquery.slimscroll.min.js"></script>
        <script src="../../assets/plugins/jquery-ui/ui/minified/jquery-ui.min.js"></script>
	<!-- ================== END PAGE LEVEL JS ================== -->
	
	<script>
	    $(document).ready(function () {
	        App.init();
	    });
	</script>
</body>
</html>
