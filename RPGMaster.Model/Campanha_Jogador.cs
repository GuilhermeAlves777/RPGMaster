using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Model
{
    public class Campanha_Jogador
    {
        public long Id_Campanha { get; set; }
        public Campanha Campanha { get; set; }
        public long Id_Usuario { get; set; }
        public Usuario Usuario { get; set; }
    }
}
