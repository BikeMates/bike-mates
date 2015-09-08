define(["knockout", "text!./admin.html", "require"], function (ko, adminTemplate, require) {

    var tokenKey = "tokenInfo"; //TODO: Create auth service which will create Token header. Remove the duplicated logic from all files

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

        target.editing = ko.observable(false);

        // Behaviors
        target.edit = function () { target.editing(true) }

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
            function(){ return "Page " + _currentPage() + " of " + target.pageCount()}
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

    function AdminViewModel(params) {
        var self = this;
        self.id = ko.observable("");
        self.firstName = ko.observable("");
        self.secondName = ko.observable("");

        self.NotEmpty = ko.observable(true);

        self.users = ko.observableArray([]).extend({ paging: 6 });

        self.unban = function () {

            var selected = new Array();
            $('input:checked').each(function () {
                selected.push($(this).attr('value'));
            });

            $.ajax({
                url: "http://localhost:51952/api/admin/unbanusers",
                contentType: "application/json",
                type: "POST",
                headers: { "Authorization": "Bearer " + sessionStorage.getItem(tokenKey) },
                data: JSON.stringify(selected),
                success: function (data) {
                    $.each(selected, function (key, val) {
                        self.users.remove(function (user) { return user.id = val });
                    });
                    self.loadUsers();
                },
            });

        }

        self.loadUsers = function () {
            $.ajax({
                url: "http://localhost:51952/api/admin/getbannedusers",
                type: 'GET',
                headers: { "Authorization": "Bearer " + sessionStorage.getItem(tokenKey) },
                contentType: 'application/x-www-form-urlencoded',

                success: function (data) {
                    if (data.length != 0) {
                        self.NotEmpty(true);
                        $.each(data, function (key, val) {
                            console.log(val.picture);
                            self.users.push(new user(val.id, val.firstName, val.secondName, val.Picture));
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
        self.loadUsers();
        return self;
    }

    function user(id, firsName, secondName, picture) {
        var self = this;
        self.id = id;
        self.firstName = firsName;
        self.secondName = secondName;
        self.imageUrl = "http://localhost:51952/api/profilepicture/";
        self.imagePath = self.imageUrl + id + '?' + Math.random();

        self.fullName = ko.computed(function () {
            return self.firstName + " " + self.secondName
        }, this);

        self.userLink = ko.computed(function () {
            return "http://localhost:51949/#profile?" + self.id;
        }, this);
    }
    return { viewModel: AdminViewModel, template: adminTemplate };
});