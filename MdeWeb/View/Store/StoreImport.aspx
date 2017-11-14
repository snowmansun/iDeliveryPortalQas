<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StoreImport.aspx.cs" Inherits="DatapoolWeb.View.Store.StoreImport" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>门店导入</title>
    <link href="../../Resources/styles/style.css" rel="stylesheet" />
    <script src="../JavaScript/common.js" type="text/javascript"></script>

    <script src="../JavaScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>

    <script src="../../Resources/scripts/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="../../Resources/scripts/jquery.form.js"></script>
     <script type="text/javascript">
         $(document).ready(function () {
             $("[id$='imgUploading']").hide();
             $("[id$='litImport']").hide();
         });

         $(function () {
             $("#btnSubmit").click(function () {
                 if ($("[id$='fulFile']").val() == "") {
                     alert("请选择要导入的门店文件!");
                     return false;
                 }

                 $("[id$='form1']").ajaxSubmit({
                     url: "UploadHandlerStore.ashx",
                     type: "post",
                     dataType: "text",
                     resetForm: "true",
                     beforeSubmit: function () {
                         $("[id$='fulFile']").hide();
                         $("[id$='imgUploading']").show();
                         $("[id$='litImport']").show();
                         $("[id$='btnSubmit']").hide();
                     },
                     success: function (msg) {
                         $("[id$='fulFile']").show();
                         $("[id$='imgUploading']").hide();
                         $("[id$='litImport']").hide();
                         $("[id$='btnSubmit']").show();
                         if (msg == "1") {
                             alert("导入门店信息成功!");
                         }
                         else if (msg == "-2") {
                             alert("禁止导入 0 KB大小的文件!");
                         }
                         else if (msg == "-1") {
                             alert("请选择要导入的门店文件!");
                         }
                         else if (msg == "-0") {
                             alert("导入失败!");
                         } else {
                             alert("其他错误!");
                         }
                         return false;
                     },
                     error: function (jqXHR, errorMsg, errorThrown) {
                         $("[id$='fulFile']").show();
                         $("[id$='imgUploading']").hide();
                         $("[id$='litImport']").hide();
                         $("[id$='btnSubmit']").show();
                         alert("错误信息:" + errorMsg);
                         return false;
                     }
                 });
             });
         });
     </script>
        <style type="text/css">
        td {
            height: 23px;
        }
            .auto-style1 {
                width: 315px;
            }
            #imgUploading {
                width: 113px;
            }
    </style>
</head>
<body class="wsd_option_area">
    <form id="form1" runat="server" enctype="multipart/form-data">
        <table id="wsd_title">
            <tr>
                <td class="wsd_titlefont">
                    <asp:Literal ID="Literal1" runat="server" Text="您当前的位置"></asp:Literal>
                </td>
                <td class="wsd_2rightarrow">&nbsp;
                </td>
                <td class="wsd_titlefont2">
                    <asp:Literal ID="Literal8" runat="server" Text="门店管理"></asp:Literal>
                </td>
                <td class="wsd_1rightarrow">&nbsp;
                </td>
                <td class="wsd_titlefont1">
                    <asp:Literal ID="Literal2" runat="server" Text="门店导入"></asp:Literal>
                </td>
            </tr>
        </table>
        <br />
              <table id="wsd_inputtable">
            <tr>
                <td class="tabletitle" colspan="5">
                    <asp:Literal ID="Literal4" runat="server" Text="导入"></asp:Literal>
                </td>
            </tr>
      
            <tr height="40px">
                <td class="tablefield" style="width: 216px">门店数据文件</td>
                <td nowrap="nowrap" Width ="120px">
                       <%--<asp:FileUpload ID="FileUpload" runat="server"  />--%>  
                    <%-- <cc1:AsyncFileUpload ID="AsyncFileUpload" runat="server" /> --%>
                     <input id="fulFile" name="fulFile" type="file" class="btn_sub" />
                     <img id="imgUploading" src="../../Resources/Images/uploading.gif"  alt="正在导入..." class="loading_img none" />
                    <asp:Literal ID="litImport" Text="正在导入..."></asp:Literal>
                </td>
                 <td  align="right" class="auto-style1">
                   <%-- <asp:Button runat="server" ID="btnImport" Text="导入" CssClass="wsd_button2" OnClick="btnImport_Click" />--%>
                    <input id="btnSubmit" type="button" value="导入" class="btn_sub" />
                </td>

<%--                <td nowrap="nowrap" Width ="120px">
                    <div id="uploadfileQueue" style="padding: 3px;">  
                    </div>  
                    <div id="file_upload">  
                    </div>  
                    
                </td>--%>

               
                <td >
                 <%--<asp:Label ID="lblMessage" runat="server" Text="" ForeColor ="red"></asp:Label>--%>
                </td>
            </tr>
        </table>
        <br />
        
    </form>
</body>
</html>
