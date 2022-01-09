using API.DTOs;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests.API
{
    
    public partial class TestGameController
    {
    
        [TestMethod]
        public void ShouldGetGameById()
        {
            Game game = SetupGame();

            var actual = _sut.Get(game.ID) as OkObjectResult;

            Assert.IsInstanceOfType(actual.Value, typeof(GameDTO));
        }

        [TestMethod]
        public void ShouldReturnNotFound()
        {
            

            var actual = _sut.Get(Guid.Empty);

            Assert.IsInstanceOfType(actual, typeof(NotFoundResult));
        }
        
    }
}
