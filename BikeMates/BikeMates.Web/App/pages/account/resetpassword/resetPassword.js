define(["knockout", "text!./resetPassword.html", "require"], function (ko, resetPasswordTemplate, require) {

    function ResetPasswordViewModel() {
        var self = this;
        self.email = ko.observable("");
        self.password = ko.observable("");
        self.confirmPassword = ko.observable("");
        self.resetPassword = function () {
            $.ajax({
                url: 'http://localhost:51952/api/account/resetpassword', //TODO: Move the Service url to the separate App settings file
                type: 'POST',
                contentType: 'application/x-www-form-urlencoded',
                data:
                    {
                        email: self.email,
                        password: self.password,
                        confirmPassword: self.confirmPassword,
                        code: location.href.split("&")[1].split("=")[1].split("#")[0]
                    },
                success: function (data) {
                    $("#error_message").hide();
                    window.location.href = "#confirmresetpassword";
                },
                error: function (data) {
                    var errors = []
                    var response = JSON.parse(data.responseText);
                    //errors.push(response.message);
                    for (key in response.modelState) {
                        for (var i = 0; i < response.modelState[key].length; i = i+2) {
                            errors.push(response.modelState[key][i]);
                        }
                    }

                    $("#error_details").text(" " + errors.join(" "));
                    $("#error_message").show();
                }
            });
        }
        return self;
    }

    return { viewModel: ResetPasswordViewModel, template: resetPasswordTemplate };
});