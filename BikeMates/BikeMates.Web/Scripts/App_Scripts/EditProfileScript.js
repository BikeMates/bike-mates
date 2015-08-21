$(document).ready(function () {

    function AppViewModel() {

        var self = this;
        self.FirstName = ko.observable("");
        self.SecondName = ko.observable("");
        self.About = ko.observable("");
        self.Picture = ko.observable("");


        self.fullName = ko.computed(function () {
            return self.FirstName() + " " + self.SecondName();
        }, this);




        $.ajax({
            url: "http://localhost:51952/api/profile",
            contentType: "text/json",
            type: "GET",
            success: function (data) {

                self.FirstName(data.FirstName);
                self.SecondName(data.SecondName);
                self.About(data.About);
                self.Picture(data.Picture);

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

