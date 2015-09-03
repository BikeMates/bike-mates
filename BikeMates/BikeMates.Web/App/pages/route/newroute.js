﻿define(["knockout", "jquery", "text!./newroute.html", "require", "googlemap"], function (ko, $, RouteTemplate, require, googlemap) {

    var localPath = "/Content/Site.css";
    var pathFromApp = require.toUrl(localPath);

    cssLoader.link(pathFromApp);

    localPath = "/Content/Route.css";
    pathFromApp = require.toUrl(localPath);
    cssLoader.link(pathFromApp);

    var map, service, renderer;
    var data = {};
    var start, end;

    var initialLocation, browserSupportFlag;
    var ALLOW_EDIT;
    var kiev;


    function AddRouteViewModel(params) {
        var self = this;

        self.initialize = function(allowEdit) {
            ALLOW_EDIT = allowEdit;
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
                    suppressMarkers: true
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
        self.handleNoGeolocation = function (errorFlag) {
            if (errorFlag == true) {
                alert("Geolocation service failed.");
                initialLocation = kiev;
            } else {
                alert("Your browser doesn't support geolocation. We've placed you in Siberia.");
                initialLocation = kiev;
            }
            map.setCenter(initialLocation);
        }
        self.saveRoute = function () {
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
                url: 'http://localhost:51952/api/route/put',
                data: $('#routeForm').serialize(),
                success: function (response) { }
            });
            return false;
        }
        self.getRoute= function(id) {
            $.ajax({
                type: 'GET',
                url: 'http://localhost:51952/api/route/getmapdata/' + id,
                response: JSON,
                success: function (response) {
                    loadRoute(response);
                }
            });
        }
        self.loadRoute = function(route) {
            var waypoints = [];
            for (var i = 0; i < route.waypoints.length; i++) {
                waypoints[i] = {
                    location: route.waypoints[i].latitude.toString() + ',' + route.waypoints[i].longitude.toString(),
                    stopover: false
                };
            }
            var origin = new google.maps.LatLng(route.start.latitude, route.start.longitude);
            var destination = new google.maps.LatLng(route.end.latitude, route.end.longitude);
            displayRoute(origin, destination, service, renderer, waypoints);
        }
        self.displayRoute = function(origin, destination, service, display) {
            displayRoute(origin, destination, service, display, []);
        }
        self.displayRoute = function (origin, destination, service, display, waypoints) {
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
        self.computeTotalDistance = function (result) {
            var total = 0;
            var myroute = result.routes[0];
            for (var i = 0; i < myroute.legs.length; i++) {
                total += myroute.legs[i].distance.value;
            }
            total = total / 1000;
            $('#Distance').val(total);
            return;
        }
        self.placeMarker = function (location) {
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
        self.clearMap = function() {
            if (renderer != null) {
                renderer.setMap(null);
                renderer = null;
            }

            if (ALLOW_EDIT || ALLOW_EDIT == null) {
                renderer = new google.maps.DirectionsRenderer({
                    draggable: true
                });
            } else {
                renderer = new google.maps.DirectionsRenderer({
                    draggable: false,
                    suppressMarkers: true
                });
            }
            renderer.setMap(map);

            start = null;
            end = null;
            data = {};
        }
        self.Start = ko.observable(new Date()),
        self.Distance = ko.observable("");
        self.Title = ko.observable("");
        self.Description = ko.observable("");
        self.MeetingPlace = ko.observable("");
        self.MapData = ko.observable("");
        self.totalDistance = ko.observable("");
        self.save = function() {
            saveRoute();
        }
    }
    return { viewModel: AddRouteViewModel(), template: RouteTemplate };
});
