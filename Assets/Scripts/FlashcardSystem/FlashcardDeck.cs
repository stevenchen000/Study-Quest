using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace FlashcardSystem
{

    [CreateAssetMenu(menuName = "Flashcards", fileName = "Flashcards")]
    public class FlashcardDeck : ScriptableObject
    {
        public List<Flashcard> flashcards;

        //public functions

        /// <summary>
        /// Adds a card to the deck
        /// Fails and returns false if there is another card with the same front text
        /// </summary>
        /// <param name="newCard"></param>
        public bool AddCard(Flashcard newCard)
        {
            bool result = false;

            if(flashcards == null)
            {
                flashcards = new List<Flashcard>(25);
            }

            if (!DeckAlreadyContainsCard(newCard))
            {
                flashcards.Add(newCard);
                result = true;
            }

            return result;
        }

        public bool AddCards(List<Flashcard> cards) {
            bool result = true;

            for (int i = 0; i < cards.Count; i++) {
                AddCard(cards[i]);
            }

            return result;
        }

        public void SetCards(List<Flashcard> cards)
        {
            flashcards = cards;
        }

        public void RemoveCard(int index)
        {
            if(flashcards.Count > index && index >= 0)
            {
                flashcards.RemoveAt(index);
            }
        }

        public List<Flashcard> GetCopy()
        {
            int size = flashcards.Count;
            List<Flashcard> cards = new List<Flashcard>(size);

            cards.AddRange(flashcards.GetRange(0, size));

            return cards;
        }

        public List<Flashcard> GetShuffledCards()
        {
            List<Flashcard> cards = GetCopy();
            int size = cards.Count;

            for(int i = 0; i < size; i++)
            {
                int rand = UnityEngine.Random.Range(0, size);

                Flashcard temp = cards[i];
                cards[i] = cards[rand];
                cards[rand] = temp;
            }

            return cards;
        }



        //static functions

        public static List<Flashcard> FileToCards(string filename)
        {
            List<Flashcard> cards = new List<Flashcard>();

            FileStream file = File.OpenRead(filename);
            StreamReader stream = new StreamReader(file);

            while (!stream.EndOfStream)
            {
                string line = stream.ReadLine();
                cards.Add(TextToFlashcard(line));
            }

            return cards;
        }

        /// <summary>
        /// Converts a block of text into flashcards
        /// Lines by default are separated by a newline
        /// Each side is by default separated by a tab
        /// </summary>
        /// <param name="text"></param>
        /// <param name="lineSeparator"></param>
        /// <returns></returns>
        public static List<Flashcard> TextblockToCards(string text, char lineSeparator = '\n', char sideSeparator = '\t') {
            List<Flashcard> cards = new List<Flashcard>();

            string[] splitText = text.Split(lineSeparator);

            for(int i = 0; i < splitText.Length; i++)
            {
                cards.Add(TextToFlashcard(splitText[i], sideSeparator));
            }

            return cards;
        }

        public static Flashcard TextToFlashcard(string text, char separator = '\t')
        {
            string[] splitText = text.Split(separator);
            string frontText = splitText[0];
            string backText = splitText[1];

            Flashcard card = new Flashcard(frontText, backText);
            return card;
        }



        //private functions

        private bool DeckAlreadyContainsCard(Flashcard newCard)
        {
            bool result = false;

            for(int i = 0; i < flashcards.Count; i++)
            {
                if(flashcards[i].GetFrontText().Equals(newCard.GetFrontText(), StringComparison.InvariantCultureIgnoreCase))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }
    }
}