define(["knockout", "text!./newroute.html", "require", "cssLoader", "RouteMap", "googlemap"], function (ko, RouteTemplate, require, cssLoader, RouteMap, googlemap) {

    var localPath = "/Content/Site.css";
    var pathFromApp = require.toUrl(localPath);

    cssLoader.link(pathFromApp);

    localPath = "/Content/Route.css";
    pathFromApp = require.toUrl(localPath);
    cssLoader.link(pathFromApp);

    ko.bindingHandlers.koDatepicker = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            var options = {
                showOtherMonths: true,
                selectOtherMonths: true
            };

            if (typeof valueAccessor() === 'object') {
                $.extend(options, valueAccessor());
            }

            $(element).datepicker(options);
        }
    };

    function AddRouteViewModel(params) {
        var self = this;

        self.Start = ko.observable(new Date(Date.today)),
        self.Distance = ko.observable("");
        self.Title = ko.observable("");
        self.Description = ko.observable("");
        self.MeetingPlace = ko.observable("");
        self.MapData = ko.observable("");

        self.add= function() {}
    }

    return { viewModel: AddRouteViewModel(), template: RouteTemplate };
});
