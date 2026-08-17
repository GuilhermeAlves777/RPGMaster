using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Model
{
    public class Personagem_Pericia
    {
        public long Id_Personagem { get; set; }
        public Personagem Personagem { get; set; }

        public long Id_Pericia { get; set; }
        public Pericia Pericia { get; set; }

        public int Valor { get; set; }
    }
}
