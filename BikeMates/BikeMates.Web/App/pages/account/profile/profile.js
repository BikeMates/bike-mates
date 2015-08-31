define(["knockout", "text!./profile.html", "require", "cssLoader"], function (ko, profileTemplate, require, cssLoader) {

    var tokenKey = "tokenInfo";
    var localPath = "/Content/Site.css";
    var pathFromApp = require.toUrl(localPath);

    cssLoader.link(pathFromApp);

    $("#properties_btn").click(function () {
        window.location = "http://localhost:51949/Account/EditProfile";
    });

    function ProfileViewModel(params) {

        var self = this;
        self.FirstName = ko.observable("");
        self.SecondName = ko.observable("");
        self.About = ko.observable("");

        self.fullName = ko.computed(function () {
            return self.FirstName() + " " + self.SecondName();
        }, this);

        self.profile = function () {
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
                    $("#avatar").attr("src", api_link + usr_id);

                },
                error: function (data) {

                }
            });
        }
        return self;
    }

    return { viewModel: ProfileViewModel, template: profileTemplate };
});

