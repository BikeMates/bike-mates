var require = {
    urlArgs: "ts=" + new Date().getTime(), // disable caching - remove in production
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
        "captcha": "http://www.google.com/recaptcha/api"
    },
    shim: {
        "bootstrap": { deps: ["jquery"] },
        "recaptcha": { exports: 'Recaptcha' }
    }
}