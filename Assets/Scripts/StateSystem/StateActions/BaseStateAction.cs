using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace StateSystem
{
    public abstract class BaseStateAction : ScriptableObject
    {
        public List<Condition> conditions = new List<Condition>();

        public abstract void RunAction();
    }
}
