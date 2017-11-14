
var name="List";

function HtmlEncode(text){
	return text.replace(/&/g, '&amp').replace(/\"/g, '"').replace(/</g, '&lt;').replace(/>/g, '&gt;');
}
function _checkbox(str,identity){
	var arr=str.split("^");
	var ck="",bc="";
	var thisarr="";
	var t="", v="";
	var thisstr="";
	//
    for(var i=0;i<arr.length;i++){
	    thisarr=arr[i].split("=");
	    if (thisarr[0].length>0){
		    t=(thisarr.length==2)?thisarr[0]:arr[i];
		    v=(thisarr.length==2)?thisarr[1]:arr[i];
		    //
		    thisstr="<label class=\"nock\" for=\"i_"+name+"_"+i+"\" id=\"l_"+name+"_"+i
		        +"\" style=\"word-spacing: 0em;\" onclick=\"document.getElementById('"
		    +HtmlEncode(v)+"$"+identity+"').checked=!document.getElementById('"
		    +HtmlEncode(v)+"$"+identity+"').checked; this.className=(document.getElementById('"
		    +HtmlEncode(v)+"$"+identity+"').checked)?'cked':'nock';\">";
		    thisstr+="<input class=\"checkbox\" onclick=\"document.getElementById('"
		    +HtmlEncode(v)+"$"+identity+"').checked=!document.getElementById('"
		    +HtmlEncode(v)+"$"+identity+"').checked; this.parentElement.className=(document.getElementById('"
		    +HtmlEncode(v)+"$"+identity+"').checked)?'cked':'nock';\" type=\"checkbox\" name=\""+name+"\" id=\""+HtmlEncode(v)+"$"+identity+"\" value=\""+HtmlEncode(t)+"\" \/> ";
		    thisstr+=HtmlEncode(t)+"</label>";
		    //onpropertychange=\"this.parentElement.className=(document.getElementById('" +HtmlEncode(v)+"$"+identity+"').checked)?'cked':'nock';\"
		    document.write(thisstr);
	    }
    }
}
function _getv(e, top_div, all_text) {
    //获取该列表中的所有项
    var o=get_all_checkBox(e,top_div);
    //获取文本元素
    var txt=e.parentElement.getElementsByTagName("input")[0];
    //保持列表值
	var allvalue="";
	//保存列表显示文本
	var allText="";
	//异常处理
	if(typeof(o)=="undefined"){return "";}
	if (typeof(o.length)=="undefined"){
		if(o.checked){return o.value+ ",";}else{return "";}
	}	
	//循环取值
	for(var i=0;i<o.length;i++){
		if(typeof(o[i].children[0])!="undefined" && typeof(o[i].children[0].value)!="undefined"){		    
		    if(o[i].children[0].checked){
			    allvalue +=o[i].children[0].id.substr(0,o[i].children[0].id.indexOf("$"))+",";
			    allText +=o[i].children[0].value+",";
			}
		}
	}
	//取得列表中选择的文本
	allText=allText.substring(0, allText.lastIndexOf(','));	
	allText=allText.length>0?allText:all_text;
	txt.value=allText;
	txt.title=allText;
	
	//取得列表中选择的值
	allvalue=allvalue.substring(0, allvalue.lastIndexOf(','));
	
	//隐藏该列表
	e.parentElement.getElementsByTagName("div")[0].style.display="none";
	//保存值
	e.parentElement.children[3].value=allvalue.length>0?allvalue:'-1';
}
function _clear_v(e,top_div,checked){
    //获取该列表中的所有项
    var o=get_all_checkBox(e,top_div);
    //获取文本元素
    var txt=e.parentElement.getElementsByTagName("input")[0];
	//
	if(typeof(o)=="undefined"){return "";}
	if (typeof(o.length)=="undefined"){
		if(o.checked){return o.value+ ",";}else{return "";}
	}
	for(var i=0;i<o.length;i++){
	    if(typeof(o[i].children[0])!="undefined"){
		    o[i].children[0].checked=checked;
		}
	}
	//清楚文本，隐藏该列表
	//txt.value="";
	//e.parentElement.getElementsByTagName("div")[0].style.display="none";
}

function select_onclick(e, top_div, return_value, all_text) {
    //获取下拉列表
    var div=e.parentElement.getElementsByTagName("div")[0];
    //
    div.style.display=div.style.display=="block"?"none":"block";
    //让文本获得焦点
    var txt=e.parentElement.getElementsByTagName("input")[0];
    //    
    if(div.style.display == "block"){
        //
        //div.style.left = txt.offsetLeft;
        //div.style.top = txt.offsetTop+txt.offsetHeight;
        //
        div.style.left = getPosLeft(txt);
        div.style.top = getPosTop(txt)+txt.offsetHeight;
        //
        var values=e.parentElement.children[3].value.split(',')
        var o;
        var _id;
        for(var j=0; j< values.length; j++)
        {
            //获取该列表中的所有项
            o=get_all_checkBox(e,top_div);
            //初始化
	        for(var i=0;i<o.length;i++){
		        if(typeof(o[i].children[0])!="undefined"){
		            _id=o[i].children[0].id;
		            if(_id.substr(0,_id.indexOf("$"))==values[j]){
			            o[i].children[0].checked=true;
			            o[i].children[0].parentElement.className="cked";
			        }
		        }
	        }
        }
         return  false;
     }
     else{
         _getv(e, top_div, all_text);
        return  return_value;
     }
    
}

function get_all_checkBox(e,top_div){
    var parent=e;
    //循环找到最外层DIV
    for(var i=0; i<10; i++)
    {
        if(parent.id==top_div)
            break;
        parent=parent.parentElement;
    }
    //获取列表中的所有项
    return parent.getElementsByTagName("div")[0].children;
}

//循环取得控件的Left值
function getPosLeft(obj)
{
   var l = obj.offsetLeft;
   while(obj = obj.offsetParent)
   {
       l += obj.offsetLeft;
   }
   return l;
} 

//循环取得控件的Top值
function getPosTop(obj)
{
   var t = obj.offsetTop;
   while(obj = obj.offsetParent)
   {
       t += obj.offsetTop;
   }
   return t;
} 
