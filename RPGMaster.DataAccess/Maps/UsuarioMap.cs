using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RPGMaster.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.DataAccess.Maps
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {

            builder.ToTable("USUARIO");

            builder.HasKey(x => x.Id_Usuario);

            builder.Property(x => x.Id_Usuario).HasColumnName("ID_USUARIO")
                                       .ValueGeneratedOnAdd()
                                       .IsRequired();
            builder.Property(x => x.user).HasColumnName("USER").IsRequired();
            builder.Property(x => x.senha_hash).HasColumnName("SENHA").IsRequired();
            builder.Property(x => x.Nome).HasColumnName("NOME").IsRequired();
            builder.Property(x => x.Sobrenome).HasColumnName("SOBRENOME").IsRequired();
            builder.Property(x => x.CPF).HasColumnName("CPF").IsRequired();
            builder.Property(x => x.email).HasColumnName("EMAIL").IsRequired();
            builder.Property(x => x.DataNascimento).HasColumnName("DATA_NASC").IsRequired();
            builder.Property(x => x.Imagem).HasColumnName("IMAGEM");
        }
    }
}
