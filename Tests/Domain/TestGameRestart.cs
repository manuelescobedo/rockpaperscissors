using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Domain
{
    [TestClass]
    public class TestGameRestart
    {
        [TestMethod]
        public void ShouldRestartGame()
        {
            var game = new Game();
            game.AddMatch(GameOption.Rock, GameOption.Scissors);

            game.Restart();

            Assert.AreEqual(0, game.Matches.Count);
            Assert.IsFalse(game.IsOver);
            Assert.AreEqual(GameResult.Pending, game.Result);
        }
    }
}
