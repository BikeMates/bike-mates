$(document).ready(function () {

    function SortViewModel() {

        var self = this;
        self.ByParticipants = ko.observable();
        self.ByDate = ko.observable();
        self.ByName = ko.observable();

      /* $.ajax({
            url: "http://localhost:51952/api/search",
            contentType: "text/json",
            type: "GET",
            success: function (data) {
                
                self.ByDate(true);
                self.ByName(true);
                self.ByParticipants(true);
            },
            error: function (data) {
                alert("error occured");
            }
        });*/


        $("#ByDate").button().click(function () {
            $.ajax({
                url: "http://localhost:51952/api/search",
                contentType: "application/json",
                type: "POST",
                dataType: 'json',
                data: ko.toJSON(self),
                success: function () {
                   
                    self.ByDate(true);
                    self.ByName(false);
                    self.ByParticipants(false);
                    alert("ByDate success");

                },
                error: function () {
                    alert("error occured");
                }
            });


        });


        $("#ByParticipants").button().click(function () {
            $.ajax({
                url: "http://localhost:51952/api/search",
                contentType: "application/json",
                type: "POST",
                dataType: 'json',
                data: ko.toJSON(self),
                success: function () {
                   
                    self.ByDate(false);
                    self.ByName(false);
                    self.ByParticipants(true);
                    alert("ByParticipants success");

                },
                error: function () {
                    alert("error occured");
                }
            });


        });
        $("#ByName").button().click(function () {
            $.ajax({
                url: "http://localhost:51952/api/search",
                contentType: "application/json",
                type: "POST",
                dataType: 'json',
                data: ko.toJSON(self),
                success: function () {
                  
                    self.ByDate(false);
                    self.ByName(true);
                    self.ByParticipants(false);
                    alert("ByName success");

                },
                error: function () {
                    alert("error occured");
                }
            });


        });
    }
        // Activates knockout.js
        // bind view model to referring view
        ko.applyBindings(new SortViewModel());

    });


