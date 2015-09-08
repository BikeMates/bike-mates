define(["knockout", "jquery", "jquery-ui", "text!./route.html", "require", "googlemap"], function (ko, $, $$, RouteTemplate, require, googlemap) {

    var tokenKey = "tokenInfo";

    var map, service, renderer;
    var data = {};
    var start, end;
    var Id = location.href.split('?')[1];

    var initialLocation, browserSupportFlag;
    var allowEdit = false;
    var kievCoordinates = { lat: 50.464484293992086, lng: 30.522704422473907 };
    ko.extenders.paging = function (target, pageSize) {
        var _pageSize = ko.observable(pageSize || 100),
            _currentPage = ko.observable(1);

        target.pageSize = ko.computed({
            read: _pageSize,
            write: function (newValue) {
                if (newValue > 0) {
                    _pageSize(newValue);
                }
                else {
                    _pageSize(200);
                }
            }
        });

        target.currentPage = ko.computed({
            read: _currentPage,
            write: function (newValue) {
                if (newValue > target.pageCount()) {
                    _currentPage(target.pageCount());
                }
                else if (newValue <= 0) {
                    _currentPage(1);
                }
                else {
                    _currentPage(newValue);
                }
            }
        });

        target.pageCount = ko.computed(function () {
            return Math.ceil(target().length / target.pageSize()) || 1;
        });

        target.currentPageData = ko.computed(function () {
            var pageSize = _pageSize(),
                pageIndex = _currentPage(),
                startIndex = pageSize * (pageIndex - 1),
                endIndex = pageSize * pageIndex;

            return target().slice(startIndex, endIndex);
        });

        target.moveFirst = function () {
            target.currentPage(1);
        };       
        target.movePrevious = function () {
            target.currentPage(target.currentPage() - 1);
        };
        target.moveNext = function () {
            target.currentPage(target.currentPage() + 1);
        };
        target.moveLast = function () {
            target.currentPage(target.pageCount());
        };

        return target;
    };

    function RouteViewModel(params) {
        setTimeout(function () {
            initialize();
            getRoute();
            console.log('google maps initialized');
        }, 50);
        var self = this;
        self.id = ko.observable();
        self.title = ko.observable("");
        self.start = ko.observable(new Date());
        self.distance = ko.observable("");
        self.MeetingPlace = ko.observable("");
        self.Participants = ko.observable();
        self.description = ko.observable("");
        self.subscribed = ko.observable(false);
        self.sub_show = ko.observable(false);
        self.unsub_show = ko.observable(false);
        self.Author = ko.observable();
        self.FirstName = ko.observable("");
        self.SecondName = ko.observable("");
        self.IsBanned = ko.observable(true);
        self.Subscribes = ko.observableArray([]).extend({ paging: 5 });
        function initialize () {
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

        }
        function handleNoGeolocation(errorFlag) {
            if (errorFlag == true) {
                alert("Geolocation service failed.");
                initialLocation = kievCoordinates;
            } else {
                alert("Your browser doesn't support geolocation. We've placed you in Siberia.");
                initialLocation = kievCoordinates;
            }
            map.setCenter(initialLocation);
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
        function getRoute() {
            $.ajax({
                type: 'GET',
                url: 'http://localhost:51952/api/route/find/' + Id,
                response: JSON,
                success: function(response) {
                    var mapData = JSON.parse(response.mapData);
                    console.log("getRoute");
                    loadRoute(mapData);
                    var author = JSON.parse(response.author);
                    $("#AuthorLink").attr("href", "http://localhost:51949/#profile?" + author.Id)
                    self.Author(author.FirstName + author.SecondName);
                    self.title(response.title);
                    self.description(response.description);
                    self.start(response.start);
                    self.distance(response.distance);
                    self.MeetingPlace(response.meetingPlace);
                    self.IsBanned(response.isBanned);
                    $('#MapData').val(response.mapData);
                }
            });
        }

        function refreshSubscribeButtonsVisibility() {
            if (self.subscribed()) {
                self.sub_show(false);
                self.unsub_show(true);
            }
            else {
                self.sub_show(true);
                self.unsub_show(false);
            }
        }

        self.subscribe = function () {
            if (self.subscribed())
            {
                return;
            }

            var apiurl = "http://localhost:51952/api/subscribe/" + Id;
            $.ajax({
                url: apiurl,
                contentType: "application/json",
                type: "PUT",
                headers: { "Authorization": "Bearer " + sessionStorage.getItem(tokenKey) },
                success: function (data) {
                    self.subscribed(true);
                    refreshSubscribeButtonsVisibility();
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
                    self.subscribed(false);
                    refreshSubscribeButtonsVisibility();
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
                    location.href = "#bannedroutes";
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
        var userStatus = sessionStorage.getItem("authorized");
        if (userStatus == 'true') {
            $.ajax({
                url: "http://localhost:51952/api/route/findlogged" + '/' + Id,
                contentType: "application/json",
                type: "GET",
                headers: { "Authorization": "Bearer " + sessionStorage.getItem(tokenKey) },
                success: function (data) {
                    self.description(data.description);
                    self.subscribed(data.isSubscribed);

                    if (self.subscribed()) {
                        self.sub_show(false);
                        self.unsub_show(true);
                    }
                    else {
                        self.sub_show(true);
                        self.unsub_show(false);
                    }
                },
                error: function (data) {
                }
            });
        }
        self.Subscribes = function () {
            $.ajax({
                url: "http://localhost:51952/api/route/find"+'/'+Id,
                contentType: "application/json",
                type: "GET",
                dataType: 'json',
                data: ko.toJSON(self),
                success: function (data) {
                    $.each(data, function (key, val) {

                        self.Subscribes.push(new User(val.id, val.FirstName, val.SecondName));
                    });
                }
            });
        }
        //self.Author = function () {
        //    $.ajax({
        //        url: "http://localhost:51952/api/route/find"+'/'+Id,
        //        contentType: "application/json",
        //        type: "GET",
        //        success: function (data) {
        //            //self.Author(new User(data.author.id, data.author.FirstName, data.author.SecondName));
        //        }
        //    });
        //}
        self.goToUser = function (id) {
            return "http://localhost:51949/#profile?" + id;
        };
        return self;

    }
    function User(id,FirstName,SecondName ) {
        var self = this;
        self.FirstName = FirstName;
        self.SecondName = SecondName;
        self.id = id;
    }
    return { viewModel: RouteViewModel, template: RouteTemplate };
});
