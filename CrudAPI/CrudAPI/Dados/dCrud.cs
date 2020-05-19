using CrudAPI.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CrudAPI.Dados
{
    public class dCrud
    {
        private readonly string connectionString = "Server=(localdb)\\mssqllocaldb;Database=PBS_TESTE;Trusted_Connection=True";

        public List<vCLIENTES> ListarClientes()
        {
            List<vCLIENTES> res = new List<vCLIENTES>();
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand sqlComm = new SqlCommand("spListarClientes", conn))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = sqlComm.ExecuteReader();
                    res = ReadListClientes(reader);
                }
                conn.Close();
            }
            
            return res;
        }

        public vCLIENTES Cliente(int id)
        {
            vCLIENTES res = new vCLIENTES();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand sqlComm = new SqlCommand("spCliente", conn))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    SqlDataReader reader = sqlComm.ExecuteReader();
                    res = ReadCliente(reader);
                }
                conn.Close();
            }

            return res;
        }

        public vCLIENTES CriarCliente(vCLIENTES cliente)
        {
            vCLIENTES res = new vCLIENTES();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand sqlComm = new SqlCommand("spCriarCliente", conn))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.Add("@NOME", SqlDbType.VarChar).Value = cliente.CLIENTES.NOME;
                    sqlComm.Parameters.Add("@DT_NASCIMENTO", SqlDbType.DateTime).Value = cliente.CLIENTES.DT_NASCIMENTO;
                    sqlComm.Parameters.Add("@STATUS", SqlDbType.TinyInt).Value = cliente.CLIENTES.STATUS;
                    sqlComm.Parameters.Add("@JSON_ENDERECOS", SqlDbType.VarChar).Value = JsonConvert.SerializeObject(cliente.CLIENTE_ENDERECOS);

                    SqlDataReader reader = sqlComm.ExecuteReader();
                    res = ReadCliente(reader);
                }
                conn.Close();
            }

            return res;
        }

        public int AlterarCliente(vCLIENTES cliente)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand sqlComm = new SqlCommand("spAlterarCliente", conn))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.Add("@ID", SqlDbType.Int).Value = cliente.CLIENTES.ID;
                    if (!string.IsNullOrWhiteSpace(cliente.CLIENTES.NOME))
                    {
                        sqlComm.Parameters.Add("@NOME", SqlDbType.VarChar).Value = cliente.CLIENTES.NOME;
                    }
                    if (cliente.CLIENTES.DT_NASCIMENTO != null && cliente.CLIENTES.DT_NASCIMENTO != DateTime.MinValue)
                    {
                        sqlComm.Parameters.Add("@DT_NASCIMENTO", SqlDbType.DateTime).Value = cliente.CLIENTES.DT_NASCIMENTO;
                    }
                    if (cliente.CLIENTES.STATUS != 0)
                    {
                        sqlComm.Parameters.Add("@STATUS", SqlDbType.TinyInt).Value = cliente.CLIENTES.STATUS;
                    }
                    if (cliente.CLIENTE_ENDERECOS.Count != 0)
                    {
                        sqlComm.Parameters.Add("@JSON_ENDERECOS", SqlDbType.VarChar).Value = JsonConvert.SerializeObject(cliente.CLIENTE_ENDERECOS);
                    }
                    sqlComm.ExecuteNonQuery();
                }
                conn.Close();
            }

            return 1;
        }

        public int ExcluirCliente(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand sqlComm = new SqlCommand("spExcluirCliente", conn))
                {
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    sqlComm.ExecuteNonQuery();
                }
                conn.Close();
            }

            return 1;
        }

        public List<vCLIENTES> ReadListClientes(SqlDataReader reader)
        {
            List<vCLIENTES> res = new List<vCLIENTES>();

            List<CLIENTES> CLIENTES = new List<CLIENTES>();
            while (reader.Read())
            {
                CLIENTES CLIENTE = new CLIENTES();

                CLIENTE.ID = int.Parse(reader["ID"].ToString());
                CLIENTE.NOME = reader["NOME"].ToString();
                CLIENTE.DT_NASCIMENTO = DateTime.Parse(reader["DT_NASCIMENTO"].ToString());
                CLIENTE.STATUS = short.Parse(reader["STATUS"].ToString());
                CLIENTE.DAT_INCLUSAO = DateTime.Parse(reader["DAT_INCLUSAO"].ToString());

                CLIENTES.Add(CLIENTE);
            }

            reader.NextResult();

            List<CLIENTE_ENDERECOS> CLIENTE_ENDERECOS = new List<CLIENTE_ENDERECOS>();
            while (reader.Read())
            {
                CLIENTE_ENDERECOS ENDERECO = new CLIENTE_ENDERECOS();
                ENDERECO.ID = int.Parse(reader["ID"].ToString());
                ENDERECO.ID_CLIENTE = int.Parse(reader["ID_CLIENTE"].ToString());
                ENDERECO.LOGRADOURO = reader["LOGRADOURO"].ToString();
                ENDERECO.CEP = reader["CEP"].ToString();
                ENDERECO.UF = reader["UF"].ToString();
                ENDERECO.CIDADE = reader["CIDADE"].ToString();
                ENDERECO.BAIRRO = reader["BAIRRO"].ToString();
                ENDERECO.STATUS = short.Parse(reader["STATUS"].ToString());
                ENDERECO.DAT_INCLUSAO = DateTime.Parse(reader["DAT_INCLUSAO"].ToString());

                CLIENTE_ENDERECOS.Add(ENDERECO);
            }

            foreach (CLIENTES c in CLIENTES)
            {
                vCLIENTES v = new vCLIENTES();
                v.CLIENTES = c;
                v.CLIENTE_ENDERECOS = CLIENTE_ENDERECOS.FindAll(x => x.ID_CLIENTE == c.ID);

                res.Add(v);
            }

            return res;
        }

        public vCLIENTES ReadCliente(SqlDataReader reader)
        {
            vCLIENTES res = new vCLIENTES();

            res.CLIENTES = new CLIENTES();
            while (reader.Read())
            {
                res.CLIENTES.ID = int.Parse(reader["ID"].ToString());
                res.CLIENTES.NOME = reader["NOME"].ToString();
                res.CLIENTES.DT_NASCIMENTO = DateTime.Parse(reader["DT_NASCIMENTO"].ToString());
                res.CLIENTES.STATUS = short.Parse(reader["STATUS"].ToString());
                res.CLIENTES.DAT_INCLUSAO = DateTime.Parse(reader["DAT_INCLUSAO"].ToString());
            }

            reader.NextResult();

            res.CLIENTE_ENDERECOS = new List<CLIENTE_ENDERECOS>();
            while (reader.Read())
            {
                CLIENTE_ENDERECOS ENDERECO = new CLIENTE_ENDERECOS();
                ENDERECO.ID = int.Parse(reader["ID"].ToString());
                ENDERECO.ID_CLIENTE = int.Parse(reader["ID_CLIENTE"].ToString());
                ENDERECO.LOGRADOURO = reader["LOGRADOURO"].ToString();
                ENDERECO.CEP = reader["CEP"].ToString();
                ENDERECO.UF = reader["UF"].ToString();
                ENDERECO.CIDADE = reader["CIDADE"].ToString();
                ENDERECO.BAIRRO = reader["BAIRRO"].ToString();
                ENDERECO.STATUS = short.Parse(reader["STATUS"].ToString());
                ENDERECO.DAT_INCLUSAO = DateTime.Parse(reader["DAT_INCLUSAO"].ToString());

                res.CLIENTE_ENDERECOS.Add(ENDERECO);
            }

            return res;
        }
    }
}
