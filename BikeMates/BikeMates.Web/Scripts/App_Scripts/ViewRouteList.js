
    function RouteListViewModel() {
        var self = this;
        self.tites = ko.observable("fdgfdg");

        self.allRoutes = ko.observableArray();
        $.ajax({
            url: "http://localhost:51952/api/search",
            contentType: "application/json",
            type: "GET",
            success: function (data) {
                self.allRoutes(data.allRoutes);

                console.log("succes");
            },
            error: function (data) {
            }
        });

    }
    ko.applyBindings(new RouteListViewModel());
