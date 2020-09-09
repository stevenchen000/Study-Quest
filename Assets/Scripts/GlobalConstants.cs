using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalConstants : MonoBehaviour
{
    public static GlobalConstants gc;

    [Header("Floats")]
    public float defaultScale = 0.5f;

    [Header("GameObjects")]
    public GameObject skillObject;









    public void Awake()
    {
        if (gc == null)
        {
            gc = this;
        }
        else {
            Destroy(this);
        }
    }
    
}
