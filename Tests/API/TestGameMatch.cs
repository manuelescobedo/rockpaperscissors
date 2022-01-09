using API.Controllers;
using API.DTOs;
using Domain;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests.API
{
    public partial class TestGameController
    {
        

        [TestMethod]
        public void ShouldAddMatch()
        {
            var game = SetupGame();

            var actual = _sut.AddMatch(game.ID, new NewMatchDTO
            {
                Option = (int)GameOption.Rock
            }) as CreatedAtActionResult;

            Assert.IsTrue(actual.RouteValues.ContainsKey("id"));
            Assert.IsTrue(actual.RouteValues.ContainsKey("matchId"));
            Assert.IsInstanceOfType(actual.Value, typeof(MatchDTO));
            Assert.AreEqual(nameof(_sut.GetMatch), actual.ActionName);
        }

        [TestMethod]
        public void ShouldReturnNotFoundGameToMatch()
        {


            var actual = _sut.AddMatch(Guid.Empty, new NewMatchDTO
            {
                Option = GameOption.Paper
            });

            Assert.IsInstanceOfType(actual, typeof(NotFoundResult));
        }
                
    }
}
