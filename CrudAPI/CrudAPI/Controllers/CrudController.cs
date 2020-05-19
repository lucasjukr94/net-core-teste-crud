using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudAPI.Model;
using CrudAPI.Negocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CrudAPI.Controllers
{
    [ApiController]
    public class CrudController : ControllerBase
    {
        [Route("api/[controller]/listarclientes")]
        [HttpGet]
        public Response ListarClientes()
        {
            Response res = new Response();

            try
            {
                nCrud negocio = new nCrud();

                res.response = negocio.ListarClientes();
                res.status = "Sucesso";
                res.statusCode = 200;
            }
            catch (Exception ex)
            {
                res.response = null;
                res.status = "Erro";
                res.statusCode = 500;
            }

            return res;
        }
        
        [Route("api/[controller]/cliente")]
        [HttpGet]
        public Response Cliente([FromQuery(Name = "id")]int id)
        {
            Response res = new Response();

            try
            {
                if (id == 0)
                {
                    res.response = null;
                    res.status = "Parâmetro inválido, campo id";
                    res.statusCode = 400;

                    return res;
                }

                nCrud negocio = new nCrud();

                res.response = negocio.Cliente(id);
                res.status = "Sucesso";
                res.statusCode = 200;
            }
            catch (Exception ex)
            {
                res.response = null;
                res.status = "Erro";
                res.statusCode = 500;
            }

            return res;
        }
        
        [Route("api/[controller]/criarcliente")]
        [HttpPost]
        public Response CriarCliente([FromBody] JObject param)
        {
            Response res = new Response();

            try
            {
                #region parâmetros
                dynamic req = param;

                nCrud negocio = new nCrud();

                vCLIENTES cliente = new vCLIENTES();

                cliente.CLIENTES = new CLIENTES();
                cliente.CLIENTES.NOME = req.CLIENTES.NOME;
                if (string.IsNullOrWhiteSpace(cliente.CLIENTES.NOME))
                {
                    res.response = null;
                    res.status = "Parâmetro inválido, campo NOME";
                    res.statusCode = 400;

                    return res;
                }
                cliente.CLIENTES.STATUS = (short)req.CLIENTES.STATUS;
                if (cliente.CLIENTES.STATUS == 0)
                {
                    res.response = null;
                    res.status = "Parâmetro inválido, campo STATUS";
                    res.statusCode = 400;

                    return res;
                }
                cliente.CLIENTES.DT_NASCIMENTO = (DateTime)req.CLIENTES.DT_NASCIMENTO;
                if (cliente.CLIENTES.DT_NASCIMENTO == null || cliente.CLIENTES.DT_NASCIMENTO == DateTime.MinValue)
                {
                    res.response = null;
                    res.status = "Parâmetro inválido, campo DT_NASCIMENTO";
                    res.statusCode = 400;

                    return res;
                }

                cliente.CLIENTE_ENDERECOS = new List<CLIENTE_ENDERECOS>();
                if(req.CLIENTE_ENDERECOS.Count == 0)
                {
                    res.response = null;
                    res.status = "Parâmetro inválido, campo CLIENTE_ENDERECOS";
                    res.statusCode = 400;

                    return res;
                }
                for(int i = 0; i < req.CLIENTE_ENDERECOS.Count; i++)
                {
                    dynamic endereco = req.CLIENTE_ENDERECOS[i];

                    CLIENTE_ENDERECOS end = new CLIENTE_ENDERECOS();
                    end.LOGRADOURO = endereco.LOGRADOURO;
                    if (string.IsNullOrWhiteSpace(end.LOGRADOURO))
                    {
                        res.response = null;
                        res.status = "Parâmetro inválido, campo LOGRADOURO";
                        res.statusCode = 400;

                        return res;
                    }
                    end.CEP = endereco.CEP;
                    if (string.IsNullOrWhiteSpace(end.CEP))
                    {
                        res.response = null;
                        res.status = "Parâmetro inválido, campo CEP";
                        res.statusCode = 400;

                        return res;
                    }
                    end.UF = endereco.UF;
                    if (string.IsNullOrWhiteSpace(end.UF))
                    {
                        res.response = null;
                        res.status = "Parâmetro inválido, campo UF";
                        res.statusCode = 400;

                        return res;
                    }
                    end.CIDADE = endereco.CIDADE;
                    if (string.IsNullOrWhiteSpace(end.CIDADE))
                    {
                        res.response = null;
                        res.status = "Parâmetro inválido, campo CIDADE";
                        res.statusCode = 400;

                        return res;
                    }
                    end.BAIRRO = endereco.BAIRRO;
                    if (string.IsNullOrWhiteSpace(end.BAIRRO))
                    {
                        res.response = null;
                        res.status = "Parâmetro inválido, campo BAIRRO";
                        res.statusCode = 400;

                        return res;
                    }
                    end.STATUS = (short)endereco.STATUS;
                    if (end.STATUS == 0)
                    {
                        res.response = null;
                        res.status = "Parâmetro inválido, campo STATUS";
                        res.statusCode = 400;

                        return res;
                    }

                    cliente.CLIENTE_ENDERECOS.Add(end);
                }
                #endregion

                res.response = negocio.CriarCliente(cliente);
                res.status = "Sucesso";
                res.statusCode = 200;
            }
            catch (Exception ex)
            {
                res.response = null;
                res.status = "Erro";
                res.statusCode = 500;
            }

            return res;
        }
        
        [Route("api/[controller]/alterarcliente")]
        [HttpPut]
        public Response AlterarCliente([FromBody] JObject param)
        {
            Response res = new Response();

            try
            {
                #region parâmetros
                dynamic req = param;

                nCrud negocio = new nCrud();

                List<vCLIENTES> clientesAlterar = new List<vCLIENTES>();

                for(int j = 0; j < req.vCLIENTES.Count; j++)
                {
                    vCLIENTES cliente = new vCLIENTES();

                    cliente.CLIENTES = new CLIENTES();
                    cliente.CLIENTES.ID = (int)req.vCLIENTES[j].CLIENTES.ID;
                    if (cliente.CLIENTES.ID == 0)
                    {
                        res.response = null;
                        res.status = "Parâmetro inválido";
                        res.statusCode = 400;

                        return res;
                    }
                    cliente.CLIENTES.NOME = req.vCLIENTES[j].CLIENTES.NOME;
                    cliente.CLIENTES.STATUS = (short)req.vCLIENTES[j].CLIENTES.STATUS;
                    cliente.CLIENTES.DT_NASCIMENTO = (DateTime)req.vCLIENTES[j].CLIENTES.DT_NASCIMENTO;

                    cliente.CLIENTE_ENDERECOS = new List<CLIENTE_ENDERECOS>();
                    for (int i = 0; i < req.vCLIENTES[j].CLIENTE_ENDERECOS.Count; i++)
                    {
                        dynamic endereco = req.vCLIENTES[j].CLIENTE_ENDERECOS[i];

                        CLIENTE_ENDERECOS end = new CLIENTE_ENDERECOS();
                        end.ID = (int)endereco.ID;
                        if (end.ID == 0)
                        {
                            res.response = null;
                            res.status = "Parâmetro inválido";
                            res.statusCode = 400;

                            return res;
                        }
                        end.LOGRADOURO = endereco.LOGRADOURO;
                        end.CEP = endereco.CEP;
                        end.UF = endereco.UF;
                        end.CIDADE = endereco.CIDADE;
                        end.BAIRRO = endereco.BAIRRO;
                        end.STATUS = (short)endereco.STATUS;

                        cliente.CLIENTE_ENDERECOS.Add(end);
                    }

                    clientesAlterar.Add(cliente);
                }

                negocio.AlterarCliente(clientesAlterar);
                #endregion

                res.response = negocio.ListarClientes();
                res.status = "Sucesso";
                res.statusCode = 200;
            }
            catch (Exception ex)
            {
                res.response = null;
                res.status = "Erro";
                res.statusCode = 500;
            }

            return res;
        }
        
        [Route("api/[controller]/excluircliente")]
        [HttpDelete("{id}")]
        public Response ExcluirCliente([FromQuery(Name = "id")]int id)
        {
            Response res = new Response();

            try
            {
                if(id == 0)
                {
                    res.response = null;
                    res.status = "Parâmetro inválido, campo id";
                    res.statusCode = 400;

                    return res;
                }

                nCrud negocio = new nCrud();

                res.response = negocio.ExcluirCliente(id);
                res.status = "Sucesso";
                res.statusCode = 200;
            }
            catch (Exception ex)
            {
                res.response = null;
                res.status = "Erro";
                res.statusCode = 500;
            }

            return res;
        }
    }
}