define(["knockout", "text!./confirmForgotPassword.html", "require", "cssLoader"], function (ko, confirmForgotPasswordTemplate, require, cssLoader) {

    var localPath = "/Content/Site.css";
    var pathFromApp = require.toUrl(localPath);

    cssLoader.link(pathFromApp);

    function ConfirmForgotPasswordViewModel(params) {
        var self = this;

        return self;
    }
    return { viewModel: ConfirmForgotPasswordViewModel, template: confirmForgotPasswordTemplate };
});