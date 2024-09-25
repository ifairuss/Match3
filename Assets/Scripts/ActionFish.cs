using System;
using UnityEngine;

namespace Match3
{
    public class ActionFish : MonoBehaviour
    {
        public static event Action<ActionFish,BackgroundSlot> FishTransformPosotion;

        public static void SendTransformFish(ActionFish fish, BackgroundSlot slot)
        {
            if(FishTransformPosotion != null) FishTransformPosotion.Invoke(fish, slot);
        }
    }
}
