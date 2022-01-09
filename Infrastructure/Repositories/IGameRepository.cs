using Domain;
using System;
using System.Collections.Generic;

namespace Infrastructure.Repositories
{
    public interface IGameRepository
    {
        void Add(Game game);

        void Edit(Game game);

        Game Get(Guid id);

        IReadOnlyList<Game> Get();
        
    }
}
