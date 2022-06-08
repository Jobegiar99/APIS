using UnityEngine;
using System.Collections.Generic;
public class Brain : MonoBehaviour
{
        public DNA dna;

        public float timeAlive = 0;
        public float productiveTime = 0;
        public float unprodictiveTime = 0;
        [System.NonSerialized] public bool alive;

        public StateEnemyState enemyState;
        public List<GameObject> fellowMinions;
        public EnemyMathHelper mathHelper;

        LevelManager levelManager;
        // Start is called before the first frame
        private void Start()
        {
                alive = false;
        }

        public void Init()
        {
                levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

                dna = new DNA(ref levelManager.levelInformation.entrances);
                
                enemyState = new StateEnemyChooseTarget(this.gameObject, null);


                mathHelper = new EnemyMathHelper(gameObject, null);

                gameObject.transform.Rotate(0.01f, 0, 0);
        }


        private void Update()
        {

                transform.rotation = Quaternion.identity;
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
