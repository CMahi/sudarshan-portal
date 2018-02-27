<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" Async="true"
    CodeFile="Service_PO_Approval.aspx.cs" Inherits="Service_PO_Approval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Service PO Approval</title>
    <link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/bootstrap-datepicker/css/datepicker.css" rel="stylesheet" />
    <link href="../../assets/plugins/bootstrap-datepicker/css/datepicker3.css" rel="stylesheet" />
    <link href="../../assets/plugins/bootstrap-calendar/css/bootstrap_calendar.css" rel="stylesheet" />
    <link href="../../assets/plugins/DataTables/media/css/dataTables.bootstrap.min.css" rel="stylesheet" />

</head>
<body style="overflow-x: hidden">
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <div id="divIns" runat="server" style="display: none">
            <div style="background-color: #E6E6E6; position: absolute; top: 0; left: 0; width: 100%; height: 300%; z-index: 1001; -moz-opacity: 0.8; opacity: .80; filter: alpha(opacity=80);">
                <img src="../../images/loading_transparent2.gif" style="background-color: Aqua; position: fixed; top: 40%; left: 46%;" />
            </div>
        </div>
        <div class="modal fade" id="div_user_data">
            <div class="modal-dialog" style="width: 90%; margin-left: 5%">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><font color="white">Requestor's Detail</font></h4>
                    </div>



                    <div class="modal-footer">
                        <a href="javascript:;" class="btn btn-sm btn-danger" data-dismiss="modal">Close</a>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <div class="btn-group pull-right">
                            <%--  <div class="panel-heading-btn">
                                <a href="#div_user_data" data-toggle="modal" title="Requestor's Detail" style="color: blue"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-user"></i></a>
                            </div>--%>
                        </div>
                        <h3 class="panel-title"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-user"></i>SERVICE PO APPROVAL</h3>

                    </div>
                    <div class="panel-body" id="div_hdr">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-md-1"></div>

                                <label class="col-md-2">Request No</label>
                                <div class="col-md-3">
                                    <div id="Div15"><span id="spn_req_no" runat="server"></span></div>
                                </div>

                                <label class="col-md-2">Date</label>
                                <div class="col-md-3">
                                    <div id="Div11"><span id="spn_date" runat="server"></span></div>
                                </div>

                            </div>

                        </div>
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-md-1"></div>

                                <label class="col-md-2">Vendor Name</label>
                                <div class="col-md-3">
                                    <div id="Div2"><span id="spn_vendor_name" runat="server"></span></div>
                                </div>

                                <label class="col-md-2">Vendor Code</label>
                                <div class="col-md-3">
                                    <div id="Div3"><span id="spn_vendor_code" runat="server"></span></div>
                                </div>

                            </div>

                        </div>
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-md-1"></div>

                                <label class="col-md-2">Invoice NO.</label>
                                <div class="col-md-3">
                                    <div id="Div5"><span id="spn_invoice_no" runat="server"></span></div>
                                </div>

                                <label class="col-md-2">Invoice Date</label>
                                <div class="col-md-3">
                                    <div id="Div4"><span id="spn_invoice_date" runat="server"></span></div>
                                </div>

                            </div>

                        </div>
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-md-1"></div>

                                <label class="col-md-2">Invoice Amount</label>
                                <div class="col-md-3">
                                    <div id="Div6"><span id="Span_invoice_amount" runat="server"></span></div>
                                </div>

                                <label class="col-md-2">Delivery Note</label>
                                <div class="col-md-3">
                                    <div id="Div9"><span id="spn_delvr_note" runat="server"></span></div>
                                </div>

                            </div>

                        </div>
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-md-1"></div>

                                <label class="col-md-2">Service From Date</label>
                                <div class="col-md-3">
                                    <div id="Div12"><span id="Span_From" runat="server"></span></div>
                                </div>

                                <label class="col-md-2">Service To Date</label>
                                <div class="col-md-3">
                                    <div id="Div13"><span id="Span_To" runat="server"></span></div>
                                </div>

                            </div>

                        </div>
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-md-1"></div>

                                <label class="col-md-2">Service Location</label>
                                <div class="col-md-3">
                                    <div id="Div8"><span id="Span_Location" runat="server"></span></div>
                                </div>

                                <label class="col-md-2">Remark</label>
                                <div class="col-md-3">
                                    <div id="Div10"><span id="Span_Remark" runat="server"></span></div>
                                </div>

                            </div>

                        </div>
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-md-1"></div>

                                <label class="col-md-2">Supporting Documents</label>
                                <div class="col-md-3">
                                    <a href="#div_UploadDocument" data-toggle="modal">
                                        <img id="A_FileUpload1" src="../../images/attachment.png" alt="Click here to attach file." height="20" width="20" /></a>

                                </div>



                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12" id="div_Local">
                <div class="panel panel-grey">
                    <div class="panel-heading">

                        <h3 class="panel-title"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-user"></i>Service PO Details</h3>

                    </div>
                    <div class="panel-body" id="div14">
                        <div class="form-horizontal" id="div_Header" runat="server">

                            <%--<div class="table-responsive" style="width: 100%; height:500px;overflow:auto;" id="div_LocalData" runat="server" >--%>
                        </div>

                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-12">
            <div class="panel panel-grey">
                <div class="panel-heading">
                    <h3 class="panel-title"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-thumbs-o-up"></i>Action</h3>
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">

                        <div class="form-group">
                            <div class="col-md-1"></div>
                            <label class="col-md-1">Action</label>
                            <div class="col-md-2">
                                <asp:DropDownList ID="ddlAction" runat="server" class="form-control input-sm" onchange="hide_show_remark()"></asp:DropDownList>
                            </div>
                            <label class="col-md-1"  id="lbl_rmk">Remark</label>
                            <div class="col-md-6"  id="div_remark">
                                <textarea type='text' class="form-control" cols="10" rows="2" id="txtRemark" runat="server"></textarea>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-12" id="tab_btn3" runat="server" style="text-align: center;">
                                <asp:Button ID="btnRequest" runat="server" class="btn btn-danger  btn-rounded" Text="Submit" OnClick="btnRequest_Click" OnClientClick="return prepareData()" />
                                <asp:Button ID="btn_Cancel" runat="server" class="btn btn-danger btn-rounded" Text="Cancel" OnClick="imgBtnRelease_Click" />
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
        </div>
        <div style="display: none;" class="modal" id="div_UploadDocument">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Uploaded Document</h5>
                    </div>
                    <div class="modal-body">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <div style="width: 100%" id="divalldata" runat="server"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <a href="javascript:;" class="btn btn-danger btn-rounded width-100" data-dismiss="modal">Close</a>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-12" style="margin-top: 10px;" id="div1">
            <div class="panel panel-grey">
                <div class="panel-heading">
                    <h4 class="panel-title">Audit Trail</h4>
                </div>
                <div class="panel-body">
                    <div id="div_Audit" runat="server">
                    </div>
                </div>
            </div>
        </div>

        <div style="display: none;" class="modal" id="div_Policy">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Advance Request Policy</h5>
                    </div>
                    <div class="modal-body">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <div id="div7" runat="server"></div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <a href="javascript:;" class="btn btn-danger btn-rounded width-100" data-dismiss="modal">Close</a>
                    </div>
                </div>

            </div>
        </div>

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div id="div_txt" style="display: none" runat="server">
                    <asp:TextBox ID="txt_Username" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Dispatch" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Request" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Condition" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtProcessID" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_StepId" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtInstanceID" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtWIID" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtEmailID" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtApproverEmail" runat="server"></asp:TextBox>
                    <asp:TextBox ID="lnkText" runat="server"></asp:TextBox>
                    <asp:TextBox ID="ddlText" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Action" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Audit" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtDummy" runat="server"></asp:TextBox>
                    <asp:TextBox ID="app_Path" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Remark" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Approver" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_designation" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Approver_Email" runat="server"></asp:TextBox>
                    <asp:TextBox ID="doa" runat="server"></asp:TextBox>
                    <asp:TextBox ID="doa_user" runat="server"></asp:TextBox>
                    <asp:TextBox ID="doa_email" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Mailid" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Initiator" runat="server"></asp:TextBox>
                    <asp:TextBox ID="Init_Email" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_pk_id" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Deviate" runat="server" Text="0"></asp:TextBox>
                    <asp:TextBox ID="txt_adv_type" runat="server" Text=""></asp:TextBox>
                    <asp:TextBox ID="txt_paymode" runat="server" Text="0"></asp:TextBox>
                    <asp:TextBox ID="txt_location" runat="server" Text=""></asp:TextBox>
                    <asp:TextBox ID="txt_perdaysamt" runat="server" Text=""></asp:TextBox>
                    <asp:TextBox ID="txt_perdaysamt1" runat="server" Text=""></asp:TextBox>
                    <asp:TextBox ID="txt_remark_hdr" runat="server" Text=""></asp:TextBox>
                    <asp:TextBox ID="txt_sameapprover" runat="server" Text=""></asp:TextBox>
                    <asp:TextBox ID="step_name" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Unique_ID" runat="server"></asp:TextBox>

                    <asp:TextBox ID="txt_Vendor" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_PO" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_dtl_rfc" runat="server"></asp:TextBox>
                   
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <!-- ================== BEGIN BASE JS ================== -->
        <script src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
        <script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
        <script src="../../assets/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
        <script src="../../assets/plugins/bootstrap-calendar/js/bootstrap_calendar.min.js"></script>
        <script src="../../assets/plugins/DataTables/media/js/jquery.dataTables.js"></script>
        <script src="../../assets/plugins/DataTables/media/js/dataTables.bootstrap.min.js"></script>
        <script src="../../assets/js/page-table-manage-responsive.demo.min.js"></script>

    </form>

    <script type="text/javascript">
        var rowflag = [];
        $(document).ready(function () {
            var mode = $("#ddl_Payment_Mode").html();
            var adva = $("#span_advance").html();
            if (adva == "Other Expense Advance") {
                $("#divpol").hide();
            }
            else {
                $("#divpol").show();
            }

            if (mode == "BANK") {
                $("#div_payment").hide();
            }
            else {
                $("#div_payment").show();
            }

            if (adva == "Foreign Travel Advance") {

                $("#div_payment").hide();
                $("#div_payment_mode").hide();
            }

        });

        function prepareData() {
            var act = document.getElementById("ddlAction").value;
            if (act == "") {
                alert("Please Select Action first...!");
                return false;
            }
            var remrk = document.getElementById("txtRemark").value;
            if (act != "Approve") {
                if (remrk == "") {
                    alert("Please Enter Remark...!");
                    return false;
                }
            }
            $("#divIns").show();
            return true;
        }

        function hide_show_remark() {
            $("#txt_Remark").val("");
            if ($("#ddlAction option:selected").index() < 2) {
                $("#div_remark").hide();
                $("#lbl_rmk").hide();
            }
            else {
               $("#div_remark").show();
                $("#lbl_rmk").show();
           }
	 //var remrk = document.getElementById("txtRemark").value;
            //if (act == "Reject") {
             //   if (remrk == "") {
             //       alert("Please Enter Remark...!");
             //       return false;
             //   }
           // }
        }

        g_serverpath = '/Sudarshan-Portal';
          function downloadfiles(index) {

            var tbl = document.getElementById("uploadTable");
            var Vendorcode = document.getElementById("txt_Vendor").value;
            var reqno = document.getElementById("txt_Request").value;

            window.open('../../Common/FileDownload.aspx?enquiryno=' + reqno + '&filename=' + tbl.rows[index].cells[2].innerText + '&filetag=ServicePO&vendor=' + Vendorcode + '', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
        }


        function imgChange(id, reqno, pkservid) {

            var val = id;
            if (rowflag.indexOf(val) == -1) {
                rowflag.push(val);

                pttab = id;
                var div = document.getElementById(reqno + 'NewExpand' + id);
                var img = document.getElementById(reqno + 'NewimgExpand' + id);
                var div1 = document.getElementById(reqno + 'NewExpand1' + id);

                if (div.style.display == "none") {
                    div.style.display = "";
                    div1.style.display = "";

                    Service_PO_Approval.getservicepodtl(reqno, id, pkservid, callback_table);
                    // img.src = "../../Img/minus.gif";
                }
                else {
                    div.style.display = "none";
                    div1.style.display = "none";
                    // img.src = "../../Img/MoveUp.gif";
                }
            }
            else {
                pttab = id;
                var div = document.getElementById(reqno + 'NewExpand' + id);
                var img = document.getElementById(reqno + 'NewimgExpand' + id);
                var div1 = document.getElementById(reqno + 'NewExpand1' + id);
                if (div.style.display == "none") {
                    div.style.display = "";
                    div1.style.display = "";

                }
                else {
                    div.style.display = "none";
                    div1.style.display = "none";

                }
            }
        }

        function callback_table(response) {

            var PK_PRID = response.value.split('||');
            document.getElementById(PK_PRID[1] + 'NewExpand' + pttab).innerHTML = PK_PRID[0];

        }

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
</body>
</html>

