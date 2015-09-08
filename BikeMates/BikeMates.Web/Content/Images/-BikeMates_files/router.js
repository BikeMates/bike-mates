/// <reference path="/Scripts/crossroads/crossroads.js" />
define(["jquery", "knockout", "crossroads", "hasher"], function ($, ko, crossroads, hasher) {

    function Router(config) {
        var currentRoute = this.currentRoute = ko.observable({});

        ko.utils.arrayForEach(config.routes, function (route) {
            crossroads.addRoute(route.url, function (requestParams) {
                currentRoute(ko.utils.extend(requestParams, route.params));
            });
        });
        crossroads.routed.add(console.log, console);
        activateCrossroads();
    }

    function activateCrossroads() {
        function parseHash(newHash, oldHash) {
            crossroads.parse(newHash);
        }

        function changeHash(newHash, oldHash) {
            var route = newHash;
            crossroads.parse(newHash);
        }
        crossroads.normalizeFn = crossroads.NORM_AS_OBJECT;

        hasher.initialized.add(parseHash);
        hasher.changed.add(changeHash);
        hasher.init();
    }


    return new Router({
        routes: [
            { url: '', params: { page: 'home' } },
            { url: 'register', params: { page: 'register' } },
            { url: 'login', params: { page: 'login' } },
            { url: 'forgotpassword', params: { page: 'forgotpassword' } },
            { url: 'confirmforgotpassword', params: { page: 'confirmforgotpassword' } },
            { url: 'resetpassword', params: { page: 'resetpassword' } },
            { url: 'confirmresetpassword', params: { page: 'confirmresetpassword' } },
            { url: 'newroute', params: { page: 'newroute' } },
            { url: 'route?{id}', params: { page: 'route' } },
            { url: 'editroute?{id}', params: { page: 'editroute' } },
            { url: 'profile?{id}', params: { page: 'profile' } },
            { url: 'editprofile', params: { page: 'editprofile' } },
            { url: 'admin', params: { page: 'admin' } },
            { url: 'bannedroutes', params: { page: 'bannedroutes' } },
            { url: 'error?{id}', params: { page: 'error' } }
        ]
    });
});