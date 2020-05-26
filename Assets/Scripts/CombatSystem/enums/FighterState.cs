﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombatSystem
{
    public enum FighterState
    {
        StartingTurn,
        SelectingSkill,
        AnsweringQuestion,
        Acting,
        Defending,
        EndingTurn,
        AwaitingTurn
    }
}
