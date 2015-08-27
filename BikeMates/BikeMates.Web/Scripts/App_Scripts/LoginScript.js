$(document).ready(function () {

    var tokenKey = "tokenInfo";

    function LoginViewModel() {
        this.username = ko.observable("");
        this.password = ko.observable("");
        this.login = function()
        {
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
                    window.location.href = "/Home/Index";
                },
                error: function (data) {
                    var response = JSON.parse(data.responseText);

                    $("#error_details").text(response.error_description)
                    $("#error_message").show();
                }
            });
        }
    }

    ko.applyBindings(new LoginViewModel());
  });

