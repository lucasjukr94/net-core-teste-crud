var settings = {
  "url": "https://localhost:44384/api/crud/alterarcliente",
  "method": "PUT",
  "timeout": 0,
  "headers": {
    "Content-Type": ["application/json", "text/plain"]
  },
  "data": "{\n    \"vCLIENTES\": [\n        {\n            \"CLIENTES\": {\n                \"ID\": 2,\n                \"NOME\": \"Alberto\",\n                \"STATUS\": 2,\n                \"DT_NASCIMENTO\": \"2019-06-18\"\n            },\n            \"CLIENTE_ENDERECOS\": [\n                {\n                    \"ID\": 1,\n                    \"LOGRADOURO\": \"Faria Lima\",\n                    \"CEP\": \"22222000\",\n                    \"UF\": \"SP\",\n                    \"CIDADE\": \"São Paulo\",\n                    \"BAIRRO\": \"Pinheiros\",\n                    \"STATUS\": 2\n                }\n            ]\n        },\n        {\n            \"CLIENTES\": {\n                \"ID\": 7,\n                \"NOME\": \"José\",\n                \"STATUS\": 2,\n                \"DT_NASCIMENTO\": \"2019-06-18\"\n            },\n            \"CLIENTE_ENDERECOS\": [\n                {\n                    \"ID\": 8,\n                    \"LOGRADOURO\": \"Faria Lima\",\n                    \"CEP\": \"22222000\",\n                    \"UF\": \"SP\",\n                    \"CIDADE\": \"São Paulo\",\n                    \"BAIRRO\": \"Pinheiros\",\n                    \"STATUS\": 2\n                }\n            ]\n        }\n    ]\n}",
};

$.ajax(settings).done(function (response) {
  console.log(response);
});