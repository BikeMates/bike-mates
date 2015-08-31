define(['jquery', 'knockout', './router',  'knockout-projections'], function ($, ko, router) {

    ko.components.register('home', { require: 'App/pages/home/home' });
    ko.components.register('register', { require: 'App/pages/account/register/register' });
    ko.components.register('login', { require: 'App/pages/account/login/login' });
    ko.components.register('forgotpassword', { require: 'App/pages/account/forgotpassword/forgotPassword' });
    ko.components.register('resetpassword', { require: 'App/pages/account/resetpassword/resetPassword' });

    ko.components.register('route', { require: 'App/pages/route/route' });
    ko.components.register('profile', { require: 'App/pages/account/profile/profile' });
    ko.components.register('editprofile', { require: 'App/pages/account/editprofile/editprofile' });
    ko.components.register('admin', { require: 'App/pages/admin/admin' });
    ko.applyBindings({ route: router.currentRoute });
});
