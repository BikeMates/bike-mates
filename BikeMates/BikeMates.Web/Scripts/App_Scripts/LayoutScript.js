$(document).ready(function () {        
    $(".logout").on('click', function () {
        sessionStorage.clear();
        $('#userpanel').find('#anonim').show();
        $('#userpanel').find('#user').hide();
        $('#userpanel').find('#admin').hide();
        $('#page1').find('#userFunctions').hide();
    });
});