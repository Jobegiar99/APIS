using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlaceableState : State
{
        public new enum STATE
        {
                placeableSetTarget, placeableAttack,
                placeableReload, placeableNoAmmo
        }

        public new STATE state;
        public GameObject objective;

        public StatePlaceableState(GameObject myGo, GameObject objective)
                : base(myGo)
        {
                myGameObject = myGo;
                this.objective = objective;
        }
}
