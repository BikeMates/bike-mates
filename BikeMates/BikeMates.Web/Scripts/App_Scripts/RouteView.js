﻿$(document).ready(function () {

    function AppViewModel() {

        var self = this;
        self.title = ko.observable("");
        self.start = ko.observable("");
        self.distance = ko.observable("");
        self.MeetingPlace = ko.observable("");


        $.ajax({
            url: "http://localhost:51952/api/routes",
            contentType: "application/json",
            type: "GET",

            success: function (data) {

                self.title(data.title);
                self.start(data.Start);
                self.distance(data.Distance);
                self.MeetingPlace(data.meetingPlace);

            },
            error: function (data) {

            }
        });
    }

    ko.applyBindings(new AppViewModel());

});

