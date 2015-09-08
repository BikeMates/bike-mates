define(["knockout", "require"], function (ko, require) {

    function AuthModel(params) {
        var self = this;
        self.logout = function () {
            sessionStorage.clear();
            $('#userpanel').find('#anonim').show();
            $('#userpanel').find('#user').hide();
            $('#userpanel').find('#admin').hide();
            $('#page1').hide();
        }
        return self;
    }
    return { viewModel: AuthViewModel, template: null };
});