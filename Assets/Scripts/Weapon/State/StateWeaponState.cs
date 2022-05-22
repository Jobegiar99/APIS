using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWeaponState : State
{
        public  new enum STATE
        {
                weaponAttack,
                weaponReload,
                weaponCooldown,
                weaponNoAmmo
        }
        public new STATE state;
        public WeaponController weaponController;

        public StateWeaponState(GameObject myGo)
                : base(myGo)
        {
                this.myGameObject = myGo;
                this.weaponController = myGo.GetComponent<WeaponController>();
        }
}
