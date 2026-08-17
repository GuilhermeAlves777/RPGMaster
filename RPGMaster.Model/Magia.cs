using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RPGMaster.Model
{
    public class Magia
    {
        public long Id_Magia { get; set; }
        public long Id_Campanha { get; set; }
        public Campanha Campanha { get; set; }
        public string Nome { get; set; }
        public string Dados { get; set; }
        public string Imagem { get; set; }
    }
}
