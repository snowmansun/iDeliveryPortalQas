
window.onload = function(){
    $('#loading-mask').fadeToggle();
}

$(function () {
	tabCloseEven();
})


//初始化左侧
function InitLeftMenu(strJSON) {
    var obj = eval("("+strJSON+")");//转换后的JSON对象
	$("#nav").accordion({animate:false,fit:true,border:false});
	$.each(obj, function (i, n) {
	    var menulist = '';
	    menulist += '<ul class="navlist">';
	    $.each(n.children, function (j, o) {
	        var url = o.url + "?ModuleID=" + o.menu_id;
	        menulist += '<li>'
	        menulist += '   <div >';
	        menulist += '       <a ref="' + o.menu_id + '" href="#" rel="' + url + '" ><i class="fa fa-caret-right"></i><span class="nav">' + o.name + '</span></a>';
	        menulist += '   </div>';
	        menulist += '</li>';
	    })
	    menulist += '</ul>';

	    $('#nav').accordion('add', {
	        title: n.name,
	        content: menulist,
	        border: false,
	        iconCls: 'icon-' + n.icon
	    });

	});

	$('#nav').accordion('select', 0);

    //点击菜单栏选项，右侧显示a标签地址内容
	$('.navlist li div').each(function () {
	    $(this).click(function () {
	        //鼠标点击菜单栏a标签时改变样式
	        $('.navlist li div').removeClass("selected");
	        $(this).addClass("selected");
	        var tabTitle = $(this).children("a").children('.nav').text();
	        var url = $(this).children("a").attr("rel");
	        var menuid = $(this).children("a").attr("ref");
	        var icon = $(this).children("a").find('.icon').attr('class');
	        var tab = $(this).children("a").text();
	        addTab(tabTitle, url, icon);
	    })
	});
}

function addTab(subtitle, url, icon) {
    $("#tabTitle").val(subtitle);
    $("#CurrentUrl").val(url);
    if (!$('#tabs').tabs('exists', subtitle)) {
        $('#tabs').tabs('add', {
            title: subtitle,
            fit:true,
            content: createFrame(url),
            closable: true,
            icon: icon
            //bodyCls: "no-overflow"
        });
    } else {
        //点击已经打开的tab链接时，设置当前显示选中tab，同时刷新页面
        $('#tabs').tabs('select', subtitle);
        var currTab = self.parent.$('#tabs').tabs('getSelected'); //获得当前tab
        self.parent.$('#tabs').tabs('update', {
            tab: currTab,
            options: {
                fit:true,
                content: createFrame(url)
            }
        });
    }
    tabClose();
}

function createFrame(url)
{
    var s = '<iframe scrolling="auto" frameborder="0"  src="' + url + '" style="border:none;width:100%;height:100%;"></iframe>';
	return s;
}
