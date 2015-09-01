﻿define(["knockout", "text!./login.html", "require"], function (ko, loginTemplate, require) {

    var tokenKey = "tokenInfo";

    function LoginViewModel(params) {
        var self = this;
        self.username = ko.observable("admin@admin.com");
        self.password = ko.observable("Qwerty1#");
        self.login = function () {

            var userlogin = self.username;

            $.ajax({
                url: 'http://localhost:51952/api/account/login',
                type: 'POST',
                contentType: 'application/x-www-form-urlencoded',
                data:
                    {
                        grant_type: 'password',
                        email: self.username,
                        Password: self.password
                    },
                success: function (data) {
                    $("#error_message").hide();

                    sessionStorage.setItem(tokenKey, data.token);
                    sessionStorage.setItem("authorized", data.isAuthorized);
                    sessionStorage.setItem("username", data.firstName + " " + data.secondName);
                    sessionStorage.setItem("role", data.role);

                    $("#authorized").show();
                    $("#anonimus").hide();
                    $('#user-name').text(sessionStorage.getItem('username'));

                    window.location.href = "#";
                },
                error: function (data) {
                    var response = JSON.parse(data.responseText);

                    $("#error_details").text(response.error_description);
                    $("#error_message").show();
                }
            });
        }
        return self;
    }
    return { viewModel: LoginViewModel, template: loginTemplate };
});