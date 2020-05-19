using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudAPI.Model
{
    public class vCLIENTES
    {
        public CLIENTES CLIENTES { get; set; }
        public List<CLIENTE_ENDERECOS> CLIENTE_ENDERECOS { get; set; }
    }
}
