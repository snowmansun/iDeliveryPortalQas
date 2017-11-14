$.extend($.fn.form.methods, {
    extSubmit: function (jq, info) {
        if (!jq.form('validate'))
            return;
        jq.form('submit', {
            url: info.url,
            beforeSubmit: $.messager.progress(),
            success: function (data) {
                $.messager.progress("close");
                data = eval('(' + data + ')');
                if (data.Success) {
                    info.success();
                }
                else
                    if (language == "zh-cn")
                        $.messager.alert('操作提示', '保存失败：' + data.Message, "info");
                    else
                        $.messager.alert('Operation Tips', 'Save Failed：' + data.Message, "info");
            }
        })
    }
})

$.extend($.fn.datagrid.methods, {
    getUnchecked: function (jq) {
        var checkedRows = jq.datagrid('getChecked');
        var uncheckRows = [];
        var allRows = jq.datagrid('getRows');

        for (i = 0; i < allRows.length; i++) {
            if ($.inArray(allRows[i], checkedRows) < 0)
                uncheckRows.push(allRows[i]);
        }

        return uncheckRows;
    }
})

//扩展Tree的清空选择方法
$.extend($.fn.tree.methods, {
    uncheckAll: function (target) {
        var roots = $(target).tree('getRoots');
        for (var i = 0; i < roots.length; i++) {
            //查找节点
            var node = $(target).tree('find', roots[i].id);
            //取消选中的结点
            $(target).tree('uncheck', node.target);
        }
    }
})

//实现EasyUI的pid这种扁平化的数据结构Json
$.fn.tree.defaults.loadFilter = function (data, parent) {
    var opt = $(this).data().tree.options;
    var idFiled,
	textFiled,
	parentField;
    if (opt.parentField) {
        idFiled = opt.idFiled || 'id';
        textFiled = opt.textFiled || 'text';
        parentField = opt.parentField;

        var i,
		l,
		treeData = [],
		tmpMap = [];

        for (i = 0, l = data.length; i < l; i++) {
            tmpMap[data[i][idFiled]] = data[i];
        }

        for (i = 0, l = data.length; i < l; i++) {
            if (tmpMap[data[i][parentField]] && data[i][idFiled] != data[i][parentField]) {
                if (!tmpMap[data[i][parentField]]['children'])
                    tmpMap[data[i][parentField]]['children'] = [];
                data[i]['text'] = data[i][textFiled];
                tmpMap[data[i][parentField]]['children'].push(data[i]);
            } else {
                data[i]['text'] = data[i][textFiled];
                treeData.push(data[i]);
            }
        }
        return treeData;
    }
    return data;
};

/**
	 * 扩展树表格级联勾选方法：
	 * @param {Object} container
	 * @param {Object} options
	 * @return {TypeName} 
	 */
$.extend($.fn.treegrid.methods, {
    /**
     * 级联选择
     * @param {Object} target
     * @param {Object} param 
     *		param包括两个参数:
     *			id:勾选的节点ID
     *			deepCascade:是否深度级联
     * @return {TypeName} 
     */
    cascadeCheck: function (target, param) {
        var opts = $.data(target[0], "treegrid").options;
        if (opts.singleSelect)
            return;
        var idField = opts.idField;//这里的idField其实就是API里方法的id参数
        var status = false;//用来标记当前节点的状态，true:勾选，false:未勾选
        var selectNodes = $(target).treegrid('getSelections');//获取当前选中项
        for (var i = 0; i < selectNodes.length; i++) {
            if (selectNodes[i][idField] == param.id)
                status = true;
        }
        //级联选择父节点
        selectParent(target[0], param.id, idField, status);
        selectChildren(target[0], param.id, idField, param.deepCascade, status);
        /**
         * 级联选择父节点
         * @param {Object} target
         * @param {Object} id 节点ID
         * @param {Object} status 节点状态，true:勾选，false:未勾选
         * @return {TypeName} 
         */
        function selectParent(target, id, idField, status) {
            var parent = $(target).treegrid('getParent', id);
            if (parent) {
                var parentId = parent[idField];
                if (status)
                    $(target).treegrid('select', parentId);
                else
                    $(target).treegrid('unselect', parentId);
                selectParent(target, parentId, idField, status);
            }
        }
        /**
         * 级联选择子节点
         * @param {Object} target
         * @param {Object} id 节点ID
         * @param {Object} deepCascade 是否深度级联
         * @param {Object} status 节点状态，true:勾选，false:未勾选
         * @return {TypeName} 
         */
        function selectChildren(target, id, idField, deepCascade, status) {
            //深度级联时先展开节点
            if (!status && deepCascade)
                $(target).treegrid('expand', id);
            //根据ID获取下层孩子节点
            var children = $(target).treegrid('getChildren', id);
            for (var i = 0; i < children.length; i++) {
                var childId = children[i][idField];
                if (status)
                    $(target).treegrid('select', childId);
                else
                    $(target).treegrid('unselect', childId);
                selectChildren(target, childId, idField, deepCascade, status);//递归选择子节点
            }
        }
    }
});