using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RPGMaster.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.DataAccess.Maps
{
    public class RacaAtributoMap : IEntityTypeConfiguration<Raca_Atributo>
    {
        public void Configure(EntityTypeBuilder<Raca_Atributo> builder)
        {
            builder.ToTable("RACA_ATRIBUTO");
            builder.HasKey(x => new { x.Id_Raca, x.Id_Atributo });

            builder.Property(x => x.Id_Raca).HasColumnName("ID_RACA").IsRequired();
            builder.Property(x => x.Id_Atributo).HasColumnName("ID_ATRIBUTO").IsRequired();
            builder.Property(x => x.Modificador).HasColumnName("MODIFICADOR").IsRequired();

            builder.HasOne(x => x.Raca).WithMany(x => x.RacaAtributos)
                   .HasForeignKey(x => x.Id_Raca).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Atributo).WithMany()
                   .HasForeignKey(x => x.Id_Atributo).OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class ClasseAtributoMap : IEntityTypeConfiguration<Classe_Atributo>
    {
        public void Configure(EntityTypeBuilder<Classe_Atributo> builder)
        {
            builder.ToTable("CLASSE_ATRIBUTO");
            builder.HasKey(x => new { x.Id_Classe, x.Id_Atributo });

            builder.Property(x => x.Id_Classe).HasColumnName("ID_CLASSE").IsRequired();
            builder.Property(x => x.Id_Atributo).HasColumnName("ID_ATRIBUTO").IsRequired();
            builder.Property(x => x.Modificador).HasColumnName("MODIFICADOR").IsRequired();

            builder.HasOne(x => x.Classe).WithMany(x => x.ClasseAtributos)
                   .HasForeignKey(x => x.Id_Classe).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Atributo).WithMany()
                   .HasForeignKey(x => x.Id_Atributo).OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class ItemAtributoMap : IEntityTypeConfiguration<Item_Atributo>
    {
        public void Configure(EntityTypeBuilder<Item_Atributo> builder)
        {
            builder.ToTable("ITEM_ATRIBUTO");
            builder.HasKey(x => new { x.Id_Item, x.Id_Atributo });

            builder.Property(x => x.Id_Item).HasColumnName("ID_ITEM").IsRequired();
            builder.Property(x => x.Id_Atributo).HasColumnName("ID_ATRIBUTO").IsRequired();
            builder.Property(x => x.Modificador).HasColumnName("MODIFICADOR").IsRequired();

            builder.HasOne(x => x.Item).WithMany(x => x.ItemAtributos)
                   .HasForeignKey(x => x.Id_Item).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Atributo).WithMany()
                   .HasForeignKey(x => x.Id_Atributo).OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class PersonagemAtributoMap : IEntityTypeConfiguration<Personagem_Atributo>
    {
        public void Configure(EntityTypeBuilder<Personagem_Atributo> builder)
        {
            builder.ToTable("PERSONAGEM_ATRIBUTO");
            builder.HasKey(x => new { x.Id_Personagem, x.Id_Atributo });

            builder.Property(x => x.Id_Personagem).HasColumnName("ID_PERSONAGEM").IsRequired();
            builder.Property(x => x.Id_Atributo).HasColumnName("ID_ATRIBUTO").IsRequired();
            builder.Property(x => x.Valor).HasColumnName("VALOR").IsRequired();

            builder.HasOne(x => x.Personagem).WithMany(x => x.Atributos)
                   .HasForeignKey(x => x.Id_Personagem).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Atributo).WithMany()
                   .HasForeignKey(x => x.Id_Atributo).OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class PersonagemPericiaMap : IEntityTypeConfiguration<Personagem_Pericia>
    {
        public void Configure(EntityTypeBuilder<Personagem_Pericia> builder)
        {
            builder.ToTable("PERSONAGEM_PERICIA");
            builder.HasKey(x => new { x.Id_Personagem, x.Id_Pericia });

            builder.Property(x => x.Id_Personagem).HasColumnName("ID_PERSONAGEM").IsRequired();
            builder.Property(x => x.Id_Pericia).HasColumnName("ID_PERICIA").IsRequired();
            builder.Property(x => x.Valor).HasColumnName("VALOR").IsRequired();

            builder.HasOne(x => x.Personagem).WithMany(x => x.Pericias)
                   .HasForeignKey(x => x.Id_Personagem).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Pericia).WithMany()
                   .HasForeignKey(x => x.Id_Pericia).OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class PersonagemMagiaMap : IEntityTypeConfiguration<Personagem_Magia>
    {
        public void Configure(EntityTypeBuilder<Personagem_Magia> builder)
        {
            builder.ToTable("PERSONAGEM_MAGIA");
            builder.HasKey(x => new { x.Id_Personagem, x.Id_Magia });

            builder.Property(x => x.Id_Personagem).HasColumnName("ID_PERSONAGEM").IsRequired();
            builder.Property(x => x.Id_Magia).HasColumnName("ID_MAGIA").IsRequired();

            builder.HasOne(x => x.Personagem).WithMany(x => x.Magias)
                   .HasForeignKey(x => x.Id_Personagem).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Magia).WithMany()
                   .HasForeignKey(x => x.Id_Magia).OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class PersonagemItemMap : IEntityTypeConfiguration<Personagem_Item>
    {
        public void Configure(EntityTypeBuilder<Personagem_Item> builder)
        {
            builder.ToTable("PERSONAGEM_ITEM");
            builder.HasKey(x => new { x.Id_Personagem, x.Id_Item });

            builder.Property(x => x.Id_Personagem).HasColumnName("ID_PERSONAGEM").IsRequired();
            builder.Property(x => x.Id_Item).HasColumnName("ID_ITEM").IsRequired();
            builder.Property(x => x.Quantidade).HasColumnName("QUANTIDADE").IsRequired();

            builder.HasOne(x => x.Personagem).WithMany(x => x.Itens)
                   .HasForeignKey(x => x.Id_Personagem).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Item).WithMany()
                   .HasForeignKey(x => x.Id_Item).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
