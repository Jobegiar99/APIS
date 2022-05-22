using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
        fileName = "PlayerStats",
        menuName = "ScriptableObjects/Player",
        order = 1
)]
public class PlayerStats:ScriptableObject
{
        [SerializeField] public int attack;
        [SerializeField] public float cooldown; //update enemy decision class
        [SerializeField] public int hp;
        [SerializeField] public int defense;
        [SerializeField] public float moveSpeed;
        [SerializeField] public List<BoostNode> activeBoosts;
}
