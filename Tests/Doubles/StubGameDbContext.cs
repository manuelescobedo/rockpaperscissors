using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Tests.Doubles
{
    public class StubDbContext : GameDbContext    {
        
         protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
        {
            optionsBuilder.UseInMemoryDatabase("ut");
        }
    }

}