using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
        fileName = "PlaceableInformation",
        menuName = "ScriptableObjects/Placeables/Placeable Information",
        order = 1
)]
public class PlaceableInformation : ScriptableObject
{
        public string placeableName;
        public string description;
        public int hp;
        public Sprite icon;
        public string placeableType;
}
