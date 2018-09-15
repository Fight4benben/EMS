var DepartmentLimit = (function () {

    function _departmentLimit() {

        var selectedInfo;
        var unSelectedList = [];

        function initDom() {
            initDateTime();
            initOperation();
        }

        function initDateTime() {
            var today = new Date();
            var year = today.getFullYear();
            var month = today.getMonth();
            var date = today.getDate();

            EMS.DOM.initDateTimePicker('CURRENTDATE', new Date(year, month, date - 1), $("#StartDate"), $("#StartBox"));
            EMS.DOM.initDateTimePicker('CURRENTDATE', new Date(year, month, date), $("#EndDate"), $("#EndBox"));
        }

        function initOperation() {
            $("#AddNew").click(function () {
                $("#AddOrUpdate").html("新增");
                $("#limitValue").val("");
            });

            $("#AddOrUpdate").click(function (event) {
                var text = $(this).text();

                var buildId = $("#buildinglist").val();
                var departmentId = $("#unsetDeviceList").val();
                var startTimeTemp = EMS.Tool.getDateByString($("#StartBox").val() + ' ' +
                    $("#StartHour").val() + ":" + $("#StartMinute").val(), "yyyy-MM-dd");
                var endTimeTemp = EMS.Tool.getDateByString($("#EndBox").val() + ' ' +
                    $("#EndHour").val() + ":" + $("#EndMinute").val(), "yyyy-MM-dd");
                var isOverDay;

                if (startTimeTemp > endTimeTemp) {
                    alert("开始时间不能大于结束时间！");
                    return;
                }

                if (EMS.Tool.dateDiff(startTimeTemp, endTimeTemp) > 1) {
                    alert("日期相差只能为1天");
                    return;
                } else if (EMS.Tool.dateDiff(startTimeTemp, endTimeTemp) == 1) {
                    isOverDay = 1;
                } else
                    isOverDay = 0;

                var input = $("#limitValue").val();

                if (isNaN(input)) {
                    alert("请输入数字！");
                    return;
                }

                if (parseFloat(input) < 0) {
                    alert("报警值不能为负值！");
                    return;
                }

                var obj = {
                    buildId: buildId,
                    departmentID: departmentId,
                    startTime:
                        EMS.Tool.appendZero(parseInt($("#StartHour").val())) + ":" +
                        EMS.Tool.appendZero(parseInt($("#StartMinute").val())),
                    endTime:
                        EMS.Tool.appendZero(parseInt($("#EndHour").val())) + ":" +
                        EMS.Tool.appendZero(parseInt($("#EndMinute").val())),
                    isOverDay: isOverDay,
                    limitValue: input
                };

                $.post('/api/SettingAlarmDepartmentOverLimit/Set', obj, function (data) {
                    if (data == 1) {
                        initDateTime();
                        getDataFromServer("/api/SettingAlarmDepartmentOverLimit", "buildId=" + $("#buildinglist").val());
                    }
                    else
                        alert(text + "报警值失败！");
                });

            });
        }

        this.show = function () {
            initDom();
            var url = "/api/SettingAlarmDepartmentOverLimit";

            getDataFromServer(url, "");
        };


        function getDataFromServer(url, params) {
            EMS.Loading.show();
            $.getJSON(url, params, function (data) {
                //console.log(data);
                try {
                    showBuilds(data);
                    showTable(data);
                    unSelectedList = data.unsettingDept;
                    showUnsetList(data.unsettingDept);
                } catch (e) {
                    console.log(e);
                } finally {
                    EMS.Loading.hide();
                }
            }).fail(function (e) {
                EMS.Tool.statusProcess(e.status);
                EMS.Loading.hide();
            });
        }

        function showBuilds(data) {
            if (!data.hasOwnProperty('builds'))
                return;

            EMS.DOM.initSelect(data.builds, $("#buildinglist"), "buildName", "buildID");

            $("#buildinglist").change(function (event) {
                var buildId = $(this).val();

                getDataFromServer("/api/SettingAlarmDepartmentOverLimit", "buildId=" + $("#buildinglist").val());
            });
        }

        // function showEnergys(data){
        // 	if(!data.hasOwnProperty('builds'))
        // 		return;

        // 	EMS.DOM.initSelect(data.builds,$("#buildinglist"),"buildName","buildID");
        // }

        function showTable(data) {
            var height = $("#statisticTable").parent('div').height() - 61;
            $("#statisticTable").html('<table id="mainTable"></table>');
            $("#mainTable").attr('data-height', height);
            var columns = [
                { field: 'id', title: '设备编号' },
                { field: 'name', title: '设备名称' },
                { field: 'startTime', title: '起始时间' },
                { field: 'endTime', title: '结束时间' },
                { field: 'limitValue', title: '报警值' },
                { field: 'edit', title: '编辑' },
                { field: 'delete', title: '删除' }
            ];
            var rows = [];
            $.each(data.alarmLimitValues, function (index, val) {
                var row = {};
                row.id = val.id;
                row.name = val.name;
                row.startTime = val.startTime;
                row.endTime = val.endTime;
                row.limitValue = val.limitValue;
                row.edit = '<i class="limit-edit fa fa-2 fa-pencil-square-o"></i>';
                row.delete = '<i class="limit-trash fa fa-trash"></i>';
                rows.push(row);
            });

            EMS.DOM.showTable($("#mainTable"), columns, rows, { striped: true, classes: 'table table-border' });

            $("#mainTable").on('click-row.bs.table', function (e, row, $element, field) {
                $(".currentSelect").css('background', 'white').removeClass('currentSelect');
                $element.css('background', '#cee4f9').addClass('currentSelect');

                selectedInfo = row;

                switch (field) {
                    case 'edit':
                        $("#AddOrUpdate").html("修改");
                        $("#unsetDeviceList").html("");
                        EMS.DOM.initSelect([{ id: row.id, name: row.name }], $("#unsetDeviceList"), "name", "id");
                        $("#StartHour").val(parseInt(row.startTime.trim().split(':')[0]));
                        $("#StartMinute").val(parseInt(row.startTime.trim().split(':')[1]));
                        $("#EndHour").val(parseInt(row.endTime.trim().split(':')[0]));
                        $("#EndMinute").val(parseInt(row.endTime.trim().split(':')[1]));
                        $("#limitValue").val(row.limitValue);
                        break;
                    case 'delete':
                        $("#AddOrUpdate").html("保存");
                        var result = confirm("是否要删除" + row.name + "的报警值?");

                        if (!result)
                            return;

                        $.ajax({
                            url: '/api/SettingAlarmDepartmentOverLimit/Delete',
                            type: 'delete',
                            contentType: 'application/json',
                            data: JSON.stringify({
                                buildId: $("#buildinglist").val(),
                                departmentID: row.id
                            })
                        })
                            .done(function (data) {
                                if (data == 1) {
                                    getDataFromServer("/api/SettingAlarmDepartmentOverLimit", "buildId=" + $("#buildinglist").val());
                                } else {
                                    alert("删除报警值失败！");
                                }
                            })
                            .fail(function () {
                                console.log("error");
                            })
                            .always(function () {
                                console.log("complete");
                            });


                        break;

                }
            });

        }

        function showUnsetList(data) {
            $("#unsetDeviceList").html("");

            EMS.DOM.initSelect(data, $("#unsetDeviceList"), "name", "id");
        }

    };

    return _departmentLimit;

})();

jQuery(document).ready(function ($) {

    $("#settings").attr("class", "start active");
    $("#departmentLimit").attr("class", "active");

    var limit = new DepartmentLimit();
    limit.show();


});