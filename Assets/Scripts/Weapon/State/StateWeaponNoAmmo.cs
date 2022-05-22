using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWeaponNoAmmo : StateWeaponState
{
        public StateWeaponNoAmmo(GameObject go)
                : base(go){}

        public override void Update()
        {
                base.Update();
                if (Input.GetKeyDown(KeyCode.Mouse0))
                        Debug.Log("No ammo!");
                if (weaponController.ammo == 0)
                        return;
                stage = STAGE.Exit;
        }

        public override void Exit()
        {
                base.Exit();
                nextState = new StateWeaponAttack(myGameObject);
        }
}
