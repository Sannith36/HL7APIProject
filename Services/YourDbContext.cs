using HL7APIProject.Models;
using Microsoft.EntityFrameworkCore;

namespace HL7APIProject.Services
{
    public class YourDbContext : DbContext
    {
        public YourDbContext(DbContextOptions<YourDbContext> options) : base(options) { }

        public DbSet<HL7Message> HL7Messages { get; set; }
    }
}
