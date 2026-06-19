using Microsoft.EntityFrameworkCore;
using Oraculo.Infrastructure.Persistence.Entities;

namespace Oraculo.Infrastructure.Persistence;

public partial class OraculoContext : DbContext
{
    public OraculoContext() { }

    public OraculoContext(DbContextOptions<OraculoContext> options) : base(options) { }

    public virtual DbSet<Country> Countries { get; set; }
    public virtual DbSet<Match> Matches { get; set; }
    public virtual DbSet<Phase> Phases { get; set; }
    public virtual DbSet<Prediction> Predictions { get; set; }
    public virtual DbSet<Seer> Seers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Match>(entity =>
        {
            entity.Property(e => e.Group).HasColumnType("INTEGER");
            entity.HasOne(d => d.AwayTeamNavigation).WithMany(p => p.MatchAwayTeamNavigations)
                .HasForeignKey(d => d.AwayTeam)
                .OnDelete(DeleteBehavior.ClientSetNull);
            entity.HasOne(d => d.HomeTeamNavigation).WithMany(p => p.MatchHomeTeamNavigations)
                .HasForeignKey(d => d.HomeTeam)
                .OnDelete(DeleteBehavior.ClientSetNull);
            entity.HasOne(d => d.Phase).WithMany(p => p.Matches)
                .HasForeignKey(d => d.PhaseId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Prediction>(entity =>
        {
            entity.HasOne(d => d.Match).WithMany(p => p.Predictions)
                .HasForeignKey(d => d.MatchId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            entity.HasOne(d => d.Seer).WithMany(p => p.Predictions)
                .HasForeignKey(d => d.SeerId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}