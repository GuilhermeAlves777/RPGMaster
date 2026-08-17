using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RPGMaster.Model;

public class Campanha_JogadorMap : IEntityTypeConfiguration<Campanha_Jogador>
{
    public void Configure(EntityTypeBuilder<Campanha_Jogador> builder)
    {
        builder.ToTable("CAMPANHA_JOGADOR");

        // chave composta - EF Core não infere sozinho, precisa declarar as duas
        builder.HasKey(x => new { x.Id_Campanha, x.Id_Usuario });

        builder.Property(x => x.Id_Campanha).HasColumnName("ID_CAMPANHA").IsRequired();
        builder.Property(x => x.Id_Usuario).HasColumnName("ID_USUARIO").IsRequired();

        builder.HasOne(x => x.Campanha)
               .WithMany(x => x.CampanhaJogadores)
               .HasForeignKey(x => x.Id_Campanha)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Usuario)
               .WithMany()
               .HasForeignKey(x => x.Id_Usuario)
               .OnDelete(DeleteBehavior.Restrict);
    }
}