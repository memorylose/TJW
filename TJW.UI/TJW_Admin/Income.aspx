<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Income.aspx.cs" Inherits="TJW.UI.TJW_Admin.Income" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>线上交易总览</title>
    <link rel="stylesheet" href="../Styles/AdminCommon.css" />
    <link rel="stylesheet" href="../Styles/Admin.css" />
    <link rel="stylesheet" href="../Styles/Pager.css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="3" Style="font-size: small; text-align: center;" Width="60%" BorderStyle="None" BorderWidth="1px" BorderColor="#CCCCCC" AllowPaging="True" OnPageIndexChanged="GridView1_PageIndexChanged" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="20" BackColor="White">
            <Columns>
                <asp:BoundField DataField="OrderNumber" HeaderText="订单编号" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Price" HeaderText="付款总额" />
                <asp:BoundField DataField="Grade" HeaderText="使用积分" />
                <asp:BoundField DataField="IncomeSucTime" HeaderText="付款时间" />

                <asp:TemplateField HeaderText="订单状态">
                    <ItemTemplate>
                        <%#SetStatus(Eval("StatusName").ToString()) %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <a href="Income.aspx?detail=<%#Eval("OrderNumber") %>" class="clothtype_a">详细</a><a class="clothtype_aLine">
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" BorderStyle="Solid" BorderWidth="1px" BorderColor="#DDDDDD" ForeColor="#000066" HorizontalAlign="left" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>

        <div class="IncomeDiv" id="detailId" runat="server">

            <div class="aau_div_label">
                <div class="aau_div_left">订单编号：</div>
                <div class="aau_div_right">
                    <label id="lblOrder" runat="server" class="label_text"></label>
                </div>
                <div class="clear"></div>
            </div>


            <div class="aau_div_label">
                <div class="aau_div_left">姓名：</div>
                <div class="aau_div_right">
                    <label id="lblName" runat="server" class="label_text"></label>
                </div>
                <div class="clear"></div>
            </div>

            <div class="aau_div_label">
                <div class="aau_div_left">地址：</div>
                <div class="aau_div_right">
                    <label id="lblAddress" runat="server" class="label_text"></label>
                </div>
                <div class="clear"></div>
            </div>

            <div class="aau_div_label">
                <div class="aau_div_left">邮编：</div>
                <div class="aau_div_right">
                    <label id="lblCode" runat="server" class="label_text"></label>
                </div>
                <div class="clear"></div>
            </div>

            <div class="aau_div_label">
                <div class="aau_div_left">电话：</div>
                <div class="aau_div_right">
                    <label id="lblTel" runat="server" class="label_text"></label>
                </div>
                <div class="clear"></div>
            </div>

            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="3" Style="font-size: small; text-align: center;" Width="600px" BorderStyle="None" BorderWidth="1px" BorderColor="#CCCCCC" AllowPaging="True" OnPageIndexChanged="GridView1_PageIndexChanged" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="20" BackColor="White">
                <Columns>
                    <asp:BoundField DataField="StuffGuid" HeaderText="衣服编号" />

                    <asp:BoundField DataField="StuffName" HeaderText="名称" />
                    <asp:BoundField DataField="Type" HeaderText="类别" />
                    <asp:BoundField DataField="Color" HeaderText="颜色" />
                    <asp:BoundField DataField="Size" HeaderText="尺寸" />
                    <asp:BoundField DataField="BH" HeaderText="编号" />
                    <asp:BoundField DataField="Status" HeaderText="状态" />
                    <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <a href="<%#SendStuff(Eval("StuffGuid").ToString()) %>"><%#Eval("SendStatus") %></a>
                    </ItemTemplate>
                </asp:TemplateField>


                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" BorderStyle="Solid" BorderWidth="1px" BorderColor="#DDDDDD" ForeColor="#000066" HorizontalAlign="left" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
            </asp:GridView>

            <asp:Button ID="Button1" runat="server" Text="发货" CssClass="commonBtn_net" OnClick="Button1_Click" />
        </div>
    </form>
</body>
</html>
