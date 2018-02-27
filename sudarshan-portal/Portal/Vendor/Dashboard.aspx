<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" Async="true"
    CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>SUDARSHAN</title>
    <meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" name="viewport" />
    <meta content="" name="description" />
    <meta content="" name="author" />
    <link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.min.css" rel="stylesheet" />
    <link href="../../CSS/loading.css" rel="stylesheet" />
</head>
<body style="overflow:hidden">
    <form id="frm_Dashboard" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <div class="row" style="margin-top:15%">
             <div class="col-sm-2 col-lg-2"></div>
            <div class="col-sm-2 col-lg-2">
                <div class="widget widget-stat widget-stat-right bg-inverse text-white">
                    <div class="widget-stat-icon"><i class="fa fa-chrome"></i></div>
                    <div class="widget-stat-info">
                        <div class="widget-stat-title" id="div_NewPO"><a href="#NewPO" data-toggle="modal">New POs</a>	</div>
                        <div class="widget-stat-number">
                            <asp:Label ID="Label2" runat="server"></asp:Label></div>
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


        <div class="modal fade" id="NewPO">
            <div class="modal-dialog">

                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title">New POs :</h4>
                    </div>

                    <div class="modal-body" style="overflow-y: scroll; height: auto;">
                        <asp:UpdatePanel ID="upModal" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div id="div_po" runat="server" style="overflow-y: scroll; height: 296px"></div>

                             
                                <div class="modaltestfooter">
                                    <asp:Button ID="btn_Add" runat="server" Text="Acknowledge" class="btn btn-sm btn-danger" OnClientClick="editdatails();" OnClick="btnsubmit_click"></asp:Button>
                                    <asp:Button ID="btn_sendback" runat="server" Text="Send Back" class="btn btn-sm btn-danger" OnClientClick="editdatails();" OnClick="btn_sendback_click"></asp:Button>
                                    <a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>
                                    <br />
                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                </div>
                            </ContentTemplate>

                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_Add" />
                            </Triggers>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_sendback" />
                            </Triggers>
                        </asp:UpdatePanel>

                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upModal">
                            <ProgressTemplate>
                                <div class="modal1">
                                    <div class="center1">
                                        <img alt="Processing" id="img_Progress" src="../../images/loading_transparent.gif" />
                                    </div>
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>

                        <div id="conditionButton" style="display: none">
                            <asp:TextBox ID="txt_DomainName" runat="server" Width="0.5px"></asp:TextBox>
                            <asp:TextBox ID="txt_procedure" runat="server" Width="0.5px"></asp:TextBox>
                            <asp:TextBox ID="txt_SearchString" runat="server" Width="0.5px" Text="%"></asp:TextBox>
                            <asp:TextBox ID="txtCondition" runat="server" Width="0.5px"></asp:TextBox>
                            <asp:TextBox ID="txtParameterID" runat="server" Width="1px"></asp:TextBox>
                            <asp:TextBox ID="txtCreatedBy" runat="server" Width="1px"></asp:TextBox>
                            <asp:TextBox ID="txt_PONO" runat="server" Width="1px"></asp:TextBox>
                            <asp:TextBox ID="txt_ProjectXml" runat="server" Width="1px"></asp:TextBox>
                            <asp:TextBox ID="txt_CREATED_BY" runat="server" Width="1px"></asp:TextBox>
                            <asp:TextBox ID="txt_PO_VALUE" runat="server" Width="1px"></asp:TextBox>
                            <asp:TextBox ID="txt_check_PO_Nos" runat="server" Width="1px"></asp:TextBox>
                            <asp:TextBox ID="txt_dt_count" runat="server" Width="1px"></asp:TextBox>
                            <asp:TextBox ID="txt_ack_value" runat="server" Width="1px"></asp:TextBox>
                            <asp:TextBox ID="app_Path" runat="server" Width="1px"></asp:TextBox>
                            <asp:TextBox ID="txt_sendback_po" runat="server" Width="1px"></asp:TextBox>
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <!-- ================== BEGIN BASE JS ================== -->

        <script src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
        <script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
        <!-- ================== END PAGE LEVEL JS ================== -->

    </form>
</body>
<script language="javascript" type="text/javascript">
    function editdatails() {
        document.getElementById("txt_PONO").value = "";
        document.getElementById("txt_check_PO_Nos").value = "";
        var tbl = document.getElementById("mytable");
        var rowCount = $('#mytable tr').length;
        var vals = "";
        var j = 1;
        for (var i = 1; i < rowCount; i++) {

            var checkboxes = document.getElementById("open_po" + i + "");
            if (checkboxes.checked) {


                document.getElementById("txt_PONO").value += tbl.rows[i].cells[1].innerText + ";";
                document.getElementById("txt_ProjectXml").value += tbl.rows[i].cells[2].innerText + ";";
                if (tbl.rows[i].cells[3].innerText != "0") {
                    document.getElementById("txt_PO_VALUE").value += tbl.rows[i].cells[3].innerText + ";";
                }
                else {
                    document.getElementById("txt_PO_VALUE").value += 0 + ";";
                }

                vals += "," + checkboxes.value;
                document.getElementById("txt_check_PO_Nos").value = j++;

            }
        }
        var updateProgress = $get("<%= UpdateProgress1.ClientID %>");
            updateProgress.style.display = "block";
        }

        var count = $("#txt_dt_count").val();
        $("#div_NewPO").click(function () {
            if (count == 0) {
                $('div#NewPO').attr('id', 'highlight')
                alert("New PO's are not Found");
            } else {
                $("#NewPO").show();
            }
        });
        function viewData(index) {
            try {
                var app_path = $("#app_Path").val();
                var po_val = $("#encrypt_po_" + (index)).val();
                window.open(app_path + '/Common/ViewDocument.aspx?pono=' + po_val + '&type=PO', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
            }
            catch (exception) {
            }
        }

</script>

</html>
