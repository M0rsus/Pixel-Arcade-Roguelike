using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class Laser : IWeapon
    {
        public void Fire() => Debug.Log("Laser fired");
        [SerializeField] 
        private float fireRate;
    }
}