CE.RegisterNamespace('Helpers');

CE.Helpers.ObjectArrayIndexOf = function (arr, value, properties) {
    if (typeof properties == "string")
        properties = [properties];

    for (var i = 0, len = arr.length; i < len; i++) {
        var currentItem = arr[i];
        properties.forEach(function (property) {
            if (currentItem != null)
                currentItem = currentItem[property];
            if (currentItem == null)
                return false;
        });
        if (currentItem === value)
            return i;
    }
    return -1;
}

CE.Helpers.CloneObject = function (objectA, cloneChilds) {
    cloneChilds = cloneChilds == false ? false : true;
    var result = {};
    for (var att in objectA) {
        var item = objectA[att];
        if (item != null && cloneChilds && Array.isArray(item))
            result[att] = CE.Helpers.CloneArray(item)
        else if (item != null && cloneChilds && typeof item == "object")
            result[att] = CE.Helpers.CloneObject(item)
        else
            result[att] = item;
    }
    return result;
}

CE.Helpers.CloneArray = function (arrayA, cloneChilds) {
    cloneChilds = cloneChilds == false ? false : true;
    var result = [];
    arrayA.forEach(function (item, itemIndex) {
        if (item != null && cloneChilds && Array.isArray(item))
            result[itemIndex] = CE.Helpers.CloneArray(item)
        else if (item != null && cloneChilds && typeof item == "object")
            result[itemIndex] = CE.Helpers.CloneObject(item)
        else
            result[itemIndex] = item;
    });
    return result;
}

CE.Helpers.MergeObject = function (objectA, objectB) {
    var result = CE.Helpers.CloneObject(objectA);
    for (var att in objectB) {
        if (objectA[att] != null && typeof objectA[att] == 'object' && !Array.isArray(objectA[att])) {
            result[att] = CE.Helpers.MergeObject(result[att], objectB[att]);
        }
        else
            result[att] = objectB[att];
    }
    return result;
}

CE.Helpers.ReplaceAll = function (target, search, replacement) {
    if (target.indexOf(search) >= 0)
        return target.replace(new RegExp(search, 'g'), replacement);
    else
        return target;
}

Number.prototype.padLeft = function (len, chr) {
    var self = Math.abs(this) + '';
    return (this < 0 && '-' || '') +
            (String(Math.pow(10, (len || 2) - self.length))
              .slice(1).replace(/0/g, chr || '0') + self);
}

String.format = function (format) {
    var args = Array.prototype.slice.call(arguments, 1);
    return format.replace(/{(\d+)}/g, function (match, number) {
        return typeof args[number] != 'undefined' ? args[number] : match;
    });
};