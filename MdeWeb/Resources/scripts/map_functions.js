var map, manager;
var centerLatitude = 40.736462, centerLongitude = -73.98777, startZoom = 12;


function createMarkerClickHandler(marker,windowHtml) {
  return function() {
    marker.openInfoWindowHtml(windowHtml);
    return false;
  };
}


function createMarker(latlng,index,windowHtml,imageUrl,addlist) {

  var icon = new GIcon();
  icon.image = imageUrl;
  icon.iconSize = new GSize(16, 16);
  icon.iconAnchor = new GPoint(8, 8);
  icon.infoWindowAnchor = new GPoint(25, 7);

  opts = {
    "icon": icon,
    "clickable": true,
    "labelText": index,
    "labelOffset": new GSize(0, 0)
  };
 
  var marker = new LabeledMarker(latlng, opts);
  var handler = createMarkerClickHandler(marker, windowHtml);
	
  GEvent.addListener(marker, "click", handler);
  
  if (addlist)
  {
  var listItem = document.createElement('li');
  listItem.innerHTML = '<a href="#">' + index+'</a>';
  listItem.getElementsByTagName('a')[0].onclick = handler;

  document.getElementById('sidebar-list').appendChild(listItem);
  }
  return marker;
}
//本方法仅适用于GIS模块中右边显示的文本能有颜色
//Jeff.Yuan 05-14 14:23 
function createMarkerX(latlng,index,windowHtml,imageUrl,addlist,itemtxtcolor,lbltxtcolor) {

  var icon = new GIcon();
  icon.image = imageUrl;
  icon.iconSize = new GSize(16, 16);
  icon.iconAnchor = new GPoint(8, 8);
  icon.infoWindowAnchor = new GPoint(25, 7);
  var newindex = "";
  //地图上LABLE的颜色
  if(lbltxtcolor!="")
  {
     newindex  = '<span style=\"color:'+lbltxtcolor+'\"><b>'+index+'<b/></span></a>';
  }
  opts = {
    "icon": icon,
    "clickable": true,
    "labelText": newindex,
    "labelOffset": new GSize(0, 0)
  };
 
  var marker = new LabeledMarker(latlng, opts);
  var handler = createMarkerClickHandler(marker, windowHtml);
	
  GEvent.addListener(marker, "click", handler);
  
  if (addlist)
  {
  var listItem = document.createElement('li');
  listItem.innerHTML = '<a href="#"><span style=\"color:'+itemtxtcolor+'\">' + index+'</span></a>';
  listItem.getElementsByTagName('a')[0].onclick = handler;

  document.getElementById('sidebar-list').appendChild(listItem);
  }
  return marker;
}