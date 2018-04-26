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
					getTypeDate =year +"-"+(month+1);
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
            	color:['#1E90FF','#FF8C00','#FF0000','#9ACD32', '#91c7ae','#749f83',  '#ca8622', '#bda29a','#6e7074', '#546570', '#c4ccd3']
        	};

        	if(legendData != undefined)
        		option.legend =  {
			                data: legendData,
			                bottom:'bottom'
			            };

        	charts.init($Line.get(0),'macarons').setOption(option);
		},
		showBar:function(charts,$Bar,legendData,xData,series,gridSetting){
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
        		option.legend =  {
			                data: legendData,
			                bottom:'bottom'
			            };
			if(gridSetting != undefined)
				option.grid = gridSetting;


        	charts.init($Bar.get(0),'macarons').setOption(option);
		}
	}

};