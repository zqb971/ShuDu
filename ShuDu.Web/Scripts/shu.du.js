function get_null_array(length) {
    var array = [];
    for (var i = 0; i < length; i++) {
        array.push(0);
    }
    return array;
}
function set_box(array,box, data) {
    for (var i = 0; i < 81; i++) {
        if ((parseInt(parseInt(i / 9) / 3) * 3 + parseInt(i % 9 / 3)) == box && array[i] == 0) {
            array[i] = data;
        }
    }
    return array;
}
function set_row(array, row, data) {
    for (var i = 0; i < 81; i++) {
        if (parseInt(i / 9) == row && array[i] == 0) {
            array[i] = data;
        }
    }
    return array;
}
function set_column(array, column, data) {
    for (var i = 0; i < 81; i++) {
        if (parseInt(i % 9) == column && array[i] == 0) {
            array[i] = data;
        }
    }
    return array;
}
//唯一值计算
function shu_du_only_one() {
    var oldArray = get_text(); //原始数据
    //唯一值计算
    while (true) {
        var isBreak = true;
        for (var i = 1; i <= 9; i++) {
            var rows = get_null_array(9);
            var columns = get_null_array(9);
            var boxs = get_null_array(9);
            for (var j = 0; j < 81; j++) {
                if (oldArray[j] > 0) {
                    continue;
                }
                oldArray[j] = i;
                if (check_data(oldArray, j)) {
                    oldArray[j] = 0;
                    //同列
                    var column = j % 9;
                    columns[column] = columns[column] + 1;
                    //同行
                    var row = parseInt(j / 9);
                    rows[row] = rows[row] + 1;
                    //同块
                    var box = parseInt(parseInt(j / 9) / 3) * 3 + parseInt(j % 9 / 3);
                    boxs[box] = boxs[box] + 1;
                }
                else {
                    oldArray[j] = -i;
                }
            }
            for (var j = 0; j < 9; j++) {
                if (rows[j] == 1) {
                    oldArray = set_row(oldArray, j, i);
                    isBreak = false;
                    show_value(oldArray);
                }
                if (columns[j] == 1) {
                    oldArray = set_column(oldArray, j, i);
                    isBreak = false;
                    show_value(oldArray);
                }
                if (boxs[j] == 1) {
                    oldArray = set_box(oldArray, j, i);
                    isBreak = false;
                    show_value(oldArray);
                }
            }
            for (var j = 0; j < 81; j++) {
                if (oldArray[j] < 0) {
                    oldArray[j] = 0;
                }
            }
        }
        if (isBreak) {
            break;
        }
    }
    for (var i = 0; i < 81; i++) {
        if (oldArray[i] > 0 && $('#s' + (i + 1)).val() == '') {
            $('#s' + (i + 1)).val(oldArray[i]);
        }
    }
}
function shu_du_only_one2() {
    var oldArray = get_text(); //原始数据
    var ss = 9;
    for (var i = 0; i < 81; i++) {
        if (oldArray[i] != 0) {
            continue;
        }
        oldArray[i] = ss;
        if (check_data(oldArray, i)) {

        }
        else {
            oldArray[i] = -10;
        }
    }
    for (var i = 0; i < 81; i++) {
        if ($('#s' + (i + 1)).val() == '') {
            $('#s' + (i + 1)).val(oldArray[i]);
        }
    }
}
//回溯推算
function shu_du_backtracking() {
    var oldArray = get_text(); //原始数据
    //回溯推算
    var valueArray = [];
    for (var i = 0; i < oldArray.length; i++) {
        valueArray[i] = oldArray[i];
    }
    var isBack = false;
    for (var i = 0; i < oldArray.length; i++) {
        if (oldArray[i] > 0) {
            if (isBack) {
                i = i - 2;
            }
            continue;
        }
        isBack = false;
        do {
            valueArray[i] = valueArray[i] + 1;
        } while (valueArray[i] <= 9 && !check_data(valueArray, i));
        if (valueArray[i] > 9) {
            valueArray[i] = 0;
            i = i - 2;
            isBack = true;
        }
    }
    console.info(valueArray);
    for (var i = 0; i < 81; i++) {
        if (valueArray[i] > 0 && $('#s' + (i + 1)).val() == '') {
            $('#s' + (i + 1)).val(valueArray[i]);
        }
    }
}
function shu_du_auto() {
    var oldArray = get_text(); //原始数据
    //唯一值计算
    while (true) {
        var isBreak = true;
        for (var i = 1; i <= 9; i++) {
            var rows = get_null_array(9);
            var columns = get_null_array(9);
            var boxs = get_null_array(9);
            for (var j = 0; j < 81; j++) {
                if (oldArray[j] > 0) {
                    continue;
                }
                oldArray[j] = i;
                if (check_data(oldArray, j)) {
                    oldArray[j] = 0;
                    //同列
                    var column = j % 9;
                    columns[column] = columns[column] + 1;
                    //同行
                    var row = parseInt(j / 9);
                    rows[row] = rows[row] + 1;
                    //同块
                    var box = parseInt(parseInt(j / 9) / 3) * 3 + parseInt(j % 9 / 3);
                    boxs[box] = boxs[box] + 1;
                }
                else {
                    oldArray[j] = -i;
                }
            }
            for (var j = 0; j < 9; j++) {
                if (rows[j] == 1) {
                    oldArray = set_row(oldArray, j, i);
                    isBreak = false;
                }
                if (columns[j] == 1) {
                    oldArray = set_column(oldArray, j, i);
                    isBreak = false;
                }
                if (boxs[j] == 1) {
                    oldArray = set_box(oldArray, j, i);
                    isBreak = false;
                }
            }
            for (var j = 0; j < 81; j++) {
                if (oldArray[j] < 0) {
                    oldArray[j] = 0;
                }
            }
        }
        if (isBreak) {
            break;
        }
    }

    
    //回溯推算
    var valueArray = [];
    for (var i = 0; i < oldArray.length; i++) {
        valueArray[i] = oldArray[i];
    }
    var isBack = false;
    for (var i = 0; i < oldArray.length; i++) {
        if (oldArray[i] > 0) {
            if (isBack) {
                i = i - 2;
            }
            continue;
        }
        isBack = false;
        do {
            valueArray[i] = valueArray[i] + 1;
        } while (valueArray[i] <= 9 && !check_data(valueArray, i));
        if (valueArray[i] > 9) {
            valueArray[i] = 0;
            i = i - 2;
            isBack = true;
        }
    }
    console.info(valueArray);
    for (var i = 0; i < 81; i++) {
        if (valueArray[i]>0&&$('#s' + (i + 1)).val() == '') {
            $('#s' + (i + 1)).val(valueArray[i]);
        }
    }
}
function get_text() {
    var result = [];
    for (var i = 1; i <= 81; i++) {
        var temp = $('#s' + i);
        var s = parseInt(temp.val());
        if (isNaN(s)) {
            result.push(0);
        }
        else {
            result.push(s);
        }
    }
    return result;
}
function check_data(valueArray, index) {
    for (var i = 0; i < 81; i++) {
        if (i == index) {
            continue;
        }
        //同列
        if (i % 9 == index % 9 && valueArray[i] == valueArray[index]) {
            return false;
        }
        //同行
        if (parseInt(i / 9) == parseInt(index / 9) && valueArray[i] == valueArray[index]) {
            return false;
        }
        //同块
        if ((parseInt(parseInt(i / 9) / 3) * 3 + parseInt(i % 9 / 3)) == (parseInt(parseInt(index / 9) / 3) * 3 + parseInt(index % 9 / 3)) && valueArray[i] == valueArray[index]) {
            return false;
        }
    }
    return true;
}
function show_value(data) {
    for (var i = 0; i < data.length; i++) {
        if ($('#s' + (i + 1)).val() == '') {
            if (data[i] <= 0)
                continue;
            $('#s' + (i + 1)).val(data[i]);
        }
    }
}
