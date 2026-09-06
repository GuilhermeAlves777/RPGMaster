using RPGMaster.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Model.DTOs
{
    public class ItemDto
    {
        public long Id_Item { get; set; }
        public string Nome { get; set; }
        public TiposEnum Tipo { get; set; }
        public string? Descricao { get; set; }
        public string? Imagem { get; set; }
        public long Id_Campanha { get; set; }
    }
}
