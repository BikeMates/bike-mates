function Instance(Route_name) {
    return {
        RouteName: ko.observable(Route_name)
    };
}

var viewModel = {
    
    instances: ko.observableArray([
        new Instance(1, "Zed"),
        new Instance(2, "Jane"),
        new Instance(3, "John"),
        new Instance(4, "Anne"),
        new Instance(5, "Ted")
    ])
};

viewModel.sortFunction = function (a, b) {
    return a.RouteName().toLowerCase() > b.RouteName().toLowerCase() ? 1 : -1;
};

viewModel.sortedInstances = ko.dependentObservable(function () {
    return this.instances.slice().sort(this.sortFunction);
}, viewModel);


ko.applyBindings(viewModel);