using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RPGMaster.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.DataAccess.Maps
{
    public class ConviteMap: IEntityTypeConfiguration<Convite>
    {
        public void Configure (EntityTypeBuilder<Convite> builder)
        {
            builder.ToTable("CONVITE");
            builder.HasKey(x => x.Id_Convite);
            builder.HasIndex(x => x.Codigo).IsUnique();

            builder.Property(x => x.Id_Convite).HasColumnName("ID_CONVITE").ValueGeneratedOnAdd().IsRequired();
            builder.Property(x => x.Id_Campanha).HasColumnName("ID_CAMPANHA").IsRequired();
            builder.Property(x => x.Ativo).HasColumnName("ATIVO").IsRequired();
            builder.Property(x => x.Codigo).HasColumnName("CODIGO").HasMaxLength(12).IsRequired();
            builder.Property(x => x.CriadoEm).HasColumnName("CRIADO_EM").IsRequired();

            builder.HasOne(x => x.Campanha).WithMany()
                .HasForeignKey(x => x.Id_Campanha).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
