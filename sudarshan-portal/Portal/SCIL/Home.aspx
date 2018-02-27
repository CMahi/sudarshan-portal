<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Portal_SCIL_Home" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>Work Queue</title>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no"
        name="viewport" />
    <meta content="" name="description" />
    <meta content="" name="author" />
    <link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../assets/css/default.css" rel="stylesheet" />
</head>
<body style="overflow: hidden">
    <form id="form1" runat="server">
    <div id="Div1" runat="server" style="width: 95%; text-align: right;">
        <h3>
            <a href="#modal-without-animation" data-toggle="modal">
                <img src="../../Img/attendance_list_icon.jpg" height="40" style="margin-top: 1px;" title="Employee Attendance" /></a></h3>
    </div>
    <div class="row" style="margin-top: 200px;">
        <div class="col-sm-2 col-lg-1">
        </div>
        <div class="col-sm-2 col-lg-2">
            <div class="widget widget-stat widget-stat-right bg-danger text-white">
                <div class="widget-stat-icon">
                    <i class="fa fa-chrome"></i>
                </div>
                <div class="widget-stat-info">
                    <div class="widget-stat-title">
                        <a href="../Dashboard/NewChart.aspx" class="text-muted">
                            <h4>
                                Dashboard</h4>
                        </a>
                    </div>
                </div>
                <div class="widget-stat-progress">
                    <div class="progress">
                        <div class="progress-bar" style="width: 80%">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-2 col-lg-2">
            <div class="widget widget-stat widget-stat-right bg-success text-white">
                <div class="widget-stat-icon">
                    <i class="fa fa-diamond"></i>
                </div>
                <div class="widget-stat-info">
                    <div class="widget-stat-title">
                        <a href="TaskDetails.aspx" class="text-muted">
                            <h4>
                                My Task</h4>
                        </a>
                    </div>
                </div>
                <div class="widget-stat-progress">
                    <div class="progress">
                        <div class="progress-bar" style="width: 60%">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-2 col-lg-2">
            <div class="widget widget-stat widget-stat-right bg-primary text-white">
                <div class="widget-stat-icon">
                    <i class="fa fa-hdd-o"></i>
                </div>
                <div class="widget-stat-info">
                    <div class="widget-stat-title">
                        <asp:Label ID="rid" runat="server" Text="0" Style="display: none"></asp:Label>
                        <asp:LinkButton ID="rptid" runat="server" class="text-muted" OnClick="goToReport"><h4>Reports</h4></asp:LinkButton>
                    </div>
                </div>
                <div class="widget-stat-progress">
                    <div class="progress">
                        <div class="progress-bar" style="width: 70%">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-2 col-lg-2">
            <div class="widget widget-stat widget-stat-right bg-info text-white">
                <div class="widget-stat-icon">
                    <i class="fa fa-file"></i>
                </div>
                <div class="widget-stat-info">
                    <div class="widget-stat-title">
                        <asp:Label ID="mid" runat="server" Text="0" Style="display: none"></asp:Label>
                        <asp:LinkButton ID="masterid" runat="server" class="text-muted" OnClick="goToMaster"><h4>Masters</h4></asp:LinkButton>
                    </div>
                </div>
                <div class="widget-stat-progress">
                    <div class="progress">
                        <div class="progress-bar" style="width: 70%">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-2 col-lg-2">
            <div class="widget widget-stat widget-stat-right bg-info text-white">
                <div class="widget-stat-icon">
                    <i class="fa fa-file"></i>
                </div>
                <div class="widget-stat-info">
                    <div class="widget-stat-title">
                        <asp:Label ID="pid" runat="server" Text="0" Style="display: none"></asp:Label>
                        <asp:LinkButton ID="LinkButton1" runat="server" class="text-muted" OnClick="goToProcess"><h4>Processes</h4></asp:LinkButton>
                    </div>
                </div>
                <div class="widget-stat-progress">
                    <div class="progress">
                        <div class="progress-bar" style="width: 70%">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div style="display: none">
        <asp:TextBox ID="txt_UserName" runat="server"></asp:TextBox>
    </div>
    <div class="modal" id="modal-without-animation">
        <div class="modal-dialog" style="width: 95%;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        ×</button>
                    <h4 class="modal-title">
                        Employee Attendance</h4>
                </div>
                <div class="modal-body">
                 <div class="form-horizontal">
                <div class="form-group">
                        <label class="control-label col-md-2">
                            <b>Employee No</b></label>
                        <div class="col-md-3">
                            <label  id="lbl_Emp_No" runat="server" class="control-label" />
                        </div>
                        <label class="control-label col-md-2">
                            <b>Employee Name</b></label>
                        <div class="col-md-3">
                            <label  id="lbl_Emp_Name" runat="server" class="control-label" />
                        </div>
                       
                    </div>
                   
                    <div class="form-group" style="display:none">
                        <label class="control-label col-md-2">
                            From Date</label>
                        <div class="col-md-3">
                            <input type="text" id="txt_frm_Date" runat="server" class="form-control input-sm datepicker-example" />
                        </div>
                        <label class="control-label col-md-2">
                            To Date</label>
                        <div class="col-md-3">
                            <input type="text" id="txt_to_Date" runat="server" class="form-control input-sm datepicker-example" />
                        </div>
                    </div>
                    </div>
                    <div class="form-group">
                    <div class="col-md-10" style="text-align: center; display:none">
                        <a href="javascript:;" class="btn btn-info width-100" onclick="getdata()">Show</a> 
                        <a href="javascript:;"
                            class="btn btn-info width-100" data-dismiss="modal" onclick="getdataclear()">Close</a>
                    </div>
                    </div>
                    <div id="dv_Emp" runat="server">
                    </div>
                    
                </div>
                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>
    </form>
    <script src="../../assets/plugins/jquery/jquery-2.1.4.js" type="text/javascript"></script>
    <script src="../../assets/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <%--<script src="../../assets/js/zebra_datepicker.src.js" type="text/javascript"></script>--%>
    <script src="../../assets/plugins/DataTables/media/js/jquery.dataTables.js" type="text/javascript"></script>
    <script src="../../assets/plugins/DataTables/media/js/dataTables.bootstrap.min.js"
        type="text/javascript"></script>
    <script src="../../assets/plugins/DataTables/extensions/Responsive/js/dataTables.responsive.min.js"
        type="text/javascript"></script>
    <script src="../../assets/js/page-table-manage-responsive.demo.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.datepicker-example').Zebra_DatePicker({ format: 'd-M-Y' });
            PageDemo.init();
            $('#tbl_Emp').dataTable();
        });

        function getdata() {
            var frmdate = $("#txt_frm_Date").val();
            var todate = $("#txt_to_Date").val();
            var user = $("#txt_UserName").val();

            Portal_SCIL_Home.GetData(frmdate, todate,user, OnSuccess);
        }

        function OnSuccess(response) {
            document.getElementById("dv_Emp").innerHTML = response.value;
            $('#tbl_Emp').dataTable();
        }
        function getdataclear() {
            $("#txt_frm_Date").val('');
            $("#txt_to_Date").val('');

            var frmdate = $("#txt_frm_Date").val();
            var todate = $("#txt_to_Date").val();
            var user = $("#txt_UserName").val();
            Portal_SCIL_Home.GetData(frmdate, todate,user, OnSuccess);
        }

        
    </script>
</body>
</html>
