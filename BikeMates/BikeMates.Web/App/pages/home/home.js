define(["knockout", "text!./home.html", "require", "cssLoader"], function (ko, homeTemplate, require, cssLoader) {
    //TODO: Remove this page if it is not used
    var localPath = "/Content/Site.css";
    var pathFromApp = require.toUrl(localPath);

    cssLoader.link(pathFromApp);

    function HomeViewModel() {

        var self = this;
        self.title = ko.observable("");
        self.start = ko.observable("");
        self.distance = ko.observable("");
        self.MeetingPlace = ko.observable("");
        self.Participants = ko.observable();
        //self.Start = ko.observable();
        //self.End = ko.observable();
        self.Location = ko.observable("");
        self.DateFrom = ko.observable("");
        self.DateTo = ko.observable("");
        self.MinDistance = ko.observable("");
        self.MaxDistance = ko.observable("");
        self.allRoutes = ko.observableArray();

        self.ByTitle = ko.observable("");
        $.ajax({
            url: "http://localhost:51952/api/route" + '/' + 2,
            contentType: "application/json",
            type: "GET",
            success: function (data) {
                //self.Start(data.Start)
                //self.End(data.End)
                self.title(data.title);
                self.start(data.start);
                self.distance(data.distance);
                self.MeetingPlace(data.meetingPlace);
            },
            error: function (data) {
            }
        });

        $("#Search").button().click(function () {
            $.ajax({
                url: "http://localhost:51952/api/route",
                contentType: "application/json",
                type: "POST",
                dataType: 'json',
                data: ko.toJSON(self),
                success: function (data) {
                    console.log(data);
                },
                error: function (data) {
                }
            });
        });
        $("#ByDate").button().click(function () {
            $.ajax({
                url: "http://localhost:51952/api/search" + '/' + 3,
                contentType: "application/json",
                type: "GET",
                success: function (data) {
                    self.allRoutes(data.ListSort);
                    console.log("ByDate");
                },
                error: function (data) {
                }
            });
        });
        $("#ByParticipants").button().click(function () {
            $.ajax({
                url: "http://localhost:51952/api/search" + '/' + 2,
                contentType: "application/json",
                type: "GET",
                success: function (data) {
                    self.allRoutes(data.ListSort);
                },
                error: function (data) {
                }
            });
        });
        $("#ByTitle").button().click(function () {
            $.ajax({
                url: "http://localhost:51952/api/search" + '/' + 1,
                contentType: "application/json",
                type: "GET",
                success: function (data) {
                    self.allRoutes(data.ListSort);
                },
                error: function (data) {
                }
            });
        });

        $.ajax({
            url: "http://localhost:51952/api/search",
            contentType: "application/json",
            type: "GET",
            success: function (data) {
                self.allRoutes(data.allRoutes);
                console.log("succes");
            },
            error: function (data) {
            }
        });
    }
    function route(id, title) {
        self.id = id;
        self.title = title;
    }
    return { viewModel: HomeViewModel, template: homeTemplate };
});