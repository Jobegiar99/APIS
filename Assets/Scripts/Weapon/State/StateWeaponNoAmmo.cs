using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWeaponNoAmmo : StateWeaponState
{
        public StateWeaponNoAmmo(GameObject go)
                : base(go){}

        public override void Enter()
        {
                base.Enter();
                myGameObject.transform.parent.gameObject.GetComponent<Animator>().SetTrigger("player_iddle");
                state = STATE.weaponNoAmmo;
        }
        public override void Update()
        {
                base.Update();
                
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
