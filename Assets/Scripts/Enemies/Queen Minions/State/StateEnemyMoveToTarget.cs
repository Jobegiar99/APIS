using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEnemyMoveToTarget : State
{
        protected GameObject target;

        public StateEnemyMoveToTarget(GameObject go)
                :base(go)
        {

        }

        public override void Enter()
        {
                base.Enter();
                //myGameObject.GetComponent<DNA>().GetGene(1);
        }
}
