using Domain;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class GameRepository : IGameRepository
    {
        readonly GameDbContext _context;

        public GameRepository(GameDbContext context)
        {
            _context = context;
        }

        public void Add(Game game)
        {

            _context.Games.Add(game);
            _context.SaveChanges();

        }

        public void Edit(Game game)
        {
            _context.Games.Attach(game);
            _context.SaveChanges();
        }



        public Game Get(Guid id)
        {
            return _context.Games.Include(g => g.Matches)

                    .FirstOrDefault(g => g.ID == id);
        }

        public IReadOnlyList<Game> Get()
        {
            var items = _context.Games.Include("Matches");

            return items.ToList();
        }
    }
}
