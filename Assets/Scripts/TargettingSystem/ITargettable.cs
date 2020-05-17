using StatSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TargettingSystem
{
    public interface ITargettable : IDamageable
    {
        string GetTag();
    }
}
