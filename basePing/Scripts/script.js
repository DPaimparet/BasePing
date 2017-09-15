var show;
var hide;
var part;

function Init() {
    show = document.getElementById("btnShow");
    hide = document.getElementById("btnHide");
    part = document.getElementById("participant");
}


function ShowParticipant() {
    show.style.display="none";
    part.style.display = "block";
    hide.style.display = "block";
    hide.className = "btn btn-success";
}

function HideParticipant() {
    show.style.display = "block";
    part.style.display = "none";
    hide.style.display = "none";
    hide.className = null;
}

window.onload = Init;
