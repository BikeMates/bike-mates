define(["knockout", "text!./route.html", "require", "cssLoader", "RouteMap"], function (ko, RouteTemplate, require, cssLoader, Map) {


    Map.asdf();
    var localPath = "/Content/Site.css";
    var pathFromApp = require.toUrl(localPath);

    cssLoader.link(pathFromApp);

    localPath = "/Content/Route.css";
    pathFromApp = require.toUrl(localPath);
    cssLoader.link(pathFromApp);

    function AddRouteViewModel(params) {
        var self = this;

        self.Start = ko.observable("");
        self.Distance = ko.observable("");
        self.Title = ko.observable("");
        self.Description = ko.observable("");
        self.MeetingPlace = ko.observable("");
        self.MapData = ko.observable("");
        self.add = function () {
            alert("asdf");
        }
    }
    return { viewModel: AddRouteViewModel(), template: RouteTemplate };
});
