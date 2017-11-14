<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Navigation.aspx.cs" Inherits="DatapoolWeb.Navigation" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>左侧导航</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Expires" content="0" />

    <script src="main_left.files/sohuflash_1.js" type="text/javascript"></script>

    <script src="main_left.files/prototype_1.5.0.js" type="text/javascript"></script>

    <script src="main_left.files/pp18030.js" type="text/javascript"></script>

    <link href="Resources/Style/style.css" rel="stylesheet" />

    <link href="main_left.files/global.css" type="text/css" rel="stylesheet" />
    <link href="main_left.files/style3.bergdog.css" type="text/css" rel="stylesheet" />
    <meta content="MSHTML 6.00.2900.5626" name="GENERATOR" />
</head>
<body style="background: #e5cc64">
    <div id="menu">

        <%=LoadMenuItem() %>
        
        <%=WriteScript()%>

        </div>
    <br />
</body>
</html>
