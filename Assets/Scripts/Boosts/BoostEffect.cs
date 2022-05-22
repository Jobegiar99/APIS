using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
        fileName = "BoostEffect", 
        menuName = "ScriptableObjects/Boosts", 
        order = 1
)]
public class BoostEffect : ScriptableObject
{
        [SerializeField] public Sprite sprite;
        [SerializeField] public string description;
        [SerializeField] public int price;
        [SerializeField] public string effectType;
        [SerializeField] public int effectIMagnitude;
        [SerializeField] public float effectFMagnitude;
        [SerializeField] public List<ItemInformation> items;

        public void ApplyEffectInt(ref int attribute)
        {
                attribute += effectIMagnitude;
        }

        public void ApplyEffectFloat(ref float attribute)
        {
                attribute += effectFMagnitude;
        }

}
