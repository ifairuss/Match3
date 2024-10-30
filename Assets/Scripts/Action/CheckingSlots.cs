using System.Collections;
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
        private GeneratorFish _generatorFish;

        private bool _isInitialized;

        public void Init()
        {
            _board = GameObject.FindGameObjectWithTag("GameManager").GetComponent<UIBoard>();
            _slotBox = GameObject.FindGameObjectWithTag("SlotBox").GetComponent<Transform>();

            _slot = GetComponent<BackgroundSlot>();

            _verticalRow = new BackgroundSlot[_board.Height];

            _isInitialized = true;
        }

        private void Update()
        {
            if (!_isInitialized)
                return;

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
                if (_verticalRow[i - 1].transform.childCount > 0 && _verticalRow[i].IsEmpty == true)
                {
                    ActionFish fishGameObject = _verticalRow[i - 1].transform.GetChild(0).GetComponent<ActionFish>();
                    BackgroundSlot previousePosition = _verticalRow[i - 1].GetComponent<BackgroundSlot>();
                    BackgroundSlot fishPosition = _verticalRow[i].GetComponent<BackgroundSlot>();

                    ActionFish.SendTransformFish(fishGameObject, fishPosition);
                    StartCoroutine(Timer(previousePosition, fishPosition));
                }
            }
        }

        private IEnumerator Timer(BackgroundSlot previousePosition, BackgroundSlot fishPosition)
        {
            yield return new WaitForSeconds(0.3f);
            previousePosition.Fish = null;
        }
    }
}
