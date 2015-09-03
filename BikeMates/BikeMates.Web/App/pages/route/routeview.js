define(["knockout", "text!./routeview.html", "require", "cssLoader", "RouteMap", "googlemap"], function (ko, routeviewTemplate, require, cssLoader, RouteMap, googlemap) {
    var tokenKey = "tokenInfo";
    var localPath = "/Content/Site.css";
    var pathFromApp = require.toUrl(localPath);

    cssLoader.link(pathFromApp);

    localPath = "/Content/Route.css";
    pathFromApp = require.toUrl(localPath);
    cssLoader.link(pathFromApp);
    function RoutevViewModel() {

        var self = this;
        self.id = ko.observable();
        self.title = ko.observable("");
        self.start = ko.observable("");
        self.distance = ko.observable("");
        self.MeetingPlace = ko.observable("");
        self.Participants = ko.observable();
        self.description = ko.observable("");
        //self.Start = ko.observable();
        //self.End = ko.observable();
        self.ByTitle = ko.observable("");
        self.subscribed = ko.observable(false);

        self.subscribe = function () {
            var apiurl = "http://localhost:51952/api/subscribe/" + 91;
            $.ajax({
                url: apiurl,
                contentType: "application/json",
                type: "PUT",
                headers: { "Authorization": "Bearer " + sessionStorage.getItem(tokenKey) },
                success: function (data) {
                    self.isSubscribed();
                },
                error: function (data) {
                }
            });
        }
        self.unsubscribe = function () {
            var apiurl = "http://localhost:51952/api/subscribe/" + 91;
            $.ajax({
                url: apiurl,
                contentType: "application/json",
                type: "DELETE",
                headers: { "Authorization": "Bearer " + sessionStorage.getItem(tokenKey) },
                success: function (data) {
                    self.isSubscribed();
                },
                error: function (data) {
                }
            });
        }
        self.isSubscribed = function ()
        {
            var apiurl = "http://localhost:51952/api/subscribe/" + 91;
            $.ajax({
                url: apiurl,
                contentType: "application/json",
                type: "GET",
                headers: { "Authorization": "Bearer " + sessionStorage.getItem(tokenKey) },
                success: function (data) {
                    self.subscribed(data);
                    if (self.subscribed())
                    {
                        $("#sub_btn").hide();
                        $("#unsub_btn").show();
                    }
                    else
                    {
                        $("#sub_btn").show();
                        $("#unsub_btn").hide();
                    }
                },
                error: function (data) {
                }
            });

        }

        $.ajax({
            url: "http://localhost:51952/api/route/get"+'/'+ 2,
            contentType: "application/json",
            type: "GET",
            success: function (data) {
                //self.Start(data.Start)
                //self.End(data.End)
                self.title(data.title);
                self.start(data.start);
                self.distance(data.distance);
                self.MeetingPlace(data.meetingPlace);
<<<<<<< HEAD
                self.description(data.description);
=======

>>>>>>> a3b73c51b54c4a79c0051961c35bddd3e4e79dc9
            },
            error: function (data) {
            }
        });



        return self;
    }
    return { viewModel: RoutevViewModel, template: routeviewTemplate };
});

