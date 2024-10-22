using Polyperfect.Crafting.Framework;
using Polyperfect.Crafting.Integration;
using UnityEngine;

namespace Items.Scripts
{
    public class GameItemStack : MonoBehaviour
    {
        public ItemStack itemStack;
        public BaseItemObject baseItemObject;
        Quantity _amount;
        long _id;

        void Start()
        {
            // Value is the amount of items in the stack
            _amount = itemStack.Value;
            _id = itemStack.ID.GetVal();
        }
    }
}
