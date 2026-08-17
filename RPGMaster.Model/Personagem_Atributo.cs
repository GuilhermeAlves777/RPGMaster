using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Model
{
    public class Personagem_Atributo
    {
        public long Id_Personagem { get; set; }
        public Personagem Personagem { get; set; }

        public long Id_Atributo { get; set; }
        public Atributo Atributo { get; set; }

        public int Valor { get; set; }
    }
}
