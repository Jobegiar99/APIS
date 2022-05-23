using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class StateEnemyMoveToTarget : StateEnemyState
{
        NavMeshAgent agent;
        float accuracy;
        float initialSuccessGuess;

        public StateEnemyMoveToTarget(GameObject go,GameObject obj)
                : base(go, obj) { }
       

        public override void Enter()
        {
                base.Enter();
                initialSuccessGuess = mathHelper.GetSuccessGuess();
                if (brain.dna.fear > initialSuccessGuess)
                        stage = STAGE.Exit;

                agent = myGameObject.GetComponent<NavMeshAgent>();
                agent.updateRotation = false;
                agent.updateUpAxis = false;
                accuracy = controller.attackRange / 2f;
                
        }

        public override void Update()
        {
                base.Update();
                float distance = Vector2.Distance(myGameObject.transform.position, objective.transform.position);
                if (distance > accuracy)
                        agent.SetDestination(objective.transform.position);
                else
                {
                        agent.isStopped = true;
                        stage = STAGE.Exit;
                }
        }

        public override void Exit()
        {
                base.Exit();
                if (TargetIsDead() || brain.dna.fear > initialSuccessGuess)
                        nextState = new StateEnemyChooseTarget(myGameObject, null);
                else
                        nextState = new StateEnemyAttack(myGameObject, objective);
               
        }

        private bool TargetIsDead()
        {
                PlayerBrain playerBrain = objective.GetComponent<PlayerBrain>();
                HostilePlaceable hostilePlaceable = objective.GetComponent<HostilePlaceable>();
                Placeable placeable = objective.GetComponent<Placeable>();
                bool playerDied = playerBrain != null && playerBrain.currentHP == 0;
                bool hostileDied = hostilePlaceable != null && hostilePlaceable.hp == 0;
                bool placeableDied = placeable != null && placeable.hp == 0;
                return playerBrain || hostileDied || placeableDied;
        }

}
