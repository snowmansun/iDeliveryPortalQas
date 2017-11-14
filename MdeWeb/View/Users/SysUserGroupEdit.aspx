<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysUserGroupEdit.aspx.cs" Inherits="DatapoolWeb.View.Users.SysUserGroupEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>用户组维护</title>
    <link href="../../Resources/styles/style.css" rel="stylesheet" />
    <script src="../JavaScript/common.js" type="text/javascript"></script>

    <script src="../JavaScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>

    <script src="../Resources/scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

    <script language="JavaScript" type="text/javascript"> 
        <!--        // 说明：有初始值的 form 表单元素重置(reset)解决方案 
    function clearForm(formName) {
        var formObj = document.forms[formName];
        var formEl = formObj.elements;
        for (var i = 0; i < formEl.length; i++) {
            var element = formEl[i];
            if (element.type == 'submit') { continue; }
            if (element.type == 'reset') { continue; }
            if (element.type == 'button') { continue; }
            if (element.type == 'hidden') { continue; }
            if (element.type == 'text') { element.value = ''; }
            if (element.type == 'textarea') { element.value = ''; }

            if (element.type == 'radio') { element.checked = false; }
            if (element.type == 'select-multiple') { element.selectedIndex = 0; }
            if (element.type == 'select-one') { element.selectedIndex = 0; }
        }
    } //-->
    </script>

    <style type="text/css">
        td {
            height: 23px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table id="wsd_title">
            <tr>
                <td class="wsd_titlefont">
                    <asp:Literal ID="Literal1" runat="server" Text="您当前的位置"></asp:Literal>
                </td>
                <td class="wsd_2rightarrow">&nbsp;
                </td>
                <td class="wsd_titlefont2">
                    <asp:Literal ID="Literal2" runat="server" Text="系统管理"></asp:Literal>
                </td>
                <td class="wsd_1rightarrow">&nbsp;
                </td>
                <td class="wsd_titlefont1">
                    <asp:Literal ID="Literal3" runat="server" Text="用户组编辑"></asp:Literal>
                </td>
            </tr>
        </table>
        <br />
        <table id="wsd_inputtable">
            <tr>
                <td colspan="2" class="tabletitle" style="height: 10px">
                    <asp:Literal ID="Literal4" runat="server" Text="输入表单"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <img src="../../Resources/Images/prompt/notice.jpg" />
                    <font color="red" size="2"><b>
                        <asp:Literal ID="Literal6" runat="server" Text="提示：*为必填项！"></asp:Literal>
                    </b></font>
                </td>
            </tr>
            <tr>
                <td class="tablefield" style="width: 30%">
                    <span class="keyword">*</span>
                    用户组编码:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtCODE" MaxLength="20"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="编号不能为空"
                        ControlToValidate="txtCODE"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tablefield" style="width: 30%">
                    <span class="keyword">*</span>
                    有效性:
                </td>
                <td>
                    <asp:CheckBox ID="chkValid" runat="server" Checked="true" />
                    <asp:HiddenField ID="hidID" runat="server" />
                </td>
            </tr>
        </table>
        <br />
        <table id="wsd_inputtable">
            <tr>
                <td width="100%" class="tabletitle">操作
                </td>
            </tr>
            <tr>
                <td class="buttonarea">
                    <asp:Button ID="btnSave" runat="server" class="wsd_button2" Text="保存" OnClick="btnSave_Click" />&nbsp;&nbsp;
                    <input type="button" value="返回" class="wsd_button2" onclick="javascript: history.back(); return false;" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
