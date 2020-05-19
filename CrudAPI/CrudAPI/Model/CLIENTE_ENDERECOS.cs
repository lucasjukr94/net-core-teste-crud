using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudAPI.Model
{
    public class CLIENTE_ENDERECOS
    {
        public int ID { get; set; }
        public int ID_CLIENTE { get; set; }
        public string LOGRADOURO { get; set; }
        public string CEP { get; set; }
        public string UF { get; set; }
        public string CIDADE { get; set; }
        public string BAIRRO { get; set; }
        public short STATUS { get; set; }
        public DateTime DAT_INCLUSAO { get; set; }
    }
}
