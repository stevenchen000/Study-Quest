using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatSystem
{
    public interface ITurnTaker
    {
        void StartTurn();
        bool IsTakingTurn();
    }
}
