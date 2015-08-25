
$(document).ready(function () {

    function AppViewModel() {
        uploads: ko.observableArray([]);
        var self = this;
        self.FirstName = ko.observable("");
        self.SecondName = ko.observable("");
        self.About = ko.observable("");
        self.Picture = ko.observable("");
        self.OldPass = ko.observable("");
        self.NewPass = ko.observable("");
        self.NewPass2 = ko.observable("");
        self.Id = ko.observable("");

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
                self.Id(data.id);
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
                data: ko.toJSON(self),
                success: function (data) {

                    alert("success");

                },
                error: function (data) {
                    alert("error occured");
                }
            });
     

        });




   $("#upload_btn").button().click(function () {
        $.ajax({
            url: 'http://localhost:51952/api/profilepicture',
            type: 'POST',
            data: new FormData($('#files')[0]), // The form with the file inputs.
            processData: false                          // Using FormData, no need to process data.
        }).done(function () {
            alert("Success: Files sent!");
        }).fail(function () {
            alert("An error occurred, the files couldn't be sent!");
        });
    });



        var input = document.getElementById("file");
        var formdata = false;
        if (window.FormData) {
            formdata = new FormData();
        }
        input.addEventListener("change", function (evt) {
            var i = 0, len = this.files.length, img, reader, file;

            for (; i < len; i++) {
                file = this.files[i];

                if (!!file.type.match(/image.*/)) {
                    if (window.FileReader) {
                        reader = new FileReader();
                        reader.onloadend = function (e) {
                            //showUploadedItem(e.target.result, file.fileName);
                        };
                        reader.readAsDataURL(file);
                    }

                    if (formdata) {
                        formdata.append("image", file);
                        formdata.append("extra", 'extra-data');
                    }

                    if (formdata) {
                        jQuery('div#response').html('<br /><img src="ajax-loader.gif"/>');

                        jQuery.ajax({
                            url: "http://localhost:51952/api/profilepicture",
                            type: "POST",
                            data: formdata,
                            processData: false,
                            contentType: false,
                            success: function (res) {
                                jQuery('div#response').html("Successfully uploaded");
                            }
                        });
                    }
                }
                else {
                    alert('Not a vaild image!');
                }
            }

        }, false);


    }



    // Activates knockout.js

    // bind view model to referring view
    ko.applyBindings(new AppViewModel());

});







