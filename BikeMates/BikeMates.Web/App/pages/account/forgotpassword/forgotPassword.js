define(["knockout", "text!./forgotPassword.html", "require", "captcha"], function (ko, forgotPasswordTemplate, require, captcha) {

    function ForgotPasswordViewModel(params) {
        var self = this;
        self.username = ko.observable("");

        self.forgotPassword = function () {
            $.ajax({
                url: 'http://localhost:51952/api/account/forgotpassword',
                type: 'POST',
                contentType: 'application/x-www-form-urlencoded',
                data:
                    {
                        email: self.username,
                        host: location.href.split("/")[2],
                        response: $("#g-recaptcha-response").val()
                    },
                success: function (data) {
                    $("#error_message").hide();
                    window.location.href = "#confirmForgotPassword";
                },
                error: function (data) {
                    var response = JSON.parse(data.responseText);
                    $("#error_details").text(response.message)
                    $("#error_message").show();
                    console.log(response);
                }
            });
        }
        return self;
    }

    return { viewModel: ForgotPasswordViewModel, template: forgotPasswordTemplate };
});