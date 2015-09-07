define(["knockout", "jquery", "jquery-ui", "text!./home.html", "require"], function (ko, $, $$, homeTemplate, require) {

    ko.bindingHandlers.datepicker = {
        init: function (element, valueAccessor, allBindingsAccessor) {
            var options = allBindingsAccessor().datepickerOptions || {},
                $el = $(element);

            //initialize datepicker with some optional options
            $el.datepicker(options);

            //handle the field changing
            ko.utils.registerEventHandler(element, "change", function () {
                var observable = valueAccessor();
                observable($el.datepicker("getDate"));
            });

            //handle disposal (if KO removes by the template binding)
            ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
                $el.datepicker("destroy");
            });

        },
        update: function (element, valueAccessor) {
            var value = ko.utils.unwrapObservable(valueAccessor()),
                $el = $(element),
                current = $el.datepicker("getDate");

            if (value - current !== 0) {
                $el.datepicker("setDate", value);
            }
        }
    };
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

        target.currentPageData = ko.computed(function () {
            var pageSize = _pageSize(),
                pageIndex = _currentPage(),
                startIndex = pageSize * (pageIndex - 1),
                endIndex = pageSize * pageIndex;

            return target().slice(startIndex, endIndex);
        });
        
        target.currentPage1 = target.currentPage();
        target.currentPage2 = target.currentPage()+1;
        target.currentPage3 = target.currentPage()+2;
        target.moveFirst = function () {
            target.currentPage(1);
        };
        target.moveOne = function () {
            target.currentPage(target.currentPage());
        };
        target.moveTwo = function () {
            target.currentPage(target.currentPage()+1);
        };
        target.moveThree = function () {
            target.currentPage(target.currentPage() + 2);
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

    var HomeViewModel= function () {

        var self = this;
        self.author = ko.observable();
        self.isBanned = ko.observable();
        self.mapData = ko.observable();
        self.meetingPlace = ko.observable("");
        self.subscribers = ko.observable();
        self.id = ko.observable();
        self.title = ko.observable("");
        self.start = ko.observable("");
        self.distance = ko.observable("");
        self.Participants = ko.observable();
        self.Location = ko.observable("");
        self.DateFrom = ko.observable(new Date());
        self.DateTo = ko.observable(new Date());
        self.MinDistance = ko.observable("");
        self.MaxDistance = ko.observable("");
        self.description = ko.observable("");
        self.allRoutes = ko.observableArray([]).extend({ paging: 5 });
        self.OrderByFieldName = ko.observable("");

        self.setOrderAndSearch = function (orderBy) {
            if (orderBy) {
                self.OrderByFieldName(orderBy);
            }
            self.searchRoutes();
        }
      
        self.searchRoutes = function () {
            self.allRoutes.removeAll();
            $.ajax({
                url: "http://localhost:51952/api/route/getroutes",
                contentType: "application/json",
                type: "POST",
                dataType: 'json',
                data: ko.toJSON(self),
                success: function (data) {
                    $.each(data, function (key, val) {
                        
                  self.allRoutes.push(new route(val.author,val.description, val.distance, val.id, val.isBanned, val.mapData, val.meetingPlace, val.start, val.subscribers, val.title));
                    });
                }
            });
        }
        self.searchRoutes();
        return self;
    }

    self.goToRoute = function (id) {
        $.ajax({
            url: "http://localhost:51952/api/route/getid"+'/'+id,
            contentType: "application/json",
            type: "GET",
            success: function (data) {
                console.log("success");
            },
            error: function (data) {
            }
        });
        //window.location = "http://localhost:51949/#route?"+id;
    };
    function route(author, description, distance, id, isBanned, mapData, meetingPlace, start, subscribers, title) {
        var self = this;
        self.author = author;
        self.description = description;
        self.distance = distance;
        self.id = id;
        self.isBanned = isBanned;
        self.mapData = mapData;
        self.meetingPlace = meetingPlace;
        self.start = start;
        self.subscribers = subscribers;
        self.title = title;
    }
    
    return { viewModel: HomeViewModel, template: homeTemplate };
});