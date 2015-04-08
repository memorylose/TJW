<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MngCustomBH.aspx.cs" Inherits="TJW.UI.TJW_Admin.MngCustomBH" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="../Styles/AdminCommon.css" />
    <link rel="stylesheet" href="../Styles/Admin.css" />
    <script type="text/javascript" src="../Scripts/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/Admin.js"></script>
</head>
<body>
    <form id="form1" runat="server">

        <div class="mngCus_adddiv">
            <div class="mngCus_adddiv_t">添加类别：</div>
            <input type="text" runat="server" id="txtType" class="input_text" style="float: left; margin-right: 10px;" /><asp:Button ID="btnType" runat="server" CssClass="commonBtn" Text="提交" Style="float: left; margin-top: 2px;" OnClick="btnType_Click" />
        </div>
        <div class="mngCus_adddiv">
            <div class="mngCus_adddiv_t">添加编号：</div>
            <asp:DropDownList ID="dpFType" runat="server" CssClass="typeDp"></asp:DropDownList>
            <input type="text" runat="server" id="txtBH" class="input_text" style="float: left; margin-right: 10px;" /><asp:Button ID="btnBH" runat="server" CssClass="commonBtn" Text="提交" Style="float: left; margin-top: 2px;" OnClick="btnBH_Click" />
        </div>
        <div class="clear"></div>

        <div style="margin-top:10px;">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="50%">
                <Columns>
                    <asp:BoundField DataField="CustomName" HeaderText="编号" />
                    <asp:BoundField DataField="TypeName" HeaderText="类别" />
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <a href="MngCustomBH.aspx?editId=<%#Eval("CustomId") %>" style="color:black;">编辑</a> | <a href="MngCustomBH.aspx?delId=<%#Eval("CustomId") %>" onclick="return delcfm('确认要删除吗？')" style="color:black;">删除</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>
