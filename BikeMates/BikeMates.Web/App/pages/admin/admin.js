define(["knockout", "text!./admin.html", "require"], function (ko, adminTemplate, require) {

    var tokenKey = "tokenInfo";

    function AdminViewModel(params) {
        var self = this;
        self.id = ko.observable("");
        self.firstName = ko.observable("");
        self.secondName = ko.observable("");
        self.users = ko.observableArray([]);

        self.unban = function () {

            var selected = new Array();
            $('input:checked').each(function () {
                selected.push($(this).attr('value'));
            });

            $.ajax({
                url: "http://localhost:51952/api/admin/unbanusers",
                contentType: "application/json",
                type: "POST",
                headers: { "Authorization": "Bearer " + sessionStorage.getItem(tokenKey) },
                data: JSON.stringify(selected),
                success: function (data) {
                    $.each(selected, function (key, val) {
                        self.users.remove(function (user) { return user.id = val });
                    });
                    self.loadUsers();
                }
            });

        }

        self.loadUsers = function () {
            $.ajax({
                url: "http://localhost:51952/api/admin/getbannedusers",
                type: 'GET',
                headers: { "Authorization": "Bearer " + sessionStorage.getItem(tokenKey) },
                contentType: 'application/x-www-form-urlencoded',

                success: function (data) {
                    $.each(data, function (key, val) {
                        self.users.push(new user(val.id, val.firstName, val.secondName));
                    });
                }
            });
        }
        self.loadUsers();
        return self;
    }

    function user(id, firsName, secondName) {
        var self = this;
        self.id = id;
        self.firstName = firsName;
        self.secondName = secondName;
    }
    return { viewModel: AdminViewModel, template: adminTemplate };
});