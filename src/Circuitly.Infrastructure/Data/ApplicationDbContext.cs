using Circuitly.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Circuitly.Infrastructure;

public class ApplicationDbContext: IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<League> Leagues { get; set; }
    
    public DbSet<Player> Players { get; set; }
    
    public DbSet<Team> Teams { get; set; }
}
