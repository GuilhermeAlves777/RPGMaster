using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Model
{
    public class Personagem_Magia
    {
        public long Id_Personagem { get; set; }
        public Personagem Personagem { get; set; }

        public long Id_Magia { get; set; }
        public Magia Magia { get; set; }
    }
}