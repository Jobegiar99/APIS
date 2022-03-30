using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWeaponState : State
{
        public enum STATE
        {
                weaponAttack,
                weaponReload,
                weaponCooldown,
                weaponNoAmmo
        }
        public STATE state;

        public StateWeaponState(GameObject myGo)
                : base(myGo)
        {
                this.myGameObject = myGo;
        }
}
