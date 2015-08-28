//TODO: Rewrite with RequireJS
$(document).ready(function () {

    var tokenKey = "tokenInfo";
    var api_link = "aaa";
    var usr_id = "nnn";

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
        window.location = "http://localhost:51949/Account/Profile";
    });


    function AppViewModel() {

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

        $("#save_btn").button().click(function () {
            $.ajax({
                url: "http://localhost:51952/api/profile",
                contentType: "application/json",
                type: "POST",
                headers: { "Authorization": "Bearer " + sessionStorage.getItem(tokenKey) },
                dataType: 'json',
                data: ko.toJSON(self),
                success: function (data) {
                    
                    $("#error_message").hide();
                    $(".validation-summary-errors").hide();
                  
                },

                error: function (data) {
                    //var errors = [];
                    //var response = JSON.parse(data.responseText);
                    //$("#error_details").text(response.error_description)
                    //$("#error_message").show();

                    //for (key in response.modelState) {
                    //    for (var i = 0; i < response.modelState[key].length; i++) {
                    //        errors.push(response.modelState[key][i]);
                    //    }
                    //}
                    //$("#error_details").text(" " + errors.join(" "));

                    //if (errors.length > 0) {
                    //    $("#error_message").show();
                    //    $(".validation-summary-errors").hide();
                    //}
                }
            });
        });


    }
    // Activates knockout.js - bind view model to referring view
    ko.applyBindings(new AppViewModel());
});







