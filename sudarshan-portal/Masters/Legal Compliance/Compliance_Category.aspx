<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Compliance_Category.aspx.cs" Inherits="Compliance_Category" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <title>Compliance Category Master</title>
    <link href="../../assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../assets/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../assets/css/style.min.css" rel="stylesheet" />
    <script src="../../assets/plugins/jquery/jquery-2.1.1.min.js"></script>
    <script src="../../assets/plugins/bootstrap/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="row" style="margin-top: 10px" id="div_AddCountryDetails">
            <div class="col-lg-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        <h4 class="panel-title"><b>Compliance Category Master</b></h4>
                    </div>
                    <div class="panel-body">
                        <div class="col-md-12">
                            <div class="form-horizontal bordered-group">
                                <div class="form-group">
                                    <label class="col-md-2 control-label"><b>Category Name</b><font color="red">*</font> </label>
                                    <div class="col-md-2 ui-sortable">
                                        <asp:TextBox ID="txt_Cat_Name" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div class="col-md-3"></div>
                        <div class="col-md-6" style="text-align: center">
                            <asp:Button ID="btn_AddNew" runat="server" Text="Add New" class="btn btn-grey btn-rounded m-b-5" OnClick="btn_AddNew_Click" />
                            <asp:Button ID="btnClear" runat="server" Text="Clear" class="btn btn-danger btn-rounded m-b-5" OnClick="btnClear_Click" />
                        </div>
                        <div class="col-md-3"></div>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="CountryPanel">
            <div class="modal-dialog" style="height: auto; width: 50%; margin-left: 25%">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><font color="white">Update Compliance Category Master Details </font></h4>
                    </div>

                    <div class="modal-body" id="div_header" runat="server">
                        <div class="col-md-12">
                            <div class="col-md-2"></div>
                            <div class="col-md-3">
                                <b>Category Name</b>
                            </div>
                            <div class="col-md-5">
                                <asp:TextBox ID="txt_Edit_Cat_Name" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-md-2"></div>
                        </div>
                    </div>
                    <div class="row">&nbsp;</div>
                    <div class="modal-footer">
                        <asp:Button ID="btn_upd" class="btn btn-sm btn-grey btn-rounded m-b-5" Text="Update" OnClick="btn_Edit_onClick" runat="server"></asp:Button>
                        <a href="javascript:;" class="btn btn-sm btn-danger btn-rounded m-b-5" data-dismiss="modal" id="btnClose">Close</a>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="DeletePanel">
            <div class="modal-dialog" style="height: auto; width: 36%; margin-left: 25%">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h4 class="modal-title"><font color="white">Delete Compliance Category Master Details </font></h4>
                    </div>

                    <div class="modal-body" id="div2" runat="server">
                        <div class="col-md-12">
                            <div class="col-md-2"></div>
                            <div class="col-md-8">
                                  <div class="alert alert-warning" style="width: 100%; text-align: left">Are you sure you want to delete this Record?</div>
                            </div>
                            <div class="col-md-2"></div>
                        </div>
                        <div class="row">&nbsp;</div>
                    </div>
                    <div class="modal-footer">
                          <button id="Button2" align="center" type="button" class="btn btn-sm btn-danger btn-rounded m-b-5" runat="server" onserverclick="btn_Delete_onClick">Yes</button>
                        <a href="javascript:;" class="btn btn-sm btn-danger btn-rounded m-b-5" data-dismiss="modal" id="deleteClose">Close</a>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-12" id="div_ListCountryDetails">
            <div class="panel panel-danger">
                <div class="panel-heading">
                    <h4 class="panel-title"><b>Compliance Category Master Details</b></h4>
                    <div class="btn-group pull-right" style="margin-top:-31px" >
                        <div class="col-md-12">
                            <input id="txt_search"  style="float: right" runat="server" onkeyup="getdata();" type="text" placeholder="Category Name to Search" class="form-control" />
                        </div>
                    </div>

                </div>
                <div class="panel-body">
                    <div class="panel pagination-danger" id="div_Details" runat="server">
                    </div>
                </div>
            </div>
        </div>

        <div style="display: none">
            <asp:TextBox ID="txt_PK_ID" runat="server"></asp:TextBox>
            <asp:TextBox ID="txt_Username" runat="server"></asp:TextBox>
            <asp:TextBox ID="txtEmailID" runat="server"></asp:TextBox>
        </div>
    </form>
</body>
<script type="text/javascript">

    //Validation
    $("#btn_AddNew").click(function () {
        if ($("#txt_Cat_Name").val() == "") {
            alert('Validation Error:Please Enter Category Name..!');
            return false;
        }
    });

    //Search
    function getdata() {
        var Cat_Name = $("#txt_search").val();
        Compliance_Category.bindData("5", Cat_Name, callbackuser_data);
    }

    function callbackuser_data(response) {
        div_Details.innerHTML = response.value;
    }

    //Update
    function bindData(index) {
        $("#txt_Edit_Cat_Name").val($("#txt_cat_name" + index).val());
        $("#txt_PK_ID").val($("#txt_pkid" + index).val());
    }

    //Delete
    function deleteCity(index) {
        $("#txt_PK_ID").val($("#txt_pkid" + index).val());
    }




</script>
</html>

