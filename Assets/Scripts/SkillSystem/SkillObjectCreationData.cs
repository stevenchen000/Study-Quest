using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SkillSystem
{
    public class SkillObjectCreationData
    {
        public int skillObjIndex;
        public float timing;
        public Vector3 positionOffset;
        public bool createCopyPerTarget;

        public SkillObjectAnimation animation;
    }
}
