using System.Collections;
using UnityEngine;

namespace Match3
{
    public class GeneratorFish : MonoBehaviour
    {
        [Header("Generator Component")]
        [SerializeField] private float _timeToSpawn;

        private float _spawnTime;

        [Header("Generator Component")]
        [SerializeField] private GameObject[] _allFishVariable;
        [SerializeField] private UIBoard _board;

        public void Init()
        {
            _spawnTime = _timeToSpawn;
            StartCoroutine(SpawnRoutine());
        }

        private IEnumerator SpawnRoutine()
        {
            while (true)
            {
                if (_spawnTime > 0)
                {
                    _spawnTime -= Time.deltaTime;
                }
                else
                {
                    Spawn();
                }

                yield return null;
            }
        }

        private void Spawn()
        {
            for (int x = 0; x < _board.Width; x++)
            {
                var slot = _board.SlotBox.transform.GetChild(x).GetComponent<BackgroundSlot>();

                if (slot.IsEmpty)
                {
                    if (slot.GetComponent<BackgroundSlot>() != null)
                    {
                        int randomFish = Random.Range(0, _allFishVariable.Length);

                        var fish = Instantiate(_allFishVariable[randomFish], slot.transform.position, Quaternion.identity, slot.transform);
                        slot.Fish = fish;
                    }
                }
            }

            _spawnTime = _timeToSpawn;
        }
    }
}
