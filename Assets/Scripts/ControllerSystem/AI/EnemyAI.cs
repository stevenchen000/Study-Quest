using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatSystem;

namespace ControllerSystem
{
    public class EnemyAI : ControllerAI
    {
        public override void ControlCharacter(Fighter fighter, CombatManager battle)
        {
            
        }

        public override void RunTurn(Fighter fighter, CombatManager battle)
        {
            throw new NotImplementedException();
        }

        public override void SelectSkill(Fighter fighter, CombatManager battle)
        {
            throw new NotImplementedException();
        }
    }
}
