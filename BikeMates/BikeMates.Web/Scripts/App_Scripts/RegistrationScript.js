$(document).ready(function () {
    $("#Register").click(function () {
        $.ajax({
            url: 'http://localhost:51952/api/account/register',
            type: 'POST',
            contentType: 'application/x-www-form-urlencoded',
            data: { Email: $('#EmailReg').val(), Password: $('#PasswordReg').val(), ConfirmPassword: $('#ConfirmPassword').val(), FirstName: $('#FirstName').val() },
            success: function (data) {
                alert('Registration successful');
            },
            error: function (data) {
                alert('Registration failed');
            }
        });
    });
});