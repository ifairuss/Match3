using Match3;
using UnityEngine;
using UnityEngine.EventSystems;

public class FishController : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        var slot = GetComponentInParent<BackgroundSlot>();

        if(slot != null)
        {
            if (slot.IsEmpty == false)
            {
                Destroy(gameObject);
            }
            else
            {
                print("Slot isEmpty");
            }
        }
    }
}
