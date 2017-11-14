var tmp;
var m_objPopupWindow
//关闭弹出窗口
function closePopup() 
{
    if (m_objPopupWindow != null) {

        if (!m_objPopupWindow.closed) {
            m_objPopupWindow.close();
        }
        m_objPopupWindow = null;
    }
}

function openPopup(url,name,width,height)
{
    closePopup();
	var iLeft = ( screen.availWidth - width ) / 2;
	var iTop = ( screen.availHeight - height ) / 2;

    m_objPopupWindow = window.open(url,name,"modal=yes,toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=0,resizable=0,left=" + iLeft + ",top=" + iTop + ",width="+ width +",height="+ height +"",false);
    m_objPopupWindow.focus();
}

function openWindow(url,name)
{
    closePopup();

    m_objPopupWindow = window.open(url);
    m_objPopupWindow.focus();
}

function CheckValue(object)
{
    if ((event.keyCode<48 && event.keyCode!=8  && event.keyCode!=9) || event.keyCode>57)
    {event.returnValue =false;}

}
function CheckValue1(object)
{
    if ((event.keyCode<48 && event.keyCode!=8  && event.keyCode!=9) || (event.keyCode>57 && event.keyCode!=190))
    {event.returnValue =false;}
    if (event.keyCode==190)
    {
        
        if (object.value.indexOf(".")!=-1)
        {

            event.returnValue=false;
        }
    }    
}
function IsNumber(object)
    {
        if (isNaN(object.value))
        {
            object.value=tmp;
        }
        else
            tmp=object.value;
    }

function Submit(object)
{
    object.disabled="true";
    object.value="提交中......";
}

//绑定最顶层下拉
function BindTop(obj)
{
	obj.options.add(new Option(defaultText,""));
	for (i in train)
	{
		if (train[i][2]==0)
			obj.options.add(new Option(train[i][1],train[i][0]));
	}
}
//绑定其它下拉
function Bind(obj,selectValue)
{
	obj.options.length=0;
	obj.options.add(new Option(defaultText,""));
	if (selectValue!="")
	{
		for (i in train)
		{
			if (train[i][2]==selectValue)
				obj.options.add(new Option(train[i][1],train[i][0]));
		}
	}
}
//初始化
function init(text)
{
	defaultText=text;
	BindTop(arguments[1]);
	for (var i=2;i<arguments.length ;i++ )
		Bind(arguments[i],"");
}
function slide(obj)
{
    if (obj.style.display=="none")
        obj.style.display="block";
    else
        obj.style.display="none";
}