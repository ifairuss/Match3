using Match3;
using UnityEngine;
using UnityEngine.EventSystems;

public class FishController : MonoBehaviour, IPointerDownHandler
{
    private UIBoard board;

    private void Start()
    {
        board = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIBoard>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        var slot = GetComponentInParent<BackgroundSlot>();

        if(slot != null)
        {
            if (slot.IsEmpty == false && board.successChecking == true)
            {
                board.StartCoroutine(board.StartCheckFish());
                Destroy(gameObject);
            }
            else
            {
                print("Checking....");
            }
        }
    }
}
