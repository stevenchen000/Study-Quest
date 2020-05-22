using CombatSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateSystem
{
    public class StateManager
    {
        public Fighter character;

        public List<CharacterState> allStates;
        public List<StateTransition> allTransitions;

        public CharacterState currentState;
        public int currentStateIndex;
        public List<StateTransition> currentTransitions;

        
    }
}
