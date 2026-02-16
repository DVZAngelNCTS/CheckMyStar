// Fichier : Data/CheckMyStarDbContext.Society.cs
using CheckMyStar.Data.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CheckMyStar.Data;

public partial class CheckMyStarDbContext
{
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Society>(entity =>
        {
            entity.HasKey(e => e.Identifier);
            entity.ToTable("Society");

            entity.Property(e => e.Identifier)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.Property(e => e.IsActive)
                .HasDefaultValue(true);

            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime");

            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime");

            entity.HasIndex(e => e.AddressIdentifier)
                .HasDatabaseName("IX_Society_AddressIdentifier");
        });
    }
}