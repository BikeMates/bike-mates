$(document).ready(function () {

    var tokenKey = "tokenInfo";

    function LoginViewModel() {
        this.username = ko.observable("");
        this.password = ko.observable("");
        this.login = function()
        {
            $.ajax({
                url: 'http://localhost:51952/token',
                type: 'POST',
                contentType: 'application/x-www-form-urlencoded',
                data:
                    {
                        grant_type: 'password',
                        username: this.username,
                        Password: this.password
                    },
                success: function (data) {
                    sessionStorage.setItem(tokenKey, data.access_token);
                    alert('Your token: ' + sessionStorage.getItem(tokenKey));
                    window.location.href = "/Home/Index";
                },
                error: function (data) {
                    alert("Error! Data:" + data);
                }
            });
        }
    }

    ko.applyBindings(new LoginViewModel());
  });

