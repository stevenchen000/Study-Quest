using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace QuizSystem
{
    public class QuizManager : MonoBehaviour
    {
        public QuestionSheet sheet;
        public List<Question> questions = new List<Question>();

        public static QuizManager quiz;

        public Question currentQuestion;
        public int currentIndex = 0;

        public delegate void QuestionAsked(Question question);
        public event QuestionAsked OnQuestionAsked;

        public delegate void QuestionAnswered(bool correct);
        public event QuestionAnswered OnQuestionAnswered;

        private bool isAnswered = false;


        private void Awake()
        {
            if (quiz == null)
            {
                quiz = this;
                DontDestroyOnLoad(this);
            }
            else {
                Destroy(this);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }




        //public functions

        public Question GetCurrentQuestion()
        {
            return currentQuestion;
        }

        public Question GetNextQuestion()
        {
            currentIndex = (currentIndex + 1) % questions.Count;
            SetQuestion();

            return questions[currentIndex];
        }


        public bool AnswerQuestion(string answer)
        {
            bool correct = currentQuestion.CheckAnswer(answer);

            if (!isAnswered)
            {
                OnQuestionAnswered?.Invoke(correct);
                GetNextQuestion();
                isAnswered = true;
            }
            
            return correct;
        }

        public void AskQuestion()
        {
            Question question = GetCurrentQuestion();
            OnQuestionAsked?.Invoke(question);
            isAnswered = false;
            Debug.Log("Asked Question");
        }

        public bool QuestionIsAnswered()
        {
            return isAnswered;
        }


        public void SetNewQuestions(QuestionSheet newQuestions)
        {
            sheet = newQuestions;
            questions = new List<Question>();
            questions.AddRange(sheet.GetQuestions());
            ScrambleQuestions();
            currentIndex = -1;
            currentQuestion = GetNextQuestion();
        }

        public void SubscribeToOnQuestionAsked(QuestionAsked func)
        {
            OnQuestionAsked += func;
        }

        public void UnsubscribeFromOnQuestionAsked(QuestionAsked func)
        {
            OnQuestionAsked -= func;
        }

        public void SubscribeToOnQuestionAnswered(QuestionAnswered func)
        {
            OnQuestionAnswered += func;
        }

        public void UnsubscribeFromOnQuestionAnswered(QuestionAnswered func)
        {
            OnQuestionAnswered -= func;
        }

        /// <summary>
        /// Rearranges the order of the questions
        /// </summary>
        private void ScrambleQuestions()
        {
            for (int i = 0; i < questions.Count; i++)
            {
                int rand = UnityEngine.Random.Range(0, questions.Count);
                Question temp = questions[i];
                questions[i] = questions[rand];
                questions[rand] = temp;
            }
        }



        /// <summary>
        /// Updates the currentQuestion based on currentIndex
        /// </summary>
        private void SetQuestion()
        {
            currentQuestion = questions[currentIndex];
        }

        public void SetQuestion(Question newQuestion)
        {
            currentQuestion = newQuestion;
        }


    }
}