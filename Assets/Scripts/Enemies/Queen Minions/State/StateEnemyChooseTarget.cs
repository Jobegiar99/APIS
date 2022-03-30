using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEnemyChooseTarget : State
{
        protected GameObject target;

        public StateEnemyChooseTarget(GameObject go)
                : base(go)
        {
             
        }


        public override void Enter()
        {
                base.Enter();

        }

        public override void Exit()
        {
                base.Exit();
               // nextState = new StateEnemyMoveToTarget(myGameObject);
        }
}
