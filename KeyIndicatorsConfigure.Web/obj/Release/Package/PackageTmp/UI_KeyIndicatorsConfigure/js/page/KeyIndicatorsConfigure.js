var mItemId = '';
var pageItemid = '';
$(function () {
    pageItemid = $('#Hiddenfield_PageId').val();
    LoadMainDataGrid("first");
    LoadPageIdType();
    LoadSelectType();
    initPageAuthority();
});
function onOrganisationTreeClick(node) {
    $('#organizationName').textbox('setText', node.text);
    $('#organizationId').val(node.OrganizationId);
    mOrganizationId = node.OrganizationId;
    var mLevel = mOrganizationId.split('_');
    if (mLevel.length != 5) {
        $.messager.alert('提示', '请选择产线级别！');
    }
    //LoadStaffInfo(mOrganizationId);
    //PrcessTypeItem(mOrganizationId);
}
function initPageAuthority() {
    $.ajax({
        type: "POST",
        url: "MasterSlaveMachinedescription.aspx/AuthorityControl",
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: false,//同步执行
        success: function (msg) {
            PageOpPermission = msg.d;
            //增加
            if (PageOpPermission[1] == '0') {
                $("#id_add").linkbutton('disable');
            }
            //修改
            //if (authArray[2] == '0') {
            //    $("#edit").linkbutton('disable');
            //}
            //删除
            //if (PageOpPermission[3] == '0') {
            //    $("#id_deleteAll").linkbutton('disable');
            //}
        }
    });
}
function LoadPageIdType() {
    $.ajax({
        type: 'POST',
        url: "KeyIndicatorsConfigure.aspx/PageIdType",
        data: '{pageItemid:"' + pageItemid + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            Pageid = jQuery.parseJSON(msg.d);
            $('#comb_ProcessType').combobox({
                data: Pageid.rows,
                valueField: 'PAGE_ID',
                textField: 'PageIdName',
                onLoadSuccess: function () { //加载完成后,设置选中第一项
                    var val = $(this).combobox("getData");
                    for (var item in val[0]) {
                        if (item == "PAGE_ID") {
                            $(this).combobox("select", val[0][item]);
                        }
                    }
                }
            });
        }
    });
}

function LoadSelectType() {
    $.ajax({
        type: 'POST',
        url: "KeyIndicatorsConfigure.aspx/SelectType",
        data: '{pageItemid:"' + pageItemid + '"}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            Pageid = jQuery.parseJSON(msg.d);
            $('#selectType').combobox({
                data: Pageid.rows,
                valueField: 'PAGE_ID',
                textField: 'PageIdName',
                onLoadSuccess: function () { //加载完成后,设置选中第一项
                    var val = $(this).combobox("getData");
                    for (var item in val[0]) {
                        if (item == "PAGE_ID") {
                            $(this).combobox("select", val[0][item]);
                        }
                    }
                }
            });
        }
    });
}

function LoadMainDataGrid(type, myData) {
    if (type == "first") {
        $('#grid_Main').datagrid({
            columns: [[
                  { field: 'ItemId', title: '标识列', width: 100, hidden: true },
                  //{ field: 'Name', title: '考核分组', width: 100 },
                  //{ field: 'CycleType', title: '考核周期', width: 60 },
                  { field: 'ItemName', title: '名称', width: 120, align: 'left' },
                  { field: 'Unit', title: '单位', width: 60, align: 'left' },
                  {
                      field: 'ValueType', title: '计算类型', width: 60, align: 'left',
                      formatter: function (value, row) {
                          if (row.ValueType == 'ElectricityConsumption') {
                              return  "电耗";
                          }
                          if (row.ValueType == 'CoalConsumption') {
                              return  "煤耗";
                          }
                          if (row.ValueType == 'DCSTagAvg') {
                              return  "DCS标签";
                          }
                      }
                  },
                  { field: 'Tags', title: '标签名', width: 80, align: 'left' },
                  { field: 'SubtrahendTags', title: '减数标签名', width: 80, align: 'left' },
                  { field: 'Min', title: '最小值', width: 60, align: 'left' },
                  { field: 'Max', title: '最大值', width: 60, align: 'left' },
                  { field: 'AlarmH', title: '高报警值', width: 60, align: 'left' },
                  { field: 'AlarmHH', title: '高高报警值', width: 65, align: 'left' },
                  { field: 'DisplayIndex', title: '显示顺序', width: 60, align: 'left' },
                  {
                      field: 'MessageEnabled', title: '短信报警', width: 60, align: 'left',
                      formatter: function (value, row) {
                          if (row.MessageEnabled == 'True') {
                                  return "是";
                              }
                          if (row.MessageEnabled == 'False') {
                                  return "否";
                              }
                          }
                  },//添加的
                  {
                      field: 'edit', title: '编辑', width: 100, formatter: function (value, row, index) {
                          var str = "";
                          str = '<a href="#" onclick="editFun(true,\'' + row.ItemId + '\')"><img class="iconImg" src = "/lib/extlib/themes/images/ext_icons/notes/note_edit.png" title="编辑页面" onclick="editFun(true,\'' + row.ItemId + '\')"/>编辑</a>';
                          str = str + '<a href="#" onclick="deleteFun(\'' + row.ItemId + '\')"><img class="iconImg" src = "/lib/extlib/themes/images/ext_icons/notes/note_delete.png" title="删除页面"  onclick="deleteFun(\'' + row.ItemId + '\')"/>删除</a>';
                          //str = str + '<img class="iconImg" src = "/lib/extlib/themes/images/ext_icons/notes/note_deleteFun.png" title="删除页面" onclick="deleteFunPageFun(\'' + row.id + '\');"/>删除';
                          return str;
                      }
                  }
            ]],
            fit: true,
            toolbar: "#toorBar",
            idField: 'ItemId',
            rownumbers: true,
            singleSelect: true,
            striped: true,
            data: []
        });
    }
    else {
        $('#grid_Main').datagrid('loadData', myData);
    }
}

function Query() {
    var morganizationId = $('#organizationId').val();
    if (morganizationId == '') {
        $.messager.alert('提示', '请先选择需要分析的组织机构。');
        return;
    }
    var mPageId = $('#comb_ProcessType').combobox('getValue');
    $.ajax({
        type: "POST",
        url: "KeyIndicatorsConfigure.aspx/GetKeyIndicators",
        data: "{mOrganizationId:'" + mOrganizationId + "',mPageId:'" + mPageId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $.messager.progress('close');
            var myData = jQuery.parseJSON(msg.d);
            if (myData.total == 0) {
                LoadMainDataGrid("last", []);
                $.messager.alert('提示', '没有查询到记录！');
            } else {
                LoadMainDataGrid("last", myData);
            }
        },
        error: function () {
            $.messager.progress('close');
            $("#grid_Main").datagrid('loadData', []);
            $.messager.alert('失败', '加载失败！');
        }
    });
}
function refresh() {
    Query();
}
function addFun() {
    editFun(false);
}
var mMessage = '';
var selectType = '';
function editFun(IsEdit, editContrastId) {
    if (IsEdit) {
        IsAdd = false;
        $('#grid_Main').datagrid('selectRecord', editContrastId);
        var data = $('#grid_Main').datagrid('getSelected');
        $('#itemName').textbox('setText', data.ItemName);
        $('#unit').textbox('setText', data.Unit);       
        $('#valueType').combobox('setValue', data.ValueType);
        $('#selectType').combobox('setValue', data.PageId);
        $('#tags').textbox('setText', data.Tags);
        $('#subtrahendTags').textbox('setText', data.SubtrahendTags);
        $('#min').numberbox('setText', data.Min);
        $('#max').numberbox('setText', data.Max);
        $('#alarmH').numberbox('setText', data.AlarmH);
        $('#alarmHH').numberbox('setText', data.AlarmHH);
        $('#displayIndex').numberbox('setText', data.DisplayIndex);

        if (data.MessageEnabled == 'True') {
            mMessage = "1";
        }
        if (data.MessageEnabled == 'False') {
            mMessage = "0";
        }
        $('#messageEnabled').combobox('setValue', mMessage);//添加的
        mItemId = data.ItemId;
    }
    else {
        IsAdd = true;
        $('#itemName').textbox('clear');
        $('#unit').textbox('clear');
        $('#valueType').combobox('clear');
        $('#tags').textbox('clear');
        $('#subtrahendTags').textbox('clear');
        $('#min').numberbox('clear');
        $('#max').numberbox('clear');
        $('#alarmH').numberbox('clear');
        $('#alarmHH').numberbox('clear');
        $('#displayIndex').numberbox('clear');
        if (mOrganizationId == "" && mOrganizationId == undefined) {
            $.messager.alert('提示', '请选择组织机构！');
        }
    }
    $('#AddandEditor').window('open');
}
function save() {
    var mItemName = $('#itemName').textbox('getText');
    var mUnit = $('#unit').textbox('getText');
    var mValueType = $('#valueType').combobox('getValue');
    var mCaculateType = '';
    if (mValueType == 'ElectricityConsumption' || mValueType == 'CoalConsumption') {
        mCaculateType = 'Energy';
    }
    if (mValueType == 'DCSTagAvg') {
        mCaculateType = 'DCSTag';
    }
    var mPageId = $('#selectType').combobox('getValue');
    var mGroupId = $('#selectType').combobox('getValue');
    var mTags = $('#tags').textbox('getText');
    var mSubtrahendTags = $('#subtrahendTags').textbox('getText');;
    var mMin = $('#min').numberbox('getText');
    var mMax = $('#max').numberbox('getText');
    var mAlarmH = $('#alarmH').numberbox('getText');
    var mAlarmHH = $('#alarmHH').numberbox('getText');
    var mDisplayIndex = $('#displayIndex').numberbox('getText');
    var mEnabled = $('#enabled').combobox('getValue');
    var mMessageEnabled = $('#messageEnabled').combobox('getValue');//添加的
    if (mItemName == "" || mUnit == "" || mTags == "" || mMin == "") {
        $.messager.alert('提示', '请填写未填项!');
    }
    else {
        var mUrl = "";
        var mdata = "";
        if (IsAdd) {
            mUrl = "KeyIndicatorsConfigure.aspx/AddKeyIndicators";
            mdata = "{mOrganizationId:'" + mOrganizationId + "',mItemName:'" + mItemName + "',mUnit:'" + mUnit + "',mValueType:'" + mValueType + "',mCaculateType:'" + mCaculateType + "',mPageId:'" + mPageId + "',mGroupId:'" + mGroupId + "',mTags:'" + mTags + "',mSubtrahendTags:'" + mSubtrahendTags + "',mMin:'" + mMin + "',mMax:'" + mMax + "',mAlarmH:'" + mAlarmH + "',mAlarmHH:'" + mAlarmHH + "',mDisplayIndex:'" + mDisplayIndex + "',mEnabled:'" + mEnabled + "',mMessageEnabled:'" + mMessageEnabled + "'}";
        } else if (IsAdd == false) {
            mUrl = "KeyIndicatorsConfigure.aspx/EditKeyIndicators";
            mdata = "{mItemId:'" + mItemId + "',mOrganizationId:'" + mOrganizationId + "',mItemName:'" + mItemName + "',mUnit:'" + mUnit + "',mValueType:'" + mValueType + "',mCaculateType:'" + mCaculateType + "',mPageId:'" + mPageId + "',mGroupId:'" + mGroupId + "',mTags:'" + mTags + "',mSubtrahendTags:'" + mSubtrahendTags + "',mMin:'" + mMin + "',mMax:'" + mMax + "',mAlarmH:'" + mAlarmH + "',mAlarmHH:'" + mAlarmHH + "',mDisplayIndex:'" + mDisplayIndex + "',mEnabled:'" + mEnabled + "',mMessageEnabled:'" + mMessageEnabled + "'}";
        }
        $.ajax({
            type: "POST",
            url: mUrl,
            data: mdata,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                var myData = msg.d;
                if (myData == 1) {
                    $.messager.alert('提示', '操作成功！');
                    $('#AddandEditor').window('close');
                    refresh();
                }
                else {
                    $.messager.alert('提示', '操作失败！');
                    refresh();
                }
            },
            error: function () {
                $.messager.alert('提示', '操作失败！');
                refresh();
            }
        });
    }
}
function deleteFun(deleteFunContrastId) {
    $.messager.confirm('提示', '确定要删除吗？', function (r) {
        if (r) {
            $('#grid_Main').datagrid('selectRecord', deleteFunContrastId);
            var data = $('#grid_Main').datagrid('getSelected');

            mItemId = data.ItemId;

            $.ajax({
                type: "POST",
                url: "KeyIndicatorsConfigure.aspx/deleteKeyIndicators",
                data: "{mItemId:'" + mItemId + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var myData = msg.d;
                    if (myData == 1) {
                        $.messager.alert('提示', '删除成功！');
                        $('#AddandEditor').window('close');
                        refresh();
                    }
                    else {
                        $.messager.alert('提示', '操作失败！');
                        refresh();
                    }
                },
                error: function () {
                    $.messager.alert('提示', '操作失败！');
                    $('#AddandEditor').window('close');
                    refresh();
                }
            });
        }
    })
}



