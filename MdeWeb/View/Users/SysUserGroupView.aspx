<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysUserGroupView.aspx.cs" Inherits="DatapoolWeb.View.Users.SysUserGroupView" %>

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
<body class="wsd_option_area">
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
                    <asp:Literal ID="Literal2" runat="server" Text="用户组查看"></asp:Literal>
                </td>
            </tr>
        </table>
        <br />
        <table id="wsd_inputtable">
            <tr>
                <td class="tabletitle" colspan="5">
                    <asp:Literal ID="Literal4" runat="server" Text="查询"></asp:Literal>
                </td>
            </tr>
            <tr height="40px">
                <td class="tablefield">用户组编码</td>
                <td nowrap="nowrap">
                    <asp:TextBox ID="txtCode" runat="server" Width="124px"></asp:TextBox>
                </td>
                <td width="200px" align="right">
                    <asp:Button runat="server" ID="btnSearch" Text="查询" CssClass="wsd_button2" OnClick="btnSearch_Click" />
                    <asp:Button ID="btnNew" runat="server" class="wsd_button2" Text="新增" OnClick="btnNew_Click" />
                </td>
            </tr>
        </table>
        <br />
        <table id="wsd_inputtable">
            <tr>
                <td class="tabletitle" style="height: 21px" width="100%">用户组列表
                </td>
            </tr>
        </table>
        <table id="wsd_listtable" width="100%">
            <tr>
                <td>
                    <asp:GridView Width="100%" ID="gvGroupList" runat="server" AutoGenerateColumns="False"
                        EmptyDataText="空" EmptyDataRowStyle-Font-Bold="true"
                        EmptyDataRowStyle-HorizontalAlign="Center" OnRowDataBound="Grid_RowDataBound"
                        EnableEmptyContentRender="true" AllowPaging="True" OnPageIndexChanging="gvChannelType_PageIndexChanging"
                        PageSize="20">
                        <HeaderStyle CssClass="titlist" />
                        <EmptyDataRowStyle HorizontalAlign="Center" Font-Bold="True"></EmptyDataRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="人员名称">
                                <ItemTemplate>
                                    <a href="sysusergroupedit.aspx?id=<%#Eval("ID") %>">
                                        <%#Eval("CODE")%></a>
                                </ItemTemplate>
                            </asp:TemplateField>                           
                              <asp:TemplateField HeaderText="有效性" ItemStyle-HorizontalAlign="center" HeaderStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    <b><%# (bool)Eval("IsActive") ? "<font size='3' color='green'>√</font>" : "<font size='3' color='red'>×</font>"%></b>
                                </ItemTemplate>
                                 <HeaderStyle HorizontalAlign="Center" />
                                 <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle CssClass="evenline" />
                        <AlternatingRowStyle CssClass="oddline" />
                        <PagerSettings PageButtonCount="30" FirstPageText="第一页"
                            LastPageText="最后一页" Mode="NumericFirstLast"
                            NextPageText="下一页" PreviousPageText="前一页" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

