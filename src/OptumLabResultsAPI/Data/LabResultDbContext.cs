using Microsoft.EntityFrameworkCore;
using OptumLabResultsAPI.Models;
using System.Reflection.Emit;

namespace OptumLabResultsAPI.Data;

public class LabResultDbContext : DbContext
{
    public LabResultDbContext(DbContextOptions<LabResultDbContext> options)
        : base(options)
    {
    }

    public DbSet<LabResult> LabResults => Set<LabResult>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LabResult>(entity =>
        {
            entity.ToTable("LabResults");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.PatientId)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(x => x.TestName)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(x => x.ResultValue)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(x => x.Unit)
                .IsRequired()
                .HasMaxLength(30);

            entity.Property(x => x.ObservedDate)
                .IsRequired();

            entity.HasIndex(x => new
            {
                x.PatientId,
                x.ObservedDate
            });
        });
    }
}