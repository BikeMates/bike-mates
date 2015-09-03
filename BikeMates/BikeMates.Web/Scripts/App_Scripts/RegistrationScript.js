$(document).ready(function () { //TODO: Rewrite with RequireJS

    var tokenKey = "tokenInfo";

    function RegisterViewModel() {

        this.firstName = ko.observable("");
        this.secondName = ko.observable("");
        this.email = ko.observable("");
        this.password = ko.observable("");
        this.confirmPassword = ko.observable("");

        this.register = function () {
            $.ajax({
                url: 'http://localhost:51952/api/account/register',
                type: 'POST',
                contentType: 'application/x-www-form-urlencoded',
                data:
                    {
                        Email: this.email,
                        Password: this.password,
                        ConfirmPassword: this.confirmPassword,
                        FirstName: this.firstName,
                        SecondName: this.secondName
                    },

                success: function (data) {
                    $("#error_message").hide();
                    $(".validation-summary-errors").hide();

                    sessionStorage.setItem(tokenKey, data.access_token);
                    window.location.href = "/Home/Index";

                },
                error: function (data) {
                    var errors = [];
                    
                    $("#error_message").hide();
                    $(".validation-summary-errors").show();

                    var response = JSON.parse(data.responseText);
                    for (key in response.modelState)
                    {
                        for (var i = 0; i < response.modelState[key].length; i++)
                        {
                            errors.push(response.modelState[key][i]);
                        }
                    }

                    $("#error_details").text(" "+errors.join(" "));

                    if (errors.length > 0) {
                        $("#error_message").show();
                        $(".validation-summary-errors").hide();
                    }
                }
            });
        }
    }


    ko.applyBindings(new RegisterViewModel());
});