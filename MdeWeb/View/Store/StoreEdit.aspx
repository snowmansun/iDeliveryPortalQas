<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StoreEdit.aspx.cs" Inherits="DatapoolWeb.View.Store.StoreEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>门店信息</title>
    <link href="../../Resources/styles/style.css" rel="stylesheet" />
    <script src="../JavaScript/common.js" type="text/javascript"></script>

    <script src="../JavaScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>

    <script src="../Resources/scripts/jquery-1.4.2.min.js" type="text/javascript"></script>
     <script language="javascript" type="text/javascript">

         function submit() {
             $('#form').form('submit', {
                 onSubmit: function () {
                     return $(this).form('validate');
                 },
                 success: function (msg) {
                     $.messager.alert('提示', "保存成功", 'info', function () {
                         window.location.href = "/Article/Admin/%>/";
                    });
                }
            });
        }

    </script>


    <style type="text/css">
        .style1
        {
            height: 23px;
        }
    </style>
</head>
<body class="wsd_option_area">
    <form id="form1" runat="server" >
        <div>
            <table id="wsd_title">
                <tr>
                    <td class="wsd_titlefont">
                        您当前的位置
                    </td>
                    <td class="wsd_2rightarrow">
                        &nbsp;
                    </td>
                    <td class="wsd_titlefont2">
                        <asp:Literal ID="Literal8" runat="server" Text="门店管理"></asp:Literal>
                    </td>
                    <td class="wsd_1rightarrow">
                        &nbsp;
                    </td>
                    <td class="wsd_titlefont1">
                        <asp:Literal ID="Literal2" runat="server" Text="门店数据维护"></asp:Literal>
                    </td>
                </tr>
            </table>
        <br />
        <table id="wsd_inputtable">
            <tr>
                <td colspan="6" class="tabletitle">
                    <asp:Literal ID="Literal5" runat="server" Text="输入表单"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <img src="../../Resources/Images/prompt/notice.jpg" />
                    <font color="red" size="2"><b>
                        <asp:Literal ID="Literal4" runat="server" Text="提示：*为必填项！"></asp:Literal>
                    </b></font>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                </td>
            </tr>
            <tr>
               <%-- <td class="tablefield">
                    <asp:Literal ID="Literal3" runat="server" Text="门店编码"></asp:Literal>
                </td>
                <td>
                    <asp:TextBox ID="txtStoreCode" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtStoreCode" Text="*" ErrorMessage="请输入门店编码！"
                        ID="RequiredFieldValidator1"></asp:RequiredFieldValidator>
                </td>--%>
                <td class="tablefield">
                     <span class="keyword">*</span>
                    <asp:Literal ID="Literal12" runat="server" Text="门店名称"></asp:Literal>
                </td>
                <td nowrap="nowrap">
                    <asp:TextBox runat="server" ID="txtStoreNm"  MaxLength="50"></asp:TextBox>
                     <asp:RequiredFieldValidator runat="server" ControlToValidate="txtStoreNm" Text="*" ErrorMessage="请输入门店名称！"
                        ID="RequiredFieldValidator5"></asp:RequiredFieldValidator>
                </td>
                 <td class="tablefield">
                    <span class="keyword">*</span>
                    <asp:Literal ID="Literal21" runat="server" Text="城市"></asp:Literal>
                </td>
                <td nowrap="nowrap">
                    <asp:DropDownList ID="ddlCity" runat="server" Width="130">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlCity" Text="*" ErrorMessage="请选择城市！"
                        ID="RequiredFieldValidator12"></asp:RequiredFieldValidator>
                </td>
                <td class="tablefield">
                   
                </td>
                <td nowrap="nowrap">
                   
                </td>
               <%-- <td class="tablefield">
                    <asp:Literal ID="Literal6" runat="server" Text="门店编码组合"></asp:Literal>
                </td>
                <td nowrap="nowrap">
                    <asp:TextBox runat="server" ID="txtName" MaxLength="20"></asp:TextBox>
                </td>
                <td class="tablefield">
                     <asp:Literal ID="Literal7" runat="server" Text="辅助编码"></asp:Literal>
                </td>
                <td nowrap="nowrap">
                    <asp:TextBox ID="txtCusAbbreviation" runat="server"></asp:TextBox>
                </td>--%>
            </tr>
            <tr>
              <%--  <td class="tablefield">
                    <span class="keyword">*</span>
                    <asp:Literal ID="Literal9" runat="server" Text="区域"></asp:Literal>
                </td>
                <td>
                    <asp:DropDownList ID="ddlRegion" runat="server" Width="130px">
                    </asp:DropDownList>
                     <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlRegion" Text="*" ErrorMessage="请选择区域！"
                        ID="RequiredFieldValidator2"></asp:RequiredFieldValidator>
                </td>--%>
                
               <%-- <td class="tablefield">
                    <span class="keyword">*</span>
                    <asp:Literal ID="Literal10" runat="server" Text="零售商集团"></asp:Literal>
                </td>
                <td nowrap="nowrap">
                    <asp:DropDownList ID="ddlBannerGroup" runat="server" Width="130px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlBannerGroup" Text="*" ErrorMessage="请选择客户！"
                        ID="RequiredFieldValidator3"></asp:RequiredFieldValidator>
                </td>--%>
                 <td class="tablefield">
                     <span class="keyword">*</span>
                     <asp:Literal ID="Literal11" runat="server" Text="零售商"></asp:Literal>
                 </td>
                <td nowrap="nowrap">
                    <asp:DropDownList ID="ddlBanner" runat="server" Width="130px">
                    </asp:DropDownList>
                     <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlBanner" Text="*" ErrorMessage="请选择连锁客户！"
                        ID="RequiredFieldValidator4"></asp:RequiredFieldValidator>
                </td>
                <td class="tablefield">
                     <span class="keyword">*</span>
                    <asp:Literal ID="Literal13" runat="server" Text="片区经理"></asp:Literal>
                 </td>
                <td nowrap="nowrap">
                     <asp:DropDownList ID="ddlDSM" runat="server" Width="130px">
                    </asp:DropDownList>
                    <%--<asp:TextBox runat="server" ID="txtDSM" MaxLength="50"></asp:TextBox>--%>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlDSM" Text="*" ErrorMessage="请选择片区经理！"
                        ID="RequiredFieldValidator6"></asp:RequiredFieldValidator>
                </td>
               <td class="tablefield">
                     <span class="keyword">*</span>
                    <asp:Literal ID="Literal14" runat="server" Text="销售"></asp:Literal>
                </td>
                <td nowrap="nowrap">
                   <%-- <asp:TextBox runat="server" ID="txtSR" MaxLength="15"></asp:TextBox>--%>
                     <asp:DropDownList ID="ddlSR" runat="server" Width="130px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlSR" Text="*" ErrorMessage="请选择销售！"
                        ID="RequiredFieldValidator7"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <%-- <td class="tablefield">
                     <span class="keyword">*</span>
                    <asp:Literal ID="Literal15" runat="server" Text="销售编码"></asp:Literal>
                </td>
                <td nowrap="nowrap">
                    <asp:TextBox runat="server" ID="txtSRCode" MaxLength="50"></asp:TextBox>
                     <asp:RequiredFieldValidator runat="server" ControlToValidate="txtSRCode" Text="*" ErrorMessage="请输入销售编码！"
                        ID="RequiredFieldValidator8"></asp:RequiredFieldValidator>
                </td>--%>
                <td class="tablefield">
                     <span class="keyword">*</span>
                     <asp:Literal ID="Literal16" runat="server" Text="销售类型"></asp:Literal>
                </td>
                 <td nowrap="nowrap">
                     <asp:DropDownList ID="ddlSRType" runat="server" Width="130">
                    </asp:DropDownList>
                   <%-- <asp:TextBox ID="txtSRType" runat="server"></asp:TextBox>--%>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlSRType" Text="*" ErrorMessage="请选择销售职称！"
                        ID="RequiredFieldValidator9"></asp:RequiredFieldValidator>
                </td>
                 <td class="tablefield" style="height: 23px">
                     <span class="keyword">*</span>
                     <asp:Literal ID="Literal17" runat="server" Text="经销商"></asp:Literal>
                </td>
                <td nowrap="nowrap" class="style1">
                     <asp:DropDownList ID="ddlDistributor" runat="server" Width="130">
                    </asp:DropDownList>
                     <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlDistributor" Text="*" ErrorMessage="请选择经销商！"
                        ID="RequiredFieldValidator10"></asp:RequiredFieldValidator>
                  <%--  <asp:TextBox runat="server" ID="txtDistributor" MaxLength="15"
                        ReadOnly="true"></asp:TextBox>--%>
                </td>
                 <td class="tablefield" style="height: 23px">
                     <asp:Literal ID="Literal18" runat="server" Text="二批经销商"></asp:Literal>
                </td>
                <td nowrap="nowrap" class="style1">
                     <asp:CheckBox runat="server" ID="chkDistributor2" />
                </td>
            </tr>
            <tr>
                <td class="tablefield" style="height: 23px">
                     <span class="keyword">*</span>
                    <asp:Literal ID="Literal19" runat="server" Text="渠道"></asp:Literal>
                </td>
                <td nowrap="nowrap" class="style1">
                     <asp:DropDownList ID="ddlChannel" runat="server" 
                        Width="130px">
                    </asp:DropDownList>
                     <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlChannel" Text="*" ErrorMessage="请选择渠道！"
                        ID="RequiredFieldValidator13"></asp:RequiredFieldValidator>
                </td>
                 <td class="tablefield">
                     <span class="keyword">*</span>
                     <asp:Literal ID="Literal20" runat="server" Text="类型"></asp:Literal>
                </td>
                <td nowrap="nowrap">
                    <asp:DropDownList ID="ddlType" runat="server" Width="130px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlType" Text="*" ErrorMessage="请选择类型！"
                        ID="RequiredFieldValidator14"></asp:RequiredFieldValidator>
                </td>
                  <td class="tablefield">
                     <span class="keyword">*</span>
                    <asp:Literal ID="Literal22" runat="server" Text="客户类型"></asp:Literal>
                </td>
                <td nowrap="nowrap">
                    <asp:DropDownList ID="ddlNonKA" runat="server" Width="130px">
                    </asp:DropDownList>
                     <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlNonKA" Text="*" ErrorMessage="请选择客户类型！"
                        ID="RequiredFieldValidator1"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tablefield">
                     <span class="keyword">*</span>
                   <asp:Literal ID="Literal23" runat="server" Text="状态"></asp:Literal>
                </td>
                <td nowrap="nowrap">
                    <asp:DropDownList ID="ddlStatus" runat="server" Width="130px">
                   <%--     <asp:ListItem Value=""></asp:ListItem>
                        <asp:ListItem Value="1">Open</asp:ListItem>
                        <asp:ListItem Value="0">Close</asp:ListItem>--%>
                    </asp:DropDownList>
                     <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlStatus" Text="*" ErrorMessage="请选择状态！"
                        ID="RequiredFieldValidator2"></asp:RequiredFieldValidator>
                </td>
                 <td class="tablefield">
                     <span class="keyword">*</span>
                     <asp:Literal ID="Literal24" runat="server" Text="配送"></asp:Literal>
                </td>
                <td nowrap="nowrap">
                    <asp:DropDownList ID="ddlDelivery" runat="server" Width="130px">
                    </asp:DropDownList>
                     <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlDelivery" Text="*" ErrorMessage="请选择配送！"
                        ID="RequiredFieldValidator15"></asp:RequiredFieldValidator>
                </td>
                 <td class="tablefield">
                     <span class="keyword">*</span>
                     <asp:Literal ID="Literal25" runat="server" Text="区"></asp:Literal>
                </td>
                <td nowrap="nowrap">
                    <asp:TextBox runat="server" ID="txtDistrict" MaxLength="50" Width="182px"></asp:TextBox>
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDistrict" Text="*" ErrorMessage="请输入区！"
                        ID="RequiredFieldValidator16"></asp:RequiredFieldValidator>
                 </td>
            </tr>
            <tr>
                <td class="tablefield">

                     <asp:Literal ID="Literal26" runat="server" Text="门店地址"></asp:Literal>
                </td>
                <td nowrap="nowrap">
                    <asp:TextBox runat="server" ID="txtAddress" MaxLength="500" Width="200px"></asp:TextBox>
                  <%--  <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAddress" Text="*" ErrorMessage="请输入门店地址！"
                        ID="RequiredFieldValidator17"></asp:RequiredFieldValidator>--%>
                 </td>
                  <td class="tablefield">
                      <asp:Literal ID="Literal27" runat="server" Text="联系人员"></asp:Literal>
                 </td>
                <td nowrap="nowrap">
                    <asp:TextBox runat="server" ID="txtContactPerson" MaxLength="50"></asp:TextBox>
                </td>
                 <td class="tablefield">
                    <asp:Literal ID="Literal28" runat="server" Text="电话"></asp:Literal>
                </td>
                <td nowrap="nowrap">
                    <asp:TextBox runat="server" ID="txtTelNo" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tablefield">
                    <asp:Literal ID="Literal29" runat="server" Text="面积"></asp:Literal>
                </td>
                <td nowrap="nowrap">
                    <asp:TextBox runat="server" ID="txtArea" MaxLength="50" onkeyup="this.value=this.value.replace(/\D/g,'')"></asp:TextBox>
                <%--    <asp:DropDownList ID="ddlArea" runat="server" Width="130px">
                     </asp:DropDownList>--%>
                </td>
                 <td class="tablefield">
                     <asp:Literal ID="Literal31" runat="server" Text="冰箱"></asp:Literal>
                </td>
                <td nowrap="nowrap">
                     <asp:CheckBox runat="server" ID="chkRef" />
                </td>
                 <td class="tablefield">
                     <asp:Literal ID="Literal30" runat="server" Text="是否加盟店"></asp:Literal>
                </td>
                <td nowrap="nowrap">
                    <asp:CheckBox runat="server" ID="chkAff" />
                 </td>
            </tr>
          <%--  <tr>
                
                <td class="tablefield">
                   </td>
                <td nowrap="nowrap">
                     
                </td>
                <td class="tablefield">
                   </td>
                <td nowrap="nowrap">
                     
                </td>
            </tr>--%>
            </table>
        <br />
        <table id="wsd_inputtable">
            <tr>
                <td width="100%" class="tabletitle">
                    <asp:Literal ID="Literal40" runat="server" Text="操作选项"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="buttonarea">
                    <asp:Button ID="btnSave" runat="server" class="wsd_button2" Text="提  交"
                        OnClick="btnSave_Click" />&nbsp;
                        <%--<input id="Button1" type="button" value="提  交" onclick="submit();" class="wsd_button2"/>--%>
                        <input id="Button1" type="button" onclick="javascript: location.href = 'StoreList1.aspx?status=u'" value="返  回" class="wsd_button2"/>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
