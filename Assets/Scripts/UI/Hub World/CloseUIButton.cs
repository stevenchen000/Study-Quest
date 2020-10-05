using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CloseUIButton : MonoBehaviour
{
    private Button button;
    public UnityEvent onClickEvent;
    // Start is called before the first frame update
    void Start()
    {
        button = transform.GetComponent<Button>();
        button.onClick.AddListener(onClickEvent.Invoke);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
