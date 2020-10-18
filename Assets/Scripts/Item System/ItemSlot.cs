using UnityEngine;
using System.Collections;
using System;

namespace ItemSystem
{
    [Serializable]
    public class SlotData
    {
        public Item item;
        public bool isStackable;
        public int stack;

        public SlotData(Item item)
        {
            this.item = item;
            isStackable = item.IsStackable();
            stack = 1;
        }

        public int AddItemAndReturnExcess(int amount)
        {
            int excess = 0;

            if(amount > 0)
            {
                stack += amount;
                int max = item.GetMaxStack();
                excess = Mathf.Max(0, stack - max);
            }

            return excess;
        }
    }

    public class ItemSlot : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}