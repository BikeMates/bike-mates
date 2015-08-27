var map, service, renderer;
var data = {};
var start, end;

var initialLocation, browserSupportFlag;
var ALLOW_EDIT;

function initialize(allowEdit) {
    ALLOW_EDIT = allowEdit;

    var kiev = new google.maps.LatLng(50.464484293992086, 30.522704422473907);

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
        waypoints[i] = [wp[i].lat(), wp[i].lng()];
    }
    data.waypoints = waypoints;

    var stringifiedData = JSON.stringify(data);
    $('#MapData').val(stringifiedData);

    $.ajax({
        type: 'POST',
        url: '/Route/Save',
        data: $('#routeForm').serialize(),
        success: function (response) { }
    });
    return false;
}

function loadRoute(route) {
    var waypoints = [];
    for (var i = 0; i < route.waypoints.length; i++) {
        waypoints[i] = {
            'location': new google.maps.LatLng(route.waypoints[i][0], route.waypoints[i][1]),
            'stopover': false
        }
    }
    service.route({
        'origin': new google.maps.LatLng(route.start.lat, route.start.lng),
        'destination': new google.maps.LatLng(route.end.lat, route.end.lng),
        'waypoints': waypoints,
        'travelMode': google.maps.DirectionsTravelMode.DRIVING
    }, function (result, status) {
        if (status == google.maps.DirectionsStatus.OK) {
            renderer.setDirections(result);
        }
    });
}

function displayRoute(origin, destination, service, display) {
    service.route({
        origin: origin,
        destination: destination,
        waypoints: [],
        travelMode: google.maps.TravelMode.DRIVING,
        avoidTolls: true
    }, function (response, status) {
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
    document.getElementById('totalDistance').value = total;
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
        return;
    }
}

function clearMap() {
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

$('#ClearMapButton').click(function (e) { clearMap(); });


$("#datepicker").datepicker();
