using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;

public class QuestionTeletype : MonoBehaviour
{
    private string questionText;
    [SerializeField]
    private TMP_Text textbox;
    [SerializeField]
    private int textSpeed;
    private bool isTyping = false;
    [SerializeField]
    private float waitTime;

    public delegate void TextFinished();
    public event TextFinished OnTextFinished;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeQuestion(string question)
    {
        questionText = question;
        textbox.text = question;
        if (!isTyping)
        {
            StartCoroutine(PrintText());
        }
    }

    private IEnumerator PrintText()
    {
        float index = 0;
        int textsize = questionText.Length;
        isTyping = true;
        
        while(index < textsize)
        {
            index += textSpeed * Time.deltaTime;
            textbox.maxVisibleCharacters = (int)index;
            
            yield return null;
        }

        WaitForSeconds time = new WaitForSeconds(waitTime);
        yield return time;

        isTyping = false;
        OnTextFinished?.Invoke();
    }

    public bool IsTyping() { return isTyping; }

}
