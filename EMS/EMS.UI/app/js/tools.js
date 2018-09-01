var EMS = {

	Tool:{
		//日期时间对象的操作
		dateFormat:function(date){
			var year = date.getFullYear();
			var month =date.getMonth()+1;
			var dayOfMonth = date.getDate();

			if(month<10)
				month="0"+month;
			if(dayOfMonth<10)
				dayOfMonth="0"+dayOfMonth;

			return (year+"-"+month+"-"+dayOfMonth);
		},
		appendZero:function(num){
			if(num<10)
				return "0"+num;

			return num;
		},
		sortByObjTime:function(a,b){
			var dateA = new Date(a.time);
			var dateB = new Date(b.time);

			return dateA>dateB ? 1 : -1;
		},
		statusProcess:function(status){
			if(status === 401)
				window.location = "/Account/Login";
		}
	},

	DOM :{

		//type:日期类型，date：日期时间,$calendar：日期空间,$box:日期时间显示框
		initDateTimePicker:function(type,date,$calendar,$box,option){
			$calendar.datetimepicker('remove');
			var dayOfWeek=date.getDay();//0~6 :周日到周六（本周表示周一到周日）
			var dateOfMonth=date.getDate();//1~31:
			var month=date.getMonth();//0~11:1月到12月
			var year=date.getFullYear();

			var endDate;
			var getTypeDate;

			switch(type){
				case "WEEKSTART":
					getTypeDate=EMS.Tool.dateFormat(new Date(year,month,dateOfMonth-dayOfWeek+1));
				break;
				case "WEEKEND":
					getTypeDate=EMS.Tool.dateFormat(new Date(year,month,dateOfMonth+7-dayOfWeek));
				break;
				case "CURRENTDATE":
					getTypeDate=EMS.Tool.dateFormat(new Date(year,month,dateOfMonth));
				break;
				case "SEVENAGO":
					getTypeDate=EMS.Tool.dateFormat(new Date(year,month,dateOfMonth-6));
				break;
				case "THREEDAYAGO":
					getTypeDate=EMS.Tool.dateFormat(new Date(year,month,dateOfMonth-2));
				break;
				case "YEARMONTH":
					getTypeDate =year +"-"+EMS.Tool.appendZero(month+1);
				break;
				case "YEAR":
					getTypeDate = year;
				break;
			}

			$box.val(getTypeDate);

			if(option!=undefined){
				//option.endDate = date;
				$calendar.datetimepicker(option);
			}else{
				$calendar.datetimepicker({
					format:'yyyy-mm-dd',
			        language: 'zh-CN',
			        weekStart: 1,
			        todayBtn: 1,
			 		todayHighlight: 1,
			        autoclose: 1,
			        startView: 2,
			        minView: 2,
			        endDate:date,
			        forceParse: 0,
			        pickerPosition: "bottom-left"
				});
			}
			

			return this;
		},
		initSelect:function(data,$select,displayName,value){
			$select.html("");
			$.each(data,function(key,val){
				$select.append("<option value="+val[value]+">"+val[displayName]+"</option>");
			});
			return this;//允许使用连缀方式对多个select标签进行填充
		},
		initTreeview:function(data,$treeview,option){
			var treedata = new Array();
			var pattern = new RegExp('\\,\\"nodes\\"\\:\\[\\]',"g");
			for (var i = 0; i < data.length; i++) {
				var tempStr = JSON.stringify(data[i]);
				treedata.push(JSON.parse(tempStr.replace(pattern,"")));
			};

			option.data = treedata;

			$treeview.treeview(option);
		},
		showTable:function($table,columns,rows,options){
			options.columns = columns;
			options.data=rows;
			$table.bootstrapTable(options);
		}
	},
	Loading:{
		show:function($container){
			if($container == undefined)
				$container = $("#main-content");

			$container.busyLoad("show",{
	            background: "rgba(255, 255, 255, 0.50)",
	            image: "data:image/gif;base64,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"
	        });
			$(".busy-load-container").css('z-index',3);
		},
		hide:function($container){
			if($container == undefined)
				$container = $("#main-content");

			$container.busyLoad("hide");
		}
	},
	Chart:{
		//绘制echarts饼图
		showPie:function(charts,$Pie,names,values,seriesName){

			option = {
				tooltip : {
			        trigger: 'item',
			        formatter: "{a} <br/>{b} : {c} ({d}%)"
			    },
			    legend: {
			        orient: 'horizontal',
			        bottom: 'bottom',
			        data: names
			    },
			    series : [
			        {
			            name: seriesName,
			            type: 'pie',
			            radius : '75%',
			            center: ['50%', '50%'],
			            data:values,
			            itemStyle: {
			                emphasis: {
			                    shadowBlur: 10,
			                    shadowOffsetX: 0,
			                    shadowColor: 'rgba(0, 0, 0, 0.5)'
			                }
			            }
			        }
			    ],
			    color:['#FF0000','#FF8C00', '#1E90FF', '#9ACD32', '#91c7ae','#749f83',  '#ca8622', '#bda29a','#6e7074', '#546570', '#c4ccd3']
			};

			charts.init($Pie.get(0),'macarons').setOption(option);
		},
		showLine:function(charts,$Line,legendData,xData,series){
			var option = {
			            tooltip: {
			                trigger: 'axis'
			            },
			            /*legend: {
			                data: legendData,
			                bottom:'bottom'
			            },*/
			            grid: {
			                left: 50,
			                right: 10,
			                top:5,
			                bottom:'20%'
			            },
			            xAxis: {
			                type: 'category',
			                boundaryGap: false,
			                data: xData
			            },
			            yAxis: {
			                type: 'value',
			                axisLabel: {
			                formatter: '{value}'
			            }
			        },
		            series: series,
            	color:['#74B000','#1E90FF','#FF8C00','#FF0000','#9ACD32', '#91c7ae','#749f83',  '#ca8622', '#bda29a','#6e7074', '#546570', '#c4ccd3']
        	};

        	if(legendData != undefined)
        		option.legend =  {
			                data: legendData,
			                bottom:'bottom'
			            };

        	charts.init($Line.get(0),'macarons').setOption(option);
		},
		showBar:function(charts,$Bar,legendData,xData,series,gridSetting,defaultColor){
			var option = {
			            tooltip: {
			                trigger: 'axis'
			            },
			            /*legend: {
			                data: legendData,
			                bottom:'bottom'
			            },*/
			            grid: {
			                left: 50,
			                right: 10,
			                top:5,
			                bottom:'20%'
			            },
			            xAxis: {
			                type: 'category',
			                data: xData
			            },
			            yAxis: {
			                type: 'value',
			                axisLabel: {
			                formatter: '{value}'
			            }
			        },
		            series: series,
            	color:['#1E90FF','#FF8C00','#FF0000','#9ACD32', '#91c7ae','#749f83',  '#ca8622', '#bda29a','#6e7074', '#546570', '#c4ccd3']
        	};

        	if(legendData != undefined)
        		option.legend =  legendData;
			if(gridSetting != undefined)
				option.grid = gridSetting;

			if(defaultColor != undefined)
				delete option.color;


        	charts.init($Bar.get(0),'macarons').setOption(option);
		},
		showLandscapeBar:function(charts,$Bar,legendData,xData,series,gridSetting){
			var option = {
	            tooltip: {
	                trigger: 'axis',
	                axisPointer: {
			            type: 'shadow'
			        }
	            },
	            legend: {
	                data: legendData,
	                bottom:'bottom'
	            },
	            grid: {
			        left: '3%',
			        right: '4%',
			        bottom: '3%',
			        top:'1%',
			        containLabel: true
			    },
	            xAxis: {
	                type: 'value',
			        axisLine:{show:false},
			        axisTick:{show:false},
			        axisLabel:{show:false},
			        boundaryGap: [0, 0.01]
	            },
	            yAxis: {
	                type: 'category',
        			data: xData
	            },
		       
	            series: series,
            	color:['#1E90FF','#FF8C00','#FF0000','#9ACD32', '#91c7ae','#749f83',  '#ca8622', '#bda29a','#6e7074', '#546570', '#c4ccd3']
        	};

        	/*if(legendData != undefined)
        		option.legend =  legendData;*/
			if(gridSetting != undefined)
				option.grid = gridSetting;


        	charts.init($Bar.get(0),'macarons').setOption(option);
		},
		showStackBar:function(charts,$Pie,names,xData,series,unit){
			var option = {
				tooltip : {
		        	trigger: 'axis',
			        axisPointer : {            // 坐标轴指示器，坐标轴触发有效
			            type : 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
			        }
		    	},
				legend: {
		        	data:names,
		        	bottom:'bottom'
			    },
			    grid: {
			        left: '3%',
			        right: 10,
			        bottom: '10%',
			        top:25,
			        containLabel: true
			    },
			    xAxis : [
			        {
			            type : 'category',
			            data : xData
			        }
			    ],
			    yAxis : [
			        {
			            type : 'value',
			            name:unit
			        }
			    ],
			    series :series,
			    color:['#1E90FF','#FF8C00','#FF0000','#9ACD32', '#91c7ae','#749f83',  '#ca8622', '#bda29a','#6e7074', '#546570', '#c4ccd3']
			}

			charts.init($Pie.get(0),'macarons').setOption(option);
		}
	}

};

//添加浏览器对filter的支持
if (!Array.prototype.filter)
{
  Array.prototype.filter = function(fun /*, thisArg */)
  {
    "use strict";

    if (this === void 0 || this === null)
      throw new TypeError();

    var t = Object(this);
    var len = t.length >>> 0;
    if (typeof fun !== "function")
      throw new TypeError();

    var res = [];
    var thisArg = arguments.length >= 2 ? arguments[1] : void 0;
    for (var i = 0; i < len; i++)
    {
      if (i in t)
      {
        var val = t[i];

        // NOTE: Technically this should Object.defineProperty at
        //       the next index, as push can be affected by
        //       properties on Object.prototype and Array.prototype.
        //       But that method's new, and collisions should be
        //       rare, so use the more-compatible alternative.
        if (fun.call(thisArg, val, i, t))
          res.push(val);
      }
    }

    return res;
  };
}