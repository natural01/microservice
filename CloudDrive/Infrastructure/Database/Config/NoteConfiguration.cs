using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Infrastructure.Database.Config;


public class NoteConfiguration : IEntityTypeConfiguration<CNote>
{
    public void Configure(EntityTypeBuilder<CNote> builder)
    {
        builder.ToTable("note");

        builder.HasKey(kb => kb.Id);

        builder.Property(jc => jc.Id)
            .HasColumnName("id");

        builder.Property(jc => jc.Title)
            .HasColumnName("title");

        builder.Property(jc => jc.Description)
            .HasColumnName("description");
    }
}
