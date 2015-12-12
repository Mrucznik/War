﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Wojna
{
    internal class Runda
    {
        private readonly List<Zawodnik> _zawodnicy;

        public Runda(List<Zawodnik> zawodnicy)
        {
            _zawodnicy = zawodnicy;
        }
        
        public void Rozegraj()
        {
            var cards = CheckCards();
            for (int i = 0; i < _zawodnicy.Count; i++)
            {
                Console.WriteLine(_zawodnicy[i].Name + "(" + _zawodnicy[i].NumberOfCards + ") rzucił: " + cards[i].Name);
            }

            Console.WriteLine("\nRozgrywam...\n");
            var warCards = new List<Card>();
            //while (CanWeDoTheWar()) warCards.AddRange(DoTheWar(GetWarPlayers()));
            var winCard = EmergeWinnerCard(cards);
            Console.WriteLine("Zwyciezka karta: " + winCard.Name);
            var winner = GetZawodnikByCard(winCard);
            Console.WriteLine("Zwyciezca: " + winner.Name);
            winner.TakeCards(ThrowCards());
            winner.TakeCards(warCards.ToArray());
        }

        private Zawodnik[] GetWarPlayers()
        {
            Card checkedCard = null;
            var zawodnicy = new List<Zawodnik>();
            foreach (var card in CheckCards())
            {
                if (checkedCard == null) checkedCard = card;
                else if (card.IsEqual(checkedCard))
                {
                    var previousZawodnik = GetZawodnikByCard(checkedCard);
                    if(!zawodnicy.Contains(previousZawodnik))
                        zawodnicy.Add(previousZawodnik);
                    zawodnicy.Add(GetZawodnikByCard(card));
                    checkedCard = card;
                }
            }
            return zawodnicy.ToArray();
        }

        private Card[] DoTheWar(Zawodnik[] zawodnicy)
        {
            var cards = new List<Card>();
            return cards;
        }

        private bool CanWeDoTheWar()
        {
            Card checkedCard = null;
            foreach (var card in CheckCards())
            {
                if (checkedCard == null) checkedCard = card;
                else if (card.IsEqual(checkedCard))
                    return true;
            }
            return false;
        }

        private Zawodnik GetZawodnikByCard(Card card)
        {
            return _zawodnicy.First(zawodnik => zawodnik.CheckCard() == card);
        }

        private static Card EmergeWinnerCard(IEnumerable<Card> cards)
        {
            Card winCard = null;
            foreach (var card in cards)
            {
                if (winCard == null)
                {
                    winCard = card;
                }
                else if (card.IsStronger(winCard))
                {
                    winCard = card;
                }
            }
            return winCard;
        }

        private Card[] CheckCards() => _zawodnicy.Select(zawodnik => zawodnik.CheckCard()).ToArray();
        private Card[] ThrowCards()
        {
            /*foreach (var zawodnik in _zawodnicy)
            {
                
            }*/
            return _zawodnicy.Select(zawodnik => zawodnik.ThrowCard()).ToArray();
        }
    }
}
