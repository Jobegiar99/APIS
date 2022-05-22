using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlaceableAttack : StatePlaceableState
{
        public StatePlaceableAttack(GameObject go,GameObject obj)
                : base(go, obj) { }

        public override void Enter()
        {
                base.Enter();

                for(int round = 0; round < hostilePlaceable.rounds; round++)
                {
                        int projectiles = hostilePlaceable.projectilesPerRound;
                        hostilePlaceable.canAttack = true;
                        while (projectiles >= 0)
                        {
                                if (!hostilePlaceable.canAttack)
                                        continue;

                                projectiles--;
                                hostilePlaceable.canAttack = false;

                                hostilePlaceable.StartCoroutine(
                                        hostilePlaceable.StopCooldown(hostilePlaceable.roundCooldown)
                                );

                                hostilePlaceable.CreateProjectile(objective);
                        }
                }
                stage = STAGE.Exit;
        }

        public override void Exit()
        {
                base.Exit();
                nextState = new StatePlaceableCooldown(myGameObject, objective);
        }

}
