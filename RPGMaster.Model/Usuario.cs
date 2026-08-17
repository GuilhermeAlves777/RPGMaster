namespace RPGMaster.Model
{
    public class Usuario
    {
        public long Id_Usuario { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string email { get; set; }
        public string user { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public string senha_hash { get; set; }
        public string Imagem { get; set; }

        public ICollection<Campanha> CampanhasCriadas { get; set; }   

    }
}
