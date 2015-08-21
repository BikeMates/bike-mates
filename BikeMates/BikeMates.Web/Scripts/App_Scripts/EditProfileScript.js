$(document).ready(function () {

    function AppViewModel() {

        var self = this;
        self.FirstName = ko.observable("");
        self.SecondName = ko.observable("");
        self.About = ko.observable("");
        self.Picture = ko.observable("");
        self.OldPass = ko.observable("");
        self.NewPass = ko.observable("");
        self.NewPass2 = ko.observable("");

        self.fullName = ko.computed(function () {
            return self.FirstName() + " " + self.SecondName();
        }, this);

     

        $.ajax({
            url: "http://localhost:51952/api/profile",
            contentType: "application/json",
            type: "GET",
            success: function (data) {

                self.FirstName(data.firstName);
                self.SecondName(data.secondName);
                self.About(data.about);
                self.Picture(data.picture);

            },
            error: function (data) {
                alert("error occured");
            }
        });
        






        $("#save_btn").button().click(function () {
            $.ajax({
                url: "http://localhost:51952/api/profile",
                contentType: "application/json",
                type: "POST",
                dataType: 'json',
                data: ko.toJSON(AppViewModel),
                success: function (data) {

                    alert("success");

                },
                error: function (data) {
                    alert("error occured");
                }
            });
     

        });













    }



   



    // Activates knockout.js

    // bind view model to referring view
    ko.applyBindings(new AppViewModel());

});

