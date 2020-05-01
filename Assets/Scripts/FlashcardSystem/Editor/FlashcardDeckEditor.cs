using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

namespace FlashcardSystem
{
    [CustomEditor(typeof(FlashcardDeck))]
    public class FlashcardDeckEditor : Editor
    {

        private FlashcardDeck deck;
        private Flashcard card;
        private string filename;

        private void OnEnable()
        {
            card = new Flashcard("", "");
        }

        public override void OnInspectorGUI()
        {
            deck = (FlashcardDeck)target;

            if(deck.flashcards == null)
            {
                deck.flashcards = new List<Flashcard>();
            }

            CardsGUI(deck.flashcards);

            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("New Card");
            CardGUI(card);
            if (GUILayout.Button("Add Card"))
            {
                deck.AddCard(card);
                card = new Flashcard("", "");
            }

            EditorGUILayout.Space(10);
            CreateCardsFromFileGUI();
        }


        private void CardsGUI(List<Flashcard> cards)
        {
            int removeIndex = -1;
            for(int i = 0; i < cards.Count; i++)
            {
                CardGUI(cards[i]);

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField($"Card {i+1}");
                if (GUILayout.Button("Remove Card"))
                {
                    removeIndex = i;
                }
                EditorGUILayout.EndHorizontal();


                EditorGUILayout.Space(5);
            }

            if (removeIndex >= 0)
            {
                cards.RemoveAt(removeIndex);
            }
        }

        private void CardGUI(Flashcard card)
        {
            string frontText = EditorGUILayout.TextField("Front Side", card.GetFrontText());
            string backText = EditorGUILayout.TextField("Back Side", card.GetBackText());

            card.SetFrontText(frontText);
            card.SetBackText(backText);
        }

        private void CreateCardsFromFileGUI()
        {
            EditorGUILayout.LabelField("Import cards");
            filename = EditorGUILayout.TextField("Filename", filename);

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Add Cards")) {
                List<Flashcard> newCards = FlashcardDeck.FileToCards(filename);
                deck.AddCards(newCards);
            }

            if (GUILayout.Button("Set Cards")) {
                List<Flashcard> newCards = FlashcardDeck.FileToCards(filename);
                deck.SetCards(newCards);
            }

            EditorGUILayout.EndHorizontal();
        }

        
    }
}