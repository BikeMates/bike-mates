define(['jquery', 'knockout', './router',  'knockout-projections'], function ($, ko, router) {

    ko.components.register('home', { require: 'App/pages/home/home' });
    ko.components.register('register', { require: 'App/pages/account/register/register' });
    ko.components.register('login', { require: 'App/pages/account/login/login' });
    ko.components.register('forgotpassword', { require: 'App/pages/account/forgotpassword/forgotPassword' });
    ko.components.register('confirmforgotpassword', { require: 'App/pages/account/confirmForgotPassword/confirmForgotPassword' });
    ko.components.register('resetpassword', { require: 'App/pages/account/resetpassword/resetPassword' });
    ko.components.register('confirmresetpassword', { require: 'App/pages/account/confirmResetPassword/confirmResetPassword' });
    ko.components.register('route', { require: 'App/pages/route/route' });
    ko.components.register('profile', { require: 'App/pages/account/profile/profile' });
    ko.components.register('editprofile', { require: 'App/pages/account/editprofile/editprofile' });
    ko.components.register('admin', { require: 'App/pages/admin/admin' });
    ko.components.register('bannedroutes', { require: 'App/pages/admin/bannedRoutes' });
    ko.applyBindings({ route: router.currentRoute });

    ko.bindingHandlers.myBinding = {
        init: function (element, valueAccessor, allBindings) {
            var value = valueAccessor();

            var valueUnwrapped = ko.unwrap(value);

            var authorized = allBindings.get('authorized') || false;

            if (authorized == true)
                $(element).show(); 
            else
                $(element).hide(); 
        }
    };
});
