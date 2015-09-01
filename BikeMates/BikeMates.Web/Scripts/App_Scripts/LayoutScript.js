$(document).ready(function () {        
        if (sessionStorage.getItem("authorized")) {
            var username = sessionStorage.getItem("username");
            $("#authorized").show();
            $("#anonimus").hide();
            $('#user-name').text(usernaem);
        } else {
            $("#authorized").hide();
            $("#anonimus").show();
        }
});