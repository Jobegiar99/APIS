using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponStats : ScriptableObject
{
        [SerializeField] public int damage;
        [SerializeField] public float cooldown;
        [SerializeField] public float ammo;
}
