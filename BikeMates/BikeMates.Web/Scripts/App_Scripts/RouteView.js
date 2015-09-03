$(document).ready(function () { //TODO: Rewrite with RequireJS

    function RouteViewModel() {

        var self = this;
        self.title = ko.observable("");
        self.start = ko.observable("");
        self.distance = ko.observable("");
        self.MeetingPlace = ko.observable("");
        self.Participants = ko.observable();
        //self.Start = ko.observable();
        //self.End = ko.observable();
        self.Location = ko.observable("");
        self.Date1 = ko.observable("");
        self.Date2 = ko.observable("");
        self.Distance1 = ko.observable("");
        self.Distance2 = ko.observable("");
        self.allRoutes = ko.observableArray();

        self.ByTitle=ko.observable("");
        $.ajax({
            url: "http://localhost:51952/api/routes"+'/'+2,
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
         //TODO: Use Knockout to assign methods. Do not use jQuery
        $("#Search").button().click(function () {
            $.ajax({
                url: "http://localhost:51952/api/routes",
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
                url: "http://localhost:51952/api/search"+ '/'+1,
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
            url: "http://localhost:51952/api/search/" ,
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
        ko.applyBindings(new RouteViewModel());
});

