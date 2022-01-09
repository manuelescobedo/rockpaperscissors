using Domain;
using System;
using System.Linq;
using Infrastructure.Persistence;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var context = new GameDbContext()) {
                context.Database.EnsureCreated();
                
                var game = new Game(3);
                
                while(!game.IsOver)
                {
                    game.AddMatch();

                    var lastMatch = game.Matches.LastOrDefault();
                    Console.WriteLine($"Last match at: {lastMatch.MatchDate.ToShortDateString()}, cpu: {lastMatch.CpuOption}, you: {lastMatch.Option}, result: {lastMatch.Result}");
                }
                context.Games.Add(game);
                context.SaveChanges();

                Console.WriteLine(game.Result);
            }
        }
    }
}
