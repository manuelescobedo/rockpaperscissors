using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Domain
{
    [TestClass]
    public class TestMatch
    {
        [TestMethod]
        public void RockShouldBeatsScissors()
        {
            var cpuResult = new Match(GameOption.Rock, GameOption.Scissors);
            var result = new Match(GameOption.Scissors, GameOption.Rock);

            Assert.AreEqual(MatchResult.Lose, cpuResult.Result);
            Assert.AreEqual(MatchResult.Win, result.Result);
        }

        [TestMethod]
        public void PaperShouldBeatsRock()
        {
            var cpuResult = new Match(GameOption.Paper, GameOption.Rock);
            var result = new Match(GameOption.Rock, GameOption.Paper);

            Assert.AreEqual(MatchResult.Lose, cpuResult.Result);
            Assert.AreEqual(MatchResult.Win, result.Result);
        }

        [TestMethod]
        public void ScissorsShouldBeatsPaper()
        {
            var cpuResult = new Match(GameOption.Scissors, GameOption.Paper);
            var result = new Match(GameOption.Paper, GameOption.Scissors);
            

            Assert.AreEqual(MatchResult.Lose, cpuResult.Result);
            Assert.AreEqual(MatchResult.Win, result.Result);
        }

        
        [TestMethod]
        public void ShouldDraw()

        {
            GameOption option = GameOption.Rock;

            var result = new Match(option, option);

            Assert.AreEqual(MatchResult.Tie, result.Result);
        }
    }
}
