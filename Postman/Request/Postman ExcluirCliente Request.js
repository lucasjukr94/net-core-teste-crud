var settings = {
  "url": "https://localhost:44384/api/crud/excluircliente?id=2",
  "method": "DELETE",
  "timeout": 0,
  "headers": {
    "Content-Type": "application/json"
  },
};

$.ajax(settings).done(function (response) {
  console.log(response);
});