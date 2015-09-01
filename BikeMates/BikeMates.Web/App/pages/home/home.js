define(["knockout", "text!./home.html", "require", "cssLoader"], function (ko, homeTemplate, require, cssLoader) {
    //TODO: Remove this page if it is not used
    var localPath = "/Content/Site.css";
    var pathFromApp = require.toUrl(localPath);

    cssLoader.link(pathFromApp);

    function HomeViewModel(params) {
        var self = this;

        return self;
    }
    return { viewModel: HomeViewModel, template: homeTemplate };
});