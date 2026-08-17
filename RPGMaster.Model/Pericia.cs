using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Model
{
    public class Pericia
    {
        public long Id_Pericia { get; set; }
        public long Id_Campanha { get; set; }
        public Campanha Campanha { get; set; }
        public string Nome {  get; set; }
        public int Valor_Padrao { get; set; }
    }
}
