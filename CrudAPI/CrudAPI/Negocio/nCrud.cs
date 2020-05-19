using CrudAPI.Dados;
using CrudAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudAPI.Negocio
{
    public class nCrud
    {
        public List<vCLIENTES> ListarClientes()
        {
            List<vCLIENTES> res = new List<vCLIENTES>();

            dCrud dados = new dCrud();
            res = dados.ListarClientes();

            return res;
        }

        public vCLIENTES Cliente(int id)
        {
            vCLIENTES res = new vCLIENTES();

            dCrud dados = new dCrud();
            res = dados.Cliente(id);

            return res;
        }

        public vCLIENTES CriarCliente(vCLIENTES cliente)
        {
            vCLIENTES res = new vCLIENTES();

            dCrud dados = new dCrud();
            res = dados.CriarCliente(cliente);

            return res;
        }

        public int AlterarCliente(List<vCLIENTES> clientesAlterar)
        {
            List<vCLIENTES> clientesExistentes = ListarClientes();

            foreach (vCLIENTES c in clientesAlterar)
            {
                if (clientesExistentes.Find(x => x.CLIENTES.ID == c.CLIENTES.ID) == null)//não existe cliente na base de dados, então insere cliente novo
                {
                    CriarCliente(c);
                }else//existe cliente, então atualiza cliente existente
                {
                    dCrud dados = new dCrud();
                    dados.AlterarCliente(c);
                }
            }

            foreach(vCLIENTES c in clientesExistentes)
            {
                if(clientesAlterar.Find(x => x.CLIENTES.ID == c.CLIENTES.ID) == null)//se não existir na lista de clientes a alterar, então exclui cliente existente do banco de dados
                {
                    ExcluirCliente(c.CLIENTES.ID);
                }
            }

            return 1;
        }

        public int ExcluirCliente(int id)
        {
            dCrud dados = new dCrud();
            dados.ExcluirCliente(id);

            return 1;
        }
    }
}
