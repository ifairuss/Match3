using UnityEngine;

namespace Match3
{
    public class UIBoard : MonoBehaviour
    {
        [Header("Board Preferences")]
        public int Width;
        public int Height;

        [Header("Board Component")]
        [SerializeField] private GameObject _backgroundSlotPrefab;

        public static BackgroundSlot[,] _slots;

        public Transform SlotBox;

        private void Awake()
        {
            _slots = new BackgroundSlot[Width, Height];
            SetBoardSlot();
        }

        private void SetBoardSlot()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Vector2 tempPosition = new Vector2(x, y);

                    var slot = Instantiate(_backgroundSlotPrefab, tempPosition, Quaternion.identity);
                    var checkSlot = slot.gameObject.GetComponent<CheckingSlots>();
                    var slotComponent = slot.gameObject.GetComponent<BackgroundSlot>();

                    slot.transform.SetParent(SlotBox);

                    slot.gameObject.name = $"{x},{y}";

                    slotComponent.yName = y;

                    if (slot.gameObject.name == $"{0},{y}")
                    {
                        slot.gameObject.AddComponent<CheckingSlots>();
                    }
                }
            }
        }
    }
}
