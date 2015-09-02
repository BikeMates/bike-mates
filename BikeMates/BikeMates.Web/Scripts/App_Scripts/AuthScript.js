define(["knockout", "jquery"], function (ko, $) {

    ko.bindingHandlers.myBinding = {
        update: function (element, valueAccessor) {
            var value = sessionStorage.getItem("authorized");
            console.log(value);
            
            if (value == 'true') {
                $(element).show();
            }
            else {
                $(element).hide();
            }
        }
    };
});