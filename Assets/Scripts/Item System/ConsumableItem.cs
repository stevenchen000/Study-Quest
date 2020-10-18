using UnityEngine;
using System.Collections;
using System;

namespace ItemSystem
{
    public class ConsumableItem : Item
    {
        public override Type GetItemType()
        {
            return typeof(ConsumableItem);
        }

        public override ConsumableItem GetConsumableItem()
        {
            return this;
        }

        public override bool IsStackable()
        {
            return true;
        }
    }
}