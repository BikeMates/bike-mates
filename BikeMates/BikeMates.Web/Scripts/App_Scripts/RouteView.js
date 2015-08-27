$(document).ready(function () {

    function AppViewModel() {

        var self = this;
        self.title = ko.observable("");
        self.start = ko.observable("");
        self.distance = ko.observable("");
        self.meeting_place = ko.observable("");


        $.ajax({
            url: "http://localhost:51952/api/route",
            contentType: "application/json",
            type: "GET",
            success: function (data) {

                self.title(data.title);
                self.start(data.start);
                self.distance(data.distance);
                self.meeting_place(data.meeting_place);

            },
            error: function (data) {

            }
        });
    }
    ko.applyBindings(new AppViewModel());

});

