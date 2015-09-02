define(["knockout", "text!./home.html", "require", "cssLoader"], function (ko, homeTemplate, require, cssLoader) {
    //TODO: Remove this page if it is not used
    var localPath = "/Content/Site.css";
    var pathFromApp = require.toUrl(localPath);

    cssLoader.link(pathFromApp);

    function HomeViewModel() {

        var self = this;
        self.author = ko.observable();
        self.isBanned = ko.observable();
        self.mapData = ko.observable();
        self.meetingPlace = ko.observable("");
        self.subscribers = ko.observable();
        self.id = ko.observable();
        self.title = ko.observable("");
        self.start = ko.observable("");
        self.distance = ko.observable("");
        self.Participants = ko.observable();
        self.Location = ko.observable("");
        self.DateFrom = ko.observable("");
        self.DateTo = ko.observable("");
        self.MinDistance = ko.observable("");
        self.MaxDistance = ko.observable("");
        self.description = ko.observable("");
        self.allRoutes = ko.observableArray([]);

        self.goToRoute = function (data) {
            //$.ajax({
            //    url: "http://localhost:51952/api/route",
            //    contentType: "application/json",
            //    type: "GET",
            //    success: function (data) {
            //        //self.Start(data.Start)
            //        //self.End(data.End)
            //        self.title(data.title);
            //        self.start(data.start);
            //        self.distance(data.distance);
            //        self.MeetingPlace(data.meetingPlace);
            //    },
            //    error: function (data) {
            //    }
            //});
        };

        $("#Search").button().click(function () {
            $.ajax({
                url: "http://localhost:51952/api/route/search",
                contentType: "application/json",
                type: "POST",
                dataType: 'json',
                data: ko.toJSON(self),
                success: function (data) {
                    self.allRoutes(data.routes);
                    console.log(data);
                },
                error: function (data) {
                }
            });
        });
        $("#ByDate").button().click(function () {
            $.ajax({
                url: "http://localhost:51952/api/route/sortdate",
                contentType: "application/json",
                type: "GET",
                success: function (data) {
                    self.allRoutes(data.routeSort);
                    console.log("ByDate");
                },
                error: function (data) {
                }
            });
        });
        $("#ByParticipants").button().click(function () {
            $.ajax({
                url: "http://localhost:51952/api/route/sortsubscribes",
                contentType: "application/json",
                type: "GET",
                success: function (data) {
                    self.allRoutes(data.routeSort);
                },
                error: function (data) {
                }
            });
        });
        $("#ByTitle").button().click(function () {
            $.ajax({
                url: "http://localhost:51952/api/route/sorttitle",
                contentType: "application/json",
                type: "GET",
                success: function (data) {
                    self.allRoutes(data.routeSort);
                },
                error: function (data) {
                }
            });
        });

        $.ajax({
            url: "http://localhost:51952/api/route/getroutes",
            contentType: "application/json",
            type: "GET",
            success: function (data) {

                self.allRoutes(data.routes);
                console.log("succes");
            },
            error: function (data) {
            }
        });
    }
    function route(author, description, distance, id, isBanned, mapData, meetingPlace, start, subscribers, title) {
        var self = this;
        self.author = author;
        self.description = description;
        self.distance = distance;
        self.id = id;
        self.isBanned = isBanned;
        self.mapData = mapData;
        self.meetingPlace = meetingPlace;
        self.start = start;
        self.subscribers = subscribers;
        self.title = title;
    }
    return { viewModel: HomeViewModel, template: homeTemplate };
});