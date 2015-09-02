define(["knockout", "text!./confirmResetPassword.html", "require"], function (ko, confirmResetPasswordTemplate, require) {

    function ConfirmResetPasswordViewModel(params) {
        var self = this;

        return self;
    }
    return { viewModel: ConfirmResetPasswordViewModel, template: confirmResetPasswordTemplate };
});