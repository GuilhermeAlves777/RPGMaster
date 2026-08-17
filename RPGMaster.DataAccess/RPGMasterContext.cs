using Microsoft.EntityFrameworkCore;
using RPGMaster.Model;
using RPGMaster.DataAccess.Maps;

namespace RPGMaster.DataAccess
{
    public class RPGMasterContext: DbContext
    {
        public RPGMasterContext(DbContextOptions<RPGMasterContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Campanha> Campanhas { get; set; }
        public DbSet<Campanha_Jogador> CampanhaJogadores { get; set; }
        public DbSet<Personagem> Personagens { get; set; }
        public DbSet<Atributo> Atributos { get; set; }
        public DbSet<Pericia> Pericias { get; set; }
        public DbSet<Magia> Magias { get; set; }
        public DbSet<Item> Itens { get; set; }
        public DbSet<Raca> Racas { get; set; }
        public DbSet<Classe> Classes { get; set; }
        public DbSet<Arma_Detalhe> ArmasDetalhe { get; set; }
        public DbSet<Armadura_Detalhe> ArmadurasDetalhe { get; set; }
        public DbSet<Raca_Atributo> RacaAtributos { get; set; }
        public DbSet<Classe_Atributo> ClasseAtributos { get; set; }
        public DbSet<Item_Atributo> ItemAtributos { get; set; }
        public DbSet<Personagem_Atributo> PersonagemAtributos { get; set; }
        public DbSet<Personagem_Pericia> PersonagemPericias { get; set; }
        public DbSet<Personagem_Magia> PersonagemMagias { get; set; }
        public DbSet<Personagem_Item> PersonagemItens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new CampanhaMap());
            modelBuilder.ApplyConfiguration(new Campanha_JogadorMap());
            modelBuilder.ApplyConfiguration(new PersonagemMap());
            modelBuilder.ApplyConfiguration(new AtributoMap());
            modelBuilder.ApplyConfiguration(new PericiaMap());
            modelBuilder.ApplyConfiguration(new MagiaMap());
            modelBuilder.ApplyConfiguration(new ItemMap());
            modelBuilder.ApplyConfiguration(new RacaMap());
            modelBuilder.ApplyConfiguration(new ClasseMap());
            modelBuilder.ApplyConfiguration(new ArmaDetalheMap());
            modelBuilder.ApplyConfiguration(new ArmaduraDetalheMap());
            modelBuilder.ApplyConfiguration(new RacaAtributoMap());
            modelBuilder.ApplyConfiguration(new ClasseAtributoMap());
            modelBuilder.ApplyConfiguration(new ItemAtributoMap());
            modelBuilder.ApplyConfiguration(new PersonagemAtributoMap());
            modelBuilder.ApplyConfiguration(new PersonagemPericiaMap());
            modelBuilder.ApplyConfiguration(new PersonagemMagiaMap());
            modelBuilder.ApplyConfiguration(new PersonagemItemMap());
        }
    }
}
