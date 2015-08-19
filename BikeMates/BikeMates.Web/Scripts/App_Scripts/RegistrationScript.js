$(document).ready(function () {
    var checker = document.getElementById('acceptCheckBox');
    var sendbtn = document.getElementById('submitButton');

    sendbtn.disabled = true;

    checker.onchange = function () {
        sendbtn.disabled = !this.checked;
    }
});