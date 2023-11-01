using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyTrello.Data.Entities;

namespace MyTrello.Data.Configurations;

public class BoardsConfiguration : IEntityTypeConfiguration<Board>
{
    public void Configure(EntityTypeBuilder<Board> entity)
    {
        entity.ToTable("Boards");

        entity.HasKey(e => e.BoardId)
            .HasName("PRIMARY");

        entity.HasIndex(e => e.Name)
            .IsUnique();

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50);
    }
}
