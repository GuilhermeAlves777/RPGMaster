namespace RPGMaster.Model
{
    public class Campanha
    {
        public long ID_Campanha { get; set; }
        public string Nome { get; set; }

        public long ID_Criador { get; set; }
        public Usuario Criador { get; set; }              

        public ICollection<Personagem> Personagens { get; set; }
        public ICollection<Campanha_Jogador> CampanhaJogadores { get; set; }
    }
}