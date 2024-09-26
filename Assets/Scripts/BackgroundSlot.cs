using UnityEngine;

namespace Match3
{
    public class BackgroundSlot : MonoBehaviour
    {
        [Header("Slot Preferences")]
        [SerializeField] private ActionFish _fishComponent;

        public int yName;

        public bool IsEmpty => Fish == null;

        public GameObject Fish;

        private void Update()
        {
            CheckSlot();
        }

        private void CheckSlot()
        {
            if (IsEmpty)
            {
                _fishComponent = null;
            }
            else
            {
                _fishComponent = Fish.GetComponent<ActionFish>();
            }
        }
    }
}
