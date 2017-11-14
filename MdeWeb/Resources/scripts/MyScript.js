var ajaxType = "Post";
var ajaxAsync = false;
var ajaxContentType = "application/json; charset=utf-8";
var ajaxDataType = "json";

function Post(requestUrl, paramsString) {
    var strRetValue = "";
    if (paramsString != null)
        paramsString = "{" + paramsString + "}";
        $.ajax({
            type: ajaxType,
            async: ajaxAsync,
            url: requestUrl,
            contentType: ajaxContentType,
            dataType: ajaxDataType,
            data: paramsString,
            success: function(data) {
            strRetValue = data.d;
            },error: function(err) {
                strRetValue = "";
            }
        });
        return String(strRetValue);
}

//ªÒ»°URL÷µ
function getQueryString(name) 
{
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null)
        return unescape(r[2]);
                
    return null;
}