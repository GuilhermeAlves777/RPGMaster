using System.Text.Json.Serialization;

namespace RPGMaster.Model
{
    public class Campanha
    {
        public long ID_Campanha { get; set; }
        public string Nome { get; set; }

        public long ID_Criador { get; set; }

        [JsonIgnore]
        public Usuario Criador { get; set; }

        public ICollection<Personagem> Personagens { get; set; } = new List<Personagem>();
        public ICollection<Campanha_Jogador> CampanhaJogadores { get; set; } = new List<Campanha_Jogador>();
    }
}