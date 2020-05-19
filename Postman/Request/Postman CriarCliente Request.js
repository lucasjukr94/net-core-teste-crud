var settings = {
  "url": "https://localhost:44384/api/crud/criarcliente",
  "method": "POST",
  "timeout": 0,
  "headers": {
    "Content-Type": ["application/json", "text/plain"]
  },
  "data": "{\n    \"CLIENTES\": {\n        \"NOME\": \"José\",\n        \"STATUS\": 1,\n        \"DT_NASCIMENTO\": \"2020-05-18\"\n    },\n    \"CLIENTE_ENDERECOS\": [{\n        \"LOGRADOURO\": \"Avenida Paulista\",\n        \"CEP\": \"11111000\",\n        \"UF\": \"SP\",\n        \"CIDADE\": \"São Paulo\",\n        \"BAIRRO\": \"Paulista\",\n        \"STATUS\": 1\n    },\n    {\n        \"LOGRADOURO\": \"Avenida Paulista\",\n        \"CEP\": \"11111000\",\n        \"UF\": \"SP\",\n        \"CIDADE\": \"São Paulo\",\n        \"BAIRRO\": \"Paulista\",\n        \"STATUS\": 1\n    }]\n}",
};

$.ajax(settings).done(function (response) {
  console.log(response);
});