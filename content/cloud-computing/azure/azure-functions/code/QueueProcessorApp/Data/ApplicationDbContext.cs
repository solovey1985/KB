using Microsoft.EntityFrameworkCore;
using QueueProcessorApp.Models;

namespace QueueProcessorApp.Data;

/// <summary>
/// Database context for SQL Server integration
/// </summary>
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ProcessedItem> ProcessedItems => Set<ProcessedItem>();
    public DbSet<ExecutionLog> ExecutionLogs => Set<ExecutionLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure ProcessedItem
        modelBuilder.Entity<ProcessedItem>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<ProcessedItem>()
            .Property(x => x.MessageId)
            .IsRequired()
            .HasMaxLength(255);

        modelBuilder.Entity<ProcessedItem>()
            .Property(x => x.Data)
            .IsRequired()
            .HasColumnType("nvarchar(max)");

        modelBuilder.Entity<ProcessedItem>()
            .Property(x => x.Status)
            .HasDefaultValue(ProcessingStatus.Pending);

        modelBuilder.Entity<ProcessedItem>()
            .HasIndex(x => x.MessageId)
            .IsUnique();

        modelBuilder.Entity<ProcessedItem>()
            .HasIndex(x => x.Status);

        modelBuilder.Entity<ProcessedItem>()
            .HasIndex(x => x.CreatedAt);

        // Configure ExecutionLog
        modelBuilder.Entity<ExecutionLog>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<ExecutionLog>()
            .Property(x => x.FunctionName)
            .IsRequired()
            .HasMaxLength(255);

        modelBuilder.Entity<ExecutionLog>()
            .Property(x => x.MessageId)
            .IsRequired()
            .HasMaxLength(255);

        modelBuilder.Entity<ExecutionLog>()
            .Property(x => x.Message)
            .HasColumnType("nvarchar(max)");

        modelBuilder.Entity<ExecutionLog>()
            .Property(x => x.Exception)
            .HasColumnType("nvarchar(max)");

        modelBuilder.Entity<ExecutionLog>()
            .HasIndex(x => new { x.FunctionName, x.CreatedAt });

        modelBuilder.Entity<ExecutionLog>()
            .HasIndex(x => x.Level);
    }
}
