define(["knockout", "text!./home.html"], function (ko, homeTemplate) {

    function HomeViewModel(params) {
        var self = this;

        return self;
    }
    return { viewModel: HomeViewModel, template: homeTemplate };
});