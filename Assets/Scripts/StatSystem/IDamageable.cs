using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatSystem
{
    public interface IDamageable
    {
        void TakeDamage(int damage);
        void HealDamage(int damage);
    }
}
