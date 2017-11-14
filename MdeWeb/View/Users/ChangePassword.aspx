<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="DatapoolWeb.View.Users.ChangePassword" %>

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

    <script type="text/javascript">
        /*        var rC = {
                    lW: '[a-z]',
                    uW: '[A-Z]',
                    nW: '[0-9]',
                    sW: '[\\u0020-\\u002F\\u003A-\\u0040\\u005B-\\u0060\\u007B-\\u007E]'
                };*/
        var rC = {
            lW: '^[A-Za-z0-9]+$'
        };

        function Reg(str, rStr) {
            var reg = new RegExp(rStr);
            if (reg.test(str)) return true;
            else return false;
        }

        function testPass(str) {

            if (document.getElementById('txtNewPwd').innerText != document.getElementById('txtConfirmPwd').innerText) {
                return false;
            }

            if (str.length == 0) { return false; }
            else if (str.length < 6) {
                document.getElementById('msg').innerText = "密码长度太短";
                //document.getElementById('txtmsg').innerText = '您的密码长度太短';
                return false;
            }
            else {
                /*var tR = {
                    l: Reg(str, rC.lW),
                    u: Reg(str, rC.uW),
                    n: Reg(str, rC.nW),
                    s: Reg(str, rC.sW)
                };
                if ((tR.l && tR.u && tR.n) || (tR.l && tR.u && tR.s) || (tR.s && tR.u && tR.n) || (tR.s && tR.l && tR.n)) {
                    document.getElementById('tt').innerText = '密码符合要求';
                    return true;
                } else {
                    document.getElementById('tt').innerText = '您的密码必须含有“小写字母”、“大写字母”、“数字”、“特殊符号”中的任意三种';
                    return false;
                }*/
                var tR = {
                    l: Reg(str, rC.lW)
                };
                if (tR.l) {
                    //document.getElementById('msg').innerText = 'success';
                    return true;
                } else {
                    document.getElementById('msg').innerText = '密码以6-10位字母或数字组成';
                    return false;
                }
            }
        }
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
                <td class="tablefield" style="width: 30%">原始密码:
                </td>
                <td>
                    <input type="password" id="txtOldPwd" runat="server" maxlength="20" style="width: 130px" />
                    <%--<asp:TextBox runat="server" ID="txtOldPwd" MaxLength="20"></asp:TextBox>--%>
                    <font color="red" size="2"><b>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="密码不能为空"
                        ControlToValidate="txtOldPwd"></asp:RequiredFieldValidator>

                          <asp:RegularExpressionValidator ID="REVPro" runat="server" ControlToValidate="txtOldPwd"
                            ErrorMessage="密码为6-10位数字字母组合" ValidationExpression="^(\s|\S){0,250}$"></asp:RegularExpressionValidator>
                                           </b>
                        </font>
                </td>
            </tr>
            <tr>
                <td class="tablefield" style="width: 30%">新密码:
                </td>
                <td>
                    <input type="password" id="txtNewPwd" runat="server" maxlength="20" style="width: 130px" />
                    <%--<asp:TextBox runat="server" ID="txtNewPwd" MaxLength="20"></asp:TextBox>--%>
                    <font color="red" size="2"><b>
                            <span id="msg"></span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="新密码不能为空"
                        ControlToValidate="txtNewPwd"></asp:RequiredFieldValidator> 
                                               </b>
                        </font>
                </td>
            </tr>
            <tr>
                <td class="tablefield" style="width: 30%">确认密码:
                </td>
                <td>
                    <input type="password" id="txtConfirmPwd" runat="server" maxlength="20" style="width: 130px" />
                    <%--<asp:TextBox runat="server" ID="txtConfirmPwd" MaxLength="20"></asp:TextBox>--%>
                    <font color="red" size="2"><b>
                    <asp:CompareValidator ID="CompareValidator1" 
                        ControlToCompare="txtNewPwd" ControlToValidate="txtConfirmPwd" 
                        runat="server" ErrorMessage="确认密码有误"></asp:CompareValidator>
                        </b>
                        </font>

                </td>
            </tr>
            <tr>
                <td class="tablefield" style="width: 30%"></td>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" class="wsd_button2"
                        Text="修 改" OnClick="btnSubmit_Click" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
