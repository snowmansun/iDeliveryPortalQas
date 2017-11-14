<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonView.aspx.cs" Inherits="DatapoolWeb.View.Person.PersonView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>人员查看</title>
    <link href="../../Resources/styles/style.css" rel="stylesheet" />
    <link href="../../JavaScript/jquery-ui-1.10.3.custom/css/ui-lightness/jquery-ui-1.10.3.custom.css" rel="stylesheet" />
    <script src="../../JavaScript/jquery-ui-1.10.3.custom/js/jquery-1.9.1.js"></script>
    <script src="../../JavaScript/jquery-ui-1.10.3.custom/js/jquery-ui-1.10.3.custom.js"></script>

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
        <asp:ScriptManager ID="ScriptManager" runat="server"/>
        <asp:UpdatePanel ID="updatePanel" runat="server">
        <ContentTemplate>
        <table id="wsd_title">
            <tr>
                <td class="wsd_titlefont">
                    <asp:Literal ID="Literal1" runat="server" Text="您当前的位置"></asp:Literal>
                </td>
                <td class="wsd_2rightarrow">&nbsp;
                </td>
                <td class="wsd_titlefont2">
                    <asp:Literal ID="Literal8" runat="server" Text="人员管理"></asp:Literal>
                </td>
                <td class="wsd_1rightarrow">&nbsp;
                </td>
                <td class="wsd_titlefont1">
                    <asp:Literal ID="Literal2" runat="server" Text="人员查看"></asp:Literal>
                </td>
            </tr>
        </table>
        <br />

        <table id="wsd_inputtable">
            <tr>
                <td class="tabletitle" colspan="6">
                    <asp:Literal ID="Literal4" runat="server" Text="查询条件"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="tablefield">人员编号</td>
                <td nowrap="nowrap">
                    <asp:TextBox ID="txtID" runat="server" Width="124px"></asp:TextBox>
                </td>
                <td class="tablefield">人员账号</td>
                <td nowrap="nowrap">
                    <asp:TextBox ID="txtAccountCode" runat="server" Width="124px"></asp:TextBox>
                </td>
                <td class="tablefield">英文名</td>
                <td nowrap="nowrap">
                    <asp:TextBox ID="txtNameEn" runat="server" Width="124px"></asp:TextBox>
                </td>
             </tr>
              <tr>
                <td class="tablefield">中文名</td>
                <td nowrap="nowrap">
                    <asp:TextBox ID="txtName" runat="server" Width="124px"></asp:TextBox>
                </td>
                <td class="tablefield">城市</td>
                <td nowrap="nowrap">
                    <asp:DropDownList ID="ddlCity" runat="server" Width="124px"></asp:DropDownList>
                </td>
                <td class="tablefield">职位</td>
                <td nowrap="nowrap">
                    <asp:TextBox ID="txtPostion" runat="server" Width="124px"></asp:TextBox>
                </td>
             </tr>
             <tr>
                <td class="tablefield">组织名称</td>
                <td nowrap="nowrap">
                    <asp:TextBox ID="txtOrgNm" runat="server" Width="124px"></asp:TextBox>
                </td>
                <td class="tablefield">所属上级名</td>
                <td nowrap="nowrap">
                    <asp:TextBox ID="txtPar" runat="server" Width="124px"></asp:TextBox>
                </td>
                <td class="tablefield"></td>
                <td>
                </td>
             </tr>
             <tr>
                <td class="buttonarea" colspan="6">
                    <asp:Button runat="server" ID="btnSearch" Text="查询" CssClass="wsd_button2" OnClick="btnSearch_Click" />
                    <asp:Button ID="btnNew" runat="server" class="wsd_button2" Text="新增" OnClick="btnNew_Click" />
                </td>
            </tr>
        </table>
        <br />

        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="100">
            <ProgressTemplate>
                <img src="../../Resources/Images/loading.gif"  alt=""><span style="color: blue">正在查询数据，请您稍等！ </span>
            </ProgressTemplate>
        </asp:UpdateProgress>


        <table id="wsd_inputtable">
            <tr>
                <td class="tabletitle" style="height: 21px" width="100%">人员列表
                </td>
            </tr>
        </table>
        <div style="overflow: scroll; width: 98%; overflow-y: hidden;">
        <table id="wsd_listtable" width="100%">
            <tr>
                <td>
                    <asp:GridView Width="100%" ID="gvPerson" runat="server" AutoGenerateColumns="False"
                        EmptyDataText="空" EmptyDataRowStyle-Font-Bold="true"
                        EmptyDataRowStyle-HorizontalAlign="Center" OnRowDataBound="Grid_RowDataBound"
                        EnableEmptyContentRender="true" AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging"
                        PageSize="15">
                        <HeaderStyle CssClass="titlist" />
                        <EmptyDataRowStyle HorizontalAlign="Center" Font-Bold="True"></EmptyDataRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="人员编号">
                                <ItemTemplate>
                                    <a href="PersonEdit.aspx?id=<%#Eval("ID") %>">
                                        <%#Eval("ID")%></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="人员账号" DataField="ACCOUNT_CODE" />
                            <asp:BoundField HeaderText="英文名" DataField="NAME_EN" />
                            <asp:BoundField HeaderText="中文名" DataField="NAME" />
                            <asp:BoundField HeaderText="城市" DataField="CITY_NM_CN" />
                            <asp:BoundField HeaderText="职位" DataField="POSITION" />
                            <asp:BoundField HeaderText="组织编号" DataField="ORG_NO" />
                            <asp:BoundField HeaderText="组织名称" DataField="ORG_NM" />
                            <asp:BoundField HeaderText="COST_CTR" DataField="COST_CTR" />
                            <asp:BoundField HeaderText="COST_CENTER" DataField="COST_CENTER" />
                            <asp:BoundField HeaderText="所属上级" DataField="PAR_NM" />
                            <asp:BoundField HeaderText="CID" DataField="CID" />
                            <asp:BoundField HeaderText="E-MAIL" DataField="LONG_ID" />

                        </Columns>
                        <RowStyle CssClass="evenline" />
                        <AlternatingRowStyle CssClass="oddline" />
                         <PagerTemplate>
                                <table style="font-size: 12px;"  align="left" class="pagera">
                                    <tr>
                                        <td>
                                            总共<asp:Label ID="Label1" runat="server" Text="<%#((GridView)Container.NamingContainer).PageCount %>"></asp:Label>页
                                            &nbsp;
                                        </td>
                                        <td>
                                            第<asp:Label ID="Label2" runat="server" Text="<%#((GridView)Container.NamingContainer).PageIndex+1 %>"></asp:Label>页
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument="1" CommandName="Page"
                                                Enabled="<%#((GridView)Container.NamingContainer).PageIndex!=0 %>">首页</asp:LinkButton>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument="Prev" CommandName="Page"
                                                Enabled="<%#((GridView)Container.NamingContainer).PageIndex!=0 %>">上一页</asp:LinkButton>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton3" runat="server" CommandArgument="Next" CommandName="Page"
                                                Enabled="<%#((GridView)Container.NamingContainer).PageIndex!=((GridView)Container.NamingContainer).PageCount-1 %>">下一页 </asp:LinkButton>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton4" runat="server" CommandArgument="Last" CommandName="Page"
                                                Enabled="<%#((GridView)Container.NamingContainer).PageIndex!=((GridView)Container.NamingContainer).PageCount-1 %>">尾页</asp:LinkButton>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton5" runat="server" CommandArgument="-1" CommandName="Page"
                                                ValidationGroup="1">GO</asp:LinkButton>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNum" runat="server" Width="30px" Text="<%#((GridView)Container.NamingContainer).PageIndex+1 %>"
                                                ValidationGroup="1"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage=""
                                                ValidationExpression="^/d+$" ControlToValidate="txtNum" ValidationGroup="1"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                </table>
                                </PagerTemplate>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
