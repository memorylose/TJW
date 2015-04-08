<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddTea.aspx.cs" Inherits="TJW.UI.TJW_Admin.AddTea" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>茶-添加</title>
    <link rel="stylesheet" href="../Styles/AdminCommon.css" />
    <link rel="stylesheet" href="../Styles/Admin.css" />
    <script type="text/javascript" src="../Scripts/nicEdit.js"></script>
    <script type="text/javascript">
        bkLib.onDomLoaded(function () { nicEditors.allTextAreas() });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="aau_div">
            <div class="aau_div_left">茶品名称：</div>
            <div class="aau_div_right">
                <input type="text" id="txtTeaName" class="input_text" runat="server" />
            </div>
            <div class="clear"></div>
        </div>
        <div class="aau_div">
            <div class="aau_div_left">库存：</div>
            <div class="aau_div_right">
                <input type="text" id="txtCount" class="input_text" runat="server" />
            </div>
            <div class="clear"></div>
        </div>
        <div class="aau_div">
            <div class="aau_div_left">茶品进价：</div>
            <div class="aau_div_right">
                <input type="text" id="txtOriPrice" class="input_text" runat="server" />
            </div>
            <div class="clear"></div>
        </div>
        <div class="aau_div">
            <div class="aau_div_left">茶品卖价：</div>
            <div class="aau_div_right">
                <input type="text" id="txtPrice" class="input_text" runat="server" />
            </div>
            <div class="clear"></div>
        </div>
        <div class="aau_div">
            <div class="aau_div_left">茶品类别：</div>
            <div class="aau_div_right">
                <asp:DropDownList ID="dpTeaType" runat="server" CssClass="addCloth_dp">
                </asp:DropDownList>
            </div>
            <div class="clear"></div>
        </div>
        <div class="aau_div">
            <div class="aau_div_left">茶品年份：</div>
            <div class="aau_div_right">
                <asp:DropDownList ID="dpTeaYear" runat="server" CssClass="addCloth_dp">
                    <asp:ListItem Value="1">1年</asp:ListItem>
                    <asp:ListItem Value="2">2年</asp:ListItem>
                    <asp:ListItem Value="3">3年</asp:ListItem>
                    <asp:ListItem Value="4">4年</asp:ListItem>
                    <asp:ListItem Value="5">5年</asp:ListItem>
                    <asp:ListItem Value="6">6年</asp:ListItem>
                    <asp:ListItem>7年</asp:ListItem>
                    <asp:ListItem Value="8">8年</asp:ListItem>
                    <asp:ListItem Value="9">9年</asp:ListItem>
                    <asp:ListItem Value="10">10年</asp:ListItem>
                    <asp:ListItem Value="11">10年以上</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="clear"></div>
        </div>
        <div class="aau_div">
            <div class="aau_div_left">茶品介绍：</div>
            <div class="aau_div_right">

                <textarea id="txtDes" runat="server" name="area2" style="width: 600px; height: 400px;"></textarea>

            </div>
            <div class="clear"></div>
        </div>

        <div class="aau_div">
            <div class="aau_div_left"></div>
            <div class="aau_div_right">
                <asp:Button ID="btnSubmit" runat="server" Text="提交" CssClass="input_btn" OnClick="btnSubmit_Click" />
            </div>
            <div class="clear"></div>
        </div>
    </form>
</body>
</html>
