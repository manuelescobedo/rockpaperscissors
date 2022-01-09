using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Domain
{
    [TestClass]
    public class TestGameMatch
    {
        [TestMethod]
        public void ShouldWinTheGame()
        {
            var game = new Game(1);

            game.AddMatch(GameOption.Rock, GameOption.Scissors);

            Assert.AreEqual(GameResult.Win, game.Result);
            Assert.IsTrue(game.IsOver);
        }

        [TestMethod]
        public void ShouldLoseTheGame()
        {
            var game = new Game(1);

            game.AddMatch(GameOption.Rock, GameOption.Paper);

            Assert.AreEqual(GameResult.Lose, game.Result);
            Assert.IsTrue(game.IsOver);
        }

        [TestMethod]
        public void ShouldBePending()
        {
            var game = new Game(2);

            game.AddMatch(GameOption.Rock, GameOption.Rock);

            Assert.AreEqual(GameResult.Pending, game.Result);
        }

        [TestMethod]
        public void ShouldRecordTheMatchIfGameIsNotOver()
        {
            var game = new Game(1);

            game.AddMatch(GameOption.Rock, GameOption.Scissors);

            Assert.AreEqual(1, game.Matches.Count);
        }

        [TestMethod]
        public void ShouldRaiseErrorIfMatchIsOnGameOver()
        {
            var game = new Game(1);

            game.Over();

            Assert.ThrowsException<GameException>(() => game.AddMatch(GameOption.Rock, GameOption.Scissors));

        }
    }
}
