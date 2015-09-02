define(["knockout", "text!./editprofile.html", "require"], function (ko, editprofileTemplate, require) {

    var tokenKey = "tokenInfo";
    var api_link = "aaa";

    function ProfileViewModel(params) {

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

        self.loadpicture = function () {
            var data = new FormData(jQuery('#image_form')[0]);

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

        }

        self.cancelchanges = function () {
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
                    api_link = "http://localhost:51952/api/profilepicture/";
                    $("#avatar").attr("src", api_link + self.Id);
                    $('#avatar').attr('src', $('#avatar').attr('src') + '?' + Math.random());
                },
                error: function (data) {
                }
            });
        }

        self.savedata = function () {
            $.ajax({
                url: "http://localhost:51952/api/profile",
                contentType: "application/json",
                type: "POST",
                headers: { "Authorization": "Bearer " + sessionStorage.getItem(tokenKey) },
                dataType: 'json',
                data: ko.toJSON(self),
                success: function (data) {
                    window.location = "http://localhost:51949/#profile";
                },
                error: function (data) {
                }
            });

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
                self.Picture(data.picture);
                self.Id(data.id);
                usr_id = data.id;
                api_link = "http://localhost:51952/api/profilepicture/";
                $("#avatar").attr("src", api_link + usr_id);
            },
            error: function (data) {
            }
        });

        return self;
    }

    return { viewModel: ProfileViewModel, template: editprofileTemplate };

});