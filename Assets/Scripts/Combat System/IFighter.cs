using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatSystem
{
    public interface IFighter
    {
        int GetCurrentHealth();
        int GetMaxHealth();
        void TakeDamage(int damage);
        void HealHealth(int healing);

    }
}
