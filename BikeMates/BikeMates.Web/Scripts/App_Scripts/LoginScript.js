$(document).ready(function () {
    $("#Login").click(function () {
        $.ajax({
            url: 'http://localhost:51952/token',
            type: 'POST',
            contentType: 'application/x-www-form-urlencoded',
            data: { grant_type: 'password', username: $('#Email').val(), Password: $('#Password').val() },
            success: function (data) {
                alert('Your token: ' + data.access_token);
            },
            error: function (data) {
                alert("Error! Data:" + data);
            }
        });
    });
});