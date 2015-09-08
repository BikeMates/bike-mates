define(['jquery', 'knockout', './router',  'knockout-projections', 'auth'], function ($, ko, router) {

    ko.components.register('home', { require: 'App/pages/home/home' });
    ko.components.register('register', { require: 'App/pages/account/register/register' });
    ko.components.register('login', { require: 'App/pages/account/login/login' });
    ko.components.register('forgotpassword', { require: 'App/pages/account/forgotpassword/forgotPassword' });
    ko.components.register('confirmforgotpassword', { require: 'App/pages/account/confirmForgotPassword/confirmForgotPassword' });
    ko.components.register('resetpassword', { require: 'App/pages/account/resetpassword/resetPassword' });
    ko.components.register('confirmresetpassword', { require: 'App/pages/account/confirmResetPassword/confirmResetPassword' });
    ko.components.register('newroute', { require: 'App/pages/route/newroute' });
    ko.components.register('route', { require: 'App/pages/route/route' });
    ko.components.register('editroute', { require: 'App/pages/route/editroute' });
    ko.components.register('profile', { require: 'App/pages/account/profile/profile' });
    ko.components.register('editprofile', { require: 'App/pages/account/editprofile/editprofile' });
    ko.components.register('admin', { require: 'App/pages/admin/admin' });
    ko.components.register('bannedroutes', { require: 'App/pages/admin/bannedRoutes' });
    ko.components.register('error', { require: 'App/pages/error/error' });

    ko.applyBindings({ route: router.currentRoute });    
});
