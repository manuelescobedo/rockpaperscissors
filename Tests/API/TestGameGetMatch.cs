using API.Controllers;
using API.DTOs;
using Domain;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Doubles;

namespace Tests.API
{
    public partial class TestGameController
    {
        

        [TestMethod]
        public void ShouldGetMatch()
        {
            Game game = SetupGameWithMatch();

            var actual = _sut.GetMatch(game.ID, game.Matches[0].ID) as OkObjectResult;

            Assert.IsInstanceOfType(actual.Value, typeof(MatchDTO));
        }

        [TestMethod]
        public void ShouldReturnNotFoundForNonExistingGameOrMatch()
        {


            var actual = _sut.GetMatch(Guid.Empty, Guid.Empty);

            Assert.IsInstanceOfType(actual, typeof(NotFoundResult));
        }

        Game SetupGameWithMatch()
        {
            var game = new Game();
            game.AddMatch();
            _context.Games.Add(game);
            _context.SaveChanges();
            return game;
        }
    }
}
