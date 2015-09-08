define(["knockout", "jquery", "jquery-ui", "text!./editroute.html", "require", "googlemap"], function (ko, $, $$, RouteTemplate, require, googlemap) {

    var tokenKey = "tokenInfo";

    var map, service, renderer;
    var data = {};
    var start, end;

    var initialLocation, browserSupportFlag;
    var allowEdit = true;
    var kiev;

    ko.bindingHandlers.datepicker = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            var options = allBindingsAccessor().datepickerOptions || {},
                $el = $(element);

            //initialize datepicker with some optional options
            $el.datepicker(options);

            //handle the field changing
            ko.utils.registerEventHandler(element, "change", function () {
                var observable = valueAccessor();
                observable($el.datepicker("getDate"));
            });

            //handle disposal (if KO removes by the template binding)
            ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                $el.datepicker("destroy");
            });

        },
        update: function (element, valueAccessor) {
            var value = ko.utils.unwrapObservable(valueAccessor()),
                $el = $(element),
                current = $el.datepicker("getDate");

            if (value - current !== 0) {
                $el.datepicker("setDate", value);
            }
        }
    };

    function EditRouteViewModel(params) {
        var self = this;
        var Id = location.href.split('?')[1];

        if (sessionStorage.getItem('authorized') != 'true') {
            window.location.href = "http://localhost:51949/#error?=401";
        }
        self.Id = ko.observable(Id);
        self.Start = ko.observable(new Date()),
        self.Distance = ko.observable("");
        self.Title = ko.observable("");
        self.Description = ko.observable("");
        self.MeetingPlace = ko.observable("");
        self.MapData = ko.observable("");

        self.initialize = function(allowEdit) {
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
                url: 'http://localhost:51952/api/route/update/',
                data: $('#routeForm').serialize(),
                success: function(response) {
                    window.location.href = "http://localhost:51949/#route?"+Id;
                }
            });
            return false;
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
                travelMode: google.maps.TravelMode.WALKING,
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
            $('#km').show();
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
        function clearMap() {
            if (renderer != null) {
                renderer.setMap(null);
                renderer = null;
            }

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

            start = null;
            end = null;
            data = {};
        }
        function getRoute() {
            $.ajax({
                type: 'GET',
                url: 'http://localhost:51952/api/route/find/' + Id,
                response: JSON,
                success: function (response) {
                    var mapData = JSON.parse(response.mapData);
                    console.log("getRoute");
                    self.Title(response.title);
                    self.Description(response.description);
                    self.Start(response.start);
                    self.Distance(response.distance);
                    self.MeetingPlace(response.meetingPlace);
                    $('#MapData').val(response.mapData);
                    loadRoute(mapData);
                }
            });
        }

        self.Save = function () {
            alert("Route added to DB\n" +
                "Remove alert and make redirect to all user routes\n" +
                "after that page is ready");
            saveRoute();
        }
        self.Clear = function () {
            clearMap();
            $('#Distance').val(0);
            $('#km').hide();
        }
        self.Load = function(Id) {
            getRoute(Id);
        }
        self.Delete = function () {
            $.ajax({
                type: 'DELETE',
                headers: { "Authorization": "Bearer " + sessionStorage.getItem(tokenKey) },
                url: 'http://localhost:51952/api/route/delete/' + Id,
                response: JSON,
                success: function (response) {
                    window.location.href = "#";
                }
            });
        }
        self.Load(Id);
    }
    return { viewModel: EditRouteViewModel(), template: RouteTemplate };
});
