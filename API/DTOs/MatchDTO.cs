using Domain;
using System;

namespace API.DTOs

{
    public class MatchDTO
    {
        public DateTime Date { get; private set; }
        public Guid ID { get; private set; }
        public string CpuOption { get; private set; }
        public string Option { get; private set; }
        public Guid GameID { get; private set; }
        public string Result { get; private set; }
        public MatchDTO(Match m)
        {
            Date = m.MatchDate;
            ID = m.ID;
            CpuOption = m.CpuOption.ToString();
            Option = m.Option.ToString();
            GameID = m.Game.ID;
            Result = m.Result.ToString();
        }
    }
}
