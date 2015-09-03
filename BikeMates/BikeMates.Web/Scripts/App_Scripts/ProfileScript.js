$(document).ready(function () {
    var tokenKey = "tokenInfo";

    //TODO: Remove this. Do not use jQuery selectors
    $("#properties_btn").click(function () {
        window.location = "http://localhost:51949/Account/EditProfile";
    });

    function ProfileViewModel() {

        var self = this;
        self.FirstName = ko.observable("");
        self.SecondName = ko.observable("");
        self.About = ko.observable("");

        self.fullName = ko.computed(function () {
            return self.FirstName() + " " + self.SecondName();
        }, this);

        $.ajax({
            url: "http://localhost:51952/api/profile",
            contentType: "application/json",
            type: "GET",
            headers: { "Authorization": "Bearer " + sessionStorage.getItem(tokenKey) },
            success: function (data) {

                self.FirstName(data.firstName);
                self.SecondName(data.secondName);
                self.About(data.about);
                var api_link = "http://localhost:51952/api/profilepicture/";
                var usr_id = data.id;
                $("#avatar").attr("src", api_link + usr_id); //TODO: Replace jQuery with knockout

            },
            error: function (data) {

            }
        });
    }
    // Activates knockout.js - bind view model to referring view 
    ko.applyBindings(new ProfileViewModel());
});

