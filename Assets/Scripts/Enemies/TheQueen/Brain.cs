using UnityEngine;
using System.Collections.Generic;
public class Brain : MonoBehaviour
{
        public DNA dna;

        public float timeAlive = 0;
        public float productiveTime = 0;
        public float unprodictiveTime = 0;
        public bool alive = false;

        public StateEnemyState enemyState;
        public List<GameObject> fellowMinions;
        public EnemyMathHelper mathHelper;
        // Start is called before the first frame
        // 
        private void Start()
        {
                dna = new DNA(ref GameObject.Find("LevelManager").GetComponent<LevelManager>().info.entrances);
        }

        public void Init()
        {

                enemyState = new StateEnemyChooseTarget(this.gameObject, null);

                mathHelper = new EnemyMathHelper(gameObject, null);

        }

        public void BringToLife()
        {
                alive = true;
        }

        private void Update()
        {
                if (alive)
                {
                        enemyState = (StateEnemyState)(enemyState.Process());
                        timeAlive += Time.deltaTime;
                        StateEnemyState.STATE state = (StateEnemyState.STATE)enemyState.state;
                        if (state == StateEnemyState.STATE.enemyAttack
                                || state == StateEnemyState.STATE.enemyMoveToTarget
                                || state == StateEnemyState.STATE.enemyHeal)
                        {
                                productiveTime += Time.deltaTime;
                        }
                        else
                        {
                                unprodictiveTime += Time.deltaTime;
                        }
                }
               
        }
}
