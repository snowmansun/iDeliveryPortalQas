<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Head.aspx.cs" Inherits="DatapoolWeb.Head" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <title>上海意贝斯特信息技术有限公司</title>
    <link href="resources/styles/style.css" type="text/css" rel="stylesheet" />

    <script src="JavaScript/common.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function GetDate() {
            var date = new Date();
            var str;
            var day = new Array("日", "一", "二", "三", "四", "五", "六");
            str = date.getFullYear() + "年" + (date.getMonth() + 1) + "月" + date.getDate() + "日  ";
            str = str + "星期" + day[date.getDay()] + "  ";
            if (date.getHours() <= 12)
                str = str + "上午" + date.getHours() + ":";
            else
                str = str + "下午" + (date.getHours() - 12) + ":";
            if (date.getMinutes() < 10)
                str = str + "0" + date.getMinutes() + ":";
            else
                str = str + date.getMinutes() + ":";
            if (date.getSeconds() < 10)
                str = str + "0" + date.getSeconds();
            else
                str = str + date.getSeconds();
            document.getElementById("clock").innerText = str;
            setTimeout("GetDate()", 1000);
        }
    </script>

</head>
<body class="wsd_head_body">
    <form id="head" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr align="center">
                <td background="resources/images/head/header.png" width="100%" height="59" align="center">&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3" height="23" width="100%" background="Resources/images/head/headbg.gif">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td width="228">&nbsp;
                            </td>
                            <td class="wsd_head_fontsz">
                                <asp:Literal ID="Literal7" runat="server" Text="欢迎您登录"></asp:Literal>,<asp:Label 
ID="lblName" runat="server" Text="Label"></asp:Label>!&nbsp;&nbsp;&nbsp;
                <%--            <asp:Literal ID="Literal1" runat="server" Text="您是"></asp:Literal>:
                            <asp:Literal ID="lblDomain" runat="server"></asp:Literal>--%>
                            </td>
                                <%--<a href="" runat="server" id="HomePage" target="mainFrame"></a>--%>
                            <td width="40" style="height: 24px;font-size:12px;">
                                <a href="#" runat="server" id="LoginOut">注销
                                </a>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
