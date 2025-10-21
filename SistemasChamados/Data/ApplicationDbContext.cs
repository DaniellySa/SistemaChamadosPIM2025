using Microsoft.EntityFrameworkCore;
using SistemaChamados.Api.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SistemaChamados.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketComment> TicketComments { get; set; }
        public DbSet<AiHistory> AiHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>().HasKey(t => t.TicketId);
            modelBuilder.Entity<TicketComment>().HasKey(c => c.TicketCommentId);
            modelBuilder.Entity<AiHistory>().HasKey(a => a.AiHistoryId);

            modelBuilder.Entity<TicketComment>()
                .HasOne(c => c.Ticket)
                .WithMany(t => t.Comments)
                .HasForeignKey(c => c.TicketId);

            base.OnModelCreating(modelBuilder);
        }
    }
}