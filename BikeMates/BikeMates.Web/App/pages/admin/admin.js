define(["knockout", "text!./admin.html", "require", "cssLoader"], function (ko, adminTemplate, require, cssLoader) {

    var localPath = "/Content/Site.css";
    var pathFromApp = require.toUrl(localPath);
    var tokenKey = "tokenInfo";

    cssLoader.link(pathFromApp);

    function AdminViewModel(params) {
        var self = this;
        self.id = ko.observable("");
        self.firstName = ko.observable("");
        self.secondName = ko.observable("");
        self.users = ko.observableArray([
            new user("1", "Vasyl", "Korzhyk"),
            new user("2", "Vasyl", "Korzhyk"),
            new user("3", "Vasyl", "Korzhyk")
        ]);

        self.loadUsers = function () {
            $.ajax({
                url: "http://localhost:51952/api/admin/getbanedusers",
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