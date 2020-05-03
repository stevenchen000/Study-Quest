using QuizSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LoadScreen
{
    public class LoadScreenButton : MonoBehaviour
    {

        public string levelName;
        public QuestionSheet questions;
        public Button button;
        public Text text;

        public void SetData(LevelData data)
        {
            levelName = data.levelName;
            questions = data.sheet;
            button = transform.GetComponent<Button>();
            button.onClick.AddListener(LoadLevel);
            text = button.GetComponentInChildren<Text>();
            text.text = data.name;
        }

        private void LoadLevel() {
            QuizManager.quiz.SetNewQuestions(questions);
            SceneManager.LoadScene(levelName);
        }

    }
}
