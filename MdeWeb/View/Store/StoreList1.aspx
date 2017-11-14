<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StoreList1.aspx.cs" Inherits="DatapoolWeb.View.Store.StoreList1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>门店维护</title>
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
        <asp:ScriptManager ID="ScriptManager" runat="server"/>
        <asp:UpdatePanel ID="updatePanel" runat="server">
        <Triggers>
                <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
        <ContentTemplate>
        <div>
            <table id="wsd_title">
                <tr>
                    <td class="wsd_titlefont">
                        您当前的位置
                    </td>
                    <td class="wsd_2rightarrow">&nbsp;
                    </td>
                    <td class="wsd_titlefont2">
                        <asp:Literal ID="Literal8" runat="server" Text="门店管理"></asp:Literal>
                    </td>
                    <td class="wsd_1rightarrow">&nbsp;
                    </td>
                    <td class="wsd_titlefont1">
                        <asp:Literal ID="Literal2" runat="server" Text="门店维护"></asp:Literal>
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
                    <td class="tablefield">门店编码</td>
                    <td nowrap="nowrap">
                        <asp:TextBox ID="txtStoreCode" runat="server" Width="124px"></asp:TextBox>
                    </td>
                    <td class="tablefield">门店名称</td>
                    <td nowrap="nowrap">
                        <asp:TextBox ID="txtStoreName" runat="server" Width="124px"></asp:TextBox>
                    </td>
                    <td class="tablefield">渠道</td>
                    <td>
                        <asp:DropDownList ID="ddlChannal" runat="server" AutoPostBack="True"
                            Width="130px">
                        </asp:DropDownList>
                    </td>

                </tr>
                <tr>
                    <td class="tablefield">
                        <asp:Literal ID="Literal3" runat="server" Text="区域"></asp:Literal>
                    </td>
                    <td nowrap="nowrap">
                        <asp:DropDownList ID="ddlRegion" runat="server" Width="130px" AutoPostBack="True" OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td class="tablefield">
                        <asp:Literal ID="Literal1" runat="server" Text="CLUSTER"></asp:Literal>
                    </td>
                    <td nowrap="nowrap">
                        <asp:DropDownList ID="ddlCluster" runat="server" Width="130px" AutoPostBack="True" OnSelectedIndexChanged="ddlCluster_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td class="tablefield">
                        <asp:Literal ID="Literal15" runat="server" Text="城市"></asp:Literal>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCity" runat="server"  Width="130px" AutoPostBack="True" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tablefield">销售
                    </td>
                    <td nowrap="nowrap">
                        <asp:TextBox ID="txtSR" runat="server" Width="124px"></asp:TextBox>
                    </td>
                  <%--  <td class="tablefield">零售商集团
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBannerGroupCode" runat="server" AutoPostBack="True" Width="130px">
                        </asp:DropDownList>
                    </td>--%>
                    <td class="tablefield">零售商
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBanner" Width="130" runat="server">
                        </asp:DropDownList>
                    </td>
                      <td class="tablefield">片区经理
                    </td>
                    <td nowrap="nowrap">
                        <asp:TextBox ID="txtDSM" runat="server" Width="124px"></asp:TextBox>
                    </td>
                  
                </tr>
                <tr>
                  
                    <td class="tablefield">经销商
                    </td>
                    <td>
                       <asp:DropDownList ID="ddlDistributor" runat="server" Width="160">
                        </asp:DropDownList>
                     </td>
                   <%-- <td nowrap="nowrap">
                        <asp:TextBox ID="txtDistributor" runat="server" Width="124px"></asp:TextBox>
                    </td>--%>
                     <td class="tablefield">配送
                    </td>
                    <td nowrap="nowrap">
                        <asp:DropDownList ID="ddlDelivery" runat="server" Width="130px">
                        </asp:DropDownList>
                    </td>
                     <td class="tablefield">面积
                    </td>
                    <td nowrap="nowrap">
                        <asp:DropDownList ID="ddlArea" runat="server" Width="130px">
                        </asp:DropDownList>
                    </td>
                   <%-- <td class="tablefield">二批经销商
                    </td>
                    <td nowrap="nowrap">
                        <asp:TextBox ID="txtDistributor2" runat="server" Width="124px"></asp:TextBox>
                    </td>--%>
                 <%--   <td class="tablefield">类型
                    </td>
                    <td nowrap="nowrap">
                        <asp:DropDownList ID="ddlType" runat="server" Width="130px">
                        </asp:DropDownList>
                    </td>--%>
                </tr>
                <tr>
                   
                    <td class="tablefield">客户类型
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlNonKA" runat="server" Width="130px">
                            <%--<asp:ListItem Value="-1">--全部--</asp:ListItem>
                            <asp:ListItem Value="1">KA</asp:ListItem>
                            <asp:ListItem Value="0">Non Ka</asp:ListItem>--%>
                        </asp:DropDownList>
                    </td>
                    <td class="tablefield">状态
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlStatus" runat="server" Width="130px">
   <%--                         <asp:ListItem Value="-1">--全部--</asp:ListItem>
                            <asp:ListItem Value="1">Open</asp:ListItem>
                            <asp:ListItem Value="0">Close</asp:ListItem>--%>
                        </asp:DropDownList>
                    </td>
                     <td class="tablefield">
                    </td>
                    <td>
                         </td>
                </tr>
              
                <tr>
                    <td class="buttonarea" colspan="6">
                        <asp:Button ID="btnQuery" runat="server" class="wsd_button2" Text="查询"
                            OnClick="btnQuery_Click" />
                        &nbsp;
                        <asp:Button ID="btnExport" runat="server" class="wsd_button2" Text="导出"
                            OnClick="btnExport_Click" />
                            &nbsp;
                     <%--   <input id="Button1" type="button" class="wsd_button2" runat="server" size="47" value="重置"
                            name="reset" onclick="clearForm('form1')" />--%>
<%--                        <input type="button" id="btnNew" value="新  增" class="wsd_button2" onclick="location.href = 'StoreEdit.aspx'" />--%>
                         <asp:Button ID="btnNew" runat="server" class="wsd_button2" Text="新  增"  OnClientClick="location.href = 'StoreEdit.aspx'" />
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
                    <td class="tabletitle" style="height: 21px" width="100%">门店列表
                    </td>
                </tr>
            </table>
            <div style="overflow: scroll; width: 98%; overflow-y: hidden;">
                <table id="wsd_listtable" width="100%">
                    <tr>
                        <td style="width: 100%">
                            <asp:GridView Width="1800px" ID="gvStore" runat="server" AutoGenerateColumns="False"
                                EmptyDataText="没有找到数据" EmptyDataRowStyle-Font-Bold="true" AllowSorting="True"
                                EmptyDataRowStyle-HorizontalAlign="Center" OnRowDataBound="Grid_RowDataBound"
                                EnableEmptyContentRender="true" AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging"
                                PageSize="30">
                                <HeaderStyle CssClass="titlist" />
                                <EmptyDataRowStyle HorizontalAlign="Center" Font-Bold="True"></EmptyDataRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="门店编码">
                                        <ItemTemplate>
                                            <%--<a href="StoreEdit.aspx?id=<%#Eval("STORE_CODE") %>">
                                                <%#Eval("STORE_CODE")%></a>--%>
                                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# "StoreEdit.aspx?id=" +  DataBinder.Eval(Container.DataItem,"STORE_CODE")%>'  
                                                Text ='<%#Eval("STORE_CODE")%>' />
                                            <%--<asp:Literal ID="Literal15" runat="server" Text='<%#Eval("STORE_CODE")%>' Visible=' <%# !Convert.ToBoolean(Eval("IS_VISIBLE")) %> '></asp:Literal>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="门店名称" DataField="STORE_NAME" />
                                    <%--<asp:BoundField HeaderText="门店编码" DataField="StoreCode" />--%>
                                    <asp:BoundField HeaderText="区域" DataField="REGION_NM" />
                                    <asp:BoundField HeaderText="城市" DataField="CITY_NM" />
                                    <%--<asp:BoundField HeaderText="客户" DataField="Customer" />--%>
                                    <%--<asp:BoundField HeaderText="零售商集团" DataField="BANNER_GROUP_NM" />--%>
                                    <asp:BoundField HeaderText="零售商" DataField="BANNER_NM" />
                                    <asp:BoundField HeaderText="片区经理" DataField="DSM" />
                                    <asp:BoundField HeaderText="销售" DataField="SR" />
                                    <%--<asp:BoundField HeaderText="销售编码" DataField="SR" />
                                    <asp:BoundField HeaderText="销售职称" DataField="SRType" />--%>
                                    <asp:BoundField HeaderText="经销商" DataField="DISTRIBUTOR_NM" />
                                    <%--<asp:BoundField HeaderText="二批经销商" DataField="DISTRIBUTOR_NM2" />--%>
                                    <asp:BoundField HeaderText="渠道" DataField="CHANNEL" />
                                    <asp:BoundField HeaderText="类型" DataField="TYPE" />
                                    <asp:BoundField HeaderText="客户类型" DataField="KA" />
                                    <asp:BoundField HeaderText="状态" DataField="STATUS" />
                                    <%--<asp:BoundField HeaderText="配送" DataField="DELIVERY" />--%>
                                    <asp:BoundField HeaderText="区" DataField="DISTRICT" />
                                    <asp:BoundField HeaderText="门店地址" DataField="ADDRESS" />
                                    <%--<asp:BoundField HeaderText="联系人员" DataField="CONTACT_PERSON" />
                                    <asp:BoundField HeaderText="电话" DataField="TEL_NO" />
                                    <asp:BoundField HeaderText="面积" DataField="AREA" />
                                    <asp:BoundField HeaderText="是否加盟店" DataField="AFFILIATE" />--%>
                                    <asp:BoundField HeaderText="冰箱" DataField="REFRIGERATOR" />
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
            <br />
            <table id="Table2">
                <tr>
                    <td width="100%" class="tabletitle">
                        <asp:Literal ID="Literal12" runat="server" Text=""></asp:Literal>
                    </td>
                </tr>
               
            </table>
        </div>

    </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
