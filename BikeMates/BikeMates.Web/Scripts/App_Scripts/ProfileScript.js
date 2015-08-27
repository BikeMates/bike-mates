﻿$(document).ready(function () {

    var tokenKey = "tokenInfo";
  
function AppViewModel() {

    var self = this;
    self.FirstName = ko.observable("");
    self.SecondName = ko.observable("");
    self.About = ko.observable("");
    self.Picture = ko.observable("");
    

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

            },
            error: function (data) {
                alert("error occured");
                //TODO: remove alerts - if you need it so, use console.log
                //TODO: format code (spaces/tabs, remove blank lines) use - ctrl k, d
            }
        });




}
    // Activates knockout.js


    // bind view model to referring view
        ko.applyBindings(new AppViewModel());

});  

