using EVoting_backend.DB.Models;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace EVoting_backend.DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Form> Form { get; set; }
        public DbSet<UserVoted> UserVotes { get; set; }
        public DbSet<SubForm> SubForm { get; set; }
        public DbSet<FormOption> FormOption { get; set; }
        public DbSet<Vote> Vote { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserVoted>()
                .HasOne(uv => uv.User)
                .WithMany(u => u.Votes)
                .HasForeignKey(uv => uv.UserId);
            builder.Entity<SubForm>()
                .HasOne(sf => sf.Form)
                .WithMany(f => f.SubForms)
                .HasForeignKey(sf => sf.FormId);
            builder.Entity<FormOption>()
                .HasOne(fo => fo.SubForm)
                .WithMany(sf => sf.Options)
                .HasForeignKey(fo => fo.SubFormId);
            builder.Entity<Vote>()
                .HasOne(v => v.Form)
                .WithMany(f => f.Votes)
                .HasForeignKey(v => v.FormId);

            base.OnModelCreating(builder);

        }
    }
}
