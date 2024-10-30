using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Match3
{
    public class BackgroundSlot : MonoBehaviour
    {
        [Header("Slot Preferences")]
        [SerializeField] private ActionFish _fishComponent;

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
                _fishComponent = null;
            }
            else
            {
                _fishComponent = Fish.GetComponent<ActionFish>();
                GetMatch(_fishComponent, true);
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

        public List<ActionFish> GetMatch(ActionFish fish, bool main)
        {
            var connectFish = new List<ActionFish>();

            RowCheck(ref connectFish);
            ColumnCheck(ref connectFish);

            if(main)
            {
                for (int i = 0; i < connectFish.Count; i++)
                {
                    GetMatch(connectFish[i], false);
                    Destroy(connectFish[i]);
                }
            }

            return connectFish;
        }

        private void ColumnCheck(ref List<ActionFish> connectFish)
        {
            var line = new List<ActionFish>();

            for (int i = 1; i < 3; i++)
            {

                if(yName < 0 || yName >= UIBoard._slots.GetLength(1) || xName < 0 || xName >= UIBoard._slots.GetLength(0))
                {
                    return;
                }
                else
                {
                    if(yName > 0 && yName != 0)
                    {
                        var thisSlot = UIBoard._slots[xName, yName]._fishComponent;
                        var nexSlot = UIBoard._slots[xName, yName - 1]._fishComponent;

                        if (thisSlot.Equals(nexSlot))
                        {
                            line.Add(nexSlot);
                        }
                        else
                        {
                            if(line.Count > 2)
                            {
                                for(int j = 0; j < line.Count; j++)
                                {
                                    connectFish.Add(line[j]);
                                    connectFish.Distinct();
                                }
                            }
                        }
                    }
                    else
                    {
                        break;
                    }

                    if (yName < UIBoard._slots.GetLength(1) - 1 && yName != UIBoard._slots.GetLength(1) - 1)
                    {
                        var thisSlot = UIBoard._slots[xName, yName]._fishComponent;
                        var nexSlot = UIBoard._slots[xName, yName + 1]._fishComponent;

                        if (thisSlot.Equals(nexSlot))
                        {
                            line.Add(nexSlot);
                        }
                        else
                        {
                            if (line.Count > 2)
                            {
                                for (int j = 0; j < line.Count; j++)
                                {
                                    connectFish.Add(line[j]);
                                    connectFish.Distinct();
                                }
                            }
                        }
                    }
                    else
                    {
                        break;
                    }

                }

            }
        }

        private void RowCheck(ref List<ActionFish> connectFish)
        {
            var line = new List<ActionFish>();

            for (int i = 1; i < 3; i++)
            {
                if (yName < 0 || yName >= UIBoard._slots.GetLength(1) || xName < 0 || xName >= UIBoard._slots.GetLength(0))
                {
                    return;
                }
                else
                {
                    if (xName > 0 && xName != 0)
                    {
                        var thisSlot = UIBoard._slots[xName, yName]._fishComponent;
                        var nexSlot = UIBoard._slots[xName - 1, yName]._fishComponent;

                        if (thisSlot.Equals(nexSlot))
                        {
                            line.Add(nexSlot);
                        }
                        else
                        {
                            if (line.Count > 2)
                            {
                                for (int j = 0; j < line.Count; j++)
                                {
                                    connectFish.Add(line[j]);
                                    connectFish.Distinct();
                                }
                            }
                        }
                    }
                    else
                    {
                        break;
                    }

                    if (xName < UIBoard._slots.GetLength(0) - 1 && xName != UIBoard._slots.GetLength(0) - 1)
                    {
                        var thisSlot = UIBoard._slots[xName, yName]._fishComponent;
                        var nexSlot = UIBoard._slots[xName + 1, yName]._fishComponent;

                        if (thisSlot.Equals(nexSlot))
                        {
                            line.Add(nexSlot);
                        }
                        else
                        {
                            if (line.Count > 2)
                            {
                                for (int j = 0; j < line.Count; j++)
                                {
                                    connectFish.Add(line[j]);
                                    connectFish.Distinct();
                                }
                            }
                        }
                    }
                    else
                    {
                        break;
                    }

                }

            }
        }
    }
}
