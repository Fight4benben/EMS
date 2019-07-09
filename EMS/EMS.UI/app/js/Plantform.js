
var Plantform = (function(){
	function _plantform(){
        var baseUrl= '/api/Platform';
        var dayData = [];
        var monthData = [];
        var yearData = [];

        this.show = function(){
			getDataFromServer(baseUrl,"");
        };
        function getDataFromServer(url,params){
			$.getJSON(url,params, function(data) {
                
                showOverView(data);//平台概况
                showEnergyshare(data);//能源占比
				showMap(data);//地图
				showDMY(data);//日月年
			}).fail(function(e){
				
			});
        };
        function showOverView(data){
            console.log(data);
            if (!data.hasOwnProperty('device'))
                return;
            $('.D-survey1 h3').text(data.device.build);
            $('.D-survey2 h3').text(data.device.collector);
            $('.D-survey3 h3').text(data.device.meter);
            //累积运行天数
            $('.D-time span').text(data.runningDay);
        };
        function showMap(data){
            var bmap = new BMap.Map("map");
            var myIcon = new BMap.Icon("/app/img/mapCon.png",new BMap.Size(50,50));

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
			bmap.centerAndZoom(pointArr[0],4);
			bmap.enableScrollWheelZoom(true);
            var styleJson = [
                {
                    "featureType": "land",
                    "elementType": "all",
                    "stylers": {
                        "color": "#253758"
                    }
                }, {
                    "featureType": "water",
                    "elementType": "all",
                    "stylers": {
                        "color": "#0b4186"
                    }
                },{
                    "featureType": "railway",
                    "elementType": "all",
                    "stylers": {
                        "visibility": "off"
                    }
                },{
                    "featureType": "highway",
                    "elementType": "geometry",
                    "stylers": {
                        "color": "#004981"
                    }
                },{
                    "featureType": "highway",
                    "elementType": "geometry.fill",
                    "stylers": {
                        "color": "#005b96",
                        "lightness":1
                    }
                },{
                    "featureType": "highway",
                    "elementType": "labels",
                    "stylers": {
                        "visibility": "off"
                    }
                },{
                    "featureType": "arterial",
                    "elementType": "geometry",
                    "stylers": {
                        "color": "#004981"
                    }
                },{
                    "featureType": "arterial",
                    "elementType": "geometry.fill",
                    "stylers": {
                        "color": "#00508b"
                    }
                },{
                    "featureType": "poi",
                    "elementType": "all",
                    "stylers": {
                        "visibility": "off"
                    }
                },{
                    "featureType": "green",
                    "elementType": "all",
                    "stylers": {
                        "color": "#056197",
                        "visibility": "off"
                    }
                },{
                    "featureType": "subway",
                    "elementType": "all",
                    "stylers": {
                        "visibility": "off"
                    }
                },{
                    "featureType": "manmade",
                    "elementType": "all",
                    "stylers": {
                        "visibility": "off"
                    }
                },
            ];
            bmap.setMapStyle({styleJson:styleJson});
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
        };
        function showEnergyshare(data){
            $("#pei").html('');
            if(!data.hasOwnProperty('standardcoal'))
				return;
                var values = [];
                var names = [];
    
                $.each(data.standardcoal, function (key, val) {
                    values.push({ name: val.name, value: val.value });
                    names.push(val.name);
                });
                EMS.Chart.showPieOver(echarts, $('#pei'), names, values, "能源占比",true);
        };
        function showDMY(data){
            if (!data.hasOwnProperty('device'))
                return;
            dayData = data.dayDate;
            monthData = data.monthDate;
            yearData = data.yearDate;
            
            $.each(dayData,function(key,val){
                switch (val.id) {
                    case "01000":
                        $("#yesPower").text('今日用电');
                        $("#yespowerValue").text(val.lastValue.toFixed(2));
                        $("#dayPower").text('昨日用电');
                        $("#dayPowerValue").text(val.value.toFixed(2));
                        break;
                    case "02000":
                        $("#yesWater").text('今日用水');
                        $("#yesWaterValue").text(val.lastValue.toFixed(2));
                        $("#dayWater").text('昨日用水');
                        $("#dayWaterValue").text(val.value.toFixed(2));
                        break;
                    case "03000":
                        $("#yesGas").text('今日燃气');
                        $("#yesGasValue").text(val.lastValue.toFixed(2));
                        $("#dayGas").text('昨日燃气');
                        $("#dayGasValue").text(val.value.toFixed(2));
                        break;
                    case "40000":
                        $("#yesWgas").text('今日蒸汽');
                        $("#yesWgasValue").text(val.lastValue.toFixed(2));
                        $("#dayWgas").text('昨日蒸汽');
                        $("#dayWgasValue").text(val.value.toFixed(2));
                        break;
                    default:
                        break;
                }
            })
        };
        $("#day").click(function(){
            initDom();
            $("#day").attr("class",'select');
            $("#month").removeAttr("class",'select');
            $("#year").removeAttr("class",'select');

            $.each(dayData,function(key,val){
                switch (val.id) {
                    case "01000":
                        $("#Unit").html('电'+'<br/>(kW.h)')
                        $("#yesPower").text('今日用电');
                        $("#yespowerValue").text(val.lastValue.toFixed(2));
                        $("#dayPower").text('昨日用电');
                        $("#dayPowerValue").text(val.value.toFixed(2));
                        break;
                    case "02000":
                        $("#yesWater").text('今日用水');
                        $("#yesWaterValue").text(val.lastValue.toFixed(2));
                        $("#dayWater").text('昨日用水');
                        $("#dayWaterValue").text(val.value.toFixed(2));
                        break;
                    case "03000":
                        $("#yesGas").text('今日燃气');
                        $("#yesGasValue").text(val.lastValue.toFixed(2));
                        $("#dayGas").text('昨日燃气');
                        $("#dayGasValue").text(val.value.toFixed(2));
                        break;
                    case "40000":
                        $("#yesWgas").text('今日蒸汽');
                        $("#yesWgasValue").text(val.lastValue.toFixed(2));
                        $("#dayWgas").text('昨日蒸汽');
                        $("#dayWgasValue").text(val.value.toFixed(2));
                        break;
                    default:
                        break;
                }
            })
        });
        $("#month").click(function(){
            initDom();
            $("#month").attr("class",'select');
            $("#day").removeAttr("class",'select');
            $("#year").removeAttr("class",'select');
            $.each(monthData,function(key,val){
                switch (val.id) {
                    case "01000":
                        $("#Unit").html('电'+'<br/>(kW.h)')
                        $("#yesPower").text('上月用电');
                        $("#yespowerValue").text(val.lastValue.toFixed(2));
                        $("#dayPower").text('当月用电');
                        $("#dayPowerValue").text(val.value.toFixed(2));
                        break;
                    case "02000":
                        $("#yesWater").text('上月用水');
                        $("#yesWaterValue").text(val.lastValue.toFixed(2));
                        $("#dayWater").text('当月用水');
                        $("#dayWaterValue").text(val.value.toFixed(2));
                        break;
                    case "03000":
                        $("#yesGas").text('上月燃气');
                        $("#yesGasValue").text(val.lastValue.toFixed(2));
                        $("#dayGas").text('当月燃气');
                        $("#dayGasValue").text(val.value.toFixed(2));
                        break;
                    case "40000":
                        $("#yesWgas").text('上月蒸汽');
                        $("#yesWgasValue").text(val.lastValue.toFixed(2));
                        $("#dayWgas").text('当月蒸汽');
                        $("#dayWgasValue").text(val.value.toFixed(2));
                        break;
                    default:
                        break;
                }
            })
        });
        $("#year").click(function(){
            initDom();
            $("#year").attr("class",'select');
            $("#day").removeAttr("class",'select');
            $("#month").removeAttr("class",'select');
            $.each(yearData,function(key,val){
                switch (val.id) {
                    case "01000":
                        $("#Unit").html('电'+'<br/>(MW.h)')
                        $("#yesPower").text('去年用电');
                        $("#yespowerValue").text((val.lastValue/1000).toFixed(2));
                        $("#dayPower").text('今年用电');
                        $("#dayPowerValue").text((val.value/1000).toFixed(2));
                        break;
                    case "02000":
                        $("#yesWater").text('去年用水');
                        $("#yesWaterValue").text(val.lastValue.toFixed(2));
                        $("#dayWater").text('今年用水');
                        $("#dayWaterValue").text(val.value.toFixed(2));
                        break;
                    case "03000":
                        $("#yesGas").text('去年燃气');
                        $("#yesGasValue").text(val.lastValue.toFixed(2));
                        $("#dayGas").text('今年燃气');
                        $("#dayGasValue").text(val.value.toFixed(2));
                        break;
                    case "40000":
                        $("#yesWgas").text('去年蒸汽');
                        $("#yesWgasValue").text(val.lastValue.toFixed(2));
                        $("#dayWgas").text('今年蒸汽');
                        $("#dayWgasValue").text(val.value.toFixed(2));
                        break;
                    default:
                        break;
                }
            })
        });
        function initDom(){
            $("#yesPower").text('');
            $("#yespowerValue").text('--');
            $("#dayPower").text('');
            $("#dayPowerValue").text('--');

            $("#yesWater").text('');
            $("#yesWaterValue").text('--');
            $("#dayWater").text('');
            $("#dayWaterValue").text('--');

            $("#yesGas").text('');
            $("#yesGasValue").text('--');
            $("#dayGas").text('');
            $("#dayGasValue").text('--');

            $("#yesWgas").text('');
            $("#yesWgasValue").text('--');
            $("#dayWgas").text('');
            $("#dayWgasValue").text('--');
        }
    }

	return _plantform;
})();

jQuery(document).ready(function ($) {

    $('.fanhui').click(function(){
        window.history.back(-1);
    })

    var plantform = new Plantform();
	plantform.show();
});