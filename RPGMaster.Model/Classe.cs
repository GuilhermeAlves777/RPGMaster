using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Model
{
    public class Classe
    {
        public long Id_Classe { get; set; }
        public long Id_Campanha { get; set; }
        public Campanha Campanha { get; set; }
        public string Nome {  get; set; }
        public string Descricao { get; set; }

        public ICollection<Classe_Atributo> ClasseAtributos { get; set; }
    }
}
