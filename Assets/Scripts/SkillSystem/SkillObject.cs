using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SkillSystem
{
    public class SkillObject : MonoBehaviour
    {
        public float lifetime;

        private Timer timer;


        private void Start()
        {
            timer = new Timer();
        }

        private void Update()
        {
            timer.Tick();

            if(timer.AtTime(lifetime))
            {
                gameObject.SetActive(false);
                timer.ResetTimer();
            }
        }










        


    }
}
