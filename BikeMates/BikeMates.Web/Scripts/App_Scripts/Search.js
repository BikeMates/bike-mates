$(document).ready(function () {

    function AppViewModel() {

        var self = this;
        self.Location = ko.observable("");
        self.Date1 = ko.observable("");
        self.Date2 = ko.observable("");
        self.Distance1 = ko.observable("");
        self.Distance2 = ko.observable("");

        $.ajax({
            url: "http://localhost:51952/api/home",
            contentType: "text/json",
            type: "GET",
            success: function (data) {

                self.Location(data.Location);
                self.Date1(data.Date1);
                self.Date2(data.Date2);
                self.Distance1(data.Distance1);
                self.Distance2(data.Distance2);
            },
            error: function (data) {
                alert("error occured");
            }
        });




    }
    // Activates knockout.js
    // bind view model to referring view
    ko.applyBindings(new AppViewModel());

});

