﻿define(["knockout", "text!./register.html", "require", "cssLoader"], function (ko, registerTemplate, require, cssLoader) {
    
    var localPath = "/Content/Site.css";
    var pathFromApp = require.toUrl(localPath);

    cssLoader.link(pathFromApp);

    var tokenKey = "tokenInfo";

    function RegisterViewModel(params) {
        var self = this;
        self.firstName = ko.observable("");
        self.secondName = ko.observable("");
        self.email = ko.observable("");
        self.password = ko.observable("");
        self.confirmPassword = ko.observable("");

        self.add = function () {
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

                    sessionStorage.setItem(tokenKey, data.access_token);
                    window.location.href = "/index.html";
                    console.log(sessionStorage.getItem(tokenKey));

                },
                error: function (data) {
                    var errors = [];

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
