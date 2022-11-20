using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GymAssistant.Models;

namespace GymAssistant.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<GymAssistant.Models.JakZrobicMase> JakZrobicMase { get; set; }
        public DbSet<GymAssistant.Models.Maxy> Maxy { get; set; }
        public DbSet<GymAssistant.Models.Plan> Plan { get; set; }
        public DbSet<GymAssistant.Models.Total> Total { get; set; }
    }
}