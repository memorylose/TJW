<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MngMember.aspx.cs" Inherits="TJW.UI.TJW_Admin.MngMember" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>会员管理</title>
    <link rel="stylesheet" href="../Styles/AdminCommon.css" />
    <link rel="stylesheet" href="../Styles/Admin.css" />

</head>
<body>
    <form id="form1" runat="server">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" GridLines="None" Style="font-size: small; text-align: center;" Width="60%" ForeColor="#333333" BorderStyle="Solid" BorderWidth="1px" BorderColor="#DDDDDD" AllowPaging="True" OnPageIndexChanged="GridView1_PageIndexChanged" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="10" OnRowDataBound="GridView1_RowDataBound">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="UserName" HeaderText="用户" />
                <asp:BoundField DataField="UserMail" HeaderText="邮箱" />
                <asp:BoundField DataField="CreateDate" HeaderText="创建时间" />
                <asp:BoundField DataField="LastLoginDate" HeaderText="上次登录时间" />
                <asp:BoundField DataField="LoginTimes" HeaderText="登录次数" />
                <asp:BoundField DataField="GradeNum" HeaderText="总积分" />
                <asp:BoundField DataField="VGradeNum" HeaderText="可用积分" />
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <a href="MngMember.aspx?userId=<%#Eval("UserId") %>" style="color: black;">修改积分</a>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="" BorderStyle="Solid" BorderWidth="1px" BorderColor="#DDDDDD" ForeColor="black" HorizontalAlign="left" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>

        <div id="jfModify" runat="server" style="display: none; margin-top: 10px;">



            <div class="aau_div">
                <div class="aau_div_left" style="width:60px;">总积分：</div>
                <div class="aau_div_right">
                    <input type="text" id="txtTotalJf" class="input_text" runat="server" />
                </div>
                <div class="aau_div_left" style="width:70px; margin-left:20px;">可用积分：</div>
                <div class="aau_div_right">
                    <input type="text" id="txtJf" class="input_text" runat="server" />
                </div>
                <div class="clear"></div>
            </div>
            <asp:Button ID="btnJf" runat="server" Text="提交" CssClass="btnUp" OnClick="btnJf_Click" />
        </div>

    </form>
</body>
</html>
