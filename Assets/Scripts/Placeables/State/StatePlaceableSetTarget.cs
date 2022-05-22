using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlaceableSetTarget : StatePlaceableState
{
        public StatePlaceableSetTarget(GameObject go,GameObject obj)
                :base(go,obj) { }

        public override void Enter()
        {
                base.Enter();
                if (objective == null)
                {
                        FindObjective();
                }
                else
                {
                        float distance = Vector2.Distance(objective.transform.position, myGameObject.transform.position);
                        if (distance < hostilePlaceable.hostilePlaceableInfo.attackRange)
                        {
                                FindObjective();
                        }
                }
                stage = STAGE.Exit;
        }

        public override void Exit()
        {
                base.Exit();
                if (objective != null)
                        nextState = new StatePlaceableAttack(myGameObject, objective);
                else
                        nextState = new StatePlaceableCooldown(myGameObject, objective);
        }

        private void FindObjective()
        {
                GameObject enemyContainer = GameObject.Find("Enemy Container");
                int enemies = enemyContainer.transform.childCount;
                List<GameObject> enemyList = new List<GameObject>();
                for (int child = 0; child < enemies; child++)
                {
                        float distance = Vector2.Distance(
                                myGameObject.transform.position,
                                enemyContainer.transform.GetChild(child).position
                                );

                        if( distance <= hostilePlaceable.attackRange)
                        {
                                enemyList.Add(enemyContainer.transform.GetChild(child).gameObject);
                        }
                }
                if (enemyList.Count > 0)
                        objective = enemyList[Random.Range(0, enemyList.Count)];
        }


}
