using System.Collections;
using UnityEngine;

namespace Match3
{
    public class BackgroundSlot : MonoBehaviour
    {
        [Header("Slot Preferences")]
        [SerializeField] private ActionFish _fishComponent;

        private Transform _dragBox;

        public int yName;

        public bool IsEmpty => Fish == null;

        public GameObject Fish;

        private void Awake()
        {
            ActionFish.FishTransformPosotion += TransformFish;
            _dragBox = GameObject.FindGameObjectWithTag("DragBox").GetComponent<Transform>();
        }

        private void Update()
        {
            CheckSlot();
        }

        private void OnDestroy()
        {
            ActionFish.FishTransformPosotion -= TransformFish;
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

        public void TransformFish(ActionFish fish, BackgroundSlot slot)
        {
            if (slot.IsEmpty == true)
            {
                fish.transform.SetParent(_dragBox);
                StartCoroutine(TransformFishInNewSlot(fish, slot));
            }
        }

        private IEnumerator TransformFishInNewSlot(ActionFish fish, BackgroundSlot slot)
        {
            yield return new WaitForSeconds(0.2f);

            slot.Fish = fish.gameObject;
            yield return new WaitForSeconds(0.1f);
            fish.transform.SetParent(slot.transform);
            fish.transform.localPosition = Vector3.zero;
        }
    }
}
