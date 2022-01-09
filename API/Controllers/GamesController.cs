using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Infrastructure.Repositories;
using API.DTOs;

namespace API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {

        readonly IGameRepository _gameRepository;

        public GamesController(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        // GET api/games/{id}
        [HttpGet]
        public ActionResult Get()
        {

            IEnumerable<GameDTO> games = _gameRepository.Get().Select(g => new GameDTO(g));

            return Ok(games);

        }

        // GET api/games/{id}
        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {

            var game = _gameRepository.Get(id);

            if (game != null) return Ok(new GameDTO(game));
            return NotFound();

        }

        // POST api/games
        [HttpPost]
        public ActionResult Register([FromBody] NewGameDTO payload)
        {

            var game = new Game(payload.MaxOfVictories);
            // save game
            _gameRepository.Add(game);

            return CreatedAtAction(nameof(Get), new { id = game.ID }, new GameDTO(game));

        }

        // PUT api/games/{id}/over
        [HttpPut("{id}/[action]")]
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
