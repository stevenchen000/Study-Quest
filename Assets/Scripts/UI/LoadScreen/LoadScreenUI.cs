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

        // Start is called before the first frame update
        void Start()
        {
            LoadLevelButtons();
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
                newButton.transform.parent = transform;
            }
        }
    }
}