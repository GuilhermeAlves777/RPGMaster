using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Model.DTOs
{
    public class AtributoDto
    {
        public long Id_Atributo { get; set; }
        public string Nome { get; set; }
        public int Valor_Padrao { get; set; }
        public long Id_Campanha { get; set; }
    }
}
