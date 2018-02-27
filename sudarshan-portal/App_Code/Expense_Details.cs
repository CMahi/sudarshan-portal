using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using AjaxPro;
using FSL.Logging;
using FSL.Controller;
using FSL.Message;
/// <summary>
/// Summary description for Expense_Details
/// </summary>
public class Expense_Details
{
	public Expense_Details()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string Expense_Request_Details(string req_no, string process_name)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            string isData = "";
            DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx","getreqdetails",ref isData,req_no,process_name);
            #region OTHER EXPENSES
            if (process_name == "OTHER EXPENSES")
            { 
                 string pk_id = Convert.ToString(dsData.Tables[0].Rows[0]["pk_other_expense_hdr_id"]);
                string initiator = Convert.ToString(dsData.Tables[0].Rows[0]["user_adid"]);
                sb.Append("<div class='col-md-12'>");
                sb.Append("<div class='panel panel-danger'><div class='panel-heading'>");
                    //<div class="panel-heading-btn">
                    //        <a href="#div_user_data" data-toggle="modal" title="Requestor's Detail" style="color:blue"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-user"></i></a>
                    //    </div>
                sb.Append("<h3 class='panel-title'>OTHER EXPENSE - REQUEST DETAILS</h3></div>");
                sb.Append("<div class='panel-body' id='div_hdr'><div class='form-horizontal'>");
                sb.Append("<div class='form-group'>");
                sb.Append("<div class='col-md-1'></div><label class='col-md-2'>Payment Mode :</label><div class='col-md-2'><div id='Div10'>" + Convert.ToString(dsData.Tables[0].Rows[0]["P_MODE"]) + "</div></div>");
                string location = "NA";
                if (Convert.ToString(dsData.Tables[0].Rows[0]["P_LOCATION"]) != "")
                {
                    location = Convert.ToString(dsData.Tables[0].Rows[0]["P_LOCATION"]);
                }
                sb.Append("<div class='col-md-1'></div><label class='col-md-2'>Payment Location :</label><div class='col-md-2'>" + location + "</div></div>");
                            
                sb.Append("<div class='form-group'>");
                sb.Append("<div class='col-md-1'></div><label class='col-md-2'>Remark :</label><div class='col-md-6'>" + Convert.ToString(dsData.Tables[0].Rows[0]["remark"]) + "</div></div>");
                sb.Append("</div></div></div></div>");
                string adv_Data = fill_OtherAdvanceAmount(initiator, pk_id);
                sb.Append("<div class='col-md-12'><div class='panel panel-grey'><div class='panel-heading'><h4 class='panel-title'>Advance Details</h4></div>");
                sb.Append("<div class='panel-body'><div id='div_Advance' runat='server'>" + adv_Data + "</div></div></div></div>");

                string[] req_Data = fill_OtherRequest_data(dsData.Tables[1]).Split('@');

                sb.Append("<div class='col-md-12'><div class='panel panel-grey'><div class='panel-heading'><div class='panel-heading-btn'>");
				sb.Append("<h4 class='panel-title'>Total Amount : <span id='spn_Total' runat='server'>"+req_Data[0]+"</span></h4></div><h4 class='panel-title'>Expense Details</h4></div>");
                sb.Append("<div class='panel-body'><div id='div_req_data' runat='server' class='table-responsive'>" + req_Data[1] + "</div></div></div></div>");

            }
            #endregion OTHER EXPENSES

            #region TRAVEL EXPENSE
            else if (process_name == "TRAVEL EXPENSE")
            {
                string pk_id = Convert.ToString(dsData.Tables[0].Rows[0]["pk_travel_expense_hdr_id"]);
                string initiator = Convert.ToString(dsData.Tables[0].Rows[0]["user_adid"]);
                string voucher_date = Convert.ToDateTime(dsData.Tables[0].Rows[0]["created_date"]).ToString("dd-MMM-yyyy");
                 sb.Append("<div class='col-md-12'>");
                 sb.Append("<div class='panel panel-danger'><div class='panel-heading'>");
                        //<div class="panel-heading-btn">
                        //    <a href="#div_user_data" data-toggle="modal" title="Requestor's Detail"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-user"></i></a>
                        //</div>
                 sb.Append("<h3 class='panel-title'>DOMESTIC TRAVEL EXPENSE - REQUEST DETAILS</h3></div>");
                 sb.Append("   <div class='panel-body' id='div_hdr'><div class='form-horizontal'>");
                 sb.Append("<div class='form-group'>");
                 sb.Append("<div class='col-md-1'></div><label class='col-md-2'>Request No</label><div class='col-md-3'><div><span id='spn_req_no' runat='server'>" + req_no + "</span></div></div>");
                 sb.Append("<label class='col-md-2'>Date</label><div class='col-md-3'><div><span id='spn_date' runat='server'>" + voucher_date + "</span></div></div><div class='col-md-1'></div></div>");

                 sb.Append("<div class='form-group'>");
                 sb.Append("<div class='col-md-1'></div><label class='col-md-2'>Payment Mode</label><div class='col-md-2'><div><span id='pay_mode' runat='server'>" + Convert.ToString(dsData.Tables[0].Rows[0]["pay_mode"]) + "</span></div></div>");
                 string location = "NA";
                 if (Convert.ToString(dsData.Tables[0].Rows[0]["location_name"]).Trim() != "")
                 {
                     location = Convert.ToString(dsData.Tables[0].Rows[0]["location_name"]);
                 }
                 sb.Append("<div class='col-md-1'></div><label class='col-md-2'>Payment Location</label><div class='col-md-2'><div><span id='pay_location' runat='server'>" + Convert.ToString(dsData.Tables[0].Rows[0]["location_name"]) + "</span></div></div></div>");
                     
                 sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Travel From Date</label><div class='col-md-2'>");
                 sb.Append("<div><span id='travelFromDate' runat='server'>" + Convert.ToDateTime(dsData.Tables[0].Rows[0]["from_date"]).ToString("dd-MMM-yyyy") + "</span></div></div>");
                 sb.Append("<div class='col-md-1'></div><label class='col-md-2'>Travel To Date</label><div class='col-md-2'><div><span id='travelToDate' runat='server'>" + Convert.ToDateTime(dsData.Tables[0].Rows[0]["to_date"]).ToString("dd-MMM-yyyy") + "</span></div>");
                 sb.Append("</div><div class='col-md-1'></div></div>");

                 sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Remark</label><div class='col-md-6'><span id='req_remark' runat='server'>" + Convert.ToString(dsData.Tables[0].Rows[0]["remark"]) + "</span></div>");
                 sb.Append("</div></div></div></div></div>");
                 
                 string adv_details = fill_DomesticAdvanceAmount(initiator,pk_id);
                 sb.Append("<div class='col-md-12'><div class='panel panel-grey'><div class='panel-heading'><h4 class='panel-title'>Advance Details</h4></div>");
                 sb.Append("<div class='panel-body'><div id='div_Advance' runat='server'>" + adv_details + "</div></div></div></div>");

                 string req_Data = get_Journey_Data(Convert.ToDateTime(dsData.Tables[0].Rows[0]["from_date"]).ToString("dd-MMM-yyyy"), Convert.ToDateTime(dsData.Tables[0].Rows[0]["to_date"]).ToString("dd-MMM-yyyy"), req_no, process_name);
                 sb.Append("<div class='col-md-12'><div class='panel panel-grey'><div class='panel-heading'>");

                 sb.Append("<h3 class='panel-title'>Request Details</h3></div>");
                 sb.Append("<div class='panel-body'><div class='panel-group' id='accordion'>" + req_Data + "</div></div></div></div>");
            }
            #endregion TRAVEL EXPENSE

            #region LOCAL CONVEYANCE
            else if (process_name == "LOCAL CONVEYANCE")
            {
                string pk_id = Convert.ToString(dsData.Tables[0].Rows[0]["PK_Local_Conveyance_HDR_Id"]);
                string initiator = Convert.ToString(dsData.Tables[0].Rows[0]["emp_ad_id"]);
                string processid = Convert.ToString(dsData.Tables[0].Rows[0]["FK_PROCESS_ID"]);
                string instanceid = Convert.ToString(dsData.Tables[0].Rows[0]["FK_INSTANCE_ID"]);
                sb.Append("<div class='row'><div class='col-md-12'><div class='panel panel-danger'>");
                sb.Append("<div class='panel-heading'>");
                    //<div class="panel-heading-btn">
                    //        <a href="#div_user_data" data-toggle="modal" title="Requestor's Detail" style="color:blue"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-user"></i></a>
                    //    </div>
                sb.Append("<h3 class='panel-title'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-user'></i>LOCAL CONVEYANCE - REQUEST DETAILS</h3></div>");


                sb.Append("<div class='panel-body' id='div_hdr'><div class='form-horizontal'>");
                sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Payment Mode </label>");
                sb.Append("<div class='col-md-2'>"+Convert.ToString(dsData.Tables[0].Rows[0]["pay_mode"])+"</div>");
                string location = "NA";
                if (Convert.ToString(dsData.Tables[0].Rows[0]["LOCATION_NAME"]) != "")
                {
                    location = Convert.ToString(dsData.Tables[0].Rows[0]["LOCATION_NAME"]);
                }
                sb.Append("<div class='col-md-1'></div><label class='col-md-2'>Payment Location</label><div class='col-md-2'>" + location + "</div></div>");

                sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Remark </label><div class='col-md-6'><div id='Remark'>" + Convert.ToString(dsData.Tables[0].Rows[0]["remark"]) + "</div></div>");
                sb.Append("</div></div></div></div></div>");

                sb.Append("<div class='col-md-12' id='advance_details'><div class='panel panel-grey'><div class='panel-heading'><h4 class='panel-title'>Advance Details</h4></div>");
                
                sb.Append("<div class='panel-body'><div id='div_Advance' runat='server'>"+fill_LocalAdvanceAmount(initiator, pk_id)+"</div></div></div></div>");
                
                sb.Append("<div class='col-md-12' id='div_Local'><div class='panel panel-grey'><div class='panel-heading'>");
                //sb.Append("<div class='panel-heading-btn'><h4 class='panel-title'><a href='#div_policy1' data-toggle='modal'>View Policy</a></h4></div>");
				sb.Append("<h4 class='panel-title'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-mobile-phone'></i>Local Conveyance Approval</h4></div></div>");
				
                sb.Append("<div class='panel-body' id='div7'><div class='table-responsive' style='width: 100%' id='div_LocalData' runat='server'>"+ShowLocalUser(dsData.Tables[1])+"</div></div></div></div>");
            }
            #endregion LOCAL CONVEYANCE

            #region MOBILE DATACARD EXPENSE
            else if (process_name == "MOBILE DATACARD EXPENSE")
            {
                string username = Convert.ToString(dsData.Tables[0].Rows[0]["emp_ad_id"]);
                string processid = Convert.ToString(dsData.Tables[0].Rows[0]["fk_process_id"]);
                string instanceid = Convert.ToString(dsData.Tables[0].Rows[0]["fk_instance_id"]);

                sb.Append("<div class='row'><div class='col-md-12'>");
			    sb.Append("<div class='panel panel-danger'><div class='panel-heading'>");
                //sb.Append("<div class="panel-heading-btn"><a href="#div_user_data" data-toggle="modal" title="Requestor's Detail" style="color:blue"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-user"></i></a></div>");
			    sb.Append("<h3 class='panel-title'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-user'></i>MOBILE DATACARD - REQUEST DETAILS</h3></div>");
				sb.Append("<div class='panel-body' id='div_hdr'><div class='form-horizontal'>");

                sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Payment Mode</label><div class='col-md-3'>" + Convert.ToString(dsData.Tables[0].Rows[0]["pay_mode"]) + "</div>");
                sb.Append("<div class='col-md-1'></div><div id='div_payment'><label class='col-md-2'>Location</label><div class='col-md-3'>" + Convert.ToString(dsData.Tables[0].Rows[0]["LOCATION_NAME"]) + "</div></div></div>");
                
                sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Expense Head</label><div class='col-md-3'>"+ Convert.ToString(dsData.Tables[0].Rows[0]["FK_EXPENSEHEAD_ID"])+"</div>");
                sb.Append("</div></div></div></div></div>");

                if (Convert.ToString(dsData.Tables[0].Rows[0]["FK_EXPENSEHEAD_ID"]) == "DataCard")
                {
                    sb.Append("<div class='col-md-12' id='div_DataCard'><div class='panel panel-grey'><div class='panel-heading'><h4 class='panel-title'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-mobile-phone'></i>DataCard Approval</h4></div>");
                    sb.Append("<div class='panel-body'><div class='form-horizontal'>");
                    sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Mobile No</label><div class='col-md-3'>" + Convert.ToString(dsData.Tables[1].Rows[0]["MOBILE_CARD_NO"]) + "</div>");
                    sb.Append("<label class='col-md-2'>Service Provider</label><div class='col-md-3'>" + Convert.ToString(dsData.Tables[1].Rows[0]["Provider_Name"]) + "</div><div class='col-md-1'></div></div>");

                    sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Year</label><div class='col-md-3'>" + Convert.ToString(dsData.Tables[1].Rows[0]["YEAR"]) + "</div>");
                    sb.Append("<label class='col-md-2'>Month</label><div class='col-md-3'>" + Convert.ToString(dsData.Tables[1].Rows[0]["MonthName"]) + "</div><div class='col-md-1'></div></div>");

                    sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Bill No</label><div class='col-md-3'>" + Convert.ToString(dsData.Tables[1].Rows[0]["BILL_NO"]) + "</div>");
                    sb.Append("<label class='col-md-2'>Bill Date</label><div class='col-md-3'>" + Convert.ToString(dsData.Tables[1].Rows[0]["Bill_Date"]) + "</div><div class='col-md-1'></div></div>");

                    sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Bill Amount</label><div class='col-md-3'>" + Convert.ToString(dsData.Tables[1].Rows[0]["BILL_AMOUNT"]) + "</div>");
                    sb.Append("<label class='col-md-2'>Reimbursement Amount</label><div class='col-md-3'>" + Convert.ToString(dsData.Tables[1].Rows[0]["REIMBURSEMENT_AMOUNT"]) + "</div><div class='col-md-1'></div></div>");

                    sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Tax</label><div class='col-md-3'>" + Convert.ToString(dsData.Tables[1].Rows[0]["TAX"]) + "</div>");
                    sb.Append("</div></div>");
                    sb.Append("</div></div></div>");
               }
               else
               {
                    sb.Append("<div class='col-md-12' id='div_UserMobileA'><div class='panel panel-grey'><div class='panel-heading'>");
                    //sb.Append("<div class="panel-heading-btn"><h4 class='panel-title'><a href="#div_Policy1" data-toggle="modal">View Policy</a></h4></div>");
                    sb.Append("<h4 class='panel-title'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-mobile-phone'></i>Mobile Approval</h4>	</div>");

                    sb.Append("<div class='panel-body' id='div3'>");
                    sb.Append("<div class='table-responsive'><div id='div_userMobile' runat='server'>"+ShowMobileUser(username, processid, instanceid)+"</div></div></div></div></div></div>");
               }
                                
            }
            #endregion MOBILE DATACARD EXPENSE

            #region ADVANCE REQUEST
            else if (process_name == "ADVANCE REQUEST")
            {
                
                sb.Append("<div class='col-md-12'><div class='panel panel-danger'><div class='panel-heading'>");
                        // <div class="btn-group pull-right" >
                        //          <div class="panel-heading-btn">
                        //    <a href="#div_user_data" data-toggle="modal" title="Requestor's Detail" style="color:blue"><i class="fa fa-fw m-r-10 pull-left f-s-18 fa-user"></i></a>
                        //</div>      </div>
                sb.Append("<h3 class='panel-title'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-user'></i>ADVANCE REQUEST DETAILS</h3></div>");
                sb.Append("<div class='panel-body' id='div_hdr'><div class='form-horizontal'>");

                sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Payment Mode </label><div class='col-md-2'>" + Convert.ToString(dsData.Tables[0].Rows[0]["PAY_MODE"]) + "</div>");
                string location="NA";
                if(Convert.ToString(dsData.Tables[0].Rows[0]["LOCATION_NAME"])!="")
                {
                location=Convert.ToString(dsData.Tables[0].Rows[0]["LOCATION_NAME"]);
                }
                sb.Append("<div class='col-md-1'></div><div id='div_payment'><label class='col-md-2'>Payment Location</label><div class='col-md-2'>"+location+"</div></div></div>");

                sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Remark </label><div class='col-md-6'>" + Convert.ToString(dsData.Tables[0].Rows[0]["REMARK"]) + "</div></div>");

                sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Advance For </label><div class='col-md-2'>" + Convert.ToString(dsData.Tables[0].Rows[0]["advance_Type_name"]) + "</div><div class='col-md-1'></div>");
                sb.Append("</div></div></div></div></div>");
       
                sb.Append("<div class='col-md-12' id='div_Local'><div class='panel panel-grey'><div class='panel-heading'>");
                    // <div class="panel-heading-btn" id="divpol" style="display:none">				
                    //    <h4 class='panel-title'><a href="#div_Policy" data-toggle="modal">View Policy</a></h4>
                    //</div>
			    sb.Append("<h4 class='panel-title'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-mobile-phone'></i>Expense Details</h4></div></div>");
				
                sb.Append("<div class='panel-body' id='div7'><div class='table-responsive' style='width: 100%' id='div_LocalData' runat='server'>"+fill_AdvData(Convert.ToString(dsData.Tables[0].Rows[0]["advance_for"]),dsData.Tables[0])+"</div></div></div>");
            }
            #endregion ADVANCE REQUEST

#region CAR POLICY
            else if (process_name == "CAR POLICY")
            {
                DataTable dtHdr = (DataTable)ActionController.ExecuteAction("", "Car_Expense_Approval.aspx", "gethdrdata", ref isData, req_no);
                if (dtHdr != null && dtHdr.Rows.Count > 0)
                {
                    string process_id = Convert.ToString(dtHdr.Rows[0]["FK_PROCESS_ID"]);
                    string instance_id = Convert.ToString(dtHdr.Rows[0]["FK_INSTANCE_ID"]);

                    DataTable dtData = (DataTable)ActionController.ExecuteAction("", "Car_Expense_Approval.aspx", "getpkcarexpns", ref isData, process_id, instance_id);
                    if (dtData != null)
                    {

                        sb.Append("<div class='row'>");
                        sb.Append("<div class='col-md-12'><div class='panel panel-danger'>");
                        sb.Append("<div class='panel-heading'><h3 class='panel-title'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-user'></i>CAR EXPENSE PAYMENT APPROVAL</h3></div>");
                        sb.Append("<div class='panel-body' id='div_hdr'>");
                        sb.Append("<div class='form-horizontal'>");
                        sb.Append("<div class='form-group'>");
                        sb.Append("<div class='col-md-1'></div><label class='col-md-2'>Payment Mode </label>");
                        sb.Append("<div class='col-md-3'><div id='Div7'><span id='span_paymode' runat='server'>"+Convert.ToString(dtData.Rows[0]["PAYMENT_MODE"])+"</span></div></div>");

                        if (dtData.Rows[0]["FK_PAYMENT_MODE"].ToString() == "1")
                        {
                            sb.Append("<div id='div_loc' runat='server'><div class='col-md-1'></div>");
                            sb.Append("<label class='col-md-2'>Payment Location</label><div class='col-md-3'>");
                            sb.Append("<div id='Div11'><span id='span_payloc' runat='server'>" + Convert.ToString(dtData.Rows[0]["LOCATION"]) + "</span></div></div></div>");
                        }
                        else
                        {
                            sb.Append("<div id='div_loc' runat='server' style='display:none'><div class='col-md-1'></div>");
                            sb.Append("<label class='col-md-2'>Payment Location</label><div class='col-md-3'>");
                            sb.Append("<div id='Div11'><span id='span_payloc' runat='server'></span></div></div></div>");
                        }

                        sb.Append("</div>");

                        sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Expense Amount</label>");
                        sb.Append("<div class='col-md-3'><div id='Div8'><span id='expnsamt' runat='server'>" + Convert.ToString(dtData.Rows[0]["EXPENSE_AMOUNT"]) + "</span></div></div>");
                        sb.Append("</div></div></div></div></div>");

                        string ret_data = bindCarData(process_id, instance_id, 1);

                        sb.Append("<div class='col-md-12' id='tab_CarExpense'><div class='panel' style='background-color: #717B85;'>");
                        sb.Append("<ul class='nav nav-tabs'>");
                        sb.Append("<li id='Li7' runat='server' class='active'><a aria-expanded='true' href='#tab_fuel' data-toggle='tab'><b>Fuel</b></a></li>");
                        sb.Append("<li id='Li8' runat='server'><a aria-expanded='true' href='#tab_maitaince' data-toggle='tab'><b>Maintenance</b></a></li>");
                        sb.Append("<li id='Li9' runat='server'><a aria-expanded='true' href='#tab_driver' data-toggle='tab'><b>Driver</b></a></li>");
                        sb.Append("<li id='Li1' runat='server'><a aria-expanded='true' href='#tab_Battery' data-toggle='tab'><b>Battery</b></a></li>");
                        sb.Append("<li id='Li2' runat='server'><a aria-expanded='true' href='#tab_tyre' data-toggle='tab'><b>Tyre</b></a></li>");
                        sb.Append("<li id='Li16' runat='server'><a aria-expanded='true' href='#tab_expense' onclick='getexpensedtl1()' data-toggle='tab'><b>Summary</b></a></li>");
                        sb.Append("<a href='#div_car_policy_data' data-toggle='modal' title='Car Policy Details'  style='color: blue; float:right'><i  class='fa fa-fw m-r-10 pull-left f-s-18 fa-automobile'></i></a></ul>");

                        sb.Append("<div class='tab-content'><div class='tab-pane fade active in' id='tab_fuel'>");
                        sb.Append("<div class='table-responsive'><table id='tbl_Fuel' class='table table-striped table-bordered'>");
                        sb.Append("<tr><td><div id='divfuel' runat='server'>" + ret_data + "</div></td></tr></table></div></div>");

                        ret_data = bindCarData(process_id, instance_id, 2);
                        sb.Append("<div class='tab-pane fade' id='tab_maitaince'><div class='table-responsive'>");
                        sb.Append("<table id='tbl_Maintenance' class='table table-striped table-bordered'><tr><td><div id='divmaintenance' runat='server'>");
			sb.Append(ret_data);
                        sb.Append("</div></td></tr></table></div></div>");

                        ret_data = bindCarData(process_id, instance_id, 3);
                        sb.Append("<div class='tab-pane fade' id='tab_driver'><div class='table-responsive'>");
                        sb.Append("<div class='col-md-2'></div><div class='col-md-8'><table id='tbl_Driver' class='table table-striped table-bordered'>");
                        sb.Append("<tr><td><div id='div_driver' runat='server'>" + ret_data + "</div></td></tr></table></div></div></div>");

                        ret_data = bindCarData(process_id, instance_id, 4);
                        sb.Append("<div class='tab-pane fade' id='tab_Battery'><div class='table-responsive'><div class='col-md-2'></div><div class='col-md-8'>");
                        sb.Append("<table id='tbl_battery' class='table table-striped table-bordered'><tr><td><div id='dv_battery' runat='server'>" + ret_data + "</div>");
                        sb.Append("</td></tr></table></div></div></div>");

                        ret_data = bindCarData(process_id, instance_id, 5);
                        sb.Append("<div class='tab-pane fade' id='tab_tyre'><div class='table-responsive'><div class='col-md-2'></div><div class='col-md-8'>");
                        sb.Append("<table id='tbl_tyre' class='table table-striped table-bordered'><tr><td><div id='dv_tyre' runat='server'>" + ret_data + "</div>");
                        sb.Append("</td></tr></table></div></div></div>");

			ret_data = bindCarData(process_id, instance_id, 6);
 			sb.Append(" <div class='tab-pane fade' id='tab_expense'><div class='table-responsive'><div class='col-md-2'></div>");
                        sb.Append("<div class='col-md-8'>");
//<table id='tbl_expns' class='table table-striped table-bordered'><thead>");
                        //sb.Append("<tr class='grey'><th style='width: 20%'>GL Code</th><th style='width: 20%'>Expense Head</th><th>Amount</th></tr></thead>");
                        sb.Append("<table id='tbl_tyre' class='table table-striped table-bordered'><tr><td><div id='dv_tyre' runat='server'>" + ret_data + "</div>");
                        sb.Append("</td></tr></table></div></div></div>");

                        sb.Append("</table></div></div></div>");

                        sb.Append("</div></div></div></div>");
                    }
                }
            }
            #endregion CAR POLICY
        }
        catch (Exception ex)
        {
            sb.Append("");
        }
        return sb.ToString();
    }

  public string bindCarData(string processid, string instanceid, int param)
    {
        StringBuilder html_Header = new StringBuilder();
        StringBuilder html_Header1 = new StringBuilder();
        StringBuilder html_Header2 = new StringBuilder();
        StringBuilder html_Header3 = new StringBuilder();
        StringBuilder html_Header4 = new StringBuilder();
        StringBuilder html_Header5 = new StringBuilder();
StringBuilder html_Header6 = new StringBuilder();
        string ret_data = "";
        try
        {
            string inco_terms = string.Empty;
            string isdata = string.Empty;
            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Car_Expense_Approval.aspx", "getpkcarexpns", ref isdata, processid, instanceid);
            isdata = "";
            DataSet ds = (DataSet)ActionController.ExecuteAction("", "Car_Expense_Approval.aspx", "getcarexpnsdetails", ref isdata, dt.Rows[0]["PK_CAR_EXPNS_ID"].ToString());


            if (ds != null)
            {
                if (param == 1)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        html_Header.Append("<table class='table table-bordered' id='tblfuel' width='100%'>");
                        html_Header.Append("<thead><tr class='grey'><th>#</th><th style='text-align:center'>Date</th><th style='text-align:center'>Bill Details</th><th style='text-align:center'>Rate</th><th style='text-align:center'>Litre</th><th style='text-align:center'>Amount</th></tr></thead>");
                        html_Header.Append("<tbody>");

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                html_Header.Append("<tr><td>" + (i + 1) + "<input type='text' id='fgl" + (i + 1) + "'  runat='server' Value='" + ds.Tables[0].Rows[i]["sap_glcode"] + "'  style='display:none' /></td><td style='text-align:center'>" + ds.Tables[0].Rows[i]["FUEL_DATE"] + "<input type='hidden' id='txtfueldate" + (i + 1) + "'  runat='server' Value='" + ds.Tables[0].Rows[i]["FUEL_DATE"] + "'  /></td><td style='text-align:center'>" + ds.Tables[0].Rows[i]["PETROL_PUMP"] + "</td><td style='text-align:center'>" + ds.Tables[0].Rows[i]["FUEL_RATE"] + "</td><td style='text-align:center'>" + ds.Tables[0].Rows[i]["FUEL_LITRE"] + "</td><td style='text-align:right'>" + ds.Tables[0].Rows[i]["AMOUNT"] + "<input type='hidden' id='txtfuelamount" + (i + 1) + "'  runat='server' Value='" + ds.Tables[0].Rows[i]["AMOUNT"] + "'  /></td></tr>");
                            }
                        }

                        html_Header.Append("</tbody></table>");
                        ret_data = html_Header.ToString();
                        //txt_glcodef.Text = ds.Tables[0].Rows[0]["SAP_GLCode"].ToString();
                    }
                    else
                    {
                        //Li7.Style["display"] = "none";
                        //dv_polict_f.Style["display"] = "none";
                    }
                }
                else if (param == 2)
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {

                        html_Header1.Append("<table class='table table-bordered' id='tblmaintenance' width='100%'>");
                        html_Header1.Append("<thead><tr class='grey'><th>#</th><th style='text-align:center'>Date</th><th style='text-align:center'>Car Age</th><th style='text-align:center'>Bill Details</th><th style='text-align:center'>Vehical No.</th><th style='text-align:center'>Date Of Purchase</th><th style='text-align:center'>Amount</th></tr>  </thead>");
                        html_Header1.Append("<tbody>");

                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                            {
                                html_Header1.Append("<tr><td>" + (j + 1) + "<input type='text' id='mgl" + (j + 1) + "'  runat='server' Value='" + ds.Tables[1].Rows[j]["sap_glcode"] + "'  style='display:none' /></td><td style='text-align:center'>" + ds.Tables[1].Rows[j]["MAINTAINCE_DATE"] + "<input type='hidden' id='txtmaintenancedate" + (j + 1) + "'  runat='server' Value='" + ds.Tables[1].Rows[j]["MAINTAINCE_DATE"] + "'  /></td><td style='text-align:center'>" + ds.Tables[1].Rows[j]["CAR_AGE"] + "</td><td style='text-align:center'>" + ds.Tables[1].Rows[j]["BILL_DETAILS"] + "</td><td style='text-align:center'>" + ds.Tables[1].Rows[j]["VEHICLE_NO"] + "</td><td style='text-align:center'>" + ds.Tables[1].Rows[j]["DATE_OF_PURCHASE"] + "</td><td style='text-align:right'>" + ds.Tables[1].Rows[j]["AMOUNT"] + "<input type='hidden' id='txtmaintenanceamount" + (j + 1) + "'  runat='server' Value='" + ds.Tables[1].Rows[j]["AMOUNT"] + "'  /></td></tr>");
                            }
                        }

                        html_Header1.Append("</tbody></table>");
                        ret_data = html_Header1.ToString();
                        //txt_glcodem.Text = ds.Tables[1].Rows[0]["SAP_GLCode"].ToString();
                    }
                    else
                    {
                        //Li8.Style["display"] = "none";
                        //dv_polictm.Style["display"] = "none";
                    }
                }
                else if (param == 3)
                {
                    if (ds.Tables[2].Rows.Count > 0)
                    {

                        html_Header2.Append("<table class='table table-bordered' id='tbldriver' width='100%'>");
                        html_Header2.Append("<thead><tr class='grey'><th>#</th><th style='text-align:center'>Type</th><th style='text-align:center'>Date</th><th style='text-align:center'>Amount</th></tr></thead>");
                        html_Header2.Append("<tbody>");

                        if (ds.Tables[2].Rows.Count > 0)
                        {
                            for (int k = 0; k < ds.Tables[2].Rows.Count; k++)
                            {
                                html_Header2.Append("<tr><td>" + (k + 1) + "<input type='text' id='dgl" + (k + 1) + "'  runat='server' Value='" + ds.Tables[2].Rows[k]["sap_glcode"] + "'  style='display:none' /></td><td style='text-align:center'>" + ds.Tables[2].Rows[k]["DRIVER_TYPE"] + "</td><td style='text-align:center'>" + ds.Tables[2].Rows[k]["DATE"] + "<input type='hidden' id='txtdriverdate" + (k + 1) + "'  runat='server' Value='" + ds.Tables[2].Rows[k]["DATE"] + "'  /></td><td style='text-align:right'>" + ds.Tables[2].Rows[k]["AMOUNT"] + "<input type='hidden' id='txtdriveramount" + (k + 1) + "'  runat='server' Value='" + ds.Tables[2].Rows[k]["AMOUNT"] + "'  /></td></tr>");
                            }
                        }

                        html_Header2.Append("</tbody></table>");
                        ret_data = html_Header2.ToString();
                        //txt_glcoded.Text = ds.Tables[2].Rows[0]["SAP_GLCode"].ToString();
                    }
                    else
                    {
                        //Li9.Style["display"] = "none";
                    }
                }
                else if (param == 5)
                {
                    if (ds.Tables[3].Rows.Count > 0)
                    {

                        html_Header4.Append("<table class='table table-bordered' id='tbltyre' width='100%'>");
                        html_Header4.Append("<thead><tr class='grey'><th>#</th><th style='text-align:center'>Date</th><th>Bill Details</th><th style='text-align:center'>KM Threshold Crossed</th><th style='text-align:center'>Kilometers</th><th style='text-align:center'>Amount</th></tr></thead>");
                        html_Header4.Append("<tbody>");

                        if (ds.Tables[3].Rows.Count > 0)
                        {
                            for (int k = 0; k < ds.Tables[3].Rows.Count; k++)
                            {
                                html_Header4.Append("<tr><td>" + (k + 1) + "<input type='text' id='tgl" + (k + 1) + "'  runat='server' Value='" + ds.Tables[3].Rows[k]["sap_glcode"] + "'  style='display:none' /></td><td style='text-align:center'>" + ds.Tables[3].Rows[k]["TYRE_DATE"] + "<input type='hidden' id='txtdate" + (k + 1) + "'  runat='server' Value='" + ds.Tables[3].Rows[k]["TYRE_DATE"] + "'  /></td>");
                                html_Header4.Append("<td style='text-align:center'>" + ds.Tables[3].Rows[k]["BILL_DETAILS"] + "</td>");
                                if (ds.Tables[3].Rows[k]["KM_THREHOLD_CROSEED"].ToString() == "Yes")
                                    html_Header4.Append("<td style='text-align:center'><input id='km_chk" + (k + 1) + "' checked value='' type='checkbox'></td>");
                                else
                                    html_Header4.Append("<td style='text-align:center'><input id='km_chk" + (k + 1) + "' value='' type='checkbox'></td>");
                                html_Header4.Append("<td style='text-align:right'>" + ds.Tables[3].Rows[k]["KILO_METRES"] + "<input type='hidden' id='txtkm" + (k + 1) + "'  runat='server' Value='" + ds.Tables[3].Rows[k]["KILO_METRES"] + "'  /></td><td style='text-align:right'>" + ds.Tables[3].Rows[k]["AMOUNT"] + "<input type='hidden' id='txtamount" + (k + 1) + "'  runat='server' Value='" + ds.Tables[3].Rows[k]["AMOUNT"] + "'  /></td></tr>");
                            }
                        }

                        html_Header4.Append("</tbody></table>");
                        ret_data = html_Header4.ToString();
                        //txt_glcodet.Text = ds.Tables[3].Rows[0]["SAP_GLCode"].ToString();
                    }
                    else
                    {
                        //Li2.Style["display"] = "none";
                    }
                }
                else if (param == 4)
                {
                    if (ds.Tables[4].Rows.Count > 0)
                    {

                        html_Header5.Append("<table class='table table-bordered' id='tblbattery' width='100%'>");
                        html_Header5.Append("<thead><tr class='grey'><th>#</th><th style='text-align:center'>Date</th><th>Bill Details</th><th style='text-align:center'>Amount</th></tr></thead>");
                        html_Header5.Append("<tbody>");

                        if (ds.Tables[4].Rows.Count > 0)
                        {
                            for (int k = 0; k < ds.Tables[4].Rows.Count; k++)
                            {
                                html_Header5.Append("<tr><td>" + (k + 1) + "<input type='text' id='bgl" + (k + 1) + "'  runat='server' Value='" + ds.Tables[4].Rows[k]["sap_glcode"] + "' style='display:none' /></td><td style='text-align:center'>" + ds.Tables[4].Rows[k]["BATTERY_DATE"] + "<input type='hidden' id='txtbdate" + (k + 1) + "'  runat='server' Value='" + ds.Tables[4].Rows[k]["BATTERY_DATE"] + "'  /></td><td style='text-align:center'>" + ds.Tables[4].Rows[k]["BILL_DETAILS"] + "</td><td style='text-align:right'>" + ds.Tables[4].Rows[k]["AMOUNT"] + "<input type='hidden' id='txtbamount" + (k + 1) + "'  runat='server' Value='" + ds.Tables[4].Rows[k]["AMOUNT"] + "'  /></td></tr>");
                            }
                        }

                        html_Header5.Append("</tbody></table>");
                        ret_data = html_Header5.ToString();
                    }
                    else
                    {
                        //Li1.Style["display"] = "none";
                    }
                }
		else if (param == 6)
                {
		    int cal=0;
                    if (ds.Tables[5].Rows.Count > 0)
                    {

                        html_Header6.Append("<table class='table table-bordered' id='tblSummery' width='100%'>");
                        html_Header6.Append("<thead><tr class='grey'><th>#</th><th style='text-align:center'>Expense Head</th><th style='text-align:center'>Amount</th></tr></thead>");
                        html_Header6.Append("<tbody>");

                        if (ds.Tables[5].Rows.Count > 0)
                        {
                            for (int k = 0; k < ds.Tables[5].Rows.Count; k++)
                            {
                                html_Header6.Append("<tr><td>" + (k + 1) + "</td><td style='text-align:center'>" + ds.Tables[5].Rows[k]["Exp_Head"] + "</td><td style='text-align:center'>" + ds.Tables[5].Rows[k]["AMOUNT"] + "</td></tr>");
                            	cal=cal+Convert.ToInt32(ds.Tables[5].Rows[k]["AMOUNT"]);
			    }
                        }
			html_Header6.Append("<tr><td></td><td style='text-align:center'><b>Total Amount </b></td><td style='text-align:center'>" + cal + "</td></tr>");
                        html_Header6.Append("</tbody></table>");
                        ret_data = html_Header6.ToString();
                    }
                    else
                    {
                        //Li1.Style["display"] = "none";
                    }
                }

            }

            
        }
        catch (Exception Exc) { Logger.WriteEventLog(false, Exc); }
        return ret_data;
    }

    public string fill_AdvData(string adv_type, DataTable dt)
    {
        StringBuilder sb = new StringBuilder();
        if (adv_type == "1")
        {
            sb.Append("<div class='col-md-12' id='advance_travel'>");
            sb.Append("<div class='form-horizontal'>");
            sb.Append("<div class='form-group'><div class='col-md-1'></div>");
            sb.Append("<label class='col-md-2'>Place From</label>");
            sb.Append("<div class='col-md-3'>");
            if (Convert.ToString(dt.Rows[0]["from_city"]) == "0" || Convert.ToString(dt.Rows[0]["from_city"]) == "-1" || Convert.ToString(dt.Rows[0]["from_city"]) == "")
            {
                sb.Append(Convert.ToString(dt.Rows[0]["other_f_city"]));
            }
            else
            {
                sb.Append(Convert.ToString(dt.Rows[0]["fcity"]));
            }
            sb.Append("</div>");
            sb.Append("<label class='col-md-2'>Place To</label>");
            sb.Append("<div class='col-md-3'>");
            if (Convert.ToString(dt.Rows[0]["to_city"]) == "0" || Convert.ToString(dt.Rows[0]["to_city"]) == "-1" || Convert.ToString(dt.Rows[0]["to_city"]) == "")
            {
                sb.Append(Convert.ToString(dt.Rows[0]["other_t_city"]));
            }
            else
            {
                sb.Append(Convert.ToString(dt.Rows[0]["tcity"]));
            }
            sb.Append("</div></div>");

            sb.Append("<div class='form-group'><div class='col-md-1'></div> ");
            sb.Append("<label class='col-md-2'>From Date</label>");
            sb.Append("<div class='col-md-3'><div class='input-group' id='Div1'>" + Convert.ToDateTime(dt.Rows[0]["from_date"]).ToString("dd-MMM-yyyy"));
            sb.Append("</div></div>");
            sb.Append("<label class='col-md-2'>To Date</label><div class='col-md-3'><div class='input-group' id='Div6'>" + Convert.ToDateTime(dt.Rows[0]["to_date"]).ToString("dd-MMM-yyyy"));
            sb.Append("</div></div><div class='col-md-1'></div></div>");

            sb.Append("<div class='form-group'><div class='col-md-1'></div>");
            sb.Append("<label class='col-md-2'>Allowed Amount</label><div class='col-md-3'>");

            string isvalid = "";
            string amount = (string)ActionController.ExecuteAction("", "Advance_Request_Approval.aspx", "getadvanceamount", ref isvalid, Convert.ToString(dt.Rows[0]["PK_ADVANCE_HDR_ID"]), Convert.ToString(dt.Rows[0]["to_city"]), Convert.ToString(dt.Rows[0]["grade_name"]));
            if (amount == "")
            {
                amount = "0";
            }
            sb.Append(amount);
            sb.Append("</div>");
            sb.Append("<label class='col-md-2'>Amount</label>");
            sb.Append("<div class='col-md-3'>" + Convert.ToString(dt.Rows[0]["amount"]) + "</div><div class='col-md-1'></div></div>");
           
            sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Remark</label>");
            sb.Append("<div class='col-md-8'>" + Convert.ToString(dt.Rows[0]["remark1"]) + "</div></div>");
            sb.Append("</div></div>");
        }
        else
        {
            sb.Append("<div class='col-md-12' id='advance_other'>");
            sb.Append("<div class='form-horizontal'>");
            sb.Append("<div class='form-group'><div class='col-md-1'></div>");
            sb.Append("<label class='col-md-2'>Advance Date</label>");
            sb.Append("<div class='col-md-3'><div class='input-group' id='Div4'>" + Convert.ToDateTime(dt.Rows[0]["advance_date"]).ToString("dd-MMM-yyyy"));
            sb.Append("</div></div>");
            sb.Append("<label class='col-md-2'>Amount</label><div class='col-md-3'>" + Convert.ToString(dt.Rows[0]["amount"]) + "</div><div class='col-md-1'></div></div>");
            
            string isvalid = "";
            DataTable dtA = (DataTable)ActionController.ExecuteAction("", "Advance_Request_Approval.aspx", "getadvancedeviate", ref isvalid, Convert.ToString(dt.Rows[0]["amount"]));
           
            sb.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Remark</label>");
            sb.Append("<div class='col-md-8'>" + Convert.ToString(dt.Rows[0]["remark1"]) + "</div></div></div></div>");
        }

       return Convert.ToString(sb);
    }

    public string ShowMobileUser(string username, string processid, string instanceid)
    {
        StringBuilder str = new StringBuilder();
        try
        {
            string IsValid = string.Empty;
            string limit = string.Empty;
            string IsData = string.Empty;
            DataTable DTS = (DataTable)ActionController.ExecuteAction("", "Mobile_DataCard_Expense_Approval.aspx", "addetails", ref IsValid, username, processid, instanceid);
            DTS.Columns[0].ColumnName = "Sr.No";
            decimal total_Amount = 0;
            decimal total = 0;
            string suppdoc = string.Empty;
            if (DTS.Rows.Count > 0)
            {
                
                for (int i = 0; i < DTS.Rows.Count; i++)
                {
                    DTS.Rows[i]["Sr.No"] = i + 1;
                }

               
                if (DTS.Rows[0]["TAX"].ToString() != "" && DTS.Rows[0]["REIMBURSEMENT_AMOUNT"].ToString() != "")
                {
                    str.Append("<table id='datatable1'  class='table table-bordered hover'> <thead><tr class='grey'><th>#</th><th>Mobile No</th><th>Service Provider</th><th>Year</th><th>Month</th><th>Bill No</th><th width='20%'>Bill Date</th><th>Bill Amount</th><th>Rei.Amount</th><th>Tax</th><th>Total Reimb.</th><th>Supprting Doc</th><th>Supprting Particulars</th><th>Remark</th></tr></thead>");
                }
                else
                {
                    str.Append("<table id='datatable1'  class='table table-bordered'> <thead><tr class='grey'><th>#</th><th>Mobile No</th><th>Service Provider</th><th>Year</th><th>Month</th><th>Bill No</th><th width='20%'>Bill Date</th><th>Bill Amount</th><th>Rei.Amount</th><th>Supprting Doc</th><th>Supprting Particulars</th><th>Remark</th></tr></thead>");
                }
                str.Append("<tbody>");
                for (int i = 0; i < DTS.Rows.Count; i++)
                {
                    str.Append(" <tr>");
                    str.Append("<td align='left'>" + (i + 1) + "</td>");
                    str.Append("<td align='left'>" + DTS.Rows[i]["MOBILE_CARD_NO"].ToString() + "</td>");
                    str.Append("<td align='left'>" + DTS.Rows[i]["Provider_Name"].ToString() + "</td>");
                    str.Append("<td align='left'>" + DTS.Rows[i]["YEAR"].ToString() + "</td>");
                    str.Append("<td >" + DTS.Rows[i]["MonthName"].ToString() + "</td>");
                    str.Append("<td align='left'>" + DTS.Rows[i]["BILL_NO"].ToString() + "</td>");
                    str.Append("<td align='left'>" + DTS.Rows[i]["Bill_Date"].ToString() + "</td>");
                    str.Append("<td align='right'>" + DTS.Rows[i]["BILL_AMOUNT"].ToString() + "</td>");
                    str.Append("<td align='right'>" + DTS.Rows[i]["REIMBURSEMENT_AMOUNT"].ToString() + "</td>");
                    if (DTS.Rows[0]["TAX"].ToString() != "" && DTS.Rows[0]["REIMBURSEMENT_AMOUNT"].ToString() != "")
                    {
                        total = Convert.ToDecimal(DTS.Rows[i]["REIMBURSEMENT_AMOUNT"]) + Convert.ToDecimal(DTS.Rows[i]["TAX"].ToString());
                        str.Append("<td align='right'>" + DTS.Rows[i]["TAX"].ToString() + "</td>");
                        str.Append("<td align='right'>" + total + "</td>");
                    }
                    if (DTS.Rows[i]["SUPPORT_DOC"].ToString() == "Y")
                    {
                        suppdoc = "Yes";
                    }
                    else if (DTS.Rows[i]["SUPPORT_DOC"].ToString() == "N")
                    {
                        suppdoc = "No";
                    }
                    str.Append("<td>" + suppdoc + "</td>");
                    str.Append("<td align='left'>" + DTS.Rows[i]["SUPPORTING_PARTICULARS"].ToString() + "</td>");
                    str.Append("<td>" + DTS.Rows[i]["REMARK"].ToString() + "</td>");
                    if (DTS.Rows[0]["TAX"].ToString() != "" && DTS.Rows[0]["REIMBURSEMENT_AMOUNT"].ToString() != "")
                    {
                        total_Amount = total_Amount + total;
                    }
                    else
                    {
                        total_Amount = total_Amount + Convert.ToDecimal(DTS.Rows[i]["REIMBURSEMENT_AMOUNT"].ToString());
                    }
                    str.Append("</tr>");
                }
                str.Append("   </tbody>   </table> ");
               
               
            }
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
        return str.ToString();
    }

    public string ShowLocalUser(DataTable dtTable)
    {
        StringBuilder str = new StringBuilder();
        try
        {
            string IsValid = string.Empty;
            dtTable.Columns[0].ColumnName = "Sr.No";
            decimal total_Amount = 0;
            if (dtTable.Rows.Count > 0)
            {
                for (int i = 0; i < dtTable.Rows.Count; i++)
                {
                    dtTable.Rows[i]["Sr.No"] = i + 1;
                }

            
                str.Append("<table id='datatable1'  class='table table-bordered table-hover'> <thead><tr class='grey'><th>#</th><th>Vehicle Type</th><th>From Location</th><th>To Location</th><th>From Date</th><th>To Date</th><th>KMs</th><th>Bill Amount</th></tr></thead>");
                str.Append("<tbody>");
                for (int i = 0; i < dtTable.Rows.Count; i++)
                {
                    string kms = string.Empty;
                    if (dtTable.Rows[i]["KMS"].ToString() == "0")
                    {
                        kms = "";
                    }
                    else
                    {
                        kms = dtTable.Rows[i]["KMS"].ToString();
                    }
                    str.Append(" <tr>");
                    str.Append("<td align='left'>" + (i + 1) + "</td>");
                    str.Append("<td align='left'>" + dtTable.Rows[i]["VEHICLE_TYPE"].ToString() + "</td>");
                    str.Append("<td align='left'>" + dtTable.Rows[i]["FROM_LOACATION"].ToString() + "</td>");
                    str.Append("<td align='left'>" + dtTable.Rows[i]["TO_LOACATION"].ToString() + "</td>");
                    str.Append("<td align='left'>" + dtTable.Rows[i]["From_Date"].ToString() + "</td>");
                    str.Append("<td align='left'>" + dtTable.Rows[i]["To_Date"].ToString() + "</td>");
                    str.Append("<td align='right'>" + kms + "</td>");
                    str.Append("<td align='right'>" + dtTable.Rows[i]["BILL_AMOUNT"].ToString() + "</td>");
                    str.Append("</tr>");
                    total_Amount = total_Amount + Convert.ToDecimal(dtTable.Rows[i]["BILL_AMOUNT"]);
                }

                str.Append("   </tbody>   </table> ");
                   
            }
        }
        catch (Exception ex)
        {
            FSL.Logging.Logger.WriteEventLog(false, ex);
        }
        return str.ToString();
    }

    private string fill_LocalAdvanceAmount(string initiator, string pk_id)
    {
        StringBuilder tblHTML = new StringBuilder();
        string data = string.Empty;
        try
        {
            string isValid = string.Empty;
            DataSet dt = (DataSet)ActionController.ExecuteAction("", "Local_Conveyance.aspx", "pgetadvancedetails", ref isValid, initiator, pk_id, 2);
            
            tblHTML.Append("<table ID='tblAdvance' class='table table-bordered'><thead><tr class='grey'><th>#</th><th>Request No</th><th>Advance Date</th><th>Amount</th><th>Remark</th></tr></thead>");
            tblHTML.Append("<tbody>");
            if (dt != null && dt.Tables[0].Rows.Count > 0)
            {
                for (int Index = 0; Index < dt.Tables[0].Rows.Count; Index++)
                {
                    tblHTML.Append("<tr>");
                    tblHTML.Append("<td>" + (Index + 1) + "</td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["REQUEST_NO"]) + "</td>");
                    tblHTML.Append("<td>" + Convert.ToDateTime(dt.Tables[0].Rows[Index]["ADVANCE_DATE"]).ToString("dd-MMM-yyyy") + "</td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["amount"]) + "</td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["APPROVED"]) + "</td>");
                    tblHTML.Append("</tr>");
                }
            }
            else
            {
                tblHTML.Append("<tr>");
                tblHTML.Append("<td colspan='5' style='text-align:center'>Advance Not Taken</td>");
                tblHTML.Append("</tr>");
            }
            tblHTML.Append("</tbody>");
            tblHTML.Append("</table>");
            
        }
        catch (Exception ex)
        {
            Logger.WriteEventLog(false, ex);
        }
        return tblHTML.ToString();
    }

    private string fill_DomesticAdvanceAmount(string initiator,string pk_id)
    {
        StringBuilder tblHTML = new StringBuilder();
        string data = string.Empty;
        try
        {
            string isValid = string.Empty;
            DataSet dt = (DataSet)ActionController.ExecuteAction("", "Domestic_Travel_Request.aspx", "pgetadvancedetails", ref isValid, initiator, pk_id, 2);
            tblHTML.Append("<table ID='tblAdvance' class='table table-bordered'><thead><tr class='grey'><th>#</th><th>Request No</th><th>Advance Date</th><th>Amount (<i class='fa fa-rupee'></i>)</th><th>Approved By</th></tr></thead>");
            tblHTML.Append("<tbody>");
            if (dt.Tables[0] != null && dt.Tables[0].Rows.Count > 0)
            {
                for (int Index = 0; Index < dt.Tables[0].Rows.Count; Index++)
                {
                    tblHTML.Append("<tr>");
                    tblHTML.Append("<td>" + (Index + 1) + "</td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["REQUEST_NO"]) + "</td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["ADVANCE_DATE"]) + "</td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["amount"]) + "</td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Tables[0].Rows[Index]["Approved"]) + "</td>");
                    tblHTML.Append("</tr>");
                }
            }
            else
            {
                tblHTML.Append("<tr><td colspan='5' align='center'>Advance Not Taken</td></tr>");
            }
            tblHTML.Append("</tbody>");
            tblHTML.Append("</table>");
            

        }
        catch (Exception ex)
        {
            Logger.WriteEventLog(false, ex);
        }
        return tblHTML.ToString();
    }

    public static string get_Journey_Data(string jFDate, string jTDate, string req_no, string process_name)
    {
        string jdata = string.Empty;
        DateTime fdate = Convert.ToDateTime(jFDate);
        DateTime tdate = Convert.ToDateTime(jTDate);
        StringBuilder html = new StringBuilder();
        try
        {
            string IsValid = string.Empty;

            html.Append("<div class='panel'>");
            html.Append("<div class='panel-heading' style='background-color:#94b8b8;border-radius:0px 0px 0px 0px'>");
            html.Append("<div class='panel-heading-btn'></div>");
            html.Append("<h3 class='panel-title'><a class='accordion-toggle' data-toggle='collapse' data-parent='#accordion' href='#collapse0'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i><span id='spnSummary'>Summary</span></a></h3></div>");
            html.Append("<div id='collapse0' class='panel-collapse collapse in'><div class='panel-body' style='background-color:#f0f5f5'><div class='form-horizontal'>");

            html.Append("<div class='form-group'></div>");
            html.Append("<div class='col-md-12'>");

            html.Append("<table class='table table-bordered'>");
            html.Append("<thead><tr class='grey'>");
            html.Append("<th>Voucher Amount (<i class='fa fa-rupee'></i>)</th>");
	    html.Append("<th>Non Required Supporting Amount (<i class='fa fa-rupee'></i>)</th>");
            html.Append("<th>Required Supporting Amount (<i class='fa fa-rupee'></i>)</th>");
            html.Append("<th>Non Supporting Excluded Percentage (%)</th>");
            html.Append("<th>Advance Amount (<i class='fa fa-rupee'></i>)</th>");
            html.Append("<th>Remark (<i class='fa fa-rupee'></i>)</th>");
            html.Append("</tr></thead>");
            html.Append("<tbody><tr>");

            //DataSet dtSummary = (DataSet)ActionController.ExecuteAction("", "Domestic_Travel_Request_Approval.aspx", "pgetsummary", ref IsValid, req_no, "TRAVEL SUMMARY");
            DataSet dtSummary = (DataSet)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "getreqdetails", ref IsValid, req_no, "TRAVEL SUMMARY");
            if (dtSummary != null && dtSummary.Tables[0].Rows.Count > 0)
            {
 		Decimal tot = Convert.ToInt32(dtSummary.Tables[0].Rows[0]["voucher_amount"]);
                Decimal nreq = Convert.ToInt32(dtSummary.Tables[0].Rows[0]["Tot_Req_Supp_Amt"]);
                Decimal req = tot - nreq;
		
                html.Append("<td align='right'>" + Convert.ToString(dtSummary.Tables[0].Rows[0]["voucher_amount"]) + "</td>");
                if(tot!=nreq)
{
		html.Append("<td align='right'>" + Convert.ToDouble(req).ToString("#.00") + "</td>");
}
else
{
		html.Append("<td align='right'>" + Convert.ToString("0.00") + "</td>");
}
                html.Append("<td align='right'>" + Convert.ToString(dtSummary.Tables[0].Rows[0]["Tot_Req_Supp_Amt"]) + "</td>");
                html.Append("<td align='right'>" + Convert.ToString(dtSummary.Tables[0].Rows[0]["Non_Supp_perc"]) + "</td>");
                html.Append("<td align='right'>" + Convert.ToString(dtSummary.Tables[0].Rows[0]["advance_amount"]) + "</td>");
                string[] rem = Convert.ToString(dtSummary.Tables[0].Rows[0]["remark"]).Split('@');
                if (rem[0] == "NIL")
                {
                    html.Append("<td>" + rem[0] + "</td>");
                }
                else
                {
                    html.Append("<td>" + rem[0] + " <i class='fa fa-rupee'></i> " + rem[1] + "</td>");
                }
            }
            else
            {
                html.Append("<td align='right'>0</td><td align='right'>0</td><td align='right'>0</td><td align='right'>0</td><td align='right'>0</td><td align='right'>0</td>");
            }
            html.Append("</tr></tbody></table>");
            html.Append("</div>");



            html.Append("</div></div></div></div>");

            //DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Domestic_Travel_Request_Approval.aspx", "pgetrequestdata", ref IsValid, wiid);
            DataSet dsData = (DataSet)ActionController.ExecuteAction("", "Bulk_Travel_Expense_Doc_Verification.aspx", "getreqdetails", ref IsValid, req_no, process_name);
            if (dsData != null)
            {
                for (int i = 0; i < dsData.Tables[1].Rows.Count; i++)
                {
                    html.Append("<div class='panel'>");
                    html.Append("<div class='panel-heading' style='background-color:#94b8b8;border-radius:0px 0px 0px 0px'>");
                    html.Append("<div class='panel-heading-btn'><div>Amount : <span id='Total" + (i + 1) + "'>" + dsData.Tables[1].Rows[i]["total_amount"] + "</span> <i class='fa fa-rupee'></i></div></div>");
                    html.Append("<h3 class='panel-title'><a class='accordion-toggle' data-toggle='collapse' data-parent='#accordion' href='#collapse" + (i + 1) + "'><i class='fa fa-fw m-r-10 pull-left f-s-18 fa-plus'></i><span id='spn_date_" + (i + 1) + "'>" + Convert.ToDateTime(dsData.Tables[1].Rows[i]["travel_date"]).ToString("dd-MMM-yyyy") + "</span></a></h3></div>");

                    html.Append("<div id='collapse" + (i + 1) + "' class='panel-collapse collapse'><div class='panel-body' style='background-color:#f0f5f5'><div class='form-horizontal'>");

                    html.Append("<div class='form-group'><div class='col-md-1'></div><label class='col-md-2'>Journey Type</label>");
                    html.Append("<div class='col-md-3'><span id='journey_Type" + (i + 1) + "' name='jt'>" + dsData.Tables[1].Rows[i]["journey"] + "</span></div>");
                    html.Append("<div class='col-md-1'></div><label class='col-md-2'>Particulars</label><div class='col-md-3'><span id='remark_note" + (i + 1) + "'>" + dsData.Tables[1].Rows[i]["remark_note"] + "</span></div>");
                    html.Append("</div>");

                    string tr_mode = Convert.ToString(dsData.Tables[1].Rows[i]["travel_name"]);
                    string tr_class = Convert.ToString(dsData.Tables[1].Rows[i]["travel_mapping_class"]);
                    if (Convert.ToString(dsData.Tables[1].Rows[i]["travel_mode"]) == "-1")
                    {
                        tr_mode = "Other";
                        tr_class = "--";
                    }
                    html.Append("<div class='form-group'><div id='div_Travel_Mode" + (i + 1) + "'><div class='col-md-1'></div><label class='col-md-2'>Travel Mode</label>");
                    html.Append("<div class='col-md-3'>" + tr_mode + "</div></div>");

                    html.Append("<div id='div_Travel_Class" + (i + 1) + "'><div class='col-md-1'></div><label class='col-md-2'>Travel Class</label><div class='col-md-3'>");
                    html.Append("<span ID='Travel_Class" + (i + 1) + "' runat='server'>" + tr_class + "</span>");
                    html.Append("</div></div></div>");


                    html.Append("<div class='form-group'><div id='div_FPlant" + (i + 1) + "'><div class='col-md-1'></div><label class='col-md-2'>Plant From</label><div class='col-md-3'>");
                    html.Append("<span ID='From_Plant" + (i + 1) + "' runat='server'>" + dsData.Tables[1].Rows[i]["f_plant"] + "</span>");
                    html.Append("</div></div>");

                    html.Append("<div id='div_TPlant" + (i + 1) + "'><div class='col-md-1'></div><label class='col-md-2'>Plant To</label><div class='col-md-3'>");
                    html.Append("<span ID='To_Plant" + (i + 1) + "' runat='server'>" + dsData.Tables[1].Rows[i]["t_plant"] + "</span>");
                    html.Append("</div></div></div>");

		     html.Append("<div class='form-group'>");
                    if (Convert.ToString(dsData.Tables[1].Rows[i]["journey"]) == "Overnight Stay Within Plant" || Convert.ToString(dsData.Tables[1].Rows[i]["journey"]) == "One Day Outstation Within Plant")
                    {
                        if (Convert.ToString(dsData.Tables[1].Rows[i]["beyond_time"]) == "1")
                        {
                            html.Append("<div id='div_PM" + (i + 1) + "' style='display:normal'><div class='col-md-1'></div><label class='col-md-5'><input type='checkbox' id='chk_reach_" + (i + 1) + "' checked='checked' disabled/>Reached Beyond 10.00 PM?</label></div>");
                        }
                        else
                        {
                            html.Append("<div id='div_PM" + (i + 1) + "' style='display:normal'><div class='col-md-1'></div><label class='col-md-5'><input type='checkbox' id='chk_reach_" + (i + 1) + "' disabled/>Reached Beyond 10.00 PM?</label></div>");
                        }
                    }
                    else
                    {
                        html.Append("<div id='div_PM" + (i + 1) + "' style='display:none'><div class='col-md-1'></div><label class='col-md-5'><input type='checkbox' id='chk_reach_" + (i + 1) + "' disabled/>Reached Beyond 10.00 PM?</label></div>");
                    }

                    if (Convert.ToString(dsData.Tables[1].Rows[i]["journey"]) == "Overnight Stay Within Plant")
                    {
                        if (Convert.ToString(dsData.Tables[1].Rows[i]["Stay_Guest_House"]) == "1")
                        {
                            html.Append("<div id='div_GH" + (i + 1) + "' style='display:normal'><div class='col-md-1'></div><label class='col-md-5'><input type='checkbox' id='chk_guest_" + (i + 1) + "' checked='checked' disabled/>Stay at Guest House?</label></div>");
                        }
                        else
                        {
                            html.Append("<div id='div_GH" + (i + 1) + "' style='display:normal'><div class='col-md-1'></div><label class='col-md-5'><input type='checkbox' id='chk_guest_" + (i + 1) + "' disabled/>Stay at Guest House?</label></div>");
                        }
                    }
                    else
                    {
                        html.Append("<div id='div_GH" + (i + 1) + "' style='display:none'><div class='col-md-1'></div><label class='col-md-5'><input type='checkbox' id='chk_guest_" + (i + 1) + "' disabled/>Stay at Guest House?</label></div>");
                    }
                    html.Append("</div>");

                    html.Append("<div class='form-group' id='div_City" + (i + 1) + "'><div class='col-md-1'></div><label class='col-md-2'>From City</label>");
                    html.Append("<div class='col-md-3'>" + dsData.Tables[1].Rows[i]["f_city"] + "</div>");
                    html.Append("<div class='col-md-1'></div><label class='col-md-2'>To City</label><div class='col-md-3'>" + dsData.Tables[1].Rows[i]["t_city"] + "</div></div>");

                    html.Append("<div class='form-group' id='div_PlaceRoom" + (i + 1) + "'><div class='col-md-1'></div><label class='col-md-2'>Place Class</label>");
                    html.Append("<div class='col-md-3'><span id='placeclass" + (i + 1) + "'>" + dsData.Tables[1].Rows[i]["place_class"] + "</span> Class</div><div class='col-md-1'></div><label class='col-md-2' style='display:none'>Room Type</label>");
                    html.Append("<div class='col-md-3' style='display:none'>" + dsData.Tables[1].Rows[i]["Room_Type"] + "</div></div>");

                    html.Append("<div class='form-group' id='div_HotelContact" + (i + 1) + "'><div class='col-md-1'></div><label class='col-md-2'>Hotel Name</label><div class='col-md-3'>" + dsData.Tables[1].Rows[i]["hotel_name"] + "</div>");
                    html.Append("<div class='col-md-1'></div><label class='col-md-2'>Hotel Contact No</label><div class='col-md-3'>" + dsData.Tables[1].Rows[i]["hotel_no"] + "</div></div>");

                    string trdate = Convert.ToDateTime(dsData.Tables[1].Rows[i]["travel_date"]).ToString("dd-MMM-yyyy");

                    html.Append("<div id='exp_data" + (i + 1) + "'>");

                    html.Append("<div class='form-group'></div>");
                    html.Append("<div class='col-md-12'>");
                    html.Append("<div class='col-md-1'></div>");
                    html.Append("<div class='col-md-10'>");
                    html.Append("<table class='table table-bordered'>");
                    html.Append("<thead><tr class='grey'>");
                    html.Append("<th>Expense Head</th><th>Reimbursement Type</th><th>Amount (<i class='fa fa-rupee'></i>)</th><th>Supporting Doc</th><th>Supporting Particulars</th><th>Remark</th>");
                    html.Append("</tr></thead>");
                    html.Append("<tbody>");

                    for (int j = 0; j < dsData.Tables[2].Rows.Count; j++)
                    {
                        string trdate1 = Convert.ToDateTime(dsData.Tables[2].Rows[j]["fk_travel_date"]).ToString("dd-MMM-yyyy");

                        if (trdate == trdate1)
                        {
                            html.Append("<tr>");
                            html.Append("<td style='text-align:left'>" + dsData.Tables[2].Rows[j]["expense_head"] + "</td>");
                            //html.Append("<td style='text-align:left'>" + dsData.Tables[2].Rows[j]["SAP_GLCode"] + "</td>");
                            html.Append("<td style='text-align:left'>" + dsData.Tables[2].Rows[j]["REIMBURSEMENT_TYPE"] + "</td>");
                            html.Append("<td style='text-align:right'>" + dsData.Tables[2].Rows[j]["exp_amt"] + "</td>");
                            html.Append("<td style='text-align:center'>" + dsData.Tables[2].Rows[j]["supp_doc1"] + "</td>");
                            html.Append("<td style='text-align:center'>" + dsData.Tables[2].Rows[j]["supp_remark"] + "</td>");
                            html.Append("<td style='text-align:center'>" + dsData.Tables[2].Rows[j]["other_remark"] + "</td>");
                            html.Append("</tr>");
                        }
                    }
                    html.Append("</tbody></table>");
                    html.Append("</div>");
                    html.Append("<div class='col-md-1'></div>");
                    html.Append("</div>");
                    html.Append("<div class='form-group'></div>");

                    html.Append("</div>");

                    html.Append("</div></div></div></div></div>");

                }
            }

            jdata = Convert.ToString(html);

        }
        catch (Exception Exc) { FSL.Logging.Logger.WriteEventLog(false, Exc); }
        return jdata;
    }

    private string fill_OtherAdvanceAmount(string initiator,string pk_id)
    {
        StringBuilder tblHTML = new StringBuilder();
        string data = string.Empty;
        try
        {
            string isValid = string.Empty;

            DataTable dt = (DataTable)ActionController.ExecuteAction("", "Other_Expenses_Request.aspx", "pgetadvancedetails", ref isValid, initiator, pk_id, 2, "Other Expense Advance");
            tblHTML.Append("<table ID='tblAdvance' class='table table-bordered'><thead><tr class='grey'><th>#</th><th>Request No</th><th>Advance Date</th><th>Amount</th><th>Approved By</th></tr></thead>");
            tblHTML.Append("<tbody>");
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int Index = 0; Index < dt.Rows.Count; Index++)
                {
                    tblHTML.Append("<tr>");
                    tblHTML.Append("<td><input type='text' id='PK_ADVANCE_ID" + (Index + 1) + "' value='" + Convert.ToString(dt.Rows[Index]["PK_ADVANCE_HDR_ID"]) + "' style='display:none'>" + (Index + 1) + "</td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Rows[Index]["REQUEST_NO"]) + "</td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Rows[Index]["ADVANCE_DATE"]) + "</td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Rows[Index]["amount"]) + "</td>");
                    tblHTML.Append("<td>" + Convert.ToString(dt.Rows[Index]["Approved"]) + "</td>");
                    tblHTML.Append("</tr>");
                }
            }
            else
            {
                tblHTML.Append("<tr><td colspan='5' align='center'>Advance Not Taken</td></tr>");
            }
            tblHTML.Append("</tbody>");
            tblHTML.Append("</table>");
            
        }
        catch (Exception ex)
        {
            Logger.WriteEventLog(false, ex);
        }
        return tblHTML.ToString();
    }

    protected string fill_OtherRequest_data(DataTable dt)
    {
        string DisplayData = string.Empty;
            try
            {
                string isData = string.Empty;
                string isValid = string.Empty;
               
                decimal total_Amount = 0;
                DisplayData = "<table class='table table-bordered' id='tbl_Data'><thead><tr class='grey'> <th style='width:2%;'>#</th><th style='width:10%; text-align:center'>Expense Head</th><th style='width:10%; text-align:center'>GL Code</th><th style='width:10%; text-align:center'>Date</th><th style='width:10%; text-align:center'>Bill No</th><th style='width:10%; text-align:center'>Bill Date</th><th style='width:15%; text-align:center'>Particulars</th><th style='width:5%; text-align:center'>Amount</th></tr></thead>";
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DisplayData += "<tr>";
                        DisplayData += "<td align='center'>" + (i + 1) + "</td>";
                        DisplayData += "<td align='center'>" + Convert.ToString(dt.Rows[i]["EXPENSE_HEAD"]) + "</td>";
                        DisplayData += "<td align='center'>" + Convert.ToString(dt.Rows[i]["SAP_GLCode"]) + "</td>";
                        DisplayData += "<td align='center'>" + Convert.ToString(dt.Rows[i]["date"]) + "</td>";
                        DisplayData += "<td align='center'>" + Convert.ToString(dt.Rows[i]["billno"]) + "</td>";
                        DisplayData += "<td align='center'>" + Convert.ToString(dt.Rows[i]["bill_date"]) + "</td>";
                        DisplayData += "<td align='center'>" + Convert.ToString(dt.Rows[i]["remark"]) + "</td>";
                        DisplayData += "<td style='text-align:right'>" + Convert.ToString(dt.Rows[i]["amount"]) + "</td>";
                        DisplayData += "</tr>";
                        total_Amount = total_Amount + Convert.ToDecimal(dt.Rows[i]["amount"]);
                    }
                }
                DisplayData += "</table>";
                DisplayData=Convert.ToString(total_Amount)+"@"+DisplayData;
            }
            catch (Exception ex)
            {
                FSL.Logging.Logger.WriteEventLog(false, ex);
            }
            return DisplayData;
        }
    
}