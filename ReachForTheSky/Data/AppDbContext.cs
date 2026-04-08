using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ReachForTheSky.Models;

namespace ReachForTheSky.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}