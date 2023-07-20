using Microsoft.AspNetCore.Mvc;
using GoFish.Models;
using System;

namespace GoFish.Controllers
{
    public class GameController : Controller
    {
        [HttpGet("/NewGame")]
        public ActionResult CreateGame()
        {
            return View();
        }

        [HttpPost("/NewGame")]
        public ActionResult CreateGame(int players)
        {
            new GameState(players);
            return RedirectToAction("Play");
        }

        [HttpGet("/Game")]
        public ActionResult Play()
        {
            int currentPlayer = GameState.Game.Turn;
            Hand currentHand = GameState.Game.Hands[currentPlayer];
            return View(currentHand);
        }

        [HttpPost("/Game")]
        public ActionResult Play(bool isGoFish, bool isSteal, int card, int playerFrom)
        {
            Console.WriteLine("FORM WAS SUBMITTED HERE IS GO FISH: ", isGoFish);
            if (isGoFish)
            {
                GoFish();
            }
            return RedirectToAction("Play");
        }
        private void GoFish()
        {
            // Get current player
            int currentPlayer = GameState.Game.Turn;
            // Draw a Card
            int drawnCard = GameState.Game.DrawCard();
            // If pair is in hand add pair
            bool isPairMade = GameState.Game.HasPair(drawnCard, currentPlayer);
            if (!isPairMade)
            {
                // Add to current players hand
                GameState.Game.Hands[currentPlayer].Cards.Add(drawnCard);
            }
            GameState.Game.NextTurn();
        }
    }
}