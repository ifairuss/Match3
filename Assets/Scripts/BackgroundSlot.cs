using UnityEngine;

namespace Match3
{
    public class BackgroundSlot : MonoBehaviour
    {
        [Header("Slot Preferences")]
        [SerializeField] private ActionFish _fishComponent;

        public int yName;

        public bool IsEmpty;

        private void Start()
        {
            IsEmpty = true;
        }

        private void Update()
        {
            CheckSlot();
        }

        private void CheckSlot()
        {
            if(transform.childCount <= 0)
            {
                IsEmpty = true;
                _fishComponent = null;
            }
            else
            {
                if(transform.GetChild(0) != null)
                {
                    IsEmpty = false;
                    _fishComponent = transform.GetChild(0).GetComponent<ActionFish>();
                }
            }
        }
    }
}
