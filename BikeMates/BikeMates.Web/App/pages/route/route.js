﻿define(["knockout", "jquery", "jquery-ui", "text!./route.html", "require", "googlemap"], function (ko, $, $$, RouteTemplate, require, googlemap) {

    var tokenKey = "tokenInfo";

    var map, service, renderer;
    var data = {};
    var start, end;
    var Id = location.href.split('?')[1];

    var initialLocation, browserSupportFlag;
    var allowEdit = false;
    var kiev;

    function RouteViewModel(params) {
        var self = this;
        self.id = ko.observable();
        self.title = ko.observable("");
        self.start = ko.observable("");
        self.distance = ko.observable("");
        self.MeetingPlace = ko.observable("");
        self.Participants = ko.observable();
        self.description = ko.observable("");
        self.subscribed = ko.observable(false);
        self.sub_show = ko.observable(true);
        self.unsub_show = ko.observable(true);
        self.Author = ko.observableArray([]);
        self.FirstName = ko.observable("");
        self.SecondName = ko.observable("");
        self.IsBanned = ko.observable(true);

        self.initialize = function (allowEdit) {
            kiev = new google.maps.LatLng(50.464484293992086, 30.522704422473907);
            var mapOptions = {
                zoom: 16,
                mapTypeId: google.maps.MapTypeId.ROADMAP,
            }

            if (navigator.geolocation) {
                browserSupportFlag = true;
                navigator.geolocation.getCurrentPosition(function (position) {
                    initialLocation = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
                    map.setCenter(initialLocation);
                }, function () {
                    handleNoGeolocation(browserSupportFlag);
                });
            }
            else {
                browserSupportFlag = false;
                handleNoGeolocation(browserSupportFlag);
            }

            map = new google.maps.Map(document.getElementById("map"), mapOptions);

            service = new google.maps.DirectionsService;
            if (allowEdit || allowEdit == null) {
                renderer = new google.maps.DirectionsRenderer({
                    draggable: true
                });
            } else {
                renderer = new google.maps.DirectionsRenderer({
                    draggable: false,
                    suppressMarkers: false
                });
            }
            renderer.setMap(map);

            renderer.addListener('directions_changed', function () {
                computeTotalDistance(renderer.getDirections());
            });

            google.maps.event.addListener(map, 'click', function (event) {
                placeMarker(event.latLng);
            });
        }
        function handleNoGeolocation(errorFlag) {
            if (errorFlag == true) {
                alert("Geolocation service failed.");
                initialLocation = kiev;
            } else {
                alert("Your browser doesn't support geolocation. We've placed you in Siberia.");
                initialLocation = kiev;
            }
            map.setCenter(initialLocation);
        }
        function saveRoute() {
            var waypoints = [], wp = [];
            var routeLeg = renderer.directions.routes[0].legs[0];
            data.start = {
                'latitude': routeLeg.start_location.lat(),
                'longitude': routeLeg.start_location.lng()
            }
            data.end = {
                'latitude': routeLeg.end_location.lat(),
                'longitude': routeLeg.end_location.lng()
            }
            wp = routeLeg.via_waypoints;

            for (var i = 0; i < wp.length; i++) {
                waypoints[i] = {
                    'latitude': wp[i].lat(),
                    'longitude': wp[i].lng()
                };
            }
            data.waypoints = waypoints;

            var stringifiedData = JSON.stringify(data);
            $('#MapData').val(stringifiedData);

            $.ajax({
                type: 'PUT',
                headers: { "Authorization": "Bearer " + sessionStorage.getItem(tokenKey) },
                url: 'http://localhost:51952/api/route/put',
                data: $('#routeForm').serialize(),
                success: function (response) { }
            });
            return false;
        }
        function getRoute(id) {
            $.ajax({
                type: 'GET',
                url: 'http://localhost:51952/api/route/find/' + id,
                response: JSON,
                success: function (response) {
                    var mapData = JSON.parse(response.mapData);

                    loadRoute(mapData);
                    $('#Start').val(response.start);
                    $('#Distance').val(response.distance);
                    $('#Title').val(response.title);
                    $('#Description').val(response.description);
                    $('#MeetingPlace').val(response.meetingPlace);
                    $('#MapData').val(response.mapData);
                }
            });
        }
        function loadRoute(route) {
            var waypoints = [];
            for (var i = 0; i < route.Waypoints.length; i++) {
                waypoints[i] = {
                    location: route.Waypoints[i].Latitude.toString() + ',' + route.Waypoints[i].Longitude.toString(),
                    stopover: false
                };
            }
            var origin = new google.maps.LatLng(route.Start.Latitude, route.Start.Longitude);
            var destination = new google.maps.LatLng(route.End.Latitude, route.End.Longitude);
            displayRoute(origin, destination, service, renderer, waypoints);
        }
        function displayRoute(origin, destination, service, display) {
            displayRoute(origin, destination, service, display, []);
        }
        function displayRoute(origin, destination, service, display, waypoints) {
            var route = {
                origin: origin,
                destination: destination,
                waypoints: waypoints,
                travelMode: google.maps.TravelMode.DRIVING,
                avoidTolls: true
            };
            service.route(route, function (response, status) {
                if (status === google.maps.DirectionsStatus.OK) {
                    display.setDirections(response);
                } else {
                    alert('Could not display directions due to: ' + status);
                }
            });
        }
        function computeTotalDistance(result) {
            var total = 0;
            var myroute = result.routes[0];
            for (var i = 0; i < myroute.legs.length; i++) {
                total += myroute.legs[i].distance.value;
            }
            total = total / 1000;
            $('#Distance').val(total);
            return;
        }
        function placeMarker(location) {
            if (start == null) {
                start = new google.maps.Marker({
                    position: location,
                    map: map
                });
                return;
            }
            if (start != null && end == null) {
                end = new google.maps.Marker({
                    position: location,
                    map: map
                });
                start.setMap(null);
                end.setMap(null);
                displayRoute(start.position, end.position, service, renderer);
            }
        }
        function getRoute() {
            $.ajax({
                type: 'GET',
                url: 'http://localhost:51952/api/route/find/' + Id,
                response: JSON,
                success: function (response) {
                    var mapData = JSON.parse(response.mapData);
                    console.log("getRoute");
                    loadRoute(mapData);
                    self.IsBanned(response.isBanned);
                    $('#MapData').val(response.mapData);
                }
            });
        }
        self.Load = function(id) {
            getRoute(id);
        }
        self.subscribe = function () {
            var apiurl = "http://localhost:51952/api/subscribe/" + Id;
            $.ajax({
                url: apiurl,
                contentType: "application/json",
                type: "PUT",
                headers: { "Authorization": "Bearer " + sessionStorage.getItem(tokenKey) },
                success: function (data) {

                    if (self.subscribed()) {
                        self.sub_show(false);
                        self.unsub_show(true);
                        self.subscribed(false);
                    }
                    else {
                        self.sub_show(true);
                        self.unsub_show(false);
                        self.subscribed(true);
                    }
                },
                error: function (data) {
                }
            });
        }
        self.unsubscribe = function () {
            var apiurl = "http://localhost:51952/api/subscribe/" + Id;
            $.ajax({
                url: apiurl,
                contentType: "application/json",
                type: "DELETE",
                headers: { "Authorization": "Bearer " + sessionStorage.getItem(tokenKey) },
                success: function (data) {
                    if (self.subscribed()) {
                        self.sub_show(false);
                        self.unsub_show(true);
                        self.subscribed(false);
                    }
                    else {
                        self.sub_show(true);
                        self.unsub_show(false);
                        self.subscribed(true);
                    }
                },
                error: function (data) {
                }
            });
        }
    
        self.IsAdmin = ko.computed(function () {
            return (sessionStorage.getItem("role") == "Admin") && (!self.IsBanned());
        });
        console.log("isA"+self.IsAdmin()+"isB"+self.IsBanned());
        self.ban = function () {
            $.ajax({
                url: "http://localhost:51952/api/admin/banroute",
                contentType: "application/json",
                type: "GET",
                headers: { "Authorization": "Bearer " + sessionStorage.getItem(tokenKey) },
                data: { routeId: Id },
                success: function (data) {
                    location.href = "#";
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
            url: "http://localhost:51952/api/route/findlogged" + '/' + Id,
            contentType: "application/json",
            type: "GET",
            headers: { "Authorization": "Bearer " + sessionStorage.getItem(tokenKey) },
            success: function (data) {
                //self.Start(data.Start)
                //self.End(data.End)
                self.title(data.title);
                self.start(data.start);
                self.distance(data.distance);
                self.MeetingPlace(data.meetingPlace);
                self.Author(data.Author);
                self.description(data.description);
                self.subscribed(data.isSubscribed);

                userRole = sessionStorage.getItem("role");
                if (userRole == 'User') {

                    if (self.subscribed()) {
                        self.sub_show(false);
                        self.unsub_show(true);
                    }
                    else {
                        self.sub_show(true);
                        self.unsub_show(false);
                    }
                }
                else {
                    self.sub_show(false);
                    self.unsub_show(false);
                }

            },
            error: function (data) {
            }
        });
        self.author = function () {
            $.ajax({
                url: "http://localhost:51952/api/route/find" + '/' + Id,
                contentType: "application/json",
                type: "GET",
                dataType: 'json',
                data: ko.toJSON(self),
                success: function (data) {
                    $.each(data, function (key, val) {

                        self.Author.push(new Author(val.FirstName,val.SecondName));
                    });
                }
            });
        }

        $.ajax({
            url: "http://localhost:51952/api/route/find" + '/' + Id,
            contentType: "application/json",
            type: "GET",
            success: function (data) {
                self.title(data.title);
                self.description(data.description);
                self.start(data.start);
                self.distance(data.distance);
                self.MeetingPlace(data.meetingPlace);
                self.Author(data.Author);
            }
        });

        return Load(Id);
    }
    function Author(FirstName,SecondName ) {
        var self = this;
        self.FirstName = FirstName;
        self.SecondName = SecondName;
    }
    return { viewModel: RouteViewModel(), template: RouteTemplate };
});
