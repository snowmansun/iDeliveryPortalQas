<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonEdit.aspx.cs" Inherits="DatapoolWeb.View.Person.PersonEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                    <asp:Literal ID="Literal2" runat="server" Text="人员管理"></asp:Literal>
                </td>
                <td class="wsd_1rightarrow">&nbsp;
                </td>
                <td class="wsd_titlefont1">
                    <asp:Literal ID="Literal3" runat="server" Text="人员编辑"></asp:Literal>
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
                    人员编码:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtID" MaxLength="20"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="人员编码不能为空"
                        ControlToValidate="txtID"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tablefield" style="width: 30%">
                    <span class="keyword">*</span>
                    人员账号:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtCode" MaxLength="20"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="人员账号不能为空"
                        ControlToValidate="txtCode"></asp:RequiredFieldValidator>
                </td>
            </tr>
             <tr>
                <td class="tablefield" style="width: 30%">
                    英文名:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtNameEn" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tablefield" style="width: 30%">
                    中文名:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtName" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td class="tablefield" style="width: 30%">
                    职位:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtPosition" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td class="tablefield" style="width: 30%">
                    组织编号:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtOrgNo" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td class="tablefield" style="width: 30%">
                    组织名称:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtOrgNm" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td class="tablefield" style="width: 30%">
                    COST_CTR:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtCostCtr" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td class="tablefield" style="width: 30%">
                    COST_CENTER:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtCostCenter" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tablefield" style="width: 30%">
                    <span class="keyword">*</span>
                    上级:
                </td>
                <td nowrap="nowrap">
                    <asp:DropDownList runat="server" ID="ddlPerPerson"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="tablefield" style="width: 30%">
                    <span class="keyword">*</span>
                    城市:
                </td>
                <td nowrap="nowrap">
                    <asp:DropDownList runat="server" ID="ddlCity"></asp:DropDownList>
                </td>
            </tr>
             <tr>
                <td class="tablefield" style="width: 30%">
                    CID:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtCid" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td class="tablefield" style="width: 30%">
                    E-MAIL:
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtEmail" MaxLength="20"></asp:TextBox>
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
