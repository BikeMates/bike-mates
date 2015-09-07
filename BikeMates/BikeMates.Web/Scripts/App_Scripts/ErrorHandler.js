$(function () {
$.ajaxSetup({
        error: function (jqXHR, textStatus, errorThrown) {
            if (jqXHR.status === 500) {
                window.location.href = "http://localhost:51949/#error?=500";
            }
            if (jqXHR.status === 404) {
                window.location.href = "http://localhost:51949/#error?=404";
            }
            if (jqXHR.status === 403) {
                window.location.href = "http://localhost:51949/#error?=403";
            }
            if (jqXHR.status === 401) {
                window.location.href = "http://localhost:51949/#error?=401";
            }
            if (jqXHR.status === 400) {
                window.location.href = "http://localhost:51949/#error?=400";
            }
        }
    });
});
