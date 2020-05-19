using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudAPI.Model
{
    public class CLIENTES
    {
        public int ID { get; set; }
        public string NOME { get; set; }
        public DateTime DT_NASCIMENTO { get; set; }
        public short STATUS { get; set; }
        public DateTime DAT_INCLUSAO { get; set; }
    }
}
