<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="DatapoolWeb.Menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        A:link
        {
            color: #000000;
            font-size: 12px;
            text-decoration: none;
            height: 26px;
            width: 164px;
            white-space: nowrap;
            background-image: url(resources/images/menu/menubgone.gif);
            margin-right: 0px;
            padding-left: 5px;
            padding-top: 7px;
            margin-top: -1px;
            text-algin: right;
        }
        A:visited
        {
            color: #000000;
            font-size: 12px;
            text-decoration: none;
            white-space: nowrap;
            height: 26px;
            width: 164px;
            white-space: nowrap;
            background-image: url(resources/images/menu/menubgone.gif);
            margin-top: -1px;
            margin-right: 10px;
            padding-left: 5px;
            padding-top: 7px;
            text-algin: right;
        }
        A:hover
        {
            color: #FF0000;
            font-size: 12px;
            text-decoration: none;
            height: 26px;
            width: 164px;
            white-space: nowrap;
            background-image: url(resources/images/menu/menubgone.gif);
            padding-left: 5px;
            margin-top: -1px;
            padding-top: 7px;
        }
        .bg1
        {
            margin-top: 0px;
            margin-left: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
            font-size: 12px;
            background-image: url(resources/images/menu/menubg.gif);
            width: 26px;
            height: 100%;
            background-repeat: repeat-y;
        }
        TD
        {
            font-size: 12px;
            line-height: 150%;
        }
        .gdt
        {
            overflow-x: hidden;
            overflow-y: scroll;
        }
    </style>

      <script src="JavaScript/Menu.js" type="text/javascript"></script>

     <%= LoadMenuItem()%>

</head>
<body class="bg1" ondragstart="return false;" oncontextmenu="return false;" onselectstart="return false">
    <table id="mnuList" style="width: 164px; height: 100%" cellspacing="0" cellpadding="0"
        align="left" border="0">
        <tr>
            <td class="bg1" id="outLookBarShow" style="height: 100%; width: 100%" valign="top"
                align="middle" name="outLookBarShow">
                <%=LoadTopMenu()%>
            </td>
        </tr>
    </table>
</body>
</html>
