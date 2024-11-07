using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Match3
{
    public class UIBoard : MonoBehaviour
    {
        [Header("Board Preferences")]
        [SerializeField] private float _slotSize;
        [Space]
        public int Width;
        public int Height;

        [Header("Board Component")]
        [SerializeField] private GameObject _backgroundSlotPrefab;
        [SerializeField] private GridLayoutGroup _boardLayout;
        [SerializeField] private GeneratorFish _generatorFish;

        public static BackgroundSlot[,] _slots;

        public RectTransform SlotBox;

        public bool successChecking = false;
        private float firstCheck;

        private void Start()
        {
            firstCheck = 5;

            var cellSize = Mathf.Min(SlotBox.rect.width / Width, SlotBox.rect.height / Height) - Width * _slotSize;
            _boardLayout.cellSize = new Vector2(cellSize, cellSize);
            _slots = new BackgroundSlot[Width, Height];
            SetBoardSlot();
            _generatorFish.Init();
            StartCoroutine(StartCheckFish());

            foreach (var slot in _slots)
            {
                if (slot.gameObject.TryGetComponent<CheckingSlots>(out var checkingSlots)) 
                {
                    checkingSlots.Init();
                }
            }
        }

        public  IEnumerator StartCheckFish()
        {
            successChecking = false;
            yield return new WaitForSeconds(firstCheck);
            CheckFish();
            yield return new WaitForSeconds(0.5f);
            firstCheck = 3f;
            successChecking = true;
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
                    slotComponent.xName = x;

                    if (slot.gameObject.name == $"{0},{y}")
                    {
                        slot.gameObject.AddComponent<CheckingSlots>();
                    }
                }
            }
        }

        public void CheckFish()
        {
            var connectList = new List<ActionFish>();

            CheckRow(connectList);
            CheckColumn(connectList);


            for(int i = 0; i < connectList.Count; i++)
            {
                connectList.Distinct();
                Destroy(connectList[i].gameObject);
                StartCoroutine(StartCheckFish());
            }
        }

        private void CheckRow(List<ActionFish> connectList)
        {
            for(int y = 0; y < _slots.GetLength(1); y++)
            {
                var line = new List<ActionFish>();

                for (int x = 0; x < _slots.GetLength(0) - 1; x++)
                {
                    var slot = _slots[y, x];
                    var nextSlot = _slots[y, x + 1];

                    if (slot.FishComponent.FishName == nextSlot.FishComponent.FishName)
                    {
                        line.Add(slot.FishComponent);
                        line.Add(nextSlot.FishComponent);
                        line.Distinct();
                    }
                    else if(line.Count > 0 && slot.FishComponent.FishName != nextSlot.FishComponent.FishName)
                    {
                        break;
                    }
                }

                if (line.Count > 3)
                {
                    connectList.AddRange(line);
                }
            }
        }

        private void CheckColumn(List<ActionFish> connectList)
        {
            for (int x = 0; x < _slots.GetLength(0); x++)
            {
                var line = new List<ActionFish>();

                for (int y = 0; y < _slots.GetLength(1) - 1; y++)
                {
                    var slot = _slots[y, x];
                    var nextSlot = _slots[y + 1, x];

                    if (slot.FishComponent.FishName == nextSlot.FishComponent.FishName)
                    {
                        line.Add(slot.FishComponent);
                        line.Add(nextSlot.FishComponent);
                        line.Distinct();
                    }
                    else if (line.Count > 0 && slot.FishComponent.FishName != nextSlot.FishComponent.FishName)
                    {
                        break;
                    }
                }

                if (line.Count > 3)
                {
                    connectList.AddRange(line);
                }
            }
        }
    }
}
