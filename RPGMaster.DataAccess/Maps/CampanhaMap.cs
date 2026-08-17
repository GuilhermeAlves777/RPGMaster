using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RPGMaster.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGMaster.DataAccess.Maps
{
    public class CampanhaMap : IEntityTypeConfiguration<Campanha>
    {
        public void Configure(EntityTypeBuilder<Campanha> builder)
        {
            builder.ToTable("CAMPANHA");

            builder.HasKey(x => x.ID_Campanha);

            builder.Property(x => x.ID_Campanha).HasColumnName("ID_CAMPANHA")
                                       .ValueGeneratedOnAdd()
                                       .IsRequired();
            builder.Property(x => x.Nome).HasColumnName("NOME").IsRequired();
            builder.Property(x => x.ID_Criador).HasColumnName("ID_CRIADOR").IsRequired();

            builder.HasOne(x => x.Criador)
                   .WithMany(x => x.CampanhasCriadas)
                   .HasForeignKey(x => x.ID_Criador)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
