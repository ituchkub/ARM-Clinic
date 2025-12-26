const splitObjectByKeys = (obj, array) => {
  const filteredKeys = Object.keys(obj).filter((key) => !array.includes(key));
  const filteredObj = filteredKeys.reduce((acc, key) => {
    acc[key] = obj[key];
    return acc;
  }, {});
  return filteredObj;
};
const base64toBlob = (base64Data) => {
  var byteString = atob(base64Data);
  var arrayBuffer = new ArrayBuffer(byteString.length);
  var int8Array = new Uint8Array(arrayBuffer);
  for (var i = 0; i < byteString.length; i++) {
    int8Array[i] = byteString.charCodeAt(i);
  }
  return new Blob([int8Array], { type: "application/octet-stream" });
};
const onSetPagination = (rows, rowsPerPage) => {
    var rowsMap = []
    var arraySet = []
    for (let i = 0; i < rows.length; i++) {
        arraySet.push(rows[i])
        if (arraySet.length == rowsPerPage) {
            rowsMap.push(arraySet)
            arraySet = []
        }
    }
    if (arraySet.length > 0) {
        rowsMap.push(arraySet)
    }
    return {
        totalRows: rows.length,
        totalPages: rowsMap.length,
        rowsData: rowsMap
    }
}
export { splitObjectByKeys, base64toBlob, onSetPagination };
