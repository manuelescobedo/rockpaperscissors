using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Domain;
using Infrastructure.Repositories;
using API.DTOs;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class GamesController : ControllerBase
    {

        readonly IGameRepository _gameRepository;

        public GamesController(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        // GET api/games/{id}
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GameDTO>), StatusCodes.Status200OK)]
        public ActionResult Get()
        {

            IEnumerable<GameDTO> games = _gameRepository.Get().Select(g => new GameDTO(g));

            return Ok(games);

        }

        // GET api/games/{id}
        [HttpGet("{id}")]

        [ProducesResponseType(typeof(GameDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public ActionResult Get(Guid id)
        {

            var game = _gameRepository.Get(id);

            if (game != null) return Ok(new GameDTO(game));
            return NotFound();

        }

        // POST api/games
        [HttpPost]

        [ProducesResponseType(typeof(GameDTO), StatusCodes.Status201Created)]
        public ActionResult Register([FromBody] NewGameDTO payload)
        {

            var game = new Game(payload.MaxOfVictories);
            // save game
            _gameRepository.Add(game);

            return CreatedAtAction(nameof(Get), new { id = game.ID }, new GameDTO(game));

        }

        // PUT api/games/{id}/over
        [HttpPut("{id}/[action]")]

        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public ActionResult Over(Guid id)
        {

            var game = _gameRepository.Get(id);
            if (game == null) return NotFound();

            game.Over();

            _gameRepository.Edit(game);

            return NoContent();

        }

        // PUT api/games/{id}/resume
        [HttpPut("{id}/[action]")]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public ActionResult Resume(Guid id)
        {
            var game = _gameRepository.Get(id);
            if (game == null) return NotFound();

            game.Resume();

            _gameRepository.Edit(game);

            return NoContent();
        }


        // PUT api/games/{id}/restart
        [HttpPut("{id}/[action]")]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public ActionResult Restart(Guid id)
        {


            var game = _gameRepository.Get(id);
            if (game == null) return NotFound();

            game.Restart();

            _gameRepository.Edit(game);

            return NoContent();


        }

        // PUT api/games/{id}/matches
        [HttpPost("{id}/matches")]
        [ProducesResponseType(typeof(MatchDTO),StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public ActionResult AddMatch(Guid id, [FromBody] NewMatchDTO payload)
        {

            var game = _gameRepository.Get(id);

            if (game == null) return NotFound();

            game.AddMatch((GameOption)payload.Option);

            _gameRepository.Edit(game);

            var match = game.Matches.LastOrDefault();
            return CreatedAtAction(nameof(GetMatch), new { id, matchId = match.ID }, new MatchDTO(match));

        }

        [HttpGet("{id}/matches/{matchId}")]
        [ProducesResponseType(typeof(MatchDTO),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public ActionResult GetMatch(Guid id, Guid matchId)
        {
            var game = _gameRepository.Get(id);
            if (game == null) return NotFound();

            var match = game.Matches.FirstOrDefault(m => m.ID == matchId);
            if (match == null) return NotFound();

            return Ok(new MatchDTO(match));
        }

    }


}
