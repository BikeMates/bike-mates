var require = {
    urlArgs: "ts=" + new Date().getTime(),
    waitSeconds: 15,
    baseUrl: "/",
    paths: {
        "bootstrap": "Scripts/bootstrap",
        "crossroads": "Scripts/crossroads/crossroads",
        "jquery": "Scripts/jquery-1.10.2",
        "knockout": "Scripts/knockout-3.3.0",
        "knockout-projections": "Scripts/knockout-projections.min",
        "signals": "Scripts/crossroads/signals",
        "hasher": "Scripts/crossroads/hasher",
        "text": "Scripts/require/text",
        "cssLoader": "Scripts/require/CSSLoader",
        "captcha": "http://www.google.com/recaptcha/api",
        "jquery-ui": "/Scripts/jquery-ui",
        "googlemap": "https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=false&callback=initialize",
        "RouteMap": "/Scripts/App_Scripts/Route",
        "auth": "/Scripts/App_Scripts/AuthScript"
    },
    shim: {
        'knockout': { deps: [ 'jquery'] },
        "bootstrap": { deps: ["jquery"] },
        "jquery-ui": { deps: ["jquery"] }
    }
}