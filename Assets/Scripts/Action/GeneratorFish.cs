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

        private void Awake()
        {
            Initialized();
        }

        private void Update()
        {
            Initialized();

            if (_spawnTime > 0)
            {
                _spawnTime -= 0.1f * Time.deltaTime;
            }
        }

        private void Initialized()
        {
            if(_spawnTime <= 0)
            {
                for (int x = 0; x < _board.Width; x++)
                {
                    var slots = _board.SlotBox.transform.GetChild(x).GetComponent<BackgroundSlot>();

                    if (slots.IsEmpty == true)
                    {
                        if (slots.GetComponent<BackgroundSlot>() != null)
                        {
                            int randomFish = Random.Range(0, _allFishVariable.Length);

                            var fish = Instantiate(_allFishVariable[randomFish], slots.transform.position, Quaternion.identity);

                            fish.transform.SetParent(slots.transform);

                            slots.IsEmpty = false;

                            _spawnTime = _timeToSpawn;
                        }
                    }
                }
            }
        }
    }
}
