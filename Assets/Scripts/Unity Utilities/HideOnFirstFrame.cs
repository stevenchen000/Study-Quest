using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnFirstFrame : MonoBehaviour
{

    // Hides the object on the first frame
    void Update()
    {
        gameObject.SetActive(false);
        Destroy(this);
    }
}
