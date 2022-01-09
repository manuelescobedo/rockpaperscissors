using System;
using API.Controllers;

using Domain;
using Infrastructure.Repositories;
using Infrastructure.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using API.DTOs;
using Tests.Doubles;
using System.Collections.Generic;

namespace Tests.API
{
    [TestClass]
    public partial class TestGameController
    {
        StubDbContext _context;
        GameRepository _repository;
        GamesController _sut;

        [TestInitialize]
        public void Setup()
        {
            _context = new StubDbContext();
            _repository = new GameRepository(_context);
            _sut = new GamesController(_repository);
        }

        [TestCleanup]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public void ShouldGetListOfAvailableGames()
        {

            var actual = _sut.Get() as OkObjectResult;

            Assert.IsInstanceOfType(actual.Value, typeof(IEnumerable<GameDTO>));
        }

        [TestMethod]
        public void ShouldRegisterGame()
        {

            var actual = _sut.Register(new NewGameDTO
            {
                MaxOfVictories = 3
            }) as CreatedAtActionResult;


            Assert.IsTrue(actual.RouteValues.ContainsKey("id"));
            Assert.IsInstanceOfType(actual.Value, typeof(GameDTO));
            Assert.AreEqual(nameof(_sut.Get), actual.ActionName);
        }


        Game SetupGame()
        {
            var game = new Game();
            _context.Games.Add(game);
            _context.SaveChanges();
            return game;
        }












    }
}