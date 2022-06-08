using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEnemyFlee : StateEnemyState
{
        int escapeFrames;

        float initialSuccessGuess;

        public StateEnemyFlee(GameObject go, GameObject obj)
                : base(go, obj) { }

        public override void Enter()
        {
                base.Enter();
                Debug.Log("FLEE");
                state = STATE.enemyFlee;
                escapeFrames = (int) (100 * brain.dna.fear);
                initialSuccessGuess = mathHelper.GetSuccessGuess();
        }

        public override void Update()
        {
                base.Update();
                if (escapeFrames == 0)
                {
                        stage = STAGE.Exit;
                        return;
                }
                Vector3 direction = myGameObject.transform.position - objective.transform.position;

                myGameObject.transform.position += direction * Time.deltaTime * controller.moveSpeed;

                escapeFrames--;
        }

        public override void Exit()
        {
                base.Exit();
                float currentSuccessRate = mathHelper.GetSuccessGuess();

                if (initialSuccessGuess <= currentSuccessRate && brain.dna.bravery >= currentSuccessRate)
                        nextState = new StateEnemyMoveToTarget(myGameObject, objective);

                else if (initialSuccessGuess <= currentSuccessRate && brain.dna.bravery >= currentSuccessRate)
                        nextState = new StateEnemyChooseTarget(myGameObject, objective);

                else if (initialSuccessGuess > currentSuccessRate)
                        nextState = new StateEnemyHeal(myGameObject, objective);

                else
                        nextState = new StateEnemyHeal(myGameObject, objective);
        }
}
