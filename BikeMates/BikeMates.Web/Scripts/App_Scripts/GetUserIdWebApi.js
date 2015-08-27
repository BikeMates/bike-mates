alert("d"); //TODO: <- ???

    $("#Login").on('click', function () {
        alert("d"); //TODO: remove alerts - if you need it so, use console.log
        $.ajax({
            url: 'http://localhost:51952/api/account/confirmEmail',
            type: 'GET',
            headers: {"Authorization": "Bearer " + "zPmYoz7Q4xZzX0gWPjjHsPKEgGV03rvsZfVfytCtMZC1v89zfdq36ybFt59lIeonZrWNowc1wCxXz2yUVyiTYUXjAS2-GGzN3x4wxFzH3iqYdI0X2PxbWI8ikUEk_Lj-4W75j-mbymaTwGyOV8upMBKApkFOAa6g020WD2kbQVT9dxKzrXYB7izlqkqFJMrSB2t-GRjzeFDBfEUdySZ1UA9LoHoQE0yi3Z8XajCPXqg5mWTWizZPQl2mME-c7IVKl2w7BejJHjVb871LkcnZExegSNOTMb9TxCz-R_X36uM"},
            contentType: 'application/x-www-form-urlencoded',
            //beforeSend: function (request) {
            //    request.setRequestHeader("Authorization", "Bearer " + "zPmYoz7Q4xZzX0gWPjjHsPKEgGV03rvsZfVfytCtMZC1v89zfdq36ybFt59lIeonZrWNowc1wCxXz2yUVyiTYUXjAS2-GGzN3x4wxFzH3iqYdI0X2PxbWI8ikUEk_Lj-4W75j-mbymaTwGyOV8upMBKApkFOAa6g020WD2kbQVT9dxKzrXYB7izlqkqFJMrSB2t-GRjzeFDBfEUdySZ1UA9LoHoQE0yi3Z8XajCPXqg5mWTWizZPQl2mME-c7IVKl2w7BejJHjVb871LkcnZExegSNOTMb9TxCz-R_X36uM");
            //},
            success: function (data) {
                alert(data);
                //TODO: remove alerts - if you need it so, use console.log
            },
            error: function (data) {
                alert(data);
                //TODO: remove alerts - if you need it so, use console.log
            }
        });
    });


