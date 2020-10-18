using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LoadScreen
{
    public class LoadScreenUI : MonoBehaviour
    {
        public List<LevelData> levels;
        public LoadScreenButton loadScreenButtonPrefab;

        public GameObject stageSelectUI;
        public GameObject difficultySelectUI;

        // Start is called before the first frame update
        void Start()
        {
            LoadLevelButtons();
            DisableDifficultySelectUI();
            EnableStageSelectUI();
        }

        // Update is called once per frame
        void Update()
        {

        }



        private void LoadLevelButtons()
        {
            for(int i = 0; i < levels.Count; i++)
            {
                LoadScreenButton newButton = Instantiate(loadScreenButtonPrefab);
                newButton.SetData(levels[i]);
                newButton.transform.parent = stageSelectUI.transform;
            }
        }

        public void EnableStageSelectUI()
        {
            stageSelectUI.SetActive(true);
        }

        public void DisableStageSelectUI()
        {
            stageSelectUI.SetActive(false);
        }

        public void EnableDifficultySelectUI()
        {
            difficultySelectUI.SetActive(true);
        }

        public void DisableDifficultySelectUI()
        {
            difficultySelectUI.SetActive(false);
        }

        public void LoadLevel()
        {
            string levelName = WorldState.GetDungeonName();
            WorldState.LoadLevel(levelName);
        }
    }
}