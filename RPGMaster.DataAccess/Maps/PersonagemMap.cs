using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RPGMaster.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.DataAccess.Maps
{
    public class PersonagemMap : IEntityTypeConfiguration<Personagem>
    {
        public void Configure(EntityTypeBuilder<Personagem> builder)
        {
            builder.ToTable("PERSONAGEM");

            builder.HasKey(x => x.Id_Personagem);

            builder.Property(x => x.Id_Personagem).HasColumnName("ID_PERSONAGEM")
                                       .ValueGeneratedOnAdd()
                                       .IsRequired();
            builder.Property(x => x.Nome).HasColumnName("NOME").IsRequired();
            builder.Property(x => x.Id_Campanha).HasColumnName("ID_CAMPANHA").IsRequired();
            builder.Property(x => x.Id_Jogador).HasColumnName("ID_JOGADOR").IsRequired();
            builder.Property(x => x.Id_Raca).HasColumnName("ID_RACA").IsRequired();
            builder.Property(x => x.Id_Classe).HasColumnName("ID_CLASSE").IsRequired();

            builder.HasOne(x => x.Campanha)
                   .WithMany(x => x.Personagens)
                   .HasForeignKey(x => x.Id_Campanha)
                   .OnDelete(DeleteBehavior.Cascade); // se apagar campanha, apaga os personagens dela

            builder.HasOne(x => x.Jogador)
                   .WithMany()
                   .HasForeignKey(x => x.Id_Jogador)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Raca)
                   .WithMany()
                   .HasForeignKey(x => x.Id_Raca)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Classe)
                   .WithMany()
                   .HasForeignKey(x => x.Id_Classe)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
