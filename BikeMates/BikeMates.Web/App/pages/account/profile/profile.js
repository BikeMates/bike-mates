﻿define(["knockout", "text!./profile.html", "require"], function (ko, profileTemplate, require) {

    var tokenKey = "tokenInfo";

    function ProfileViewModel(params) {

        var self = this;
        self.FirstName = ko.observable("");
        self.SecondName = ko.observable("");
        self.About = ko.observable("");
        self.Imagesrc = ko.observable("");

        self.fullName = ko.computed(function () {
            return self.FirstName() + " " + self.SecondName();
        }, this);

        self.goToEdit = function () { //TODO: rename
            window.location = "http://localhost:51949/#editprofile";
        }

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
                self.Imagesrc( image_url + userId + '?' + Math.random());
            },
            error: function (data) {
            }
        });

        return self;
    }

    return { viewModel: ProfileViewModel, template: profileTemplate };
});

