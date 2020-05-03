using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KarutaSystem
{
    public class KarutaCard : MonoBehaviour
    {

        public string answer;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


        public string GetAnswer() { return answer; }
        public void SetAnswer(string newAnswer)
        {
            answer = newAnswer;
        }
    }
}