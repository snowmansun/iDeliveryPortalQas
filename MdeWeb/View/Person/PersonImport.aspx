<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="true"  CodeBehind="PersonImport.aspx.cs" Inherits="DatapoolWeb.View.Person.PersonImport" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>人员导入</title>
    <link href="../../Resources/styles/style.css" rel="stylesheet" />
    <script src="../JavaScript/common.js" type="text/javascript"></script>

    <script src="../JavaScript/My97DatePicker/WdatePicker.js" type="text/javascript"></script>

    <script src="../../Resources/scripts/jquery-1.4.2.min.js" type="text/javascript"></script>
    <script src="../../Resources/scripts/jquery.form.js"></script>
    <script src="../../JavaScript/Uploadify/jquery.uploadify.js"></script>
    <script src="../../JavaScript/Uploadify/jquery-1.4.2.min.js"></script>
    <link href="../../JavaScript/Uploadify/uploadify.css" rel="stylesheet" />
     <script type="text/javascript">
         $(document).ready(function(){
             $("[id$='imgUploading']").hide();
             $("[id$='litImport']").hide();
         });

         //function getPath(obj) {
         //    if (obj) {
         //        if (window.navigator.userAgent.indexOf("MSIE") >= 1) {
         //            obj.select();
         //            obj.blur();
         //            return document.selection.createRange().text;
         //        }
         //        else if (window.navigator.userAgent.indexOf("Firefox") >= 1) {
         //            if (obj.files) {
         //                return obj.files.item(0).getAsDataURL();
         //            }
         //            return obj.value;
         //        }
         //        return obj.value;
         //    }
         //}

         $(function () {
             $("#file_upload").uploadify({
                 //开启调试  
                 'debug': true,
                 //是否自动上传  
                 'auto': true,
                 'swf'      : 'uploadify.swf',
                 'uploader': 'UploadHandler.ashx',
                 'multi': false,
                 'queueID': 'uploadfileQueue',
                 //返回一个错误，选择文件的时候触发  
                 'onSelectError': function (file, errorCode, errorMsg) {
                     switch (errorCode) {
                         case -100:
                             alert("上传的文件数量已经超出系统限制的" + $('#file_upload').uploadify('settings', 'queueSizeLimit') + "个文件！");
                             break;
                         case -110:
                             alert("文件 [" + file.name + "] 大小超出系统限制的" + $('#file_upload').uploadify('settings', 'fileSizeLimit') + "大小！");
                             break;
                         case -120:
                             alert("文件 [" + file.name + "] 大小异常！");
                             break;
                         case -130:
                             alert("文件 [" + file.name + "] 类型不正确！");
                             break;
                     }
                 },
                 //检测FLASH失败调用  
                 'onFallback': function () {
                     alert("您未安装FLASH控件，无法上传图片！请安装FLASH控件后再试。");
                 },
                 //上传到服务器，服务器返回相应信息到data里  
                 'onUploadSuccess': function (file, data, response) {
                     //alert(data);  
                 }
             });
         });

         $(function () {
             $("#btnSubmit").click(function () {
                 if ($("[id$='fulFile']").val() == "") {
                     alert("请选择要导入的人员文件!");
                     return false;
                 }

                 $("[id$='form1']").ajaxSubmit({
                     url: "UploadHandler.ashx",
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
                             alert("导入人员信息成功!");
                         }
                         else if (msg == "-2") {
                             alert("禁止导入 0 KB大小的文件!");
                         }
                         else if (msg == "-1") {
                             alert("请选择要导入的人员文件!");
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
                    <asp:Literal ID="Literal8" runat="server" Text="人员管理"></asp:Literal>
                </td>
                <td class="wsd_1rightarrow">&nbsp;
                </td>
                <td class="wsd_titlefont1">
                    <asp:Literal ID="Literal2" runat="server" Text="人员导入"></asp:Literal>
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
                <td class="tablefield" style="width: 216px">人员数据文件</td>
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

                <td nowrap="nowrap" Width ="120px">
                    <div id="uploadfileQueue" style="padding: 3px;">  
                    </div>  
                    <div id="file_upload">  
                    </div>  
                    
                </td>

               
                <td >
                 <%--<asp:Label ID="lblMessage" runat="server" Text="" ForeColor ="red"></asp:Label>--%>
                </td>
            </tr>
        </table>
        <br />
        
    </form>
</body>
</html>
