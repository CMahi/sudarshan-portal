<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HomePage.aspx.cs" Inherits="HomePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Sudarshan</title>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" name="viewport" />
    <meta content="" name="description" />
    <meta content="" name="author" />

    <link href="../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/style.min.css" rel="stylesheet" />
    <link href="../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />

</head>
<body style="overflow: hidden">
    <form id="form1" runat="server">
	<marquee style="color:red; padding-top:10px; font-size:medium">All New Released Purchase Orders will be reflected on portal in span of 3 hrs.</marquee>
	<div style="height: 70px;">
             <div style="height: 75px; padding-top: 15px; padding-left: 40px; float:right; font-size:large;">
                <a href='../Document/Vendor Portal -User Manual.pdf' role='button' id='anc1' data-toggle='modal' target="_blank">
                    <img src="../Img/User_Manul.png"</a>
		</div>
	<div style="height: 75px; padding-top: 15px; padding-left: 40px; float:right; font-size:large;">
                <a href="../Video.aspx">Video</a>
		</div>
             </div>
        <div class="row" style="margin-top: 150px;">
            <div class="col-sm-1 col-lg-3"></div>
         <%--   <div class="col-sm-2 col-lg-2">
                <div class="widget widget-stat widget-stat-right bg-danger text-white">
                    <div class="widget-stat-icon"><i class="fa fa-chrome"></i></div>
                    <div class="widget-stat-info">
                        <div class="widget-stat-title"><a href="Vendor/DashBoard.aspx" class="text-muted">
                            <h4>Dashboard</h4>
                        </a></div>
                    </div>
                    <div class="widget-stat-progress">
                        <div class="progress">
                            <div class="progress-bar" style="width: 80%"></div>
                        </div>
                    </div>
                </div>
            </div>--%>
            <div class="col-sm-2 col-lg-2">
                <div class="widget widget-stat widget-stat-right bg-danger text-white">
                    <div class="widget-stat-icon"><i class="fa fa-chrome"></i></div>
                    <div class="widget-stat-info">
                        <div class="widget-stat-title"><a href="DashBoard/Vendor_Dashboard_Chart.aspx" class="text-muted">
                            <h4>Dashboard</h4>
                        </a></div>
                    </div>
                    <div class="widget-stat-progress">
                        <div class="progress">
                            <div class="progress-bar" style="width: 80%"></div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-2 col-lg-2">
                <div class="widget widget-stat widget-stat-right bg-info text-white">
                    <div class="widget-stat-icon"><i class="fa fa-file"></i></div>
                    <div class="widget-stat-info">
                        <div class="widget-stat-title"><a href="Vendor/TaskDetails.aspx" class="text-muted">
                            <h4>My Tasks</h4>
                        </a></div>
                    </div>
                    <div class="widget-stat-progress">
                        <div class="progress">
                            <div class="progress-bar" style="width: 80%"></div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-2 col-lg-2">
                <div class="widget widget-stat widget-stat-right bg-success text-white">
                    <div class="widget-stat-icon"><i class="fa fa-diamond"></i></div>
                    <div class="widget-stat-info">
                        <div class="widget-stat-title"><a href="../Processes/DISPATCH CREATION NOTE/PODispatchDeatails.aspx?processid=9&stepid=96" class="text-muted">
                            <h4>Dispatches</h4>
                        </a></div>
                    </div>
                    <div class="widget-stat-progress">
                        <div class="progress">
                            <div class="progress-bar" style="width: 60%"></div>
                        </div>
                    </div>
                </div>
            </div>
            
             <div class="col-sm-2 col-lg-2">
                <div class="widget widget-stat widget-stat-right bg-success text-white">
                    <div class="widget-stat-icon"><i class="fa fa-diamond"></i></div>
                    <div class="widget-stat-info">
                        <div class="widget-stat-title"><a href="../Processes/SERVICE PO/Service_PO_Request.aspx?processid=26&stepid=280" class="text-muted">
                            <h4>Service PO</h4>
                        </a></div>
                    </div>
                    <div class="widget-stat-progress">
                        <div class="progress">
                            <div class="progress-bar" style="width: 60%"></div>
                        </div>
                    </div>
                </div>
            </div>

	    </div>
	    <div class="row" style="margin-top: 40px;">
            <div class="col-sm-1 col-lg-3"></div>
	    <div class="col-sm-2 col-lg-2">
                <div class="widget widget-stat widget-stat-right bg-success text-white">
                    <div class="widget-stat-icon"><i class="fa fa-diamond"></i></div>
                    <div class="widget-stat-info">
                        <div class="widget-stat-title"><a href="../Processes/Vendor Advance Request/Vendor_Advance_Request.aspx?processid=23&stepid=119" class="text-muted">
                            <h4>Advance Request</h4>
                        </a></div>
                    </div>
                    <div class="widget-stat-progress">
                        <div class="progress">
                            <div class="progress-bar" style="width: 60%"></div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-2 col-lg-2">
                <div class="widget widget-stat widget-stat-right bg-primary text-white">
                    <div class="widget-stat-icon"><i class="fa fa-hdd-o"></i></div>
                    <div class="widget-stat-info">
                        <div class="widget-stat-title"><a href="../Reports/Report_Master.aspx" class="text-muted">
                            <h4>Reports</h4>
                        </a></div>
                    </div>
                    <div class="widget-stat-progress">
                        <div class="progress">

                            <div class="progress-bar" style="width: 70%"></div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-2 col-lg-2">
                <div class="widget widget-stat widget-stat-right bg-warning text-white">
                    <div class="widget-stat-icon"><i class="fa fa-users"></i></div>
                    <div class="widget-stat-info">
                        <div class="widget-stat-title"><a href="../Processes/EARLY PAYMENT REQUEST/Early_Payment_Request.aspx?processid=10&stepid=102" class="text-muted">
                            <h4>Early Payment Request</h4>
                        </a></div>
                    </div>
                    <div class="widget-stat-progress">
                        <div class="progress">
                            <div class="progress-bar" style="width: 70%"></div>
                        </div>
                    </div>
                </div>
            </div>
             
            <div class="col-sm-1 col-lg-1"></div>
        </div>
        <div style="display: none">
            <asp:TextBox ID="txt_UserName" runat="server"></asp:TextBox>
        </div>
    </form>
</body>
</html>
