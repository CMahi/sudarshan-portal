<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LC_Compliance_Completion.aspx.cs" Inherits="LC_Compliance_Completion" ValidateRequest="false" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <!-- ================== BEGIN BASE CSS STYLE ================== -->
    <meta charset="utf-8" />
    <title>Legal Compliance</title>
    <link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.min.css" rel="stylesheet" />
    <script src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
    <script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
    <!-- ================== END BASE CSS STYLE ================== -->
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <div class="row" style="margin-top: 10px" id="Close_div" runat="server">
            <div class="col-lg-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <h4 class="panel-title"><b>Legal Compliance Completion</b></h4>
                    </div>
                    <div class="panel-body">
                        <div class="col-md-12">
                            <div class="form-horizontal bordered-group">
                                <div class="form-group">
                                    <label class="col-md-2 control-label"><b>Request By</b></label>
                                    <div class="col-md-4 ui-sortable">
                                        <label class="control-label" id="lbl_Requester" runat="server"></label>
                                    </div>
                                    <label class="col-md-2 control-label"><b>Request Date/Time</b></label>
                                    <div class="col-md-4 ui-sortable">
                                        <label class="control-label" id="lbl_Request_Date" runat="server"></label>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-horizontal bordered-group">
                                <div class="form-group">
                                    <label class="col-md-2 control-label"><b>Compliance Category</b></label>
                                    <div class="col-md-4 ui-sortable">
                                        <label class="control-label" id="Lbl_Category" runat="server"></label>
                                    </div>

                                    <label class="col-md-2 control-label"><b>Compliance Name</b></label>
                                    <div class="col-md-4 ui-sortable">
                                        <label class="control-label" id="lbl_Compliance_Name" runat="server"></label>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-horizontal bordered-group">
                                <div class="form-group">
                                    <label class="col-md-2 control-label"><b>Compliance Description </b></label>
                                    <div class="col-md-4 ui-sortable" style="overflow: auto; height: 30px">
                                        <label class="control-label" id="lbl_Cmp_Desc" runat="server"></label>
                                    </div>
                                    <label class="col-md-2 control-label"><b>Compliance Section/Reference  </b></label>
                                    <div class="col-md-4 ui-sortable" style="overflow: auto; height: 30px">
                                        <label class="control-label" id="lbl_Cmp_Section" runat="server"></label>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-horizontal bordered-group">
                                <div class="form-group">
                                  <label class="col-md-2 control-label"><b>Last Date of Submission</b></label>
                                    <div class="col-md-4 ui-sortable">
                                        <label class="control-label" id="lbl_Last_Date" runat="server"></label>
                                    </div>
                                        <label class="col-md-2 control-label"><b>Grace Date</b></label>
                                    <div class="col-md-4 ui-sortable">
                                        <label class="control-label" id="lbl_Grace_Date" runat="server"></label>
                                    </div>

                                </div>
                            </div>
                        </div>

                        <div class="col-md-12" id="Sub_Appr_Div" runat="server" >
                            <div class="form-horizontal bordered-group">
                                <div class="form-group">
                                
                                    <label class="col-md-2 control-label"><b>Submission Date</b></label>
                                    <div class="col-md-4 ui-sortable">
                                        <label class="control-label" id="lbl_Submission" runat="server"></label>
                                    </div>
                                      <label class="col-md-2 control-label"><b>Approver Name</b></label>
                                    <div class="col-md-4 ui-sortable">
                                        <label class="control-label" id="lbl_Approver_Name" runat="server"></label>
                                    </div>

                                </div>
                            </div>
                        </div>

                        
                        <div class="col-md-12">
                            <div class="form-horizontal bordered-group">
                                <div class="form-group">
                                    <label class="col-md-2 control-label" id="Assign_Name" runat="server"><b>Assign To Name</b></label>
                                    <div class="col-md-4 ui-sortable" id="Assign_Value" runat="server">
                                        <label class="control-label" id="lbl_assign_name" runat="server"></label>
                                    </div>
                                    <label class="col-md-2 control-label"><b>Status</b><font color="red">*</font></label>
                                    <div class="col-md-4 ui-sortable">
                                        <asp:DropDownList ID="ddl_Staus" CssClass="form-control" runat="server" Width="70%">
                                            <asp:ListItem Value="0">---Select One---</asp:ListItem>
                                            <asp:ListItem Value="Not Started">Not Started</asp:ListItem>
                                            <asp:ListItem Value="On Hold">On Hold</asp:ListItem>
                                            <asp:ListItem Value="In Process">In Process </asp:ListItem>
                                            <asp:ListItem Value="Completed">Completed</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>

                <div style="display: none">
                    <asp:TextBox ID="txt_amd_by" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtCreatedByEmail" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtProcessID" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtInstanceID" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Step_ID" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Step_Name" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtEmailID" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_row_no" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txtXMLFiles" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Approver" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Compliance_ID" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Request_No" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_wiid" runat="server"></asp:TextBox>
                    <asp:TextBox ID="txt_Request_by" runat="server"></asp:TextBox>
                     <asp:TextBox ID="txt_Category_Id" runat="server"></asp:TextBox>
                     <asp:TextBox ID="txt_Verifyid" runat="server"></asp:TextBox>
                      <asp:TextBox ID="txt_TaskType" runat="server"></asp:TextBox>
                </div>
            </div>

            <div class="col-md-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <h4 class="panel-title"><b>Compliance Documents</b></h4>
                    </div>
                    <div class="panel-body">
                        <div class="panel pagination-danger" id="div_Details" runat="server">
                        </div>
                    </div>
                </div>
            </div>

    <div class="col-md-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <h4 class="panel-title"><b>Remark</b></h4>
                    </div>
                    <div class="panel-body">
                        <div  runat="server">
  <asp:TextBox runat="server" ID="txt_Remarks" CssClass="form-control" TextMode="MultiLine" Width="70%"></asp:TextBox>                        
</div>
                    </div>
                </div>
            </div>
          



            <div class="col-md-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <h4 class="panel-title">Action</h4>
                    </div>
                    <div class="panel-body">
                        <div class="col-md-5"></div>
                        <div class="col-md-7">
                            <asp:Button ID="btn_Approve" runat="server" Text="Approve" class="btn btn-grey btn-rounded m-b-5" OnClick="btn_Approve_Click" />
                            <asp:Button ID="btn_Resubmit" runat="server" Text="Resubmit" class="btn btn-grey btn-rounded m-b-5" OnClick="btn_Resubmit_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <h4 class="panel-title"><b>Audit Details</b></h4>
                    </div>
                    <div class="panel-body">
                        <div class="panel pagination-danger" id="Audit_trail" runat="server">
                        </div>
                    </div>
                </div>
            </div>

            <div>
                <table>
                    <tr>
                        <td colspan="6" align="center">
                            <asp:Button ID="btnTemps" runat="server" Style="display: none" Visible="true" />
                            <asp:ModalPopupExtender ID="MP_Documents" DropShadow="false" runat="server" TargetControlID="btnTemps"
                                PopupControlID="pnlDocuments" BackgroundCssClass="modalBackground" EnableViewState="false" />
                            <asp:Panel ID="pnlDocuments" runat="server" Style="display: block; width: 70%; background-color: White"
                                BorderColor="Red" EnableViewState="false">
                                <asp:UpdatePanel ID="updatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table style="border: solid 2px Black" width="100%">
                                            <tr>
                                                <td align="left">

                                                    <asp:Label ID="Label1" Text="Upload Document" Font-Bold="true"
                                                        ForeColor="#0099cc" runat="server"></asp:Label>
                                                    <table width="100%">
                                                        <tr>
                                                            <td colspan="2" align="right">
                                                                <img src="../../Img/button_cancel.png" alt="Close" style="cursor: pointer;" onclick="hideDocuments();" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td style="width: 28%">Select File
                                                                        </td>
                                                                        <td style="width: 2%">:
                                                                        </td>
                                                                        <td style="width: 80%">
                                                                            <asp:AsyncFileUpload ID="FileUpload1" runat="server" OnClientUploadError="uploadError"
                                                                                OnClientUploadStarted="StartUpload" OnClientUploadComplete="UploadComplete" CompleteBackColor="Lime"
                                                                                UploaderStyle="Traditional" ErrorBackColor="Red" OnUploadedComplete="btnUpload_Click"
                                                                                UploadingBackColor="#66CCFF" EnableViewState="false" ToolTip="Select File" />

                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>

                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <!--End of Document Upload-->
            </div>

            <div id="hiddenContent">
                <asp:Button ID="Button3" runat="server" Style="display: none" OnClientClick="javascript:void(0);" />
                <asp:ModalPopupExtender ID="MP_Loading" DropShadow="false" runat="server" TargetControlID="btnTemps"
                    PopupControlID="pnlPopups" BackgroundCssClass="modalBackground" />
                <asp:Panel ID="pnlPopups" runat="server" Style="display: none; width: 30%; background-color: White"
                    BorderColor="Red">
                    <asp:UpdatePanel ID="updatePanels1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div align="center" style="border: solid 2px Black">
                                <div>
                                    <img alt="Processing" id="img_Progress" src="/Sudharshab-Portal/Img/loading_transparent.gif" />
                                </div>
                                <div>
                                    <font color="red"><b>Request Is Processing</font>
                                </div>
                            </div>
                            </b>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </asp:Panel>
            </div>
        </div>
    </form>
</body>
<script type="text/javascript">
    //  Compliance & Document Details
    $("#ddl_Compliance_Name").change(function () {
        if ($("#ddl_Compliance_Name").val() != "0") {
            var dataString = JSON.stringify({
                FK_Comp_ID: $("#ddl_Compliance_Name").val()
            });

            $.ajax({
                type: "POST",
                url: "LC_Compliance_Completion.aspx/getdata",
                contentType: "application/json; charset=utf-8",
                data: dataString,
                dataType: "json",
                success: function (response) {
                    div_Details.innerHTML = response.d[0];
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }
    });

    // Document Againt Compliance

    function loadDocuments(rowId) {
        document.getElementById('txt_row_no').value = rowId;
        $find("MP_Documents").show();
    }
    function hideDocuments() {
        $find("MP_Documents").hide();
    }

    function uploadError(sender, args) {
        alert(args.get_errorMessage());
        var uploadText = document.getElementById('fileUpload1').getElementsByTagName("input");
        for (var i = 0; i < uploadText.length; i++) {
            if (uploadText[i].type == 'text') {
                uploadText[i].value = '';
            }
        }
    }

    function StartUpload(sender, args) {
        var filename = args.get_fileName();
        filename = filename.replace(/[&\/\\#,+()$~%'":*?<>{} ]/g, "");
        var filelength = filename.filelength;
    }

    function UploadComplete(sender, args) {
        var filename = args.get_fileName();
        filename = filename.replace(/[&\/\\#,+()$~%'":*?<>{} ]/g, "");

        var fileNameSplit = filename.split('.');

        var contentType = args.get_contentType();
        var filelength = args.get_length();
        if (parseInt(filelength) == 0) {
            alert("Sorry cannot upload file ! File is Empty or file does not exist", "VALIDATION");
        }
        else if (parseInt(filelength) > 15000000) {
            alert("Sorry cannot upload file ! File Size Exceeded. Not More Than 15 MB", "VALIDATION");
        }
        else if (contentType == "application/octet-stream" && fileNameSplit[1] == "exe") {
            alert("Kindly Check File Type.", "VALIDATION");
        }
        else {
            addToClientTable(filename, args.get_length());
        }
        var uploadText = document.getElementById('fileUpload1');
        if (uploadText) {
            uploadText = uploadText.getElementsByTagName("input");
            for (var i = 0; i < uploadText.length; i++) {
                if (uploadText[i].type == 'text') {
                    uploadText[i].value = '';
                }
            }
        }
        document.forms[0].target = "";
    }

    function addToClientTable(name, size) {
        var HTML = "";
        var tbl = document.getElementById("CMP_Docs");
        var row = document.getElementById('txt_row_no').value;
        HTML = "<a id='a_downloadfiles" + row + "' style='cursor: pointer' onclick=\"return downloadfiles('" + row + "');\" >" + name + "</a>";
        tbl.rows[row].cells[2].innerHTML = HTML;
        hideDocuments();
    }
    //to download file 
    function downloadfiles(index) {
        var tbl = document.getElementById("CMP_Docs");
        var lastRow = tbl.rows.length;
        window.open('/Sudarshan-Portal/Common/FileDownload.aspx?enquiryno=' + $("#txt_Request_No").val() + '&filename=' + tbl.rows[index].cells[2].innerText + '&filetag=', 'Download', 'left=150,top=100,width=600,height=300,toolbar=no,menubars=no,status=no,scrollbars=yes,resize=no');
    }
    //to Delete file
    function delteDocuments(rowId) {
        var tbl = document.getElementById("CMP_Docs");
        tbl.rows[rowId].cells[2].innerHTML = "";
    }

    //--------------------------END----------------------------//////



    //For Save Validation
    $("#btn_Approve").click(function () {
        if ($("#ddl_Staus").val() == "0") {
            alert('Validation Error:Please Select Status..!');
            document.getElementById("ddl_Staus").focus();
            return false;
        }
        else if ($("#ddl_Staus").val() != "Completed") {
            if ($("#txt_Remarks").val() == "") {
                alert('Validation Error:Please Enter the  Remark..!');
                document.getElementById("txt_Remarks").focus();
                return false;
            }
        }
         if ($("#ddl_Staus").val() == "Completed") {
            var XMLFILE = "|ROWSET||";
            var tbl1 = document.getElementById("CMP_Docs");
            if (tbl1 == null) {
                alert('Validation Error:Please Upload Documents..!');
                return false;
            }
            else {
                var tbl1 = document.getElementById("CMP_Docs");
                var lastRow1 = tbl1.rows.length;
                if (lastRow1 > 1)
                {
                    for (var j = 0; j < lastRow1 - 1; j++)
                    {
                        var name = tbl1.rows[j + 1].cells[1].innerText;
                        var new_name = name.trim();
                        if (tbl1.rows[j + 1].cells[2].innerText == "")
                        {
                            alert("Validation Error:Please Attach Documents At Row " + (j + 1) + " ...!");
                            return false;
                        }
                        else
                        {
                            XMLFILE += "|ROW||";
                            XMLFILE += "|OBJECT_TYPE||LEGAL COMPLIANCE|/OBJECT_TYPE||";
                            XMLFILE += "|OBJECT_VALUE||" + $("#txt_Request_No").val() + "|/OBJECT_VALUE||";
                            XMLFILE += "|FILENAME||" + tbl1.rows[j + 1].cells[2].innerText + "|/FILENAME||";
                            XMLFILE += "|FK_DOC_ID||" + $("#txt_PK_ID"+(j+1)+"").val() + "|/FK_DOC_ID||";
                            XMLFILE += "|/ROW||";                        
                        }
                    }
                    XMLFILE += "|/ROWSET||";
                    document.getElementById("txtXMLFiles").value = XMLFILE;
                    $find("MP_Loading").show();
                }
            }
         }
         else {
             var XMLFILE = "|ROWSET||";
             var tbl1 = document.getElementById("CMP_Docs");
             if (tbl1 != null) {
                 var lastRow1 = tbl1.rows.length;
                 if (lastRow1 > 1) {
                     for (var j = 0; j < lastRow1 - 1; j++) {
                         var name = tbl1.rows[j + 1].cells[1].innerText;
                         var new_name = name.trim();
                         XMLFILE += "|ROW||";
                         XMLFILE += "|OBJECT_TYPE||LEGAL COMPLIANCE|/OBJECT_TYPE||";
                         XMLFILE += "|OBJECT_VALUE||" + $("#txt_Request_No").val() + "|/OBJECT_VALUE||";
                         XMLFILE += "|FILENAME||" + tbl1.rows[j + 1].cells[2].innerText + "|/FILENAME||";
                         XMLFILE += "|FK_DOC_ID||" + $("#txt_PK_ID" + (j + 1) + "").val() + "|/FK_DOC_ID||";
                         XMLFILE += "|/ROW||";
                     }
                     XMLFILE += "|/ROWSET||";
                     document.getElementById("txtXMLFiles").value = XMLFILE;
                     $find("MP_Loading").show();
                 }
             }
     
         }


        return true;
    });

    $("#btn_Resubmit").click(function () {
        if ($("#txt_Remarks").val() == "") {
            alert('Validation Error:Please the Remarks..!');
            document.getElementById("txt_Remarks").focus();
            return false;
        }

        var XMLFILE = "|ROWSET||";
        var tbl1 = document.getElementById("CMP_Docs");
        if (tbl1 != null) {
            var lastRow1 = tbl1.rows.length;
            if (lastRow1 > 1) {
                for (var j = 0; j < lastRow1 - 1; j++) {
                    var name = tbl1.rows[j + 1].cells[1].innerText;
                    var new_name = name.trim();
                    XMLFILE += "|ROW||";
                    XMLFILE += "|OBJECT_TYPE||LEGAL COMPLIANCE|/OBJECT_TYPE||";
                    XMLFILE += "|OBJECT_VALUE||" + $("#txt_Request_No").val() + "|/OBJECT_VALUE||";
                    XMLFILE += "|FILENAME||" + tbl1.rows[j + 1].cells[2].innerText + "|/FILENAME||";
                    XMLFILE += "|FK_DOC_ID||" + $("#txt_PK_ID" + (j + 1) + "").val() + "|/FK_DOC_ID||";
                    XMLFILE += "|/ROW||";
                }
                XMLFILE += "|/ROWSET||";
                document.getElementById("txtXMLFiles").value = XMLFILE;
                $find("MP_Loading").show();
            }
        }
        return true;
    });

</script>
</html>
