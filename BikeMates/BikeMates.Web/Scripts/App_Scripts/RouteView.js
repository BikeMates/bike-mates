$(document).ready(function () {

    function AppViewModel() {

        var self = this;
        self.title = ko.observable("");
        self.start = ko.observable("");
        self.distance = ko.observable("");
        self.MeetingPlace = ko.observable("");
        self.Participants = ko.observable();
        //self.Start = ko.observable();
        //self.End = ko.observable();


        $.ajax({
            url: "http://localhost:51952/api/routes",
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
    ko.applyBindings(new AppViewModel());

});

