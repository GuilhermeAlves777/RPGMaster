using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Model.DTOs
{
    public class MagiaDto
    {
        public long Id_Magia { get; set; }
        public string Nome { get; set; }
        public string Dados { get; set; }
        public long Id_Campanha { get; set; }
    }
}
