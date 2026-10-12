using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Model.DTOs
{
    public class PersonagemItemDto
    {
        public long Id_Item { get; set; }
        public string Nome { get; set; }
        public string? Imagem { get; set; }
        public int Quantidade { get; set; }
    }
}
