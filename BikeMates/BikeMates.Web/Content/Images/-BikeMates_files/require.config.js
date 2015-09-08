var require = {
    urlArgs: "ts=" + new Date().getMilliseconds(),
    baseUrl: "/",
    paths: {
        "async": 'Scripts/require/async',
        "googlemap": "https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=false&callback=initialize",
        "bootstrap": "Scripts/bootstrap",
        "crossroads": "Scripts/crossroads/crossroads",
        "jquery": "Scripts/jquery-1.10.2",
        "knockout": "Scripts/knockout-3.3.0",
        "knockout-projections": "Scripts/knockout-projections.min",
        "signals": "Scripts/crossroads/signals",
        "hasher": "Scripts/crossroads/hasher",
        "text": "Scripts/require/text",
        "captcha": "http://www.google.com/recaptcha/api",
        "jquery-ui": "/Scripts/jquery-ui",
        "auth": "/Scripts/App_Scripts/AuthScript",
        "authmodel": "/Scripts/App_Scripts/AuthModelScript"
    },
    shim: {
        shim: {
        'knockout': { deps: [ 'jquery'] },
        'bootstrap': { deps: ['jquery'] },
        'jquery-ui': { deps: ['jquery'] },
        'googlemap': { exports: 'google'}
        }
    }
}