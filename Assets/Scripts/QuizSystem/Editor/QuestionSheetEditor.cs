using FlashcardSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace QuizSystem
{
    [CustomEditor(typeof(QuestionSheet))]
    public class QuestionSheetEditor : Editor
    {

        QuestionSheet sheet;
        Question newQuestion;
        FlashcardDeck deck;
        bool reverse = false;

        private void OnEnable()
        {
            newQuestion = new Question();
        }

        public override void OnInspectorGUI()
        {
            sheet = (QuestionSheet)target;
            FixList();
            FixFoldouts();

            int deleteIndex = -1;
            List<Question> questions = sheet.GetQuestions();
            for (int i = 0; i < questions.Count; i++) {
                Question question = questions[i];
                sheet.foldouts[i] = EditorGUILayout.BeginFoldoutHeaderGroup(sheet.foldouts[i], $"Q: {question.question} | A: {question.GetAnswer()}");
                if (sheet.foldouts[i])
                {
                    QuestionGUI(question);

                    GUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("Remove Question", GUILayout.Width(150)))
                    {
                        deleteIndex = i;
                        break;
                    }
                    GUILayout.EndHorizontal();
                }
                EditorGUILayout.EndFoldoutHeaderGroup();
            }

            if (deleteIndex >= 0) {
                DeleteAtIndex(deleteIndex);
            }

            EditorGUILayout.Space(20);
            QuestionGUI(newQuestion);
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Add Question", GUILayout.Width(150)))
            {
                AddQuestion(newQuestion);
            }
            EditorGUILayout.EndHorizontal();

            FlashcardsGUI();

            EditorUtility.SetDirty(target);
        }




        //GUI functions

        private void QuestionGUI(Question question) {
            question.question = EditorGUILayout.TextField("Question", question.question);
            QuestionType newType = (QuestionType)EditorGUILayout.EnumPopup("Question Type", question.type);
            question.SetQuestionType(newType);
            /*
            Solution solution = question.solution;
            switch (question.type)
            {
                case QuestionType.TrueFalse:
                    TrueOrFalseGUI(solution);
                    break;
                case QuestionType.MultipleChoice:
                    MultipleChoiceGUI(question);
                    break;
                case QuestionType.FillInTheBlank:
                    FillInTheBlankGUI(solution);
                    break;
            }*/

            EditorGUILayout.Space(10);
        }



        private void TrueOrFalseGUI(Solution solution) {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Solution");
            solution.isTrue = EditorGUILayout.Toggle(solution.isTrue.ToString(), solution.isTrue);
            EditorGUILayout.EndHorizontal();
        }

        private void MultipleChoiceGUI(Question question) {
            /*Solution solution = question.solution;
            solution.solution = EditorGUILayout.TextField("Solution", solution.solution);

            FixChoicesArray(question);

            question.wrongChoices[0] = EditorGUILayout.TextField("Wrong 1", question.wrongChoices[0]);
            question.wrongChoices[1] = EditorGUILayout.TextField("Wrong 2", question.wrongChoices[1]);
            question.wrongChoices[2] = EditorGUILayout.TextField("Wrong 3", question.wrongChoices[2]);*/
        }

        private void FillInTheBlankGUI(Solution solution) {
            solution.solution = EditorGUILayout.TextField("Solution", solution.solution);
        }


        private void FlashcardsGUI()
        {
            deck = (FlashcardDeck)EditorGUILayout.ObjectField("Flashcard Deck", deck, typeof(FlashcardDeck));
            string reverseText = reverse ? "Back Is Question" : "Front Is Question";
            reverse = EditorGUILayout.Toggle(reverseText, reverse);

            if (GUILayout.Button("Add Questions"))
            {
                List<Question> newQuestions = QuestionSheet.CreateQuestionsFromFlashcards(deck.flashcards, reverse);
                sheet.AddQuestions(newQuestions);
            }
        }




        //helper functions

        private void FixChoicesArray(Question question) {
            /*if (question.wrongChoices.Length == 0)
            {
                question.wrongChoices = new string[3];
            }
            else if (question.wrongChoices.Length < 3)
            {
                string[] choices = new string[3];
                for (int i = 0; i < question.wrongChoices.Length; i++) {
                    choices[i] = question.wrongChoices[i];
                }
            } else if (question.wrongChoices.Length > 3) {
                string[] choices = new string[3];
                for (int i = 0; i < 3; i++)
                {
                    choices[i] = question.wrongChoices[i];
                }
            }*/
        }

        private void FixFoldouts() {
            if (sheet.foldouts == null || sheet.questions.Count != sheet.foldouts.Count) {
                sheet.foldouts = new List<bool>(new bool[sheet.questions.Count]);
            }
        }
        private void FixList()
        {
            if(sheet.questions == null)
            {
                sheet.questions = new List<Question>();
            }
        }

        private void DeleteAtIndex(int index) {
            sheet.questions.RemoveAt(index);
            sheet.foldouts.RemoveAt(index);
        }

        private void AddQuestion(Question question) {
            sheet.AddQuestion(question);
            QuestionType type = question.type;
            newQuestion = new Question();
            newQuestion.type = type;
            sheet.foldouts.Add(false);
        }





        private void AddQuestionsFromDeck(FlashcardDeck deck)
        {
            List<Question> questions = QuestionSheet.CreateQuestionsFromFlashcards(deck.flashcards);
            for(int i = 0; i < questions.Count; i++)
            {
                sheet.AddQuestion(questions[i]);
            }
        }
    }
}