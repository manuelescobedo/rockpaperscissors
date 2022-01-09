using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests.API
{
    
    public partial class TestGameController
    {
    
        [TestMethod]
        public void ShouldResume()
        {
            var game = SetupGame();
            game.Over();

            var actual = _sut.Resume(game.ID);

            Assert.IsInstanceOfType(actual, typeof(NoContentResult));
        }

        [TestMethod]
        public void ShouldReturnNotFoundGameToResume()
        {


            var actual = _sut.Resume(Guid.Empty);

            Assert.IsInstanceOfType(actual, typeof(NotFoundResult));
        }
    }
}
