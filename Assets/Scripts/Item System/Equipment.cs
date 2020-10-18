using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ItemSystem
{
    public class Equipment : Item
    {
        public override Type GetItemType()
        {
            return typeof(Equipment);
        }

        public override Equipment GetEquipmentItem()
        {
            return this;
        }

        public override bool IsStackable()
        {
            return false;
        }
    }
}