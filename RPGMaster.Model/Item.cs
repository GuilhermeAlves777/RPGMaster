using RPGMaster.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Model
{
    public class Item
    {
        public long Id_Item { get; set; }
        public long Id_Campanha { get; set; }
        public Campanha Campanha { get; set; }
        public string Nome { get; set; }
        public TiposEnum Tipo { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }

        public Arma_Detalhe ArmaDetalhe { get; set; }        
        public Armadura_Detalhe ArmaduraDetalhe { get; set; }
        public ICollection<Item_Atributo> ItemAtributos { get; set; }
    }
}
