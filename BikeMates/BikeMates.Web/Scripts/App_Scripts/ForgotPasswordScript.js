$(document).ready(function () {

    function ForgotPasswordViewModel() {
        this.username = ko.observable("");
        this.forgotPassword = function () {
            $.ajax({
                url: 'http://localhost:51952/api/account/forgotpassword',
                type: 'POST',
                contentType: 'application/x-www-form-urlencoded',
                data:
                    {
                        email: this.username,
                        host: location.href.split("/")[2],
                        response: $("#g-recaptcha-response").val()
                    },
                success: function (data) {
                    $("#error_message").hide();
                    window.location.href = "/index.html";
                },
                error: function (data) {
                    var response = JSON.parse(data.responseText);
                    $("#error_details").text(response.message)
                    $("#error_message").show();
                    console.log(response);
                }
            });
        }
    }

    ko.applyBindings(new ForgotPasswordViewModel());
});

