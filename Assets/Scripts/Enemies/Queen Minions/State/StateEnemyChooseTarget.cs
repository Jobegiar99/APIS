using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEnemyChooseTarget : StateEnemyState
{

       
        public StateEnemyChooseTarget(GameObject go,GameObject obj)
                : base(go, obj) { }

        public override void Enter()
        {
                base.Enter();
                state = STATE.enemyChooseTarget;
                stage = STAGE.Exit;

        }

        public override void Exit()
        {
                
                base.Exit();
                nextState = new StateEnemyMoveToTarget(myGameObject,objective);
        }
}
