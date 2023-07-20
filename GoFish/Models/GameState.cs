using System.Collections.Generic;
using System.Linq;
using System;

namespace GoFish.Models
{
    public class GameState
    {
        public static GameState Game;
        private const int StartingHand = 5;
        private const int TotalCards = 52;
        private int _turn;
        private Random rng;
        public int Players { get; }
        public int Turn
        {
            get
            {
                return _turn;
            }
        }
        public List<int> Deck { get; } = new List<int>();
        public List<Hand> Hands { get; } = new List<Hand>();
        public List<int> PairCount { get; } = new List<int>();

        public GameState()
        {
            Game = this;
            rng = new Random();
            Players = 2;
            _turn = 0;
            Deck.AddRange(Enumerable.Range(0, TotalCards));
            for (int i = 0; i < Players; i++)
            {
                Hand hand = new Hand();
                for (int j = 0; j < StartingHand; j++)
                {
                    hand.Cards.Add(DrawCard());
                }
                Hands.Add(hand);
                PairCount.Add(0);
            }
        }

        public GameState(int players)
        {
            Game = this;
            rng = new Random();
            if (players > 5)
            {
                players = 5;
            }
            Players = players;
            _turn = 0;
            Deck.AddRange(Enumerable.Range(0, TotalCards));
            for (int i = 0; i < Players; i++)
            {
                Hand hand = new Hand();
                for (int j = 0; j < StartingHand; j++)
                {
                    hand.Cards.Add(DrawCard());
                }
                Hands.Add(hand);
            }
        }

        public void NextTurn()
        {
            _turn = (++_turn) % Players;
        }

        public bool CanDraw()
        {
            return Deck.Count > 0;
        }

        public int DrawCard()
        {
            int index = rng.Next(Deck.Count);
            int card = Deck[index];
            Deck.RemoveAt(index);
            return card;
        }

        public bool isGameOver()
        {
            return PairCount.Sum() == TotalCards / 2;
        }

        public bool HasPair(int card1, int player)
        {
            foreach (int card2 in Hands[player].Cards)
            {
                if (IsPair(card1, card2))
                {
                    StealCard(card2, Turn, player);
                    return true;
                }
            }
            return false;
        }

        private void StealCard(int card, int playerTo, int playerFrom)
        {
            //Add card to playerTo hand
            //Hands[playerTo].Cards.Add(card);
            PairCount[playerTo]++;
            //Remove card from playerFrom hand
            Hands[playerFrom].Cards.Remove(card);
        }

        public bool IsPair(int card1, int card2)
        {
            // Step 1: Check if same color (card# % 28)
            // Step 2: Check if same value (card# % 14)
            if (card1 % 28 == card2 % 28 && card1 % 14 == card2 % 14)
            {
                return true;
            }
            return false;
        }
    }
}


