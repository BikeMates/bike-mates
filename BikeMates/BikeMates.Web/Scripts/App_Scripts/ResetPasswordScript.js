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
                    window.location.href = "/index.html";
                },
                error: function (data) {
                    var errors = []
                    var response = JSON.parse(data.responseText);
                    errors.push(response.message);
                    for (key in response.modelState)
                    {
                        for (var i = 0; i < response.modelState[key].length; i++)
                        {
                            errors.push(response.modelState[key][i]);
                        }
                    }

                    $("#error_details").text(" " + errors.join(" "));
                    $("#error_message").show();
                }
            });
        }
    }

    ko.applyBindings(new ResetPasswordViewModel());
});

