using RPGMaster.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Model
{
    public class Arma_Detalhe
    {
        public long Id_Item {  get; set; }
        public Item Item { get; set; }
        public string Dano {  get; set; }
        public TipoDanoEnum Tipo_Dano { get; set; } 
        public int Alcance { get; set; }
    }
}
