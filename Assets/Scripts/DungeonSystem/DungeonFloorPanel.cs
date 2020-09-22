using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSystem
{
    public class DungeonFloorPanel : MonoBehaviour
    {

        public bool floorIsCleared = false;
        public DungeonFloorData data;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetData(DungeonFloorData newData)
        {
            data = newData;
            GameObject symbol = Instantiate(data.symbol);
            symbol.transform.SetParent(transform);

            transform.GetComponent<SpriteRenderer>().color = data.color;
            symbol.transform.localPosition = new Vector3();
        }

        public void LoadLevel()
        {
            data.LoadLevel();
        }

        public void UnloadLevel()
        {
            data.UnloadLevel();
        }
    }
}