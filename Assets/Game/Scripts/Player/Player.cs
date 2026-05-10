using System.Collections;
using UnityEngine;

namespace Game
{
    public class Player : MonoBehaviour
    {
        [SerializeField] Laser laser;

        [SerializeField] Gun gun;
        
        [SerializeField] GameObject canvas;
        private void Start()
        {
            laser.Fire();
            gun.Fire();
        }
    }
}