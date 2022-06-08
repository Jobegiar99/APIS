using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWeaponAttack : StateWeaponState
{
        public StateWeaponAttack(GameObject go)
                :base(go){}

        public override void Enter()
        {
                base.Enter();
                stage = STAGE.Update;
                state = STATE.weaponAttack;
        }

        public override void Update()
        {
                base.Update();
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                        weaponController.weaponBehavior.ShootProjectile();
                        myGameObject.transform.parent.gameObject.GetComponent<Animator>().SetTrigger("player_attack");
                        stage = STAGE.Exit;
                }
        }

        public override void Exit()
        {
                base.Exit();
                if (myGameObject.GetComponent<WeaponController>().ammo > 0)
                        nextState = new StateWeaponCooldown(myGameObject);
                
                else
                        nextState = new StateWeaponNoAmmo(myGameObject);
        }
}
