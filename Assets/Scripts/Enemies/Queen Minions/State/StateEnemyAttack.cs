using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEnemyAttack : StateEnemyState
{
        float successGuess;
        public StateEnemyAttack(GameObject go, GameObject obj)
                : base(go, obj) 
        {
                mathHelper = new EnemyMathHelper(myGameObject, objective);
         }

        public override void Enter()
        {
                base.Enter();
                successGuess = mathHelper.GetSuccessGuess();
                controller = myGameObject.GetComponent<EnemyController>();
        }

        public override void Update()
        {
                base.Update();
                float distance = Vector2.Distance(myGameObject.transform.position, objective.transform.position);
                if (controller.attackRange < distance)
                {
                        stage = STAGE.Exit;
                }
                else
                {
                        if (controller.canAttack)
                                controller.Attack(objective);
                        float newSuccessGuess = mathHelper.GetSuccessGuess();
                        if (successGuess > newSuccessGuess)
                                nextState = new StateEnemyFlee(myGameObject, objective);

                }
        }

        public override void Exit()
        {
                base.Exit();
                Brain brain = myGameObject.GetComponent<Brain>();
                float tolerance = brain.dna.tolerance;
                float newSuccessGuess = mathHelper.GetSuccessGuess();

                if (tolerance * successGuess >= newSuccessGuess)
                        nextState = new StateEnemyMoveToTarget(myGameObject, objective);

                else 
                        nextState = new StateEnemyFlee(myGameObject, objective);
        }
}
