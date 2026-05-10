using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class Gun : IWeapon
    {
        public void Fire() => Debug.Log("Gun fired");
        [SerializeField]
        private float bulletSpeed;
    }
}