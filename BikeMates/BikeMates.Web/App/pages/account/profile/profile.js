define(["knockout", "text!./profile.html", "require", "cssLoader"], function (ko, profileTemplate, require, cssLoader) {

    var tokenKey = "tokenInfo";
    var localPath = "/Content/Site.css";
    var pathFromApp = require.toUrl(localPath);
    cssLoader.link(pathFromApp);

    function ProfileViewModel(params) {

        var self = this;
        self.FirstName = ko.observable("");
        self.SecondName = ko.observable("");
        self.About = ko.observable("");


        self.fullName = ko.computed(function () {
            return self.FirstName() + " " + self.SecondName();
        }, this);

        self.gotoedit = function () {
            window.location = "http://localhost:51949/index.html#editprofile";
        }

        {
            $.ajax({
                url: "http://localhost:51952/api/profile",
                contentType: "application/json",
                type: "GET",
                headers: { "Authorization": "Bearer " + sessionStorage.getItem(tokenKey) },
                success: function (data) {

                    self.FirstName(data.firstName);
                    self.SecondName(data.secondName);
                    self.About(data.about);
                    var image_url = "http://localhost:51952/api/profilepicture/";
                    var userId = data.id;
                    $("#avatar").attr("src", image_url + userId);
                    $('#avatar').attr('src', $('#avatar').attr('src') + '?' + Math.random());

                },
                error: function (data) {

                }
            });
        }
        return self;
    }

    return { viewModel: ProfileViewModel, template: profileTemplate };
});

