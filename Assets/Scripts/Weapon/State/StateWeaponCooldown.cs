using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateWeaponCooldown : StateWeaponState
{
        public StateWeaponCooldown(GameObject go)
                :base(go){}

        public override void Enter()
        {
                base.Enter();
                state = STATE.weaponCooldown;
                weaponController = myGameObject.GetComponent<WeaponController>();
                weaponController.canAttack = false;
                weaponController.StartCoroutine(weaponController.StopCooldown());
        }

        public override void Update()
        {
                base.Update();

                if (Input.GetKeyDown(KeyCode.Mouse0))
                        Debug.Log("RELOADING!");
                
                if (!weaponController.canAttack)
                        return;

                stage = STAGE.Exit;
        }

        public override void Exit()
        {
                base.Exit();
                nextState = new StateWeaponAttack(myGameObject);
        }

}
