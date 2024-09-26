using System.Collections;
using UnityEngine;

namespace Match3
{
    public class TransformFishPosition : MonoBehaviour
    {
        private Transform _dragBox;
        private CheckingSlots _slot;

        private void Awake()
        {
            ActionFish.FishTransformPosotion += TransformFish;
            _dragBox = GameObject.FindGameObjectWithTag("DragBox").GetComponent<Transform>();
        }

        private void OnDestroy()
        {
            ActionFish.FishTransformPosotion -= TransformFish;
        }

        public void TransformFish(ActionFish fish,BackgroundSlot slot)
        {
            if (slot.IsEmpty)
            {
                fish.transform.SetParent(_dragBox);
                StartCoroutine(TransformFishInNewSlot(fish, slot));
            }
        }

        private IEnumerator TransformFishInNewSlot(ActionFish fish, BackgroundSlot slot)
        {
            yield return new WaitForSeconds(0.2f);

            slot.Fish = fish.gameObject;
            fish.transform.SetParent(slot.transform);
            fish.transform.localPosition = Vector3.zero;
        }
    }
}

