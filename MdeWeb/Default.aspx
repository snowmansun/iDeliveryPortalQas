<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs"  Inherits="DatapoolWeb.Default"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head>
    <title>DATAPOOL</title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <script language="JavaScript" type="text/JavaScript">
<!--
    function MM_reloadPage(init) {  //reloads the window if Nav4 resized
        if (init == true) with (navigator) {
            if ((appName == "Netscape") && (parseInt(appVersion) == 4)) {
                document.MM_pgW = innerWidth; document.MM_pgH = innerHeight; onresize = MM_reloadPage;
            }
        }
        else if (innerWidth != document.MM_pgW || innerHeight != document.MM_pgH) location.reload();
    }
    MM_reloadPage(true);
    //-->
    </script>

</head>
<frameset rows="82,*,27" cols="*" frameborder="NO" border="0" framespacing="0" noresize>
   <frame name="topFrame" src="Head.aspx" scrolling="NO" noresize>
   <frameset id="fid" cols="164,9,*" rows="*" border="0" framespacing="0" frameborder="NO" noresize>
   	  <!--<frame name="menu" src="Menu.aspx" scrolling="no" noresize>-->
   	  <frame name="menu" src="Navigation.aspx" scrolling="yes" noresize>
   	  <frame name="center" src="center.htm" scrolling="no" noresize>
      <frame name="mainFrame" src="main.htm" scrolling="yes" noresize>
   </frameset>
   <frame name="bottomFrame" src="bottom.aspx"  scrolling="NO" noresize >
</frameset>
<noframes>
    <body>
        <p>
            此网页使用了框架，但您的浏览器不支持框架。
        </p>
    </body>
</noframes>
<body>
</body>
</html>
