using System;
using System.Linq;
using System.Collections.Generic;

namespace Domain
{
    public class Game
    {
        public int MaxVictories { get; private set; }
        public bool IsOver { get; private set; }
        public Guid ID { get; private set; }
        public DateTime GameDate { get; private set; }
        public GameResult Result { get; private set; }


        List<Match> _matches = new List<Match>();
        public IReadOnlyList<Match> Matches => _matches;


        public Game(Guid id, int maxVictories, DateTime dateTime)
        {
            if (maxVictories < 1 || maxVictories > 5) throw new GameException(400, "Invalid configuration for game");

            GameDate = dateTime;
            ID = id;
            MaxVictories = maxVictories;

            IsOver = false;
            Result = GetResult();
        }
        public Game(int maxVictories = 3) : this(Guid.NewGuid(), maxVictories, DateTime.Now)
        {

        }
        GameOption GetRandomOption()
        {
            Random random = new Random();
            return (GameOption)random.Next((int)GameOption.Rock, (int)GameOption.Scissors + 1);
        }
        public void AddMatch()
        {
            GameOption option = GetRandomOption();
            GameOption cpuOption = GetRandomOption();

            AddMatch(option, cpuOption);
        }
        public void AddMatch(GameOption option)
        {
            GameOption cpuOption = GetRandomOption();

            AddMatch(option, cpuOption);
        }
        public void AddMatch(GameOption option, GameOption cpuOption)
        {
            if (IsOver) throw new GameException(422, "Cannot add match to overed game");

            _matches.Add(new Match(cpuOption, option));

            Result = GetResult();
            if (Result != GameResult.Pending)
            {
                Over();
            }
        }
        public void Over()
        {
            IsOver = true;

        }

        public void Resume()
        {
            if (Result == GameResult.Pending && IsOver)
            {
                IsOver = false;
            }
            else
            {
                throw new GameException(422, "Cannot resume overed game");
            }



        }

        public void Restart()
        {
            _matches.Clear();
            IsOver = false;
            Result = GetResult();
        }

        GameResult GetResult()
        {
            int cpuWins = _matches.Count(m => m.Result == MatchResult.Lose);
            int wins = _matches.Count(m => m.Result == MatchResult.Win);

            if (cpuWins == MaxVictories) return GameResult.Lose;
            if (wins == MaxVictories) return GameResult.Win;
            return GameResult.Pending;
        }


    }

}
