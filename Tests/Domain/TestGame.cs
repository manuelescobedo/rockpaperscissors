using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;

namespace Tests.Domain
{
    [TestClass]
    public class TestGame
    {
        [DataRow(0)]
        [DataRow(6)]
        [DataTestMethod]
        public void ShouldRaiseAnErrorForInvalidNumberOfVictories(int maxOfVictories) {
            Assert.ThrowsException<GameException>(() => new Game(maxOfVictories));            
        }
    }
}