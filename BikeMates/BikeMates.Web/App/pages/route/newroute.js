define(["knockout", "text!./newroute.html", "require", "cssLoader", "RouteMap"], function (ko, RouteTemplate, require, cssLoader, Map) {

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

        self.add= function() {}
    }
    return { viewModel: AddRouteViewModel(), template: RouteTemplate };
});
