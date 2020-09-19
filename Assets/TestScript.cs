using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestScript : MonoBehaviour
{

    public string levelName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SceneManager.UnloadSceneAsync(levelName);
        }
    }
}
