define(["knockout", "text!./confirmForgotPassword.html", "require"], function (ko, confirmForgotPasswordTemplate, require) {

    function ConfirmForgotPasswordViewModel(params) {
        var self = this;

        return self;
    }
    return { viewModel: ConfirmForgotPasswordViewModel, template: confirmForgotPasswordTemplate };
});