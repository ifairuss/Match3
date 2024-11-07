using System.Collections;
using UnityEngine;

namespace Match3
{
    public class BackgroundSlot : MonoBehaviour
    {
        [Header("Slot Preferences")]
        public ActionFish FishComponent;

        private Transform _dragBox;

        public int yName;
        public int xName;

        public bool IsEmpty => Fish == null;

        public GameObject Fish;

        private void Awake()
        {
            ActionFish.FishTransformPosotion += TransformFish;
            _dragBox = GameObject.FindGameObjectWithTag("DragBox").GetComponent<Transform>();
        }

        private void OnDestroy()
        {
            ActionFish.FishTransformPosotion -= TransformFish;
        }

        private void Update()
        {
            CheckSlot();
        }

        private void CheckSlot()
        {
            if (IsEmpty)
            {
                FishComponent = null;
            }
            else
            {
                FishComponent = Fish.GetComponent<ActionFish>();
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
