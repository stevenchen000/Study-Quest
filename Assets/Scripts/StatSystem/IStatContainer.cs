using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatSystem
{
    public interface IStatContainer
    {
        int GetStatValue(string statName);
    }
}
