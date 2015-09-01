define(["knockout", "text!./confirmResetPassword.html", "require", "cssLoader"], function (ko, confirmResetPasswordTemplate, require, cssLoader) {

    var localPath = "/Content/Site.css";
    var pathFromApp = require.toUrl(localPath);

    cssLoader.link(pathFromApp);

    function ConfirmResetPasswordViewModel(params) {
        var self = this;

        return self;
    }
    return { viewModel: ConfirmResetPasswordViewModel, template: confirmResetPasswordTemplate };
});