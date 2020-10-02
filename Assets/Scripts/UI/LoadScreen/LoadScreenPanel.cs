using SOEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LoadScreenPanel : MonoBehaviour
{

    public EventSO caller;
    public Color defaultColor;
    public float fadeTime;
    private Image image;

    private void Awake()
    {
        image = transform.GetComponent<Image>();
    }

    // Start is called before the first frame update
    private void OnEnable()
    {
        image.color = defaultColor;
    }

    // Update is called once per frame
    void Update()
    {
        FadeLoadScreen();

        if(image.color.a <= 0)
        {
            gameObject.SetActive(false);
            caller.CallEvent();
        }
    }

    private void FadeLoadScreen()
    {
        Color currColor = image.color;
        float subtractAmount = 1f / fadeTime * Time.deltaTime;
        Color newColor = new Color(currColor.r, currColor.g, currColor.b, currColor.a - subtractAmount);
        image.color = newColor;
    }
}
