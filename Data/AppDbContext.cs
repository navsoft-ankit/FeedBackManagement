using Microsoft.EntityFrameworkCore;
using Authservice.Models;
using Authservice.Data;
using System.Reflection.Metadata;
namespace Authservice.Data;
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
    }