<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCloth.aspx.cs" Inherits="TJW.UI.TJW_Admin.AddCloth" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>服饰添加</title>
    <link rel="stylesheet" href="../Styles/AdminCommon.css" />
    <link rel="stylesheet" href="../Styles/Admin.css" />
    <script type="text/javascript" src="../Scripts/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/Admin.js"></script>
</head>
<body>
    <form id="form1" runat="server">

        <div style="height: 20px;"></div>
        <div class="aau_div">
            <div class="aau_div_left">服饰名称：</div>
            <div class="aau_div_right">
                <input type="text" id="txtClothName" class="input_text" runat="server" />
            </div>
            <div class="clear"></div>
        </div>
        <div class="aau_div">
            <div class="aau_div_left">服饰类别：</div>
            <div class="aau_div_right">
                <asp:DropDownList ID="dpF" runat="server" CssClass="addCloth_dp" AutoPostBack="True" OnSelectedIndexChanged="dpF_SelectedIndexChanged"></asp:DropDownList>
                <asp:DropDownList ID="dpC" runat="server" CssClass="addCloth_dp"></asp:DropDownList>
            </div>
            <div class="clear"></div>
        </div>
        <div class="aau_div">
            <div class="aau_div_left">库存：</div>
            <div class="aau_div_right">
                <input type="text" id="txtStore" class="input_text" runat="server" />
            </div>
            <div class="clear"></div>
        </div>
        <div class="aau_div">
            <div class="aau_div_left">进货价格：</div>
            <div class="aau_div_right">
                <input type="text" id="txtOriPrice" class="input_text" runat="server" />
            </div>
            <div class="clear"></div>
        </div>
        <div class="aau_div">
            <div class="aau_div_left">价格：</div>
            <div class="aau_div_right">
                <input type="text" id="txtPrice" class="input_text" runat="server" />
            </div>
            <div class="clear"></div>
        </div>
        <div class="aau_div">
            <div class="aau_div_left">折扣：</div>
            <div class="aau_div_right">
                <asp:DropDownList ID="dpZK" runat="server" CssClass="addCloth_dp">
                    <asp:ListItem Selected="True" Value="10">无</asp:ListItem>
                    <asp:ListItem Value="9.5">9.5折</asp:ListItem>
                    <asp:ListItem Value="9">9折</asp:ListItem>
                    <asp:ListItem Value="8.5">8.5折</asp:ListItem>
                    <asp:ListItem Value="8">8折</asp:ListItem>
                    <asp:ListItem Value="7.5">7.5折</asp:ListItem>
                    <asp:ListItem Value="7">7折</asp:ListItem>
                    <asp:ListItem Value="6.5">6.5折</asp:ListItem>
                    <asp:ListItem Value="6">6折</asp:ListItem>
                    <asp:ListItem Value="5.5">5.5折</asp:ListItem>
                    <asp:ListItem Value="5">5折</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="clear"></div>
        </div>
        <div class="aau_div">
            <div class="aau_div_left">颜色：</div>
            <div class="aau_div_right">
                <asp:DropDownList ID="dpColor" runat="server" CssClass="addCloth_dp"></asp:DropDownList>
            </div>
            <div class="clear"></div>
        </div>
        <div class="aau_div">
            <div class="aau_div_left">尺寸：</div>
            <div class="aau_div_right">
                <asp:DropDownList ID="dpSize" runat="server" CssClass="addCloth_dp"></asp:DropDownList>
            </div>
            <div class="clear"></div>
        </div>

        <div class="aau_div">
            <div class="aau_div_left">是否上架：</div>
            <div class="aau_div_right">
                <asp:CheckBox ID="ckValid" runat="server" Text="上架" CssClass="addCloth_ck" />
            </div>
            <div class="clear"></div>
        </div>

        <div class="aau_div">
            <div class="aau_div_left">推荐部分：</div>
            <div class="aau_div_right">
                <asp:CheckBox ID="ckRmTop" runat="server" Text="热门推荐大" CssClass="addCloth_ck" /><asp:CheckBox ID="ckRm" runat="server" Text="热门推荐" CssClass="addCloth_ck" />
                <asp:CheckBox ID="ckRxTop" runat="server" Text="热销商品大" CssClass="addCloth_ck" /><asp:CheckBox ID="ckRx" runat="server" Text="热销商品" CssClass="addCloth_ck" />
            </div>
            <div class="clear"></div>
        </div>

        <div class="aau_div">
            <div class="aau_div_left" style="width: 80px;">自定义编号：</div>
            <div class="aau_div_right" style="width: 200px;">
                <input type="checkbox" id="ckCustomBH" runat="server" style="float: left; margin-top: 11px;" onclick="CustomCheck()" /><div style="float: left; margin-top: 5px; margin-right: 5px; padding-top: 5px;">自定义编号</div>
                <div id="cusDp" runat="server" style="display: none">
                    <asp:DropDownList ID="dpCustom" runat="server" CssClass="addCloth_dp"></asp:DropDownList>
                </div>
            </div>
            <div class="clear"></div>
        </div>

        <div class="aau_div">
            <div class="aau_div_left">显示位置：</div>
            <div class="aau_div_right">
                <asp:RadioButtonList ID="rdShow" runat="server" CssClass="addCloth_rd" RepeatDirection="Vertical"></asp:RadioButtonList>

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

        <input type="hidden" id="hdCustomFlag" runat="server" value="0" />

    </form>
</body>
</html>
