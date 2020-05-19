var settings = {
  "url": "https://localhost:44384/api/crud/cliente?id=2",
  "method": "GET",
  "timeout": 0,
  "headers": {
    "Content-Type": "application/json"
  },
};

$.ajax(settings).done(function (response) {
  console.log(response);
});