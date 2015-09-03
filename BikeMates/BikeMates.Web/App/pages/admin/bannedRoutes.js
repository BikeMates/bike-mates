define(["knockout", "text!./bannedRoutes.html", "require"], function (ko, bannedRoutesTemplate, require) {

    var tokenKey = "tokenInfo";

    function BannedRoutesViewModel(params) {
        var self = this;
        self.id = ko.observable("");
        self.name = ko.observable("");
        self.routes = ko.observableArray([]);

        self.unban = function () {

            var selected = new Array();
            $('input:checked').each(function () {
                selected.push($(this).attr('value'));
            });

            $.ajax({
                url: "http://localhost:51952/api/admin/unbanroutes",
                contentType: "application/json",
                type: "POST",
                headers: { "Authorization": "Bearer " + sessionStorage.getItem(tokenKey) },
                data: JSON.stringify(selected),
                success: function (data) {
                    $.each(selected, function (key, val) {
                        self.routes.remove(function (route) { return route.id = val });
                    });
                    self.loadRoutes();
                }
            });

        }

        self.loadRoutes = function () {
            $.ajax({
                url: "http://localhost:51952/api/admin/getbannedroutes",
                type: 'GET',
                headers: { "Authorization": "Bearer " + sessionStorage.getItem(tokenKey) },
                contentType: 'application/x-www-form-urlencoded',

                success: function (data) {
                    $.each(data, function (key, val) {
                        self.routes.push(new route(val.id, val.title, val.description));
                    });
                },
                statusCode: {
                401: function (response) {
                    window.location.href = "#login";
                }
            }
            });
        }
        self.loadRoutes();
        return self;
    }

    function route(id, title, description) {
        var self = this;
        self.id = id;
        self.title = title;
        self.description = description;
    }
    return { viewModel: BannedRoutesViewModel, template: bannedRoutesTemplate };
});