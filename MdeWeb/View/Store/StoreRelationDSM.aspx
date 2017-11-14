<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StoreRelationDSM.aspx.cs" Inherits="DatapoolWeb.View.Store.StoreRelationDSM" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
   <title>DSM门店关系</title>
    <link href="../../Resources/styles/style.css" rel="stylesheet" />
    <link href="../../Styles/ShowWindown.css" rel="stylesheet" />

    <script src="../JavaScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Resources/scripts/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="../../JavaScript/jquery.js" type="text/javascript"></script>
    <script src="../../JavaScript/tipswindown.js" type="text/javascript"></script>

    <script src="../JavaScript/common.js" type="text/javascript"></script>

    <script language="JavaScript" type="text/javascript">


        function CheckAll(gridName, parentChk, ChildId) {
            var oElements = document.getElementsByTagName("INPUT");
            var bIsChecked = parentChk.checked;

            for (i = 0; i < oElements.length; i++) {
                if (IsCheckBox(oElements[i]) &&
                    IsMatchPattern(gridName, oElements[i].id, ChildId)) {
                    oElements[i].checked = bIsChecked;
                }
            }
        }

        function IsMatchPattern(gridName, id, ChildId) {
            var sPattern = gridName;
            var oRegExp = new RegExp(sPattern);
            if (oRegExp.exec(id))
                return true;
            else
                return false;
        }

        function IsCheckBox(chk) {
            if (chk.type == 'checkbox') return true;
            else return false;
        }



        function showTipsWindown(title, id, width, height) {
            tipsWindown(title, "id:" + id, width, height, "true", "", "true", id);
        }

        function confirmTerm(s) {
            //parent.closeWindown();
            showselect('t123_');
            $("#windownbg").remove();
            $("#windown-box").fadeOut("slow", function () { $(this).remove(); });
        }

        //弹出层调用
        function popTips() {
            showTipsWindown("转移", 'simTestContent', 280, 100);
        }

        $(document).ready(function () {
            $("#btnTransfer").click(popTips);
            //$('#simTestContent').appendTo($("form:form1"))


        });

        function comfrimData() {
            var _value = $('#windown-box').find('select[id="ddlTrunsfelSr"]').val();
            if (_value == null || _value == "") {
                alert("请选择将转移到的DSM！");
                return;
            }
            $("#hidSr").val(_value);
            //alert($("#hidSr").val());
            $("#btnComfrim").click();


        }

    </script>

    <style type="text/css">
        td
        {
            height: 23px;
        }
    </style>


</head>
<body class="wsd_option_area">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <asp:UpdatePanel ID="updatePanel" runat="server">
            <ContentTemplate>
                <div>
                    <table id="wsd_title">
                        <tr>
                            <td class="wsd_titlefont">您当前的位置
                            </td>
                            <td class="wsd_2rightarrow">&nbsp;
                            </td>
                            <td class="wsd_titlefont2">
                                <asp:Literal ID="Literal8" runat="server" Text="门店管理"></asp:Literal>
                            </td>
                            <td class="wsd_1rightarrow">&nbsp;
                            </td>
                            <td class="wsd_titlefont1">
                                <asp:Literal ID="Literal2" runat="server" Text="人点关系维护"></asp:Literal>
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
                            <td class="tablefield">DSM</td>
                            <td nowrap="nowrap">
                                <asp:DropDownList ID="ddlSR" runat="server" AutoPostBack="True"
                                    Width="130px" OnSelectedIndexChanged="ddlSR_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnQuery" runat="server" class="wsd_button2" Text="查询门店"
                                    OnClick="btnQuery_Click" />
                            </td>
                            <td></td>
                        </tr>
                    </table>

                    <br />
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="100">
                        <ProgressTemplate>
                            <img src="../../Resources/Images/loading.gif" alt=""><span style="color: blue">正在查询数据，请您稍等！ </span>
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
                                        PageSize="30" DataKeyNames="STORE_CODE" OnRowCommand="gvStore_RowCommand">
                                        <HeaderStyle CssClass="titlist" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" Font-Bold="True"></EmptyDataRowStyle>
                                        <Columns>
                                            <asp:TemplateField HeaderText="全部选择">
                                                <HeaderTemplate>
                                                    <input type="checkbox" id="chkAll" name="chkAll" onclick="CheckAll('gvStore', this, 'cbxIsSelect')" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox runat="server" ID="cbxIsSelect" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="门店编码">
                                                <ItemTemplate>
                                                    <a href="StoreEdit.aspx?id=<%#Eval("STORE_CODE") %>">
                                                        <%#Eval("STORE_CODE")%></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="门店名称" DataField="STORE_NAME" />
                                            <%--<asp:BoundField HeaderText="门店编码" DataField="StoreCode" />--%>
                                            <asp:BoundField HeaderText="区域" DataField="REGION_NM" />
                                            <asp:BoundField HeaderText="城市" DataField="CITY_NM" />
                                            <%--<asp:BoundField HeaderText="客户" DataField="Customer" />--%>
                                            <asp:BoundField HeaderText="零售商集团" DataField="BANNER_GROUP_NM" />
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
                                            <%--<asp:TemplateField HeaderText="操作">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnTransfer" Text="转移>"
                                                        runat="server" CssClass="btn3_mouseover" CommandName="TransferCustomer" CommandArgument='<%# Eval("STORE_CODE")%>' />
                                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnDel" Text="删除"
                                        runat="server" CssClass="btn3_mouseover" CommandName="DeleteCustomer" CommandArgument='<%# Eval("STORE_CODE")%>' />

                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                        <RowStyle CssClass="evenline" />
                                        <AlternatingRowStyle CssClass="oddline" />
                                        <PagerTemplate>
                                            <table style="font-size: 12px;" align="left" class="pagera">
                                                <tr>
                                                    <td>总共<asp:Label ID="Label1" runat="server" Text="<%#((GridView)Container.NamingContainer).PageCount %>"></asp:Label>页
                                            &nbsp;
                                                    </td>
                                                    <td>第<asp:Label ID="Label2" runat="server" Text="<%#((GridView)Container.NamingContainer).PageIndex+1 %>"></asp:Label>页
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
                </div>

                <input id="hidSr" type="hidden" runat="server" value="" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="display: none;">

            <div id="simTestContent" class="simScrollCont">

                <div class="mainlist" style="text-align: center;">
                    将转移到:<asp:DropDownList ID="ddlTrunsfelSr" runat="server" Width="130px">
                    </asp:DropDownList>
                </div>
                <div class="btnbox">
                    <asp:Button ID="btnTrun" runat="server" Text=" 转移 " OnClientClick="comfrimData();return false;" CausesValidation="false" />


                    &nbsp;&nbsp;
                            <input type="button" id="confirmTerm" value=" 取消 " onclick="confirmTerm(1);" class="confirmBtn" />
                </div>
            </div>
        </div>
        <br />
        <table id="wsd_inputtable">
            <tr>
                <td width="100%" class="tabletitle">
                    <asp:Literal ID="Literal7" runat="server" Text="操作"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="buttonarea">
                    <asp:Button ID="BtnAdd" runat="server" class="wsd_button2" Text=" 新增 " OnClick="BtnAdd_Click" />
                    &nbsp;
                                <asp:Button ID="btnTransfer" runat="server" class="wsd_button2" Text=" 转移 " OnClientClick=" return false;" Height="23" />
                    &nbsp;
                                <asp:Button ID="btnDel" runat="server" class="wsd_button2" Text=" 删除 " Height="23" OnClick="btnDel_Click" />
                    <asp:Button ID="btnComfrim" runat="server" Text=" 转移 " CausesValidation="false" OnClick="btnComfrim_Click" Style="display: none;" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

