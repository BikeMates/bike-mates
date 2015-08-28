define(['jquery', 'knockout', './router',  'knockout-projections'], function ($, ko, router) {

    ko.components.register('home', { require: 'App/pages/home/home' });

    ko.components.register('register', { require: 'App/pages/register/register' });

    ko.components.register('login', { require: 'App/pages/login/login' });

    ko.components.register('route', { require: 'App/pages/route/add' });

    ko.applyBindings({ route: router.currentRoute });
});
