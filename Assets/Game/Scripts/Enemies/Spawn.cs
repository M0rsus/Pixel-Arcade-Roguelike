using UnityEngine;

namespace Game
{
    public class Spawn : MonoBehaviour
    {
        private GameObject[] _spawner;

        private void Start()
        {
            int count = 0;
            _spawner = new GameObject[transform.childCount];
            foreach(Transform child in transform)
            {
                _spawner[count] = child.gameObject;
                count++;
            }
        }
        public void Instantiate(GameObject gameObject)
        {
            Instantiate(gameObject, _spawner[Random.Range(0, _spawner.Length)].transform.position, Quaternion.identity);
        }
    }
}