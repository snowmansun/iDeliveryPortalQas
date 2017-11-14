/***************自定义叠加层，可作为站点显示在地图上******************/
function MyMarker(map, options) {
    // Now initialize all properties.   
    this.latlng = options.latlng; //设置图标的位置
    this.image_ = options.image;  //设置图标的图片
    this.labelText = options.labelText || '标记';
    this.labelClass = options.labelClass || 'shadow';//设置文字的样式
    this.clickFun = options.clickFun;//注册点击事件
    //    this.labelOffset = options.labelOffset || new google.maps.Size(8, -33);
    this.map_ = map;

    this.div_ = null;
    // Explicitly call setMap() on this overlay   
    this.setMap(map);
}

MyMarker.prototype = new google.maps.OverlayView();

//初始化图标
MyMarker.prototype.onAdd = function () {
    // Note: an overlay's receipt of onAdd() indicates that  
    // the map's panes are now available for attaching   
    // the overlay to the map via the DOM.    
    // Create the DIV and set some basic attributes.  
    var div = document.createElement('DIV'); //创建存放图片和文字的div
    div.style.border = "none";
    div.style.borderWidth = "0px";
    div.style.position = "absolute";
    div.style.cursor = "hand";
    div.style.width = "100px";
    div.onclick = this.clickFun || function () { };//注册click事件，没有定义就为空函数
    // Create an IMG element and attach it to the DIV.  
    //var img = document.createElement("img"); //创建图片元素
    //img.src = this.image_;
    //img.style.width = "100%";
    //img.style.height = "100%";
    //初始化文字标签
    var label = document.createElement('div');//创建文字标签
    label.className = this.labelClass;
    label.innerHTML = this.labelText;
    label.style.position = 'absolute';
    //label.style.width = '100px';
    //	label.style.fontWeight = "bold";
    label.style.textAlign = 'left';
    label.style.padding = "1px";
    label.style.fontSize = "6px";
    //	label.style.fontFamily = "Courier New";

    //div.appendChild(img);
    div.appendChild(label);
    this.div_ = div;
    // We add an overlay to a map via one of the map's panes.  
    // We'll add this overlay to the overlayImage pane.  
    var panes = this.getPanes();
    panes.overlayLayer.appendChild(div);
}

//绘制图标，主要用于控制图标的位置
MyMarker.prototype.draw = function () {
    // Size and position the overlay. We use a southwest and northeast   
    // position of the overlay to peg it to the correct position and size.  
    // We need to retrieve the projection from this overlay to do this.  
    var overlayProjection = this.getProjection();
    // Retrieve the southwest and northeast coordinates of this overlay  
    // in latlngs and convert them to pixels coordinates.  
    // We'll use these coordinates to resize the DIV.  
    var position = overlayProjection.fromLatLngToDivPixel(this.latlng);   //将地理坐标转换成屏幕坐标
    //  var ne = overlayProjection.fromLatLngToDivPixel(this.bounds_.getNorthEast());    
    // Resize the image's DIV to fit the indicated dimensions.   
    var div = this.div_;
    div.style.left = position.x -16 + 'px';
    div.style.top = position.y  + 20 + 'px';
    //控制图标的大小
    div.style.width = '0px';
    div.style.height = '0px';
}

MyMarker.prototype.onRemove = function () {
    this.div_.parentNode.removeChild(this.div_);
    this.div_ = null;
}

//Note that the visibility property must be a string enclosed in quotes 
MyMarker.prototype.hide = function () {
    if (this.div_) {
        this.div_.style.visibility = "hidden";
    }
}

MyMarker.prototype.show = function () {
    if (this.div_) {
        this.div_.style.visibility = "visible";
    }
}

//显示或隐藏图标
MyMarker.prototype.toggle = function () {
    if (this.div_) {
        if (this.div_.style.visibility == "hidden") {
            this.show();
        } else {
            this.hide();
        }
    }
}


