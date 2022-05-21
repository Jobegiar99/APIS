using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEnemyChooseTarget : State
{
        protected GameObject target;
       
        public StateEnemyChooseTarget(GameObject go)
                : base(go)
        {
                this.myGameObject = go;
                Brain brain = go.GetComponent<Brain>();
                
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

        private bool SelectTarget(char currentOption)
        {
                switch(currentOption){
                        case 'P':
                                {
                                        target = GameObject.Find("Player");
                                        return true;
                                }
                        case 'B':
                                {
                                        int beaconAmount = GameObject.Find("LevelSettings")
                                                .GetComponent<LevelInformation>()
                                                        .beaconAmount;

                                        target = GameObject.Find("BeaconContainer")
                                                .transform.GetChild(Random.Range(0, beaconAmount))
                                                        .gameObject;

                                        return true;
                                             
                                }
                }
                return false;
        }
}
