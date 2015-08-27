define(["knockout", "text!./login.html"], function (ko, loginTemplate) {

    function LoginViewModel(params) {
        var self = this;

        return self;
    }
    return { viewModel: LoginViewModel, template: loginTemplate };
});