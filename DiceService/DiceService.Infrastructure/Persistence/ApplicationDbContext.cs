using DiceService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiceService.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
    
    public DbSet<DiceRoll> DiceRolls { get; set; }
}