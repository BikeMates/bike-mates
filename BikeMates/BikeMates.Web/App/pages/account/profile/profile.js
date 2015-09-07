define(["knockout", "text!./profile.html", "require"], function (ko, profileTemplate, require) {

    var tokenKey = "tokenInfo";

    function ProfileViewModel(params) {

        var self = this;
        var Id = location.href.split('?')[1];

        self.FirstName = ko.observable("");
        self.SecondName = ko.observable("");
        self.About = ko.observable("");
        self.Imagesrc = ko.observable("");

        self.AllowEdit = ko.observable(false);
        self.IsBanned = ko.observable(true);
        self.IsAdmin = ko.computed(function () {
            return (sessionStorage.getItem("role") == "Admin") && (!self.IsBanned());
        });

        self.ban = function () {
            $.ajax({
                url: "http://localhost:51952/api/admin/banuser",
                contentType: "application/json",
                type: "GET",
                headers: { "Authorization": "Bearer " + sessionStorage.getItem(tokenKey) },
                data: { userId: Id },
                success: function (data)
                {
                    location.href = "#";
                }
            });
        }
        self.fullName = ko.computed(function () {
            return self.FirstName() + " " + self.SecondName();
        }, this);

        self.goToEdit = function () {
            window.location = "http://localhost:51949/#editprofile";
        }

        if (Id == "my") {
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
                        self.Imagesrc(image_url + userId + '?' + Math.random());
                        self.AllowEdit(true);
                    },
                    error: function (data) {
                    }
                });
            } else {
            $.ajax({  
                url: "http://localhost:51952/api/profile",
                contentType: "application/json",
                type: "GET",
                headers: { "Authorization": "Bearer " + sessionStorage.getItem(tokenKey) },
                data: {id :Id},
                success: function (data) {
                    self.FirstName(data.firstName);
                    self.SecondName(data.secondName);
                    self.About(data.about);
                    var image_url = "http://localhost:51952/api/profilepicture/";
                    var userId = data.id;
                    self.Imagesrc(image_url + userId + '?' + Math.random());
                    self.AllowEdit(false);
                    self.IsBanned(data.isBanned);
                },
                error: function (data) {
                }
            });
            }
            return self;
        }

        return { viewModel: ProfileViewModel, template: profileTemplate };
    });

