using System;

namespace Domain
{
    public class Match
    {
        public DateTime MatchDate { get; private set; }
        public Guid ID { get; private set; }
        public GameOption CpuOption { get; private set; }
        public GameOption Option { get; private set; }
        public Game Game { get; private set; }
        
        
        public Match(GameOption cpuOption, GameOption option) : this(Guid.NewGuid(), cpuOption, option, DateTime.Now)
        {
        }
        public Match( Guid id, GameOption cpuOption, GameOption option, DateTime dateTime)
        {
            CpuOption = cpuOption;
            Option = option;

            MatchDate = dateTime;
            ID = id;

            Result = GetResult();
        }

        public virtual MatchResult Result
        {
            get;
            private set;
        }

        MatchResult GetResult() {
            if (Option == GameOption.Scissors && CpuOption == GameOption.Rock) return MatchResult.Lose;
            if (Option == GameOption.Paper && CpuOption == GameOption.Scissors) return MatchResult.Lose;
            if (Option == GameOption.Rock && CpuOption == GameOption.Paper) return MatchResult.Lose;

            if (Option == GameOption.Rock && CpuOption == GameOption.Scissors) return MatchResult.Win;
            if (Option == GameOption.Scissors && CpuOption == GameOption.Paper) return MatchResult.Win;
            if (Option == GameOption.Paper && CpuOption == GameOption.Rock) return MatchResult.Win;

            return MatchResult.Tie;
        }

    }
   
}
