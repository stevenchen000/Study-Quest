using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace QuizSystem
{
    public class QuestionUI : MonoBehaviour
    {
        public Text questionText;

        public void Start()
        {
            
        }


        public void SetQuestion(string question) {
            questionText.text = question;
        }
    }
}
