using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests.API
{
    
    public partial class TestGameController
    {
        [TestMethod]
        public void ShouldOverGame()
        {
            var game = SetupGame();

            var actual = _sut.Over(game.ID);

            Assert.IsInstanceOfType(actual, typeof(NoContentResult));
        }

        [TestMethod]
        public void ShouldReturnNotFoundGameToOver()
        {


            var actual = _sut.Over(Guid.Empty);

            Assert.IsInstanceOfType(actual, typeof(NotFoundResult));
        }
    }
}
