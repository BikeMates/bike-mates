define(["knockout", "text!./routeview.html", "require", "cssLoader"], function (ko, routeviewTemplate, require) {

    function RoutevViewModel() {

        var self = this;
        self.title = ko.observable("");
        self.start = ko.observable("");
        self.distance = ko.observable("");
        self.MeetingPlace = ko.observable("");
        self.Participants = ko.observable();
        //self.Start = ko.observable();
        //self.End = ko.observable();
        self.ByTitle = ko.observable("");
        $.ajax({
            url: "http://localhost:51952/api/route/viewmodel",
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
    }
    return { viewModel: RoutevViewModel, template: routeviewTemplate };
});

