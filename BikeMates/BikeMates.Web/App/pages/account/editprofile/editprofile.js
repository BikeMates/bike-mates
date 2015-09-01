﻿define(["knockout", "text!./editprofile.html", "require", "cssLoader"], function (ko, editprofileTemplate, require, cssLoader) {

    var localPath = "/Content/Site.css";
    var pathFromApp = require.toUrl(localPath);

    cssLoader.link(pathFromApp);

    var tokenKey = "tokenInfo";
    var api_link = "aaa";
    var usr_id = "nnn";

    //TODO: Do not use JQuery functions. Move this logic to the ProfileViewModel and declare methods in it, then use Knockout to bind these methods
    $('#image_form').submit(function (e) {
        var data = new FormData(jQuery('#image_form')[0]);
        e.preventDefault();

        $.ajax({
            url: 'http://localhost:51952/api/profilepicture',
            type: 'POST',
            data: data,
            headers: { "Authorization": "Bearer " + sessionStorage.getItem(tokenKey) },
            contentType: false,
            processData: false,
            success: function (data) {
                api_link = "http://localhost:51952/api/profilepicture/";
                $('#avatar').attr('src', $('#avatar').attr('src') + '?' + Math.random());
            },
        });

    });

    $("#profile_btn").click(function () {
        $.ajax({
            url: "http://localhost:51952/api/profile",
            contentType: "application/json",
            type: "GET",
            headers: { "Authorization": "Bearer " + sessionStorage.getItem(tokenKey) },
            success: function (data) {

                self.FirstName(data.firstName);
                self.SecondName(data.secondName);
                self.About(data.about);
                self.Picture(data.picture);
                self.Id(data.id);
                usr_id = data.id;
                api_link = "http://localhost:51952/api/profilepicture/";
                $("#avatar").attr("src", api_link + usr_id);
            },
            error: function (data) {
            }
        });
    });


    $("#save_btn").button().click(function () {
        $.ajax({
            url: "http://localhost:51952/api/profile",
            contentType: "application/json",
            type: "POST",
            headers: { "Authorization": "Bearer " + sessionStorage.getItem(tokenKey) },
            dataType: 'json',
            data: ko.toJSON(self),
            success: function (data) {
                window.location = "http://localhost:51949/Account/Profile";
            },
            error: function (data) {
            }
        });
    });

    function AppViewModel(params) { //TODO: Rename to ProfileViewModel

        var self = this;
        self.FirstName = ko.observable("");
        self.SecondName = ko.observable("");
        self.About = ko.observable("");
        self.Picture = ko.observable("");
        self.OldPass = ko.observable("");
        self.NewPass = ko.observable("");
        self.NewPass2 = ko.observable("");
        self.Id = ko.observable("");

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
                    self.Picture(data.picture);
                    self.Id(data.id);
                    usr_id = data.id;
                    api_link = "http://localhost:51952/api/profilepicture/";
                    $("#avatar").attr("src", api_link + usr_id);
                },
                error: function (data) {
                }
            });
        }



        return self;
    }





    return { viewModel: AppViewModel, template: editprofileTemplate };

});