<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MngCloth.aspx.cs" Inherits="TJW.UI.TJW_Admin.MngCloth" %>

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
        <%=Msg %>
        <div>
            <div class="addBtnDiv">
                <asp:Button ID="Button1" runat="server" Text="添加服饰" CssClass="commonBtn" OnClick="Button1_Click" />
            </div>
            <div class="mng_search_div">
                <input type="text" id="txtSearch" runat="server" class="input_text" style="float: left;" />
                <asp:RadioButtonList ID="rdBtnType" runat="server" CssClass="mng_search_radio" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True">名称</asp:ListItem>
                    <asp:ListItem>条码</asp:ListItem>
                </asp:RadioButtonList>
                <asp:Button ID="btnSearch" runat="server" Text="搜索" CssClass="mng_search_btn" OnClick="btnSearch_Click" />
                <asp:Button ID="btnAll" runat="server" Text="全部" CssClass="mng_search_btn" OnClick="btnAll_Click" />
            </div>

            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" GridLines="None" Style="font-size: small; text-align: center;" Width="60%" BorderStyle="Solid" BorderWidth="1px" BorderColor="#DDDDDD" AllowPaging="True" OnPageIndexChanged="GridView1_PageIndexChanged" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="20" OnRowDataBound="GridView1_RowDataBound">
                <AlternatingRowStyle BackColor="White"/>
                <Columns>
                    <asp:BoundField DataField="ClothName" HeaderText="衣服名称" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" />
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <a href="MngChildCloth.aspx?cCloth=<%#Eval("ClothGuid") %>" class="clothtype_a">管理</a><a class="clothtype_aLine">|</a><a href="MngClothPicture.aspx?clothGUID=<%#Eval("ClothGuid") %>" class="clothtype_a">添加图片</a><a class="clothtype_aLine"><a class="clothtype_aLine">|</a></a><a href="MngCloth.aspx?delId=<%#Eval("ClothGuid") %>" onclick="return delcfm('确认删除吗？')" class="clothtype_a">删除</a>
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
        </div>
    </form>
</body>
</html>
