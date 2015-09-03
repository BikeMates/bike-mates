define(["knockout", "text!./routeview.html", "require", "googlemap"], function (ko, routeviewTemplate, require,  googlemap) {
    var tokenKey = "tokenInfo";
   
    var map, service, renderer;
    var data = {};
    var start, end;

    var initialLocation, browserSupportFlag;
    var ALLOW_EDIT;
    var kiev;
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
        self.Author = ko.observable();
        self.view = ko.observable(false);

        self.initialize = function (view) {
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
        self.getRoute = function (id) {
            $.ajax({
                type: 'GET',
                url: 'http://localhost:51952/api/route/getmapdata/' + id,
                response: JSON,
                success: function (response) {
                    loadRoute(response);
                }
            });
        }
        self.loadRoute = function (route) {
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
        self.displayRoute = function (origin, destination, service, display) {
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
                self.Author(data.Author);
                self.description(data.description);

            },
            error: function (data) {
            }
        });



        return self;
    }
    return { viewModel: RoutevViewModel, template: routeviewTemplate };
});

