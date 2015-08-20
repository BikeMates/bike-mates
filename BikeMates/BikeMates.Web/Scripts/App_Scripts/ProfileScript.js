function AppViewModel() {
    this.firstName = ko.observable("Volodymyr");
    this.lastName = ko.observable("Zavayko");
    this.about = ko.observable("Wow Im in the BikeMates!");
    this.imagePath = ko.observable("~/Content/Images/avatar-big.png");


    this.fullName = ko.computed(function () {
        return this.firstName() + " " + this.lastName();
    }, this);
}

// Activates knockout.js
$(document).ready(function () {

    // bind view model to referring view
    ko.applyBindings(new AppViewModel());

    // load student data
  //  studentRegisterViewModel.studentListViewModel.getStudents();
});
