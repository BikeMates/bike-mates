define(["knockout", "jquery"], function (ko, $) {

    ko.bindingHandlers.authBinding = {
        update: function (element, valueAccessor) {
            var value = sessionStorage.getItem("role");

            $(element).find('#anonim').hide();
            $(element).find('#user').hide();
            $(element).find('#admin').hide();

            if (value == 'User') {
                $(element).find('#user').show();
                $(element).find('#user').find("#user-name").text(sessionStorage.getItem("username"));
            }
            else {
                if (value == 'Admin') {
                    $(element).find('#admin').show()
                    $(element).find('#admin').find("#user-name").text(sessionStorage.getItem("username"));
                } else {
                    $(element).find('#anonim').show();
                }
            }
        }
    };
});