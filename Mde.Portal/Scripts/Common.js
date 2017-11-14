(function ($) {
    //关闭AJAX相应的缓存 
    $.ajaxSetup({
        cache: false 
    });
    $(document).keydown(function (e) {
        var doPrevent;
        if (e.keyCode == 8) {
            var d = e.srcElement || e.target;
            //想禁止什么组件的退格键回退，就往下增加其属性
            if (d.tagName.toUpperCase() == 'INPUT'
                    || d.tagName.toUpperCase() == 'TEXTAREA') {
                doPrevent = d.readOnly || d.disabled;
            }
            else {
                doPrevent = true;
            }
        }
        else {
            doPrevent = false;
        }
        if (doPrevent) {
            e.preventDefault();
        }
    });
    //绑定组织架构
    $.fn.bindChannel = function () {
        $(this).combotree({
            url: "/Customer/GetTreeChannelJson?time="+new Date(),
            method: "get",
            multiple: true,
            cascadeCheck: true,
            panelHeight: "auto",
            lines: true,
            onLoadSuccess: function (node, data) {
                $(this).tree("collapseAll");
            }
        })
    };

    //绑定用户组下拉
    $.fn.bindUserGroup = function (type) {
        var topSelect;
        if (type == "all")
            topSelect = "true";
        else
            type = "false";

        $(this).combobox({
            editable: false,
            method: "get",
            url: "/System/GetDropDownRole?all=" + topSelect+"&time="+new Date(),
            valueField: 'id',
            textField: 'name'
        });
    };

    //绑定字典表数据
    $.fn.bindDirectory = function (type, topSelect) {
        if (topSelect == "all")
            topSelect = "true";
        else
            topSelect = "false";
        $(this).combobox({
            editable: false,
            method: "get",
            url: "/System/GetDropDownDirectoryDetail?type=" + type + "&all=" + topSelect+"&time="+new Date(),
            valueField: 'id',
            textField: 'name'
        });
    };


    //打开选择组织架构窗口
    $.fn.showOrganization = function (callBack) {
        var returnValue = window.showModalDialog("/Organization/Select", null, "dialogWidth=300px;dialogHeight=500px");
        if (!returnValue)
            return;

        if (returnValue.length == 1)
            $(this).val(returnValue[0].name);
        else {
            if (language == "zh-cn")
                $(this).val("已选择" + returnValue.length + "个");
            else
                $(this).val("Selected " + returnValue.length);
        }

        var ids = [];
        for (i = 0; i < returnValue.length; i++)
            ids.push(returnValue[i].id);

        //返回的ID放到隐藏控件里面
        var hiddenID = $(this).attr('id') + '_ids';
        if ($("#" + hiddenID).length > 0)
            $("#" + hiddenID).val(ids.join(","));
        else {
            var $htmlHide = $("<input id=\"" + hiddenID + "\" name=\"" + hiddenID + "\" type=\"hidden\" value=\"" + ids.join(",") + "\" />");
            $(this).parent().append($htmlHide);
        }

        //如果有回调函数则调用
        if (callBack)
            callBack(ids);
    }

    //返回选择的组织架构ID串(id1,id2,id3...)
    $.fn.getOrganization=function () {
        var hiddenID = $(this).attr('id') + '_ids';
        var $hidden = $(this).siblings("#" + hiddenID);

        return $hidden.val();
    }

    //将Form的参数转换成Json格式
    $.fn.serializeJson = function () {
        var serializeObj = {};
        var array = this.serializeArray();
        var str = this.serialize();
        $(array).each(function () {
            if (serializeObj[this.name]) {
                if ($.isArray(serializeObj[this.name])) {
                    serializeObj[this.name].push(this.value);
                } else {
                    serializeObj[this.name] = [serializeObj[this.name], this.value];
                }
            } else {
                serializeObj[this.name] = this.value;
            }
        });
        return serializeObj;
    };

})(jQuery);

//验证required项全是空格
$.extend($.fn.validatebox.defaults.rules, {
    isBlank: {
        validator: function (value, param) { return $.trim(value) != '' },
        message: 'This field is required.'
    }
});

$.getUrlParam = function (name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}
function CheckButtonRole(url) {
    var ModuleID = $.getUrlParam('ModuleID');
    var jsonData = { ModuleID: ModuleID };
    //var url = "/Base/GetRoleFromCache";
    $.ajax({
        type: "post",
        data: jsonData,
        dataType: "json",
        url: url,
        success: function (data) {
            var action = eval("(" + data[0].Action + ")");

            var buttonRole = data[0].ButtonRole;
            buttonRole = "," + buttonRole + ",";
            var button = $('div.datagrid div.datagrid-toolbar a');
            var count = 0;
            for (var i = 0; i < button.length; i++) {
                var toolbar = button[i];
                var key = toolbar.id;
                var btnKey = key.split('_')[1];
                if (buttonRole.indexOf(',' + btnKey + ',') > 0) {
                    $('div.datagrid div.datagrid-toolbar a').eq(i).show();
                    count++;
                }
                else {
                    $('div.datagrid div.datagrid-toolbar a').eq(i).hide();
                }
            }
            if (count == 0) {
                //如果按钮都没权限，可直接隐藏toolbar
                $('div.datagrid div.datagrid-toolbar').hide();
            }
        }
    });
}

function compareDate(strDate1,strDate2){
    var date1 = new Date(strDate1.replace(/\-/g, "\/"));
    var date2 = new Date(strDate2.replace(/\-/g, "\/"));
    return date1-date2;
}

//获取当前时间
function formatterDate(date) {
    var day = date.getDate() > 9 ? date.getDate() : "0" + date.getDate();
    var month = (date.getMonth() + 1) > 9 ? (date.getMonth() + 1) : "0"
    + (date.getMonth() + 1);
    var hor = date.getHours();
    var min = date.getMinutes();
    var sec = date.getSeconds();
    return date.getFullYear() + '-' + month + '-' + day;//+ " " + hor + ":" + min + ":" + sec;
};

function ww4(date) {
    var y = date.getFullYear();
    var m = date.getMonth() + 1;
    var d = date.getDate();
    var h = date.getHours();
    return y + '-' + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d);
}

function ChangeDateFormat(jsondate) {
    jsondate = jsondate.replace("/Date(", "").replace(")/", "");//替换  
    if (jsondate.indexOf("+") > 0) {
        jsondate = jsondate.substring(0, jsondate.indexOf("+"));//截取  
    }
    else if (jsondate.indexOf("-") > 0) {
        jsondate = jsondate.substring(0, jsondate.indexOf("-"));//截取 
    }
    var date = new Date(parseInt(jsondate, 10));
    var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;//月份  
    var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();//时间  
    return date.getFullYear() + "-" + month + "-" + currentDate;//组合起来  
}
function ChangeTimeFormat(jsondate) {
    console.log(jsondate);
    jsondate = jsondate.replace("/Date(", "").replace(")/", "");//替换  
    if (jsondate.indexOf("+") > 0) {
        jsondate = jsondate.substring(0, jsondate.indexOf("+"));//截取  
    }
    else if (jsondate.indexOf("-") > 0) {
        jsondate = jsondate.substring(0, jsondate.indexOf("-"));//截取 
    }
    var date = new Date(parseInt(jsondate, 10));
    var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;//月份  
    var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();//时间  
    var hour = date.getHours() < 10 ? "0" + date.getHours() : date.getHours();//时
    var minute = date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();//分
    var second = date.getSeconds() < 10 ? "0" + date.getSeconds() : date.getSeconds();//秒
    return date.getFullYear() + "-" + month + "-" + currentDate +" " + hour + ":" + minute + ":" + second;//组合起来
}

//扩展Data的format方法
Date.prototype.format = function (format) {
    var o = {
        "M+": this.getMonth() + 1, //month
        "d+": this.getDate(), //day
        "h+": this.getHours(), //hour
        "m+": this.getMinutes(), //minute
        "s+": this.getSeconds(), //second
        "q+": Math.floor((this.getMonth() + 3) / 3), //quarter
        "S": this.getMilliseconds() //millisecond
    }
    if (/(y+)/.test(format)) format = format.replace(RegExp.$1,
    (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o) if (new RegExp("(" + k + ")").test(format))
        format = format.replace(RegExp.$1,
        RegExp.$1.length == 1 ? o[k] :
        ("00" + o[k]).substr(("" + o[k]).length));
    return format;
}

var JsonDataFormat = {
    //EasyUI用DataGrid用日期格式化
    Time: function (value, rec, index) {
        if (value == undefined) {
            return "";
        }
        /*json格式时间转js时间格式*/
        var value = value.replace("/", "").replace("/", "");
        var obj = eval('(' + "{Date: new " + value + "}" + ')');
        var dateValue = obj["Date"];
        if (dateValue.getFullYear() < 1900) {
            return "";
        }
        var val = dateValue.format("hh:mm");
        return val.substr(11, 5);
    },
    DateTime: function (value, rec, index) {
        if (value == undefined) {
            return "";
        }
        /*json格式时间转js时间格式*/
        var value = value.replace("/", "").replace("/", "");
        var obj = eval('(' + "{Date: new " + value + "}" + ')');
        var dateValue = obj["Date"];
        if (dateValue.getFullYear() < 1900) {
            return "";
        }

        return dateValue.format("yyyy-MM-dd hh:mm");
    },

    //EasyUI用DataGrid用日期格式化
    Date: function (value, rec, index) {
        if (value == undefined) {
            return "";
        }
        /*json格式时间转js时间格式*/
        var value = value.replace("/", "").replace("/", "");
        var obj = eval('(' + "{Date: new " + value + "}" + ')');
        var dateValue = obj["Date"];
        if (dateValue.getFullYear() < 1900) {
            return "";
        }

        return dateValue.format("yyyy-MM-dd");
    }
};

function GetNowDate() {
    var date = new Date();
    var strDate = date.getFullYear() + "-";
    strDate += (date.getMonth() + 1) + "-";
    strDate += date.getDate();

    return strDate;
};


function fmoney(s, n) {
    n = n > 0 && n <= 20 ? n : 2;
    s = parseFloat((s + "").replace(/[^\d\.-]/g, "")).toFixed(n) + "";//n:理论保留小数位
    var l = s.split(".")[0].split("").reverse(),
    r = s.split(".")[1];
    t = "";
    for (i = 0; i < l.length; i++) {
        t += l[i] + ((i + 1) % 3 == 0 && (i + 1) != l.length ? "," : "");
    };
    n = parseInt(r).toString().length;
    var p = ".";
    if (n == 1 && parseInt(r) == 0) {
        n = 0;
        p = "";
    };

    var value = t.split("").reverse().join("") + p + r.substring(0, n);//n:保留小数位

    return value;
}


function DateFormat(str) {
    var value = str.split('-');
    var year = value[0];
    var month = value[1];
    var day = value[2];
    if (month < 10 && month.length < 2)
        month = "0" + month;
    if (day < 10 && day.length < 2)
        day = "0" + day;
    value = year + "-" + month + "-" + day;

    return value;
};