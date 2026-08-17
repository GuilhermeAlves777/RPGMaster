using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Model
{
    public class Raca
    {
        public long Id_Raca { get; set; }
        public long Id_Campanha { get; set; }
        public Campanha Campanha { get; set; }
        public string Nome {  get; set; }
        public string Descricao {  get; set; }
        public ICollection<Raca_Atributo> RacaAtributos { get; set; }
    }
}
