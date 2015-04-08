<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddNews.aspx.cs" Inherits="TJW.UI.TJW_Admin.AddNews" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="../Styles/AdminCommon.css" />
    <link rel="stylesheet" href="../Styles/Admin.css" />
    <script type="text/javascript" src="../Scripts/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/Admin.js"></script>
    <script type="text/javascript" src="../Scripts/nicEdit.js"></script>
    <script type="text/javascript">
        var area1, area2;
        function toggleArea1() {
            area1 = new nicEditor({ fullPanel: true }).panelInstance('myArea1', { hasPanel: true });
        }
        bkLib.onDomLoaded(function () { toggleArea1(); });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <%=Msg %>
        <div id="addId">
            <div class="aau_div">
                <div class="aau_div_left">标题：</div>
                <div class="aau_div_right">
                    <input type="text" id="txtTitle" class="input_text" runat="server" />
                </div>
                <div class="clear"></div>
            </div>
            <div class="aau_div">
                <div class="aau_div_left">首页标题：</div>
                <div class="aau_div_right">
                    <input type="text" id="txtIndexTitle" class="input_text" runat="server" />
                </div>
                <div class="clear"></div>
            </div>

            <div class="aau_div">
                <div class="aau_div_left">摘要：</div>
                <div class="aau_div_right">

                    <asp:TextBox ID="txtzy" runat="server" CssClass="input_mulText" TextMode="MultiLine"></asp:TextBox>
                </div>
                <div class="clear"></div>
            </div>

            <div class="aau_div">
                <div class="aau_div_left">插入图片：</div>
                <div class="aau_div_right">
                    <input type="file" id="fileContents" runat="server" class="fileUp_1" /><asp:Button ID="btnInsertImg" runat="server" Text="确定" CssClass="btnUp" OnClick="btnInsertImg_Click" />
                </div>
                <div class="clear"></div>
            </div>

            <div class="aau_div">
                <div class="aau_div_left">推荐部分：</div>
                <div class="aau_div_right">
                    <asp:CheckBox ID="ckSyNews" runat="server" Text="首页资讯" CssClass="addCloth_ck" />
                    <asp:CheckBox ID="ckTj" runat="server" Text="新闻推荐" CssClass="addCloth_ck" /><asp:CheckBox ID="ckHot" runat="server" Text="热点新闻" CssClass="addCloth_ck" />
                </div>
                <div class="clear"></div>
            </div>
            <div class="aau_div">
                <div class="aau_div_left">内容：</div>
                <div class="aau_div_right">
                    <textarea id="myArea1" runat="server" name="area2" style="width: 800px; height: 600px;"></textarea>
                </div>
                <div class="clear"></div>
            </div>
            <div class="aau_div">
                <div class="aau_div_left" style="width: 92px;">首页图片：</div>
                <div class="MiddleRight">
                    <img id="fileImg" src="" runat="server" />
                    <input type="button" onclick="fileHidden.click()" value="选择文件" class="btnUp" />
                    <input type="file" id="fileHidden" runat="server" class="fileUp" onchange="UploadChange(this.value,this.files[0])" />
                    <input type="text" id="txtImgSrc" runat="server" readonly="true" class="inputText" style="display: none;" />
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
        </div>
    </form>
</body>
</html>
