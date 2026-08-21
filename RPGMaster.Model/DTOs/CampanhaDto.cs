using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Model.DTOs
{
    public class CampanhaDto
    {
        public long Id_Campanha { get; set; }
        public string Nome { get; set; }
        public long Id_Criador { get; set; }
        public List<PersonagemResumoDto> Personagens { get; set; }
        public List<JogadorResumoDto> Jogadores { get; set; }
    }
}
