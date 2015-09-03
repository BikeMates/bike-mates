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

        Share = {
            vkontakte: function (purl, ptitle, pimg, text) {
                url = 'http://vkontakte.ru/share.php?';
                url += 'url=' + encodeURIComponent(purl);
                url += '&title=' + encodeURIComponent(ptitle);
                url += '&description=' + encodeURIComponent(text);
                url += '&image=' + encodeURIComponent(pimg);
                url += '&noparse=true';
                Share.popup(url);
            },
            facebook: function (purl, ptitle, pimg, text) {
                url = 'http://www.facebook.com/sharer.php?s=100';
                url += '&p[title]=' + encodeURIComponent(ptitle);
                url += '&p[summary]=' + encodeURIComponent(text);
                url += '&p[url]=' + encodeURIComponent(purl);
                url += '&p[images][0]=' + encodeURIComponent(pimg);
                Share.popup(url);
            },
            google: function (purl, ptitle, pimg, text) {
                url = 'https://plus.google.com/share?url=<?php';
                url += 'url=' + encodeURIComponent(purl);
                url += '&title=' + encodeURIComponent(ptitle);
                url += '&description=' + encodeURIComponent(text);
                url += '&image=' + encodeURIComponent(pimg);
                url += '&noparse=true';
                Share.popup(url);
            },
            popup: function (url) {
                window.open(url, '', 'toolbar=0,status=0,width=626,height=436');
            }
        };
        $.ajax({
            url: "http://localhost:51952/api/route/get"+'/'+2,
            contentType: "application/json",
            type: "GET",
            success: function (data) {
                //self.Start(data.Start)
                //self.End(data.End)
                self.title(data.title);
                self.start(data.start);
                self.distance(data.distance);
                self.MeetingPlace(data.meetingPlace);

                self.description(data.description);

            },
            error: function (data) {
            }
        });



        return self;
    }
    return { viewModel: RoutevViewModel, template: routeviewTemplate };
});

