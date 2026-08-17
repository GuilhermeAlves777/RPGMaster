using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RPGMaster.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.DataAccess.Maps
{
    public class AtributoMap : IEntityTypeConfiguration<Atributo>
    {
        public void Configure(EntityTypeBuilder<Atributo> builder)
        {
            builder.ToTable("ATRIBUTO");
            builder.HasKey(x => x.Id_Atributo);

            builder.Property(x => x.Id_Atributo).HasColumnName("ID_ATRIBUTO").ValueGeneratedOnAdd().IsRequired();
            builder.Property(x => x.Id_Campanha).HasColumnName("ID_CAMPANHA").IsRequired();
            builder.Property(x => x.Nome).HasColumnName("NOME").IsRequired();
            builder.Property(x => x.Valor_Padrao).HasColumnName("VALOR_PADRAO").IsRequired();

            builder.HasOne(x => x.Campanha).WithMany()
                   .HasForeignKey(x => x.Id_Campanha).OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class PericiaMap : IEntityTypeConfiguration<Pericia>
    {
        public void Configure(EntityTypeBuilder<Pericia> builder)
        {
            builder.ToTable("PERICIA");
            builder.HasKey(x => x.Id_Pericia);

            builder.Property(x => x.Id_Pericia).HasColumnName("ID_PERICIA").ValueGeneratedOnAdd().IsRequired();
            builder.Property(x => x.Id_Campanha).HasColumnName("ID_CAMPANHA").IsRequired();
            builder.Property(x => x.Nome).HasColumnName("NOME").IsRequired();
            builder.Property(x => x.Valor_Padrao).HasColumnName("VALOR_PADRAO").IsRequired();

            builder.HasOne(x => x.Campanha).WithMany()
                   .HasForeignKey(x => x.Id_Campanha).OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class MagiaMap : IEntityTypeConfiguration<Magia>
    {
        public void Configure(EntityTypeBuilder<Magia> builder)
        {
            builder.ToTable("MAGIA");
            builder.HasKey(x => x.Id_Magia);

            builder.Property(x => x.Id_Magia).HasColumnName("ID_MAGIA").ValueGeneratedOnAdd().IsRequired();
            builder.Property(x => x.Id_Campanha).HasColumnName("ID_CAMPANHA").IsRequired();
            builder.Property(x => x.Nome).HasColumnName("NOME").IsRequired();
            builder.Property(x => x.Dados).HasColumnName("DADOS").IsRequired();

            builder.HasOne(x => x.Campanha).WithMany()
                   .HasForeignKey(x => x.Id_Campanha).OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class ItemMap : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("ITEM");
            builder.HasKey(x => x.Id_Item);

            builder.Property(x => x.Id_Item).HasColumnName("ID_ITEM").ValueGeneratedOnAdd().IsRequired();
            builder.Property(x => x.Id_Campanha).HasColumnName("ID_CAMPANHA").IsRequired();
            builder.Property(x => x.Nome).HasColumnName("NOME").IsRequired();
            builder.Property(x => x.Tipo).HasColumnName("TIPO").IsRequired();
            builder.Property(x => x.Descricao).HasColumnName("DESCRICAO");
            builder.Property(x => x.Imagem).HasColumnName("IMAGEM");

            builder.HasOne(x => x.Campanha).WithMany()
                   .HasForeignKey(x => x.Id_Campanha).OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class RacaMap : IEntityTypeConfiguration<Raca>
    {
        public void Configure(EntityTypeBuilder<Raca> builder)
        {
            builder.ToTable("RACA");
            builder.HasKey(x => x.Id_Raca);

            builder.Property(x => x.Id_Raca).HasColumnName("ID_RACA").ValueGeneratedOnAdd().IsRequired();
            builder.Property(x => x.Id_Campanha).HasColumnName("ID_CAMPANHA").IsRequired();
            builder.Property(x => x.Nome).HasColumnName("NOME").IsRequired();
            builder.Property(x => x.Descricao).HasColumnName("DESCRICAO");

            builder.HasOne(x => x.Campanha).WithMany()
                   .HasForeignKey(x => x.Id_Campanha).OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class ClasseMap : IEntityTypeConfiguration<Classe>
    {
        public void Configure(EntityTypeBuilder<Classe> builder)
        {
            builder.ToTable("CLASSE");
            builder.HasKey(x => x.Id_Classe);

            builder.Property(x => x.Id_Classe).HasColumnName("ID_CLASSE").ValueGeneratedOnAdd().IsRequired();
            builder.Property(x => x.Id_Campanha).HasColumnName("ID_CAMPANHA").IsRequired();
            builder.Property(x => x.Nome).HasColumnName("NOME").IsRequired();
            builder.Property(x => x.Descricao).HasColumnName("DESCRICAO");

            builder.HasOne(x => x.Campanha).WithMany()
                   .HasForeignKey(x => x.Id_Campanha).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
