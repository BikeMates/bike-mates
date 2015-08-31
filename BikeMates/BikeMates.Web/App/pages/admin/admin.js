define(["knockout", "text!./admin.html", "require", "cssLoader"], function (ko, adminTemplate, require, cssLoader) {

    var localPath = "/Content/Site.css";
    var pathFromApp = require.toUrl(localPath);

    cssLoader.link(pathFromApp);

    function AdminViewModel(params) {
        var self = this;
        self.id = ko.observable("");
        self.firstName = ko.observable("");
        self.fecondName = ko.observable("");
        self.users = ko.observableArray([
            new user("1", "Vasyl", "Korzhyk"),
            new user("2", "Vasyl", "Korzhyk"),
            new user("3", "Vasyl", "Korzhyk")
        ]);
        self.loadJson = function ()
        {
            $.getJSON("http://localhost:51952/api/admin/getbanedusers", function (data) {
                console.log(data[0].firstName);
                console.log(data);
                $.each(data, function (key, val) {
                    self.users.push(new user(val.id, val.firstName, val.secondName));
                    console.log(val.firsName);
                });
            });
        }
        self.loadJson();
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