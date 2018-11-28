var Map = (function(){
	function _map(){

		
		var bmap = new BMap.Map("map");
		var myIcon = new BMap.Icon("/app/img/mapCon.png",new BMap.Size(50,50));

		this.show = function(){
			var url = "/api/Map";

			$.getJSON(url, "", function(data) {
				//console.log(data);
				showMap(data);
			}).fail(function(e){
				EMS.Tool.statusProcess(e.status);
			});
		}

		function showMap(data){
			

			if(!data.hasOwnProperty('builds'))
				return;

			var pointArr=[];
			var markerArr=[];
			var buildArr=[];

			$.each(data.builds, function(key, val) {
				if(val.hasOwnProperty('buildLong') && val.hasOwnProperty('buildLat')){
					var tempPoint = new BMap.Point(val.buildLong,val.buildLat);
					pointArr.push(tempPoint);
					buildArr.push({name:val.buildName,id:val.buildID});
					markerArr.push(new BMap.Marker(tempPoint,{icon:myIcon}));
				}
			});

			var labelArr=[];
			bmap.centerAndZoom(pointArr[0],10);
			bmap.enableScrollWheelZoom(true);

			$.each(markerArr, function(key, val) {
				labelArr.push(new BMap.Label(buildArr[key].name,{offset:new BMap.Size(40,5)}));
				var marker = markerArr[key];
				var buildId = buildArr[key].id;

				marker.addEventListener('click',function(){
					$.cookie('buildId',buildId,{path:'/'});
					
					location.href="/Home";
				});

				bmap.addOverlay(marker);
				marker.setLabel(labelArr[key]);

				labelArr[key].setStyle({
					maxWidth:'none',
					fontSize:'15px',
					padding:'5px',
					border:'none',
					color:'#fff',
					background:'#ff8355',
					borderRadius:'5px'
				});
			});

		}

	};

	return _map;
})();

jQuery(document).ready(function($) {
	
	$("#mappage").attr("class","start active");
	$("#mappage").attr("class","active");

	var map = new Map();
	map.show();

});