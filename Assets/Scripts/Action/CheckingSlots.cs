using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    public class CheckingSlots : MonoBehaviour
    {
        [Header("Slot preference")]
        [SerializeField] private BackgroundSlot[] _verticalRow;
        [SerializeField] private Transform _slotBox;

        private BackgroundSlot _slot;
        private UIBoard _board;

        private void Start()
        {
            _board = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIBoard>();
            _slotBox = GameObject.FindGameObjectWithTag("SlotBox").GetComponent<Transform>();

            _slot = GetComponent<BackgroundSlot>();

            _verticalRow = new BackgroundSlot[_board.Height];
        }

        private void Update()
        {
            AddSlot();
            Checking_verticalRow();
        }

        public void AddSlot()
        {

            for (int i = 0; i < _slotBox.childCount; i++)
            {
                for (int y = 0; y < _verticalRow.Length; y++)
                {
                    var slot = _slotBox.GetChild(i).GetComponent<BackgroundSlot>();
                    if (_slot.yName == slot.yName)
                    {
                        if (_verticalRow[y] == null)
                        {
                            _verticalRow[y] = slot;
                            break;
                        }
                    }
                }
            }
        }

        public void Checking_verticalRow()
        {
            for (int i = 1; i < _verticalRow.Length; i++)
            {
                ActionFish fishGameObject = _verticalRow[i - 1].transform.GetChild(0).GetComponent<ActionFish>();
                BackgroundSlot fishPosition = _verticalRow[i].GetComponent<BackgroundSlot>();
                if (_verticalRow[i].IsEmpty == true)
                {
                    ActionFish.SendTransformFish(fishGameObject, fishPosition);
                }
                else
                {
                    //dprint($"{_verticalRow[i]} - Ne pustoi");
                }
            }
        }
    }
}
