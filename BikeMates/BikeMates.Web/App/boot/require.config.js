var require = {
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
        "jqueryValidate":"/Scripts/jquery.validate.js",
        "unobtrusive":"/Scripts/jquery.validate.unobtrusive.js",
        "jqure-ui":"/Scripts/jquery-ui.js",        "modernizr":"/Scripts/modernizr-2.6.2.js",
        "respond": "/Scripts/respond.js"
    },
    shim: {
        "bootstrap": {
            deps: ["jquery"]
        }
    }
}