USE [UAT_SA_SUDARSHAN]
GO
/****** Object:  StoredProcedure [dbo].[P_GET_RPT_SERVICE_INVOICE_DETAILS_SCIL]    Script Date: 10/27/2016 11:44:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[P_GET_RPT_SERVICE_INVOICE_DETAILS_SCIL]
@inVendorName nvarchar(max),
@inStatus nvarchar(max),
@inFrom as varchar(50),
@inTo as varchar(50)

AS
BEGIN
Declare @from as datetime
Declare @To as datetime

set @from = CONVERT(datetime,@infrom,6)
set @To = CONVERT(datetime,@inTo,6)
	
	if(@inVendorName <> '' and @inFrom <> '' and @inTo <> '')
		Begin
			select distinct v.Vendor_Name,h.Vendor_Code,h.PO_Number,convert(nvarchar(20),h.PO_DATE,105) as PO_Date,h.PO_VALUE,h.PO_GV,
			(select sum(Invoice_Amount) from T_VENDOR_SERVICE_PO_HDR where PO_Number=h.PO_Number) as Cum_Amount
			from T_VENDOR_SERVICE_PO_HDR h inner join M_Vendor v on v.Vendor_code=h.VENDOR_CODE
			 where isnull(h.INVOICE_STATUS,'')<>'REJECTED' and 
			v.Vendor_Name=@inVendorName and h.PO_Status like ('%'+@inStatus+'%') 
			and (Convert(varchar(20),h.Invoice_Date,112) between (Convert(varchar(20),@from,112)) AND (Convert(varchar(20),@To,112)))
			order by 1 desc
		End

	if(@inVendorName <> '')
		Begin
			select distinct v.Vendor_Name,h.Vendor_Code,h.PO_Number,convert(nvarchar(20),h.PO_DATE,105) as PO_Date,h.PO_VALUE,h.PO_GV,
			(select sum(Invoice_Amount) from T_VENDOR_SERVICE_PO_HDR where PO_Number=h.PO_Number) as Cum_Amount
			from T_VENDOR_SERVICE_PO_HDR h inner join M_Vendor v on v.Vendor_code=h.VENDOR_CODE
			 where isnull(h.INVOICE_STATUS,'')<>'REJECTED' and 
			v.Vendor_Name=@inVendorName and h.PO_Status like ('%'+@inStatus+'%') 
			order by 1 desc
		End

	if(@inFrom <> '' and @inTo <> '')
		Begin
			select distinct v.Vendor_Name,h.Vendor_Code,h.PO_Number,convert(nvarchar(20),h.PO_DATE,105) as PO_Date,h.PO_VALUE,h.PO_GV,
			(select sum(Invoice_Amount) from T_VENDOR_SERVICE_PO_HDR where PO_Number=h.PO_Number) as Cum_Amount
			from T_VENDOR_SERVICE_PO_HDR h inner join M_Vendor v on v.Vendor_code=h.VENDOR_CODE
			 where isnull(h.INVOICE_STATUS,'')<>'REJECTED' and h.PO_Status like ('%'+@inStatus+'%') 
			and (Convert(varchar(20),h.Invoice_Date,112) between (Convert(varchar(20),@from,112)) AND (Convert(varchar(20),@To,112)))
			order by 1 desc
		End

END

