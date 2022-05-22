using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
        fileName = "ItemInformation",
        menuName = "ScriptableObjects/Items/Item Information",
        order = 1
)]
public class ItemInformation : ScriptableObject
{
        [SerializeField] public string itemName;
        [SerializeField] public string description;
        [SerializeField] public Sprite icon;
}
