$(document).ready(function () {
    bindReturnAdvance();
});

function bindReturnAdvance()
{
    var selected_val = $("#ddlAdvType").val();
    var username = $("#txt_Username").val();
    Advance_Return_Request.bindReturnAdvance(selected_val, username, callback_Return);
}

function callback_Return(result)
{
    $("#div_InvoiceDetails").html(result.value);
    $('#data-table1').dataTable();
}

