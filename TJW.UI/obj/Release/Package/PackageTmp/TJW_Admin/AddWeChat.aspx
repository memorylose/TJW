<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddWeChat.aspx.cs" Inherits="TJW.UI.TJW_Admin.AddWeChat" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>群发微信信息</title>
    <link rel="stylesheet" href="../Styles/AdminCommon.css" />
    <link rel="stylesheet" href="../Styles/Admin.css" />
    <script type="text/javascript" src="../Scripts/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/Admin.js"></script>
    <script type="text/javascript" src="../Scripts/nicEdit.js"></script>
    <script type="text/javascript">
        bkLib.onDomLoaded(function () { nicEditors.allTextAreas() });
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
                <div class="aau_div_left">描述：</div>
                <div class="aau_div_right">
                    <input type="text" id="txtDescribe" class="input_text" runat="server" />
                </div>
                <div class="clear"></div>
            </div>
            <div class="aau_div">
                <div class="aau_div_left">类别：</div>
                <div class="aau_div_right">
                    <asp:DropDownList ID="dpType" runat="server" CssClass="addCloth_dp"></asp:DropDownList>
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
                <div class="aau_div_left">内容：</div>
                <div class="aau_div_right">
                    <textarea id="txtContent" runat="server" name="area2" style="width: 800px; height: 600px;"></textarea>
                </div>
                <div class="clear"></div>
            </div>

            <div class="aau_div">
                <div class="aau_div_left" style="width: 92px;">封面图片：</div>
                <div class="MiddleRight">
                    <img id="fileImg" src="" runat="server" />
                    <input type="button" onclick="fileHidden.click()" value="选择文件" class="btnUp" />
                    <input type="file" id="fileHidden" runat="server" class="fileUp" onchange="UploadChange(this.value,this.files[0])" />
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
