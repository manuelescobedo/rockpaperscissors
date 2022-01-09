using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Persistence
{

    public class GameDbContextFactory : IDesignTimeDbContextFactory<GameDbContext>
    {

        public GameDbContext CreateDbContext(string[] args)
        {
            return new GameDbContext();
        }
    }
}