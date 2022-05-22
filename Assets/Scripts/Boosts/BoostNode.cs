using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
        fileName = "BoostNode",
        menuName = "ScriptableObjects/Boosts/Boost Node",
        order = 2
)]
public class BoostNode :ScriptableObject
{
        [SerializeField] public BoostEffect boostEffect;
        [SerializeField] public BoostNode nextBoost;
}
