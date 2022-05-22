using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEnemyState : State
{
        public new enum STATE
        {
                enemyAttack, enemyMoveToTarget,
                enemyFlee, enemyHeal, enemyDie,
                enemyChooseTarget
        }
        public new STATE state;
        public GameObject objective;

        public StateEnemyState(GameObject myGo, GameObject objt)
                :base(myGo)
        {
                this.objective = objt;
        }
}
