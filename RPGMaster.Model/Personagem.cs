using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.Model
{
    public class Personagem
    {
        public long Id_Personagem { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }

        public long Id_Campanha { get; set; }
        public Campanha Campanha { get; set; }

        public long Id_Jogador { get; set; }
        public Usuario Jogador { get; set; }

        public long Id_Classe { get; set; }
        public Classe Classe { get; set; }

        public long Id_Raca { get; set; }
        public Raca Raca { get; set; }

        public ICollection<Personagem_Atributo> Atributos { get; set; }
        public ICollection<Personagem_Pericia> Pericias { get; set; }
        public ICollection<Personagem_Magia> Magias { get; set; }
        public ICollection<Personagem_Item> Itens { get; set; }
    }
}