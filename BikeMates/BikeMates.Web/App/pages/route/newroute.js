define(["knockout", "jquery", "text!./newroute.html", "require", "cssLoader", "RouteMap", "googlemap"], function (ko, $, RouteTemplate, require, cssLoader, RouteMap, googlemap) {

    var localPath = "/Content/Site.css";
    var pathFromApp = require.toUrl(localPath);

    cssLoader.link(pathFromApp);

    localPath = "/Content/Route.css";
    pathFromApp = require.toUrl(localPath);
    cssLoader.link(pathFromApp);

    function AddRouteViewModel(params) {
        var self = this;


        $('#map').text("dfasdfas");

        self.Start = ko.observable(new Date()),
        self.Distance = ko.observable("");
        self.Title = ko.observable("");
        self.Description = ko.observable("");
        self.MeetingPlace = ko.observable("");
        self.MapData = ko.observable("");
        self.totalDistance = ko.observable("");
        self.save = function () {
            saveRoute();
        }
        self.clearMap = function() {
            clearMap();
            return;
        }
        self.loadRoute = function () {
            loadRoute(1003);
            return;
        }
    }

    return { viewModel: AddRouteViewModel(), template: RouteTemplate };
});
