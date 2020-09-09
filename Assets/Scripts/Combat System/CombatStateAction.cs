using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CombatSystem
{
    public abstract class CombatStateAction : ScriptableObject
    {
        public string description;
        public abstract void CallAction(CombatManager manager);
    }
}
