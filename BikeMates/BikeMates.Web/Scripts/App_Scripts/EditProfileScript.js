
$(document).ready(function () {

    var tokenKey = "tokenInfo";
    var api_link = "aaa";
    var usrid = "nnn";

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
                usrid = data.id;
                api_link = "http://localhost:51952/api/profilepicture/";
                $('#ava_img').html('<img src=' + api_link + data.id + ' alt="Smiley face" height="150" width="150"  />');

            },
            error: function (data) {
                alert("error occured");
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

                    alert("success");

                },
                error: function (data) {
                    alert("error occured");
                }
            });
     

        });

     
        
    }
    // Activates knockout.js

    // bind view model to referring view
    ko.applyBindings(new AppViewModel());

   
    

});







