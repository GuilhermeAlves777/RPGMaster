using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RPGMaster.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.DataAccess.Maps
{
    public class ArmaDetalheMap : IEntityTypeConfiguration<Arma_Detalhe>
    {
        public void Configure(EntityTypeBuilder<Arma_Detalhe> builder)
        {
            builder.ToTable("ARMA_DETALHE");
            builder.HasKey(x => x.Id_Item);

            builder.Property(x => x.Id_Item).HasColumnName("ID_ITEM").IsRequired();
            builder.Property(x => x.Dano).HasColumnName("DANO").IsRequired();
            builder.Property(x => x.Tipo_Dano).HasColumnName("TIPO_DANO").IsRequired();
            builder.Property(x => x.Alcance).HasColumnName("ALCANCE").IsRequired();

            builder.HasOne(x => x.Item).WithOne(x => x.ArmaDetalhe)
                   .HasForeignKey<Arma_Detalhe>(x => x.Id_Item).OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class ArmaduraDetalheMap : IEntityTypeConfiguration<Armadura_Detalhe>
    {
        public void Configure(EntityTypeBuilder<Armadura_Detalhe> builder)
        {
            builder.ToTable("ARMADURA_DETALHE");
            builder.HasKey(x => x.Id_Item);

            builder.Property(x => x.Id_Item).HasColumnName("ID_ITEM").IsRequired();
            builder.Property(x => x.Defesa).HasColumnName("DEFESA").IsRequired();

            builder.HasOne(x => x.Item).WithOne(x => x.ArmaduraDetalhe)
                   .HasForeignKey<Armadura_Detalhe>(x => x.Id_Item).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
