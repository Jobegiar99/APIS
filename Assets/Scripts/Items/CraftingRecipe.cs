using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
        fileName = "CraftingRecipe",
        menuName = "ScriptableObjects/Items/Crafting Recipe",
        order = 2
)]
public class CraftingRecipe : ScriptableObject
{
        [SerializeField] public Dictionary<ItemInformation,int> items;
        [SerializeField] public int price;
        [SerializeField] public GameObject result;

        public bool checkIfCanCraft(PlayerBrain pBrain)
        {
                foreach (ItemInformation item in items.Keys)
                {
                        if (items[item] != pBrain.inventory[item])
                                return false;
                }
                return true;
        }

}
