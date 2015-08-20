function AppViewModel() {
    this.firstName = ko.observable("Volodymyr");
    this.lastName = ko.observable("Zavayko");
    this.about = ko.observable("Wow Im in the BikeMates!");
    this.imagePath = ko.observable('http://localhost:51949/Content/Images/avatar-big.png');
    

    this.fullName = ko.computed(function () {
        return this.firstName() + " " + this.lastName();
    }, this);

      

}



// Activates knockout.js
$(document).ready(function () {

    // bind view model to referring view
    ko.applyBindings(new AppViewModel());


      
        $.ajax({
            url: "http://localhost:51952/api/profile",
            contentType: "text/json",
            type: "GET",
            success: function (data) {
                alert("success")
            },
            error: function (data) {
                alert("error occured");
            }
        });


});  




