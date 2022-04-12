using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Weapon : MonoBehaviour
    {
        public enum WeaponType
        {
            Sword
        }


        [SerializeField] WeaponType type;
        [SerializeField] AnimatorOverrideController attackAnimation;
        public WeaponType Type { get { return type; } }
        public AnimatorOverrideController AttackAnimation { get { return attackAnimation; } }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
