define(["knockout", "text!./banedRoutes.html", "require", "cssLoader"], function (ko, banedRoutesTemplate, require, cssLoader) {

    var localPath = "/Content/Site.css";
    var pathFromApp = require.toUrl(localPath);
    var tokenKey = "tokenInfo";

    cssLoader.link(pathFromApp);

    function BanedRoutesViewModel(params) {
        var self = this;
        self.id = ko.observable("");
        self.name = ko.observable("");
        self.routes = ko.observableArray([
            new route("1", "Route"),
            new route("2", "Route"),
            new route("3", "Route")
        ]);

        self.loadRoutes = function () {
            $.ajax({
                url: "http://localhost:51952/api/admin/getbanedroutes",
                type: 'GET',
                headers: { "Authorization": "Bearer " + sessionStorage.getItem(tokenKey) },
                contentType: 'application/x-www-form-urlencoded',

                success: function (data) {
                    $.each(data, function (key, val) {
                        self.routes.push(new route(val.id, val.firstName));
                    });
                }
            });
        }
        self.loadRoutes();
        return self;
    }

    function route(id, firsName) {
        var self = this;
        self.id = id;
        self.firstName = firsName;
    }
    return { viewModel: BanedRoutesViewModel, template: banedRoutesTemplate };
});