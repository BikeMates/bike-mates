define(["knockout", "text!./error.html", "require"], function (ko, errorPageTemplate, require) {

    function ErrorPageViewModel(params) {
        var self = this;
        var id = location.href.split('=')[1];
        self.code = id;
        if (id == '500') {
            self.title = "Oops...";
            self.message = "We've got error here.";
            self.imagePath = 'http://localhost:51949/content/images/error/500.jpg';
        }
        if (id == '401') {
            self.title = "ERROR 401 UNAUTHORIZED";
            self.message = "Please authenticate or register.";
            self.imagePath = 'http://localhost:51949/content/images/error/401.png';
        }
        if (id == '404') {
            self.title = "ERROR 404 NOT FOUND";
            self.message = "The page you were looking for appears to have been moved, deleted or does not exist.";
            self.imagePath ='http://localhost:51949/content/images/error/404.png';
        }
        if (id == '403') {
            self.title = "ERROR 403 FORBIDDEN";
            self.message = "You shell not pass!";
            self.imagePath = 'http://localhost:51949/content/images/error/403.png';
        }
        if (id == '400') {
            self.title = "ERROR 400 BAD REQUEST";
            self.message = "Server can not recognize your request. Try another.";
            self.imagePath = 'http://localhost:51949/content/images/error/400.jpg';
        }
        if (id != '400' && id != '401' && id != '403' && id != '404' && id != '500')
        {
            self.title = "ERROR 404 NOT FOUND";
            self.message = "The page you were looking for appears to have been moved, deleted or does not exist.";
            self.imagePath = 'http://localhost:51949/content/images/error/404.png';
        }
    }
    return { viewModel: ErrorPageViewModel, template: errorPageTemplate };
});