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
                for (int i = 0; i < 3; i++)
                        if(SelectTarget(brain.dna.objectives[i]))
                                break;              
        }

        public override void Exit()
        {
                base.Exit();
               nextState = new StateEnemyMoveToTarget(myGameObject,objective);
        }

        private bool SelectTarget(char currentOption)
        {
                switch(currentOption){
                        case 'P':
                                {
                                        objective = GameObject.Find("Player");
                                        return true;
                                }
                        case 'B':
                                {
                                        int beaconAmount = (int) GameObject.Find("LevelManager")
                                                .GetComponent<LevelManager>().info.beaconAmount;

                                        if (beaconAmount == 0)
                                                return false;

                                        objective = GameObject.Find("BeaconContainer")
                                                .transform.GetChild(Random.Range(0,beaconAmount))
                                                        .gameObject;

                                        return true;
                                             
                                }
                        case 'T':
                                {

                                        GameObject towerContainer = GameObject.Find("TowerContainer");

                                        int towerAmount = towerContainer.transform.childCount;

                                        if (towerAmount == 0)
                                                return false;

                                        objective = towerContainer.transform.GetChild(
                                                                Random.Range(0, towerAmount)
                                                ).gameObject;

                                        return true;
                                }
                }
                return false;
        }
}
