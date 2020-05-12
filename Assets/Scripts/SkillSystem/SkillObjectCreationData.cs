using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SkillSystem
{
    [System.Serializable]
    public class SkillObjectCreationData
    {
        public int skillObjIndex;
        public float timing;
        public float lifetime;

        public SkillObjectTarget target;
        public bool parent;
        public Vector3 targetPositionOffset;
        public bool createCopyPerTarget;

        public SkillObjectAnimation animation;
    }
}
