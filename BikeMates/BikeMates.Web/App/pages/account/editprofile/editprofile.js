define(["knockout", "text!./editprofile.html", "require"], function (ko, editprofileTemplate, require) {

    var tokenKey = "tokenInfo";

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
        self.Imagesrc = ko.observable("");

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
                    var image_url = "http://localhost:51952/api/profilepicture/";
                    var userId = self.Id();
                    self.Imagesrc(image_url + userId + '?' + Math.random());

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
                    var image_url = "http://localhost:51952/api/profilepicture/";
                    var userId = self.Id();
                    self.Imagesrc(image_url + userId + '?' + Math.random());
                },
                error: function (data) {
                }
            });
        }

        self.savedata = function () {
            var username = self.fullName();
            sessionStorage.setItem("username", username);
            $('#userpanel').find('#admin').find("#user-name").text(sessionStorage.getItem("username"));
            $('#userpanel').find('#user').find("#user-name").text(sessionStorage.getItem("username"));

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
                var image_url = "http://localhost:51952/api/profilepicture/";
                var userId = self.Id();
                self.Imagesrc(image_url + userId + '?' + Math.random());
            },
            error: function (data) {
            }
        });

        return self;
    }

    return { viewModel: ProfileViewModel, template: editprofileTemplate };

});