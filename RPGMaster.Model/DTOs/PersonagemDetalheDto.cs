using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Model.DTOs
{
    public class PersonagemDetalheDto
    {
        public long Id_Personagem { get; set; }
        public long Id_Campanha { get; set; }
        public long? Id_Jogador { get; set; }
        public string Nome { get; set; }
        public string? NomeJogador { get; set; }
        public int Nivel { get; set; }
        public bool EhNpc { get; set; }
        public int vidaMaxima { get; set; }
        public int vidaAtual { get; set; }
        public int manaMaxima { get; set; }
        public int manaAtual { get; set; }
        public string? Imagem { get; set; }
        public List<PersonagemAtributoDto> Atributos { get; set; }
        public List<PersonagemPericiaDto> Pericias { get; set; }
        public List<PersonagemMagiaDto> Magias { get; set; }
        public Classe Classe { get; set; }
        public List<PersonagemItemDto> Itens { get; set; }
    }
}
