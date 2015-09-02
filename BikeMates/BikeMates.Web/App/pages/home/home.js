define(["knockout", "text!./home.html", "require", "cssLoader"], function (ko, homeTemplate, require, cssLoader) {
    //TODO: Remove this page if it is not used
    var localPath = "/Content/Site.css";
    var pathFromApp = require.toUrl(localPath);

    cssLoader.link(pathFromApp);

    function HomeViewModel() {

        var self = this;
        self.id = ko.observable();
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
        self.description = ko.observable("");
        self.Date = ko.observable("Date");
        self.Title = ko.observable("Title");
        self.Subscribe = ko.observable("Subscribers");
        self.allRoutes = ko.observableArray([new route(1, "Test", "recfrevrtvr", "Patona", 20),
            new route(2, "Test2", "23re3etvr", "1Patona", 10),
            new route(3, "Test3", "rfr45ggr", "Fatona", 40)
        ]);

        $.ajax({
            url: "http://localhost:51952/api/route",
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
                url: "http://localhost:51952/api/route/search",
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
                url: "http://localhost:51952/api/route/sortdate",
                contentType: "application/json",
                type: "GET",
                success: function (data) {
                    self.allRoutes(data.routesSort);
                    console.log("ByDate");
                },
                error: function (data) {
                }
            });
        });
        $("#ByParticipants").button().click(function () {
            $.ajax({
                url: "http://localhost:51952/api/route/sortsubscribes" ,
                contentType: "application/json",
                type: "GET",
                success: function (data) {
                    self.allRoutes(data.routesSort);
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
                    self.allRoutes(data.routesSort);
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
                self.allRoutes(data.allRoutes);
                console.log("succes");
            },
            error: function (data) {
            }
        });
    }
    function route(id, title,description,meetingplace,distance) {
      
        
        self.description=description;
        self.MeetingPlace =meetingplace;
        self.distance =distance;
        self.id = id;
        self.title = title;
    }
    return { viewModel: HomeViewModel, template: homeTemplate };
});