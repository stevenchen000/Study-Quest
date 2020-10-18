using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemSystem
{
    public abstract class Item : ScriptableObject
    {
        [SerializeField]
        protected int id;
        [SerializeField]
        protected Sprite icon;
        [SerializeField]
        protected string itemName;
        [SerializeField]
        protected bool isStackable = true;
        [SerializeField]
        protected int maxStack = 1;
        
        public int GetID()
        {
            return id;
        }
        public Sprite GetIcon()
        {
            return icon;
        }
        public string GetItemName()
        {
            return itemName;
        }
        public abstract Type GetItemType();
        public virtual Equipment GetEquipmentItem() { return null; }
        public virtual ConsumableItem GetConsumableItem() { return null; }
        public virtual CraftableItem GetCraftableItem() { return null; }

        public virtual bool IsStackable() { return isStackable; }
        public virtual int GetMaxStack() { return isStackable ? maxStack : 1; }
    }
}