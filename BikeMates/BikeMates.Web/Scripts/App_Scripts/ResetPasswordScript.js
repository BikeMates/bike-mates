$(document).ready(function () {

    function ResetPasswordViewModel() {
        this.email = ko.observable("");
        this.password = ko.observable("");
        this.confirmPassword = ko.observable("");
        this.resetPassword = function () {
            $.ajax({
                url: 'http://localhost:51952/api/account/resetpassword',
                type: 'POST',
                contentType: 'application/x-www-form-urlencoded',
                data:
                    {
                        email: this.email,
                        password: this.password,
                        confirmPassword: this.confirmPassword,
                        code: location.href.split("&")[1].split("=")[1]
                    },
                success: function (data) {
                    $("#error_message").hide();
                    window.location.href = "/Home/Index";
                },
                error: function (data) {
                    var response = JSON.parse(data.responseText);
                    console.log(response);
                }
            });
        }
    }

    ko.applyBindings(new ResetPasswordViewModel());
});

