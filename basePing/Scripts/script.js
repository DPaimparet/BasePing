function init() {

    /* Register events */
    $("btnShow").onclick = ShowParticipant;
    $("btnHide").onclick = HideParticipant;

}

function ShowParticipant() {
    show("participant");
    hide("btnShow");
}

function HideParticipant() {
    hide("participant");
    show("btnShow");
    hide("btnHide");
}

window.onload = init;