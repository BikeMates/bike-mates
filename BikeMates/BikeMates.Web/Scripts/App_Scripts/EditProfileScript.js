$(document).ready(function () {

    function AppViewModel() {

        var self = this;
        self.FirstName = ko.observable("");
        self.SecondName = ko.observable("");
        self.About = ko.observable("");
        self.Picture = ko.observable("");
        self.OldPass = ko.observable("");
        self.NewPass = ko.observable("");
        self.NewPass2 = ko.observable("");

        self.fullName = ko.computed(function () {
            return self.FirstName() + " " + self.SecondName();
        }, this);

     

        $.ajax({
            url: "http://localhost:51952/api/profile",
            contentType: "application/json",
            type: "GET",
            success: function (data) {

                self.FirstName(data.firstName);
                self.SecondName(data.secondName);
                self.About(data.about);
                self.Picture(data.picture);

            },
            error: function (data) {
                alert("error occured");
            }
        });
        
        $("#save_btn").button().click(function () {
            $.ajax({
                url: "http://localhost:51952/api/profile",
                contentType: "application/json",
                type: "POST",
                dataType: 'json',
                data: ko.toJSON(AppViewModel),
                success: function (data) {

                    alert("success");

                },
                error: function (data) {
                    alert("error occured");
                }
            });
     

        });

    }

    // Activates knockout.js

    // bind view model to referring view
    ko.applyBindings(new AppViewModel());

});







var KnockoutDemoNamespace = {};

// View model declaration
KnockoutDemoNamespace.initViewModel = function (customer) {
    var customerViewModel = {
        CustomerID: ko.observable(customer.CustomerID),
        FirstName: ko.observable(customer.FirstName),
        LastName: ko.observable(customer.LastName),
        IsMale: ko.observable(customer.IsMale)
    };
    return customerViewModel;
}

// Bind the customer
KnockoutDemoNamespace.bindData = function (customer) {
    // Create the view model
    var viewModel = KnockoutDemoNamespace.initViewModel(customer);

    ko.applyBindings(viewModel);
}

KnockoutDemoNamespace.getCustomer = function (customerID) {

    $.ajax({
        url: "/Home/Get/",
        type: 'post',
        data: "{'customerID':'1' }",
        contentType: 'application/json',
        success: function (result) {
            KnockoutDemoNamespace.bindData(result);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            var errorMessage = '';
            $('#message').html(jqXHR.responseText);
        }
    });
}

KnockoutDemoNamespace.addCustomer = function () {
    $.ajax({
        url: "/Home/Add/",
        type: 'post',
        data: ko.toJSON(this),
        contentType: 'application/json',
        success: function (result) {

            $('#message').html(result);
        }
    });
}


$(document).ready(function ()
{
    KnockoutDemoNamespace.getCustomer(1);

});