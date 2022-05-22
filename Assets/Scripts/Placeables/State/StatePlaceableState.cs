using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlaceableState : State
{
        public new enum STATE
        {
                placeableSetTarget, placeableAttack,
                placeableCooldown, placeableNoAmmo
        }

        public new STATE state;
        public GameObject objective;
        public HostilePlaceable hostilePlaceable;

        public StatePlaceableState(GameObject myGo, GameObject obj)
                : base(myGo)
        {
                myGameObject = myGo;
                objective = obj;
                hostilePlaceable = myGameObject.GetComponent<HostilePlaceable>();
                
        }
}
