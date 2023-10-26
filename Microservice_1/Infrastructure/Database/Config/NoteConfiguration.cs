using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Infrastructure.Database.Config;


public class NoteConfiguration : IEntityTypeConfiguration<Note>
{
    public void Configure(EntityTypeBuilder<Note> builder)
    {
        builder.ToTable("note");

        builder.HasKey(kb => kb.Id);

        builder.Property(jc => jc.Id)
            .HasColumnName("id");

        builder.Property(jc => jc.Name)
            .HasColumnName("name");

        builder.Property(jc => jc.Description)
            .HasColumnName("description");
    }
}
