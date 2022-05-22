using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
        fileName = "WeaponStats",
        menuName = "ScriptableObjects/Weapon/Weapon Stats",
        order = 1
)]
public class WeaponStats : ScriptableObject
{
        [SerializeField] public int damage;
        [SerializeField] public float cooldown;
        [SerializeField] public float ammo;
}
