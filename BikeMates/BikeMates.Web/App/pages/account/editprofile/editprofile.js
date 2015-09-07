define(["knockout", "text!./editprofile.html", "require"], function (ko, editprofileTemplate, require) {

    var tokenKey = "tokenInfo";

    function ProfileViewModel(params) {

        var self = this;
        self.FirstName = ko.observable("");
        self.SecondName = ko.observable("");
        self.About = ko.observable("");
        self.Picture = ko.observable("");
        self.OldPassword = ko.observable("");
        self.NewPassword = ko.observable("");
        self.NewPasswordConfirmation = ko.observable("");
        self.Id = ko.observable("");
        self.Imagesrc = ko.observable("");

        self.passwordErrors = ko.observableArray();
        self.informationStatus = ko.observableArray();
        self.nameErrors = ko.observableArray();

        self.fullName = ko.computed(function () {
            return self.FirstName() + " " + self.SecondName();
        }, this);

        self.goToProfile = function () {
            location.href = "#profile";
            window.location = "http://localhost:51949/#profile";
        }

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
                contentType: 'application/JSON',
                type: "POST",
                headers: { "Authorization": "Bearer " + sessionStorage.getItem(tokenKey) },
                data: ko.toJSON(self),
                success: function (data) {
                    console.log(data);
                    self.passwordErrors(data.passwordErrors);
                    self.informationStatus(data.informationStatus);
                    self.nameErrors(data.nameErrors);
                },
                error: function (data) {
                }
            });

            if (self.passwordErrors().length == 0 && self.nameErrors().length == 0)
                {  
                    self.goToProfile();
                }
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