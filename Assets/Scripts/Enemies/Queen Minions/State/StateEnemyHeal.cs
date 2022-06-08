using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEnemyHeal : StateEnemyState
{        
        public StateEnemyHeal(GameObject go, GameObject obj)
                : base(go, obj) { }

        public override void Enter()
        {
                base.Enter();
                state = STATE.enemyHeal;
                Debug.Log("HEAL");
        }
        public override void Update()
        {
                base.Update();
                if (!controller.canHeal)
                        return;
                if (controller.hp != 100) {
                        controller.canHeal = false;
                        controller.StartCoroutine(controller.HealCooldown());
                        return;
                }
                stage = STAGE.Exit;
        }

        public override void Exit()
        {
                base.Exit();
                //remove heart
                Brain brain = myGameObject.GetComponent<Brain>();
                float successGuess = mathHelper.GetSuccessGuess();

                if (successGuess >= brain.dna.weakness)
                        nextState = new StateEnemyMoveToTarget(myGameObject, objective);
                else
                        nextState = new StateEnemyChooseTarget(myGameObject, null);
        }
}
