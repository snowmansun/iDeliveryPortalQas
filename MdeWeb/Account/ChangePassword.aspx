<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="DatapoolWeb.Account.ChangePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>人员维护</title>
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
                    <asp:Literal ID="Literal8" runat="server" Text="系统管理"></asp:Literal>
                </td>
                <td class="wsd_1rightarrow">&nbsp;
                </td>
                <td class="wsd_titlefont1">
                    <asp:Literal ID="Literal2" runat="server" Text="密码维护"></asp:Literal>
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
                <td class="tablefield" style="width: 30%">
                    原始密码:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtOldPwd" MaxLength="20"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="密码不能为空"
                        ControlToValidate="txtOldPwd"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tablefield" style="width: 30%">
                    新密码:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtNewPwd" MaxLength="20"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="新密码不能为空"
                        ControlToValidate="txtNewPwd"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tablefield" style="width: 30%">
                    确认密码:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtConfirmPwd" MaxLength="20"></asp:TextBox>
                    
                    <asp:CompareValidator ID="CompareValidator1" 
                        ControlToCompare="txtNewPwd" ControlToValidate="txtConfirmPwd" Text="再次输入密码"
                        runat="server" ErrorMessage="确认密码与老密码不一致"></asp:CompareValidator>
                    
                </td>
            </tr>
            <tr>
                <td class="tablefield" style="width: 30%"></td>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" class="wsd_button2" Text="提交" OnClick="btnSubmit_Click" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
