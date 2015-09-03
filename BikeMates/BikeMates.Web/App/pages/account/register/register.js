define(["knockout", "text!./register.html", "require"], function (ko, registerTemplate, require) {
    
    var tokenKey = "tokenInfo";

    function RegisterViewModel(params) {
        var self = this;
        self.firstName = ko.observable("");
        self.secondName = ko.observable("");
        self.email = ko.observable("");
        self.password = ko.observable("");
        self.confirmPassword = ko.observable("");

        self.register = function () {
            $.ajax({
                url: 'http://localhost:51952/api/account/register',
                type: 'POST',
                contentType: 'application/x-www-form-urlencoded',
                data:
                    {
                        Email: self.email,
                        Password: self.password,
                        ConfirmPassword: self.confirmPassword,
                        FirstName: self.firstName,
                        SecondName: self.secondName
                    },

                success: function (data) {
                    $("#error_message").hide();
                    $(".validation-summary-errors").hide();

                    //TODO: Create a method for this
                    sessionStorage.setItem(tokenKey, data.token);
                    sessionStorage.setItem("authorized", data.isAuthorized);
                    sessionStorage.setItem("username", data.firstName + " " + data.secondName);
                    sessionStorage.setItem("role", data.role);
                    //TODO: Rewrite code below this knockout. Do not use jQuery
                    $('#userpanel').find('#anonim').hide();

                    if (data.role == "Admin") {
                        $('#userpanel').find('#admin').show()
                        $('#userpanel').find('#admin').find("#user-name").text(sessionStorage.getItem("username"));
                    } else {
                        $('#userpanel').find('#user').show();
                        $('#userpanel').find('#user').find("#user-name").text(sessionStorage.getItem("username"));
                    }

                    window.location.href = "#";
                    console.log(sessionStorage.getItem(tokenKey));

                },
                error: function (data) {
                    var errors = [];

                    //TODO: Create a common component for error handling
                    $("#error_message").hide();
                    $(".validation-summary-errors").show();

                    var response = JSON.parse(data.responseText);
                    for (key in response.modelState) {
                        for (var i = 0; i < response.modelState[key].length; i++) {
                            errors.push(response.modelState[key][i]);
                        }
                    }

                    $("#error_details").text(" " + errors.join(" "));

                    if (errors.length > 0) {
                        $("#error_message").show();
                        $(".validation-summary-errors").hide();
                    }
                }
            });
        }
        return self;
    }
    return { viewModel: RegisterViewModel, template: registerTemplate };
});
