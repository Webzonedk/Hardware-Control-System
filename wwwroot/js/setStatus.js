
function setStatus() {
    var x = document.getElementById("selector");
    var minvariabel = status;
    alert(minvariabel);
    if (status != " ") {
        x.selectedIndex = 1;
    }
    else {
        x.selectedIndex = 0;
    }
   
    
}

//document.onload = setStatus();