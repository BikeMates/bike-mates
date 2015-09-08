define(["knockout", "text!./bannedRoutes.html", "require"], function (ko, bannedRoutesTemplate, require) {

    var tokenKey = "tokenInfo";

    ko.extenders.paging = function (target, pageSize) {
        var _pageSize = ko.observable(pageSize || 100),
            _currentPage = ko.observable(1);

        target.pageSize = ko.computed({
            read: _pageSize,
            write: function (newValue) {
                if (newValue > 0) {
                    _pageSize(newValue);
                }
                else {
                    _pageSize(200);
                }
            }
        });

        target.currentPage = ko.computed({
            read: _currentPage,
            write: function (newValue) {
                if (newValue > target.pageCount()) {
                    _currentPage(target.pageCount());
                }
                else if (newValue <= 0) {
                    _currentPage(1);
                }
                else {
                    _currentPage(newValue);
                }
            }
        });

        target.pageCount = ko.computed(function () {
            return Math.ceil(target().length / target.pageSize()) || 1;
        });

        target.pagingValue = ko.computed(
            function () { return "Page " + _currentPage() + " of " + target.pageCount() }
        );

        target.currentPageData = ko.computed(function () {
            var pageSize = _pageSize(),
                pageIndex = _currentPage(),
                startIndex = pageSize * (pageIndex - 1),
                endIndex = pageSize * pageIndex;

            return target().slice(startIndex, endIndex);
        });

        target.moveFirst = function () {
            target.currentPage(1);
        };
        target.movePrevious = function () {
            target.currentPage(target.currentPage() - 1);
        };
        target.moveNext = function () {
            target.currentPage(target.currentPage() + 1);
        };
        target.moveLast = function () {
            target.currentPage(target.pageCount());
        };

        return target;
    };

    function BannedRoutesViewModel(params) {
        var self = this;
        self.id = ko.observable("");
        self.name = ko.observable("");
        self.routes = ko.observableArray([]).extend({ paging: 5 });

        self.NotEmpty = ko.observable(true);

        self.unban = function () {

            var selected = new Array();
            $('input:checked').each(function () {
                selected.push($(this).attr('value'));
            });

            $.ajax({
                url: "http://localhost:51952/api/admin/unbanroutes",
                contentType: "application/json",
                type: "POST",
                headers: { "Authorization": "Bearer " + sessionStorage.getItem(tokenKey) },
                data: JSON.stringify(selected),
                success: function (data) {
                    $.each(selected, function (key, val) {
                        self.routes.remove(function (route) { return route.id = val });
                    });
                    self.loadRoutes();
                }
            });

        }

        self.loadRoutes = function () {
            $.ajax({
                url: "http://localhost:51952/api/admin/getbannedroutes",
                type: 'GET',
                headers: { "Authorization": "Bearer " + sessionStorage.getItem(tokenKey) },
                contentType: 'application/x-www-form-urlencoded',

                success: function (data) {
                    if (data.length != 0) {
                        self.NotEmpty(true);
                        $.each(data, function (key, val) {
                            self.routes.push(new route(val.id, val.title, val.description));
                        });
                    } else {
                        self.NotEmpty(false);
                    }
                },
                statusCode: {
                    401: function (response) {
                        window.location.href = "#login";
                    }
                }
            });
        }



        self.loadRoutes();
        return self;
    }

    function route(id, title, description) {
        var self = this;
        self.id = id;
        self.title = title;
        self.description = description;
        self.routeLink = ko.computed(function () {
            return "http://localhost:51949/#route?" + self.id;
        }, this);
    }
    return { viewModel: BannedRoutesViewModel, template: bannedRoutesTemplate };
});