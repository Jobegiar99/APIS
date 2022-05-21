using UnityEngine;
using System.Collections.Generic;
public class Brain : MonoBehaviour
{
        public DNA dna;

        public float timeAlive = 0;
        public float productiveTime = 0;
        public float unprodictiveTime = 0;
        public bool alive = true;

        public StateEnemyState enemyState;
        // Start is called before the first frame update
        public void Init()
        {
                dna = new DNA(ref GameObject.Find("Main Camera").GetComponent<LevelInformation>().entrances);    
        }

        private void Update()
        {
                if (alive)
                {
                        timeAlive += Time.deltaTime;
                        if (enemyState.state == StateEnemyState.STATE.enemyAttack)
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
