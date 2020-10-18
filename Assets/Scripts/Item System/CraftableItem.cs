using UnityEngine;
using System.Collections;
using System;

namespace ItemSystem
{
    public class CraftableItem : Item
    {
        public override Type GetItemType()
        {
            return typeof(CraftableItem);
        }

        public override CraftableItem GetCraftableItem() { return this; }
    }
}