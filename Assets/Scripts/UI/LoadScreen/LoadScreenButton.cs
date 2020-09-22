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

        public LevelData data;
        public Button button;
        public Text text;

        private void Start()
        {
            button = transform.GetComponent<Button>();
            button.onClick.AddListener(SetDungeonData);
            LoadScreenUI ui = FindObjectOfType<LoadScreenUI>();
            button.onClick.AddListener(ui.DisableStageSelectUI);
            button.onClick.AddListener(ui.EnableDifficultySelectUI);
        }

        public void SetData(LevelData data)
        {
            this.data = data;
            button = transform.GetComponent<Button>();
            text = button.GetComponentInChildren<Text>();
            text.text = data.dungeon.GetDungeonName();
        }
        
        public void SetDungeonData()
        {
            WorldState.SetDungeonData(data.dungeon);
        }
    }
}
