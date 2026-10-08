using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Model.DTOs
{
    public class ArmaDetalheDto
    {
        public long Id_Item { get; set; }
        public string Dano { get; set; }
        public string Tipo_Dano { get; set; }
        public int Alcance { get; set; }
    }
}
