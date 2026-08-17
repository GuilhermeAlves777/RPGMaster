using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Model
{
    public class Personagem_Item
    {
        public long Id_Personagem { get; set; }
        public Personagem Personagem { get; set; }

        public long Id_Item { get; set; }
        public Item Item { get; set; }

        public int Quantidade { get; set; }
    }
}