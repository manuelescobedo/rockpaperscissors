using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests.API
{
    
    public partial class TestGameController
    {
        

        [TestMethod]
        
        public void ShouldRestart()
        {
            var game = SetupGame();

            var actual = _sut.Restart(game.ID);

            Assert.IsInstanceOfType(actual, typeof(NoContentResult));
        }

        [TestMethod]
        public void ShouldReturnNotFoundGameToRestart()
        {


            var actual = _sut.Restart(Guid.Empty);

            Assert.IsInstanceOfType(actual, typeof(NotFoundResult));
        }
        
    }
}
