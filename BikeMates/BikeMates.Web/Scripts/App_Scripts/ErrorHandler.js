function Process(status) {
    switch (status) {
    case 500:
        window.location.href = "http://localhost:51949/#error?=500";
        break;
    case 404:
        window.location.href = "http://localhost:51949/#error?=404";
        break;
    case 403:
        window.location.href = "http://localhost:51949/#error?=403";
        break;
    case 401:
        window.location.href = "http://localhost:51949/#error?=401";
        break;
    case 400:
        window.location.href = "http://localhost:51949/#error?=400";
        break;
    default:
        break;
    }
}