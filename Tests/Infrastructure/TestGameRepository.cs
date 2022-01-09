using Domain;
using Infrastructure.Repositories;
using Tests.Doubles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Infrastructure
{

    [TestClass]
    public class TestGameRepository
    {
        StubDbContext _context;

        [TestInitialize]
        public void Setup()
        {
            _context = new StubDbContext();

        }
        
        [TestCleanup]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public void ShouldGetGameById()
        {
            Game game = SetupGame();

            var rep = new GameRepository(_context);

            var actual = rep.Get(game.ID);

            Assert.IsNotNull(actual);

        }

        Game SetupGame()
        {
            var game = new Game();
            _context.Games.Add(game);
            _context.SaveChanges();
            return game;
        }

        [TestMethod]
        public void ShouldGetGamesList()
        {

            var game = SetupGame();

            var rep = new GameRepository(_context);

            var actual = rep.Get();

            Assert.AreEqual(1, actual.Count);

        }

        [TestMethod]
        public void ShouldEditGame()
        {

            var game = SetupGame();

            var rep = new GameRepository(_context);

            game.Over();
            rep.Edit(game);

            var actual = rep.Get(game.ID);
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.IsOver);

        }
    }
}