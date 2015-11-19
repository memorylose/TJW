<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test4.aspx.cs" Inherits="TJW.UI.Test4" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title></title>




</head>
<body>
    <form id="form1" runat="server">
        <select id="test1" multiple="multiple"" name="test1" title="Product Interested">
            <option value="Parcel Services (Parcel Post, eParcel)">Parcel Services (Parcel Post,eParcel)</option>
            <option value="Express Services (StarTrack &amp; Express)">Express Services (StarTrack&amp; Express)</option>
            <option value="Bulk Mail (Incl. UMS, Reply Paid)">Bulk Mail (Incl. UMS, Reply Paid)</option>
            <option value="ID&amp;V Services (POSTbillpay)">ID&amp;V Services (POSTbillpay)</option>
            <option value="International Services">International Services</option>
            <option value="SecurePay / PostPay">SecurePay / PostPay</option>
            <option value="StarTrack Courier (Pickup/Delivery)">StarTrack Courier (Pickup/Delivery)</option>
            <option value="Any other product/services">Any other product/services</option>
            <option value="Packaging Solutions (Prepaid, Custom)">Packaging Solutions (Prepaid,Custom)</option>
        </select>
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
    </form>
</body>
</html>
