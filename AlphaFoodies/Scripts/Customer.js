var popup = document.getElementById("logout");
var icon = document.getElementById("exit-icon");
icon.onclick = function(){ 
    if (popup.style.display === 'none') {
        popup.style.display = 'block';
    } else {
        popup.style.display = 'none';
    }
};

