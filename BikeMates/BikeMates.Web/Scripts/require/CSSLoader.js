(function () {
    "use strict";

    define(function () {

        var link = function (url) {
            var css = document.createElement('link');
            css.rel = 'stylesheet';
            css.href = url;

            document.head.appendChild(css);
        };

        return {
            link: link
        };
    });
}());