using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Model
{
    public class Convite
    {
        public long Id_Convite { get; set; }
        public long Id_Campanha { get; set; }
        public Campanha Campanha { get; set; }
        public string Codigo { get; set; }
        public bool Ativo { get; set; }
        public DateTime CriadoEm { get; set; }
    }
}
