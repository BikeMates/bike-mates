﻿define(["knockout", "text!./home.html", "require"], function (ko, homeTemplate, require) {
    //TODO: Remove this page if it is not used

    function HomeViewModel() {

        var self = this;
        self.author = ko.observable();
        self.isBanned = ko.observable();
        self.mapData = ko.observable();
        self.meetingPlace = ko.observable("");
        self.subscribers = ko.observable();
        self.id = ko.observable();
        self.title = ko.observable("");
        self.start = ko.observable("");
        self.distance = ko.observable("");
        self.Participants = ko.observable();
        self.Location = ko.observable("");
        self.DateFrom = ko.observable("");
        self.DateTo = ko.observable("");
        self.MinDistance = ko.observable("");
        self.MaxDistance = ko.observable("");
        self.description = ko.observable("");
        self.allRoutes = ko.observableArray([]);
        self.OrderByFieldName = ko.observable("");

        self.goToRoute = function (data) {
            $.ajax({
                url: "http://localhost:51952/api/route",
                contentType: "application/json",
                type: "GET",
                success: function (data) {
                    //self.Start(data.Start)
                    //self.End(data.End)
                    self.title(data.title);
                    self.start(data.start);
                    self.MeetingPlace(data.meetingPlace);
                   self.distance(data.distance);
                },
                error: function (data) {
                }
            });
        };

        self.setOrderAndSearch = function (orderBy) {
            if (orderBy) {
                self.OrderByFieldName(orderBy);
            }
            self.searchRoutes();
        }

        self.searchRoutes = function () {
            $.ajax({
                url: "http://localhost:51952/api/route/getroutes",
                contentType: "application/json",
                type: "POST",
                dataType: 'json',
                data: ko.toJSON(self),
                success: function (data) {
                    $.each(data, function (key, val) {
                  self.allRoutes.push(new route(val.author,val.description, val.distance, val.id, val.isBanned, val.mapData, val.meetingPlace, val.start, val.subscribers, val.title));
                    });
                }
            });
        }
        self.searchRoutes();
        return self;
    }
    function route(author, description, distance, id, isBanned, mapData, meetingPlace, start, subscribers, title) {
        var self = this;
        self.author = author;
        self.description = description;
        self.distance = distance;
        self.id = id;
        self.isBanned = isBanned;
        self.mapData = mapData;
        self.meetingPlace = meetingPlace;
        self.start = start;
        self.subscribers = subscribers;
        self.title = title;
    }
    
    return { viewModel: HomeViewModel, template: homeTemplate };
});