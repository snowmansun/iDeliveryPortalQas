<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysUserEdit.aspx.cs" Inherits="DatapoolWeb.View.Users.SysUserEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>系统用户编辑</title>
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
                    <asp:Literal ID="Literal3" runat="server" Text="系统用户编辑"></asp:Literal>
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
                <td colspan="6">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="tablefield" style="width: 30%">
                    账号:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtCode" MaxLength="20" Enabled ="false"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCode" Text="*" ErrorMessage="请选择有账号的用户！"
                        ID="RequiredFieldValidator5"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tablefield" style="width: 30%">
                    <span class="keyword">*</span>
                    开通登录用户:
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlPersons" AutoPostBack="True" OnSelectedIndexChanged="ddlPersons_SelectedIndexChanged"></asp:DropDownList>
                     <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlPersons" Text="*" ErrorMessage="请选择用户！"
                        ID="RequiredFieldValidator1"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tablefield" style="width: 30%">
                    <span class="keyword">*</span>
                    密码:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtPwd" MaxLength="20"></asp:TextBox>
                      <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPwd" Text="*" ErrorMessage="请输入密码！"
                        ID="RequiredFieldValidator2"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tablefield" style="width: 30%">
                    <span class="keyword">*</span>
                    组织:
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlGroup"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="tablefield" style="width: 30%">
                    <span class="keyword">*</span>
                    是否有效:
                </td>
                <td nowrap="nowrap">
                    <asp:CheckBox runat="server" ID="chkValid" Checked="true" />
                    <asp:HiddenField runat="server" ID="hidID" />
                    <asp:HiddenField runat="server" ID="hidPwd" />
                    <asp:HiddenField runat="server" ID="hidCode" />
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
