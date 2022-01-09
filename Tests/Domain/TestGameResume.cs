using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Domain
{
    [TestClass]
    public class TestGameResume
    {
        [TestMethod]
        public void ShouldResumeGame()
        {
            var game = new Game();
            game.AddMatch(GameOption.Rock, GameOption.Rock);
            game.Over();

            game.Resume();

            Assert.AreEqual(GameResult.Pending, game.Result);
        }

        [TestMethod]
        public void ShouldRaiseErrorIfGameIsDecided()
        {
            var game = new Game(1);

            game.AddMatch(GameOption.Rock, GameOption.Scissors);

            Assert.ThrowsException<GameException>(() => game.Resume());
        }
    }
}
