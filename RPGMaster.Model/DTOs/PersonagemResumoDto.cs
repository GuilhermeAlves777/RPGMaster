using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Model.DTOs
{
    public class PersonagemResumoDto
    {
        public long Id_Personagem { get; set; }
        public string Nome { get; set; }
        public int Nivel { get; set; }
        public bool EhNpc { get; set; }
        public int vidaMaxima { get; set; }
        public int vidaAtual { get; set; }
        public int manaMaxima { get; set; }
        public int manaAtual { get; set; }
    }
}
