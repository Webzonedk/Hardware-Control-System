
var check1 = document.getElementById("checkSerial");
check1.addEventListener("change", function () {
    if (!check1.checked) {
        document.getElementById("serialId").setAttribute("readonly", "readonly");
        document.getElementById("serialId").setAttribute("unselectable", "on");;
    }
    else {
        document.getElementById("serialId").removeAttribute("readonly");
    }
})

var check2 = document.getElementById("checkType");
check2.addEventListener("change", function () {
    if (!check2.checked) {
        document.getElementById("typeId").setAttribute("readonly", "readonly");
    }
    else {
        document.getElementById("typeId").removeAttribute("readonly");
    }
})

var check3 = document.getElementById("checkDevice");
check3.addEventListener("change", function () {
    if (!check3.checked) {
        document.getElementById("deviceId").setAttribute("readonly", "readonly");
    }
    else {
        document.getElementById("deviceId").removeAttribute("readonly");
    }
})

var check4 = document.getElementById("checkAccessories");
check4.addEventListener("change", function () {
    if (!check4.checked) {
        document.getElementById("accesoriesId").setAttribute("readonly", "readonly");
    }
    else {
        document.getElementById("accesoriesId").removeAttribute("readonly");
    }
})
