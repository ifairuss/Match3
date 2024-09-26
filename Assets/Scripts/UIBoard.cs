using UnityEngine;
using UnityEngine.UI;

namespace Match3
{
    public class UIBoard : MonoBehaviour
    {
        [Header("Board Preferences")]
        public int Width;
        public int Height;

        [Header("Board Component")]
        [SerializeField] private GameObject _backgroundSlotPrefab;
        [SerializeField] private GridLayoutGroup _boardLayout;
        [SerializeField] private GeneratorFish _generatorFish;

        public static BackgroundSlot[,] _slots;

        public RectTransform SlotBox;

        private void Start()
        {
            var cellSize = Mathf.Min(SlotBox.rect.width / Width, SlotBox.rect.height / Height) - Width * 5f;
            _boardLayout.cellSize = new Vector2(cellSize, cellSize);
            _slots = new BackgroundSlot[Width, Height];
            SetBoardSlot();
            _generatorFish.Init();

            foreach (var slot in _slots)
            {
                if (slot.gameObject.TryGetComponent<CheckingSlots>(out var checkingSlots)) 
                {
                    checkingSlots.Init();
                }
            }
        }

        private void SetBoardSlot()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    Vector2 tempPosition = new Vector2(x, y);

                    var slot = Instantiate(_backgroundSlotPrefab, tempPosition, Quaternion.identity, SlotBox);
                    var slotComponent = slot.gameObject.GetComponent<BackgroundSlot>();

                    slot.gameObject.name = $"{x},{y}";
                    _slots[x, y] = slotComponent;

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
