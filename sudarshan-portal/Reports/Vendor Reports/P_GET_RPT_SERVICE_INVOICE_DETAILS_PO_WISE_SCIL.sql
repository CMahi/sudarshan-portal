USE [UAT_SA_SUDARSHAN]
GO
/****** Object:  StoredProcedure [dbo].[P_GET_RPT_SERVICE_INVOICE_DETAILS_PO_WISE_SCIL]    Script Date: 10/27/2016 11:46:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[P_GET_RPT_SERVICE_INVOICE_DETAILS_PO_WISE_SCIL]
@inVendorCode nvarchar(max),
@inPoNo nvarchar(max),
@inStatus nvarchar(max)
--@inPK_ID nvarchar(20)
--@inFrom as varchar(50),
--@inTo as varchar(50)

AS
BEGIN
--Declare @from as datetime
--Declare @To as datetime

--set @from = CONVERT(datetime,@infrom,6)
--set @To = CONVERT(datetime,@inTo,6)
	
		Select distinct h.PK_SERV_PO_HDR_ID,h.SERV_PO_NO,UNIQUE_ID,isnull(h.SAP_Status,'Transition') as Status,REPLACE(convert(varchar,h.PO_DATE,106),' ','-') as Creation_Date,
		REPLACE(convert(varchar,h.Invoice_Date,106),' ','-') as Invoice_Date,h.INVOICE_NO,h.INVOICE_AMOUNT,h.PO_GV,h.PO_VALUE,
		(select top 1 plant from T_VENDOR_SERVICE_PO_HDR_DTL where FK_PO_NUMBER=h.PO_NUMBER and FK_VENDOR_CODE=h.Vendor_Code 
		order by PK_SERV_PO_DTL_ID) as PLANT from T_VENDOR_SERVICE_PO_HDR h
		where isnull(h.INVOICE_STATUS,'')<>'REJECTED' and 
		h.Vendor_Code=@inVendorCode and h.PO_Number=@inPoNo
		and h.PO_Status like ('%'+@inStatus+'%') 
		--and h.PK_SERV_PO_HDR_ID=@inPK_ID
		--and (Convert(varchar(20),h.Invoice_Date,112) between (Convert(varchar(20),@from,112)) AND (Convert(varchar(20),@To,112)))
		order by 1 desc
END

