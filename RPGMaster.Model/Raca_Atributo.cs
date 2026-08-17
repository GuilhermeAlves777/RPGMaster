using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Model
{
    public class Raca_Atributo
    {
        public long Id_Raca { get; set; }
        public Raca Raca {  get; set; }
        public long Id_Atributo { get; set; }
        public Atributo Atributo { get; set; }
        public int Modificador { get; set; }

    }
}
