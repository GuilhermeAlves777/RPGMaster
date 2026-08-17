using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Model
{
    public class Classe_Atributo
    {
        public long Id_Classe { get; set; }
        public Classe Classe { get; set; }
        public long Id_Atributo { get; set; }
        public Atributo Atributo { get; set; }
        public int Modificador {  get; set; }

    }
}
