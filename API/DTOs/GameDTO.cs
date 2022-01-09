using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.DTOs
{
    public class GameDTO
    {
        public int MaxVictories { get; private set; }
        public bool IsOver { get; private set; }
        public Guid ID { get; private set; }
        public DateTime Date { get; private set; }

        private List<MatchDTO> _matches;
        public IReadOnlyList<MatchDTO> Matches => _matches;

        public string Result { get; private set; }

        public GameDTO(Game g)
        {
            MaxVictories = g.MaxVictories;
            IsOver = g.IsOver;
            ID = g.ID;
            Date = g.GameDate;
            Result = g.Result.ToString();
            _matches = g.Matches.Select(m => new MatchDTO(m)).ToList();
        }

        public GameDTO() {
            
        }
    }
}
