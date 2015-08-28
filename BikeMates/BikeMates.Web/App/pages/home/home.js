define(["knockout", "text!./home.html", "require", "cssLoader"], function (ko, homeTemplate, require, cssLoader) {

    var localPath = "/Content/Site.css";
    var pathFromApp = require.toUrl(localPath);

    cssLoader.link(pathFromApp);

    function HomeViewModel(params) {
        var self = this;

        return self;
    }
    return { viewModel: HomeViewModel, template: homeTemplate };
});