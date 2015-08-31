define(["knockout", "text!./login.html", "require", "cssLoader"], function (ko, loginTemplate, require, cssLoader) {

    var localPath = "/Content/Site.css";
    var pathFromApp = require.toUrl(localPath);

    cssLoader.link(pathFromApp);

    var tokenKey = "tokenInfo";

    function LoginViewModel(params) {
        var self = this;
        self.username = ko.observable("");
        self.password = ko.observable("");
        self.login = function () {
            $.ajax({
                url: 'http://localhost:51952/api/account/login',
                type: 'POST',
                contentType: 'application/x-www-form-urlencoded',
                data:
                    {
                        grant_type: 'password',
                        email: this.username,
                        Password: this.password
                    },
                success: function (data) {
                    $("#error_message").hide();

                    sessionStorage.setItem(tokenKey, data.access_token);
                    window.location.href = "/index.html";
                },
                error: function (data) {
                    var response = JSON.parse(data.responseText);

                    $("#error_details").text(response.error_description)
                    $("#error_message").show();
                }
            });
        }
        return self;
    }
    return { viewModel: LoginViewModel, template: loginTemplate };
});