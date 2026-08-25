using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Model.DTOs
{
    public class CampanhaJogadorDto
    {
        public long Id_Usuario { get; set; }
        public string NomeUsuario { get; set; }
        public long Id_Campanha { get; set; }
        public string NomeCampanha { get; set; }
    }
}
