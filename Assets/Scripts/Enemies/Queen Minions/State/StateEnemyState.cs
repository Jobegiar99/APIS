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
        public Brain brain;
        public EnemyController controller;
        public EnemyMathHelper mathHelper;

        public StateEnemyState(GameObject myGo, GameObject obj)
                :base(myGo)
        {
                this.myGameObject = myGo;
                this.objective = obj;
                this.brain = myGo.GetComponent<Brain>();
                this.controller = myGo.GetComponent<EnemyController>();
                this.mathHelper = brain.mathHelper;
                if(obj != null)
                        mathHelper.objective = obj;
        }
}
