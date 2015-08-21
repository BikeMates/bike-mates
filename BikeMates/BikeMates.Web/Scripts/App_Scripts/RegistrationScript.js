$(document).ready(function () {

    function RegisterViewModel() {

        this.firstName = ko.observable("");
        this.secondName = ko.observable("");
        this.email = ko.observable("");
        this.password = ko.observable("");

        this.register = function () {
            $.ajax({
                url: 'http://localhost:51952/api/account/register',
                type: 'POST',
                contentType: 'application/x-www-form-urlencoded',
                data: { Email: this.email, Password: this.password, ConfirmPassword: this.password, FirstName: this.firstName, SecondName: this.secondName },
                success: function (data) {
                    alert('Registration successful');
                },
                error: function (data) {
                    alert('Registration failed');
                }
            });
        }
    }

    ko.applyBindings(new RegisterViewModel());
});