using UnityEngine;
using UnityEngine.EventSystems;

public class FishController : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Destroy(gameObject);
    }
}
