<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DatapoolWeb.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head id="Head1" runat="server">
    <title>Login</title>
    <link href="Styles/StyleSheet1.css" rel="stylesheet"  type="text/css"  />
    <script language="JavaScript" type="text/javascript"> 
    <!--        // 说明：有初始值的 form 表单元素重置(reset)解决方案 
        function clearForm(formName) {
            var formObj = document.forms[formName];
            //var formEl = formObj.elements;
            for (var i = 0; i < formEl.length; i++) {
                var element = formEl[i];
                //if (element.type == 'submit') { continue; }
                if (element.type == 'reset') { continue; }
                if (element.type == 'button') { continue; }
                if (element.type == 'hidden') { continue; }
                if (element.type == 'text') { element.value = ''; }
                if (element.type == 'textarea') { element.value = ''; }

                if (element.type == 'radio') { element.checked = false; }
                if (element.type == 'select-multiple') { element.selectedIndex = 0; }
                if (element.type == 'select-one') { element.selectedIndex = 0; }
            }
        } 
                  //--> </script>
                  
    <style type="text/css">
        .style1
        {
            width: 207px;
        }
    </style>


</head>
<body>
    <div id="Layer" style="z-index: 2; left: 25%; width: 40%; position: absolute; top: 18%;
        height: 15%">
        <table cellspacing="0" cellpadding="0" align="center" border="0">
            <tbody>
                <tr>
                    <td height="330">
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tbody>
                                <tr>
                                    <td class="status" width="18%">
                                    </td>
                                    <td class="errorMessage" id="errmsg1" valign="center" align="left" width="100%">
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <img src="Resources/Images/login/Login.jpg" width="543">
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div id="Layer2" 
    style="z-index: 2; left: 12%; top: 40%; width: 25%; position: absolute; ">
        <form id="form1" runat="server">
        <table height="20" cellspacing="0" width="450" border="0">
            <tbody>
                <tr>
                    <td class="menu_head" align="middle" colspan="3">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td valign="middle" align="right" style="font-size:13px;">
                         用户名
                    </td>
                    <td valign="middle" align="left" class="style1">
                        <input id="txtUserName" type="text" maxlength="20" style="width:130px" 
                            runat="server" />
                        <asp:RequiredFieldValidator runat="server" ID="rvName" Text="*" 
                            ControlToValidate="txtUserName" meta:resourcekey="rvNameResource1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td valign="middle" align="right" style="font-size:13px;">
                        密&nbsp;码
                    </td>
                    <td valign="middle" class="style1">
                        <input id="txtPassword" type="password" maxlength="20" style="width:130px" runat="server" />
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" 
                            Text="*" ControlToValidate="txtPassword" 
                            meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
              <%-- <tr>
                    <td>
                        &nbsp;</td>
                    <td align="right" style="font-size:13px;">
                    语言<br />
                    <td valign="middle" align="left" class="style1">
                        <asp:DropDownList ID="ddlLanguage" runat="server"  AutoPostBack="true"
                            meta:resourcekey="ddlLanguageResource1" 
                            onselectedindexchanged="ddlLanguage_SelectedIndexChanged">
                            <asp:ListItem meta:resourcekey="ListItemResource1" Value="zh-cn">中文</asp:ListItem>
                            <asp:ListItem meta:resourcekey="ListItemResource2" Value="en-us">English</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>--%>
                <tr>
                    <td>
                    </td>
                    <td align="right">
                    </td>
                    <td valign="top" align="left" class="style1">
                        <asp:Button ID="btnLogin" runat="server" Text="登录" OnClick="btnLogin_Click" />
                        &nbsp;&nbsp;
                        <input id="Button1"  type="button" value="重置" runat="server" name="reset" onclick="clearForm('form1')"/>
                    </td>
                </tr>
            </tbody>
        </table>
        </form>
    </div>
</body>
</html>
