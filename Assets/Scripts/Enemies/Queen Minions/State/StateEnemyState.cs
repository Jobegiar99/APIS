using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEnemyState : State
{
        public enum STATE
        {
                enemyAttack, enemyMoveToTarget,
                enemyFlee, enemyHeal, enemyDie,
                enemyChooseTarget
        }
        public STATE state;
        public GameObject objective;

        public StateEnemyState(GameObject myGo, GameObject objt)
                :base(myGo)
        {
                this.objective = objt;
        }
}
